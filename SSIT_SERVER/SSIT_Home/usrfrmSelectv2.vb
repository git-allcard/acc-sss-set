
Imports System.Net.NetworkInformation

Public Class usrfrmSelectv2

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            Label2.Top = Label2.Top + 30

            pbFeedback.Top = pbFeedback.Top + 5
            lblFeedback.Top = lblFeedback.Top + 5
            pbCitizenCharter.Top = pbCitizenCharter.Top + 5
            lblCitizenCharter.Top = lblCitizenCharter.Top + 5
        End If

        '' Add any initialization after the InitializeComponent() call.
        ''CheckForIllegalCrossThreadCalls = False
        '' '' # ADDED 10282014
        'Dim kiosk_Ip As String = xs.getIPAddress()
        'Dim newKioskIP As String = kiosk_Ip.Substring(0, 3)
        ''Dim newKioskIP As String = "10.0.202.96"

        '_frmMainMenu.disposeForms()

        ''vpn
        'If Not newKioskIP = "10." Then
        '    xs.writeSettings(xml_Filename)

        '    If _NotfirstRun = False Then
        '        _NotfirstRun = xs.retrieveFile()
        '    End If

        '    xs.getSQL()
        '    xs.getORACLE()
        'Else
        '    ' File.Delete(xml_Filename)
        '    xs.writeSettings(xml_Filename)
        '    If _NotfirstRun = False Then
        '        _NotfirstRun = xs.retrieveFile()
        '    End If
        '    xs.getSQL()
        '    xs.getORACLE()
        'End If
        ''  MsgBox(kiosk_Ip)

        'db.connect()
        'Dim connstring1 As String = "DSN=" & db_DSN & ";SERVER=" & db_server & ";DATABASE=" & db_Name & ";UID=" & db_UName & ";PWD=" & db_Pass & ""

        'Dim firstRun As String = readSettings(xml_Filename, xml_path, "firstRun")

        'If NetworkInterface.GetIsNetworkAvailable() = False Then
        '    MsgBox("The system is not connected to the network. Please make sure the network cable is connected.", vbInformation, "Information")
        'ElseIf db.webisconnected(connstring1) = False Then
        '    MsgBox("The system is not connected to the network. Please make sure the network cable is connected.", vbInformation, "Information")
        'Else
        '    If firstRun = "3" Then
        '        If Not newKioskIP = "10." Then
        '            Dim ipAdd As String = readSettings(xml_Filename, xml_path, "kioskIP")
        '            xs.getKioskDetails(ipAdd)
        '            getKioskDetails()
        '        Else
        '            xs.getKioskDetails(xs.getIPAddress)
        '            getKioskDetails()
        '        End If
        '    End If


        '    'Dim webConnected As Boolean = checkConnection()
        '    'If webConnected = True Then
        '    editSettings(xml_Filename, xml_path, "sssWebsiteLink", "https://www.sss.gov.ph/")
        'End If

        AddHandler worker.DoWork, AddressOf CheckTerminalConnections_DoWork
        AddHandler worker.RunWorkerCompleted, AddressOf CheckTerminalConnections_Complete
    End Sub

    Private _uf As usrfrmCode
    Private session As System.Threading.Thread
    Dim xs As New MySettings

    Private Sub pic1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim t = New System.Threading.Thread(AddressOf CheckTerminalConnections)
        t.Start()
    End Sub

    Public Shared osk As _frmVirtualKeyboard

    Private Sub pic2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbUmid.Click
        CheckTerminalConnections(1)
    End Sub

    Private Sub pic3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbWebsite.Click
        ''per email by Ms. Farrah on May 19, 2021 re: SET UAT scripts, disable for time being the sss website
        'CheckTerminalConnections(2)

        'SharedFunction.ShowInfoMessage("This service is temporarily unavailable.".ToUpper)
        frmSSSWebsiteMsg.IsProceed = False
        frmSSSWebsiteMsg.ShowDialog()
        If frmSSSWebsiteMsg.IsProceed Then CheckTerminalConnections(2)
    End Sub

    Dim worker As New System.ComponentModel.BackgroundWorker

    Private Sub CheckTerminalConnections(ByVal type As Short)
        'Dim worker As New System.ComponentModel.BackgroundWorker
        'AddHandler worker.DoWork, AddressOf CheckTerminalConnections_DoWork
        'AddHandler worker.RunWorkerCompleted, AddressOf CheckTerminalConnections_Complete
        If Not worker.IsBusy Then worker.RunWorkerAsync(type)
    End Sub

    Private Sub CheckTerminalConnections_DoWork(ByVal sender As System.Object,
                         ByVal e As System.ComponentModel.DoWorkEventArgs)
        Dim stroutput As String = e.Argument.ToString
        'Invoke(New Action(AddressOf _frmMainMenu.ShowLoadingScreenFloat))
        'ConnectToServer.Show()
        Dim response As Short = SharedFunction.CheckTerminalConnections(xs, db)
        e.Result = String.Format("{0}|{1}", e.Argument.ToString, response)
    End Sub

    Private Sub CheckTerminalConnections_Complete(ByVal sender As Object,
                                   ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Dim response As String = e.Result.ToString
        '_frmMainMenu.DisposeForm(_frmLoading)
        If response.Split("|")(1) = "0" Then
            UmidService_URL = db.putSingleValue("select UmidService_URL from tbl_SSS_Webservice")
            UmidService_Token = db.putSingleValue("select UmidService_Token from tbl_SSS_Webservice")

            If response.Split("|")(0) = "1" Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "UMID card option is clicked" & "|" & kioskIP & "|" & getbranchCoDE_1)
                SharedFunction.ShowMainNewUserForm(New usrfrmUMID)
                cardType = "UMIDCARD"
            Else
                SharedFunction.HouseKeeping()
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "Website option is clicked" & "|" & kioskIP & "|" & getbranchCoDE_1)
                SharedFunction.CreateCardData(3, "Website")
                SharedFunction.OpenSSIT_Member("")
            End If

        Else
            SharedFunction.ShowWarningMessage("THE SYSTEM IS NOT CONNECTED TO THE REMOTE SERVER. PLEASE MAKE SURE THE NETWORK CABLE IS CONNECTED.")
        End If
    End Sub

    Private Sub IsCloseUserFormCode()
        If _uf.TextBox1.Text = "111" Then
            'added by edel 01/01/2017
            editSettings(xml_Filename, xml_path, "CARD_DATA", "")
            Application.Exit()
        End If
    End Sub

    Private Sub CloseUserFormCode()
        _uf.Dispose()
        Me.Controls.Remove(_uf)
        'pic1.Visible = True
        Label2.Visible = True
        Me.Refresh()
    End Sub

    Private Sub TextBox1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Microsoft.VisualBasic.ChrW(Keys.Enter) Then
            e.Handled = True
            IsCloseUserFormCode()
        End If
    End Sub

    Private Sub usrfrmSelectv2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GC.Collect()
        _frmMainMenu.ClearCache2()

        ' Add any initialization after the InitializeComponent() call.
        'CheckForIllegalCrossThreadCalls = False
        ' '' # ADDED 10282014
        Dim kiosk_Ip As String = xs.getIPAddress()
        Dim newKioskIP As String = kiosk_Ip.Substring(0, 3)
        'Dim newKioskIP As String = "10.0.202.96"

        _frmMainMenu.disposeForms()
        _frmMainMenu.DisposeForm(_frmSSSwebsite)
        _frmMainMenu.DisposeForm(_frmCitizenCharter3)
        _frmMainMenu.DisposeForm(_frmFeedbackKioskMain)

        'vpn
        If Not newKioskIP = "10." Then
            xs.writeSettings(xml_Filename)

            If _NotfirstRun = False Then
                _NotfirstRun = xs.retrieveFile()
            End If

            xs.getSQL()
            xs.getORACLE()
        Else
            ' File.Delete(xml_Filename)
            xs.writeSettings(xml_Filename)
            If _NotfirstRun = False Then
                _NotfirstRun = xs.retrieveFile()
            End If
            xs.getSQL()
            xs.getORACLE()
        End If
        '  MsgBox(kiosk_Ip

        Dim firstRun As String = readSettings(xml_Filename, xml_path, "firstRun")

        'db.connect()
        'Dim connstring1 As String = "DSN=" & db_DSN & ";SERVER=" & db_server & ";DATABASE=" & db_Name & ";UID=" & db_UName & ";PWD=" & db_Pass & ""

        'If NetworkInterface.GetIsNetworkAvailable() = False Then
        '    MsgBox("The system is not connected to the network. Please make sure the network cable is connected.", vbInformation, "Information")
        'ElseIf db.webisconnected(connstring1) = False Then
        '    MsgBox("The system is not connected to the network. Please make sure the network cable is connected.", vbInformation, "Information")
        'Else
        If firstRun = "3" Then
            'If Not newKioskIP = "10." Then
            '    Dim ipAdd As String = readSettings(xml_Filename, xml_path, "kioskIP")
            '    xs.getKioskDetails(ipAdd)
            '    getKioskDetails()
            'Else
            '    xs.getKioskDetails(xs.getIPAddress)
            '    getKioskDetails()
            'End If

            'added by edel Nov25, 2020
            xs.getKioskDetails(xs.getIPAddress)
            'getKioskDetails()
        End If
        'Console.Write(kioskID)
        editSettings(xml_Filename, xml_path, "sssWebsiteLink", "https://www.sss.gov.ph/")
        editSettings(xml_Filename, xml_path, "CRN", "")

        UMID_settings = readSettings(xml_Filename, xml_path, "UMID")
        SAM_settings = readSettings(xml_Filename, xml_path, "SAM")
        PIN_settings = readSettings(xml_Filename, xml_path, "PIN")
        CardPIN = readSettings(xml_Filename, xml_path, "CardPin")

        SharedFunction.HouseKeeping()
        session = New System.Threading.Thread(AddressOf SessionTimer)
        session.IsBackground = True
        session.Start()

        'End If
    End Sub

#Region " Defaults "

    Private bln As Boolean = True
    Private intCntr As Short = (60 * 15) '20
    Private Delegate Sub Action()

    Private Sub SessionTimer()
        While bln
            If intCntr > 0 Then
                intCntr -= 1
                System.Threading.Thread.Sleep(1000)
            Else
                bln = False
                session = Nothing

                CloseUserForm()
            End If
        End While
    End Sub

    Private Sub CloseUserForm()
        Invoke(New Action(AddressOf ShowIdleForm))
    End Sub

    Private Sub ShowIdleForm()
        SharedFunction.ShowMainNewUserForm(New usrfrmIdle)
    End Sub

#End Region

    Private Sub pbCode_DoubleClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCode.DoubleClick
        'pic1.Visible = False
        _uf = New usrfrmCode
        _uf.Location = New Point(pbCode.Width, pbCode.Location.Y - _uf.Size.Height)
        _uf.BringToFront()
        AddHandler _uf.TextBox1.KeyPress, AddressOf TextBox1_KeyPress
        AddHandler _uf.cmdSubmit.Click, AddressOf IsCloseUserFormCode
        AddHandler _uf.cmdClosePanel.Click, AddressOf CloseUserFormCode
        Me.Controls.Add(_uf)
        pbUmid.SendToBack()
        Label2.Visible = False
    End Sub

    'Private Sub Test()
    '    Dim DAL As New DAL_Oracle
    '    If DAL.checkIfValid("011101960242") Then
    '        MessageBox.Show("Success. " & DAL.ObjectResult.ToString)
    '    Else
    '        MessageBox.Show("Failed")
    '    End If
    'End Sub

    Private Sub RunProgram(ByVal App As String)
        Try
            Dim _Process As New Process
            _Process.StartInfo.FileName = App
            '_Process.
            _Process.Start()
        Catch ex As Exception
            'SaveToErrorLog(TimeStamp() & "RunProgram(): Runtime catched error " & ex.Message)
        End Try
    End Sub

    Private Sub KillProgram(ByVal Program As String, Optional ByVal blnLog As Boolean = True)
        Try
            Dim programs() As Process = Process.GetProcessesByName(Program.Replace(".exe", "").Replace(".EXE", ""))
            For Each _program As Process In programs
                _program.Kill()
            Next

            'If blnLog Then SaveToLog(TimeStamp() & Program & " is killed by " & App)
        Catch ex As Exception
            'SaveToErrorLog(TimeStamp() & "KillProgram(): Runtime catched error " & ex.Message)
        End Try
    End Sub

    Private Sub PictureBox1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbFeedback.Click, lblFeedback.Click
        SharedFunction.SaveToLogv2("Kiosk feedback is clicked")
        SharedFunction.CreateCardData(4, "KioskFeedback")
        SharedFunction.OpenSSIT_Member("")
    End Sub

    Private Sub PictureBox2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        SharedFunction.SaveToLogv2("Website feedback is clicked")
        SharedFunction.CreateCardData(5, "WebsiteFeedback")
        SharedFunction.OpenSSIT_Member("")
    End Sub

    Private Sub PictureBox3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCitizenCharter.Click, lblCitizenCharter.Click
        SharedFunction.SaveToLogv2("Citizen charter is clicked")
        SharedFunction.CreateCardData(6, "Citizen charter")

        GetDriver()

        Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine("|" & "10042" & "||" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "||||" & vbNewLine)
        End Using

        SharedFunction.OpenSSIT_Member("")
    End Sub

    Public SaveToDrive As String
    Public Path1 As String
    Dim db As New ConnectionString

    Private Function GetDriver()
        Dim comboBox1 As New ComboBox
        For Each drive In Environment.GetLogicalDrives
            Dim Driver As IO.DriveInfo = New IO.DriveInfo(drive)
            If Driver.DriveType = IO.DriveType.Removable Or Driver.DriveType = IO.DriveType.Fixed Then
                comboBox1.Items.Add(drive)
            End If
        Next
        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")

        SaveToDrive = comboBox1.Items(comboBox1.Items.Count - 1)
        Path1 = (SaveToDrive & "SSIT\logs\" & getdate & " " & getbranchCoDE)
        If (Not System.IO.Directory.Exists(Path1)) Then
            System.IO.Directory.CreateDirectory(Path1)
        End If
    End Function


    Private Sub pbCode_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles pbCode.Click

    End Sub

    Dim topLoc As Integer = 0

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        SSStempFile = TextBox1.Text
        BankWorkflowWebService_URL = "http://m41sv145:7009/testbemws/BankWorkflowWebServiceImplPort?WSDL"
        BankWorkflowWebService_Token = "BhIUlWLlFxbIBoLQPeK0Qrh1vuHKocNGC8itwbm1yBlMsrSpPn"
        TextBox2.Text = _frmMainMenu.getBankAccountListBySSNumber()
        Return
        'Dim ip As New insertProcedure
        'ip.insertSSEXITSURVEY("1234567890", True, 2, 2)
        'Return


        'Dim umidService As New SSUmidService
        ''Dim dob As String = String.Format("{0}/{1}/{2}", umidData.dateOfBirth.Substring(4, 2), umidData.dateOfBirth.Substring(6, 2), umidData.dateOfBirth.Substring(0, 4))
        'If umidService.isSSSMember(TextBox1.Text, TextBox2.Text, TextBox3.Text, TextBox4.Text, TextBox5.Text, "") Then

        'End If

        'Return
        ''TextBox3.Text = SharedFunction.GetCurrentAge(TextBox1.Text)

        'Dim c As New usrFrmMenuButton
        ''c = Panel2
        'c.Top = topLoc
        '''c = Panel2
        'c.Dock = DockStyle.Fill
        ''c.Parent = Panel1
        'Panel1.Controls.Add(c)
        ''topLoc += c.Height
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub PictureBox1_Click_1(sender As Object, e As EventArgs)

    End Sub
End Class
