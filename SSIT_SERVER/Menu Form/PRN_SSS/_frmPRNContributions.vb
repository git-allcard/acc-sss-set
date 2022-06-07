Public Class _frmPRNContributions

    Private intScrollValue As Integer = 370
    Public SelectedContribution As String = ""
    Private TopCntr As Short = 0

    Private Sub _frmPRNContributions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GetContributionsFromWS()
    End Sub

    'Public Sub GetContributionsFromWS(ByVal cboContribution As ComboBox)
    Public Sub GetContributionsFromWS()
        Dim cboContribution As New ComboBox
        Dim ErrMsg As String = ""
        If Not SharedFunction.Get_getContributionListPRN(cboContribution, ErrMsg) Then
            cboContribution.Items.Clear()
            cboContribution.Items.Add("-POPULATION FAILED-")
            cboContribution.SelectedIndex = 0
        Else
            For Each ctrl As Control In SplitContainer1.Panel1.Controls
                If ctrl.Text <> "-Select Premium-" Then
                    Panel1.Controls.Remove(ctrl)
                End If
            Next

            Dim btn As Button
            Dim btn2 As Button
            Dim intTop As Integer = 41
            For i As Short = 1 To cboContribution.Items.Count - 1
                btn = New Button
                btn.Height = Button1.Height + 13
                btn.Width = Button1.Width
                btn.TextAlign = Button1.TextAlign
                btn.TextImageRelation = Button1.TextImageRelation
                btn.Font = Button1.Font
                btn.FlatStyle = Button1.FlatStyle
                btn.FlatAppearance.BorderSize = Button1.FlatAppearance.BorderSize
                btn.FlatAppearance.MouseOverBackColor = Button1.FlatAppearance.MouseOverBackColor
                btn.Location = New Point(btn.Left, intTop)
                btn.Text = "Php " & cboContribution.Items(i).ToString
                AddHandler btn.Click, AddressOf CloseForm
                Panel1.Controls.Add(btn)

                btn2 = New Button
                btn2.Height = Button2.Height
                btn2.Width = Button2.Width
                btn2.FlatStyle = Button2.FlatStyle
                btn2.FlatAppearance.BorderSize = Button2.FlatAppearance.BorderSize
                btn2.FlatAppearance.MouseOverBackColor = Button2.FlatAppearance.MouseOverBackColor
                btn2.BackgroundImage = Button2.BackgroundImage.Clone
                btn2.BackgroundImageLayout = Button2.BackgroundImageLayout
                btn2.Location = New Point(btn2.Left, intTop + btn.Height)
                Panel1.Controls.Add(btn2)

                intTop += btn.Height
            Next

            'btn = New Button
            'btn.Height = Button1.Height + 13
            'btn.Width = Button1.Width
            'btn.Font = Button1.Font
            'btn.FlatStyle = Button1.FlatStyle
            'btn.TextAlign = Button1.TextAlign
            'btn.FlatAppearance.BorderSize = Button1.FlatAppearance.BorderSize
            'btn.Location = New Point(btn.Left, intTop)
            'btn.Text = "Return to top"
            'AddHandler btn.Click, AddressOf ReturnToTop
            'Panel1.Controls.Add(btn)
        End If

        Panel1.VerticalScroll.Value = 0
        'Panel1.SetAutoScrollMargin(0, 1000)
        VScrollBar1.Maximum = 1000
        VScrollBar1.SmallChange = 200
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
            If Panel1.VerticalScroll.Value < intScrollValue Then
                Panel1.VerticalScroll.Value = 0
            Else
                Panel1.VerticalScroll.Value -= intScrollValue
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Try
            Panel1.VerticalScroll.Value += intScrollValue
        Catch ex As Exception
        End Try
    End Sub

    Private Sub Panel1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles Panel1.Scroll
       
    End Sub

    Private Sub VScrollBar1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs) Handles VScrollBar1.Scroll
        Dim range = VScrollBar1.Maximum - VScrollBar1.LargeChange + VScrollBar1.SmallChange
        Dim panelPos = (Panel1.AutoScrollMinSize.Height - Panel1.Height) * e.NewValue
        Panel1.VerticalScroll.Value = VScrollBar1.Value
        Panel1.AutoScrollPosition = New Point(panelPos, 0)

    End Sub

End Class