Public Class frmSSSWebsiteMsg

    Public IsProceed As Boolean = False

    Private Sub frmSSSWebsiteMsg_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        IsProceed = True
        Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        IsProceed = False
        Close()
    End Sub
End Class