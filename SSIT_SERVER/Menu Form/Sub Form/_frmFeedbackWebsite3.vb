Public Class _frmFeedbackWebsite3
    Dim ac As Integer


    Private Sub _frmFeedbackWebsite3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tagPage = "13.2.4"

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        If rbtNo.Checked = False And rbtUndc.Checked = False And rbtYes.Checked = False Then
            MsgBox("PLEASE SELECT A TYPE.", MsgBoxStyle.Information, "Information")
            rbtYes.Focus()

        ElseIf rbtUndc.Checked = True And rtbWhy.Text.Trim = "" Then
            MsgBox("PLEASE INPUT THE REASON WHY DO YOU NOT INTEND TO VISIT THE SITE AGAIN.", MsgBoxStyle.Information, "Information")
            rtbWhy.Focus()
        ElseIf rbtNo.Checked = True And rtbWhy.Text.Trim = "" Then
            MsgBox("PLEASE INPUT THE REASON WHY DO YOU NOT INTEND TO VISIT THE SITE AGAIN.", MsgBoxStyle.Information, "Information")
            rtbWhy.Focus()
        Else
            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmFeedbackWebsite4.TopLevel = False
            _frmFeedbackWebsite4.Parent = _frmSSSwebsite.Panel3
            _frmFeedbackWebsite4.Dock = DockStyle.Fill
            _frmFeedbackWebsite4.Show()
            Me.Hide()
            tagPage = "13.2.5"
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmFeedbackWebsite2.TopLevel = False
            _frmFeedbackWebsite2.Parent = _frmSSSwebsite.Panel3
            _frmFeedbackWebsite2.Dock = DockStyle.Fill
            _frmFeedbackWebsite2.Show()
            tagPage = "13.2.3"
        Catch ex As Exception

        End Try

    End Sub

    Private Sub rtbWhy_Enter(sender As Object, e As EventArgs) Handles rtbWhy.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub
End Class