Public Class usrfrmIdle
    Dim xs As New MySettings
    Dim db As New ConnectionString
    'Public _NotfirstRun As Boolean
    Private Sub usrfrmIdle_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Click
        SharedFunction.ShowMainDefaultUserForm()
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Form2.ShowDialog()
    End Sub

    Private Sub usrfrmIdle_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

End Class
