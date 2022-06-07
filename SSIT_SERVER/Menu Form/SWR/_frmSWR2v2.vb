
Public Class _frmSWR2v2

    Public Shared isMemberRegistered As Boolean = True
    Dim xtd As New ExtractedDetails

    Private Sub _frmSWR2v2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
            _frmMainMenu.DisposeForm(_frmLoading)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmSWR2v2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Redirect(ByVal frm As Form)
        Try
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            frm.TopLevel = False
            frm.Parent = _frmMainMenu.splitContainerControl.Panel2
            frm.Dock = DockStyle.Fill
            frm.Show()
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        _frmSWR1.TopLevel = False
        _frmSWR1.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmSWR1.Dock = DockStyle.Fill
        _frmSWR1.Show()
        Me.Hide()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If txtUserId.Text = "" Then
            SharedFunction.ShowWarningMessage("USER ID IS REQUIRED.")
        ElseIf txtUserId.Text.Length < 8 Then
            SharedFunction.ShowWarningMessage("USER ID LENGTH SHOULD BE 8-20 CHARACTERS.")
        ElseIf txtUserId.Text.Length > 20 Then
            SharedFunction.ShowWarningMessage("USER ID LENGTH SHOULD BE 8-20 CHARACTERS.")
        ElseIf Not Char.IsLetter(txtUserId.Text.Substring(0, 1)) Then
            SharedFunction.ShowWarningMessage("FIRST CHARACTER MUST BE ALPHABETIC.")
        Else
            Dim swr As New SWR
            Select Case swr.verifyUserAccount(SSStempFile, txtUserId.Text)
                Case 0
                    If swr.webResponse.processFlag <> "0" Then
                        SharedFunction.ShowWarningMessage("User ID is already existing.".ToUpper)
                    Else
                        Dim resultDis As Integer = MessageBox.Show("YOUR WEB REGISTRATION WILL BE SUBMITTED FOR PROCESSING." & vbNewLine & vbNewLine _
                                                       & "DO YOU WANT TO CONTINUE?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                        If resultDis = DialogResult.Yes Then

                            If SharedFunction.DisablePinOrFingerprint Then
                                SubmitSWR()
                            Else
                                CurrentTxnType = TxnType.SimplifiedWebRegistration
                                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                                xtd.getRawFile()
                                If Not _frm2 Is Nothing Then _frmMainMenu.DisposeForm(_frm2)
                                _frm2.CardType = CShort(xtd.checkFileType)
                                _frm2.TopLevel = False
                                _frm2.Parent = _frmMainMenu.splitContainerControl.Panel2
                                _frm2.Dock = DockStyle.Fill
                                _frm2.Show()
                            End If
                        ElseIf resultDis = DialogResult.No Then

                        End If

                    End If
                Case 1
                    SharedFunction.ShowUnableToConnectToRemoteServerMessage()
                Case 2
                    SharedFunction.ShowAPIResponseMessage(swr.verifyAccountResponse.ToUpper)
            End Select
            swr = Nothing
        End If
    End Sub

    Public Sub SubmitSWR()
        Dim swr As New SWR
        Select Case swr.insertToWesRegistration(SSStempFile, txtUserId.Text)
            Case 0
                If swr.webResponse.processFlag = "0" Then
                    _frmUserAuthentication.getTransacNum()
                    Dim inMat As New insertProcedure
                    inMat.insertSimplifiedWebRegistration(SSStempFile, "", txtUserId.Text, _frmUserAuthentication.lblTransactionNo.Text, HTMLDataExtractor.MemberFullName)
                    _frmSWR3.lblMessage.Text = _frmSWR3.lblMessage.Text.Replace("@email", "").Replace("@trn", _frmUserAuthentication.lblTransactionNo.Text)
                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmSWR3.TopLevel = False
                    _frmSWR3.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmSWR3.Dock = DockStyle.Fill
                    _frmSWR3.Show()
                    Me.Hide()
                Else
                    SharedFunction.ShowWarningMessage(swr.webResponse.returnMessage.ToUpper)
                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    Me.TopLevel = False
                    Me.Parent = _frmMainMenu.splitContainerControl.Panel2
                    Me.Dock = DockStyle.Fill
                    Me.Show()
                    Me.Hide()
                End If
            Case 1
                SharedFunction.ShowUnableToConnectToRemoteServerMessage()
            Case Else
                SharedFunction.ShowAPIResponseMessage(swr.exceptions.ToUpper)
        End Select

        swr = Nothing
    End Sub

    Private Sub LinkLabel2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles lbUserId.LinkClicked
        txtUserId.Clear()
        txtUserId.Focus()
    End Sub

    Private Sub txtEmail_Click(sender As Object, e As EventArgs) Handles txtUserId.Click
        _frmMainMenu.ShowVirtualKeyboard()
        CType(sender, TextBox).Focus()
    End Sub

    Private Sub txtUserId_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserId.KeyPress
        Try
            Dim txtLen As String

            Dim textLenStr As String = txtUserId.Text
            textLenStr = textLenStr.Substring(0, 1)

            txtLen = Len(txtUserId.Text)

            If (Char.IsLetterOrDigit(e.KeyChar)) Or (Microsoft.VisualBasic.Asc(e.KeyChar) = 95) Then
                e.Handled = False
            Else
                e.Handled = True
            End If

            If (Microsoft.VisualBasic.Asc(e.KeyChar) = 8) Then
                e.Handled = False
            End If

            If txtLen = 0 Then
                If Char.IsDigit(e.KeyChar) Then
                    e.Handled = True
                End If
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtUserId_Enter(sender As Object, e As EventArgs) Handles txtUserId.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub
End Class