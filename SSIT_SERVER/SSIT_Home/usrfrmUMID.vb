
'Imports SSIT_UMID

Public Class usrfrmUMID

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            pbCode.Size = New Size(pbCode.Size.Width - 20, pbCode.Size.Height - 20)
            'Label1.Font = New Font(Label1.Font.Name, Label1.Font.Size - 2)
            Label1.Top = Label1.Top - 18
            Label1.Left = Label1.Left - 8
            lblMessage.Top = lblMessage.Top - 8
        End If

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private session As System.Threading.Thread

    Private ErrorMessage As String = ""

    Private CRN_1 As String
    Private CRN_2 As String

    'Dim td As New testData()

    Private Sub usrfrmUMID_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        SharedFunction.HouseKeeping()

        CheckForIllegalCrossThreadCalls = False

        'temp 04
        'bgWorker.RunWorkerAsync()

        session = New System.Threading.Thread(AddressOf SessionTimer)
        session.IsBackground = True
        session.Start()
    End Sub

#Region " Defaults "

    Private bln As Boolean = True
    Private intCntr As Short = readSettings(xml_Filename, xml_path, "UMIDCard_SessionTime")
    Private Delegate Sub Action()

    Private Sub SessionTimer()
        While bln
            If intCntr > 0 Then
                intCntr -= 1
                If CheckRequirements() Then
                    'MessageBox.Show("check requirement is success")
                    If Not bgWorker.IsBusy Then bgWorker.RunWorkerAsync()
                End If
                System.Threading.Thread.Sleep(1000)
            Else
                bln = False
                session = Nothing

                lblMessage.Text = ""
                Application.DoEvents()

                'SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                Dim msg As String = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch."

                If Not AllcardUMID.IsCardPresent Then msg = "No card detected.".ToUpper

                SharedFunction.ShowWarningMessage(msg.ToUpper)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "NO CARD DETECTED" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                CloseUserForm()
            End If
        End While
    End Sub

    Private Sub CloseUserForm()
        umidCard = Nothing
        bln = False
        session = Nothing
        isShowFutronic = False
        'SharedFunction.HouseKeeping()
        Invoke(New Action(AddressOf CloseFormPIN))
        Invoke(New Action(AddressOf CloseFormAuth))
        _frmMainMenu.DisposeForm(Form2)

        'SharedFunction.SaveActivityToDbase("UMIDCARD", "")
        Invoke(New Action(AddressOf SharedFunction.ShowMainDefaultUserForm))
    End Sub

    Private Sub RefreshForm()
        Invoke(New Action(AddressOf ShowUMIDUserForm))
    End Sub

    Private Sub ShowUMIDUserForm()
        SharedFunction.ShowMainNewUserForm(New usrfrmUMID)
    End Sub

    Private Sub ShowFingerprintValidation()
        Invoke(New Action(AddressOf ShowFingerprintValidationForm))
    End Sub

    Private IsAssignPIN As Boolean

    Private Sub ShowFingerprintValidationForm()
        If Not IsCardBlocked() Then
            If Not isGSISCard Then
                If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.nineteenInch Then
                    SharedFunction.ShowMainNewUserForm(New usrfrmFingerprintValidation(tempCRN, IsAssignPIN, Nothing))
                Else
                    SharedFunction.ShowMainNewUserForm(New usrfrmFingerprintValidation12(tempCRN, IsAssignPIN, Nothing))
                End If
            Else
                Dim umid As New umid
                If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.nineteenInch Then
                    SharedFunction.ShowMainNewUserForm(New usrfrmFingerprintValidation(tempCRN, IsAssignPIN, umidCard))
                Else
                    SharedFunction.ShowMainNewUserForm(New usrfrmFingerprintValidation12(tempCRN, IsAssignPIN, umidCard))
                End If

            End If
        Else
            bln = False
            session = Nothing
            CloseUserForm()
        End If
    End Sub

#End Region

    Private lstBoxLog As New ListBox

    Private Sub bgWorker_DoWork(ByVal sender As System.Object, ByVal e As System.ComponentModel.DoWorkEventArgs) Handles bgWorker.DoWork
        'ReadActivateCard2()
        ReadUmidCard()
    End Sub

    Private Sub LabelStatus(ByVal strMsg As String)
        lblMessage.Text = strMsg
        Application.DoEvents()
    End Sub

    Dim tempCRN As String = ""

    Private Sub GetUMIDLogs(ByVal arrSystem As ArrayList, ByVal arrError As ArrayList, ByVal result As String)
        Select Case result
            Case "00", "02", "03"
                For Each strLog As String In arrSystem
                    SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & strLog & "|" & kioskIP & "|" & getbranchCoDE_1)
                Next
            Case Else
                For Each strLog As String In arrError
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & strLog & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                Next
        End Select
    End Sub

    Private Sub TempoUMIDLog(ByVal data As String)
        Dim sw As New System.IO.StreamWriter("TempUMIDLog.txt", True)
        sw.Write(Now.ToString("MM/dd/yyyy hh:mm:ss tt ") & data & vbNewLine)
        sw.Close()
        sw.Dispose()
        sw = Nothing
    End Sub

    Dim sbTest As New System.Text.StringBuilder

    Private Sub ReadUmidCard()
        isGSISCard = False
        bln = False
        LabelStatus("Reading details, please do not remove your card...")

        Dim umid As New umid()
        If umid.ReadData(True,) Then
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0}, CCDT {1}, STATUS {2}, S36 {3}", umid.crn, umid.ccdt, umid.cardStatus, umid.sssSector36) & "|" & kioskIP & "|" & getbranchCoDE_1)

            If umid.cardStatusCode = "9" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " is blocked" & "|" & kioskIP & "|" & getbranchCoDE_1)

                lblMessage.Text = ""
                Dim msg As String = "The card is blocked, the member should undergo re-enrollment."
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)

                bln = False
                session = Nothing
                CloseUserForm()
                Return
            End If

            If umid.csn <> "" Then isGSISCard = umid.isGsisCard

            Dim isReadFingerprints As Boolean = True

            If umid.appletVersion = umid.appVersion.newApplet Then
                isReadFingerprints = umid.ReadFingerprints
            ElseIf umid.cardStatusCode = "0" Then
                isReadFingerprints = umid.ReadFingerprints
            End If

            If isReadFingerprints Then
                sbTest.Append("Reading fingerprint success" & vbNewLine)

                If Not isGSISCard Then
                    'temp
                    'SharedFunction.replaceCardData(umid.crn, umid.crn, umid.sssSector36, umid.ccdt)

                    'MessageBox.Show("Reading sss")
                    ReadSSSCard(umid.crn, umid.ccdt, umid.cardStatus, umid.sssSector36)
                Else
                    If umid.csn <> "" Then
                        ReadGSISCard(umid)
                    Else
                        SharedFunction.InvokeSystemMessage(Me, "Your UMID card is not the latest card. Please seek assistance from our frontline service counter of the nearest SSS Branch.".ToUpper, SystemMessage.MsgType.Warning)
                        bln = False
                        session = Nothing
                        CloseUserForm()
                    End If
                End If
            Else
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " failed to read fingerprints." & umid.Exception & "|" & kioskIP & "|" & getbranchCoDE_1)
                SharedFunction.InvokeSystemMessage(Me, "FAILED TO READ FINGERPRINT(S) FROM CARD. PLEASE TRY AGAIN.", SystemMessage.MsgType.Warning)
                bln = False
                session = Nothing
                CloseUserForm()
            End If



        Else
            Dim msg As String = ""
            If Not umid.isAppletSelected Then
                msg = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch."
            Else
                msg = "FAILED TO AUTHENTICATE SL 1"
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "CRN " & tempCRN & " - SL1 failed" & "|" & kioskIP & "|" & getbranchCoDE_1)
            End If

            SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
            bln = False
            session = Nothing
            CloseUserForm()
        End If
    End Sub

    Private Function IsCardBlocked() As Boolean
        Dim SS_Number As String = readSettings(xml_Filename, xml_path, "SS_Number")
        Dim CardBlock As New CardBlock(SS_Number)
        If CardBlock.isBlocked Then
            CardBlock = Nothing
            Dim msg As String = "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day."
            SharedFunction.ShowWarningMessage(msg.ToUpper)
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub ReadGSISCard(ByVal umidData As umid)
        If umidData.ReadData(False, "123456") Then
            Dim umidService As New SSUmidService
            Dim dob As String = String.Format("{0}/{1}/{2}", umidData.dateOfBirth.Substring(4, 2), umidData.dateOfBirth.Substring(6, 2), umidData.dateOfBirth.Substring(0, 4))
            If umidService.isSSSMember(umidData.crn, dob, umidData.firstName, umidData.middleName, umidData.lastName, umidData.suffix) Then
                If umidService.webserviceResponse.processFlag <> "0" Then
                    If DeviceConnectivity.IsFingerprintScannerPresent Then
                        umidData.sssNumber = umidService.webserviceResponse.ssnumber
                        editSettings(xml_Filename, xml_path, "SS_Number", umidData.sssNumber)
                        umidCard = umidData
                        SharedFunction.CreateCardData(2, umidData.crn & "|")
                        session = Nothing
                        ShowFingerprintValidation()
                    Else
                        Dim msg As String = "You cannot proceed, finger scanner is either defective or disconnected."
                        SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
                        bln = False
                        session = Nothing
                        CloseUserForm()
                    End If
                Else
                    'Dim msg As String = "For SSS members with confirmed CVS rejected card applications only, please seek assistance at the MSS."
                    Dim msg As String = "YOUR UMID CARD IS NOT THE LATEST CARD. PLEASE SEEK ASSISTANCE FROM OUR FRONTLINE SERVICE COUNTER OF THE NEAREST SSS BRANCH."
                    SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
                    bln = False
                    session = Nothing
                    CloseUserForm()
                End If
            Else
                SharedFunction.ShowUnableToConnectToRemoteServerMessage()
                bln = False
                session = Nothing
                CloseUserForm()
            End If
        Else
            SharedFunction.ShowWarningMessage("FAILED TO READ UMID CARD. PLEASE REMOVE AND PLACE YOUR CARD AGAIN. ")
            bln = False
            session = Nothing
            CloseUserForm()
        End If
    End Sub

    Private Sub ReadSSSCard(ByVal crn As String, ByVal ccdt As String, ByVal cardStatus As String, ByVal sector36 As String)
        bln = False

        tempCRN = crn

        'MessageBox.Show(sbTest.ToString())
        LabelStatus("Reading details, please do not remove your card...")


        Dim _Sector36 As String = sector36

        Dim blnUMIDPERSO As Boolean = True

        If blnUMIDPERSO Then
            If tempCRN.Trim.Length = 12 Then _
                tempCRN = tempCRN.Substring(0, 4) + "-" + tempCRN.Substring(4, 7) + "-" + tempCRN.Substring(11, 1)

            CRN_1 = tempCRN.Replace("-", "")

            editSettings(xml_Filename, xml_path, "CRN", tempCRN.Replace("-", ""))
            editSettings(xml_Filename, xml_path, "CCDT", ccdt)

            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card CRN " & tempCRN & "|" & kioskIP & "|" & getbranchCoDE_1)

            If cardStatus.ToUpper.Trim = "CARD_BLOCKED" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " is blocked" & "|" & kioskIP & "|" & getbranchCoDE_1)

                lblMessage.Text = ""
                Dim msg As String = "The card is blocked, the member should undergo re-enrollment."
                'SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.ErrorMsg)
                'SharedFunction.ShowWarningMessage(msg)
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)

                bln = False
                session = Nothing
                CloseUserForm()
                Exit Sub
            End If

            If cardStatus.ToUpper.Trim = "CARD_INACTIVE" And sector36 = "" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " status is false" & "|" & kioskIP & "|" & getbranchCoDE_1)

                Dim SSNum As String = ""
                Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, ccdt, 1, SSNum, "")
                'TempoUMIDLog(String.Format("CARD_INACTIVE CHECKIFRECENT {0}, CRN {1}, CCDT {2}, SSNum {3}", intResult.ToString, tempCRN, ccdt, SSNum))
                sbTest.Append("CARD_INACTIVE" & vbNewLine)
                sbTest.Append("CHECKIFRECENT RESULT " & intResult.ToString & vbNewLine)
                'MessageBox.Show(sbTest.ToString)

                Select Case intResult
                    Case 0
                        If SSNum <> "" Then
                            editSettings(xml_Filename, xml_path, "SS_Number", SSNum)

                            Dim result As DialogResult
                            SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                            If result = DialogResult.Yes Then
                                Dim _CRN2 As String = AllcardUMID.GetCRN(ErrorMessage)
                                '_CRN2 = CRN_1
                                If _CRN2 <> "" Then
                                    If CRN_1 <> _CRN2 Then
                                        SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CANNOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                If Not ActivateCardv2(tempCRN, "123456") Then
                                    If Not ActivateCardv2(tempCRN, "12345678") Then
                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    Else
                                        IsAssignPIN = True
                                        ReadUMIDCard_NotActive()
                                        Exit Sub
                                    End If
                                Else
                                    IsAssignPIN = True
                                    ReadUMIDCard_NotActive()
                                    Exit Sub
                                End If
                            Else
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Else
                            SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        End If
                    Case 1, 2
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                    Case Else
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                End Select
            End If

            Dim isCardInactiveButWithSSSNum As Boolean = False
            If cardStatus.ToUpper.Trim = "CARD_INACTIVE" And sector36 <> "" Then
                isCardInactiveButWithSSSNum = True
            ElseIf cardStatus.ToUpper.Trim = "CARD_ACTIVE" Then
                isCardInactiveButWithSSSNum = True
            End If

            'If cardStatus.ToUpper.Trim = "CARD_ACTIVE" Then
            If isCardInactiveButWithSSSNum Then
                Dim _CRN As String = readSettings(xml_Filename, xml_path, "CRN")
                Dim _CCDT As String = readSettings(xml_Filename, xml_path, "CCDT")

                'Dim SSNum2 As String = ""
                'Dim PIN2 As String = ""
                'Dim intResult2 As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, _CCDT, 2, SSNum2, PIN2)

                If _Sector36 <> "Error" Then
                    Dim SSNum As String = ""
                    Dim PIN As String = ""

                    Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, _CCDT, 2, SSNum, PIN)
                    SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("Card is active. CHECKIFRECENT result {0}, ", intResult.ToString) & "|" & kioskIP & "|" & getbranchCoDE_1)

                    Select Case intResult
                        Case 0
                            If SSNum <> "" Then
                                editSettings(xml_Filename, xml_path, "SS_Number", SSNum)
                                editSettings(xml_Filename, xml_path, "CardPin", PIN)

                                If _Sector36 = "NO_DATA" Then
                                    Dim result As DialogResult
                                    SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                                    If result = DialogResult.Yes Then
                                        Dim _CRN2 As String = AllcardUMID.GetCRN
                                        If _CRN2 <> "" Then
                                            If CRN_1 <> _CRN2 Then
                                                SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                                bln = False
                                                session = Nothing
                                                CloseUserForm()
                                                Exit Sub
                                            End If
                                        End If

                                        LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                        IsAssignPIN = True
                                        ReadUMIDCard_NotActive()
                                        Exit Sub
                                    Else
                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                IsAssignPIN = False
                                GetMemberPIN_and_ReadCard(False, False)
                            Else
                                SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Case 1, 2
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        Case 3
                            IsAssignPIN = True
                            ReadUMIDCard_NotActive()
                            Exit Sub
                        Case Else
                            bln = False
                            session = Nothing
                            Dim msg As String = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch."
                            SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
                            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CHECKIFRECENT result {0}, ", intResult.ToString) & "|" & kioskIP & "|" & getbranchCoDE_1)
                            'SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR YOUR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

                            CloseUserForm()
                            Exit Sub
                    End Select
                Else
                    GetMemberPIN_and_ReadCard(True, True)
                End If
            End If

            If cardStatus = "Error" Then
                    SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0} Status is error.", tempCRN) & "|" & kioskIP & "|" & getbranchCoDE_1)
                    SharedFunction.InvokeSystemMessage(Me, "PLEASE REMOVE AND PLACE AGAIN YOUR CARD ON CARD READER", SystemMessage.MsgType.ErrorMsg)

                    bln = False
                    session = Nothing
                    CloseUserForm()
                    Exit Sub
                ElseIf cardStatus = "" Then
                    SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0} Status is empty.", tempCRN) & "|" & kioskIP & "|" & getbranchCoDE_1)
                    SharedFunction.InvokeSystemMessage(Me, "PLEASE REMOVE AND PLACE AGAIN YOUR CARD ON CARD READER", SystemMessage.MsgType.ErrorMsg)

                    bln = False
                    session = Nothing
                    CloseUserForm()
                    Exit Sub
                End If
            Else
                bln = False
            session = Nothing

            'Dim msg As String = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch"
            Dim msg As String = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch."
            SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
            CloseUserForm()
        End If
    End Sub

    Private Sub ValidateCheckIfRecentResponse(ByVal intResult As Short, ByVal SSNum As String)

    End Sub

    Private Sub ReadActivateCard2()
        bln = False

        LabelStatus("Reading details, please do not remove your card...")
        'TempoUMIDLog("ReadActivateCard2: Reading...")

        'CHECK ACTIVATION
        '==================================
        Dim Card_Status As String = ""
        Dim CCDT As String = ""

        Dim _Sector36 As String = "" 'AllcardUMID.ReadSector36(ErrorMessage)

        Dim blnUMIDPERSO As Boolean = AllcardUMID.GetCRNandCCDTandStatus2(tempCRN, CCDT, Card_Status, _Sector36)
        TempoUMIDLog(String.Format("blnUMIDPERSO {0}, CRN {1}, CCDT {2}, Card_Status {3}, card Sector36 {4}", blnUMIDPERSO.ToString, tempCRN, CCDT, Card_Status, _Sector36))

        If blnUMIDPERSO Then
            If tempCRN.Trim.Length = 12 Then _
                tempCRN = tempCRN.Substring(0, 4) + "-" + tempCRN.Substring(4, 7) + "-" + tempCRN.Substring(11, 1)

            CRN_1 = tempCRN.Replace("-", "")

            editSettings(xml_Filename, xml_path, "CRN", tempCRN.Replace("-", ""))
            editSettings(xml_Filename, xml_path, "CCDT", CCDT)

            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card CRN " & tempCRN & "|" & kioskIP & "|" & getbranchCoDE_1)

            If Card_Status.ToUpper.Trim = "CARD_BLOCKED" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " is blocked" & "|" & kioskIP & "|" & getbranchCoDE_1)

                lblMessage.Text = ""
                'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                Dim msg As String = "The card is blocked, the member should undergo re-enrollment."
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
                'SharedFunction.ShowWarningMessage(msg.ToUpper)

                bln = False
                session = Nothing
                CloseUserForm()
                Exit Sub
            End If

            If Card_Status.ToUpper.Trim = "CARD_INACTIVE" Then
                'SharedFunction.ShowMessage("MALI - " & Card_Status)

                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " status is false" & "|" & kioskIP & "|" & getbranchCoDE_1)

                Dim SSNum As String = ""
                'Dim CCDT As String = readSettings(xml_Filename, xml_path, "CCDT")
                Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, CCDT, 1, SSNum, "")
                TempoUMIDLog(String.Format("CARD_INACTIVE CHECKIFRECENT {0}, CRN {1}, CCDT {2}, SSNum {3}", intResult.ToString, tempCRN, CCDT, SSNum))

                Select Case intResult
                    Case 0
                        If SSNum <> "" Then
                            editSettings(xml_Filename, xml_path, "SS_Number", SSNum)

                            Dim result As DialogResult
                            SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                            If result = DialogResult.Yes Then
                                Dim _CRN2 As String = AllcardUMID.GetCRN(ErrorMessage)
                                '_CRN2 = CRN_1
                                If _CRN2 <> "" Then
                                    If CRN_1 <> _CRN2 Then
                                        SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CANNOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                If Not ActivateCard2_bak(tempCRN) Then
                                    bln = False
                                    session = Nothing
                                    CloseUserForm()
                                    Exit Sub
                                Else
                                    IsAssignPIN = True
                                    ReadUMIDCard_NotActive()
                                    Exit Sub
                                End If
                            Else
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Else
                            SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        End If
                    Case 1, 2
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                    Case Else
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                End Select
            End If

            If Card_Status.ToUpper.Trim = "CARD_ACTIVE" Then
                Dim _CRN As String = readSettings(xml_Filename, xml_path, "CRN")
                Dim _CCDT As String = readSettings(xml_Filename, xml_path, "CCDT")

                'Dim _Sector36 As String = AllcardUMID.ReadSector36(ErrorMessage)

                If _Sector36 <> "Error" Then
                    Dim SSNum As String = ""
                    Dim PIN As String = ""

                    Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, _CCDT, 2, SSNum, PIN)
                    TempoUMIDLog(String.Format("CARD_ACTIVE CHECKIFRECENT {0}, CRN {1}, CCDT {2}, SSNum {3}, PIN {4}", intResult.ToString, tempCRN, CCDT, SSNum, PIN))

                    'comment out if reader have no sam that can read both old and new umid card
                    '_Sector36 = SSNum
                    'SharedFunction.ShowInfoMessage(PIN)

                    Select Case intResult
                        Case 0
                            If SSNum <> "" Then
                                editSettings(xml_Filename, xml_path, "SS_Number", SSNum)
                                editSettings(xml_Filename, xml_path, "CardPin", PIN)

                                If _Sector36 = "NO_DATA" Then
                                    Dim result As DialogResult
                                    SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                                    If result = DialogResult.Yes Then
                                        Dim _CRN2 As String = AllcardUMID.GetCRN
                                        If _CRN2 <> "" Then
                                            If CRN_1 <> _CRN2 Then
                                                SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                                bln = False
                                                session = Nothing
                                                CloseUserForm()
                                                Exit Sub
                                            End If
                                        End If

                                        LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                        IsAssignPIN = True
                                        ReadUMIDCard_NotActive()
                                        Exit Sub
                                    Else
                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                IsAssignPIN = False
                                GetMemberPIN_and_ReadCard(False, False)
                            Else
                                SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Case 1, 2
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        Case 3
                            IsAssignPIN = True
                            ReadUMIDCard_NotActive()
                            Exit Sub
                        Case Else
                            bln = False
                            session = Nothing
                            TempoUMIDLog("intResult is " & intResult)
                            SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR YOUR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                            CloseUserForm()
                            Exit Sub
                    End Select
                Else
                    GetMemberPIN_and_ReadCard(True, True)
                End If
            End If

            If Card_Status = "Error" Then
                TempoUMIDLog("Card_Status is error")
                SharedFunction.InvokeSystemMessage(Me, "PLEASE REMOVE AND PLACE AGAIN YOUR CARD ON CARD READER", SystemMessage.MsgType.ErrorMsg)

                bln = False
                session = Nothing
                CloseUserForm()
                Exit Sub
            End If
        Else
            bln = False
            session = Nothing
            'TempoUMIDLog("blnUMIDPERSO is false")
            'SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR YOUR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

            Dim msg As String = "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch."
            SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
            CloseUserForm()
        End If
    End Sub

    Private Sub ReadActivateCard3()
        bln = False

        LabelStatus("Reading details, please do not remove your card...")

        'CHECK ACTIVATION
        '==================================
        Dim Card_Status As String = ""
        Dim CCDT As String = ""

        Dim _Sector36 As String = "" 'AllcardUMID.ReadSector36(ErrorMessage)

        'Dim blnUMIDPERSO As Boolean = AllcardUMID.GetCRNandCCDTandStatus2(tempCRN, CCDT, Card_Status, _Sector36)

        'added by edel 04/13/2018 for testing
        Dim blnUMIDPERSO As Boolean = True
        'tempCRN = "011107931000"
        'CCDT = "20130703"
        'Card_Status = "CARD_ACTIVE"
        '_Sector36 = "0226879523"

        tempCRN = TextBox1.Text
        CCDT = TextBox2.Text
        Card_Status = "CARD_ACTIVE"
        _Sector36 = TextBox3.Text


        'SharedFunction.ShowMessage(String.Format("{0},{1},{2},{3}", tempCRN, CCDT, Card_Status, _Sector36))

        If blnUMIDPERSO Then
            If tempCRN.Trim.Length = 12 Then _
                tempCRN = tempCRN.Substring(0, 4) + "-" + tempCRN.Substring(4, 7) + "-" + tempCRN.Substring(11, 1)

            CRN_1 = tempCRN.Replace("-", "")

            editSettings(xml_Filename, xml_path, "CRN", tempCRN.Replace("-", ""))
            editSettings(xml_Filename, xml_path, "CCDT", CCDT)

            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card CRN " & tempCRN & "|" & kioskIP & "|" & getbranchCoDE_1)

            If Card_Status.ToUpper.Trim = "CARD_BLOCKED" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " is blocked" & "|" & kioskIP & "|" & getbranchCoDE_1)

                lblMessage.Text = ""
                'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                Dim msg As String = "The card is blocked, the member should undergo re-enrollment."
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)

                bln = False
                session = Nothing
                CloseUserForm()
                Exit Sub
            End If

            If Card_Status.ToUpper.Trim = "CARD_INACTIVE" Then
                'SharedFunction.ShowMessage("MALI - " & Card_Status)

                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Card with CRN " & tempCRN & " status is false" & "|" & kioskIP & "|" & getbranchCoDE_1)

                Dim SSNum As String = ""
                'Dim CCDT As String = readSettings(xml_Filename, xml_path, "CCDT")
                Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, CCDT, 1, SSNum, "")

                Select Case intResult
                    Case 0
                        If SSNum <> "" Then
                            editSettings(xml_Filename, xml_path, "SS_Number", SSNum)

                            Dim result As DialogResult
                            SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                            If result = DialogResult.Yes Then
                                Dim _CRN2 As String = "" 'AllcardUMID.GetCRN(ErrorMessage)
                                _CRN2 = CRN_1
                                If _CRN2 <> "" Then
                                    If CRN_1 <> _CRN2 Then
                                        SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CANNOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                If Not ActivateCard2_bak(tempCRN) Then
                                    bln = False
                                    session = Nothing
                                    CloseUserForm()
                                    Exit Sub
                                Else
                                    IsAssignPIN = True
                                    ReadUMIDCard_NotActive()
                                    Exit Sub
                                End If
                            Else
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Else
                            SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        End If
                    Case 1, 2
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                    Case Else
                        bln = False
                        session = Nothing
                        CloseUserForm()
                        Exit Sub
                End Select
            End If

            If Card_Status.ToUpper.Trim = "CARD_ACTIVE" Then
                Dim _CRN As String = readSettings(xml_Filename, xml_path, "CRN")
                Dim _CCDT As String = readSettings(xml_Filename, xml_path, "CCDT")

                'Dim _Sector36 As String = AllcardUMID.ReadSector36(ErrorMessage)

                If _Sector36 <> "Error" Then
                    Dim SSNum As String = ""
                    Dim PIN As String = ""

                    Dim intResult As Short = SharedFunction.CHECKIFRECENT(Me, tempCRN, _CCDT, 2, SSNum, PIN)

                    'comment out if reader have no sam that can read both old and new umid card
                    '_Sector36 = SSNum
                    'SharedFunction.ShowInfoMessage(PIN)

                    Select Case intResult
                        Case 0
                            If SSNum <> "" Then
                                editSettings(xml_Filename, xml_path, "SS_Number", SSNum)
                                'MessageBox.Show(PIN)
                                editSettings(xml_Filename, xml_path, "CardPin", PIN)

                                If _Sector36 = "NO_DATA" Then

                                    Dim result As DialogResult
                                    SharedFunction.InvokeSystemMessage2(Me, "YOUR UMID CARD IS NOT YET ACTIVATED FOR SSS USE." & vbNewLine & vbNewLine & "DO YOU WANT TO ACTIVATE YOUR CARD?", SystemMessage.MsgType.QuestionMsg, result)
                                    If result = DialogResult.Yes Then
                                        Dim _CRN2 As String = AllcardUMID.GetCRN
                                        If _CRN2 <> "" Then
                                            If CRN_1 <> _CRN2 Then
                                                SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                                                bln = False
                                                session = Nothing
                                                CloseUserForm()
                                                Exit Sub
                                            End If
                                        End If

                                        LabelStatus("WARNING! PLEASE DO NOT REMOVE YOUR CARD")

                                        IsAssignPIN = True
                                        ReadUMIDCard_NotActive()
                                        Exit Sub
                                    Else
                                        bln = False
                                        session = Nothing
                                        CloseUserForm()
                                        Exit Sub
                                    End If
                                End If

                                ''temp 10/26/2018
                                'Dim sw As New System.IO.StreamWriter("test_data.txt", True)
                                'sw.WriteLine(String.Format("CRN: {0}, SSS: {1}, CCDT: {2}, PIN: {3}", tempCRN, SSNum, _CCDT, PIN))
                                'sw.Dispose()
                                'sw.Close()

                                IsAssignPIN = False
                                GetMemberPIN_and_ReadCard(False, False)
                            Else
                                SharedFunction.InvokeSystemMessage(Me, "NO SS NUMBER FOUND FOR CRN " & tempCRN, SystemMessage.MsgType.ErrorMsg)
                                bln = False
                                session = Nothing
                                CloseUserForm()
                                Exit Sub
                            End If
                        Case 1, 2
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        Case 3
                            IsAssignPIN = True
                            ReadUMIDCard_NotActive()
                            Exit Sub
                        Case Else
                            bln = False
                            session = Nothing
                            SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR YOUR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                            CloseUserForm()
                            Exit Sub
                    End Select
                Else
                    GetMemberPIN_and_ReadCard(True, True)
                End If
            End If

            If Card_Status = "Error" Then
                SharedFunction.InvokeSystemMessage(Me, "PLEASE REMOVE AND PLACE AGAIN YOUR CARD ON CARD READER", SystemMessage.MsgType.ErrorMsg)

                bln = False
                session = Nothing
                CloseUserForm()
                Exit Sub
            End If
        Else
            bln = False
            session = Nothing
            SharedFunction.InvokeSystemMessage(Me, "YOU PLACED A NON-UMID CARD OR YOUR UMID CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
            CloseUserForm()
        End If
    End Sub

    Private Sub GetMemberPIN_and_ReadCard(ByVal IsCheckSSSSector As Boolean, ByVal IsCheckIsValid As Boolean)
        Dim PIN As String = readSettings(xml_Filename, xml_path, "CardPin")
        ReadUMIDCard(IsCheckSSSSector, IsCheckIsValid, PIN)
    End Sub

    Private Sub ReadUMIDCard_NotActive()
        'SharedFunction.ShowMessage("ReadUMIDCard_NotActive")

        'continue reading UMID
        Dim blnRead As Boolean = True 'AllcardUMID.GetCRNAndFingerprints(CRN_2, ErrorMessage)

        CRN_2 = CRN_1

        If CRN_1 <> CRN_2 Then
            SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

            bln = False
            session = Nothing
            CloseUserForm()
            Exit Sub
            'Else
            '    AppletVersion = AllcardUMID.CheckAppVersion(ErrorMessage)
        End If

        If blnRead Then
            SharedFunction.CreateCardData(2, CRN_2 & "|")

            session = Nothing
            ShowFingerprintValidation()
        Else
            lblMessage.Text = ""
            If lblMessage.Text.ToUpper = "UNABLE TO AUTHENTICATE PIN..." Then
                'SharedFunction.InvokeSystemMessage(Me, "UNABLE TO AUTHENTICATE PIN." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                SharedFunction.InvokeSystemMessage(Me, "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day.".ToUpper, SystemMessage.MsgType.ErrorMsg)
            Else
                SharedFunction.InvokeSystemMessage(Me, "FAILED TO READ CARD." & vbNewLine & vbNewLine & "PLEASE REMOVE CARD AND TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
            End If

            bln = False
            session = Nothing
            CloseUserForm()
        End If
    End Sub

    Private frmSelectAuthOption As frmSelectAuth

    Private Sub ReadUMIDCard(ByVal IsCheckSSSSector As Boolean, ByVal IsCheckIsValid As Boolean, ByVal PIN As String)
        If SharedFunction.CHECK_MEMSTATUS(Me, tempCRN) <> "OK" Then
            bln = False
            session = Nothing
            CloseUserForm()
        Else
            'get counter
            If Not CheckIfCounterIsValid() Then
                bln = False
                session = Nothing
                CloseUserForm()
            Else
                'continue reading UMID
                Dim blnRead As Boolean = True 'AllcardUMID.GetCRNAndFingerprints(CRN_2, ErrorMessage)
                'If AppletVersion = "2" Then AllcardUMID.GetCRNAndFingerprints2(CRN_2, ErrorMessage)

                CRN_2 = CRN_1

                If blnRead Then
                    SharedFunction.CreateCardData(2, CRN_2 & "|")

                    If CRN_1 <> CRN_2 Then
                        SharedFunction.InvokeSystemMessage(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                        bln = False
                        session = Nothing
                        CloseUserForm()
                        'Else
                        '    AppletVersion = AllcardUMID.CheckAppVersion(ErrorMessage)
                    End If

                    lblMessage.Text = "WARNING! PLEASE DO NOT REMOVE YOUR CARD"
                    Application.DoEvents()

                    frmSelectAuthOption = New frmSelectAuth
                    frmSelectAuthOption.ShowDialog()
                    Select Case frmSelectAuthOption.SelectedAuthType
                        Case 0
                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        Case 1
                            Invoke(New Action(AddressOf ShowUserFormPIN))

                            Dim isAuthPinOk As Boolean = (AuthenticationPIN = readSettings(xml_Filename, xml_path, "CardPin"))

                            If IO.File.Exists(Application.StartupPath & "\nopin.txt") Then isAuthPinOk = True

                            'temporary Nov 25, 2020. return to previous code when done testing
                            'If AuthenticationPIN = readSettings(xml_Filename, xml_path, "CardPin") Then ' My.Settings.CardPIN Then
                            If isAuthPinOk Then ' My.Settings.CardPIN Then
                                Dim blnResult As Boolean
                                'ValidateCardInReader(blnResult)
                                'If Not blnResult Then Exit Sub
                                blnResult = True

                                Invoke(New Action(AddressOf OpenSSIT_Member))
                            Else
                                If AuthenticationATTMPTCNTR = 0 Then
                                    bln = False
                                    session = Nothing
                                    CloseUserForm()
                                    Exit Sub
                                ElseIf AuthenticationATTMPTCNTR >= 4 Then
                                    Dim blnResult As Boolean
                                    'ValidateCardInReader(blnResult)
                                    'If Not blnResult Then Exit Sub
                                    blnResult = True

                                    'If Not SharedFunction.BlockUMIDCard(tempCRN.Replace("-", ""), PIN) Then
                                    '    If Not SharedFunction.BlockUMIDCard(tempCRN.Replace("-", ""), PIN) Then
                                    '        If Not SharedFunction.BlockUMIDCard(tempCRN.Replace("-", ""), PIN) Then
                                    '            bln = False
                                    '            session = Nothing

                                    '            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "FAILED TO BLOCK UMID CARD" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                                    '            CloseUserForm()
                                    '            Exit Sub
                                    '        Else
                                    '            Dim DAL As New DAL_Mssql
                                    '            If Not DAL.InsertAUTHFAILED(tempCRN.Replace("-", ""), AuthenticationATTMPTCNTR, False, "UMIDCARD") Then
                                    '                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Match(): Failed to insert CRN " & tempCRN & ". Error: " & DAL.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                                    '            End If
                                    '            DAL.Dispose()
                                    '        End If
                                    '    Else
                                    '        Dim DAL As New DAL_Mssql
                                    '        If Not DAL.InsertAUTHFAILED(tempCRN.Replace("-", ""), AuthenticationATTMPTCNTR, False, "UMIDCARD") Then
                                    '            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Match(): Failed to insert CRN " & tempCRN & ". Error: " & DAL.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                                    '        End If
                                    '        DAL.Dispose()
                                    '    End If
                                    'Else
                                    Dim DAL As New DAL_Mssql
                                    If Not DAL.InsertAUTHFAILED(tempCRN.Replace("-", ""), AuthenticationATTMPTCNTR, False, "UMIDCARD") Then
                                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Match(): Failed to insert CRN " & tempCRN & ". Error: " & DAL.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                                    End If
                                    DAL.Dispose()
                                    'End If

                                    Try
                                        Dim str As String = readSettings(xml_Filename, xml_path, "CARD_DATA")

                                        Dim name() As String = str.Split("|")

                                        Dim p As New PrintHelper2
                                        p.prt(p.prtAuthFailedPrint(name(4).Trim & ", " & name(2).Trim & " " & name(3).Trim, tempCRN), DefaultPrinterName)
                                    Catch ex As Exception
                                    End Try

                                    'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                                    SharedFunction.InvokeSystemMessage(Me, "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day.".ToUpper, SystemMessage.MsgType.Warning)
                                End If
                            End If

                            bln = False
                            session = Nothing
                            CloseUserForm()
                            Exit Sub
                        Case 2
                            bln = False
                            session = Nothing
                            ShowFingerprintValidation()
                    End Select
                    'End If
                Else
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "ReadUMIDCard(): " & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    lblMessage.Text = ""
                    If lblMessage.Text.ToUpper = "UNABLE TO AUTHENTICATE PIN..." Then
                        'SharedFunction.InvokeSystemMessage(Me, "UNABLE TO AUTHENTICATE PIN." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                        SharedFunction.InvokeSystemMessage(Me, "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day.".ToUpper, SystemMessage.MsgType.Warning)
                    Else
                        SharedFunction.InvokeSystemMessage(Me, "FAILED TO READ CARD." & vbNewLine & vbNewLine & "PLEASE REMOVE CARD AND TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                    End If

                    bln = False
                    session = Nothing
                    CloseUserForm()
                End If
            End If
        End If
    End Sub

    Private Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try
    End Function

    Private Function CheckIfCounterIsValid() As Boolean
        Return True

        'get counter
        Dim DAL_Mssql As New DAL_Mssql
        If DAL_Mssql.GetAttemptCounterByCRN(tempCRN.Replace("-", "")) Then
            If DAL_Mssql.ObjectResult Is Nothing Then
                bln = False
                session = Nothing
                Return True
            Else
                bln = False


                If CInt(DAL_Mssql.ObjectResult) >= 4 Then
                    Application.DoEvents()
                    SharedFunction.ShowErrorMessage("YOUR UMID CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.")
                    session = Nothing
                    Return False
                End If
            End If
        Else
            session = Nothing

            SharedFunction.InvokeSystemMessage(Me, "FAILED TO GET AUTHENTICATION COUNTER", SystemMessage.MsgType.ErrorMsg)

            Return False
        End If
    End Function

    'Private Sub GetUID()
    '    Do While True
    '        If AllcardUMID.IsCardPresent Then
    '            RichTextBox1.AppendText("Detected card " & Now.ToString("hh:mm:ss tt") & vbNewLine)
    '        Else
    '            RichTextBox1.AppendText("No card " & Now.ToString("hh:mm:ss tt") & vbNewLine)
    '        End If
    '        RichTextBox1.ScrollToCaret()
    '        Application.DoEvents()
    '        System.Threading.Thread.Sleep(1000)
    '    Loop

    'End Sub

    Private Function CheckRequirements() As Boolean
        Try
            'Return True

            If DeviceConnectivity.IsCardReaderPresent Then
                'Dim result As String = AllcardUMID.GetCRN(ErrorMessage)

                'Select Case result.ToUpper
                '    Case "Unable to detect card reader".ToUpper, "Unable to detect UMID card".ToUpper, "ERROR_CSN", "ERROR", ""
                '        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                '        'lblMessage.Text = "Place your UMID card on smart card reader..."

                '        lblMessage.Text = "No card detected"
                '        Return False
                '    Case Else
                '        Return True
                'End Select

                'added by edel Nov 23, 2020
                If Not AllcardUMID.IsCardPresent Then
                    'lblMessage.Text = "No card detected"
                    Return False
                Else
                    Return True
                End If
            Else
                'lblMessage.Text = "Unable to detect card reader"
                'SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Unable to detect card reader" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                'added by edel Nov 22, 2020 
                'Dim msg As String = "You cannot use this machine, card reader is either not connected or defective"*
                Dim msg As String = "You cannot use this machine, card reader is defective."
                lblMessage.Text = msg
                'SharedFunction.ShowWarningMessage(msg.ToUpper)
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
                bln = False
                session = Nothing
                CloseUserForm()
                'SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & msg.ToUpper & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                Return False
            End If
        Catch ex As Exception
            lblMessage.Text = "An error has occurred. Please contact Administrator. " & ex.Message
            Return False
        End Try
    End Function

    Private Function ActivateCard2_bak(ByVal tempCRN As String) As Boolean
        Try
            Dim result As Boolean = AllcardUMID.ActivateCard(ErrorMessage)

            If Not result Then
                SharedFunction.InvokeSystemMessage(Me, "CARD ACTIVATION FAILED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0}.{1}", tempCRN, ErrorMessage) & "|" & kioskIP & "|" & getbranchCoDE_1)
                Return False
            Else
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Activation success " & tempCRN & "|" & kioskIP & "|" & getbranchCoDE_1)

                Return True
            End If
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "ActivateCard2(): Error " & ex.Message & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
            SharedFunction.InvokeSystemMessage(Me, "CARD ACTIVATION FAILED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
            Return False
        Finally
            Cursor = Cursors.Hand
        End Try
    End Function

    Private Function ActivateCardv2(ByVal tempCRN As String, ByVal pin As String) As Boolean
        Try
            Dim result As Boolean = AllcardUMID.ActivateCardv2(ErrorMessage,, pin)

            If Not result Then
                SharedFunction.InvokeSystemMessage(Me, "CARD ACTIVATION FAILED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0}.{1}", tempCRN, ErrorMessage) & "|" & kioskIP & "|" & getbranchCoDE_1)
                Return False
            Else
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Activation success " & tempCRN & "|" & kioskIP & "|" & getbranchCoDE_1)
                Dim ip As New insertProcedure
                ip.insertCardActivate(SSStempFile, tempCRN)
                Return True
            End If
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "ActivateCard2(): Error " & ex.Message & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
            SharedFunction.InvokeSystemMessage(Me, "CARD ACTIVATION FAILED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
            Return False
        Finally
            Cursor = Cursors.Hand
        End Try
    End Function

    Private Sub pbCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCode.Click, Label1.Click
        Invoke(New Action(AddressOf CancelForm))
    End Sub

    Private Sub CancelForm()
        CloseUserForm()
    End Sub

#Region " PIN "

    Private _frmPIN As frmPIN
    Private PIN As String

    Private Sub ShowUserFormPIN()
        If Not IsCardBlocked() Then
            Me.bln = True
            _frmPIN = New frmPIN(False)
            AddHandler _frmPIN.cmdSubmit.Click, AddressOf CloseUserFormPIN
            _frmPIN.ShowDialog()
        Else
            bln = False
            session = Nothing
            CloseUserForm()
        End If
    End Sub

    Private Sub CloseFormPIN()
        Try
            If Not _frmPIN Is Nothing Then
                _frmPIN.Dispose()
                _frmPIN.Close()
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CloseFormAuth()
        Try
            If Not frmSelectAuthOption Is Nothing Then
                frmSelectAuthOption.Dispose()
                frmSelectAuthOption.Close()
            End If

        Catch ex As Exception
        End Try
    End Sub

    Private AuthenticationPIN As String
    Private AuthenticationATTMPTCNTR As Short

    Private Sub CloseUserFormPIN()
        Dim blnPIN As Boolean = _frmPIN.Success
        AuthenticationPIN = _frmPIN.TextBox1.Text
        AuthenticationATTMPTCNTR = _frmPIN.ATTMPTCNTR
        If blnPIN Then
            Invoke(New Action(AddressOf CloseFormPIN))
        End If
    End Sub

#End Region

    Private Sub ValidateCardInReader(ByRef blnResult As Boolean)
        Dim intValidateCardinReader As Short

        Try
            Dim _CRN2 As String = AllcardUMID.GetCRN(ErrorMessage)
            If _CRN2 <> "" Then
                If tempCRN.Replace("-", "") <> _CRN2 Then
                    Dim result As DialogResult
                    SharedFunction.InvokeSystemMessage3(Me, "SORRY YOUR UMID CARD CAN NOT BE AUTHENTICATED." & vbNewLine & vbNewLine & "PLEASE PLACE YOUR CARD ON THE SMART CARD READER AGAIN AND CLICK OK TO CONTINUE. OTHERWISE, CLICK CANCEL.", SystemMessage.MsgType.QuestionMsg, result)
                    Select Case result
                        Case DialogResult.OK, DialogResult.Yes
                            intValidateCardinReader = 1
                        Case Else
                            intValidateCardinReader = 2
                    End Select
                End If
            ElseIf _CRN2 = "" Then
                Dim result As DialogResult
                SharedFunction.InvokeSystemMessage3(Me, "YOUR UMID CARD IS NOT READABLE." & vbNewLine & vbNewLine & "PLEASE PLACE YOUR CARD ON THE SMART CARD READER AGAIN AND CLICK OK TO CONTINUE. OTHERWISE, CLICK CANCEL.", SystemMessage.MsgType.QuestionMsg, result)
                Select Case result
                    Case DialogResult.OK, DialogResult.Yes
                        intValidateCardinReader = 3
                    Case Else
                        intValidateCardinReader = 4
                End Select
            Else
                intValidateCardinReader = 0
            End If
        Catch ex As Exception
            Dim result As DialogResult
            SharedFunction.InvokeSystemMessage3(Me, "YOUR UMID CARD IS NOT READABLE." & vbNewLine & vbNewLine & "PLEASE PLACE YOUR CARD ON THE SMART CARD READER AGAIN AND CLICK OK TO CONTINUE. OTHERWISE, CLICK CANCEL.", SystemMessage.MsgType.QuestionMsg, result)
            Select Case result
                Case DialogResult.OK, DialogResult.Yes
                    intValidateCardinReader = 3
                Case Else
                    intValidateCardinReader = 4
            End Select
        End Try

        Select Case intValidateCardinReader
            Case 0
                blnResult = True
            Case 2, 4
                CloseFormPIN()

                bln = False
                session = Nothing
                CloseUserForm()

                blnResult = False
        End Select
    End Sub

    Private Sub OpenSSIT_Member()
        If SharedFunction.CHECK_MEMSTATUS(Me, tempCRN) <> "OK" Then
            bln = False
            session = Nothing
            CloseUserForm()
        Else
            SharedFunction.OpenSSIT_Member(tempCRN)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If TextBox1.Text = "" Then Return
        If TextBox2.Text = "" Then Return
        If TextBox3.Text = "" Then Return

        ReadActivateCard3()
    End Sub

    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try
            TextBox3.Text = TextBox1.Text.Substring(2)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        TextBox1.Clear()
        TextBox1.SelectAll()
        TextBox1.Focus()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        TextBox2.Clear()
        TextBox2.SelectAll()
        TextBox2.Focus()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        TextBox3.Clear()
        TextBox3.SelectAll()
        TextBox3.Focus()
    End Sub

    'Private arrCRNs() As String = {"9999-0000001-6", "9999-0000001-2", "9999-0000001-3", "0033-1689774-1", "0000-0000000-5", "0000-0000000-4", "0111-0793100-0", "0033-1689774-1"}
    'Private arrSSSs() As String = {"3309579239", "3415101630", "0421567656", "3354100695", "0373638659", "0389395685", "0226879523", "3354100695"}
    'Private arrCCDTs() As String = {"20160901", "20160901", "20160901", "20130730", "20140730", "20140730", "20130703", "20130730"}
    'Private arrPINs() As String = {"123456", "012345", "123456", "777777", "888888", "123456", "110890", "777777"}

    'Private arrCRNs() As String = {"9999-0000001-2", "0111-0793100-0", "0111-6156901-0", "0111-2054094-4", "0003-7540730-9"}
    'Private arrSSSs() As String = {"3415101630", "0226879523", "0935831810", "3429069030", "0375407309"}
    'Private arrCCDTs() As String = {"20200809", "20130730", "20160930", "20130711", "20160830"}
    'Private arrPINs() As String = {"123456", "012345", "123456", "777777", "888888"}

    'Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    Try
    '        'TextBox1.Text = arrCRNs(ComboBox1.SelectedIndex).Replace("-", "")
    '        'TextBox2.Text = arrCCDTs(ComboBox1.SelectedIndex)
    '        'TextBox3.Text = arrSSSs(ComboBox1.SelectedIndex)

    '        TextBox1.Text = td.cardData(ComboBox1.SelectedIndex).crn
    '        TextBox2.Text = td.cardData(ComboBox1.SelectedIndex).ccdt
    '        TextBox3.Text = td.cardData(ComboBox1.SelectedIndex).sss
    '    Catch ex As Exception
    '        TextBox1.Clear()
    '        TextBox2.Clear()
    '        TextBox3.Clear()
    '    End Try

    'End Sub

    'Class testData

    '    Public Sub New()
    '        cardDataValue = New List(Of td)
    '        Using sr As New IO.StreamReader("test_data.txt")
    '            Do While Not sr.EndOfStream
    '                Dim td As New td
    '                Dim line As String = sr.ReadLine
    '                If line.Trim <> "" Then
    '                    td.crn = line.Split("|")(0)
    '                    td.sss = line.Split("|")(1)
    '                    td.ccdt = line.Split("|")(2)
    '                    cardDataValue.Add(td)
    '                End If
    '            Loop
    '            sr.Dispose()
    '            sr.Close()
    '        End Using
    '    End Sub

    '    Private cardDataValue As List(Of td)
    '    Public Property cardData() As List(Of td)
    '        Get
    '            Return cardDataValue
    '        End Get
    '        Set(ByVal value As List(Of td))
    '            cardDataValue = value
    '        End Set
    '    End Property

    '    Class td

    '        Private crnValue As String
    '        Public Property crn() As String
    '            Get
    '                Return crnValue
    '            End Get
    '            Set(ByVal value As String)
    '                crnValue = value
    '            End Set
    '        End Property

    '        Private sssValue As String
    '        Public Property sss() As String
    '            Get
    '                Return sssValue
    '            End Get
    '            Set(ByVal value As String)
    '                sssValue = value
    '            End Set
    '        End Property

    '        Private ccdtValue As String
    '        Public Property ccdt() As String
    '            Get
    '                Return ccdtValue
    '            End Get
    '            Set(ByVal value As String)
    '                ccdtValue = value
    '            End Set
    '        End Property

    '    End Class

    'End Class

    Private Sub btnTestData_Click(sender As Object, e As EventArgs) Handles btnTestData.Click
        Dim td As New TestData2
        Dim f As New Form2()
        f.td = td
        f.ShowDialog()
        TextBox1.Text = f.crn
        TextBox2.Text = f.ccdt
        TextBox3.Text = f.sssNo

        If TextBox1.Text = "" Then Return
        If TextBox2.Text = "" Then Return
        If TextBox3.Text = "" Then Return

        ReadActivateCard3()
    End Sub
End Class
