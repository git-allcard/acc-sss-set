Public Class _frmExitSurveyIntro

    Public memberName As String = ""

    Private Sub pbClose_Click(sender As Object, e As EventArgs) Handles pbClose.Click
        Close()
    End Sub

    Private Sub pbSurvey_Click(sender As Object, e As EventArgs) Handles pbSurvey.Click
        Hide()
        _frmMainMenu.DisposeForm(_frmExitSurveyMain)
        _frmExitSurveyMain.memberName = memberName
        _frmExitSurveyMain.ShowDialog()
    End Sub

End Class