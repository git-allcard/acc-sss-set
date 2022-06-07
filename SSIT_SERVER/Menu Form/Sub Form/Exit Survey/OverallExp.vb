
Public Class OverallExp

    Public Sub New(ByRef selValue As Short)

        ' This call is required by the designer.
        InitializeComponent()
        Me.selValue = selValue

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Public selValue As Short

    Private Sub OverallExp_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Select Case selValue
            Case 1
                chk1_1.Checked = True
            Case 2
                chk1_2.Checked = True
            Case 3
                chk1_3.Checked = True
            Case 4
                chk1_4.Checked = True
            Case 5
                chk1_5.Checked = True
        End Select

        'lblAsterisk.Visible = Not IsValid()
    End Sub

    Public Function IsValid() As Boolean
        Dim cntr As Short = 0
        If chk1_1.Checked Then cntr += 1
        If chk1_2.Checked Then cntr += 1
        If chk1_3.Checked Then cntr += 1
        If chk1_4.Checked Then cntr += 1
        If chk1_5.Checked Then cntr += 1
        If cntr > 0 Then Return True Else Return False
    End Function

    Public Function SelectedValue() As Short
        If chk1_1.Checked Then Return 1
        If chk1_2.Checked Then Return 2
        If chk1_3.Checked Then Return 3
        If chk1_4.Checked Then Return 4
        If chk1_5.Checked Then Return 5
    End Function

    Private Sub OverallExp_Leave(sender As Object, e As EventArgs) Handles Me.Leave
        selValue = SelectedValue()
    End Sub

    Private Sub EasyToUse_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        Panel1.Left = (ClientSize.Width - Panel1.Width) / 2
        Panel1.Top = (ClientSize.Height - Panel1.Height) / 2
    End Sub

    Private Sub chk_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_1.CheckedChanged, chk1_2.CheckedChanged, chk1_3.CheckedChanged, chk1_4.CheckedChanged, chk1_5.CheckedChanged
        lblAsterisk.Visible = Not IsValid()
    End Sub

End Class
