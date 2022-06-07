
Public Class usrfrmFingerprintValidation2

    Public Sub New(ByVal CRN As String)

        ' This call is required by the Windows Form Designer.
        InitializeComponent()
        _CRN = CRN.Replace("-", "")

        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private _CRN As String

    'Public AuthenticationResult As Short = 3

    Private session As System.Threading.Thread
    Private threadMatch As System.Threading.Thread

    Private cardTemplates As New List(Of Byte())

    Private fingers() As String = {"right index", "right thumb", "left index", "left thumb"}
    Private fingerSelected As Short

    Private cardBlock As CardBlock

    Private Sub usrfrmFingerprintValidation2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Process()
    End Sub

    Public Sub Process()
        CheckForIllegalCrossThreadCalls = False
        Me.Dock = DockStyle.Fill

        Me.Width = _frm2.Width
        Me.Height = _frm2.Height


        ''temp 02/04/2021
        'MatchingSuccess(0)
        'Return

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

        Do While Me.BackgroundImage Is Nothing
            Dim ms2 As New IO.MemoryStream(IO.File.ReadAllBytes(strFileImage))
            Me.BackgroundImage = Image.FromStream(ms2)
        Loop
    End Sub

    Private Sub StartMatching()
        isShowFutronic = True
        Invoke(New Action(AddressOf Matchv2))
    End Sub



    Public Sub Matchv2()
        Try
            If Not isShowFutronic Then
                threadMatch = Nothing
                session = Nothing
                CloseUserForm(False)
            End If

            Me.bln = False

            Invoke(New Action(AddressOf BindFingerScanningImage))

            If ATTMPTCNTR = SharedFunction.FAILED_MATCHING_LIMIT Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Authentication failed. Fingerprints did not match for CRN " & _CRN & "|" & kioskIP & "|" & getbranchCoDE_1)

                TxnAuthenticationResult = False
                CloseUserForm(True)
                Exit Sub
            End If

            lblMessage.Text = String.Format("Place your {0} on the fingerprint scanner for validation...", fingers(finger_index).ToUpper)
            Application.DoEvents()

            FingerScan(finger_index, ATTMPTCNTR, MATCHINGCNTR)
        Catch ex As Exception
        Finally
            threadMatch = Nothing
        End Try
    End Sub

    Private Sub FingerScan(ByRef finger_index As Short, ByRef ATTMPTCNTR As Integer, ByRef MATCHINGCNTR As Integer)
        'Dim deviceType As String = IO.File.ReadAllText(SharedFunction.FingerScannerDevice).ToUpper.Trim
        Dim bln As Boolean = False
        Dim code As String = "00"

        Try
            If Not isShowFutronic Then
                threadMatch = Nothing
                session = Nothing
                CloseUserForm(False)
            Else
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
        TxnAuthenticationResult = True
        CloseUserForm(True)
    End Sub

    'added by edel Nov 19, 2020
    Private Sub MatchingFailed(ByVal code As String, ByRef _finger_index As Integer, ByRef ATTMPTCNTR As Integer, ByRef MATCHINGCNTR As Integer)
        Dim nextFingerIndex As Integer = IIf(_finger_index = 3, 0, _finger_index + 1)

        If code = "00" Then
            ATTMPTCNTR += 1
            If Not isGSISCard Then
                If cardBlock Is Nothing Then cardBlock = New CardBlock(readSettings(xml_Filename, xml_path, "SS_Number"))
                cardBlock.attempCntr = ATTMPTCNTR
                cardBlock.SaveCardBlockBySSNUM()
            End If
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
            'While isShowFutronic
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Failed to authenticate " & fingers(_finger_index) & " finger." & "|" & kioskIP & "|" & getbranchCoDE_1)
            Application.DoEvents()
            System.Threading.Thread.Sleep(2000)

            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Authentication failed. Fingerprints did not match for CRN " & _CRN & "|" & kioskIP & "|" & getbranchCoDE_1)

            If ATTMPTCNTR >= SharedFunction.FAILED_MATCHING_LIMIT Then
                TxnAuthenticationResult = False

                If Not isGSISCard Then
                    If cardBlock Is Nothing Then cardBlock = New CardBlock(readSettings(xml_Filename, xml_path, "SS_Number"))
                    cardBlock.attempCntr = ATTMPTCNTR
                    cardBlock.SaveCardBlockBySSNUM()
                End If

                'SharedFunction.InvokeSystemMessage(Me, "YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                'Dim msg As String = "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day."
                'SharedFunction.InvokeSystemMessage(Me, msg.ToUpper, SystemMessage.MsgType.ErrorMsg)

                'threadMatch = Nothing
                'session = Nothing

                CloseUserForm(True)

                'authentication = "SET002"
                'authenticationMsg = "THE SYSTEM HAS FAILED TO AUTHENTICATE YOUR CARD. YOU CANNOT PROCEED. " & vbNewLine & vbNewLine &
                '                    "YOU MAY ACCESS YOUR ACCOUNT ON THE FOLLOWING DAY. "

                '_frmMainMenu.RedirectToserAuthenticationForm("PIN CHANGE", "PIN CHANGE", "10040")
            Else
                Dim msg As String = "The system has failed to authenticate your card. You cannot proceed. "
                SharedFunction.ShowWarningMessage(msg.ToUpper)

                TxnAuthenticationResult = False
                CloseUserForm(False)
                _frmMainMenu.btnInquiry_Click()
            End If
            'End While




            'threadMatch = Nothing
            'session = Nothing
            'CloseUserForm(True)
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
                TxnAuthenticationResult = False
                CloseUserForm(True)
            End If
        End While
    End Sub

    Private Delegate Sub Action()

    Private Sub CloseUserForm(ByVal backToPreviousTxn As Boolean)
        'ReleaseSagem()
        isShowFutronic = False
        Me.bln = False
        threadMatch = Nothing
        session = Nothing

        If backToPreviousTxn Then SharedFunction.BackToPreviousTxnv2()
    End Sub

    Private Sub pbCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCode.Click, Label1.Click
        TxnAuthenticationResult = False
        CloseUserForm(False)
        _frmMainMenu.btnInquiry_Click()
        'CloseUserForm(True)
        'Invoke(New Action(AddressOf CloseUserForm))
    End Sub

End Class
