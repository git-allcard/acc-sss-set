Public Class _frmLoad

    Private Sub _frmLoad_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub
    Public Function SSSID(ByVal timerx As Timer)

        ProgressBarX1.Value = 0
        ProgressBarX1.Minimum = 0
        ProgressBarX1.Maximum = 4

        If ProgressBarX1.Value = 0 Then
            ' lblStats.Text = _frmSSSIDClearance.lblName.Text
            ProgressBarX1.Value = ProgressBarX1.Value + 1
        End If
        If ProgressBarX1.Value = 1 Then
            'lblStats.Text = _frmSSSIDClearance.lblSS.Text
            ProgressBarX1.Value = ProgressBarX1.Value + 1
        End If

        If ProgressBarX1.Value = 2 Then
            ' lblStats.Text = _frmSSSIDClearance.lblEmpID.Text
            ProgressBarX1.Value = ProgressBarX1.Value + 1
        End If
        If ProgressBarX1.Value = 3 Then
            ' lblStats.Text = _frmSSSIDClearance.lblCapturedOn.Text
            ProgressBarX1.Value = ProgressBarX1.Value + 1
        End If
        If ProgressBarX1.Value = 4 Then
            'lblStats.Text = _frmSSSIDClearance.lblGeneratedOn.Text
            ProgressBarX1.Value = ProgressBarX1.Value + 1
        End If

        If ProgressBarX1.Value = 4 Then
            tmrTimer.Stop()
            Me.Hide()
            ProgressBarX1.Value = 0
            ProgressBarX1.Refresh()
            MsgBox("Done Printing!", MsgBoxStyle.Information, "Transaction Printing")
        End If

    End Function

    Private Sub _frmLoad_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class