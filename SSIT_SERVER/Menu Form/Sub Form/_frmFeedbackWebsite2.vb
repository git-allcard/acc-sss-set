Public Class _frmFeedbackWebsite2
    Dim ac As Integer

    Private Sub _frmFeedbackWebsite2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tagPage = "13.2.3"

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmFeedbackWebsite1.TopLevel = False
            _frmFeedbackWebsite1.Parent = _frmSSSwebsite.Panel3
            _frmFeedbackWebsite1.Dock = DockStyle.Fill
            _frmFeedbackWebsite1.Show()
            tagPage = "13.2.2"
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If chk5_1.Checked = False And chk5_2.Checked = False And chk5_3.Checked = False And chk5_4.Checked = False And chk5_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk5_1.Focus()
            ElseIf chk6_1.Checked = False And chk6_2.Checked = False And chk6_3.Checked = False And chk6_4.Checked = False And chk6_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk6_1.Focus()
            ElseIf chk7_1.Checked = False And chk7_2.Checked = False And chk7_3.Checked = False And chk7_4.Checked = False And chk7_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk7_1.Focus()
            Else
                _frmSSSwebsite.Panel3.Controls.Clear()
                _frmFeedbackWebsite3.TopLevel = False
                _frmFeedbackWebsite3.Parent = _frmSSSwebsite.Panel3
                _frmFeedbackWebsite3.Dock = DockStyle.Fill
                _frmFeedbackWebsite3.Show()

                tagPage = "13.2.4"

            End If
        Catch ex As Exception

        End Try


    End Sub


End Class