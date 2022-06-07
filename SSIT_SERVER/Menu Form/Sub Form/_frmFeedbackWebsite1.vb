Public Class _frmFeedbackWebsite1
    Dim ac As Integer
   
    Private Sub _frmFeedbackWebsite1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tagPage = "13.2.2"

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            For Each ctrl As Control In Panel6.Controls
                ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)
                ctrl.Top = ctrl.Top - 10
            Next

            For Each ctrl As Control In Panel5.Controls
                ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)

                ctrl.Top = ctrl.Top - 30
            Next

            Panel3.Height = Panel3.Height - 20
            Panel21.Height = Panel21.Height - 20
        End If
    End Sub



    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmExitSurveyMain.TopLevel = False
            _frmExitSurveyMain.Parent = _frmSSSwebsite.Panel3
            _frmExitSurveyMain.Dock = DockStyle.Fill
            _frmExitSurveyMain.Show()
            tagPage = "13.2.1"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            If chk1_1.Checked = False And chk1_2.Checked = False And chk1_3.Checked = False And chk1_4.Checked = False And chk1_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk1_1.Focus()
            ElseIf chk2_1.Checked = False And chk2_2.Checked = False And chk2_3.Checked = False And chk2_4.Checked = False And chk2_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk2_1.Focus()
            ElseIf chk3_1.Checked = False And chk3_2.Checked = False And chk3_3.Checked = False And chk3_4.Checked = False And chk3_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk1_1.Focus()
            ElseIf chk4_1.Checked = False And chk4_2.Checked = False And chk4_3.Checked = False And chk4_4.Checked = False And chk4_5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chk4_1.Focus()
            Else
                _frmSSSwebsite.Panel3.Controls.Clear()
                _frmFeedbackWebsite2.TopLevel = False
                _frmFeedbackWebsite2.Parent = _frmSSSwebsite.Panel3
                _frmFeedbackWebsite2.Dock = DockStyle.Fill
                _frmFeedbackWebsite2.Show()
                tagPage = "13.2.3"

            End If
        Catch ex As Exception

        End Try


    End Sub
End Class