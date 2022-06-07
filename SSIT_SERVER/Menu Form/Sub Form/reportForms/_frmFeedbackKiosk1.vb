Public Class _frmFeedbackKiosk1

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try

            If cba1.Checked = False And cba2.Checked = False And cba3.Checked = False And cba4.Checked = False And cba5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cba1.Focus()
            ElseIf cbb1.Checked = False And cbb2.Checked = False And cbb3.Checked = False And cbb4.Checked = False And cbb5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cbb1.Focus()
            ElseIf cbc1.Checked = False And cbc2.Checked = False And cbc3.Checked = False And cbc4.Checked = False And cbc5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cbc1.Focus()
            ElseIf cbd1.Checked = False And cbd2.Checked = False And cbd3.Checked = False And cbd4.Checked = False And cbd5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cbd1.Focus()
            ElseIf cbe1.Checked = False And cbe2.Checked = False And cbe3.Checked = False And cbe4.Checked = False And cbe5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cbe1.Focus()
            Else
                _frmFeedbackKioskMain.Panel4.Controls.Clear()
                _frmFeedbackKiosk2.TopLevel = False
                _frmFeedbackKiosk2.Parent = _frmFeedbackKioskMain.Panel4
                _frmFeedbackKiosk2.Dock = DockStyle.Fill
                _frmFeedbackKiosk2.Show()

                tagPage = "16.1.3"
            End If
        Catch ex As Exception

        End Try


    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmFeedbackKioskMain.Panel4.Controls.Clear()
            _frmFeedbackKiosk.TopLevel = False
            _frmFeedbackKiosk.Parent = _frmFeedbackKioskMain.Panel4
            _frmFeedbackKiosk.Dock = DockStyle.Fill
            _frmFeedbackKiosk.Show()

            tagPage = "16.1.1"
        Catch ex As Exception

        End Try

    End Sub

    Private Sub _frmFeedbackKiosk1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tagPage = "16.1.2"

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            For Each ctrl As Control In Panel4.Controls
                ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)
                Select Case ctrl.Name
                    'Case "Panel5", "Panel18", "Panel19", "Panel20", "Panel21"
                    Case "Label1"
                    Case "Label5"
                        ctrl.Top = ctrl.Top - 10
                    Case Else
                        ctrl.Top = ctrl.Top - 20
                End Select
            Next

            Panel3.Height = Panel3.Height - 20
            Panel2.Height = Panel2.Height - 20
        End If
    End Sub
End Class