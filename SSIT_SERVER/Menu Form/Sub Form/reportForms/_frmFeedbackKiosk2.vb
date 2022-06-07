Public Class _frmFeedbackKiosk2
    Public ac As Integer
    Dim db As New ConnectionString
    ' Dim ADDress As Integer ' 1 - HOME ,  2 BUSINESS ADDRESS
    Dim ssrate, ssrate1, ssrate2, ssrate3, ssrate4, ssrate5, ssrate6 As Integer
    Dim hlpTag As Integer

    Private Sub rtbComments_Enter(sender As Object, e As EventArgs) Handles rtbComments.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        _frmFeedbackKiosk.Panel2.Controls.Clear()
        _frmFeedbackKiosk1.TopLevel = False
        _frmFeedbackKiosk1.Parent = _frmFeedbackKiosk.Panel2
        _frmFeedbackKiosk1.Dock = DockStyle.Fill
        _frmFeedbackKiosk1.Show()
        Me.Hide()
    End Sub
    Private Sub result()
        If _frmFeedbackKiosk1.cba1.Checked = True Then
            ssrate1 = 1
        ElseIf _frmFeedbackKiosk1.cba2.Checked = True Then
            ssrate1 = 2
        ElseIf _frmFeedbackKiosk1.cba3.Checked = True Then
            ssrate1 = 3
        ElseIf _frmFeedbackKiosk1.cba4.Checked = True Then
            ssrate1 = 4
        ElseIf _frmFeedbackKiosk1.cba5.Checked = True Then
            ssrate1 = 5
        End If
        If _frmFeedbackKiosk1.cbb1.Checked = True Then
            ssrate2 = 1
        ElseIf _frmFeedbackKiosk1.cbb2.Checked = True Then
            ssrate2 = 2
        ElseIf _frmFeedbackKiosk1.cbb3.Checked = True Then
            ssrate2 = 3
        ElseIf _frmFeedbackKiosk1.cbb4.Checked = True Then
            ssrate2 = 4
        ElseIf _frmFeedbackKiosk1.cbb5.Checked = True Then
            ssrate2 = 5
        End If

        If _frmFeedbackKiosk1.cba1.Checked = True Then
            ssrate3 = 1

        ElseIf _frmFeedbackKiosk1.cbc2.Checked = True Then
            ssrate3 = 2
        ElseIf _frmFeedbackKiosk1.cbc3.Checked = True Then
            ssrate3 = 3
        ElseIf _frmFeedbackKiosk1.cbc4.Checked = True Then
            ssrate3 = 4
        ElseIf _frmFeedbackKiosk1.cbc5.Checked = True Then
            ssrate3 = 5
        End If
        If _frmFeedbackKiosk1.cbd1.Checked = True Then
            ssrate4 = 1
        ElseIf _frmFeedbackKiosk1.cbd2.Checked = True Then
            ssrate4 = 2
        ElseIf _frmFeedbackKiosk1.cbd3.Checked = True Then
            ssrate4 = 3
        ElseIf _frmFeedbackKiosk1.cbd4.Checked = True Then
            ssrate4 = 4
        ElseIf _frmFeedbackKiosk1.cbd5.Checked = True Then
            ssrate4 = 5
        End If
        If _frmFeedbackKiosk1.cbe1.Checked = True Then
            ssrate5 = 1
        ElseIf _frmFeedbackKiosk1.cbe2.Checked = True Then
            ssrate5 = 2
        ElseIf _frmFeedbackKiosk1.cbe3.Checked = True Then
            ssrate5 = 3
        ElseIf _frmFeedbackKiosk1.cbe4.Checked = True Then
            ssrate5 = 4
        ElseIf _frmFeedbackKiosk1.cbe5.Checked = True Then
            ssrate5 = 5
        End If
        If cbf1.Checked = True Then
            ssrate6 = 1
        ElseIf cbf2.Checked = True Then
            ssrate6 = 2
        ElseIf cbf3.Checked = True Then
            ssrate6 = 3
        ElseIf cbf4.Checked = True Then
            ssrate6 = 4
        ElseIf cbf5.Checked = True Then
            ssrate6 = 5
        End If

    End Sub
    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try

            If chkHelpYes.Checked = False And chkHelpNo.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                chkHelpYes.Focus()
            ElseIf cbf1.Checked = False And cbf1.Checked = False And cbf2.Checked = False And cbf3.Checked = False And cbf4.Checked = False And cbf5.Checked = False Then
                MsgBox("PLEASE COMPLETE YOUR ANSWERS.", MsgBoxStyle.Information, "Information")
                cbf1.Focus()
            Else

                Dim resultRetirement As Integer = MessageBox.Show("ARE YOU SURE YOU WANT TO SUBMIT THE SUPPLIED INFORMATION? " & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If resultRetirement = DialogResult.No Then
                ElseIf resultRetirement = DialogResult.Yes Then
                    tagPage = "16.1.0"
                    Dim add_typ As Integer
                    If _frmFeedbackKiosk.rbtBusinesAdd.CheckState = CheckState.Checked Then
                        add_typ = 0
                    ElseIf _frmFeedbackKiosk.rbtHomeAddress.CheckState = CheckState.Checked Then
                        add_typ = 1
                    End If
                    result()

                    'ARGIE101
                    Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                    Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                    Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                    'Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                    'Dim kioskID As String = kioskID
                    Dim getDateTime As String = Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt")
                    Dim add2 As String = _frmFeedbackKiosk.txtAddress2.Text & " " & _frmFeedbackKiosk.txtCP.Text
                    db.sql = "insert into SSTRANSINFOTERMFBKIOSK (NAME,EMAIL,ADDRESS_TYP, ADD_1,ADD_2, POST_CD,SSRATE1,SSRATE2,SSRATE3,SSRATE4, SSRATE5, SSRATE6, HELP_TAG,COMMNT_TAG,ENCODE_DT,BRANCH_CD,CLSTR,DIVSN,KIOSK_ID) values ('" & _
                        _frmFeedbackKiosk.txtName.Text & "', '" & _frmFeedbackKiosk.txtEmail.Text & "', '" & _
                         add_typ & "', '" & _frmFeedbackKiosk.txtAddress1.Text & _
                        "', '" & add2 & "', '" & _frmFeedbackKiosk.txtZipCode.Text & "', '" & _
                        ssrate1 & "', '" & ssrate2 & "', '" & ssrate3 & "', '" & ssrate4 & "', '" & ssrate5 & "', '" & ssrate6 & _
                        "', '" & hlpTag & "', '" & rtbComments.Text & "','" & getDateTime & "', '" & getbranchCoDE & "', '" & getkiosk_cluster & "', '" & getkiosk_group & "', '" & kioskID & "')"

                    db.ExecuteSQLQuery(db.sql)


                    Dim getdate As String = Date.Today.ToString("ddMMyyyy")

                    'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "FBKTRANS1Logs.txt", True)
                    '    SW.WriteLine(_frmFeedbackKiosk.txtName.Text & "," & _frmFeedbackKiosk.txtEmail.Text & "," & _frmFeedbackKiosk.txtEmail.Text.Trim & _
                    '                 "," & add_typ & "," & _frmFeedbackKiosk.txtAddress1.Text & "," & add2 & "," & ssrate1 & "," & ssrate2 & _
                    '                 "," & ssrate3 & "," & ssrate4 & "," & ssrate5 & "," & ssrate6 & "," & hlpTag & "," & rtbComments.Text & "," & _
                    '                 "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                    'End Using

                    MsgBox("YOU HAVE SUCCESSFULLY SUBMITTED YOUR FEEDBACK." & vbNewLine & vbNewLine & "THANK YOU FOR YOUR TIME.", vbInformation, "Feedback")
                    _frmFeedbackKiosk.clearAll()
                    'Dim pathX1 As String = Application.StartupPath & "\" & "temp" & "\"
                    '_frmMainMenu.DeleteFile(pathX1)

                    'Application.Exit()
                    'Dim startInfo As New ProcessStartInfo(Application.StartupPath & "\" & "SSIT_Home" & "\" & "SSIT_HOME.exe")
                    'startInfo.Arguments = 2
                    'Process.Start(startInfo)
                    '_frmFeedbackKioskMain.Close()

                    _frmMainMenu.IsMainMenuActive = False
                    _frmMainMenu.Hide()
                    SharedFunction.ShowMainDefaultUserForm()
                    Main.Show()


                    'SharedFunction.ShowAppMainForm(Me)
                End If


            End If

        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            _frmFeedbackKioskMain.Panel4.Controls.Clear()
            _frmFeedbackKiosk1.TopLevel = False
            _frmFeedbackKiosk1.Parent = _frmFeedbackKioskMain.Panel4
            _frmFeedbackKiosk1.Dock = DockStyle.Fill
            _frmFeedbackKiosk1.Show()

            tagPage = "16.1.2"
        Catch ex As Exception

        End Try
    End Sub

    Private Sub _frmFeedbackKiosk2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tagPage = "16.1.3"

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            For Each ctrl2 As Control In Panel3.Controls
                ctrl2.Font = New Font(ctrl2.Font.Name, ctrl2.Font.Size - 3, ctrl2.Font.Style)
            Next

            For Each ctrl3 As Control In Panel4.Controls
                ctrl3.Font = New Font(ctrl3.Font.Name, ctrl3.Font.Size - 3, ctrl3.Font.Style)
            Next

            For Each ctrl4 As Control In Panel16.Controls
                ctrl4.Font = New Font(ctrl4.Font.Name, ctrl4.Font.Size - 3, ctrl4.Font.Style)
            Next

            rtbComments.Height = rtbComments.Height - 50
            Panel1.Height = Panel1.Height - 20
            Panel5.Height = Panel5.Height - 20
        End If
    End Sub
    Private Sub rates()
        If _frmFeedbackKiosk1.cba1.Checked = True Then
            ssrate1 = 1
        ElseIf _frmFeedbackKiosk1.cba2.Checked = True Then
            ssrate1 = 2
        ElseIf _frmFeedbackKiosk1.cba3.Checked = True Then
            ssrate1 = 3
        ElseIf _frmFeedbackKiosk1.cba4.Checked = True Then
            ssrate1 = 4
        ElseIf _frmFeedbackKiosk1.cba5.Checked = True Then
            ssrate1 = 5
        End If

        If _frmFeedbackKiosk1.cbb1.Checked = True Then
            ssrate2 = 1
        ElseIf _frmFeedbackKiosk1.cbb2.Checked = True Then
            ssrate2 = 2
        ElseIf _frmFeedbackKiosk1.cbb3.Checked = True Then
            ssrate2 = 3
        ElseIf _frmFeedbackKiosk1.cbb4.Checked = True Then
            ssrate2 = 4
        ElseIf _frmFeedbackKiosk1.cbb5.Checked = True Then
            ssrate2 = 5
        End If

        If _frmFeedbackKiosk1.cbc1.Checked = True Then
            ssrate3 = 1
        ElseIf _frmFeedbackKiosk1.cbc2.Checked = True Then
            ssrate3 = 2
        ElseIf _frmFeedbackKiosk1.cbc3.Checked = True Then
            ssrate3 = 3
        ElseIf _frmFeedbackKiosk1.cbc4.Checked = True Then
            ssrate3 = 4
        ElseIf _frmFeedbackKiosk1.cbc5.Checked = True Then
            ssrate3 = 5
        End If
        If _frmFeedbackKiosk1.cbd1.Checked = True Then
            ssrate4 = 1
        ElseIf _frmFeedbackKiosk1.cbd2.Checked = True Then
            ssrate4 = 2
        ElseIf _frmFeedbackKiosk1.cbd3.Checked = True Then
            ssrate4 = 3
        ElseIf _frmFeedbackKiosk1.cbd4.Checked = True Then
            ssrate4 = 4
        ElseIf _frmFeedbackKiosk1.cbd5.Checked = True Then
            ssrate4 = 5
        End If
        If _frmFeedbackKiosk1.cbe1.Checked = True Then
            ssrate5 = 1
        ElseIf _frmFeedbackKiosk1.cbe2.Checked = True Then
            ssrate5 = 2
        ElseIf _frmFeedbackKiosk1.cbe3.Checked = True Then
            ssrate5 = 3
        ElseIf _frmFeedbackKiosk1.cbe4.Checked = True Then
            ssrate5 = 4
        ElseIf _frmFeedbackKiosk1.cbe5.Checked = True Then
            ssrate5 = 5
        End If
        If cbf1.Checked = True Then
            ssrate6 = 1
        ElseIf cbf2.Checked = True Then
            ssrate6 = 2
        ElseIf cbf3.Checked = True Then
            ssrate6 = 3
        ElseIf cbf4.Checked = True Then
            ssrate6 = 4
        ElseIf cbf5.Checked = True Then
            ssrate6 = 5
        End If

        If chkHelpYes.Checked = True Then
            hlpTag = 1
        ElseIf chkHelpNo.Checked = True Then
            hlpTag = 0
        End If

    End Sub
End Class