Public Class _frmPRNContributions2

    Private intBracket As Short = 0
    Private intCountPerBracket As Short = 7
    Public SelectedContribution As String = ""
    Public cboContribution As ComboBox

    Private Sub _frmPRNContributions2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'GetContributionsFromWS()
    End Sub

    Public Sub GetContributionsFromWS(ByVal cboContribution As ComboBox)
        Me.cboContribution = cboContribution
       PopulateContribution()
    End Sub

    Public Sub GetContributionsFromWS2(ByVal cboContribution As ComboBox)
        'Public Sub GetContributionsFromWS()
        'cboContribution = New ComboBox
        Me.cboContribution = cboContribution
        Dim ErrMsg As String = ""
        If Not SharedFunction.Get_getContributionListPRN(cboContribution, ErrMsg) Then
            cboContribution.Items.Clear()
            cboContribution.Items.Add("-POPULATION FAILED-")
            cboContribution.SelectedIndex = 0
        Else
            PopulateContribution()
        End If
    End Sub

    Private Sub PopulateContribution()
        BindToButton(intBracket + 1, btn1)
        BindToButton(intBracket + 2, btn2)
        BindToButton(intBracket + 3, btn3)
        BindToButton(intBracket + 4, btn4)
        BindToButton(intBracket + 5, btn5)
        BindToButton(intBracket + 6, btn6)
        BindToButton(intBracket + 7, btn7)
        BindToButton(intBracket + 8, btn8)
    End Sub

    Private Sub BindToButton(ByVal index As Short, ByRef btn As Button)
        Try
            Dim btn_Text As String = "Php " & cboContribution.Items(index).ToString
            btn.Text = btn_Text
            btn.Visible = True
            Panel1.Controls(btn.Name & "Sep").Visible = True
            btnDown.Visible = True
        Catch ex As Exception
            btn.Visible = False
            Panel1.Controls(btn.Name & "Sep").Visible = False
            btnDown.Visible = False
        End Try
    End Sub

    Private Sub CloseForm(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SelectedContribution = CType(sender, Button).Text
        Close()
    End Sub

    Private Sub ReturnToTop(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Panel1.VerticalScroll.Value = 0
    End Sub

    Private Sub btnUp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUp.Click
        Try
            If intBracket > 0 Then
                intBracket -= intCountPerBracket
            Else
                intBracket = 0
            End If
            PopulateContribution()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Try
            intBracket += intCountPerBracket
            PopulateContribution()
        Catch ex As Exception
        End Try
    End Sub
   
    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click
        SelectedContribution = CType(sender, Button).Text.Replace("Php ", "")
        Close()
    End Sub

End Class