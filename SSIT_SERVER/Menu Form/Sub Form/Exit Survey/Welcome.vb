Public Class Welcome

    Public Sub New(Optional isWelcomeForm As Boolean = True)

        ' This call is required by the designer.
        InitializeComponent()
        Me.isWelcomeForm = isWelcomeForm

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private isWelcomeForm As Boolean


    Private Sub Welcome_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If Not isWelcomeForm Then
            lblHeader.Text = vbNewLine & "Your response has been recorded"
            Label1.Visible = False
            btnClose.Visible = True
        End If
    End Sub

    Private Sub Welcome_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (ClientSize.Width - Panel1.Width) / 2
        Panel1.Top = (ClientSize.Height - Panel1.Height) / 2
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        _frmExitSurveyIntro.Close()
        _frmExitSurveyMain.Close()
    End Sub

End Class
