
Public Class FindInfo

    Public Sub New(ByRef selValue As Short)

        ' This call is required by the designer.
        InitializeComponent()
        Me.selValue = selValue

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public selValue As Short

    Private Sub FindInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case selValue
            Case 1
                chk1_1.Checked = True
            Case 2
                chk1_2.Checked = True
        End Select

        'lblAsterisk.Visible = Not IsValid()
    End Sub

    Public Function IsValid() As Boolean
        Dim cntr As Short = 0
        If chk1_1.Checked Then cntr += 1
        If chk1_2.Checked Then cntr += 1
        If cntr > 0 Then Return True Else Return False
    End Function

    Public Function SelectedValue() As Short
        If chk1_1.Checked Then Return 1
        If chk1_2.Checked Then Return 2
    End Function

    Private Sub EasyToUse_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        selValue = SelectedValue()
    End Sub

    Private Sub FindInfo_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (ClientSize.Width - Panel1.Width) / 2
        Panel1.Top = (ClientSize.Height - Panel1.Height) / 2
    End Sub

    Private Sub chk_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_1.CheckedChanged, chk1_2.CheckedChanged
        lblAsterisk.Visible = Not IsValid()
    End Sub

    Private Sub chk1_1_Click(sender As Object, e As EventArgs) Handles chk1_1.Click
        chk1_2.Checked = chk1_1.Checked
    End Sub

    Private Sub chk1_2_Click(sender As Object, e As EventArgs) Handles chk1_2.Click
        chk1_1.Checked = chk1_2.Checked
    End Sub

End Class
