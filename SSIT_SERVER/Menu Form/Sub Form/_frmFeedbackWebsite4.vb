Public Class _frmFeedbackWebsite4

    Dim ac As Integer
    Dim db As New ConnectionString
    Dim getAffectedTable As String
    Dim at As New auditTrail
    Dim xtd As New ExtractedDetails


    Private Sub _frmFeedbackWebsite4_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        tagPage = "13.2.5"

    End Sub


    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim resultRetirement As Integer = MessageBox.Show("ARE YOU SURE YOU WANT TO SUBMIT THE SUPPLIED INFORMATION? " & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultRetirement = DialogResult.No Then
            ElseIf resultRetirement = DialogResult.Yes Then
                _frmFeedbackWebsite.submitFeedback()
            End If
        Catch ex As Exception

        End Try



    End Sub
    Public Sub getAugitLogs()
        ' at.getModuleLogs(My.Settings.getID, My.Settings.fullName, "FORM : " & getModule, getTask, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
        'at.getModuleLogs(xtd.getTempSSS, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "")
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmFeedbackWebsite3.TopLevel = False
            _frmFeedbackWebsite3.Parent = _frmSSSwebsite.Panel3
            _frmFeedbackWebsite3.Dock = DockStyle.Fill
            _frmFeedbackWebsite3.Show()

            tagPage = "13.2.4"

        Catch ex As Exception

        End Try
    End Sub

    Private Sub rtbIf_Enter(sender As Object, e As EventArgs) Handles rtbIf.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

End Class