
Public Class _frmKioskSettings
    Public db As New ConnectionString
    Dim orcl As New ConnectionString2
    Dim kr As New kioskRegistration
    Dim pathTagFile As Integer = 0
    Dim pathTagCard As Integer = 0
    Dim tempBranch, tempCluster, tempGroup As String
    Dim firstRun As String

    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        firstRun = readSettings(xml_Filename, xml_path, "firstRun")


        TextBox1.Focus()
        If firstRun = "1" Then

            pnlDashboard.Tag = "SQL"
            pnlKiosk.Visible = False
            btnSave.Visible = False
            pnlOracle.Visible = False
            pnlSQL.Parent = pnlDashboard
            pnlSQL.Visible = True
            pnlSQL.Dock = DockStyle.Fill

            TextBox2.Text = readSettings(xml_Filename, xml_path, "db_Server") 'My.Settings.db_Server 
            TextBox3.Text = readSettings(xml_Filename, xml_path, "db_UName") ' My.Settings.db_UName
            TextBox4.Text = readSettings(xml_Filename, xml_path, "db_Pass") 'My.Settings.db_Pass
            TextBox5.Text = readSettings(xml_Filename, xml_path, "db_Name") ' My.Settings.db_Name
            TextBox1.Text = readSettings(xml_Filename, xml_path, "db_DSN") ' My.Settings.db_DSN

            txtUserID.Text = readSettings(xml_Filename, xml_path, "db_UserID") 'My.Settings.db_UserID
            txtPassword.Text = readSettings(xml_Filename, xml_path, "db_Password") 'My.Settings.db_Password


            ' lblconnection.Text = "*PLEASE SET DATABASE CONNECTION FIRST!"
        ElseIf firstRun = "2" Then


            pnlOracle.Visible = False
            pnlSQL.Visible = False
            pnlSQL.Dock = DockStyle.None
            pnlOracle.Dock = DockStyle.None
            pnlKiosk.Dock = DockStyle.Fill
            pnlKiosk.Visible = True
            pnlKiosk.Parent = pnlDashboard
            btnNext.Visible = False
            pnlDashboard.Tag = "KIOSK"

            txtKiosk.Enabled = True
            btnViewMerchant.Enabled = True
            pnlKiosk.Enabled = True
            '   lblconnection.Visible = False
            'Panel3.Enabled = True
            'Panel2.Enabled = True

            TextBox2.Text = readSettings(xml_Filename, xml_path, "db_Server") 'My.Settings.db_Server 
            TextBox3.Text = readSettings(xml_Filename, xml_path, "db_UName") ' My.Settings.db_UName
            TextBox4.Text = readSettings(xml_Filename, xml_path, "db_Pass") 'My.Settings.db_Pass
            TextBox5.Text = readSettings(xml_Filename, xml_path, "db_Name") ' My.Settings.db_Name
            TextBox1.Text = readSettings(xml_Filename, xml_path, "db_DSN") ' My.Settings.db_DSN

            txtUserID.Text = readSettings(xml_Filename, xml_path, "db_UserID") 'My.Settings.db_UserID
            txtPassword.Text = readSettings(xml_Filename, xml_path, "db_Password") 'My.Settings.db_Password
        ElseIf firstRun = "" Or firstRun = "0" Then

            'Panel2.Enabled = True
            'Panel3.Enabled = True
            pnlSQL.Enabled = True
            pnlDashboard.Tag = "SQL"
            pnlKiosk.Visible = False
            btnSave.Visible = False
            pnlOracle.Visible = False
            pnlSQL.Parent = pnlDashboard
            pnlSQL.Visible = True
            pnlSQL.Dock = DockStyle.Fill
            '  lblconnection.Visible = True
            '  lblconnection.Text = "*PLEASE SET DATABASE CONNECTION FIRST!"
        ElseIf firstRun = "3" Then
            btnNext.Enabled = False

            'Panel3.Enabled = True
            'Panel2.Enabled = True
            '   lblconnection.Visible = False
            TextBox2.Text = readSettings(xml_Filename, xml_path, "db_Server") 'My.Settings.db_Server 
            TextBox3.Text = readSettings(xml_Filename, xml_path, "db_UName") ' My.Settings.db_UName
            TextBox4.Text = readSettings(xml_Filename, xml_path, "db_Pass") 'My.Settings.db_Pass
            TextBox5.Text = readSettings(xml_Filename, xml_path, "db_Name") ' My.Settings.db_Name
            TextBox1.Text = readSettings(xml_Filename, xml_path, "db_DSN") ' My.Settings.db_DSN

            txtUserID.Text = readSettings(xml_Filename, xml_path, "db_UserID") 'My.Settings.db_UserID
            txtPassword.Text = readSettings(xml_Filename, xml_path, "db_Password") 'My.Settings.db_Password

            txtIP.Text = readSettings(xml_Filename, xml_path, "kioskIP") 'My.Settings.kioskIP
            txtID.Text = readSettings(xml_Filename, xml_path, "kioskID") 'My.Settings.kioskID
            txtKiosk.Text = readSettings(xml_Filename, xml_path, "kioskName") 'My.Settings.kioskName
            txtBranch.Text = readSettings(xml_Filename, xml_path, "kioskBranch") ' My.Settings.kioskBranch
            txtCluser.Text = readSettings(xml_Filename, xml_path, "kiosk_cluster") ' My.Settings.kiosk_cluster
            txtGroup.Text = readSettings(xml_Filename, xml_path, "kiosk_group") ' My.Settings.kiosk_group
            txtprinter.Text = readSettings(xml_Filename, xml_path, "printerPort") 'My.Settings.printerPort

            pnlSQL.Enabled = True
            pnlDashboard.Tag = "KIOSK"
            pnlKiosk.Visible = True
            btnSave.Visible = True
            pnlOracle.Visible = False
            pnlKiosk.Parent = pnlDashboard
            pnlSQL.Visible = True
            pnlKiosk.Dock = DockStyle.Fill
            btnNext.Visible = False
            ' lblconnection.Visible = True
            ' lblconnection.Text = "*Kiosk settings are already configured!"
        End If





    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim temp As String = "DSN=" & TextBox1.Text & ";SERVER=" & TextBox2.Text & ";DATABASE=" & TextBox5.Text & ";UID=" & TextBox3.Text & ";PWD=" & TextBox4.Text & ""
        'Dim temp As String = "SERVER=" & TextBox2.Text & ";DATABASE=" & TextBox5.Text & ";UID=" & TextBox3.Text & ";PWD=" & TextBox4.Text & ""
        ''Dim temp As String = "Data Source=" & TextBox2.Text & ";Initial Catalog=" & TextBox5.Text & ";User ID=" & TextBox3.Text & ";Password=" & TextBox4.Text & ""



        If db.webisconnected(temp) Then
            MsgBox("Parameters are correct" & vbNewLine & "You are now connected to server", MsgBoxStyle.Information, "Information")
        Else
            MsgBox("Unable to connect!" & vbNewLine & "Please check if all the parameters are correct", MsgBoxStyle.Exclamation, "Information")
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If pnlDashboard.Tag = "SQL" Then
            If MsgBox("Save Settings?", MsgBoxStyle.YesNo) = MsgBoxResult.Yes Then
                If TextBox2.Text = Nothing Then
                    TextBox2.Focus()
                ElseIf TextBox3.Text = Nothing Then
                    TextBox3.Focus()

                ElseIf TextBox4.Text = Nothing Then
                    TextBox4.Focus()
                End If
                Dim temp As String = "DSN=" & TextBox1.Text & ";SERVER=" & TextBox2.Text & ";DATABASE=" & TextBox5.Text & ";UID=" & TextBox3.Text & ";PWD=" & TextBox4.Text & ""
                If db.webisconnected(temp) = True Then

                    editSettings(xml_Filename, xml_path, "db_Server", TextBox2.Text)
                    editSettings(xml_Filename, xml_path, "db_UName", TextBox3.Text)
                    editSettings(xml_Filename, xml_path, "db_Pass", TextBox4.Text)
                    editSettings(xml_Filename, xml_path, "db_Name", TextBox5.Text)
                    editSettings(xml_Filename, xml_path, "db_DSN", TextBox1.Text)
                    editSettings(xml_Filename, xml_path, "firstRun", "1")
                    'My.Settings.db_Server = TextBox2.Text
                    'My.Settings.db_UName = TextBox3.Text
                    'My.Settings.db_Pass = TextBox4.Text
                    'My.Settings.db_Name = TextBox5.Text
                    'My.Settings.db_DSN = TextBox1.Text
                    'firstRun = 1
                    'My.Settings.Save()
                    'My.Settings.Reload()


                    ' COMMENT THE MESSAGE BOX AND APPLICATION EXIT
                    'MsgBox("New paramenters has been saved.", MsgBoxStyle.Information)
                    'MsgBox("Please restart the system to refresh your settings, it will be automatically close the system", MsgBoxStyle.Information)

                    'Application.Exit
                    ' Me.Close()
                    '*****

                    btnNext.Visible = True
                    pnlDashboard.Tag = "ORACLE"
                    pnlSQL.Visible = False
                    pnlSQL.Dock = DockStyle.None
                    pnlOracle.Parent = pnlDashboard
                    pnlOracle.Visible = True
                    pnlOracle.Dock = DockStyle.Fill
                Else
                    MsgBox("Unable to save!" & vbNewLine & "Please check if all the parameters are correct", MsgBoxStyle.Exclamation, "Information")
                End If
            Else
            End If

        ElseIf pnlDashboard.Tag = "ORACLE" Then ' IF ORACLE
            If MsgBox("Save Settings?", MsgBoxStyle.YesNo, "Information") = MsgBoxResult.Yes Then
                Try
                    If txtHost.Text = Nothing Then
                        txtHost.Focus()
                    End If

                    If txtPort.Text = Nothing Then
                        txtPort.Focus()
                    End If

                    If txtServiceName.Text = Nothing Then
                        txtServiceName.Focus()
                    End If

                    If txtUserID.Text = Nothing Then
                        txtUserID.Focus()
                    End If

                    If txtPassword.Text = Nothing Then
                        txtPassword.Focus()
                    End If

                    '   pnlKiosk.Show()

                    Dim temp As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & txtHost.Text & ")(PORT=" & txtPort.Text & "))(CONNECT_DATA=(SERVICE_NAME=" & txtServiceName.Text & ")));User Id=" & txtUserID.Text & ";Password=" & txtPassword.Text & ";"
                    If orcl.webisconnected(temp) = True Then
                        editSettings(xml_Filename, xml_path, "db_Host", txtHost.Text)
                        editSettings(xml_Filename, xml_path, "db_Port", txtPort.Text)
                        editSettings(xml_Filename, xml_path, "db_ServiceName", txtServiceName.Text)
                        editSettings(xml_Filename, xml_path, "db_UserID", txtUserID.Text)
                        editSettings(xml_Filename, xml_path, "db_Password", txtPassword.Text)
                        editSettings(xml_Filename, xml_path, "firstRun", "2")

                        'My.Settings.db_Host = txtHost.Text
                        'My.Settings.db_Port = txtPort.Text
                        'My.Settings.db_ServiceName = txtServiceName.Text
                        'My.Settings.db_UserID = txtUserID.Text
                        'My.Settings.db_Password = txtPassword.Text
                        'firstRun = 2
                        'My.Settings.Save()
                        MsgBox("New paramenters has been saved.", MsgBoxStyle.Information, "Information")
                        MsgBox("System will now exit to refresh your settings", MsgBoxStyle.Information, "Information")
                        Application.Exit()

                    Else
                        MsgBox("Unable to save!" & vbNewLine & "Please check if all the parameters are correct", MsgBoxStyle.Exclamation, "Information")
                    End If

                Catch ex As Exception
                    MsgBox(ex.ToString, MsgBoxStyle.Exclamation, "Information")
                End Try
            End If



        End If
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim temp As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & txtHost.Text & ")(PORT=" & txtPort.Text & "))(CONNECT_DATA=(SERVICE_NAME=" & txtServiceName.Text & ")));User Id=" & txtUserID.Text & ";Password=" & txtPassword.Text & ";"


        If orcl.webisconnected(temp) Then
            MsgBox("Parameters are correct" & vbNewLine & "You are now connected to server", MsgBoxStyle.Information, "Information")
        Else
            MsgBox("Unable to connect!" & vbNewLine & "Please check if all the parameters are correct", MsgBoxStyle.Exclamation, "Information")
        End If
    End Sub

    Private Sub pnlOracle_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles pnlOracle.Paint

    End Sub

    Private Sub btnViewMerchant_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnViewMerchant.Click
        Try

            txtID.Text = db.putSingleValue("select KIOSK_ID FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
            txtIP.Text = db.putSingleValue("select BRANCH_IP FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
            tempBranch = db.putSingleValue("select BRANCH_CD FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
            txtBranch.Text = db.putSingleValue("select BRANCH_NM from SSINFOTERMBR where BRANCH_CD = '" & tempBranch & "'")
            tempCluster = db.putSingleValue("select clstr FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
            txtCluser.Text = db.putSingleValue("select CLSTR_NM from SSINFOTERMCLSTR where CLSTR_CD = '" & tempCluster & "'")
            tempGroup = db.putSingleValue("select DIVSN FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
            txtGroup.Text = db.putSingleValue("select GROUP_NM from SSINFOTERMGROUP where GROUP_CD = '" & tempGroup & "'")

        Catch ex As Exception

        End Try
    End Sub


    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click
        Try


            Dim checkExist As String = db.putSingleValue("select TAG from SSINFOTERMKIOSK where KIOSK_NM = '" & txtKiosk.Text & "'")
            If txtIP.Text = Nothing Then
                txtIP.Focus()

            ElseIf txtID.Text = "" Then
                txtID.Focus()

            ElseIf txtID.Text = Nothing Then
                txtID.Focus()

            ElseIf txtID.Text = "" Then
                txtID.Focus()

            ElseIf txtKiosk.Text = Nothing Then
                txtKiosk.Focus()

            ElseIf txtKiosk.Text = "" Then
                txtKiosk.Focus()

            ElseIf txtBranch.Text = Nothing Then
                txtBranch.Focus()

            ElseIf txtBranch.Text = "" Then
                txtBranch.Focus()

            ElseIf txtCluser.Text = Nothing Then
                txtCluser.Focus()

            ElseIf txtCluser.Text = "" Then
                txtCluser.Focus()

            ElseIf txtGroup.Text = Nothing Then
                txtGroup.Focus()

            ElseIf txtGroup.Text = "" Then
                txtGroup.Focus()

            ElseIf checkExist = 1 Then
                MsgBox("Kiosk is already been used! ", MsgBoxStyle.Information, "Information")


                'If db.checkExistence("select KIOSK_ID,BRANCH_CD,CLSTR,DIVSN from SSINFOTERMKIOSK where KIOSK_ID ='" & txtID.Text & "' or BRANCH_CD = '" & txtKiosk.Text & "' or CLSTR = '" & txtCluser.Text & "' or DIVSN = '" & txtGroup.Text & "'") = True Then
                '    MsgBox("Kiosk Already exist", MsgBoxStyle.Information)
                'Else
            Else
                editSettings(xml_Filename, xml_path, "kioskIP", txtIP.Text)
                editSettings(xml_Filename, xml_path, "kioskID", txtID.Text)
                editSettings(xml_Filename, xml_path, "kioskName", txtKiosk.Text)
                editSettings(xml_Filename, xml_path, "kioskBranch", txtBranch.Text)
                editSettings(xml_Filename, xml_path, "kiosk_cluster", txtCluser.Text)
                editSettings(xml_Filename, xml_path, "kiosk_group", txtGroup.Text)
                editSettings(xml_Filename, xml_path, "printerPort", txtprinter.Text)
                editSettings(xml_Filename, xml_path, "firstRun", "3")
                'My.Settings.kioskIP = txtIP.Text
                'My.Settings.kioskID = txtID.Text
                'My.Settings.kioskName = txtKiosk.Text
                'My.Settings.kioskBranch = txtBranch.Text
                'My.Settings.kiosk_cluster = txtCluser.Text
                'My.Settings.kiosk_group = txtGroup.Text
                'My.Settings.printerPort = txtprinter.Text
                'firstRun = 3
                'My.Settings.Save()
                'My.Settings.Reload()

                Dim getBranch As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & txtBranch.Text & "'")
                Dim getCluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & txtCluser.Text & "'")
                Dim getGroup As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & txtGroup.Text & "'")
                Dim todayDate As Date = DateTime.Today.ToShortDateString & "  " & TimeOfDay

                'db.sql = "insert into SSINFOTERMKIOSK (KIOSK_ID,BRANCH_CD,KIOSK_NM,BRANCH_IP,STATUS,ENCODE_DT,CLSTR,DIVSN) values('" & txtID.Text & "','" & getBranch & "','" & txtKiosk.Text & "','" & txtIP.Text & _
                '    "','" & "0" & "','" & todayDate & "','" & getCluster & "', '" & getGroup & "')"
                'db.ExecuteSQLQuery(db.sql)

                db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set TAG = '" & 1 & "' where KIOSK_NM ='" & txtKiosk.Text & "'")

                MsgBox("Kiosk details has been saved, Please close the kiosk settings form! ", MsgBoxStyle.Information, "Information")
                _frmHomeScreen.lblStatus.Visible = False
                Application.Exit()
            End If
        Catch ex As Exception
            MsgBox("Settings connection error! ", MsgBoxStyle.Information, "Information")
        End Try
    End Sub

    Private Sub txtKiosk_DropDown(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKiosk.DropDown
        db.fillComboBox(db.ExecuteSQLQuery("select KIOSK_NM FROM SSINFOTERMKIOSK"), txtKiosk)
    End Sub
    Private Sub txtKiosk_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtKiosk.TextChanged
        'Try

        '    txtID.Text = db.putSingleValue("select KIOSK_ID FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
        '    txtIP.Text = db.putSingleValue("select BRANCH_IP FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
        '    tempBranch = db.putSingleValue("select BRANCH_CD FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
        '    txtBranch.Text = db.putSingleValue("select BRANCH_NM from SSINFOTERMBR where BRANCH_CD = '" & tempBranch & "'")
        '    tempCluster = db.putSingleValue("select clstr FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
        '    txtCluser.Text = db.putSingleValue("select CLSTR_NM from SSINFOTERMCLSTR where CLSTR_CD = '" & tempCluster & "'")
        '    tempGroup = db.putSingleValue("select DIVSN FROM SSINFOTERMKIOSK WHERE KIOSK_NM = '" & txtKiosk.Text & "' ")
        '    txtGroup.Text = db.putSingleValue("select GROUP_NM from SSINFOTERMGROUP where GROUP_CD = '" & tempGroup & "'")

        'Catch ex As Exception

        'End Try
    End Sub


    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim FolderBrowserDialog1 As New FolderBrowserDialog
        If FolderBrowserDialog1.ShowDialog = DialogResult.OK Then
            txtprinter.Text = FolderBrowserDialog1.SelectedPath

        End If
    End Sub
End Class