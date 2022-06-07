Public Class _frmLoading
    Dim maxNum As Integer

    Private Sub _frmLoading_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Start()
        CircularProgress1.Value = 0


    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CircularProgress1.Value = CircularProgress1.Value + 1
        CircularProgress1.Minimum = 0
        CircularProgress1.Maximum = 20
        If CircularProgress1.Value = 20 Then
            CircularProgress1.Value = 0
        End If


    End Sub

    Private Sub _frmLoading_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class