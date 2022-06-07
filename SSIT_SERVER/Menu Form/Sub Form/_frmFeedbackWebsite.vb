

Public Class _frmFeedbackWebsite
    Dim sp As New SMTP
    Public vTag As Integer  ' 0- NO, 1- YES, 2- UNDECIDED
    Dim ac, acTr As Integer
    Dim asd As New Store
    Dim db As New ConnectionString
    Dim fullnameFromCard As String
    Public autogenID As String '  1 - HOME ADDRESS , 2 - BUSINESS ADDRESS
    Public rbtTypeAdd As String
    Public rbtTagVisit As String
    Dim at As New auditTrail
    Dim getModule As String
    Dim getTask As String
    Dim getAffectedTable As String
    Dim getDetailstask As String
    Dim xtd As New ExtractedDetails
    Public txtWhy, txtWhat, txtIf As String
    Public num1, num2, num3, num4, num5, num6, num7 As String
    Dim add_typ As Integer
    Dim ssrate1, ssrate2, ssrate3, ssrate4, ssrate5, ssrate6, ssrate7 As Integer
    Dim tagVst As Integer
    Dim rtn As Integer
    Dim transNum As String
    Dim transDesc As String

    Dim webFeedEmail As String
    Dim sssWebsiteLink As String

    ReadOnly ValidChars As String =
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.- "
    ReadOnly ValidCharsCity As String =
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,- "
    ReadOnly ValidCharsNum As String =
"0123456789 "

    'Private urlString As String = sssWebsiteLink
    Private urlString As String = readSettings(xml_Filename, xml_path, "sssWebsiteLink")
    Private Sub rate()
        If _frmFeedbackWebsite1.chk1_1.Checked = True Then
            ssrate1 = 1
        ElseIf _frmFeedbackWebsite1.chk1_2.Checked = True Then
            ssrate1 = 2
        ElseIf _frmFeedbackWebsite1.chk1_3.Checked = True Then
            ssrate1 = 3
        ElseIf _frmFeedbackWebsite1.chk1_4.Checked = True Then
            ssrate1 = 4
        ElseIf _frmFeedbackWebsite1.chk1_5.Checked = True Then
            ssrate1 = 5
        End If

        If _frmFeedbackWebsite1.chk2_1.Checked = True Then
            ssrate2 = 1
        ElseIf _frmFeedbackWebsite1.chk2_2.Checked = True Then
            ssrate2 = 2
        ElseIf _frmFeedbackWebsite1.chk2_3.Checked = True Then
            ssrate2 = 3
        ElseIf _frmFeedbackWebsite1.chk2_4.Checked = True Then
            ssrate2 = 4
        ElseIf _frmFeedbackWebsite1.chk2_5.Checked = True Then
            ssrate2 = 5
        End If

        If _frmFeedbackWebsite1.chk3_1.Checked = True Then
            ssrate3 = 1
        ElseIf _frmFeedbackWebsite1.chk3_2.Checked = True Then
            ssrate3 = 2
        ElseIf _frmFeedbackWebsite1.chk3_3.Checked = True Then
            ssrate3 = 3
        ElseIf _frmFeedbackWebsite1.chk3_4.Checked = True Then
            ssrate3 = 4
        ElseIf _frmFeedbackWebsite1.chk3_5.Checked = True Then
            ssrate3 = 5
        End If

        If _frmFeedbackWebsite1.chk4_1.Checked = True Then
            ssrate4 = 1
        ElseIf _frmFeedbackWebsite1.chk4_2.Checked = True Then
            ssrate4 = 2
        ElseIf _frmFeedbackWebsite1.chk4_3.Checked = True Then
            ssrate4 = 3
        ElseIf _frmFeedbackWebsite1.chk4_4.Checked = True Then
            ssrate4 = 4
        ElseIf _frmFeedbackWebsite1.chk4_5.Checked = True Then
            ssrate4 = 5
        End If

        If _frmFeedbackWebsite2.chk5_1.Checked = True Then
            ssrate5 = 1
        ElseIf _frmFeedbackWebsite2.chk5_2.Checked = True Then
            ssrate5 = 2
        ElseIf _frmFeedbackWebsite2.chk5_3.Checked = True Then
            ssrate5 = 3
        ElseIf _frmFeedbackWebsite2.chk5_4.Checked = True Then
            ssrate5 = 4
        ElseIf _frmFeedbackWebsite2.chk5_5.Checked = True Then
            ssrate5 = 5
        End If

        If _frmFeedbackWebsite2.chk6_1.Checked = True Then
            ssrate6 = 1
        ElseIf _frmFeedbackWebsite2.chk6_2.Checked = True Then
            ssrate6 = 2
        ElseIf _frmFeedbackWebsite2.chk6_3.Checked = True Then
            ssrate6 = 3
        ElseIf _frmFeedbackWebsite2.chk6_4.Checked = True Then
            ssrate6 = 4
        ElseIf _frmFeedbackWebsite2.chk6_5.Checked = True Then
            ssrate6 = 5
        End If

        If _frmFeedbackWebsite2.chk7_1.Checked = True Then
            ssrate7 = 1
        ElseIf _frmFeedbackWebsite2.chk7_2.Checked = True Then
            ssrate7 = 2
        ElseIf _frmFeedbackWebsite2.chk7_3.Checked = True Then
            ssrate7 = 3
        ElseIf _frmFeedbackWebsite2.chk7_4.Checked = True Then
            ssrate7 = 4
        ElseIf _frmFeedbackWebsite2.chk7_5.Checked = True Then
            ssrate7 = 5
        End If
    End Sub

    Public Sub submitFeedback()

        'ARGIE101
        'db.sql = "insert into SSTRANSINFOTERMFB (SSNUM,EMAIL,ADDRESS_TYP,ADD_1,ADD_2,POST_CD,SSRATE1,SSRATE2,SSRATE3,SSRATE4,SSRATE5,SSRATE6,SSRATE7,VST_TAG,REASN_TAG,INFO_TAG,COMMNT_TAG,ENCODE_DT) values ('" & txtName.Text & "','" & txtEmail.Text & _
        '"','" & rbtTypeAdd & "','" & txtAddress1.Text & "','" & txtAddress2.Text & "','" & txtZipCode.Text _
        '& "','" & num1 & "','" & num2 & "','" & num3 & "','" & num4 & "','" & num5 & "','" & num6 & _
        '"','" & num7 & "','" & rbtTagVisit & "','" & txtWhy & "','" & txtWhat & "','" & txtIf & "','" & Date.Today & "') "

        If _frmFeedbackWebsite3.rbtYes.Checked = True Then
            tagVst = 1
        ElseIf _frmFeedbackWebsite3.rbtNo.Checked = True Then
            tagVst = 0
        ElseIf _frmFeedbackWebsite3.rbtUndc.Checked = True Then
            tagVst = 2
        End If
        rate()



        'Dim smtpUser As String = "noreply"
        'Dim smtpPass As String = "sss_sdd"
        'Dim smtpHost As String = "sssemail"
        'Dim smtpMailAdd As String = "noreply@sss.gov.ph"

        Dim smtpUser As String = SharedFunction.SMTP_USER
        Dim smtpPass As String = SharedFunction.SMTP_PASS
        Dim smtpHost As String = SharedFunction.SMTP_HOST
        Dim smtpMailAdd As String = SharedFunction.SMTP_MAILADD

        Dim smtpSubj As String = "Subject: Website Feedback Through SET"
        Dim getKioskName As String = kioskName
        'getKioskName = getKioskName.Substring(getKioskName.Length - 3)
        If getKioskName <> "" Then getKioskName = getKioskName.Substring(0, 1)

        Dim answerFBW As String
        Select Case tagVst
            Case 0
                answerFBW = "NO"
            Case 1
                answerFBW = "YES"
            Case 2
                answerFBW = "UNDECIDED"
            Case Else
                answerFBW = "UNDECIDED"

        End Select
        Dim smtpBody As String = "<p>Hello Sir,</p>" &
                                 "<p>" & txtName.Text & " has submitted his/her feedback for the SSS website. Please see details below.</p>" &
                                 "<label><strong>Complete Name:</strong> " & txtName.Text & " </label><br/>" &
                                 "<label><strong>Email Address:</strong> " & txtEmail.Text & " </label><br/>" &
                                 "<label><strong>Home Address:</strong> " & txtAddress1.Text & " " & txtAddress2.Text & " </label><br/>" &
                                 "<p><strong>Ratings:</strong></p>" &
                                 "<ol>" &
                                 "<li>Ability to inform members of their benefits and privileges under the System, as well as developments or new programs.<br/>" &
                                 "<label><strong>Rate: " & ssrate1 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Attractiveness of design or layout.<br/>" &
                                 "<label><strong>Rate: " & ssrate2 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Organization of information or categories.<br/>" &
                                 "<label><strong>Rate: " & ssrate3 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Speed of downloading material.<br/>" &
                                 "<label><strong>Rate: " & ssrate4 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Usefulness of information.<br/>" &
                                 "<label><strong>Rate: " & ssrate5 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Adequacy or exhaustiveness of information provided.<br/>" &
                                 "<label><strong>Rate: " & ssrate6 & " </strong></label></li>" &
                                 "</li>" &
                                 "<li>Ease of locating topics.<br/>" &
                                 "<label><strong>Rate: " & ssrate7 & " </strong></label></li>" &
                                 "</li>" &
                                 "</ol>" &
                                 "<p>Do you intend to visit the site again?<br/>" &
                                 "<label><strong>Answer:</strong> " & answerFBW & " </label><br/>" &
                                 "Why?<br/>" &
                                 "<label><strong>Answer:</strong> " & _frmFeedbackWebsite3.rtbWhy.Text & " </label></p>" &
                                 "<p>What other information would you like the SSS Web Pages to contain?<br/>" &
                                 "<label><strong>Answer:</strong> " & _frmFeedbackWebsite3.rtbWhat.Text & " </label></p>" &
                                 "<p><strong>Comments/Suggestions</strong><br/>" &
                                 "<label> " & _frmFeedbackWebsite4.panel001.Text & " </label></p>" &
                                 "<br/>" &
                                 "<p>This is a system-generated e-mail. Please do not reply.</p>"


        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")

        Dim sendEmail As New DAL_Oracle
        Dim sendEmailResponse As String = ""
        'webFeedEmail
        If sendEmail.pr_send_mail(webFeedEmail, smtpSubj, smtpBody, smtpBody, sendEmailResponse) Then
            If sendEmailResponse = "success" Then
                Dim add2 As String = txtAddress2.Text & " " & txtCP.Text
                Dim getDateTime As String = Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt")
                'Dim kioskID As String = kioskID
                db.sql = "insert into SSTRANSINFOTERMFB (NAME,EMAIL,ADDRESS_TYP,ADD_1,ADD_2,POST_CD,SSRATE1,SSRATE2,SSRATE3,SSRATE4,SSRATE5,SSRATE6,SSRATE7,VST_TAG,REASN_TAG,INFO_TAG,COMMNT_TAG,ENCODE_DT,BRANCH_CD,CLSTR,DIVSN,KIOSK_ID) values ('" &
                               txtName.Text & "', '" & txtEmail.Text.Trim & "', '" & add_typ & "', '" & txtAddress1.Text & "', '" & add2 & "', '" &
                               txtZipCode.Text & "', '" & ssrate1 & "', '" & ssrate2 & "', '" & ssrate3 & "', '" & ssrate4 & "', '" & ssrate5 & "', '" & ssrate6 &
                               "', '" & ssrate7 & "', '" & tagVst & "', '" & _frmFeedbackWebsite3.rtbWhy.Text & "', '" & _frmFeedbackWebsite3.rtbWhat.Text &
                               "', '" & _frmFeedbackWebsite4.panel001.Text & "', '" & getDateTime & "', '" & getbranchCoDE & "', '" & getkiosk_cluster & "', '" & getkiosk_group & "', '" & kioskID & "')"

                db.ExecuteSQLQuery(db.sql)
                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "FBWTRANS1Logs.txt", True)
                '    SW.WriteLine(txtName.Text & "," & txtEmail.Text.Trim & "," & add_typ & "," & txtAddress1.Text & "," & add2 & _
                '                 "," & txtZipCode.Text & "," & ssrate1 & "," & ssrate2 & "," & ssrate3 & "," & ssrate4 & "," & ssrate5 & "," & ssrate6 & _
                '                 "," & ssrate7 & "," & tagVst & "," & _frmFeedbackWebsite3.rtbWhy.Text & "," & _frmFeedbackWebsite3.rtbWhat.Text & _
                '                 "," & _frmFeedbackWebsite4.panel001.Text & "," & Date.Today & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                'End Using

                MsgBox("YOU HAVE SUCCESSFULLY SUBMITTED YOUR FEEDBACK." & vbNewLine & vbNewLine & "THANK YOU FOR YOUR TIME.", MsgBoxStyle.Information, "INFORMATION")
                _frmSSSwebsite.Button1.Enabled = True
                _frmSSSwebsite.Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                _frmSSSwebsite.Button2.Text = "Feedback"
                _frmSSSwebsite.Button1.Image = Image.FromFile(Application.StartupPath & "\images\HOME.png")
                _frmSSSwebsite.Button1.Text = "Home"
                _frmSSSwebsite.Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                _frmSSSwebsite.Button7.Text = "Refresh"
                _frmSSSwebsite.Button5.Enabled = True
                _frmSSSwebsite.Button8.Enabled = True
                _frmSSSwebsite.Button9.Enabled = True
                _frmSSSwebsite.Button6.Enabled = True

                _frmSSSwebsite.Panel3.Controls.Clear()
                _frmSSSwebsite.newTab()
                Me.Hide()
                _frmSSSwebsite.TabControl1.Show()
                _frmSSSwebsite.WebBrowser1.Navigate(urlString)
                _frmSSSwebsite.WebBrowser1.Show()

                tagPage = "13.1"
                _frmSSSwebsite.WebBrowser1.Navigate(sssWebsiteLink)
                getAffectedTable = "10025"
                transDesc = ""
                transNum = ""
                'getAugitLogs()
                clearFeedbackWebsite()
            End If
        End If
    End Sub

    Public Sub pangClear()

        txtEmail.Text = ""

        txtAddress2.Text = ""
        txtName.Text = ""
        txtZipCode.Text = ""
        _frmFeedbackWebsite4.panel001.Text = ""
        'rtbWhat.Text = ""
        'rtbWhy.Text = ""

        lblAdd1.Visible = False
        lblAdd2.Visible = False
        lblEmail.Visible = False
        lblname.Visible = False
        lblAdd2.Visible = False


    End Sub

    Private Sub txtName_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtName.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
                              OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtName.Enter, txtEmail.Enter, txtAddress1.Enter, txtAddress2.Enter, txtCP.Enter, txtZipCode.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress

        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If email.IsMatch(txtEmail.Text.Trim) Then
            'lblError2.Visible = False
        End If

    End Sub

    Private Sub txtZipCode_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtZipCode.KeyPress
        e.Handled = Not (ValidCharsNum.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub
    Public Sub getAugitLogs()
        ' at.getModuleLogs(My.Settings.getID, My.Settings.fullName, "FORM : " & getModule, getTask, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
        ' at.getModuleLogs(xtd.getTempSSS, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)


        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "")
        End Using
    End Sub

    Private Sub _frmFeedbackWebsite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        tagPage = "13.2.1"
        webFeedEmail = readSettings(xml_Filename, xml_path, "webFeedEmail")
        sssWebsiteLink = readSettings(xml_Filename, xml_path, "sssWebsiteLink")


        pnl1.Visible = True
        pnl1.Parent = Panel24
        pnl1.Dock = DockStyle.Fill
        pnl1.Tag = 1

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            Label5.Left = Label5.Left - 40
            Label6.Left = Label6.Left - 40
            Label4.Left = Label4.Left - 90
            Label20.Left = Label20.Left - 90
            Panel9.Left = Panel9.Left - 90
            txtName.Left = txtName.Left - 90
            txtEmail.Left = txtEmail.Left - 90
            lblname.Left = lblname.Left - 90
            lblEmail.Left = lblEmail.Left - 90

            For Each ctrl As Control In Panel5.Controls
                ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)
            Next

            For Each ctrl2 As Control In Panel6.Controls
                ctrl2.Font = New Font(ctrl2.Font.Name, ctrl2.Font.Size - 3, ctrl2.Font.Style)

                Select Case ctrl2.Name
                    Case "Panel3"
                        ctrl2.Top = ctrl2.Top - 20
                    Case "Panel9"
                        ctrl2.Top = ctrl2.Top - 30
                    Case Else
                        ctrl2.Top = ctrl2.Top - 30
                End Select
            Next

            Panel9.Top = Panel9.Top - 30
            For Each ctrl As Control In Panel9.Controls
                ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 3, ctrl.Font.Style)
                If ctrl.Name = "Panel3" Then
                    ctrl.Top = ctrl.Top - 10
                Else
                    ctrl.Top = ctrl.Top - 20
                End If
            Next

            Panel13.Height = Panel13.Height - 20
            Panel19.Height = Panel19.Height - 20

            LabelX1.Font = New Font(LabelX1.Font.Name, LabelX1.Font.Size - 3, LabelX1.Font.Style)
            LabelX1.Width = 650 'LabelX2.Width - 50
        End If
    End Sub


    Private Sub _frmFeedbackWebsite_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        '_frmSSSwebsite.Panel2.Controls.Clear()
        '_frmSSSwebsite.TopLevel = False
        '_frmSSSwebsite.Parent = _frmSSSwebsite.Panel2
        '_frmSSSwebsite.Dock = DockStyle.Fill
        '_frmSSSwebsite.Show()
    End Sub


    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        pnl1.Visible = False
        pnl1.Dock = DockStyle.None
        _frmFeedbackWebsite1.Dispose()
        _frmFeedbackWebsite2.Dispose()
        _frmFeedbackWebsite3.Dispose()
        _frmFeedbackWebsite4.Dispose()
        If pnl1.Tag = 1 Then

        ElseIf pnl1.Tag = 2 Then ' SHOW PANEL 1


            pnl1.Visible = True
            pnl1.Dock = DockStyle.Fill
            pnl1.Parent = Panel24

            pnl1.Tag = 1
        ElseIf pnl1.Tag = 3 Then ' SHOW PANEL 2


            _frmFeedbackWebsite1.TopLevel = False
            _frmFeedbackWebsite1.Parent = Panel24
            _frmFeedbackWebsite1.Dock = DockStyle.Fill
            _frmFeedbackWebsite1.Show()
            pnl1.Tag = 2
        ElseIf pnl1.Tag = 4 Then ' SHOW PANEL 3
            _frmFeedbackWebsite2.TopLevel = False
            _frmFeedbackWebsite2.Parent = Panel24
            _frmFeedbackWebsite2.Dock = DockStyle.Fill
            _frmFeedbackWebsite2.Show()
            pnl1.Tag = 3
        ElseIf pnl1.Tag = 5 Then ' SHOW PANEL 4

            _frmFeedbackWebsite3.TopLevel = False
            _frmFeedbackWebsite3.Parent = Panel24
            _frmFeedbackWebsite3.Dock = DockStyle.Fill
            _frmFeedbackWebsite3.Show()
            pnl1.Tag = 4
        End If
    End Sub

    Private Sub validateEmailFBK(ByVal mail As String)
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
        If email.IsMatch(mail) Then
            rtn = 1
            'lblError2.Visible = False

        Else
            rtn = 0
            'lblError2.Visible = True
            MsgBox("EMAIL ADDRESS IS INVALID.", vbInformation, "Feedback")
        End If

    End Sub


    Private Sub _frmFeedbackWebsite_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub


    Public Sub clearFeedbackWebsite()
        txtName.Clear()
        txtEmail.Clear()
        txtAddress1.Clear()
        txtZipCode.Clear()
        txtAddress2.Clear()
        txtCP.Clear()
        _frmFeedbackWebsite3.rbtUndc.CheckState = False
        _frmFeedbackWebsite3.rbtNo.CheckState = False
        _frmFeedbackWebsite3.rbtYes.CheckState = False
        _frmFeedbackWebsite1.chk1_1.CheckState = False
        _frmFeedbackWebsite1.chk1_2.CheckState = False
        _frmFeedbackWebsite1.chk1_3.CheckState = False
        _frmFeedbackWebsite1.chk1_4.CheckState = False
        _frmFeedbackWebsite1.chk1_5.CheckState = False
        _frmFeedbackWebsite1.chk2_1.CheckState = False
        _frmFeedbackWebsite1.chk2_2.CheckState = False
        _frmFeedbackWebsite1.chk2_3.CheckState = False
        _frmFeedbackWebsite1.chk2_4.CheckState = False
        _frmFeedbackWebsite1.chk2_5.CheckState = False
        _frmFeedbackWebsite1.chk3_1.CheckState = False
        _frmFeedbackWebsite1.chk3_2.CheckState = False
        _frmFeedbackWebsite1.chk3_3.CheckState = False
        _frmFeedbackWebsite1.chk3_4.CheckState = False
        _frmFeedbackWebsite1.chk3_5.CheckState = False
        _frmFeedbackWebsite1.chk4_1.CheckState = False
        _frmFeedbackWebsite1.chk4_2.CheckState = False
        _frmFeedbackWebsite1.chk4_3.CheckState = False
        _frmFeedbackWebsite1.chk4_4.CheckState = False
        _frmFeedbackWebsite1.chk4_5.CheckState = False
        _frmFeedbackWebsite2.chk5_1.CheckState = False
        _frmFeedbackWebsite2.chk5_2.CheckState = False
        _frmFeedbackWebsite2.chk5_3.CheckState = False
        _frmFeedbackWebsite2.chk5_4.CheckState = False
        _frmFeedbackWebsite2.chk5_5.CheckState = False
        _frmFeedbackWebsite2.chk6_1.CheckState = False
        _frmFeedbackWebsite2.chk6_2.CheckState = False
        _frmFeedbackWebsite2.chk6_3.CheckState = False
        _frmFeedbackWebsite2.chk6_4.CheckState = False
        _frmFeedbackWebsite2.chk6_5.CheckState = False
        _frmFeedbackWebsite2.chk7_1.CheckState = False
        _frmFeedbackWebsite2.chk7_2.CheckState = False
        _frmFeedbackWebsite2.chk7_3.CheckState = False
        _frmFeedbackWebsite2.chk7_4.CheckState = False
        _frmFeedbackWebsite2.chk7_5.CheckState = False
        num1 = 0
        num2 = 0
        num3 = 0
        num4 = 0
        num5 = 0
        num6 = 0
        num7 = 0


        _frmFeedbackWebsite3.rtbWhy.Clear()
        _frmFeedbackWebsite3.rtbWhat.Clear()
        _frmFeedbackWebsite4.rtbIf.Clear()
        rbtHomeAddress.CheckState = False
        rbtBusinesAdd.CheckState = False

        ''  _frmFeedbackWebsite3.rbtNo.Checked = True
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        clearFeedbackWebsite()
    End Sub
    Private Sub validateEmail(ByVal mail As String)
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
        'Dim isValid As Boolean
        If email.IsMatch(mail) Then
            ac = 0
        Else
            '  MsgBox("The Email is not Valid", MsgBoxStyle.Information, "Registration")
            'isValid = False
            ac = 1
        End If

    End Sub


    Private Sub txtAddress2_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress2.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
              OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtAddress1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddress1.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
              OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If txtName.Text.Trim = "" Then
            MsgBox("COMPLETE NAME IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtName.Focus()
        ElseIf txtEmail.Text.Trim = "" Then
            MsgBox("EMAIL ADDRESS IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtEmail.Focus()
        ElseIf rbtHomeAddress.Checked = False And rbtBusinesAdd.Checked = False Then
            MsgBox("PLEASE CHOOSE AN ADDRESS TYPE.", MsgBoxStyle.Information, "Information")
            rbtHomeAddress.Focus()
        ElseIf txtAddress1.Text.Trim = "" Then
            MsgBox("ADDRESS 1 IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtAddress1.Focus()
        ElseIf txtAddress2.Text = "" Then
            MsgBox("ADDRESS 2 IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtAddress2.Focus()
        ElseIf txtCP.Text.Trim = "" Then
            MsgBox("CITY/PROVINCE IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtCP.Focus()
        ElseIf txtZipCode.Text.Trim = "" Then
            MsgBox("POSTAL CODE IS REQUIRED.", MsgBoxStyle.Information, "Information")
            txtZipCode.Focus()
        ElseIf Not email.IsMatch(txtEmail.Text.Trim) Then
            'lblError2.Visible = True
            MsgBox("EMAIL ADDRESS IS INVALID.", vbInformation, "Information")
            txtEmail.Focus()
        Else

            _frmSSSwebsite.Panel3.Controls.Clear()
            _frmFeedbackWebsite1.TopLevel = False
            _frmFeedbackWebsite1.Parent = _frmSSSwebsite.Panel3
            _frmFeedbackWebsite1.Dock = DockStyle.Fill
            _frmFeedbackWebsite1.Show()

            tagPage = "13.2.2"
        End If
    End Sub

    Private Sub txtCP_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCP.KeyPress
        e.Handled = Not (ValidCharsCity.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub
End Class