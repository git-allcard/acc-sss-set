Public Class _frmFirstLoad

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        CircularProgress1.Value = CircularProgress1.Value + 1
        
        If CircularProgress1.Value = 30 Then
            Me.Dispose()
            _frmMainMenu.trd1.Abort()
            '  _frmMainMenu.navMain()
            CircularProgress1.Value = 0
            Timer1.Stop()
        End If
    End Sub

    Private Sub _frmFirstLoad_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub _frmFirstLoad_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        DoubleBuffered = True
        Timer1.Start()
        CircularProgress1.Minimum = 0
        CircularProgress1.Maximum = 70
        CircularProgress1.Value = 0
    End Sub
End Class