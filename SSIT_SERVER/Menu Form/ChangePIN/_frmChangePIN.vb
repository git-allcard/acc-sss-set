
Public Class _frmChangePIN

    Private SS_Number As String = ""
    Private CRN As String = ""
    Private CCDT As String = ""
    Private oldPin As String = ""

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmChangePIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SS_Number = readSettings(xml_Filename, xml_path, "SS_Number")
            CRN = readSettings(xml_Filename, xml_path, "CRN")
            CCDT = readSettings(xml_Filename, xml_path, "CCDT")
            oldPin = readSettings(xml_Filename, xml_path, "CardPin")

            If CRN = "" Then CRN = HTMLDataExtractor.getCRN(_frmWebBrowser.WebBrowser1)

            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub


    Private Sub _frmChangePIN_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmMainMenu.btnInquiry_Click()
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        If txtPIN1.Text.Length <> RequiredDigits() Then
            SharedFunction.ShowWarningMessage(String.Format("ENTER {0} DIGIT PIN.", RequiredDigits))
        Else
            Dim resultDis As Integer = MessageBox.Show("PLACE UMID CARD ON CARD READER AND DO NOT REMOVE CARD WHILE PIN CHANGE PROCESS IS ONGOING." & vbNewLine & vbNewLine & "CLICK YES TO CONTINUE. ", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultDis = DialogResult.Yes Then
                If Not AllcardUMID.IsCardPresent Then
                    SharedFunction.ShowWarningMessage(String.Format("NO CARD DETECTED."))
                Else
                    lblWarning.Visible = True
                    Application.DoEvents()
                    System.Threading.Thread.Sleep(2000)

                    If UpdatePIN() Then
                        Dim changeDate As String = Now.ToLongDateString.ToUpper
                        Dim changeTime As String = Now.ToShortTimeString()

                        authentication = "SET002"
                        authenticationMsg = "YOU HAVE SUCCESSFULLY CHANGED YOUR UMID CARD PIN. " & vbNewLine & vbNewLine &
                                        "DATE: " & changeDate & vbNewLine &
                                        "TIME: " & changeTime & vbNewLine

                        If _frmMainMenu.IsAllowedToPrint() Then
                            PrintReceipt(changeDate, changeTime)
                            Dim xtd As New ExtractedDetails
                            _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                        End If

                        _frmMainMenu.RedirectToserAuthenticationForm("PIN CHANGE", "PIN CHANGE", "10040")
                    End If

                    lblWarning.Visible = False
                    Application.DoEvents()
                End If
            End If
        End If
    End Sub

    Private Sub PrintReceipt(ByVal changeDate As String, ByVal changeTime As String)
        If SharedFunction.ShowMessage("Do you want to print the receipt?") = DialogResult.No Then Return

        Dim class1 As New PrintHelper
        Dim receiptMsg As New System.Text.StringBuilder
        receiptMsg.Append("YOU HAVE SUCCESSFULLY CHANGED YOUR UMID" & vbNewLine)
        receiptMsg.Append("CARD PIN." & vbNewLine & vbNewLine)
        receiptMsg.Append("DATE : " & changeDate & vbNewLine)
        receiptMsg.Append("TIME : " & changeTime & vbNewLine)
        class1.prt(class1.prtPrintReceipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, receiptMsg.ToString, "PIN CHANGE", "Print"), _frmMainMenu.DefaultPrinterName)
        class1 = Nothing
    End Sub

    Private Sub btnNumber_Click(sender As Object, e As EventArgs) Handles btnOne.Click, btnTwo.Click, btnThree.Click, btnFour.Click, btnFive.Click, btnSix.Click, btnSeven.Click, btnEight.Click, btnNine.Click, btnZero.Click
        If txtPIN1.Text.Length <> 6 Then txtPIN1.Text = txtPIN1.Text.Trim & CType(sender, Button).Text
        If txtPIN1.Text.Length = 6 Then DisableControl(False)
    End Sub

    Private Function UpdatePIN() As Boolean
        'If UpdateSSSPin(txtPIN1.Text) Then
        '    If ChangeCardPin() Then
        '        Return True
        '    Else
        '        UpdateSSSPin(oldPin)
        '        Return False
        '    End If
        'Else
        '    Return False
        'End If

        'revised per sir fedie's advise
        If ChangeCardPin() Then
            If UpdateSSSPin(txtPIN1.Text) Then
                Return True
            Else
                Return False
            End If
        End If
    End Function

    Private Function UpdateSSSPin(ByVal pin As String) As Boolean
        Dim DAL_Oracle As New DAL_Oracle

        If DAL_Oracle.updatePIN(CRN.Replace("-", ""), CCDT, Now.ToString("MM-dd-yyyy"), pin) Then
            If DAL_Oracle.ObjectResult.ToString = "UPDATE_SUCCESS" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "CRN " & CRN & ", CCDT " & CCDT & " - Success updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1)
                Return True
            ElseIf DAL_Oracle.ObjectResult.ToString = "UPDATE_ERROR" Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(UPDATE_ERROR): CRN " & CRN & ", CCDT " & CCDT & " - Failed updating pin on server" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
                SharedFunction.ShowWarningMessage("PIN CHANGE FAILED.")
                Return False
            End If
        Else
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "UpdatePIN(): CRN " & CRN & ", CCDT " & CCDT & " - Failed updating pin on server. " & DAL_Oracle.ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
            SharedFunction.ShowWarningMessage("PIN CHANGE FAILED.")
            Return False
        End If
    End Function

    Private Function ChangeCardPin() As Boolean
        Dim errMsg As String = ""
        If Not AllcardUMID.ChangePINv2(oldPin, txtPIN1.Text, errMsg) Then
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CRN " & CRN & ", CCDT " & CCDT & " - Failed to update pin on card" & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
            SharedFunction.ShowWarningMessage("PIN CHANGE FAILED. PLEASE TRY AGAIN.")
            Return False
        Else
            _frmUserAuthentication.getTransacNum()
            Dim ip As New insertProcedure
            ip.insertPinChange(SS_Number, _frmUserAuthentication.lblTransactionNo.Text)

            'authentication = "SET002"
            'authenticationMsg = "YOU HAVE SUCCESSFULLY CHANGED YOUR UMID CARD PIN. " & vbNewLine & vbNewLine &
            '                            "DATE: " & Now.ToLongDateString.ToUpper & vbNewLine &
            '                            "TIME: " & Now.ToShortTimeString() & vbNewLine
            '_frmMainMenu.RedirectToserAuthenticationForm("PIN CHANGE", "PIN CHANGE", "10040")

            Return True
        End If
    End Function

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPIN1.GotFocus
        If CType(sender, TextBox).Name = "TextBox1" Then
            FocusedTxtbox = 1
        Else
            FocusedTxtbox = 2
        End If
    End Sub

    Private Sub LinkLabel1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        txtPIN1.Clear()
        txtPIN1.Focus()
        DisableControl(True)
    End Sub

    Private Sub DisableControl(ByVal bln As Boolean)
        btnOne.Enabled = bln
        btnTwo.Enabled = bln
        btnThree.Enabled = bln
        btnFour.Enabled = bln
        btnFive.Enabled = bln
        btnSix.Enabled = bln
        btnSeven.Enabled = bln
        btnEight.Enabled = bln
        btnNine.Enabled = bln
        btnZero.Enabled = bln
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPIN1.KeyPress
        If e.KeyChar = ChrW(Keys.Back) Then
        ElseIf Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        ElseIf CType(sender, TextBox).Text.Length = RequiredDigits() Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtPIN1.KeyUp
        ControlFocus()
    End Sub

    Private FocusedTxtbox As Short = 1

    Private Sub ControlFocus()
        If txtPIN1.Text.Length < RequiredDigits() Then
            txtPIN1.Focus()
            txtPIN1.Select(txtPIN1.Text.Length, 1)
        Else
            btnSubmit.Focus()
        End If

        If txtPIN1.Text.Length = RequiredDigits() Then
            txtPIN1.BackColor = Color.LightGreen
        Else
            txtPIN1.BackColor = Color.White
        End If
    End Sub

    Private Function RequiredDigits() As Short
        Return 6
    End Function

    Private Sub lblWarning_Click(sender As Object, e As EventArgs) Handles lblWarning.Click

    End Sub
End Class