Public Class _frmFeedbackKiosk
    Dim at As New auditTrail
    Dim db As New ConnectionString
    Dim fullnameFromCard As String
    Dim ssnumberFromCard As String
    Dim printZero As Integer = 0
    Dim xtd As New ExtractedDetails
    Public num1, num2, num3, num4, num5, num6 As String
    Public comments As String
    Public helpKiosk As String  ' 2 - NO, YES-1
    Public ADDress As String  ' 1 - HOME ,  2 BUSINESS ADDRESS
    Public ac, acemail As Integer
    Dim isValid As Boolean
    ReadOnly ValidChars As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.- "
    ReadOnly ValidCharsCity As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,- "
    ReadOnly ValidCharsNum As String = _
"0123456789 "


    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        printTag = printZero
        webPageTag = "Feedback Kiosk"

        txtName.Focus()

        tagPage = "16.1.1"

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            Panel1.Left = 30
            For Each ctrl As Control In Panel1.Controls
                If ctrl.Name = "Panel4" Then
                    For Each ctrl3 As Control In Panel4.Controls
                        ctrl3.Font = New Font(ctrl3.Font.Name, ctrl3.Font.Size - 3, ctrl3.Font.Style)

                        Select Case ctrl3.Name
                            Case "Panel24"
                                ctrl3.Top = ctrl3.Top - 20
                                If ctrl3.Name = "Panel24" Then
                                    For Each ctrl2 As Control In Panel24.Controls
                                        ctrl2.Font = New Font(ctrl2.Font.Name, ctrl2.Font.Size - 3, ctrl2.Font.Style)
                                    Next
                                End If
                            Case Else
                                ctrl3.Top = ctrl3.Top - 30
                        End Select
                    Next
                End If

                Select Case ctrl.Name
                    Case "Label4", "Label5"
                    Case "Panel4"
                        ctrl.Top = ctrl.Top - 40
                    Case "Panel24"
                        ctrl.Top = ctrl.Top - 20
                        If ctrl.Name = "Panel24" Then
                            For Each ctrl2 As Control In Panel24.Controls
                                ctrl2.Font = New Font(ctrl2.Font.Name, ctrl2.Font.Size - 3, ctrl2.Font.Style)
                            Next
                        End If
                    Case Else
                        ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)
                        ctrl.Top = ctrl.Top - 20
                End Select
            Next

            For Each ctrl2 As Control In Panel1.Controls
                ctrl2.Font = New Font(ctrl2.Font.Name, ctrl2.Font.Size - 3, ctrl2.Font.Style)
            Next

            Panel2.Height = Panel2.Height - 20
            Panel16.Height = Panel16.Height - 20

            LabelX2.Font = New Font(LabelX2.Font.Name, LabelX2.Font.Size - 3, LabelX2.Font.Style)
            LabelX2.Width = 650 'LabelX2.Width - 50
        End If
    End Sub

    Public Sub clearAll()
        txtEmail.Text = ""
        txtAddress2.Text = ""
        txtAddress1.Text = ""
        txtName.Text = ""
        txtZipCode.Text = ""
        ADDress = Nothing
        rbtHomeAddress.CheckState = CheckState.Unchecked
        rbtBusinesAdd.CheckState = CheckState.Unchecked
        num1 = 0
        num2 = 0
        num3 = 0
        num4 = 0
        num5 = 0
        num6 = 0
        helpKiosk = 0
        comments = ""
        _frmFeedbackKiosk1.cba1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbe1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbe2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbe3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbe4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbe5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cba2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cba3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cba4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cba5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbb1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbb2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbb3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbb4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbb5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbc1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbc2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbc3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbc4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbc5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbd1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbd2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbd3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbd4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk1.cbd5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.cbf1.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.cbf2.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.cbf3.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.cbf4.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.cbf5.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.rtbComments.Text = ""
        _frmFeedbackKiosk2.chkHelpNo.CheckState = CheckState.Unchecked
        _frmFeedbackKiosk2.chkHelpYes.CheckState = CheckState.Unchecked
    End Sub

    Private Sub validateEmail(ByVal mail As String)
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If email.IsMatch(mail) Then
            ac = 0
            'lblError2.Visible = False
        Else
            ac = 1
            'lblError2.Visible = True
        End If
    End Sub


    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If txtName.Text.Trim = "" Then
            MsgBox("COMPLETE NAME IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtName.Focus()
        ElseIf txtEmail.Text.Trim = "" Then
            MsgBox("EMAIL ADDRESS IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtEmail.Focus()
        ElseIf rbtHomeAddress.Checked = False And rbtBusinesAdd.Checked = False Then
            MsgBox("PLEASE SELECT AN ADDRESS TYPE.", MsgBoxStyle.Information, "Information")
            rbtHomeAddress.Focus()
        ElseIf txtAddress1.Text.Trim = "" Then
            MsgBox("ADDRESS 1 IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtAddress1.Focus()
        ElseIf txtAddress2.Text.Trim = "" Then
            MsgBox("ADDRESS 2 IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtAddress2.Focus()
        ElseIf txtCP.Text = "" Then
            MsgBox("CITY/PROVINCE IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtCP.Focus()
        ElseIf txtZipCode.Text.Trim = "" Then
            MsgBox("POSTAL CODE IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtZipCode.Focus()
        ElseIf Not email.IsMatch(txtEmail.Text.Trim) Then
            'lblError2.Visible = True
            MsgBox("EMAIL ADDRESS IS INVALID.", vbInformation, "Information")
            txtEmail.Focus()
        Else
            _frmFeedbackKioskMain.Panel4.Controls.Clear()
            _frmFeedbackKiosk1.TopLevel = False
            _frmFeedbackKiosk1.Parent = _frmFeedbackKioskMain.Panel4
            _frmFeedbackKiosk1.Dock = DockStyle.Fill
            _frmFeedbackKiosk1.Show()

            tagPage = "16.1.2"
        End If

    End Sub

    Private Sub LabelX1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter, txtEmail.Enter, txtAddress1.Enter, txtAddress2.Enter, txtCP.Enter, txtZipCode.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

    Private Sub txtName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If email.IsMatch(txtEmail.Text.Trim) Then
            'lblError2.Visible = False
        End If
    End Sub

    Private Sub txtZipCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZipCode.KeyPress
        e.Handled = Not (ValidCharsNum.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtAddress1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress1.KeyPress
         e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtAddress2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress2.KeyPress
         e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtCP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCP.KeyPress
        e.Handled = Not (ValidCharsCity.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

End Class