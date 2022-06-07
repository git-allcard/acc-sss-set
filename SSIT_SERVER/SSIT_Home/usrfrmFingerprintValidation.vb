
Public Class usrfrmFingerprintValidation
    Dim CCDT As String
    Public Sub New(ByVal CRN As String, ByVal IsAssignPIN As Boolean, ByVal umid As umid)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _CRN = CRN.Replace("-", "")
        _IsAssignPIN = IsAssignPIN
        Me.umid = umid

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private _CRN As String
    Private _IsAssignPIN As Boolean
    Private cardBlock As CardBlock
    Private umid As umid

    Private Sub pbCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SharedFunction.ShowMainDefaultUserForm()
    End Sub

    Private session As System.Threading.Thread
    Private threadMatch As System.Threading.Thread

    Private cardTemplates As New List(Of Byte())

    Private fingers() As String = {"right index", "right thumb", "left index", "left thumb"}
    Private fingerSelected As Short

    Private Sub usrfrmFingerprintValidation_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False

        StartProcess()
        'MatchingSuccess(0)
    End Sub

    Public Sub StartProcess()
        isShowFutronic = True
        'session = New System.Threading.Thread(AddressOf Counter)
        'session.IsBackground = True
        'session.Start()
        Dim SS_Number As String = readSettings(xml_Filename, xml_path, "SS_Number")
        cardBlock = New CardBlock(SS_Number)

        For Each file As String In IO.Directory.GetFiles(Application.StartupPath & "\Temp")
            If IO.Path.GetExtension(file).ToUpper = ".ansi-fmr".ToUpper Then
                Dim cardTemplate As Byte() = IO.File.ReadAllBytes(file)
                cardTemplates.Add(cardTemplate)
            End If
        Next

        ATTMPTCNTR = cardBlock.attempCntr

        If cardTemplates.Count > 0 Then _frmFutronic.cardTemplates = cardTemplates

        'Invoke(New Action(AddressOf ShowUserFormPIN))
        'Return

        finger_index = 2
        StartMatching()
    End Sub

    Private finger_index As Short = 0
    Private ATTMPTCNTR As Integer = 0
    Private MATCHINGCNTR As Integer = 0

    Private Sub BindFingerScanningImage()
        Dim strFileImage As String

        Select Case finger_index 'cboFingerPosition.SelectedIndex
            Case 0
                strFileImage = "Images\RI.jpg"
            Case 1
                strFileImage = "Images\RT.jpg"
            Case 2
                strFileImage = "Images\LI.jpg"
            Case 3
                strFileImage = "Images\LT.jpg"
        End Select

        Dim ms As New IO.MemoryStream(IO.File.ReadAllBytes(strFileImage))
        Me.BackgroundImage = Image.FromStream(ms)
        Me.BackgroundImageLayout = ImageLayout.Stretch

        'MessageBox.Show(String.Format("Me Width {0} Height {1}, BackgroundImage Width {2} Height {3}", Me.Width, Me.Height, Me.BackgroundImage.Width, Me.BackgroundImage.Height))

        Do While Me.BackgroundImage Is Nothing
            Dim ms2 As New IO.MemoryStream(IO.File.ReadAllBytes(strFileImage))
            Me.BackgroundImage = Image.FromStream(ms2)
            Me.BackgroundImageLayout = ImageLayout.Stretch
        Loop


    End Sub

    Private Sub StartMatching()
        Invoke(New Action(AddressOf Matchv2))
    End Sub

    Private Sub FingerScan(ByRef finger_index As Short, ByRef ATTMPTCNTR As Integer, ByRef MATCHINGCNTR As Integer)
        'Dim deviceType As String = IO.File.ReadAllText(SharedFunction.FingerScannerDevice).ToUpper.Trim
        Dim bln As Boolean = False
        Dim code As String = "00"

        Try
            '_frmFutronic.Top = (Me.Height / 2)
            '_frmFutronic.Left = Me.Width / 2
            'If _frmFutronic Is Nothing Then _frmFutronic = New _frmFutronic
            _frmFutronic.Top = 100
            _frmFutronic.Left = 275
            _frmFutronic.fp = finger_index
            _frmFutronic.qualityThreshold = 90
            _frmFutronic.StartStopScan()
            _frmFutronic.ShowDialog()
            If _frmFutronic.formResponse = 0 Then
                If cardBlock Is Nothing Then cardBlock = New CardBlock(readSettings(xml_Filename, xml_path, "SS_Number"))
                cardBlock.attempCntr = 0
                cardBlock.SaveCardBlockBySSNUM()
                bln = True
            ElseIf _frmFutronic.formResponse = 2 Then
                code = "01"
                _frmFutronic.m_bCancelOperation = True
            Else
                _frmFutronic.m_bCancelOperation = True
            End If

        Catch ex As Exception
            MessageBox.Show("Futronic error " & ex.Message)
        End Try

        If bln Then
            MatchingSuccess(finger_index)
        Else
            MatchingFailed(code, finger_index, ATTMPTCNTR, MATCHINGCNTR)
        End If
    End Sub

    'added by edel Nov 19, 2020
    Private Sub MatchingSuccess(ByVal _finger_index As Integer)
        finger_index = _finger_index
        If Not _IsAssignPIN Then
            If umid Is Nothing Then
                Invoke(New Action(AddressOf OpenSSIT_Member))
            Else
                If umid.sssSector36 = "NO_DATA" Then
                    If Not SharedFunction.WriteSSSNumber_InUMIDCardSector(_CRN.Replace("-", ""), umid.sssNumber) Then
                        If Not SharedFunction.WriteSSSNumber_InUMIDCardSector(_CRN.Replace("-", ""), umid.sssNumber) Then
                            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CRN " & _CRN & ", CCDT " & CCDT & " - Failed to write SS on card sector" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                            SharedFunction.ShowWarningMessage("SYSTEM FAILED TO WRITE SSS NUMBER ON CARD SECTOR. PLEASE TRY AGAIN.")

                            threadMatch = Nothing
                            session = Nothing
                            CloseUserForm()
                        End If
                    End If

                    Dim umidService As New SSUmidService
                    If umidService.isCsnExist(umid.csn) Then
                        Select Case umidService.webserviceResponse.processFlag
                            Case "0"
                                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CRN " & _CRN & ", CCDT " & CCDT & " - Failed to verify gsis csn" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                                SharedFunction.ShowWarningMessage("SYSTEM FAILED TO VERIFY GSIS CSN. PLEASE TRY AGAIN.")
                                threadMatch = Nothing
                                session = Nothing
                                CloseUserForm()
                            Case "1"
                                If umidService.insertGSISUmid(umid) Then
                                    Dim ip As New insertProcedure
                                    Dim dob As String = String.Format("{0}/{1}/{2}", umid.dateOfBirth.Substring(4, 2), umid.dateOfBirth.Substring(6, 2), umid.dateOfBirth.Substring(0, 4))
                                    ip.insertGSISLINK(umidService.webserviceResponse.ssnumber, umid.csn, umid.crn, umid.firstName, umid.middleName, umid.lastName, umid.suffix, umid.gender, dob, umid.ccdt, HTMLDataExtractor.MemberFullName)

                                    SharedFunction.ShowInfoMessage("The UMID card data is now built in the SSS database. You may now use your UMID card in availing of available transactions in the SET.".ToUpper)

                                    Invoke(New Action(AddressOf OpenSSIT_Member))
                                Else
                                    'api insertGSISUmid failed to in

                                    SharedFunction.ShowWarningMessage("SYSTEM FAILED TO INSERT RECORD IN SSS-GSIS TABLE. PLEASE TRY AGAIN.")
                                    threadMatch = Nothing
                                    session = Nothing

                                    CloseUserForm()
                                End If
                        End Select
                    Else
                        'api insertGSISUmid failed to in
                    End If
                Else
                    'if sector 36 already have sss#
                    Invoke(New Action(AddressOf OpenSSIT_Member))
                End If
            End If
        Else
            lblMessage.Text = ""
            Application.DoEvents()
            Invoke(New Action(AddressOf ShowUserFormPIN))
        End If
    End Sub

    'added by edel Nov 19, 2020
    Private Sub MatchingFailed(ByVal code As String, ByRef _finger_index As Integer, ByRef ATTMPTCNTR As Integer, ByRef MATCHINGCNTR As Integer)
        Dim nextFingerIndex As Integer = IIf(_finger_index = 3, 0, _finger_index + 1)

        If code = "00" Then
            ATTMPTCNTR += 1
            If cardBlock Is Nothing Then cardBlock = New CardBlock(readSettings(xml_Filename, xml_path, "SS_Number"))
            cardBlock.attempCntr = ATTMPTCNTR
            cardBlock.SaveCardBlockBySSNUM()
        End If
        MATCHINGCNTR += 1

        If MATCHINGCNTR <> SharedFunction.FAILED_MATCHING_LIMIT Then
            lblMessage.Text = String.Format("Failed to authenticate {0} finger.", fingers(_finger_index))
            lblMessage.Text += vbNewLine & String.Format("Place your {0} on the fingerprint scanner for validation...", fingers(nextFingerIndex))
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Failed to authenticate " & fingers(_finger_index) & " finger." & "|" & kioskIP & "|" & getbranchCoDE_1)
            Application.DoEvents()
            System.Threading.Thread.Sleep(2000)

            threadMatch = Nothing
            finger_index = nextFingerIndex
            Invoke(New Action(AddressOf Matchv2))
        Else
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Failed to authenticate " & fingers(_finger_index) & " finger." & "|" & kioskIP & "|" & getbranchCoDE_1)
            Application.DoEvents()
            System.Threading.Thread.Sleep(2000)

            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Authentication failed. Fingerprints did not match for CRN " & _CRN & "|" & kioskIP & "|" & getbranchCoDE_1)

            If ATTMPTCNTR >= SharedFunction.FAILED_MATCHING_LIMIT Then
                Dim DAL As New DAL_Mssql
                If Not DAL.InsertAUTHFAILED(_CRN, ATTMPTCNTR, False, "UMIDCARD") Then
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Match(): Failed to insert CRN " & _CRN & ". Error: " & DAL.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                End If
                DAL.Dispose()

                Try
                    Dim strName As String = readSettings(xml_Filename, xml_path, "CARD_DATA")
                    Dim name() As String = strName.Split("|")

                    Dim p As New PrintHelper2
                    p.prt(p.prtAuthFailedPrint(name(4).Trim & ", " & name(2).Trim & " " & name(3).Trim, _CRN), DefaultPrinterName)
                Catch ex As Exception
                End Try

                'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                Dim msg As String = "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day."
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)

            Else
                Dim msg As String = "The system has failed to authenticate your card. You cannot proceed. "
                SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.Warning)
            End If

            threadMatch = Nothing
            session = Nothing

            CloseUserForm()
            Exit Sub
        End If
    End Sub

    'added by edel Nov 20, 2020
    Private Sub FailedActivation()
        SharedFunction.InvokeSystemMessage(Me, "UMID CARD AUTHENTICATION FAILED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.SystemMsg)

        threadMatch = Nothing
        session = Nothing

        If _IsAssignPIN Then SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD WAS NOT SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

        CloseUserForm()
    End Sub

    'added by edel Nov 19, 2020
    Private Sub Matchv2()
        If Not isShowFutronic Then
            threadMatch = Nothing
            session = Nothing
            Exit Sub
        End If

        Try
            'MessageBox.Show(Main.Controls(0).Name)
            If Main.Controls(0).Name <> "usrfrmFingerprintValidation" Then
                threadMatch = Nothing
                session = Nothing
                Exit Sub
            End If

            Me.bln = False

            Invoke(New Action(AddressOf BindFingerScanningImage))


            'If ATTMPTCNTR = (SharedFunction.FAILED_MATCHING_LIMIT - 1) Then
            '    lblMessage.Text = ""
            '    Application.DoEvents()

            '    'If SharedFunction.ShowInfoMessage("WARNING!" & vbNewLine & vbNewLine & "THIS WILL BE YOUR 5TH ATTEMPT TO AUTHENTICATE. YOUR UMID CARD WILL BE BLOCKED AND INVALIDATED AFTER THE 5TH ATTEMPT." & vbNewLine & vbNewLine & "DO YOU WANT TO CONTINUE?", MessageBoxButtons.YesNo) = DialogResult.No Then
            '    If SharedFunction.ShowInfoMessage("WARNING!" & vbNewLine & vbNewLine & "THIS WILL BE YOUR FINAL ATTEMPT TO AUTHENTICATE. " & vbNewLine & vbNewLine & "DO YOU WANT TO CONTINUE?", MessageBoxButtons.YesNo) = DialogResult.No Then
            '        FailedActivation()
            '        Exit Sub
            '    End If
            If ATTMPTCNTR = SharedFunction.FAILED_MATCHING_LIMIT Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Authentication failed. Fingerprints did not match for CRN " & _CRN & "|" & kioskIP & "|" & getbranchCoDE_1)

                Dim DAL As New DAL_Mssql
                If Not DAL.ChangeSSINFOTERMAUTHFAILEDStatus(_CRN, False) Then
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "Match(): Failed to ChangeCRNStatus of CRN " & _CRN & ". Error: " & DAL.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                End If
                DAL.Dispose()

                threadMatch = Nothing
                session = Nothing

                CloseUserForm()
                Exit Sub
            End If

            lblMessage.Text = String.Format("Place your {0} on the fingerprint scanner for validation...", fingers(finger_index).ToUpper)
            Application.DoEvents()

            If Main.Controls(0).Name <> "usrfrmFingerprintValidation" Then
                threadMatch = Nothing
                session = Nothing
                Exit Sub
            End If

            FingerScan(finger_index, ATTMPTCNTR, MATCHINGCNTR)
        Catch ex As Exception
        Finally
            threadMatch = Nothing
        End Try
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

    'Private Sub ShowFinalMessage()
    'SharedFunction.ShowErrorMessage("Authentication failed. Your fingerprints did not match." & vbNewLine & vbNewLine & "Please seek assistance from the Member Service Representative (MSR) at our frontline service counter immediately." & vbNewLine & vbNewLine & "Thank you very much.")
    'End Sub

    Private Sub ShowPrinter()
        SharedFunction.ShowErrorMessage(DefaultPrinterName)
    End Sub

    Private bln As Boolean = True
    Private intCntr As Short = 30

    Private Sub Counter()
        While Me.bln
            If intCntr > 0 Then
                intCntr -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                Me.bln = False
                session = Nothing

                CloseUserForm()
            End If
        End While
    End Sub

    Private Delegate Sub Action()

    Private Sub CloseUserForm()
        Invoke(New Action(AddressOf CloseFormPIN))
        isShowFutronic = False
        Me.bln = False
        session = Nothing
        SharedFunction.HouseKeeping()
        ' SharedFunction.SaveActivityToDbase("UMIDCARD", "")
        Invoke(New Action(AddressOf SharedFunction.ShowMainDefaultUserForm))
    End Sub

    Private Sub pbRetry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        threadMatch = New System.Threading.Thread(AddressOf Matchv2)
        threadMatch.IsBackground = True
        threadMatch.Start()
    End Sub

    Private Sub pbCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCode.Click, Label1.Click
        Invoke(New Action(AddressOf CancelForm))
    End Sub

    Private Sub CancelForm()
        'ReleaseSagem()
        CloseUserForm()
    End Sub

    Private _frmPIN As frmPIN

    Private Sub ShowUserFormPIN()
        Me.bln = True
        'Invoke(New Action(AddressOf CloseFormPIN))
        _frmPIN = New frmPIN(True)
        AddHandler _frmPIN.cmdSubmit.Click, AddressOf CloseUserFormPIN
        AddHandler _frmPIN.cmdClosePanel.Click, AddressOf CloseUserFormPIN
        _frmPIN.ShowDialog()
        'frmPIN.TextBox1.Focus()
    End Sub

    Private Sub CloseUserFormPIN()
        Dim blnPIN As Boolean = _frmPIN.Success
        Dim PIN As String = _frmPIN.TextBox1.Text
        If blnPIN Then
            If _frmPIN.IsAssignPIN Then
                If _frmPIN.ATTMPTCNTR = 0 Then
                    Invoke(New Action(AddressOf CloseFormPIN))

                    threadMatch = Nothing
                    session = Nothing
                    CloseUserForm()
                Else
                    Invoke(New Action(AddressOf CloseFormPIN))

                    lblMessage.Text = "WARNING! PLEASE DO NOT REMOVE YOUR CARD"
                    Application.DoEvents()

                    Dim blnResult As Boolean
                    'ValidateCardInReader(blnResult, 2)
                    'If Not blnResult Then Exit Sub
                    blnResult = True

                    If UpdatePIN(PIN) Then
                        'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "CLICK OK TO CONTINUE.", SystemMessage.MsgType.SystemMsg)
                        SharedFunction.InvokeSystemMessage(Me, "The UMID card data is now built in the SSS database. You may now use your UMID card in availing of available transactions in the SET.".ToUpper, SystemMessage.MsgType.SystemMsg)
                        Invoke(New Action(AddressOf OpenSSIT_Member))
                    Else
                        threadMatch = Nothing
                        session = Nothing
                        CloseUserForm()
                    End If
                End If
            End If

        End If
    End Sub

    Private Function UpdatePIN(ByVal PIN As String, Optional ByVal intAttemptCntr As Short = 1) As Boolean
        Dim DAL_Oracle As New DAL_Oracle
        Dim SS_Number As String = readSettings(xml_Filename, xml_path, "SS_Number")
        CCDT = readSettings(xml_Filename, xml_path, "CCDT")

        'SharedFunction.ShowMessage(String.Format("UpdatePIN(): {0},{1},{2}", SS_Number, CCDT, PIN))

        If DAL_Oracle.updatePIN(_CRN.Replace("-", ""), CCDT, Now.ToString("MM-dd-yyyy"), PIN) Then
            If DAL_Oracle.ObjectResult.ToString = "UPDATE_SUCCESS" Then
                'SharedFunction.ShowMessage("UPDATE_SUCCESS")
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "CRN " & _CRN & ", CCDT " & CCDT & " - Success updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1)

                Dim errMsg As String = ""

                If Not AllcardUMID.ChangePINv2("123456", PIN, errMsg) Then
                    If Not AllcardUMID.ChangePINv2("12345678", PIN, errMsg) Then
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CRN " & _CRN & ", CCDT " & CCDT & " - Failed to update pin on card sector" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                        SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD WAS NOT SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                        Return False
                    End If
                End If

                If Not SharedFunction.WriteSSSNumber_InUMIDCardSector(_CRN.Replace("-", ""), SS_Number) Then
                    'SharedFunction.ShowMessage("FAILED TO WRITE TO SECTOR 1")
                    If Not SharedFunction.WriteSSSNumber_InUMIDCardSector(_CRN.Replace("-", ""), SS_Number) Then
                        If Not SharedFunction.WriteSSSNumber_InUMIDCardSector(_CRN.Replace("-", ""), SS_Number) Then
                            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CRN " & _CRN & ", CCDT " & CCDT & " - Failed to write SS on card sector" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                            SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD WAS NOT SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                            Return False
                        End If
                    End If
                End If

                Return True
            ElseIf DAL_Oracle.ObjectResult.ToString = "UPDATE_ERROR" Then
                If intAttemptCntr <= 3 Then
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(" & intAttemptCntr.ToString & "): CRN " & _CRN & ", CCDT " & CCDT & " - Failed updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    UpdatePIN(PIN, intAttemptCntr + 1)
                Else
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(" & intAttemptCntr.ToString & "): CRN " & _CRN & ", CCDT " & CCDT & " - Failed updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD WAS NOT SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                    Return False
                End If
            End If
        Else
            If intAttemptCntr <= 3 Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(" & intAttemptCntr.ToString & "): CRN " & _CRN & ", CCDT " & CCDT & " - Failed updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                UpdatePIN(PIN, intAttemptCntr + 1)
            Else
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(" & intAttemptCntr.ToString & "): CRN " & _CRN & ", CCDT " & CCDT & " - Failed updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD WAS NOT SUCCESSFULLY ACTIVATED." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)

                Return False
            End If
        End If
    End Function

    Private Sub OpenSSIT_Member()
        If isGSISCard Then
            SharedFunction.OpenSSIT_Member(_CRN)
        Else
            If SharedFunction.CHECK_MEMSTATUS(Me, _CRN) <> "OK" Then
                threadMatch = Nothing
                session = Nothing

                CloseUserForm()
            Else
                SharedFunction.OpenSSIT_Member(_CRN)
            End If
        End If
    End Sub

    Private Sub CloseFormPIN()
        _frmMainMenu.DisposeForm(_frmPIN)
    End Sub

    Private Sub usrfrmFingerprintValidation_Disposed(sender As Object, e As EventArgs) Handles Me.Disposed
        _frmMainMenu.DisposeForm(_frmFutronic)
    End Sub


End Class
