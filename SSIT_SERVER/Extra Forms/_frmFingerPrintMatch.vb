Public Class _frmFingerPrintMatch

    Private Sub _frmFingerPrintMatch_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Timer1.Stop()
        Timer1.Start()
        Timer1.Interval = 5000
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        'Me.Close()
        '_frmMainMenu.Close()
        Me.Close()
    End Sub
End Class