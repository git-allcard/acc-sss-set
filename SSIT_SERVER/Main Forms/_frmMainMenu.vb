
Imports System.Text
Imports Oracle.DataAccess.Client
Imports System.Threading
Imports System.IO

Public Class _frmMainMenu
    Implements IMessageFilter

    Dim xs As New MySettings
    Public tagLoanbtn As Integer = 0
    Public getForm As Form
    Public tagnameId, techTag As Integer

    Dim db As New ConnectionString
    Dim at As New auditTrail
    Dim class1 As New PrintHelper
    Dim getModule As String
    Dim getTask As String
    Dim getAffectedTable As String
    Dim getDetailstask As String
    Dim ac As String
    Dim oi As String = "Online Inquiry"
    Dim printZero As Integer = 0
    Dim inMat As New insertProcedure
    'Public empNumber, empName As String
    Dim txn As New txnNo
    Dim xtd As New ExtractedDetails
    Dim tempSSnum As String
    Public tagPath As Integer
    Public empNumber, getEmpSalID, getEmpSalName As String
    Dim sp As New SMTP
    Public trd As Thread
    Public trd1 As Thread
    Public getFname, getSSSct, tempSalId, tempSalName, salPageTag As String
    Dim prt As New printModule
    Dim rtn As Integer
    Public getACCNTNO As String
    Public CTRno As String
    Dim procCodeLen1, procCodeLen2, procCodeLen3 As String
    Public addressPen1, addresssPen2, sssPen, refPen As String
    Public depAcop, sssACOP, refACOP, rcodeAcop As String
    Dim acopStr, acopStr2 As String
    Dim refnoAcop, ssnumAcop, messageAcop, emptyAcop As String
    Dim timeOut As Integer ' = db.putSingleNumber("select idle_time from SSINFOTERMKIOSK where kiosk_nm ='" & kioskName & "'")
    Dim vpnTimeOut As Boolean
    Public techretdate As String
    Public SaveToDrive As String
    Public Path1 As String
    Public isLoading As Integer
    Public IsMainMenuActive As Boolean
    Public prtRes As Integer
    Public getBankCode As String
    Public memberFirstName, memberLastName, memberMidName, MemdateOfBirth, memSSNUM As String
    Public ifFirstLoad As Integer
    Public techRetTransNum As String

    Public Shared osk As _frmVirtualKeyboard

    'Public contDT As String
    ' Public ifLogOut As Boolean = False
#Region "Funcation and Declaration"
    Dim printF As New printModule

    Function Fileexists(ByVal fname) As Boolean
        Try
            If Dir(fname) <> "" Then _
        Fileexists = True _
        Else Fileexists = False
        Catch ex As Exception

        End Try
    End Function

    Sub DeleteFile(ByVal FileToDelete As String)
        Try
            If Fileexists(FileToDelete & "\*.*") Then 'See above
                SetAttr(FileToDelete, vbNormal)
                Kill(FileToDelete & "\*.*")
            End If
        Catch ex As Exception

        End Try
    End Sub

#End Region

#Region "Printer Settings"
    Public Shared Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try
    End Function
#End Region

#Region "TIMER"

    Dim End_Of_Timer As Integer = 120
    Dim SecondsCount As Integer
    'Dim idlTimer As New idleTimer

    Public Sub New()
        InitializeComponent()

        Try
            'added by edel to check if mysettings.xml is valid. if mysettings.xml is empty, system will get the backup copy
            '01/18/2017

            Dim pathA As String = Application.StartupPath & "\MySettings.xml" '  destination file name
            Dim pathB As String = Application.StartupPath & "\Backup\MySettings.xml"  '  source filename            
            Dim content As String = File.ReadAllText(pathA)
            If Not File.Exists(pathA) Then
                File.Copy(pathB, pathA, True)
            Else
                If content.Trim = "" Then File.Copy(pathB, pathA, True)
            End If
        Catch ex As Exception
        End Try

        'getCurrVersionIE()
        AddRemoveIEVersion(True)

        'added by edel to reset flags every open 01/18/2017
        editSettings(xml_Filename, xml_path, "CARD_DATA", "")
        editSettings(xml_Filename, xml_path, "errorLoadTag", "0")
        editSettings(xml_Filename, xml_path, "CRN", "")

        'AddHandler worker.DoWork, AddressOf CheckTerminalConnections_DoWork
        'AddHandler worker.RunWorkerCompleted, AddressOf CheckTerminalConnections_Complete
        'CheckTerminalConnections()

        MenuButtonsFontSize()

        Application.AddMessageFilter(Me)
        'myTimer.Enabled = True
    End Sub

    Public Function PreFilterMessage(ByRef m As System.Windows.Forms.Message) As Boolean Implements System.Windows.Forms.IMessageFilter.PreFilterMessage
        Dim mouse As Boolean = (m.Msg >= &H200 And m.Msg <= &H20D) Or (m.Msg >= &HA0 And m.Msg <= &HAD)
        Dim keyboard As Boolean = (m.Msg >= &H100 And m.Msg <= &H109)

        If mouse Or keyboard Then
            myTimer.Enabled = False
            SecondsCount = 0
            'myTimer.Enabled = True
        End If
        Return False
    End Function

    Private Sub myTimer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles myTimer.Tick
        Try
            timeOut = db.putSingleNumber("select idle_time from SSINFOTERMKIOSK where kiosk_nm ='" & kioskName & "'")
            If IsMainMenuActive Then
                vpnTimeOut = False

                vpnTimeOut = db.putSingleValue("SELECT isVPN FROM SSINFOTERMKIOSK WHERE BRANCH_IP ='" & kioskIP & "'")
                If vpnTimeOut = True Then
                    isVPN = True
                    SecondsCount = 0
                Else
                    isVPN = False
                    SecondsCount += 1
                End If

                If SecondsCount > timeOut Then
                    myTimer.Enabled = False

                    tagPage = "12"
                    Dim pathX1 As String = Application.StartupPath & "\" & "temp" & "\"
                    DeleteFile(pathX1)

                    MsgBox("YOUR SESSION HAS EXPIRED.", MsgBoxStyle.Information, "Information")

                    If Not trd Is Nothing Then If trd.IsAlive Then trd.Abort()

                    printTag = 0
                    Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                    Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                    Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")

                    Dim getLastofflinedate As DateTime = Date.Today.ToShortDateString
                    Dim getlastofflinetime As DateTime = TimeOfDay
                    Dim finaldate As DateTime
                    finaldate = getLastofflinedate & " " & getlastofflinetime
                    lastOffline = finaldate
                    editSettings(xml_Filename, xml_path, "lastOffline", finaldate)

                    IsMainMenuActive = False

                    Me.Hide()
                    SharedFunction.ShowMainDefaultUserForm()
                    Main.Show()

                End If
            End If
        Catch ex As Exception
            SharedFunction.SaveToErrorLog("myTimer_Tick(): Runtime error catched " & ex.Message)
        End Try

    End Sub
#End Region

    Private Function GetDriver() As Boolean
        Dim logicalDrive As New List(Of String)
        Dim comboBox1 As New ComboBox
        For Each drive In Environment.GetLogicalDrives
            Dim Driver As DriveInfo = New DriveInfo(drive)
            If Driver.DriveType = DriveType.Fixed Then logicalDrive.Add(drive)
        Next

        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
        kioskBranchCD = getbranchCoDE

        Dim isSettingDriveExist As Boolean = False

        For Each lDrive In logicalDrive
            SaveToDrive = lDrive
            If Directory.Exists(SaveToDrive & "SSIT\Settings") Then
                isSettingDriveExist = True
                Path1 = (SaveToDrive & "SSIT\logs\" & getdate & " " & getbranchCoDE)
                If (Not System.IO.Directory.Exists(Path1)) Then System.IO.Directory.CreateDirectory(Path1)
            End If
        Next

        If Not isSettingDriveExist Then SharedFunction.ShowWarningMessage("UNABLE TO FIND REQUIRED 'SSIT\Settings' DIRECTORY TO ANY LOGICAL DRIVE(S)")

        Return isSettingDriveExist
    End Function

    'Private Function GetDriver_bak()
    '    Dim comboBox1 As New ComboBox
    '    For Each drive In Environment.GetLogicalDrives
    '        Dim Driver As DriveInfo = New DriveInfo(drive)
    '        If Driver.DriveType = DriveType.Removable Or Driver.DriveType = DriveType.Fixed Then
    '            comboBox1.Items.Add(drive)
    '        End If
    '    Next
    '    Dim getdate As String = Date.Today.ToString("ddMMyyyy")
    '    Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
    '    kioskBranchCD = getbranchCoDE

    '    SaveToDrive = comboBox1.Items(comboBox1.Items.Count - 1)
    '    Path1 = (SaveToDrive & "SSIT\logs\" & getdate & " " & getbranchCoDE)
    '    If (Not System.IO.Directory.Exists(Path1)) Then System.IO.Directory.CreateDirectory(Path1)
    'End Function

    Public Sub clearFile()
        Try
            Dim txt1 As String
            Dim txt2
            Dim getTxtDate As String


            'Dim getTransNo As String
            Using SW As New IO.StreamReader(Application.StartupPath & "\" & "REF_NUM\" & "\" & "REF_NUM.txt", True)
                txt1 = SW.ReadToEnd
            End Using

            If txt1 = "" Or txt1 = Nothing Then
                ' getTxtDate = Nothing
            Else
                Dim date1, dtoday As Date
                txt2 = txt1.Split(New Char() {"|"c})
                getTxtDate = txt2(1)
                getTxtDate = getTxtDate.Trim
                date1 = getTxtDate
                dtoday = Date.Today

                If dtoday > date1 Then
                    Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "Ref_Num\" & "\" & "REF_NUM.txt", False)
                        SW.WriteLine("")
                    End Using
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub

    Public Sub navMain()
        getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
        _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)
    End Sub

    Public Sub MainLoad()
        Try

            Me.IsMainMenuActive = True

            'xs.writeSettings(xml_Filename)
            'xs.getSQL()

            'xs.getORACLE()

            getKioskDetails()

            'vpnTimeOut = db.putSingleValue("SELECT isVPN FROM SSINFOTERMKIOSK WHERE BRANCH_IP ='" & kioskIP & "'")

            'isVPN = False

            'If vpnTimeOut = True Then
            '    Dim ipAdd As String = readSettings(xml_Filename, xml_path, "kioskIP")
            '    xs.getKioskDetails(ipAdd)
            'Else
            '    xs.getKioskDetails(xs.getIPAddress)
            'End If

            'getKioskDetails()

            '' ''   ****************************** PLEASE COMMENT THE ABOVE CODES TO BYPASS  ****************************** 

            ' ''Dim isCon As Boolean = db.isconnected
            ' '' If isCon = True Then

            clearFile()
            '_frmCitizenCharter3.Close()
            DisposeForm(_frmCitizenCharter3)
            WarningBox1.Visible = False
            WarningBox2.Visible = False
            WarningBox3.Visible = False
            'Timer1.Start()
            GC.Collect()
            Control.CheckForIllegalCrossThreadCalls = False
            trd = New Thread(AddressOf ThreadTask)
            trd.IsBackground = True
            trd.Start()

            Panel9.Visible = False
            Panel6.Visible = True
            Panel6.Dock = DockStyle.Fill
            Panel9.Dock = DockStyle.None

            Dim getdate As String = Date.Today.ToString("ddMMyyyy")
            'getKioskDetails() ' kioskBranch = readSettings(xml_Filename, xml_path, "kioskBranch")
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")

            'kioskCluster = readSettings(xml_Filename, xml_path, "kiosk_cluster")
            'kioskGroup = readSettings(xml_Filename, xml_path, "kiosk_group")
            'kioskID = readSettings(xml_Filename, xml_path, "kioskID")
            'kioskIP = readSettings(xml_Filename, xml_path, "kioskIP")
            'kioskName = readSettings(xml_Filename, xml_path, "kioskName")

            PopulateWebservicesLinks()
            SharedFunction.DownloadTechnicalRetirementRequirementDoc()

            _frmHomeScreen.Hide()

            TabControl1.SelectedTabIndex = 0
            'tagPath = xtd.rtTagPath()
            tagPath = xtd.checkFileType

            xtd.getRawFile()

            GetDriver()

            SharedFunction.ZoomFunction(False)

            If tagPath = 1 Then

                If (Not System.IO.Directory.Exists(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE)) Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE)
                End If

                Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password.Trim & ";"
                Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
                Dim dbComm As OracleCommand
                Dim getSSSall As String = xtd.getCRN
                getSSSall = getSSSall.Trim
                dbConn.Open()

                dbComm = dbConn.CreateCommand
                dbComm.Parameters.Add("msg", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
                dbComm.Parameters.Add("SSSCRN", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

                dbComm.Parameters("SSSCRN").Value = getSSSall
                dbComm.CommandText = "PKG_IKIOSK.CHECK_MEMSTATUS"
                dbComm.CommandType = CommandType.StoredProcedure
                dbComm.ExecuteNonQuery()
                dbConn.Close()

                Dim chkUT As String = dbComm.Parameters("msg").Value.ToString
                Dim passedParameter As String = chkUT
                ' Dim passedParameter As String = Command()
                'Dim passedParameter As String = "PENSIONER"

                If passedParameter.Contains("RETIREMENT PENSIONER") Or
                   passedParameter.Contains("RETIREMENT PENSIONER") Or
                   passedParameter.Contains("retirement pension") Or
                   passedParameter.Contains("RETIREMENT PENSION") Then
                    '  userType = 0
                    userType = 0
                Else
                    '  userType = 1
                    userType = 1
                End If

                Select Case userType
                    Case "1"
                        'btnACOP.Visible = False
                        'btnRetirement.Visible = True
                        'btnRegistration.Visible = True
                        'btnInquiry.Visible = True
                        'btnLoan.Visible = True
                        'Panel31.Visible = False
                        'Panel29.Visible = True
                        'Panel26.Visible = True

                        'Panel21.Visible = True
                        'Panel23.Visible = True
                        'Panel25.Visible = True
                        Panel24.Visible = True
                        Panel27.Visible = True

                        'Panel30.Visible = False
                        'Panel28.Visible = True


                        'Panel24.Visible = True

                        'pnlChangeAddresContact.Visible = False

                        Panel26.Visible = False
                        Panel29.Visible = False
                        'pnlMonthlyPensionSeparator.Visible = False
                        pnlMonthlyPension.Visible = False
                    Case "0"
                        'btnACOP.Visible = True
                        'btnRetirement.Visible = False
                        'btnRegistration.Visible = True
                        'btnInquiry.Visible = True
                        'btnLoan.Visible = False

                        'ACOP menu. disabled by edel as advised by Ms. Jenny/ Ms. Myla 06/20/2018
                        'Panel31.Visible = True

                        'Panel29.Visible = False
                        'Panel26.Visible = False

                        'Panel21.Visible = True
                        'Panel23.Visible = True
                        'Panel25.Visible = False
                        'Panel26.Visible = False
                        Panel24.Visible = False
                        Panel27.Visible = False

                        'Panel30.Visible = True
                        'Panel28.Visible = False


                        'Panel24.Visible = False

                        'pnlChangeAddresContact.Visible = True

                        Panel26.Visible = True
                        Panel29.Visible = True
                        'pnlMonthlyPensionSeparator.Visible = True
                        pnlMonthlyPension.Visible = True
                End Select


                'db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set status = '" & "True" & "', Online_Date = '" & Date.Today.ToShortDateString & _
                '     "', Online_Time = '" & TimeOfDay & "',lastOnline = '" & My.Settings.lastOnline & "' where IPAddress = '" & kioskIP & "' and Kiosk_ID = '" & kioskID & "'")


                Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")


                'db.sql = "Insert into SSINFOTERMCONSTAT (BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,ONLINE_DT,ONLINE_TME,DATESTAMP) values('" & kioskIP & "','" & getbranchCoDE & "','" & getkiosk_cluster & _
                '    "','" & getkiosk_group & "','" & Date.Today.ToShortDateString & "','" & TimeOfDay & "', '" & Today & "')"
                'db.ExecuteSQLQuery(db.sql)

                Dim getLastonlinedate As DateTime = Date.Today.ToShortDateString

                Dim getlastonlinetime As DateTime = TimeOfDay
                Dim finaldate As DateTime
                finaldate = getLastonlinedate & " " & getlastonlinetime
                lastOnline = finaldate
                ' xs.editSettings(xs.xml_Filename, xs.xml_path, "lastOnline", finaldate)

                db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set STATUS = '" & "1" & "', LONLINE_DT = '" & lastOnline & "' where BRANCH_IP = '" _
                             & kioskIP & "' and KIOSK_ID = '" & kioskID & "'")

                Me.KeyPreview = True

                tagPage = "0"

                editSettings(xml_Filename, xml_path, "SSStempFile", xtd.getCRN)
                SSStempFile = readSettings(xml_Filename, xml_path, "SSStempFile")
                getPermanentURL = readSettings(xml_Filename, xml_path, "getPermanentURL")
                getURL = getPermanentURL & "controller?action=sss&id=" & xtd.getCRN

                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & xtd.getCRN)

                BackNextControls(False)

            ElseIf tagPath = 2 Then


                'Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                'Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                If (Not System.IO.Directory.Exists(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE)) Then
                    System.IO.Directory.CreateDirectory(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE)
                End If

                Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
                Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
                Dim dbComm As OracleCommand
                Dim getSSSall As String = xtd.getCRN

                dbConn.Open()

                dbComm = dbConn.CreateCommand
                dbComm.Parameters.Add("msg", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
                dbComm.Parameters.Add("SSSCRN", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

                dbComm.Parameters("SSSCRN").Value = getSSSall
                'dbComm.Parameters("SSSCRN").Value = "0226879523"
                dbComm.CommandText = "PKG_IKIOSK.CHECK_MEMSTATUS"
                dbComm.CommandType = CommandType.StoredProcedure
                dbComm.ExecuteNonQuery()
                dbConn.Close()

                Dim chkUT As String = dbComm.Parameters("msg").Value.ToString
                Dim passedParameter As String = chkUT
                'Dim passedParameter As String = Command()
                'Dim passedParameter As String
                ' passedParameter = "PENSIONER"
                If passedParameter.Contains("RETIREMENT PENSIONER") Or passedParameter.Contains("retirement pension") Or passedParameter.Contains("RETIREMENT PENSION") Then
                    'If passedParameter.Contains("PENSIONER") Then
                    '  userType = 0
                    userType = 0
                Else
                    userType = 1
                    ' userType = 1
                End If

                Select Case userType
                    Case "1"
                        'btnACOP.Visible = False
                        'btnRetirement.Visible = True
                        'btnRegistration.Visible = True
                        'btnInquiry.Visible = True
                        'btnLoan.Visible = True
                        Panel31.Visible = False
                        Panel28.Visible = False
                        'Panel29.Visible = True
                        'Panel26.Visible = True

                        'Panel21.Visible = True
                        'Panel23.Visible = True
                        'Panel25.Visible = True
                        Panel24.Visible = True
                        Panel27.Visible = True

                        'Panel30.Visible = False
                        'Panel28.Visible = True

                        'Panel24.Visible = True

                        pnlChangeAddresContact.Visible = False
                    Case "0"
                        'btnACOP.Visible = True
                        'btnRetirement.Visible = False
                        'btnRegistration.Visible = True
                        'btnInquiry.Visible = True
                        'btnLoan.Visible = False

                        'ACOP menu. disabled by edel as advised by Ms. Jenny/ Ms. Myla 06/20/2018
                        Panel31.Visible = True
                        Panel28.Visible = True

                        'Panel29.Visible = False
                        'Panel26.Visible = False

                        'Panel21.Visible = True
                        'Panel23.Visible = True
                        'Panel25.Visible = False
                        'Panel26.Visible = False
                        Panel24.Visible = False
                        Panel27.Visible = False

                        'Panel30.Visible = True
                        'Panel28.Visible = False

                        'Panel24.Visible = False

                        pnlChangeAddresContact.Visible = False
                End Select

                Dim getLastonlinedate As DateTime = Date.Today.ToShortDateString
                Dim getlastonlinetime As DateTime = TimeOfDay
                Dim finaldate As DateTime
                finaldate = getLastonlinedate & " " & getlastonlinetime
                lastOnline = finaldate
                editSettings(xml_Filename, xml_path, "lastOnline", finaldate)
                db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set STATUS = '" & "1" & "', LONLINE_DT = '" & lastOnline & "' where BRANCH_IP = '" _
                            & kioskIP & "' and KIOSK_ID = '" & kioskID & "'")

                Me.KeyPreview = True

                'tagPage = "0"

                editSettings(xml_Filename, xml_path, "tagPage", "0")

                ' SSStempFile = xtd.getSSSNumber
                editSettings(xml_Filename, xml_path, "SSStempFile", xtd.getSSSNumber)
                SSStempFile = readSettings(xml_Filename, xml_path, "SSStempFile")
                'SSStempFile = "0226879523"
                'Dim fName As String '= xtd.getmFirName(_frmWebBrowser.WebBrowser1)
                'Dim lName As String ' = xtd.getmLasName(_frmWebBrowser.WebBrowser1)
                'Dim mName As String '= xtd.getmMidName(_frmWebBrowser.WebBrowser1)

                If Not isGSISCard Then
                    pnlChangeUmidPin.Visible = True
                    pnlChangeUmidPinSeparator.Visible = True
                Else
                    pnlChangeUmidPin.Visible = False
                    pnlChangeUmidPinSeparator.Visible = False
                End If

                getPermanentURL = readSettings(xml_Filename, xml_path, "getPermanentURL")
                'My.Settings.getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                '_frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)
                'editSettings(xml_Filename, xml_path, "getURL", getPermanentURL & "controller?action=sss&id=" & SSStempFile)
                getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)


                _frmWebBrowser.Hide()

                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "KIOSKLogs.txt", True)
                '    SW.WriteLine("ONLINE_DATE: " & My.Settings.lastOnline & "," & "BRANCH_NAME: " & kioskIP & "," & "KIOSK_ID: " & kioskID & "," & "STATUS: " & "ONLINE" & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                'End Using
            Else
                IsMainMenuActive = False
                Me.Hide()
                Main.Show()

                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "KIOSKLogs.txt", True)
                '    SW.WriteLine("ONLINE_DATE: " & My.Settings.lastOnline & "," & "BRANCH_NAME: " & kioskIP & "," & "KIOSK_ID: " & kioskID & "," & "STATUS: " & "ONLINE" & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                'End Using
            End If
            ' isLoading = 1

            ' trd.Abort()
            'xtd.getPensionDetails(SSStempFile)

            ifFirstLoad = 1

            ' ElseIf isCon = False Then
            'MsgBox("No connection")
            ''   ClearCache()
            'IsMainMenuActive = False
            'SharedFunction.ShowAppMainForm(Me)

            ' End If


        Catch ex As Exception


            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getdate As String = Date.Today.ToString("ddMMyyyy")
            'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
            '    SW.WriteLine("Loading Form" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
            'End Using
            'MsgBox("No Data Found!", MsgBoxStyle.Information, "Information")
            Dim pathX1 As String = Application.StartupPath & "\" & "temp" & "\"
            DeleteFile(pathX1)

            Me.Hide()
            'Main.Show()
            SharedFunction.ShowMainNewUserForm(New usrfrmIdle)

            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Form: Database Settings" & "', '" & "Click Kiosk Information save button error" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)
            Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Print Form" & "|" & "Click Kiosk Information save button error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
            'Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
            '    SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
            '       & "|" & "Form: Database Settings" & "|" & "Click Kiosk Information save button error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            'End Using
        End Try
    End Sub

    Private Sub _frmMainMenu_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        IsMainMenuActive = False
        Me.Hide()
        Main.Show()
        'MainLoad()
    End Sub

    Private Function GetUserType(ByVal param As String) As Short
        If param.Contains("PENSIONER") Then
            Return 0
        ElseIf param.Contains("RETIREMENT PENSIONER") Then
            Return 0
        ElseIf param.Contains("RETIREMENT PENSION") Then
            Return 0
        Else
            Return 1
        End If
    End Function

    Public Sub runTime()
        Dim getTime As String = TimeOfDay.ToString("tt hh:mm:ss")
        Dim getTimeTT As String = TimeOfDay.ToString("tt hh:mm:ss")
        getTimeTT = getTimeTT.Substring(0, 2)
        getTime = getTime.Substring(3, 8)

        Button4.Text = getTimeTT & " " & getTime

        '_frmFeedbackKiosk.Button2.Text = getTimeTT & " " & getTime

        Dim getDate As String = Date.Today.Day
        Dim getMonth As String = Date.Today.ToString("MMMM")
        getMonth = getMonth.Substring(0, 3)
        Dim getDay As String = Date.Today.ToString("dddd")
        lblDate.Text = getDate
        lblMonth.Text = getMonth
        lblDay.Text = getDay

    End Sub

    Private Sub ThreadTask()
        Do
            Try
                'If isLoading = 0 And _frmFirstLoad.Visible = False Then
                '    _frmFirstLoad.ShowDialog()
                'ElseIf isLoading = 1 And _frmFirstLoad.CircularProgress1.Value = 60 Then
                '    _frmFirstLoad.Dispose()
                'End If
                runTime()

            Catch ex As Exception
                'MsgBox("Time Settings is not Updated! ", MsgBoxStyle.Information, "Information")
                'Application.Exit()
                'System.Diagnostics.Process.Start(My.Settings.cardPath & "SSIT_HOME.exe")
            End Try
        Loop
    End Sub


    Private Sub btnRegistration_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRegistration.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then WebRegistrationv3()
        Catch ex As Exception
            ShowErrorForm("Simplified Registration", ex.Message)
        End Try
    End Sub

#Region "Web Registration"

    Public Sub BackNextControls(ByVal bln As Boolean, Optional isUpDown As Boolean = False)
        If bln Then
            If Not isUpDown Then
                Button5.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\backv2.png")
                Button6.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\nextv2.png")
            Else
                Button5.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\upv2.png")
                Button6.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\downv2.png")
            End If

        Else
            If Not isUpDown Then
                Button5.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\back_disabledv2.png")
                Button6.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\next_disabledv2.png")
            Else
                Button5.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\up_disabledv2.png")
                Button6.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\down_disabledv2.png")
            End If
        End If
        Button5.Enabled = bln
        Button6.Enabled = bln
    End Sub

    Public Sub PrintControls(ByVal bln As Boolean)
        If bln Then
            btnPrint.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\printv2.png")
        Else
            btnPrint.BackgroundImage = Image.FromFile(Application.StartupPath & "\images\print_disabledv2.png")
        End If
        btnPrint.Enabled = bln
    End Sub

    Public Sub CleanFormSession(Optional ByVal frms As Form() = Nothing)
        _frmWebBrowser.lblDisclaimer.Visible = False
        isShowFutronic = False
        Dim miscFrms As Form() = {_frm2, _frmFutronic, _frmUserAuthentication, _frmErrorForm, _frmCalendar}
        For Each frm As Form In miscFrms
            DisposeForm(frm)
        Next

        If Not frms Is Nothing Then
            For Each frm As Form In frms
                DisposeForm(frm)
            Next
        End If
    End Sub

    Public Sub WebRegistrationv3()
        CleanFormSession({_frmSWR1, _frmSWR2, _frmSWR2v2, _frmSWR3})

        transTag = "WR"
        tagPage = "1"
        _frmWebBrowser.WebBrowser1.Stop()
        xtd.getRawFile()

        Dim CRN As String = xtd.getCRN

        Dim getContact As New getContactInfo
        If getContact.Exception = "" Then
            If Not getContact.IsEmailValid Then
                If SharedFunction.ShowInfoMessage("In order to proceed, you need to provide your email address through the Update Contact Information module. ".ToUpper & vbNewLine & vbNewLine & "Please click ""OK"" to continue.".ToUpper, MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Me.btnUpdateContactInfo.PerformClick()
                Else
                    Me.btnInquiry_Click(False)
                End If
            Else
                If HTMLDataExtractor.IsRegisteredInSSSWebsite() Then
                    GC.Collect()
                    splitContainerControl.Panel2.Controls.Clear()
                    _frmSWR1.isMemberRegistered = True
                    _frmSWR1.TopLevel = False
                    _frmSWR1.Parent = Me.splitContainerControl.Panel2
                    _frmSWR1.Dock = DockStyle.Fill
                    _frmSWR1.Show()

                    PrintControls(True)

                    _frmUserAuthentication.getTransacNum()
                    SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10001", _frmUserAuthentication.lblTransactionNo.Text, "ALREADY REGISTERED TO SSS WEBSITE")
                Else
                    GC.Collect()
                    splitContainerControl.Panel2.Controls.Clear()
                    _frmSWR1.isMemberRegistered = False
                    _frmSWR1.TopLevel = False
                    _frmSWR1.Parent = Me.splitContainerControl.Panel2
                    _frmSWR1.Dock = DockStyle.Fill
                    _frmSWR1.Show()

                    SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10001", "", "")
                End If

                BackNextControls(False)
            End If
        Else
            Me.btnInquiry_Click(False)
        End If
    End Sub

#End Region

    Private Sub btnInquiry_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnInquiry.Click
        btnInquiry_Click(True)
    End Sub

    Public Sub DisposeForm(ByVal frm As Form)
        Try
            If Not frm Is Nothing Then
                frm.Dispose()
                frm.Close()
            End If
        Catch ex As Exception
            Dim errMsg As String = ex.Message
        End Try
    End Sub

    Public Sub btnInquiry_Click(Optional ByVal isChangeTab As Boolean = False)
        Try
            SharedFunction.ZoomFunction(False)

            GC.Collect()
            CleanFormSession()

            If isChangeTab Then
                TabItem2.Visible = True
                TabControl1.SelectedTabIndex = 1
            End If

            printTag = printZero
            xtd.getRawFile()
            BackNextControls(True)


            tagPage = "2"

            getEmpSalID = printF.GetEmployerIDSal(_frmWebBrowser.WebBrowser1)

            getErNo = getEmpSalID
            If getEmpSalID <> "" Then
                tempSalId = getEmpSalID
            End If

            getEmpSalName = printF.GetEmployerNameSal(_frmWebBrowser.WebBrowser1)

            getNameEr = getEmpSalName

            If getEmpSalName <> "" Then
                tempSalName = getEmpSalName
            End If

            _frmWebBrowser.Close()

            Dim getID As String = xtd.getCRN
            'new website link

            Dim result As Integer = xtd.checkFileType

            If result = 1 Then
                getPermanentURL = readSettings(xml_Filename, xml_path, "getPermanentURL")
                '  getURL = editSettings(xml_Filename, xml_path, "getURL", getPermanentURL & "controller?action=sss&id=" & xtd.getCRN)
                getURL = getPermanentURL & "controller?action=sss&id=" & xtd.getCRN
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & xtd.getCRN)
            ElseIf result = 2 Then
                getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)
            End If

            _frmWebBrowser.lblDisclaimer.Visible = False

            splitContainerControl.Panel2.Controls.Clear()
            pnlWeb.Parent = Me.splitContainerControl.Panel2
            _frmWebBrowser.TopLevel = False
            _frmWebBrowser.Parent = Me.pnlWeb
            _frmWebBrowser.Dock = DockStyle.Fill
            _frmWebBrowser.Show()

            SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10002", "", "")
        Catch ex As Exception
            ShowErrorForm("Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnLoan_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoan.Click
        btnLoanClickv2()
    End Sub

    Private Sub btnLoanClickv2()
        Try
            CleanFormSession({_frmSalaryLoanDisclosurev2, _frmSalaryLoanEmployerv2, _frmTerms, _frmSalaryLoanv2})

            transTag = "LG"


            printTag = printZero
            salPageTag = 0

            xtd.getRawFile()

            BackNextControls(False)

            tagPage = "3"

            Dim slMobileWS2BeanService As New SalaryLoan.slMobileWS2BeanService
            Select Case slMobileWS2BeanService.getMemberType(SSStempFile)
                Case 0
                    If slMobileWS2BeanService.getMemberTypeResponse.processFlag = "1" Then
                        Dim memberType As String = slMobileWS2BeanService.getMemberTypeResponse.memberType1
                        Dim employerSSNumber As String = "8888888006"
                        Dim seqNo As String = "000"

                        _frmSalaryLoanv2.seqNo = seqNo

                        Dim eligib As New SalaryLoan.eligibwebservice

                        Select Case eligib.calleligibility(SSStempFile, employerSSNumber, seqNo)
                            Case 0
                                If eligib.calleligibilityResponse.appl_st = "Qualified" Then
                                    Dim getContact As New getContactInfo
                                    If getContact.Exception = "" Then
                                        'If Not getContact.IsEmailValid Or Not getContact.IsMailingAddressValid Then
                                        If Not getContact.IsContactInfoValid Then
                                            If SharedFunction.ShowInfoMessage("In order to proceed, you need to provide your mailing address, email address and mobile/ telephone number through the Update Contact Information module. ".ToUpper & vbNewLine & vbNewLine & "Please click ""OK"" to continue.".ToUpper, MessageBoxButtons.OKCancel) = DialogResult.OK Then
                                                Me.btnUpdateContactInfo.PerformClick()
                                            Else
                                                Me.btnInquiry_Click(False)
                                            End If
                                        Else
                                            If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then _frmSalaryLoanv2.link2.Top += 14
                                            _frmSalaryLoanv2.lblMailingAddress.Text = getContact.MailingAddress
                                            _frmSalaryLoanv2.lblLandline.Text = getContact.TelephoneNos
                                            _frmSalaryLoanv2.lblEmailAddress.Text = getContact.Email
                                            _frmSalaryLoanv2.lblMobile.Text = getContact.MobileNos

                                            Dim isShowQuickCardNotes As Boolean = db.putSingleValue("select SalaryLoanUBPQuickCardNotes from SystemParameters")
                                            _frmSalaryLoanv2.lblReminder2.Text = _frmSalaryLoanv2.GetQuickCardNotes(isShowQuickCardNotes)

                                            _frmSalaryLoanv2.getBankAccountListBySSNumber()
                                            _frmSalaryLoanv2.employerSSNumber = employerSSNumber
                                            If Not _frmSalaryLoanv2.memberBankAccts Is Nothing Then
                                                If memberType = "SEVM" Then
                                                    _frmSalaryLoanv2.pnlEmployerSplitter.Visible = False
                                                    _frmSalaryLoanv2.pnlEmployer.Visible = False
                                                Else
                                                    Dim getLatestEmployer As New getLatestEmployer("SL")
                                                    _frmSalaryLoanv2.employers = getLatestEmployer.getLatestEmployers
                                                    If getLatestEmployer.Exception = "" Then
                                                        _frmSalaryLoanv2.lblEmployerId.Text = ""
                                                        _frmSalaryLoanv2.PopulateEmployers()
                                                    Else
                                                        Return
                                                    End If
                                                End If

                                                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10032", "", "")

                                                _frmSalaryLoanv2.loanAmount = eligib.calleligibilityResponse.loanableAmount
                                                _frmSalaryLoanv2.prevLoanAmount = eligib.calleligibilityResponse.totalbalance
                                                _frmSalaryLoanv2.serviceCharge = eligib.calleligibilityResponse.serviceFee
                                                _frmSalaryLoanv2.memberStatus = slMobileWS2BeanService.getMemberTypeResponse.memberTypeCode

                                                splitContainerControl.Panel2.Controls.Clear()
                                                _frmSalaryLoanv2.PopulateLonabaleAmount(eligib.calleligibilityResponse.maxLoanableAmount)
                                                _frmSalaryLoanv2.TopLevel = False
                                                _frmSalaryLoanv2.Parent = splitContainerControl.Panel2
                                                _frmSalaryLoanv2.Dock = DockStyle.Fill
                                                _frmSalaryLoanv2.Show()
                                            Else
                                                authentication = "SET001"
                                                authenticationMsg = "YOU ARE ELIGIBLE TO AVAIL OF SALARY LOAN. TO PROCEED, UPDATE THE INFORMATION BELOW:" & vbNewLine & vbNewLine &
                                                                        "(1) BANK ENROLLMENT (ENROLL YOUR ACTIVE DISBURSEMENT ACCOUNT THROUGH THE BANK ENROLLMENT MODULE IN THE WEBSITE)"
                                                RedirectToserAuthenticationForm("SALARY LOAN APPLICATION", "NO DISBURSEMENT ACCOUNT RECORD", "10032")
                                            End If
                                        End If
                                    Else
                                        btnInquiry_Click()
                                    End If
                                Else
                                    Dim sbRejectReason As New StringBuilder
                                    For Each reject As EligibilityWebserviceImplService.rejection In eligib.calleligibilityResponse.rej_dtls
                                        sbRejectReason.Append(reject.reasons.Trim & vbNewLine)
                                    Next

                                    authentication = "SET001"
                                    authenticationMsg = sbRejectReason.ToString.ToUpper()
                                    RedirectToserAuthenticationForm("SALARY LOAN APPLICATION", "CALLELIGIBILITY RESPONSE IS " & eligib.calleligibilityResponse.appl_st.ToUpper, "10032")
                                End If
                            Case 1
                                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10032", "", "calleligibility - UNABLE TO CONNECT TO REMOTE SERVER.")
                                SharedFunction.ShowUnableToConnectToRemoteServerMessage()
                                Me.btnInquiry_Click()
                            Case Else
                                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10032", "", "calleligibility - " & eligib.calleligibilityResponse.rejlist.ToUpper)
                                SharedFunction.ShowAPIResponseMessage(eligib.exceptions.ToUpper)
                                Me.btnInquiry_Click()
                        End Select
                    Else
                        _frmUserAuthentication.getTransacNum()
                        authentication = "SET002"
                        authenticationMsg = slMobileWS2BeanService.getMemberTypeResponse.returnMessage.ToUpper
                        RedirectToserAuthenticationForm("SALARY LOAN APPLICATION", "GETMEMBERTYPE RESPONSE " & authenticationMsg, "10032")
                    End If
                Case 1
                    SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10032", "", "getMemberType - UNABLE TO CONNECT TO REMOTE SERVER.")
                    SharedFunction.ShowUnableToConnectToRemoteServerMessage()
                    Me.btnInquiry_Click()
                Case Else
                    SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10032", "", "getMemberType - " & slMobileWS2BeanService.exceptions.ToUpper)
                    SharedFunction.ShowAPIResponseMessage(slMobileWS2BeanService.exceptions.ToUpper)
                    Me.btnInquiry_Click()
            End Select
        Catch ex As Exception
            ShowErrorForm("Salary loan application", ex.Message)
        End Try
    End Sub

    Public Sub RedirectToserAuthenticationForm(ByVal header As String, ByVal transDesc As String, ByVal code As String)
        Try
            splitContainerControl.Panel2.Controls.Clear()
            _frmUserAuthentication.TopLevel = False
            _frmUserAuthentication.Parent = splitContainerControl.Panel2
            _frmUserAuthentication.Dock = DockStyle.Fill
            _frmUserAuthentication.Show()

            BackNextControls(False)
            PrintControls(False)

            _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = header
            SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, code, _frmUserAuthentication.lblTransactionNo.Text, transDesc)
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private wbTemp As WebBrowser
    Private Property pageready As Boolean = False

    Private Sub WaitForPageLoad()
        AddHandler wbTemp.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        While Not pageready
            Application.DoEvents()
        End While
        pageready = False
    End Sub

    Private Sub PageWaiter(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        If wbTemp.ReadyState = WebBrowserReadyState.Complete Then
            pageready = True
            RemoveHandler wbTemp.DocumentCompleted, New WebBrowserDocumentCompletedEventHandler(AddressOf PageWaiter)
        End If
    End Sub

    Private Sub btnMaternity_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaternity.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then MaternityNotifv2()
        Catch ex As Exception
            ShowErrorForm("Maternity Notification", ex.Message)
        End Try

    End Sub

#Region "Maternity Notification"

    Public Sub MaternityNotif()
        tagPage = "4"
        _frm2.Dispose()
        _frm2.Close()
        '_frmUserAuthentication.Dispose()
        DisposeForm(_frmUserAuthentication)

        transTag = "MT"

        xtd.getRawFile()

        Dim result As Integer = xtd.checkFileType
        Dim resultSSS As String = ""
        If result = 1 Then
            resultSSS = xtd.getCRN
        ElseIf result = 2 Then
            resultSSS = SSStempFile
        End If

        'temp disabled for grace testing 01/26/2017
        If db.checkExistence("SELECT SSNUM, DATEPART(dd, ENCODE_DT) FROM SSTRANSINFORTERMMN WHERE (SSNUM = '" & resultSSS & "') AND (DATEPART(dd, ENCODE_DT) = '" & Date.Today.ToString("dd") & "') ") = True Then
            'If db.checkExistence("SELECT SSNUM, DATEPART(dd, ENCODE_DT) FROM SSTRANSINFORTERMMN WHERE (SSNUM = '123456789123456789') AND (DATEPART(dd, ENCODE_DT) = '" & Date.Today.ToString("dd") & "') ") = True Then
            '  If db.checkExistence("select SSNUM,ENCODE_DT FROM SSTRANSINFORTERMMN where SSNUM ='" & xtd.getCRN & "' ") = True Then
            'MsgBox("This account is already registered. ", MsgBoxStyle.Information, "Information") ' nikki01
            _frmUserAuthentication.getTransacNum()
            GC.Collect()

            authentication = "MNS08"
            splitContainerControl.Panel2.Controls.Clear()
            _frmUserAuthentication.TopLevel = False
            _frmUserAuthentication.Parent = splitContainerControl.Panel2
            _frmUserAuthentication.Dock = DockStyle.Fill
            _frmUserAuthentication.Show()
            BackNextControls(False)
            'Button5.Enabled = False
            'Button6.Enabled = False
            'Button5.Text = "BACK"
            'Button6.Text = "NEXT"
            ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

            PrintControls(True)
            'PrintControls(True)
            _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
            '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "
            Dim transDesc As String = "HAVE AN EXISTING APPLICATION."

            Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
            'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
            Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
            End Using
        Else

            '_frmMaternityNotification.Dispose()

            GC.Collect()
            _frmCalendar.Dispose()
            'Control.CheckForIllegalCrossThreadCalls = False
            'trd = New Thread(AddressOf ThreadTask)
            'trd.IsBackground = True
            'trd.Start()
            _frmWebBrowser.lblDisclaimer.Visible = False
            printTag = printZero

            PrintControls(False)
            BackNextControls(False)

            splitContainerControl.Panel2.Controls.Clear()

            '_frmUserAuthentication.Dispose()
            DisposeForm(_frmUserAuthentication)

            getEmpSalID = printF.GetEmployerIDSal(_frmWebBrowser.WebBrowser1)

            getErNo = getEmpSalID
            If getEmpSalID <> "" Then
                tempSalId = getEmpSalID
            End If

            getEmpSalName = printF.GetEmployerNameSal(_frmWebBrowser.WebBrowser1)

            getNameEr = getEmpSalName
            If getEmpSalName <> "" Then
                tempSalName = getEmpSalName
            End If

            'Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & My.Settings.db_Host & ")(PORT=" & My.Settings.db_Port & "))(CONNECT_DATA=(SID=" & My.Settings.db_ServiceName & ")));User Id=" & My.Settings.db_UserID & ";Password=" & My.Settings.db_Password & ";"
            'Dim cn As OracleConnection = New OracleConnection(MyConnection)

            'Procedure PR_MATNOTIF by ms.myla from SSS - april 3, 2014
            'tempSalId = tempSalId.Replace("-", "")
            'If tempSalId = "8888888006" Or tempSalId = "0000000000" Then

            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            'Dim dbConn As New OracleConnection
            Dim dbComm As OracleCommand
            ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("SSNUM", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

            If result = 1 Then
                resultSSS = xtd.getCRN
            ElseIf result = 2 Then
                resultSSS = SSStempFile
            End If
            dbComm.Parameters("SSNUM").Value = resultSSS
            dbComm.CommandText = "PR_IK_MATNOTIF"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()

            Dim maternityReturn As String = dbComm.Parameters("MSG").Value.ToString
            '  maternityReturn = "null"
            If maternityReturn.Contains("ORA-") Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                Dim prt As New printModule
                Dim f2, f3 As String
                f2 = prt.GetMiddleName(_frmWebBrowser.WebBrowser1)
                f3 = prt.GetLastName(_frmWebBrowser.WebBrowser1)
                getFname = f3 & " " & f2

                authentication = "ER01"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                Dim transDesc As String = "AN ERROR ENCOUNTERED DURING REQUEST."

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                ' at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Please request assistance Member Service Representative at frontline Service Counter. "


                'ElseIf maternityReturn = "null" Then
                '    GC.Collect()
                '    splitContainerControl.Panel2.Controls.Clear()
                '    _frmMaternityNotification.TopLevel = False
                '    _frmMaternityNotification.Parent = Me.splitContainerControl.Panel2
                '    _frmMaternityNotification.Dock = DockStyle.Fill
                '    _frmMaternityNotification.Show()
                '    tagPage = "4"

                '    ' at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                '    Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                '        SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                '    End Using

                '    BackNextControls(False)

            ElseIf maternityReturn = "The member has already availed a final claim." Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN03"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                'Button5.Image = Image.FromFile(Application.StartupPath & "\images\DOWN.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\UPBUTTON.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "MEMBER HAS AVAILED A FINAL CLAIM."
                ' at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            ElseIf maternityReturn = "No supporting document submitted to support the SS Form E-1(Temporary SS number)" Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN04"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "NO SUPPORTING DOCUMENT."
                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            ElseIf maternityReturn = "Member's SS number has inactive status." Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN05"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "SS NUMBER HAS INACTIVE STATUS."
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            ElseIf maternityReturn = "Member's age is beyond the range 14 to 60 yrs old." Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN06"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "BEYOND THE RANGE OF 14 TO 16 YRS OLD."
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            ElseIf maternityReturn = "This facility is for self-employed/voluntary members only, please submit your maternity notification through your Employer Authorized Signatory." Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN07"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "MEMBER IS NOT VALID FOR THE TRANSACTION."
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            ElseIf maternityReturn = "Member's not female per SS record." Then
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN02"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"

                '_frmUserAuthentication.lblFooter.Text = "Report on rejected maternity notifications shall be generated. "
                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "MEMBER IS NOT FEMALE."
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
            Else
                GC.Collect()
                _frmUserAuthentication.getTransacNum()
                authentication = "MN01"
                errorTag = "MATERNITY NOTIFICATION"
                splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                BackNextControls(False)
                'Button5.Enabled = False
                'Button6.Enabled = False
                'Button5.Text = "BACK"
                'Button6.Text = "NEXT"
                ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                PrintControls(True)
                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "AN ERROR ENCOUNTERED DURING REQUEST."
                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
            End If
        End If
    End Sub

    Public Function getCRN_SSSNum() As String
        Dim result As Integer = xtd.checkFileType
        If result = 1 Then Return xtd.getCRN
        If result = 2 Then Return SSStempFile
        Return ""
    End Function

    Public Sub MaternityNotifv2()
        CleanFormSession({_frmEnhanceMaternityNotif, _frmEnhanceMaternityNotifSummary})

        tagPage = "4"
        transTag = "MT"

        xtd.getRawFile()

        Dim result As Integer = xtd.checkFileType
        Dim resultSSS As String = getCRN_SSSNum()

        'temp disabled for grace testing 01/26/2017
        If db.checkExistence("SELECT SSNUM, DATEPART(dd, ENCODE_DT) FROM SSTRANSINFORTERMEMN WHERE (SSNUM = '" & resultSSS & "') AND (DATEPART(dd, ENCODE_DT) = '" & Date.Today.ToString("dd") & "') ") = True Then
            authentication = "MNS08"
            BackNextControls(False)
            PrintControls(True)
            _frmUserAuthentication.getTransacNum()
            _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
            SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10027", _frmUserAuthentication.lblTransactionNo.Text, "HAVE AN EXISTING APPLICATION.")
            ShowPanelForm(_frmUserAuthentication)
        Else
            printTag = printZero
            PrintControls(False)
            BackNextControls(False)


            splitContainerControl.Panel2.Controls.Clear()

            getEmpSalID = printF.GetEmployerIDSal(_frmWebBrowser.WebBrowser1)

            getErNo = getEmpSalID
            If getEmpSalID <> "" Then
                tempSalId = getEmpSalID
            End If

            getEmpSalName = printF.GetEmployerNameSal(_frmWebBrowser.WebBrowser1)

            getNameEr = getEmpSalName
            If getEmpSalName <> "" Then
                tempSalName = getEmpSalName
            End If

            Dim men As New Maternity.EnhanceNotification
            men.sssNo = resultSSS
            Dim isMatNotifEligible = men.isMatNotifEligible()
            If isMatNotifEligible.processFlag = "1" Then
                BackNextControls(False)
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10027", "", "")
                ShowPanelForm(_frmEnhanceMaternityNotif)
            Else
                authentication = "EMN01"
                authenticationMsg = isMatNotifEligible.returnMessage
                _frmUserAuthentication.getTransacNum()
                BackNextControls(False)
                PrintControls(True)
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10027", _frmUserAuthentication.lblTransactionNo.Text, isMatNotifEligible.returnMessage)
                ShowPanelForm(_frmUserAuthentication)
            End If
            men = Nothing
        End If
    End Sub

#End Region

    Private Sub btnRetirement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetirement.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then technicalRetv2()
        Catch ex As Exception
            ShowErrorForm("Online Retirement", ex.Message)
        End Try
    End Sub

    Public Function getMsg(ByVal dob As String)
        Try
            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand

            dbConn.Open()

            dbComm = dbConn.CreateCommand
            dbComm.CommandTimeout = 0
            dbComm.Parameters.Add("STRSSSID", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("IN_DOB_DT", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("RETDT", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output
            dbComm.Parameters("STRSSSID").Value = SSStempFile
            dbComm.Parameters("IN_DOB_DT").Value = dob
            dbComm.CommandText = "PR_IK_RETIREMENT"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()
            techretdate = dbComm.Parameters("RETDT").Value.ToString


            Dim promptMsg As String = dbComm.Parameters("MSG").Value.ToString

            Return promptMsg
        Catch ex As Exception
            Console.WriteLine(ex.Message)

            Return ""
        End Try
        ' dob = dob.ToString("dd-MM-YYYY")



    End Function

#Region "Technical Retirement"

    Public Sub ManageMenuVisibility()
        Dim bdate As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)

        Dim tempDate As String = bdate
        Dim finalDate As String
        If bdate = Nothing Or bdate = "" Then
            tempDate = xtd.getbDate

            bdate = tempDate

            If tempDate = "" Then
            Else
                finalDate = SharedFunction.GetYearDifference(bdate, Now)
            End If
        Else
            finalDate = SharedFunction.GetYearDifference(bdate, Now)
        End If


        Dim getLenSS As String = xtd.getCRN
        getLenSS = getLenSS.Length

        finalDate = "65"
    End Sub

    Public Sub technicalRetv2()
        CleanFormSession({_frmTechRetirementEmpHist, _frmTechRetirementConfirm, _frmTechRetirementApplyBank, _frmTechRetirementApplyMineworker, _frmTechRetirementApplyValidation, _frmTechRetirementApplyDate, frmTechRetirementEmpHistInvalid, _frmTechRetirementNoDaem, _frmTechRetirementERNotRegistered, _frmTerms})

        transTag = "RT"
        xtd.getRawFile()
        Dim getCRN As String = xtd.getSSSNumber

        tagPath = 2

        Dim slMobileWS2BeanService As New SalaryLoan.slMobileWS2BeanService

        Dim latestEmployer As String = ""

        Dim getContact As New getContactInfo
        If getContact.Exception = "" Then
            Dim isValid As Boolean = True

            If Not getContact.IsMailingAddressValid Then isValid = False
            If Not getContact.IsEmailValid Then isValid = False

            If Not isValid Then
                If SharedFunction.ShowInfoMessage("In order to proceed with the application, you need to provide your mailing address through the Update Contact Information module. ".ToUpper & vbNewLine & vbNewLine & "Please click ""OK"" to continue.".ToUpper, MessageBoxButtons.OKCancel) = DialogResult.OK Then
                    Me.btnUpdateContactInfo.PerformClick()
                Else
                    Me.btnInquiry_Click()
                End If
            Else
                Select Case CHECK_MEMSTATUS_Settings.ToUpper
                    Case "COVERED EMPLOYEE"
                        Dim getLatestEmployer As New getLatestEmployer("RT")
                        latestEmployer = getLatestEmployer.getLatestEmployers(0).employerName
                        Select Case getLatestEmployer.processFlag
                            Case "2"
                                'authentication = "SET002"
                                'authenticationMsg = getLatestEmployer.Exception
                                'RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", getLatestEmployer.Exception, "10028")
                                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10028", "", getLatestEmployer.Exception)
                                employmentHistory(getLatestEmployer.processFlag)
                                Return
                            Case "3"
                                'authentication = "SET002"
                                'authenticationMsg = getLatestEmployer.Exception
                                'RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "CERTIFYING EMPLOYER IS NOT REGISTERED", "10028")

                                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10028", "", getLatestEmployer.Exception)
                                employmentHistory(getLatestEmployer.processFlag)
                                Return
                            Case Else
                                If getLatestEmployer.Exception = "" Then
                                    Dim memberType As String = CHECK_MEMSTATUS_Settings
                                    _frmTechRetirementEmpHist.memberType = memberType
                                    _frmTechRetirementEmpHist.employerSSNumber = getLatestEmployer.getLatestEmployers(0).employerSSNumber
                                    _frmTechRetirementEmpHist.employerName = getLatestEmployer.getLatestEmployers(0).employerName
                                    _frmTechRetirementEmpHist.employerERBR = getLatestEmployer.getLatestEmployers(0).erbrn
                                Else
                                    Return
                                End If
                        End Select

                        If getBankAccountListBySSNumber() Then
                            ShowPanelForm(_frmTechRetirementApplyDate)
                        Else
                            ShowPanelForm(_frmTechRetirementNoDaem)
                        End If
                    Case Else
                        determineDoctg_OnlineRt("")
                End Select
            End If
        Else
            Me.btnInquiry_Click()
        End If
        'End Select
    End Sub

    Public Sub determineDoctg_OnlineRt(ByVal separationDate As String) '(ByVal separationDate As String, ByRef output As String())
        Dim onlineRetirement As New OnlineRetirement
        Try
            If onlineRetirement.determineDoctg_OnlineRt(SSStempFile, Date.Now.ToString("MM/dd/yyyy"), separationDate) Then
                _frmTechRetirementEmpHist.flg_60 = onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_60
                _frmTechRetirementEmpHist.type_of_retirement = onlineRetirement.memberClaimInformationEntitiesResponse(0).type_of_retirement
                _frmTechRetirementEmpHist.determined_doctg = onlineRetirement.memberClaimInformationEntitiesResponse(0).determined_doctg
                'output = {onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_60, onlineRetirement.memberClaimInformationEntitiesResponse(0).type_of_retirement, onlineRetirement.memberClaimInformationEntitiesResponse(0).determined_doctg}
                If onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_60 = "0" Then
                    _frmTechRetirementEmpHist.separationDate = separationDate

                    If separationDate = "" Then
                        If onlineRetirement.memberClaimInformationEntitiesResponse(0).type_of_retirement = "Technical Retirement" Then
                            employmentHistory()
                        Else
                            Select Case CHECK_MEMSTATUS_Settings.ToUpper
                                Case "HOUSEHOLD", "SELF-EMPLOYED", "SELF EMPLOYED", "HOUSEHOLD HELPER", "NON-WORKING SPOUSE", "NON WORKING SPOUSE"
                                    authentication = "SET002"
                                    authenticationMsg = "PLEASE BE INFORMED THAT YOU WILL BE REQUIRED TO SUBMIT DOCUMENTARY REQUIREMENTS. HENCE, THE RECEIPT OF RETIREMENT CLAIM APPLICATION SHALL BE HANDLED AT ANY SSS BRANCH NEAR YOU."
                                    RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", CHECK_MEMSTATUS_Settings.ToUpper & ". DENIED DUE TO MEMBERSHIP STATUS.", "10028")
                                Case Else
                                    employmentHistory()
                            End Select
                        End If
                    Else
                        employmentHistory()
                    End If
                Else
                    authentication = "SET002"
                    authenticationMsg = "INVALID DATE OF BIRTH" &
                                    vbNewLine & vbNewLine & "YOU MAY SEEK ASSISTANCE FOR CORRECTION OF YOUR EMPLOYMENT RECORD FROM OUR MEMBER SERVICE REPRESENTATIVE AT THE SERVICE COUNTER OF ANY SSS BRANCH NEAR YOU."
                    RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "INVALID DATE OF BIRTH", "10028")
                End If
            End If
        Catch ex As Exception
        Finally
            onlineRetirement = Nothing
        End Try
    End Sub

    Public Sub employmentHistory(Optional ByVal latestEmployerProcessFlag As String = "0")
        If latestEmployerProcessFlag = "0" Then If Not checkElig_OnlineRt(_frmTechRetirementEmpHist.determined_doctg) Then Return

        Dim onlineRetirement As New OnlineRetirement
        If onlineRetirement.employmentHistory(SSStempFile, Date.Now.ToString("MM/dd/yyyy")) Then
            Select Case latestEmployerProcessFlag
                Case "2"
                    'Dim empHistories As New List(Of empHist)
                    'For i As Short = 1 To 10
                    '    Dim eH As New empHist
                    '    eH.emp_name = "Employer - " & i.ToString
                    '    eH.emp_date = i.ToString & " - " & (Now.Year + i).ToString
                    '    empHistories.Add(eH)
                    'Next

                    _frmTechRetirementERNotRegistered.UsrfrmContactInfo1.PopulateContactInfo()
                    _frmTechRetirementERNotRegistered.lblMsg.Text = "Please be informed that you have simultaneous multiple employment. Hence, you will be required to submit documentary requirements. For this reason, the receipt of your retirement claim application shall be handled at any SSS Branch near you."
                    _frmTechRetirementERNotRegistered.grid.DataSource = onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List
                    ShowPanelForm(_frmTechRetirementERNotRegistered)
                Case "3"
                    'Dim empHistories As New List(Of empHist)
                    'For i As Short = 1 To 10
                    '    Dim eH As New empHist
                    '    eH.emp_name = "Employer - " & i.ToString
                    '    eH.emp_date = i.ToString & " - " & (Now.Year + i).ToString
                    '    empHistories.Add(eH)
                    'Next

                    _frmTechRetirementERNotRegistered.UsrfrmContactInfo1.PopulateContactInfo()
                    _frmTechRetirementERNotRegistered.grid.DataSource = onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List
                    _frmTechRetirementERNotRegistered.lblMsg.Text = "Please be informed that your certifiying employer " & onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List(0).emp_name & " is not registered in the SSS Website. Hence, you will be required to submit documentary requirements. For this reason, the receipt of your retirement claim application shall be handled at any SSS Branch near you."
                    ShowPanelForm(_frmTechRetirementERNotRegistered)
                Case Else
                    If onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List(0).emp_date <> "09-9999" Then
                        If getBankAccountListBySSNumber() Then
                            'if all all validations are correct
                            _frmTechRetirementEmpHist.grid.DataSource = onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List
                            _frmTechRetirementEmpHist.btnNext.Focus()
                            ShowPanelForm(_frmTechRetirementEmpHist)
                        Else
                            ShowPanelForm(_frmTechRetirementNoDaem)
                        End If
                    Else
                        '    Dim msg As String = "PLEASE BE INFORMED THAT THERE IS A DISCREPANCY IN YOUR EMPLOYMENT DATE." &
                        '                                            vbNewLine & vbNewLine & "YOU MAY SEEK ASSISTANCE FOR CORRECTION OF YOUR EMPLOYMENT RECORD FROM OUR MEMBER SERVICE REPRESENTATIVE AT THE SERVICE COUNTER OF ANY SSS BRANCH NEAR YOU."
                        TechRetirementEmpHistInvalid_emp_date9999(onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List)
                    End If
            End Select
        Else
            SharedFunction.ShowUnableToConnectToRemoteServerMessage()
        End If
        onlineRetirement = Nothing
    End Sub

    Public Function getBankAccountListBySSNumber() As Boolean
        Dim bln As Boolean = False
        Dim noOfBankAcct As Short = 0
        Dim sl As New SalaryLoan.slBankWorkflowWebService

        Try
            If sl.getBankAccountListBySSNumber(SSStempFile) Then
                If sl.getBankAccountListBySSNumberResponse(0).REPLY_CODE = "0" Then
                    For Each anw As BankWorkflowWebService.accountNumberWorkflow In sl.getBankAccountListBySSNumberResponse
                        If anw.REPLY_CODE = "0" Then noOfBankAcct += 1
                    Next

                    If noOfBankAcct > 0 Then bln = True
                End If

                'If Not memberBankAccts Is Nothing Then bln = True
            End If
        Catch ex As Exception
        Finally
            sl = Nothing
        End Try

        If File.Exists(Application.StartupPath & "\orBankAcct.txt") Then
            Return False
        Else
            Return bln
        End If

        'Return bln
        'Return False
    End Function

    Class empHist

        Private emp_nameValue As String
        Public Property emp_name() As String
            Get
                Return emp_nameValue
            End Get
            Set(ByVal value As String)
                emp_nameValue = value
            End Set
        End Property

        Private emp_dateValue As String
        Public Property emp_date() As String
            Get
                Return emp_dateValue
            End Get
            Set(ByVal value As String)
                emp_dateValue = value
            End Set
        End Property

    End Class

    Public Function checkElig_OnlineRt(ByVal determined_doctg As String) As Boolean
        Dim onlineRetirement As New OnlineRetirement
        If onlineRetirement.checkElig_OnlineRt(SSStempFile, determined_doctg) Then
            _frmTechRetirementApplyMineworker.mp_amt = onlineRetirement.memberClaimInformationEntitiesResponse(0).mp_amt
            _frmTechRetirementApplyMineworker.flg_120 = onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_120
            _frmTechRetirementApplyValidation.status_flag = onlineRetirement.memberClaimInformationEntitiesResponse(0).status_flag
            _frmTechRetirementApplyValidation.avail_18mos_flg = onlineRetirement.memberClaimInformationEntitiesResponse(0).avail_18mos_flg
            _frmTechRetirementApplyMineworker.flg_1k = onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_1k
            If onlineRetirement.memberClaimInformationEntitiesResponse(0).status_flag = "1" Then
                Dim sbReasons As New System.Text.StringBuilder
                For Each reason As String In onlineRetirement.memberClaimInformationEntitiesResponse(0).msg_List
                    If reason.Trim <> "" Then
                        sbReasons.Append(" • " & reason & vbNewLine)
                    End If
                Next

                authentication = "SET002"
                authenticationMsg = sbReasons.ToString
                RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "DISCREPANCY IN EMPLOYMENT DATE", "10028")

                Return False
            Else
                Return True
            End If
        Else
            SharedFunction.ShowUnableToConnectToRemoteServerMessage()
            Return False
        End If
    End Function

    Public Sub TechRetirementEmpHistInvalid(ByVal msg As String, ByVal eh_List As OnlineRetirementWebServiceImplService.employerHistory())
        frmTechRetirementEmpHistInvalid.lblMsg9999.Visible = False
        frmTechRetirementEmpHistInvalid.lblMsg.Text = msg
        frmTechRetirementEmpHistInvalid.grid.DataSource = eh_List
        ShowPanelForm(frmTechRetirementEmpHistInvalid)
    End Sub

    Public Sub TechRetirementEmpHistInvalid_emp_date9999(ByVal eh_List As OnlineRetirementWebServiceImplService.employerHistory())
        frmTechRetirementEmpHistInvalid.lblMsg9999.Visible = True
        frmTechRetirementEmpHistInvalid.lblMsg.Text = "YOU MAY SEEK ASSISTANCE FOR CORRECTION OF YOUR EMPLOYMENT RECORD FROM OUR MEMBER SERVICE REPRESENTATIVE AT THE SERVICE COUNTER OF ANY SSS BRANCH NEAR YOU."
        'orig location 14, 361, orig size 825, 227
        frmTechRetirementEmpHistInvalid.lblMsg.Location = New Point(14, 400)
        frmTechRetirementEmpHistInvalid.lblMsg.Height = 188
        frmTechRetirementEmpHistInvalid.grid.DataSource = eh_List
        ShowPanelForm(frmTechRetirementEmpHistInvalid)
    End Sub

#End Region

    Private Sub ShowLoadingScreen()
        DisposeForm(_frmLoading)
        _frmLoading.TopLevel = False
        _frmLoading.Parent = splitContainerControl.Panel2
        _frmLoading.Dock = DockStyle.Fill
        _frmLoading.Show()
        Application.DoEvents()
    End Sub

    Public Sub ShowLoadingScreenFloat()
        _frmLoading.Show()
        Application.DoEvents()
    End Sub

    Private Sub btnPRN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPRN.Click
        Try
            Invoke(New Action(AddressOf ShowLoadingScreen))
            newGeneratedPRN = ""
            PRN_Application()
        Catch ex As Exception
            ShowErrorForm("PRN", ex.Message)
        End Try
    End Sub

    Private Sub btnACOP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnACOP.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then acopSub()
        Catch ex As Exception
            ShowErrorForm("ACOP", ex.Message)
        End Try
    End Sub

    Public Sub ShowErrorForm(ByVal btnDesc As String, ByVal err As String)
        SharedFunction.SaveToInfoTerminalLog(Path1, btnDesc, err)

        GC.Collect()
        splitContainerControl.Panel2.Controls.Clear()
        _frmErrorForm.TopLevel = False
        _frmErrorForm.Parent = Me.splitContainerControl.Panel2
        _frmErrorForm.Dock = DockStyle.Fill
        _frmErrorForm.Show()
    End Sub

#Region " PRN "

    Public Sub PRN_Application()
        SharedFunction.Clear_PRN_Sessions()

        CleanFormSession({_frmPRN_Generate, _frmPRNApplication, _frmPRNApplication_Confirm, _frmPRNApplication_ConfirmFinal, _frmPRNApplication_ConfirmFinal_List, _frmPRNContributions, _frmPRNContributions2})

        BackNextControls(False)
        PrintControls(False)

        transTag = "PRN"

        _frmWebBrowser.lblDisclaimer.Visible = False

        xtd.getRawFile()
        tagPage = "8"

        Dim chkType As String = xtd.checkFileType

        Dim tempSS As String
        If chkType = 1 Then
            tempSS = xtd.getCRN
        ElseIf chkType = 2 Then
            tempSS = SSStempFile
        End If

        SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10046", "", "")
        '_frmUserAuthentication.getTransacNum()
        authentication = "PRN01"
        ShowPanelForm(_frmPRN_Generate)

        '_frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "PAYMENT REFERENCE NUMBER (PRN)"
    End Sub

#End Region

#Region " Update Contact Information "

    Public Sub UpdateContactInformationv3()
        Invoke(New Action(AddressOf ShowLoadingScreen))

        DisposeForm(_frmUpdCntcInfv2)

        transTag = "UCI"

        _frmWebBrowser.lblDisclaimer.Visible = False
        DisposeForm(_frmUserAuthentication)
        tagPage = "13"

        GC.Collect()

        authentication = "UCI01"

        SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10047", "", "")

        splitContainerControl.Panel2.Controls.Clear()
        _frmUpdCntcInfv2.TopLevel = False
        _frmUpdCntcInfv2.Parent = splitContainerControl.Panel2
        _frmUpdCntcInfv2.Dock = DockStyle.Fill
        _frmUpdCntcInfv2.Show()

        PrintControls(False)
        'BackNextControls(True)
        BackNextControls(True, True)

        'pnlWebContactInfo.Location = New Point(-124, -1)
        'pnlWebContactInfo.Size = New Size(1191, 1051)
        'splitContainerControl.Panel2.Controls.Clear()
        'pnlWebContactInfo.Parent = Me.splitContainerControl.Panel2
        '_frmUpdCntcInfv2.TopLevel = False
        '_frmUpdCntcInfv2.Parent = Me.pnlWebContactInfo
        '_frmUpdCntcInfv2.Dock = DockStyle.Fill
        '_frmUpdCntcInfv2.Show()


        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "UPDATE CONTACT INFORMATION"
    End Sub

#End Region

#Region "ACOP"

    Public Sub acopSub()
        CleanFormSession({_frmACOPconfirmationDependent, _frmACOPdependent, _frmAcopSummary})

        BackNextControls(False)

        transTag = "AC"

        _frmWebBrowser.lblDisclaimer.Visible = False

        xtd.getRawFile()
        tagPage = "6"
        Dim chkType As String = xtd.checkFileType
        'Dim result As String
        Dim tempSS As String = ""
        If chkType = 1 Then
            tempSS = xtd.getCRN
        ElseIf chkType = 2 Then
            tempSS = SSStempFile
        End If
        Dim currYear As String
        'If db.checkExistence("select * FROM SSTRANSACOP WHERE SSNUM ='" & tempSS & "' and NXTSUBM = '" & Date.Today & "'") = True Then
        ' If db.checkExistence("SELECT ssnum,DATEADD(month,-6, NXTSUBM) from SSTRANSACOP where ssnum ='" & tempSS & "' AND  GETDATE() > DATEADD(month, - 6, NXTSUBM) ") = True Then
        currYear = db.putSingleValue("select DATEPART(yyyy, GETDATE()) ")
        Dim existenceSSS As Date
        'Dim getPastSix As String
        'Dim checkExist As String

        Dim dateToday As Date = Date.Today

        'temp disabled for grace testing 01/26/2017
        If db.checkExistence("select ssnum from SSTRANSACOP  where ssnum ='" & tempSS & "'") = True Then
            'If db.checkExistence("select ssnum from SSTRANSACOP  where ssnum ='123456789123456789'") = True Then
            'Dim exYear As String = existenceSSS.ToString
            existenceSSS = db.putSingleValue("select max(NXTSUBM) from SSTRANSACOP  where ssnum ='" & tempSS & "' GROUP BY SSNUM  ")
            Dim bMonth As Date = DateAdd(DateInterval.Month, -6, existenceSSS)

            If dateToday < bMonth Then
                BackNextControls(False)
                PrintControls(True)
                authentication = "ACOP04"
                _frmUserAuthentication.getTransacNum()
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, "THE MEMBER HAS ALREADY SUBMITTED AN APPLICATION.")
                ShowPanelForm(_frmUserAuthentication)
            Else
                Acop1()
            End If
        Else
            Acop1()
            ' ELSE HERE
        End If
    End Sub

    Private Sub Acop1()
        xtd.getRawFile()
        tagPage = "6"
        Dim chkType As String = xtd.checkFileType
        Dim result As String
        Dim tempSS As String = ""
        If chkType = 1 Then
            tempSS = xtd.getCRN
        ElseIf chkType = 2 Then
            tempSS = SSStempFile
        End If


        'temp disabled for grace testing 01/26/2017
        If db.checkExistence("SELECT ssnum,DATEADD(month,-6, NXTSUBM) from SSTRANSACOP where ssnum ='" & tempSS & "' AND  GETDATE() < DATEADD(month, -6, NXTSUBM) ") = True Then
            'If db.checkExistence("SELECT ssnum,DATEADD(month,-6, NXTSUBM) from SSTRANSACOP where ssnum ='123456789123456789' AND  GETDATE() < DATEADD(month, -6, NXTSUBM) ") = True Then
            BackNextControls(False)
            PrintControls(True)
            authentication = "ACOP04"
            _frmUserAuthentication.getTransacNum()
            _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
            SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, "THE MEMBER HAS ALREADY SUBMITTED ACOP COMPLIANCE.")
            ShowPanelForm(_frmUserAuthentication)
        Else
            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("in_ssnum", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
            dbComm.Parameters("in_ssnum").Value = tempSS
            dbComm.CommandText = "PR_IK_ACOP"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()
            Dim acopReturn As String = dbComm.Parameters("MSG").Value.ToString
            acopReturn = acopReturn.Trim

            Dim _split3 As String() = acopReturn.Split(New Char() {"|"c})

            For J = 0 To _split3.Length - 1
                acopStr2 = _split3(J)

                If acopStr2.Contains("Reference") Then
                    acopStr2 = acopStr2.Trim
                    refACOP = acopStr2.Remove(0, 15)
                    'refACOP = acopStr2.Remove(0, 35)
                ElseIf acopStr2.Contains("SSNUM") Then
                    acopStr2 = acopStr2.Trim
                    sssACOP = acopStr2.Remove(0, 7)
                ElseIf acopStr2.Contains("Message") Then
                    acopStr2 = acopStr2.Trim
                    messageAcop = acopStr2.Remove(0, 9)
                Else
                    acopStr2 = acopStr2.Trim
                    emptyAcop = acopStr2
                End If

            Next

            'acopReturn = "null"
            If acopReturn.Contains("We regret that you cannot proceed with ACOP due to posted contributions after retirement date. Please request assistance of Member Service Representative at frontline Service Counter, otherwise, your pension will be suspended.") Then
                BackNextControls(False)
                PrintControls(True)
                authentication = "ACOP07"
                _frmUserAuthentication.getTransacNum()
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn)
                ShowPanelForm(_frmUserAuthentication)
            ElseIf acopReturn = "null" Or acopReturn.Contains("The member has successfully confirmed as a pensioner.") Or emptyAcop = "" Or emptyAcop = "null" Or emptyAcop = Nothing Then

                Dim depStr As String = xtd.getDependents(tempSS)
                Dim _split As String() = depStr.Split(New Char() {"*"c})

                If depStr = "" Or depStr = Nothing Then

                Else

                    For i = 0 To _split.Length - 1
                        acopStr = _split(i)
                        Dim _split2 As String() = acopStr.Split(New Char() {"|"c})

                        For k = 0 To _split2.Length - 1
                            acopStr = _split2(k)

                            If acopStr.Contains("SSNUM") Then
                                acopStr = acopStr.Trim
                                sssACOP = acopStr.Remove(0, 7)
                            ElseIf acopStr.Contains("Dependent") Then
                                acopStr = acopStr.Trim
                                depAcop = acopStr.Remove(0, 11)
                            ElseIf acopStr.Contains("Reference") Then
                                acopStr = acopStr.Trim
                                refACOP = acopStr.Remove(0, 15)
                            ElseIf acopStr.Contains("Relationship") Then
                                acopStr = acopStr.Trim
                                rcodeAcop = acopStr.Remove(0, 19)
                            End If

                        Next

                        If acopStr = "" Or acopStr = Nothing Then

                        Else

                            If db.checkExistence("select name from TEMPSSSDEPD where ssnum = '" & tempSS & "' and name = '" & depAcop & "'") = True Then
                                db.ExecuteSQLQuery("update TEMPSSSDEPD set name = '" & depAcop & "', ssnum = '" & sssACOP & "', rcode = '" & rcodeAcop &
                                                   "', REFNO = '" & refACOP & "' WHERE SSNUM = '" & sssACOP & "' AND NAME = '" & depAcop & "'")
                            Else
                                db.sql = "INSERT INTO TEMPSSSDEPD (SSNUM,NAME,RCODE,REFNO) values ('" & sssACOP &
                           "','" & depAcop & "', '" & rcodeAcop & "' ,'" & refACOP & "' )"
                                db.ExecuteSQLQuery(db.sql)

                            End If
                        End If
                    Next
                End If

                result = db.putSingleValue("select name from TEMPSSSDEPD where ssnum = '" & tempSS & "'")

                If result = "" Or result = Nothing Then
                    BackNextControls(False)
                    PrintControls(False)
                    _frmACOPconfirmationDependent.lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                    _frmACOPconfirmationDependent.lblBranch.Text = kioskBranch
                    _frmACOPconfirmationDependent.lblTerminalNo.Text = kioskID
                    _frmACOPconfirmationDependent.Label4.Visible = False
                    _frmACOPconfirmationDependent.Label5.Visible = False
                    _frmACOPconfirmationDependent.lblReferenceNo.Visible = False

                    If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                        _frmACOPconfirmationDependent.Panel4.Size = New Size(883, 252)
                    End If

                    ShowPanelForm(_frmACOPconfirmationDependent)
                Else

                    GC.Collect()
                    printTag = printZero
                    PrintControls(False)
                    BackNextControls(False)

                    '_frmACOPconfirmationDependent.lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                    '_frmACOPconfirmationDependent.lblBranch.Text = kioskBranch
                    '_frmACOPconfirmationDependent.lblTerminalNo.Text = kioskID
                    '_frmACOPconfirmationDependent.Label4.Visible = False
                    '_frmACOPconfirmationDependent.Label5.Visible = False
                    '_frmACOPconfirmationDependent.lblReferenceNo.Visible = False
                    'ShowPanelForm(_frmACOPconfirmationDependent)

                    If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                        _frmACOPdependent.Label6.Top -= 10
                        _frmACOPdependent.Label1.Top -= 17
                        _frmACOPdependent.Label1.Font = New Font("Verdana", 10, FontStyle.Bold)
                    End If

                    ShowPanelForm(_frmACOPdependent)
                End If

                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", "", "")

            ElseIf acopReturn.Contains("The member has already availed a final claim.") Then
                authentication = "ACOP05"
                BackNextControls(False)
                PrintControls(True)
                _frmUserAuthentication.getTransacNum()
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn)
                ShowPanelForm(_frmUserAuthentication)
            ElseIf acopReturn.Contains("The member has already availed ACOP.") Then
                authentication = "ACOP06"
                BackNextControls(False)
                PrintControls(True)
                _frmUserAuthentication.getTransacNum()
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn.Replace(vbNewLine, " "))
                ShowPanelForm(_frmUserAuthentication)
            ElseIf acopReturn.Contains("We regret that you cannot proceed with ACOP due to posted contributions after retirement date. Please request assistance of Member Service Representative at frontline Service Counter, otherwise, your pension will be suspended.") Then
                authentication = "ACOP01"
                _frmUserAuthentication.getTransacNum()
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                BackNextControls(False)
                PrintControls(True)
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn.Replace(vbNewLine, " "))
                ShowPanelForm(_frmUserAuthentication)

            ElseIf acopReturn.Contains("The member need to confirm before 6 months of your month of birthday.") Then
                authentication = "ACOP03"
                _frmUserAuthentication.getTransacNum()
                BackNextControls(False)
                PrintControls(True)
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn)
                ShowPanelForm(_frmUserAuthentication)
            Else
                authentication = "MN01"
                _frmUserAuthentication.getTransacNum()
                errorTag = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                BackNextControls(False)
                PrintControls(True)
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10029", _frmUserAuthentication.lblTransactionNo.Text, acopReturn.Replace(vbNewLine, " "))
                ShowPanelForm(_frmUserAuthentication)
            End If
        End If

    End Sub

#End Region

    Public Sub PensionMaintenanceSUb()

        Try

            'If My.Settings.errorLoadTag = 1 Then

            '    MsgBox("SORRY, THIS TERMINAL IS CURRENTLY OFFLINE. PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
            'Else

            _frm2.Dispose()
            _frm2.Close()


            _frmPensionDetails.Dispose()
            _frmPensionMaintenance.Dispose()
            _frmPensionSummary.Dispose()

            transTag = "PM"

            GC.Collect()
            'Control.CheckForIllegalCrossThreadCalls = False
            'trd = New Thread(AddressOf ThreadTask)
            'trd.IsBackground = True
            'trd.Start()
            tagPage = "7"
            _frmWebBrowser.lblDisclaimer.Visible = False
            '_frmUserAuthentication.Dispose()
            DisposeForm(_frmUserAuthentication)
            xtd.getRawFile()
            printTag = printZero

            'tagPage = "6"

            BackNextControls(False)
            'Button5.Enabled = False
            'Button6.Enabled = False
            'Button5.Text = "BACK"
            'Button6.Text = "NEXT"
            ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
            PrintControls(False)
            'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
            'Button5.Image = Image.FromFile(Application.StartupPath & "\images\DOWN.png")
            'Button6.Image = Image.FromFile(Application.StartupPath & "\images\UPBUTTON.png")
            'If userType = "Pensioner" And getAge <= "65" Then
            'ssit will automatically check if pensioner has any posted contribution from date of retirement up to current date
            'End If
            Dim result As Integer = xtd.checkFileType
            Dim resultSSS As String = ""
            If result = 1 Then
                'xtd.getPensionDetails(xtd.getCRN)
                resultSSS = xtd.getCRN
            ElseIf result = 2 Then
                'xtd.getPensionDetails(SSStempFile)
                resultSSS = SSStempFile
            End If

            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            'Dim dbConn As New OracleConnection
            Dim dbComm As OracleCommand
            ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("SSNUM2", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

            dbComm.Parameters("SSNUM2").Value = resultSSS
            dbComm.CommandText = "PR_IK_PENMAIN"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()

            Dim penMain As String = dbComm.Parameters("MSG").Value.ToString

            If penMain = "" Or penMain = Nothing Then

            Else
                Dim _split As String() = penMain.Split(New Char() {"|"c})

                For i = 0 To _split.Length - 1
                    penMain = _split(i)
                    If penMain.Contains("SSNUM") Then
                        penMain = penMain.Trim
                        sssPen = penMain.Remove(0, 7)
                    ElseIf penMain.Contains("Address") Then
                        penMain = penMain.Trim
                        addressPen1 = penMain.Remove(0, 9)
                    ElseIf penMain.Contains("*") Then
                        penMain = penMain.Trim
                        addresssPen2 = penMain.Remove(0, 1)
                    ElseIf penMain.Contains("Reference") Then
                        penMain = penMain.Trim
                        refPen = penMain.Remove(0, 15)
                    End If

                Next

            End If

            Dim result2 As Integer = xtd.checkFileType
            If result2 = 1 Then
                _frmPensionMaintenance.txtAdd1.Text = addressPen1
                _frmPensionMaintenance.txtAdd3.Text = addresssPen2
            ElseIf result2 = 2 Then
                _frmPensionMaintenance.txtAdd1.Text = xtd.getHouse() & " " & xtd.getstName
                _frmPensionMaintenance.txtAdd3.Text = xtd.getSubd() & " " & xtd.getBarangay
                _frmPensionMaintenance.txtAdd4.Text = xtd.getCity
                _frmPensionMaintenance.Add5.Text = xtd.getPostalCode()
            End If

            splitContainerControl.Panel2.Controls.Clear()

            _frmPensionMaintenance.TopLevel = False
            _frmPensionMaintenance.Parent = Me.splitContainerControl.Panel2
            _frmPensionMaintenance.Dock = DockStyle.Fill
            _frmPensionMaintenance.Show()

            Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                SW.WriteLine(xtd.getCRN & "|" & "10041" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & vbNewLine)
            End Using
            'at.getModuleLogs(xtd.getCRN, "10041", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

            'trd.Abort()
            'End If
        Catch ex As Exception

            'trd.Abort()
            GC.Collect()
            splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = Me.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getdate As String = Date.Today.ToString("ddMMyyyy")


            Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Main Form" & "|" & "Click PENSION button error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try

    End Sub

    Public Sub PensionMaintenanceSUbv2()

        Try
            DisposeForm(_frm2)

            DisposeForm(_frmPensionDetails)
            DisposeForm(_frmPensionMaintenance)
            DisposeForm(_frmPensionSummary)

            transTag = "PM"

            GC.Collect()
            tagPage = "7"
            _frmWebBrowser.lblDisclaimer.Visible = False

            DisposeForm(_frmUserAuthentication)
            xtd.getRawFile()
            printTag = printZero

            'tagPage = "6"

            BackNextControls(False)
            'Button5.Enabled = False
            'Button6.Enabled = False
            'Button5.Text = "BACK"
            'Button6.Text = "NEXT"
            ' Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
            PrintControls(False)
            'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
            'Button5.Image = Image.FromFile(Application.StartupPath & "\images\DOWN.png")
            'Button6.Image = Image.FromFile(Application.StartupPath & "\images\UPBUTTON.png")
            'If userType = "Pensioner" And getAge <= "65" Then
            'ssit will automatically check if pensioner has any posted contribution from date of retirement up to current date
            'End If
            Dim result As Integer = xtd.checkFileType
            Dim resultSSS As String = ""
            If result = 1 Then
                'xtd.getPensionDetails(xtd.getCRN)
                resultSSS = xtd.getCRN
            ElseIf result = 2 Then
                'xtd.getPensionDetails(SSStempFile)
                resultSSS = SSStempFile
            End If

            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            'Dim dbConn As New OracleConnection
            Dim dbComm As OracleCommand
            ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("SSNUM2", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

            dbComm.Parameters("SSNUM2").Value = resultSSS
            dbComm.CommandText = "PR_IK_PENMAIN"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()

            Dim penMain As String = dbComm.Parameters("MSG").Value.ToString

            If penMain = "" Or penMain = Nothing Then

            Else
                Dim _split As String() = penMain.Split(New Char() {"|"c})

                For i = 0 To _split.Length - 1
                    penMain = _split(i)
                    If penMain.Contains("SSNUM") Then
                        penMain = penMain.Trim
                        sssPen = penMain.Remove(0, 7)
                    ElseIf penMain.Contains("Address") Then
                        penMain = penMain.Trim
                        addressPen1 = penMain.Remove(0, 9)
                    ElseIf penMain.Contains("*") Then
                        penMain = penMain.Trim
                        addresssPen2 = penMain.Remove(0, 1)
                    ElseIf penMain.Contains("Reference") Then
                        penMain = penMain.Trim
                        refPen = penMain.Remove(0, 15)
                    End If

                Next

            End If

            Dim result2 As Integer = xtd.checkFileType
            If result2 = 1 Then
                _frmPensionMaintenance.txtAdd1.Text = addressPen1
                _frmPensionMaintenance.txtAdd3.Text = addresssPen2
            ElseIf result2 = 2 Then
                _frmPensionMaintenance.txtAdd1.Text = xtd.getHouse() & " " & xtd.getstName
                _frmPensionMaintenance.txtAdd3.Text = xtd.getSubd() & " " & xtd.getBarangay
                _frmPensionMaintenance.txtAdd4.Text = xtd.getCity
                _frmPensionMaintenance.Add5.Text = xtd.getPostalCode()
            End If

            splitContainerControl.Panel2.Controls.Clear()

            _frmPensionMaintenance.TopLevel = False
            _frmPensionMaintenance.Parent = Me.splitContainerControl.Panel2
            _frmPensionMaintenance.Dock = DockStyle.Fill
            _frmPensionMaintenance.Show()

            Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                SW.WriteLine(xtd.getCRN & "|" & "10041" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & vbNewLine)
            End Using
            'at.getModuleLogs(xtd.getCRN, "10041", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

            'trd.Abort()
            'End If
        Catch ex As Exception

            'trd.Abort()
            GC.Collect()
            splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = Me.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getdate As String = Date.Today.ToString("ddMMyyyy")


            Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Main Form" & "|" & "Click PENSION button error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try

    End Sub

    Private Sub btnPensionerMaintenance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPensionerMaintenance.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then PensionMaintenanceSUbv2()
        Catch ex As Exception
            ShowErrorForm("Pension Maintenance", ex.Message)
        End Try
    End Sub

    Public Sub getAugitLogs()
        xtd.getRawFile()

        ' at.getModuleLogs(xtd.getCRN, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
        Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "")
        End Using
    End Sub

    Public Sub print_cnt(ByVal ssNum As String, ByVal dateToday As String)
        db.sql = "INSERT INTO SSTRANSPRNTCNT (ENCODE_DT,SSNUM)  values ('" & dateToday & "' , '" & ssNum & "')"
        db.ExecuteSQLQuery(db.sql)
    End Sub

    Private Sub btnApremiums_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnApremiums.Click
        _frmWebBrowser.WebBrowser1.Refresh()

        Try
            _frmCalendar.Dispose()
            tagPage = "2.1"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=actualPremiums")
        Catch ex As Exception
            ShowErrorForm("Premiums Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnBClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBClaims.Click
        Try
            tagPage = "2.2"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=benefitClaim")
        Catch ex As Exception
            ShowErrorForm("Benefit Claims Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnEHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEHistory.Click
        Try
            tagPage = "2.3"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=employmentHistory")
        Catch ex As Exception
            ShowErrorForm("Employment History Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnFFund_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFFund.Click
        Try
            tagPage = "2.4"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=flexiFund")
        Catch ex As Exception
            ShowErrorForm("FLEXI FUND Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnlStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlStatus.Click
        Try
            techTag = 0
            tagPage = "2.5"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=loanStatus")
        Catch ex As Exception
            ShowErrorForm("LOAN STATUS Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnMaternityClaim_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaternityClaim.Click
        Try
            tagPage = "2.7"
            BackNextControls(True)
            PrintControls(False)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=Maternity")
        Catch ex As Exception
            ShowErrorForm("MATERNITY CLAIMS Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnMDetails_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMDetails.Click
        btnMDetails_Click()
    End Sub

    Public Sub btnMDetails_Click()

        Try
            tagPage = "2.8"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=memberDetails2")
        Catch ex As Exception
            ShowErrorForm("MEMBER DETAILS Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnSClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSClaims.Click
        Try
            tagPage = "2.9"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sicknessHistory")
        Catch ex As Exception
            ShowErrorForm("SICKNESS CLAIMS Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnClearance_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearance.Click
        Try
            tagPage = "2.10"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sssId")
        Catch ex As Exception
            ShowErrorForm("SSS / UMID CARD Information Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnFLoans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFLoans.Click

        Try
            tagLoanbtn = 0
            techTag = 0
            tagPage = "2.11"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=ddrEligibility")
        Catch ex As Exception
            ShowErrorForm("LOAN ELIGIBILITY Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnLoans_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoans.Click
        Try
            Select Case tagPage

                Case "3"
                    BackNextControls(True)
                    _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=loanComputation")
                    _frmWebBrowser.btnLoanEligibility.Visible = False
                    salPageTag = 1
                Case Else

                    tagPage = "2.12"

                    BackNextControls(True)

                    _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=loanComputation")
                    _frmWebBrowser.btnLoanEligibility.Visible = False
                    salPageTag = 1
            End Select
        Catch ex As Exception
            ShowErrorForm("LOANS Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub btnSickness_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSickness.Click

        Try
            tagPage = "2.13"
            BackNextControls(True)
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sicMatEligibility")
        Catch ex As Exception
            ShowErrorForm("SICKNESS ELIGIBILITY Online Inquiry", ex.Message)
        End Try

    End Sub

    Private Sub btnDocs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDocs.Click
        Try
            pnlMenuWithDeathClaims.Parent = TabControlPanel2
            pnlMenuWithDeathClaims.Dock = DockStyle.None
            pnlMenuWithDeathClaims.Visible = False
            pnlMenuWithDeathClaims.Visible = True
            pnlMenuWithDeathClaims.Dock = DockStyle.Fill

            Panel6.Visible = False
            Panel9.Visible = False
            Panel6.Dock = DockStyle.None
            Panel9.Dock = DockStyle.None
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=checklist")
            PrintControls(False)
        Catch ex As Exception
            ShowErrorForm("CHECKLIST DOCUMENTS Online Inquiry", ex.Message)
        End Try
    End Sub

    Private Sub _frmMainMenu_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        Try
            AddRemoveIEVersion(false)

            printTag = 0
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
            Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")

            'db.sql = "Insert into SSINFOTERMCONSTAT (BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,OFFLINE_DT,OFFLINE_TME,DATESTAMP) values('" & kioskIP & "','" & getbranchCoDE & "','" & getkiosk_cluster & _
            '   "','" & getkiosk_group & "','" & Date.Today.ToShortDateString & "','" & TimeOfDay & "','" & Today & "')"
            'db.ExecuteSQLQuery(db.sql)

            Dim getLastofflinedate As DateTime = Date.Today.ToShortDateString
            Dim getlastofflinetime As DateTime = TimeOfDay
            Dim finaldate As DateTime
            finaldate = getLastofflinedate & " " & getlastofflinetime
            lastOffline = finaldate
            editSettings(xml_Filename, xml_path, "lastOffline", finaldate)


            'db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set STATUS = '" & "False" & "', LOFFLINE_DT = '" & My.Settings.lastOffline & "' where BRANCH_IP = '" _
            '                   & kioskIP & "' and KIOSK_ID = '" & kioskID & "'")

            'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & finaldate & " " & getbranchCoDE & "\" & "KIOSKLogs.txt", True)
            '    SW.WriteLine("ONLINE_DATE: " & My.Settings.lastOnline & "," & "BRANCH_NAME: " & kioskIP & "," & "KIOSK_ID: " & kioskID & "," & "STATUS: " & "OFFLINE" & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
            'End Using



        Catch ex As Exception

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getdate As String = Date.Today.ToString("ddMMyyyy")
            'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
            '    SW.WriteLine("MAIN FORM CLOSED" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
            'End Using

            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Form: Close" & "', '" & "Form Closing or Automatic Close Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)

            Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Close" & "|" & "Click PENSION button error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try
    End Sub


    Private Sub _frmMainMenu_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        Try


            If (e.Alt AndAlso (e.KeyCode = Keys.K)) Then
                ' When Alt + L is pressed, SQL Database Settings
                System.Diagnostics.Process.Start(Application.StartupPath & "\keyboard\" & "VBSoftKeyboard.exe")
                'ElseIf (e.KeyCode = Keys.F4 And e.Modifiers = Keys.Alt) Then
                '    e.Handled = True
            End If

            If e.KeyData = Keys.Alt + Keys.F4 Then
                e.Handled = True
            End If

            If e.KeyCode = 17 Or e.KeyCode = 18 Or e.KeyCode = 46 Then

            End If

        Catch ex As Exception
            ShowErrorForm("MainMenu Keydown", ex.Message)
        End Try
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Try
            If StarIOPrinter.CheckPrinterAvailabilityv2() Then
                Dim result As Integer = MessageBox.Show("DO YOU WANT TO PRINT THE RECEIPT? " & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.No Then
                    If tagPage = "17" Or tagPage = "4" Or tagPage = "5" Or tagPage = "1" Or tagPage = "6" Or tagPage = "7" Or tagPage = "3" Then
                        PrintControls(False)
                        prtRes = 0
                        _frmUserAuthentication.lblGetReceipt.Visible = False
                    Else
                        PrintControls(True)
                        _frmUserAuthentication.lblGetReceipt.Visible = False
                        prtRes = 1
                    End If
                ElseIf result = DialogResult.Yes Then
                    _frmUserAuthentication.lblGetReceipt.Visible = True
                    printBtn()
                End If
            End If
        Catch ex As Exception
            ShowErrorForm("Print", ex.Message)
        End Try
    End Sub

    Private Sub btnSSSWebsite_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSSSWebsite.Click
        printTag = printZero
        Try
            xtd.getRawFile()
            Dim result As Integer = MessageBox.Show("DO YOU WANT TO LOGOUT FROM YOUR CURRENT SESSION?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then

                MsgBox("PLEASE MAKE SURE YOU HAVE REMOVED YOUR CARD." & vbNewLine & vbNewLine & "THANK YOU FOR USING THIS FACILITY.", MsgBoxStyle.Information, "Information")
                disposeForms()
                trd.Abort()

                tagPage = "7"

                editSettings(xml_Filename, xml_path, "CARD_DATA", "")
                editSettings(xml_Filename, xml_path, "errorLoadTag", "0")
                ClearCache2()

                IsMainMenuActive = False
                Me.Hide()
                SharedFunction.ShowMainDefaultUserForm()
                Main.Show()

                Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10026" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "SUCCESSFULLY REMOVED CARD." & vbNewLine)
                End Using
            End If
        Catch ex As Exception
            ShowErrorForm("SSS Website", ex.Message)
        End Try
    End Sub
    Sub EmptyCacheFolder(ByVal folder As DirectoryInfo)

        For Each file As FileInfo In folder.GetFiles()
            Try
                file.Delete()
            Catch ex As Exception

            End Try
        Next

        ' Shell("RunDll32.exe InetCpl.cpl, ClearMyTracksByProcess 8", vbHide)
        'For Each subfolder As DirectoryInfo In folder.GetDirectories()
        '    EmptyCacheFolder(subfolder)
        'Next
    End Sub

    Private IE_Allfiles As ArrayList

    Private Sub ListFiles(ByVal folder As String)
        Try
            Dim dirs() As System.IO.DirectoryInfo = New System.IO.DirectoryInfo(folder).GetDirectories()
            Dim files() As System.IO.FileInfo = New System.IO.DirectoryInfo(folder).GetFiles()

            For Each file As System.IO.FileInfo In files
                IE_Allfiles.Add(file.FullName)
            Next

            For Each dir As System.IO.DirectoryInfo In dirs
                ListFiles(dir.FullName)
            Next
        Catch ex As Exception

        End Try

    End Sub

    Public Function ClearCache2() As Boolean
        'Dim Empty As Boolean
        'Try
        '    EmptyCacheFolder(New DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache)))
        '    Empty = True

        'Catch
        '    Empty = False

        'End Try
        'Return Empty
        '  If ifLogOut = True Then

        ClearCache.InternetSetOption(IntPtr.Zero, ClearCache.INTERNET_OPTION_END_BROWSER_SESSION, IntPtr.Zero, 0)

        IE_Allfiles = New ArrayList()

        ListFiles(Environment.GetFolderPath(Environment.SpecialFolder.InternetCache))

        For Each fname As String In IE_Allfiles
            Try
                Dim f As New System.IO.FileInfo(fname)
                f.Delete()
            Catch ex As Exception
            End Try
        Next

        'End If
        Shell("RunDll32.exe InetCpl.cpl, ClearMyTracksByProcess 8", vbHide)
    End Function

    Private Sub btnLogout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogout.Click
        Try
            Dim result As Integer = MessageBox.Show("DO YOU WANT TO LOGOUT FROM YOUR CURRENT SESSION?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
            ElseIf result = DialogResult.Yes Then
                LogoutUser()
            End If
        Catch ex As Exception
            ShowErrorForm("Logout", ex.Message)
        End Try
    End Sub

    Public Sub LogoutUser()
        MsgBox("PLEASE MAKE SURE YOU HAVE REMOVED YOUR CARD." & vbNewLine & vbNewLine & "THANK YOU FOR USING THIS FACILITY.", MsgBoxStyle.Information, "Information")

        SharedFunction.ZoomFunction(True)

        Dim memberName As String = HTMLDataExtractor.MemberFullName
        disposeForms()
        tagPage = "12"
        Dim pathX1 As String = Application.StartupPath & "\" & "temp" & "\"
        DeleteFile(pathX1)
        myTimer.Enabled = False
        If Not trd Is Nothing Then If trd.IsAlive Then trd.Abort()

        editSettings(xml_Filename, xml_path, "CARD_DATA", "")

        printTag = 0

        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")

        Dim getLastofflinedate As DateTime = Date.Today.ToShortDateString
        Dim getlastofflinetime As DateTime = TimeOfDay
        Dim finaldate As DateTime
        finaldate = getLastofflinedate & " " & getlastofflinetime
        lastOffline = finaldate

        editSettings(xml_Filename, xml_path, "errorLoadTag", "0")

        ClearCache2()

        IsMainMenuActive = False

        'Dim exitSurvey As New _frmExitSurveyIntro()
        'exitSurvey.memberName = memberName
        'exitSurvey.ShowDialog()

        Me.Hide()
        SharedFunction.ShowMainDefaultUserForm()
        Main.Show()
    End Sub

    Public Sub disposeForms()
        DisposeForm(_frmWebBrowser)

        For Each f As Form In My.Application.OpenForms
            If Not f.Name = "Main" Then
            ElseIf Not f.Name = "usrfrmIdle" Then
            ElseIf Not f.Name = "usrfrmSelect" Then
            ElseIf Not f.Name = "usrfrmSelectv2" Then
            ElseIf Not f.Name = "_frmMainMenu" Then
            Else
                f.Dispose()
            End If
        Next
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            GC.Collect()
            xtd.getRawFile()
            Select Case tagPage
                Case "9"
                    _frmSalaryLoanDisclosurev2.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    If getAdd = 0 Then
                        getAdd -= 130
                        _frmSalaryLoanDisclosurev2.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    Else
                        getAdd -= 130
                        _frmSalaryLoanDisclosurev2.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    End If

                Case "1"

                Case "2", "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "2.10", "2.11", "2.12", "2.13", "2.14"
                    _frmWebBrowser.WebBrowser1.GoBack()
                Case "3.1"

                Case "10"
                    _frmWebBrowser.WebBrowser1.GoBack()
                Case "13"
                    _frmUpdCntcInfv2.ScrollDownv2()
                Case Else
                    MsgBox("That is not accessible", MsgBoxStyle.Information)
            End Select

        Catch ex As Exception
            ShowErrorForm("Back", ex.Message)
        End Try
    End Sub

    Private Sub btnChangeUmidPin_Click(sender As Object, e As EventArgs) Handles btnChangeUmidPin.Click
        Try
            CleanFormSession({_frmChangePIN})

            transTag = "PC"
            tagPage = "8"

            PrintControls(False)
            BackNextControls(False)

            If SharedFunction.DisablePinOrFingerprint Then
                ShowPinChangeForm()
            Else
                CurrentTxnType = TxnType.PinChange
                xtd.getRawFile()
                _frm2.CardType = CShort(xtd.checkFileType)
                ShowPanelForm(_frm2)
            End If
        Catch ex As Exception
            ShowErrorForm("PIN CHANGE", ex.Message)
        End Try
    End Sub

    Public Sub ShowPinChangeForm()
        SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10048", "", "Pin change")
        ShowPanelForm(_frmChangePIN)
    End Sub

    Public Sub ShowPanelForm(ByVal frmInput As Form)
        GC.Collect()
        splitContainerControl.Panel2.Controls.Clear()
        frmInput.TopLevel = False
        frmInput.Parent = splitContainerControl.Panel2
        frmInput.Dock = DockStyle.Fill
        frmInput.Show()
    End Sub

    Private Sub btnMonthlyPension_Click(sender As Object, e As EventArgs) Handles btnMonthlyPension.Click
        Try
            CleanFormSession()

            transTag = "MPPH"

            xtd.getRawFile()
            tagPage = "20"

            PrintControls(False)
            BackNextControls(False)

            SharedFunction.SaveToTransactionLog(Path1, xtd.getCRN, "10049", "", "")
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=paymentHistory")
        Catch ex As Exception
            ShowErrorForm("Monthly Pension Payment History", ex.Message)
        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            Select Case tagPage

                Case "9"
                    If getAdd = 0 Then
                        getAdd += 130
                        _frmSalaryLoanDisclosurev2.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    Else
                        getAdd += 130
                        _frmSalaryLoanDisclosurev2.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    End If
                Case "1" 'SIMPLIFIED REGISTRATION

                Case "2", "2.1", "2.2", "2.3", "2.4", "2.5", "2.6", "2.7", "2.8", "2.9", "2.10", "2.11", "2.12", "2.13", "2.14"
                    _frmWebBrowser.WebBrowser1.GoForward()
                Case "3.1" 'SALARY LOAN
                    Dim salaryLoanTag As String = readSettings(xml_Filename, xml_path, "salaryLoanTag")
                    Select Case salaryLoanTag

                        Case 0 ' not employer

                        Case 1 ' employer

                        Case Else

                    End Select


                Case "4" 'MATERNITY NOTIFICATION

                Case "8"
                    Try
                        xtd.getRawFile()
                        'at.getModuleLogs(xtd.getCRN, "10024", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                        Using SW As New IO.StreamWriter(Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10024" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "SUCCESSFULLY REMOVED CARD." & vbNewLine)
                        End Using
                    Catch ex As Exception
                        ShowErrorForm("Next", ex.Message)
                    End Try
                Case "10"
                    _frmWebBrowser.WebBrowser1.GoBack()
                Case "13"
                    _frmUpdCntcInfv2.ScrollUpv2()
                Case Else
                    '  nikki01
                    MsgBox("That is not accessible", MsgBoxStyle.Information)
            End Select
        Catch ex As Exception
            ShowErrorForm("Next", ex.Message)
        End Try
    End Sub


    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        Panel9.Parent = TabControlPanel2
        Panel6.Visible = False
        Panel6.Dock = DockStyle.None
        Panel9.Visible = True
        Panel9.Dock = DockStyle.Fill
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        Panel6.Parent = TabControlPanel2
        Panel9.Dock = DockStyle.None
        Panel9.Visible = False
        Panel6.Visible = True
        Panel6.Dock = DockStyle.Fill
    End Sub

    Private Sub btnKeyboard_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnKeyboard.Click
        ShowVirtualKeyboard()
    End Sub

    Public Sub ShowVirtualKeyboard(Optional ByVal parentForm As Short = 0)
        Try
            Select Case parentForm
                Case 0
                    If splitContainerControl.Panel2.Controls.Count = 0 Then
                        ShowKeyboard()
                    Else
                        Select Case splitContainerControl.Panel2.Controls(0).Name
                            Case _frmEnhanceMaternityNotif.Name, _frmPensionMaintenance.Name, _frmPRNApplication.Name, _frmSWR2v2.Name, _frmSWR2.Name,
                                 _frmUpdCntcInfv2.Name, _frmFeedbackWebsite.Name, _frmFeedbackWebsite3.Name, _frmFeedbackWebsite4.Name,
                                 _frmFeedbackKiosk.Name, _frmFeedbackKiosk2.Name, _frmSSSwebsite.Name, _frmSWR1.Name
                                ShowKeyboard()
                            Case Else
                                SharedFunction.ShowInfoMessage("VIRTUAL KEYBOARD IS NOT REQUIRED.")
                        End Select
                    End If
                Case 1
                    ShowKeyboard()
            End Select
        Catch ex As Exception
            SharedFunction.ShowErrorMessage("FAILED TO LOAD VIRTUAL KEYBOARD. " & vbNewLine & vbNewLine & ex.Message.ToUpper)
        End Try
    End Sub

    Private Sub ShowKeyboard()
        If osk Is Nothing Then
            osk = New _frmVirtualKeyboard()
            osk.Left = (ClientSize.Width - (osk.Width + 30)) / 2
            osk.Top = (ClientSize.Height - osk.Height) - 5
            osk.Show()
        Else
            osk.Show()
        End If
    End Sub

    Public Sub ShowVirtualKeyboardWithControlFocus(ByRef ctrl As Control)
        Try
            ShowKeyboard()
        Catch ex As Exception
            SharedFunction.ShowErrorMessage("FAILED TO LOAD VIRTUAL KEYBOARD. " & vbNewLine & vbNewLine & ex.Message.ToUpper)
        End Try
    End Sub

    'Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
    '    runTime()
    'End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        Panel6.Dock = DockStyle.None
        pnlMenuWithDeathClaims.Dock = DockStyle.None
        pnlMenuWithDeathClaims.Visible = False
        Panel9.Parent = TabControlPanel2
        Panel9.Visible = True
        Panel6.Visible = False
        Panel9.Dock = DockStyle.Fill
    End Sub

    Private Sub lblDate_Click(sender As Object, e As EventArgs) Handles lblDate.Click
        Dim b As Bitmap = TakeScreenShot()
        Dim destiFolder As String = "D:\AUTO_SCREENSHOT"
        If Not Directory.Exists(destiFolder) Then Directory.CreateDirectory(destiFolder)
        b.Save(String.Format("{0}\SS_{1}.jpg", destiFolder, Now.ToString("yyyyMMdd_hhmmss")), Imaging.ImageFormat.Jpeg)
        'MessageBox.Show("Done!")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Dim s As New StringBuilder
        'For Each c As Control In Panel19.Controls
        '    s.AppendLine(c.Name & vbNewLine)
        'Next
        'MessageBox.Show(s.ToString)
        'ShapeContainer1.Left -= 10
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles pnlWebContactInfo.Paint

    End Sub

    Private Sub splitContainerControl_Panel2_Paint(sender As Object, e As PaintEventArgs) Handles splitContainerControl.Panel2.Paint

    End Sub

    Private Function TakeScreenShot() As Bitmap

        Dim screenSize As Size = New Size(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)

        Dim screenGrab As New Bitmap(My.Computer.Screen.Bounds.Width, My.Computer.Screen.Bounds.Height)

        Dim g As Graphics = Graphics.FromImage(screenGrab)

        g.CopyFromScreen(New Point(0, 0), New Point(0, 0), screenSize)

        Return screenGrab

    End Function

    Private Sub btnDisabiltyClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDisabiltyClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listDisability")
        Catch ex As Exception
            ShowErrorForm("DISABILITY ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub btnEC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEC.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listEcmedreimburse")
        Catch ex As Exception
            ShowErrorForm("EC ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub btnFuneralClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFuneralClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listFuneral")
        Catch ex As Exception
            ShowErrorForm("DDR FUNERAL ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub btnMaternityClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMaternityClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listMaternity")
        Catch ex As Exception
            ShowErrorForm("MATERNITY CLAIMS ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub btnRetireClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRetireClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listRetirement")
        Catch ex As Exception
            ShowErrorForm("RETIREMENT CLAIMS ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub btnSicknessClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSicknessClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listSickness")
        Catch ex As Exception
            ShowErrorForm("SICKNESS CLAIMS ONLINE INQUIRY BUTTON", ex.Message)
        End Try
    End Sub

    Private Sub btnDeathClaims_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDeathClaims.Click
        Try
            _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=listDeath")
        Catch ex As Exception
            ShowErrorForm("DEATH CLAIMS ONLINE INQUIRY", ex.Message)
        End Try
    End Sub

    Private Sub validateEmail(ByVal mail As String)
        Dim email As New System.Text.RegularExpressions.Regex("\S+@\S+\.")
        If email.IsMatch(mail) Then
            rtn = 1
        Else
            rtn = 0
        End If

    End Sub

#Region " PRINT FUNCTION "

    Public Sub printBtn()
        If tagPage = "17" Then
            ' If webPageTag = "Loan Eligibility" And tagPage = "10" Then
            tagPage = "17"
            'ElseIf _frmWebBrowser.btnLoanEligibility.Visible = True Then
            '    tagPage = "17"
            'ElseIf Not transTag = "LG" Then
            '    tagPage = "17"
            'Else
            '    tagPage = "10"
            'End If

            'ElseIf tagPage = "4" Then
            '    tagPage = "4"
        ElseIf tagPage = "1" Then
        Else
            tagPage = "10"
        End If

        _frmUserAuthentication.lblGetReceipt.Visible = True

        ' ---------- 1=Registration 2=Online Inquiry 3=Salary Loan 4=Maternity Notification 5=Technical Retirement --------
        For Each procT In System.Diagnostics.Process.GetProcessesByName("AcroRd32")
            procT.Kill()
        Next

        Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
        Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
        Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
        Dim fullnameprint As String = lname & " " & fname & " " & mname

        Dim dateBirth As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
        ' My.Settings.dateOfBirth = dateBirth
        editSettings(xml_Filename, xml_path, "dateOfBirth", dateBirth)
        Dim dateCoverage As String = printF.GetDateCoverage(_frmWebBrowser.WebBrowser1)
        'My.Settings.dateOfCov = dateCoverage
        editSettings(xml_Filename, xml_path, "dateOfCov", dateCoverage)

        Select Case tagPage
            Case "1"
                If IsAllowedToPrint() Then
                    Dim class1 As New PrintHelper
                    Dim receiptMsg As New System.Text.StringBuilder
                    receiptMsg.Append("YOU ARE ALREADY REGISTERED TO THE SSS WEBSITE" & vbNewLine & vbNewLine)
                    receiptMsg.Append("FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine)
                    receiptMsg.Append("MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine)
                    receiptMsg.Append("AT THE SSS BRANCH.")

                    class1.prt(class1.prtSimplifiedWebRegistration_Receipt(fullnameprint, SSStempFile, receiptMsg.ToString, "SIMPLIFIED WEB REGISTRATION", "Print"), _frmMainMenu.DefaultPrinterName)
                    class1 = Nothing

                    print_cnt(getCRN_SSSNum, Date.Today.ToShortDateString)
                End If
            Case "10"

                If IsAllowedToPrint() Then
                    'Dim fileTYPerr2 As Integer = xtd.checkFileType
                    'If fileTYPerr2 = 1 Then
                    '    print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                    'ElseIf fileTYPerr2 = 2 Then
                    '    print_cnt(SSStempFile, Date.Today.ToShortDateString)
                    'End If

                    print_cnt(getCRN_SSSNum, Date.Today.ToShortDateString)

                    ' Dim webPageTag As String = readSettings(xml_Filename, xml_path, "webPageTag")


                    Select Case webPageTag

                        Case "Employee Static Information"
                            Try
                                DisposeForm(_frmPrintForm)
                                GC.Collect()
                                Dim memberDetails As String = printF.GetSSNumberStatus(_frmWebBrowser.WebBrowser1)

                                Dim bdate As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)

                                Dim recordLocation As String = printF.GetRecordLocation(_frmWebBrowser.WebBrowser1)

                                Dim dateOfCoverage As String = printF.GetDateCoverage(_frmWebBrowser.WebBrowser1)

                                Dim chkType As String = xtd.checkFileType
                                If chkType = 1 Then

                                    class1.prt(class1.prtMemDetails(fullnameprint, xtd.getCRN, memberDetails, bdate, dateOfCoverage, recordLocation, webPageTag, "Print"), DefaultPrinterName)
                                ElseIf chkType = 2 Then

                                    class1.prt(class1.prtMemDetails(xtd.getFullname, SSStempFile, memberDetails, bdate, dateOfCoverage, recordLocation, webPageTag, "Print"), DefaultPrinterName)
                                End If

                                printTag = 1

                                getAffectedTable = "10009"
                                getAugitLogs()

                            Catch ex As Exception

                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT OUR FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                Dim errorLogs As String = ex.Message
                                errorLogs = errorLogs.Trim

                                Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
                                '    SW.WriteLine("ESI PRINT BUTTON" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                                'End Using

                                'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
                                '    & "','" & "Form: Print Form" & "', '" & "Print out of Member Details Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
                                'db.ExecuteSQLQuery(db.sql)
                                Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                                    SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                                       & "|" & "Form: Print Form" & "|" & "Print out of Member Details Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
                                End Using
                            End Try

                        Case "Contributions - Actual Premiums"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)

                                Dim totalNoContribution As Integer = printF.GetNumbOfContribution(_frmWebBrowser.WebBrowser1)
                                Dim totalAmountContribution As String = printF.GetTotalAmountContribution(_frmWebBrowser.WebBrowser1)

                                Dim fileTYP As Integer = xtd.checkFileType
                                If fileTYP = 1 Then
                                    class1.prt(class1.prtActualPremium(fullnameprint, xtd.getCRN, dateBirth, dateCoverage, totalNoContribution, totalAmountContribution, webPageTag, "Print"), DefaultPrinterName)

                                ElseIf fileTYP = 2 Then
                                    class1.prt(class1.prtActualPremium(xtd.getFullname, SSStempFile, dateBirth, dateCoverage, totalNoContribution, totalAmountContribution, webPageTag, "Print"), DefaultPrinterName)

                                End If

                                printTag = 1

                                getAffectedTable = "10003"
                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                Dim errorLogs As String = ex.Message
                                errorLogs = errorLogs.Trim

                                Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
                                '    SW.WriteLine("CAP PRINT BUTTON" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                                'End Using
                                'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
                                '    & "','" & "Form: Print Form" & "', '" & "Print out of Actual Premiums Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
                                'db.ExecuteSQLQuery(db.sql)
                                Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                                    SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                                       & "|" & "Form: Print Form" & "|" & "Print out of Actual Premiums Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
                                End Using
                            End Try
                        Case "Benefit Claim"
                            Try
                                DisposeForm(_frmPrintForm)
                                GC.Collect()

                                Dim GetBenClaimsChecking1 As String = printF.GetBenClaimsChecking1(_frmWebBrowser.WebBrowser1)
                                Dim GetBenClaimsChecking2 As String = printF.GetBenClaimsChecking2(_frmWebBrowser.WebBrowser1)

                                If GetBenClaimsChecking1 = False And GetBenClaimsChecking2 = False Then
                                    Dim fileTYP As Integer = xtd.checkFileType
                                    Dim eligibstat As String = printF.GetEligibStatus(_frmWebBrowser.WebBrowser1)
                                    eligibstat = eligibstat.Replace("&nbsp;", "")
                                    If fileTYP = 1 Then
                                        class1.prt(class1.prtBenClaimNoReturn(fullnameprint, xtd.getCRN, eligibstat, webPageTag, "Print"), DefaultPrinterName)
                                    ElseIf fileTYP = 2 Then
                                        class1.prt(class1.prtBenClaimNoReturn(fullnameprint, SSStempFile, eligibstat, webPageTag, "Print"), DefaultPrinterName)
                                    End If
                                ElseIf GetBenClaimsChecking1 = True And GetBenClaimsChecking2 = False Then

                                    Dim GetBenClaims As String = printF.GetBenClaims(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenDateCon As String = printF.GetBenDateCon(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenStatus As String = printF.GetBenStatus(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType
                                    If fileTYP = 1 Then
                                        class1.prt(class1.prtBenClaim(fullnameprint, xtd.getCRN, GetBenClaims, GetBenDateCon, GetBenStatus, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        class1.prt(class1.prtBenClaim(fullnameprint, SSStempFile, GetBenClaims, GetBenDateCon, GetBenStatus, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                ElseIf GetBenClaimsChecking1 = False And GetBenClaimsChecking2 = True Then
                                    Dim GetBenClaimsClaimFiled As String = printF.GetBenClaimsClaimFiled(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsDateFiled As String = printF.GetBenClaimsDateFiled(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsFiledAt As String = printF.GetBenClaimsFiledAt(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsStatusAsOf As String = printF.GetBenClaimsStatusAsOf(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType
                                    If fileTYP = 1 Then
                                        class1.prt(class1.prtBenClaimApplication(fullnameprint, xtd.getCRN, GetBenClaimsClaimFiled, GetBenClaimsDateFiled, GetBenClaimsFiledAt, GetBenClaimsStatusAsOf, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        class1.prt(class1.prtBenClaimApplication(fullnameprint, SSStempFile, GetBenClaimsClaimFiled, GetBenClaimsDateFiled, GetBenClaimsFiledAt, GetBenClaimsStatusAsOf, webPageTag, "Print"), DefaultPrinterName)

                                    End If
                                ElseIf GetBenClaimsChecking1 = True And GetBenClaimsChecking2 = True Then

                                    Dim GetBenClaimsClaimFiled As String = printF.GetBenClaimsClaimFiled(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsDateFiled As String = printF.GetBenClaimsDateFiled(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsFiledAt As String = printF.GetBenClaimsFiledAt(_frmWebBrowser.WebBrowser1)
                                    Dim GetBenClaimsStatusAsOf As String = printF.GetBenClaimsStatusAsOf(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType
                                    If fileTYP = 1 Then
                                        class1.prt(class1.prtBenClaimApplication(fullnameprint, xtd.getCRN, GetBenClaimsClaimFiled, GetBenClaimsDateFiled, GetBenClaimsFiledAt, GetBenClaimsStatusAsOf, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        class1.prt(class1.prtBenClaimApplication(fullnameprint, SSStempFile, GetBenClaimsClaimFiled, GetBenClaimsDateFiled, GetBenClaimsFiledAt, GetBenClaimsStatusAsOf, webPageTag, "Print"), DefaultPrinterName)

                                    End If
                                Else
                                    MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                End If

                                printTag = 1

                                getAffectedTable = "10004"

                                getAugitLogs()


                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                Dim errorLogs As String = ex.Message
                                errorLogs = errorLogs.Trim

                                Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
                                '    SW.WriteLine("BC PRINT BUTTON" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                                'End Using
                                'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
                                '    & "','" & "Form: Print Form" & "', '" & "Print out of Actual Premiums Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
                                'db.ExecuteSQLQuery(db.sql)
                                Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                                    SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                                       & "|" & "Form: Print Form" & "|" & "Print out of Actual Premiums Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
                                End Using
                            End Try
                        Case "Benefit Claim Information"
                            Try
                                DisposeForm(_frmPrintForm)
                                GC.Collect()
                                Dim getBenDate As String
                                Dim getBenDateStr As String

                                Dim GetBenClaimsDOC As String = printF.GetBenClaimsDOC(_frmWebBrowser.WebBrowser1)
                                Dim GetBenClaimsStat As String = printF.GetBenClaimsStat(_frmWebBrowser.WebBrowser1)
                                Dim GetBenSetDate As String = printF.GetBenSetDate(_frmWebBrowser.WebBrowser1)
                                Dim GetBenClaimWithDate As String = printF.GetBenClaimWithDate(_frmWebBrowser.WebBrowser1)
                                Dim GetBenClaimPenDate As String = printF.GetBenClaimPenDate(_frmWebBrowser.WebBrowser1)
                                getBenDate = GetBenClaimWithDate
                                getBenDateStr = "1"
                                If GetBenClaimWithDate = "" Or GetBenClaimWithDate = Nothing Then
                                    getBenDateStr = "2"
                                End If
                                Dim GetBenClaimAmtIB As String = printF.GetBenClaimAmtIB(_frmWebBrowser.WebBrowser1)
                                Dim GetBenClaimTotMPen As String = printF.GetBenClaimTotMPen(_frmWebBrowser.WebBrowser1)
                                Dim fileTYP As Integer = xtd.checkFileType
                                If fileTYP = 1 Then

                                    class1.prt(class1.prtBenClaimInformation(fullnameprint, xtd.getCRN, GetBenClaimsDOC, GetBenClaimsStat, GetBenSetDate, getBenDateStr, GetBenClaimWithDate, GetBenClaimPenDate, GetBenClaimAmtIB, GetBenClaimTotMPen, webPageTag, "Print"), DefaultPrinterName)

                                ElseIf fileTYP = 2 Then

                                    class1.prt(class1.prtBenClaimInformation(fullnameprint, SSStempFile, GetBenClaimsDOC, GetBenClaimsStat, GetBenSetDate, getBenDateStr, GetBenClaimWithDate, GetBenClaimPenDate, GetBenClaimAmtIB, GetBenClaimTotMPen, webPageTag, "Print"), DefaultPrinterName)

                                End If

                                printTag = 1

                                getAffectedTable = "10004"

                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                Dim errorLogs As String = ex.Message
                                errorLogs = errorLogs.Trim

                                Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
                                '    SW.WriteLine("BCI PRINT BUTTON" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                                'End Using
                                'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
                                '    & "','" & "Form: Print Form" & "', '" & "Print out of Actual Premiums Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
                                'db.ExecuteSQLQuery(db.sql)
                                Using SW As New IO.StreamWriter(Path1 & "\" & "InfoTerminal_logs.txt", True)
                                    SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                                       & "|" & "Form: Print Form" & "|" & "Print out of Actual Premiums Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
                                End Using

                            End Try

                        Case "Employment History"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)

                                Dim employerID As String = printF.GetEmployerID(_frmWebBrowser.WebBrowser1)
                                Dim employerName As String = printF.GetEmployerName(_frmWebBrowser.WebBrowser1)
                                Dim reportingDate As String = printF.GetReportingDate(_frmWebBrowser.WebBrowser1)
                                Dim employmentDate As String = printF.getEmploymentDate(_frmWebBrowser.WebBrowser1)

                                Dim fileTYP As Integer = xtd.checkFileType
                                Dim msg1 As String = printF.getMsgEmployment(_frmWebBrowser.WebBrowser1)
                                msg1 = "MEMBER HAS NO EMPLOYMENT RECORD AS OF DATE."
                                If fileTYP = 1 Then
                                    If employerID = Nothing And employerName = Nothing And reportingDate = Nothing And reportingDate = Nothing And employmentDate = Nothing Then
                                        class1.prt(class1.prtNOEmpHistory(fullnameprint, xtd.getCRN, msg1, webPageTag, "Print"), DefaultPrinterName)
                                    Else
                                        class1.prt(class1.prtEmpHistory(fullnameprint, SSStempFile, employerID, employerName, reportingDate, employmentDate, webPageTag, "Print"), DefaultPrinterName)
                                    End If

                                ElseIf fileTYP = 2 Then
                                    If employerID = Nothing And employerName = Nothing And reportingDate = Nothing And reportingDate = Nothing And employmentDate = Nothing Then

                                        class1.prt(class1.prtNOEmpHistory(xtd.getFullname, SSStempFile, msg1, webPageTag, "Print"), DefaultPrinterName)
                                    Else

                                        class1.prt(class1.prtEmpHistory(xtd.getFullname, SSStempFile, employerID, employerName, reportingDate, employmentDate, webPageTag, "Print"), DefaultPrinterName)
                                    End If


                                End If

                                printTag = 1

                                getAffectedTable = "10005"

                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Employment History", ex.Message)
                            End Try
                        Case "Flexi-Fund Subsidiary Ledger"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)

                                Dim getMsgFFErr As Boolean = printF.getMsgFFErr(_frmWebBrowser.WebBrowser1)
                                If getMsgFFErr = True Then

                                    Dim resultMsg As String = printF.getMsg(_frmWebBrowser.WebBrowser1)
                                    resultMsg = resultMsg.Replace("&nbsp;", "")
                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then

                                        class1.prt(class1.prtFlexiFundEmpty(fullnameprint, xtd.getCRN, resultMsg, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then

                                        class1.prt(class1.prtFlexiFundEmpty(xtd.getFullname, SSStempFile, resultMsg, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                    printTag = 1

                                    getAffectedTable = "10006"

                                    getAugitLogs()
                                Else
                                    Dim enrollDate As String = printF.getEnrollDate(_frmWebBrowser.WebBrowser1)
                                    Dim memberSince As String = printF.getMemberSince(_frmWebBrowser.WebBrowser1)
                                    Dim totalContribution As String = printF.getTotalContribution(_frmWebBrowser.WebBrowser1)
                                    totalContribution = totalContribution.Trim

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then

                                        class1.prt(class1.prtFlexiFund(fullnameprint, xtd.getCRN, enrollDate, memberSince, totalContribution, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then

                                        class1.prt(class1.prtFlexiFund(xtd.getFullname, SSStempFile, enrollDate, memberSince, totalContribution, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                    printTag = 1

                                    getAffectedTable = "10006"

                                    getAugitLogs()
                                End If
                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Flexi-Fund", ex.Message)
                            End Try
                        Case "Loan Status"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)


                                Dim checkCreditLoan As Boolean = printF.NoLoanStat(_frmWebBrowser.WebBrowser1)
                                Dim NoCreditLoan As Boolean = printF.NoCreditLoan(_frmWebBrowser.WebBrowser1)

                                If checkCreditLoan = True And NoCreditLoan = True Then
                                    'version 1

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        class1.prt(class1.prtLoanStatusError(fullnameprint, xtd.getCRN, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        class1.prt(class1.prtLoanStatusError(xtd.getFullname, SSStempFile, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                ElseIf checkCreditLoan = True And NoCreditLoan = False Then
                                    Dim goToLoanType As String = printF.goToLoanType(_frmWebBrowser.WebBrowser1)
                                    Dim goToLoanDate As String = printF.goToLoanDate(_frmWebBrowser.WebBrowser1)
                                    'Dim goToVoucherNumber As String = printF.goToVoucherNumber(_frmWebBrowser.WebBrowser1)

                                    'revised 02/17/2017
                                    Dim goToVoucherNumber As String = printF.goToVoucherNumber_v2(_frmWebBrowser.WebBrowser1)
                                    Dim goToLoanAmount As String = printF.goToLoanAmount(_frmWebBrowser.WebBrowser1)
                                    Dim CertifyingEmployerId As String = printF.CertifyingEmployerId(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanStatusError2(fullnameprint, xtd.getCRN, goToLoanType, goToLoanDate, goToVoucherNumber, goToLoanAmount, CertifyingEmployerId, webPageTag, "Print"), DefaultPrinterName)
                                        'class1.prt(class1.prtLoanStatusError2(fullnameprint, xtd.getCRN, My.Settings.webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanStatusError2(xtd.getFullname, SSStempFile, goToLoanType, goToLoanDate, goToVoucherNumber, goToLoanAmount, CertifyingEmployerId, webPageTag, "Print"), DefaultPrinterName)
                                        'class1.prt(class1.prtLoanStatusError2(fullnameprint, SSStempFile, My.Settings.webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                ElseIf checkCreditLoan = False And NoCreditLoan = True Then
                                    Dim loanType01 As String = printF.GetLoanType01(_frmWebBrowser.WebBrowser1)
                                    'Dim AppDate01 As String = printF.GetAppDate01(_frmWebBrowser.WebBrowser1)
                                    'revised to fix value 02/16/2017
                                    Dim AppDate01 As String = printF.GetAppDate01_v2(_frmWebBrowser.WebBrowser1)
                                    Dim loanAppStat01 As String = printF.GetLoanAppStat01(_frmWebBrowser.WebBrowser1)

                                    Dim checkDate01 As String = printF.GetCheckDate01(_frmWebBrowser.WebBrowser1)
                                    Dim loanAmount01 As String = printF.GetLoanAmount01(_frmWebBrowser.WebBrowser1)
                                    Dim monthlyAmort As String = printF.GetMonthlyAmort(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanStatusError3(fullnameprint, xtd.getCRN, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanStatusError3(xtd.getFullname, SSStempFile, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                Else
                                    GC.Collect()
                                    Dim loanType01 As String = printF.GetLoanType01(_frmWebBrowser.WebBrowser1)

                                    'revised by edel 02/02/2017 due to changes in loan status page
                                    'Dim AppDate01 As String = printF.GetAppDate01(_frmWebBrowser.WebBrowser1)
                                    Dim AppDate01 As String = printF.GetAppDate01_v2(_frmWebBrowser.WebBrowser1)

                                    Dim loanAppStat01 As String = printF.GetLoanAppStat01(_frmWebBrowser.WebBrowser1)

                                    Dim checkDate01 As String = printF.GetCheckDate01(_frmWebBrowser.WebBrowser1)
                                    Dim loanAmount01 As String = printF.GetLoanAmount01(_frmWebBrowser.WebBrowser1)
                                    Dim monthlyAmort As String = printF.GetMonthlyAmort(_frmWebBrowser.WebBrowser1)


                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then

                                        class1.prt(class1.prtLoanStatus(fullnameprint, xtd.getCRN, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then

                                        class1.prt(class1.prtLoanStatus(xtd.getFullname, SSStempFile, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                End If

                                printTag = 1

                                getAffectedTable = "10007"

                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Loan Status", ex.Message)
                            End Try

                        Case "Credited Loan Payments"
                            Try
                                GC.Collect()
                                '_frmPrintForm.Dispose()
                                DisposeForm(_frmPrintForm)

                                Dim credLoanType As String = printF.getCredLoanType(_frmWebBrowser.WebBrowser1)
                                'Dim credCheckDate As String = printF.getCredCheckDate(_frmWebBrowser.WebBrowser1)
                                'Dim credLoanAmt As String = printF.getCredLoanAmt(_frmWebBrowser.WebBrowser1)

                                Dim credCheckDate As String = printF.GetLoanDateValue_V2(_frmWebBrowser.WebBrowser1)
                                Dim credLoanAmt As String = printF.GetLoanAmountValue_V2(_frmWebBrowser.WebBrowser1)

                                Dim goToCreditLoan As String = printF.goToCreditLoan(_frmWebBrowser.WebBrowser1)
                                Dim GetTotalAmtDue As String = printF.GetTotalAmtDue(_frmWebBrowser.WebBrowser1)
                                Dim GetAmountNotYetDue As String = "" 'printF.GetAmountNotYetDue(_frmWebBrowser.WebBrowser1)
                                Dim GetTotalAmtObligation As String = printF.GetTotalAmtObligation(_frmWebBrowser.WebBrowser1)
                                'Dim getCertEmpID As String = printF.getCertEmpID(_frmWebBrowser.WebBrowser1)
                                'Dim getCerEmpName As String = printF.getCerEmpName(_frmWebBrowser.WebBrowser1)
                                'Dim getLoanMonthLoan As String = printF.getLoanMonthLoan(_frmWebBrowser.WebBrowser1)
                                'Dim getMonthlyAmort As String = printF.GetMonthlyAmort(_frmWebBrowser.WebBrowser1)

                                Dim fileTYP As Integer = xtd.checkFileType

                                If fileTYP = 1 Then
                                    ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                    class1.prt(class1.prtLoanStatusCredited(fullnameprint, xtd.getCRN, credLoanType, credCheckDate, credLoanAmt, goToCreditLoan, GetTotalAmtDue, GetAmountNotYetDue, GetTotalAmtObligation, webPageTag, "Print"), DefaultPrinterName)

                                ElseIf fileTYP = 2 Then
                                    ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                    class1.prt(class1.prtLoanStatusCredited(xtd.getFullname, SSStempFile, credLoanType, credCheckDate, credLoanAmt, goToCreditLoan, GetTotalAmtDue, GetAmountNotYetDue, GetTotalAmtObligation, webPageTag, "Print"), DefaultPrinterName)

                                End If

                                printTag = 1

                                getAffectedTable = "10007"

                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                getAugitLogs()

                                'splitContainerControl.Panel2.Controls.Clear()
                                '_frmPrintForm.TopLevel = False
                                '_frmPrintForm.Parent = Me.splitContainerControl.Panel2
                                '_frmPrintForm.Dock = DockStyle.Fill
                                '_frmPrintForm.formPrint = "Credited Loan Payments"
                                '_frmPrintForm.btag = "Credited Loan Payments"
                                '_frmPrintForm.Show()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Credited Loan Payments", ex.Message)
                            End Try
                            'Case "Sickness Benefit"

                        Case "SSS / UMID Card Information"
                            Try
                                GC.Collect()
                                '_frmPrintForm.Dispose()
                                DisposeForm(_frmPrintForm)

                                Dim serialNo As String = printF.GetCSJNumberv2(_frmWebBrowser.WebBrowser1)
                                If serialNo = "" Or serialNo = Nothing Then
                                    Dim msgSSID As String = printF.getMsgSSSIDClearance(_frmWebBrowser.WebBrowser1)
                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        '  print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSSidClearanceEmpty(fullnameprint, xtd.getCRN, msgSSID, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSSidClearanceEmpty(xtd.getFullname, SSStempFile, msgSSID, webPageTag, "Print"), DefaultPrinterName)

                                    End If


                                Else
                                    Dim capturedON As String = printF.GetCapturedONv2(_frmWebBrowser.WebBrowser1)
                                    'Dim generatedON As String = printF.GetGeneratedON(_frmWebBrowser.WebBrowser1)
                                    Dim GetCapturedDate As String = printF.GetCapturedDatev2(_frmWebBrowser.WebBrowser1)

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSSidClearance(fullnameprint, xtd.getCRN, serialNo, capturedON, GetCapturedDate, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        '  print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSSidClearance(xtd.getFullname, SSStempFile, serialNo, capturedON, GetCapturedDate, webPageTag, "Print"), DefaultPrinterName)

                                    End If


                                End If


                                printTag = 1

                                getAffectedTable = "10011"

                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "SSS / UMID Card Information", ex.Message)
                            End Try

                        Case "Benefits Eligibility"

                            Select Case BenefitTag1
                                'Case "DDRF CLAIM ELIGIBILITY "

                                Case ""
                                    Try

                                        GC.Collect()
                                        '_frmPrintForm.Dispose()
                                        DisposeForm(_frmPrintForm)

                                        Dim errBenEligibility As Boolean = printF.errBenEligibility(_frmWebBrowser.WebBrowser1)
                                        Dim errBenEligibility2 As Boolean = printF.errBenEligibility2(_frmWebBrowser.WebBrowser1)
                                        If errBenEligibility = True Then
                                            Dim getMsgBenEligibility As String = printF.getMsgBenEligibility(_frmWebBrowser.WebBrowser1)
                                            getMsgBenEligibility = getMsgBenEligibility.Replace("&nbsp;", "")
                                            getMsgBenEligibility = getMsgBenEligibility.Trim
                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibilityError1(fullnameprint, xtd.getCRN, getMsgBenEligibility, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibilityError1(fullnameprint, SSStempFile, getMsgBenEligibility, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        ElseIf errBenEligibility2 = True Then
                                            Dim getMsgBenEligibility2 As String = printF.getMsgBenEligibility2(_frmWebBrowser.WebBrowser1)
                                            getMsgBenEligibility2 = getMsgBenEligibility2.Replace("&nbsp;", "")
                                            getMsgBenEligibility2 = getMsgBenEligibility2.Trim
                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibilityError1(fullnameprint, xtd.getCRN, getMsgBenEligibility2, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibilityError1(fullnameprint, SSStempFile, getMsgBenEligibility2, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        Else
                                            Dim getEarlyRetireDate As String = printF.getEarlyRetireDate(_frmWebBrowser.WebBrowser1)

                                            Dim getTotNumContri As String = printF.getTotNumContri(_frmWebBrowser.WebBrowser1)

                                            Dim getTotNumLumpContri As String = printF.getTotNumLumpContri(_frmWebBrowser.WebBrowser1)

                                            Dim getTotNumDmPaidContri As String = printF.getTotNumDmPaidContri(_frmWebBrowser.WebBrowser1)

                                            Dim getTotNumContriSemContin As String = printF.getTotNumContriSemContin(_frmWebBrowser.WebBrowser1)


                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibility(fullnameprint, xtd.getCRN, getEarlyRetireDate, getTotNumContri, getTotNumLumpContri, getTotNumDmPaidContri, getTotNumContriSemContin, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDDREligibility(fullnameprint, SSStempFile, getEarlyRetireDate, getTotNumContri, getTotNumLumpContri, getTotNumDmPaidContri, getTotNumContriSemContin, webPageTag, "Print"), DefaultPrinterName)
                                            End If
                                        End If


                                        printTag = 1
                                        getAffectedTable = "10012"
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Benefit Eligibility", ex.Message)
                                        printTag = 1
                                    End Try

                                Case "Death Benefit Eligibility"
                                    Try
                                        GC.Collect()
                                        '_frmPrintForm.Dispose()
                                        DisposeForm(_frmPrintForm)
                                        Dim averageMSC As String = printF.getAverageMSC(_frmWebBrowser.WebBrowser1)
                                        averageMSC = averageMSC.Trim
                                        averageMSC = averageMSC.Replace("&nbsp", "")

                                        Dim fileTYP As Integer = xtd.checkFileType
                                        If fileTYP = 1 Then
                                            'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtBenEligDeath(fullnameprint, xtd.getCRN, averageMSC, webPageTag, "Print"), DefaultPrinterName)
                                        ElseIf fileTYP = 2 Then
                                            ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtBenEligDeath(xtd.getFullname, SSStempFile, averageMSC, webPageTag, "Print"), DefaultPrinterName)
                                        End If

                                        printTag = 1
                                        getAffectedTable = "10033"
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()


                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Benefit Eligibility", ex.Message)
                                        printTag = 1
                                    End Try

                                Case "SS FUNERAL"
                                    Try
                                        GC.Collect()
                                        '_frmPrintForm.Dispose()
                                        DisposeForm(_frmPrintForm)
                                        Dim getTypeofBenefitSSFuneral As String = printF.getTypeofBenefitSSFuneral(_frmWebBrowser.WebBrowser1)
                                        Dim getAmtBenefit As String = printF.getAmtBenefit(_frmWebBrowser.WebBrowser1)

                                        Dim fileTYP As Integer = xtd.checkFileType
                                        If fileTYP = 1 Then
                                            ''print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtSSFuneral(fullnameprint, xtd.getCRN, getTypeofBenefitSSFuneral, getAmtBenefit, webPageTag, "Print"), DefaultPrinterName)
                                        ElseIf fileTYP = 2 Then
                                            ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtSSFuneral(fullnameprint, SSStempFile, getTypeofBenefitSSFuneral, getAmtBenefit, webPageTag, "Print"), DefaultPrinterName)
                                        End If


                                        printTag = 1
                                        getAffectedTable = "10034"
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()


                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "SS Funeral Eligibility", ex.Message)
                                        printTag = 1
                                    End Try
                                Case "Disability Eligibility"
                                    Try
                                        GC.Collect()
                                        '_frmPrintForm.Dispose()
                                        DisposeForm(_frmPrintForm)
                                        Dim getDisabilityAMSC As String = printF.getDisabilityAMSC(_frmWebBrowser.WebBrowser1)
                                        getDisabilityAMSC = getDisabilityAMSC.Trim
                                        getDisabilityAMSC = getDisabilityAMSC.Replace("&nbsp", "")
                                        getDisabilityAMSC = getDisabilityAMSC.Replace(";", "")
                                        Dim fileTYP As Integer = xtd.checkFileType
                                        If fileTYP = 1 Then
                                            ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtDisabilityPartialEligibility(fullnameprint, xtd.getCRN, getDisabilityAMSC, webPageTag, "Print"), DefaultPrinterName)
                                        ElseIf fileTYP = 2 Then
                                            ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtDisabilityPartialEligibility(fullnameprint, SSStempFile, getDisabilityAMSC, webPageTag, "Print"), DefaultPrinterName)
                                        End If
                                        printTag = 1
                                        getAffectedTable = "10035"
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "SS Funeral Eligibility", ex.Message)
                                    End Try

                                Case "Total Disability Eligibility"
                                    Try
                                        GC.Collect()
                                        '_frmPrintForm.Dispose()
                                        DisposeForm(_frmPrintForm)
                                        Dim averageMSC As String = printF.getAverageMSC2(_frmWebBrowser.WebBrowser1)
                                        averageMSC = averageMSC.Trim
                                        averageMSC = averageMSC.Replace("&nbsp", "")
                                        Dim fileTYP As Integer = xtd.checkFileType
                                        If fileTYP = 1 Then
                                            'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtBenEligTotDisable(fullnameprint, xtd.getCRN, averageMSC, "Print"), DefaultPrinterName)
                                        ElseIf fileTYP = 2 Then
                                            ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtBenEligTotDisable(xtd.getFullname, SSStempFile, averageMSC, "Print"), DefaultPrinterName)
                                        End If
                                        printTag = 1
                                        getAffectedTable = "10036"
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Total Disability Eligibility", ex.Message)
                                    End Try

                                Case "PARTIAL DISABILITY - PENSION"
                                    Try
                                        GC.Collect()
                                        _frmPrintForm.Dispose()
                                        Dim disabilityEligibleAMSC As String = printF.disabilityEligibleAMSC(_frmWebBrowser.WebBrowser1)

                                        If disabilityEligibleAMSC = "" Or disabilityEligibleAMSC = Nothing Then

                                            Dim disabilityEligiblErr As String = printF.disabilityEligiblErr(_frmWebBrowser.WebBrowser1)
                                            Dim disabilityEligiblErr2 As String = printF.disabilityEligiblErr2(_frmWebBrowser.WebBrowser1)
                                            Dim disabilityEligiblErr3 As String = printF.disabilityEligiblErr3(_frmWebBrowser.WebBrowser1)

                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDisabilityEligibiltyError(fullnameprint, xtd.getCRN, disabilityEligiblErr, disabilityEligiblErr2, disabilityEligiblErr3, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDisabilityEligibiltyError(fullnameprint, SSStempFile, disabilityEligiblErr, disabilityEligiblErr2, disabilityEligiblErr3, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        Else
                                            Dim disabilityEligibleMAB As String = printF.disabilityEligibleMAB(_frmWebBrowser.WebBrowser1)
                                            Dim disabilityEligibleTOB As String = printF.disabilityEligibleTOB(_frmWebBrowser.WebBrowser1)
                                            ' disabilityEligibleMAB = disabilityEligibleMAB.Replace(";", "")

                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDisabilityEligibilty(fullnameprint, xtd.getCRN, disabilityEligibleAMSC, disabilityEligibleTOB, disabilityEligibleMAB, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtDisabilityEligibilty(fullnameprint, SSStempFile, disabilityEligibleAMSC, disabilityEligibleTOB, disabilityEligibleMAB, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        End If
                                        printTag = 1
                                        getAffectedTable = "10034"
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "PARTIAL DISABILITY - PENSION", ex.Message)
                                    End Try

                                Case "Average Monthly Salary Credit"
                                    Try
                                        GC.Collect()
                                        _frmPrintForm.Dispose()
                                        Dim TotaldisabilityAMSC As String = printF.TotaldisabilityAMSC(_frmWebBrowser.WebBrowser1)

                                        If TotaldisabilityAMSC = "" Or TotaldisabilityAMSC = Nothing Then

                                            Dim totalDisabilityErr As String = printF.totalDisabilityErr(_frmWebBrowser.WebBrowser1)
                                            Dim totalDisabilityErr2 As String = printF.totalDisabilityErr2(_frmWebBrowser.WebBrowser1)
                                            Dim totalDisabilityErr3 As String = printF.totalDisabilityErr3(_frmWebBrowser.WebBrowser1)

                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtTotalEligibiltyError(fullnameprint, xtd.getCRN, totalDisabilityErr, totalDisabilityErr, totalDisabilityErr3, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                '   print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtTotalEligibiltyError(fullnameprint, SSStempFile, totalDisabilityErr, totalDisabilityErr, totalDisabilityErr3, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        Else
                                            Dim TotaldisabilityTOB As String = printF.TotaldisabilityTOB(_frmWebBrowser.WebBrowser1)
                                            Dim TotaldisabilityAOB As String = printF.TotaldisabilityAOB(_frmWebBrowser.WebBrowser1)

                                            Dim fileTYP As Integer = xtd.checkFileType
                                            If fileTYP = 1 Then
                                                'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtTotalEligibilty(fullnameprint, xtd.getCRN, TotaldisabilityAMSC, TotaldisabilityTOB, TotaldisabilityAOB, webPageTag, "Print"), DefaultPrinterName)
                                            ElseIf fileTYP = 2 Then
                                                '  print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                                class1.prt(class1.prtTotalEligibilty(fullnameprint, SSStempFile, TotaldisabilityAMSC, TotaldisabilityTOB, TotaldisabilityAOB, webPageTag, "Print"), DefaultPrinterName)
                                            End If

                                        End If
                                        printTag = 1
                                        getAffectedTable = "10034"
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Average Monthly Salary Credit", ex.Message)
                                    End Try

                                Case "Retirement Eligibility"
                                    Try
                                        GC.Collect()
                                        DisposeForm(_frmPrintForm)
                                        Dim retirementEligibiltyAMSC As String = printF.retirementEligibiltyAMSC(_frmWebBrowser.WebBrowser1)
                                        retirementEligibiltyAMSC = retirementEligibiltyAMSC.Trim
                                        retirementEligibiltyAMSC = retirementEligibiltyAMSC.Replace("&nbsp", "")
                                        Dim fileTYP As Integer = xtd.checkFileType
                                        If fileTYP = 1 Then
                                            ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtRetirementEligibilty(fullnameprint, xtd.getCRN, retirementEligibiltyAMSC, webPageTag, "Print"), DefaultPrinterName)
                                        ElseIf fileTYP = 2 Then
                                            ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                            class1.prt(class1.prtRetirementEligibilty(fullnameprint, SSStempFile, retirementEligibiltyAMSC, webPageTag, "Print"), DefaultPrinterName)
                                        End If

                                        printTag = 1
                                        getAffectedTable = "10034"
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        getAugitLogs()

                                    Catch ex As Exception
                                        MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information, "Information")
                                        SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Average Monthly Salary Credit", ex.Message)
                                    End Try

                                Case Else
                                    '  nikki01
                                    MsgBox("There is no data to be printed out. ", MsgBoxStyle.Information, "Information")
                            End Select
                        Case "Loan Eligibility"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)
                                PrintControls(True)

                                Dim getMsgLoan As Boolean = printF.getMsgLoan(_frmWebBrowser.WebBrowser1)
                                If getMsgLoan = True Then

                                    Dim getMsgLoan2 As String = printF.getMsgLoan2(_frmWebBrowser.WebBrowser1)
                                    getMsgLoan2 = getMsgLoan2.Replace("&nbsp;", "")
                                    getMsgLoan2 = getMsgLoan2.Trim

                                    Dim sbMsg2 As New StringBuilder
                                    For Each s As String In getMsgLoan2.Split(vbNewLine)
                                        If s.Trim = "&nbsp" Then
                                        ElseIf s.Trim = "Employer Information" Then
                                            Exit For
                                        Else
                                            sbMsg2.Append(s)
                                        End If
                                    Next

                                    Dim reason As String = sbMsg2.ToString
                                    'Dim reason As String = SharedFunction.callLoanEligibRejectReason

                                    Dim sbMsg As New StringBuilder
                                    'sbMsg.AppendLine("Salary Loan")
                                    'sbMsg.AppendLine("Loan application will be rejected due to the ")
                                    'sbMsg.AppendLine("following reason(s):")
                                    For Each r As String In reason.Split(vbLf)
                                        Dim isError As Boolean
                                        Dim p As String = SharedFunction.FormatDataWithCharLength(r, 50, isError)
                                        sbMsg.AppendLine(p)
                                    Next

                                    'Dim isError As Boolean
                                    'Dim p As String = SharedFunction.FormatDataWithCharLength(SharedFunction.callLoanEligibRejectReason, 50, isError)

                                    'Dim getMsgLoan3 As String = printF.getMsgLoan3(_frmWebBrowser.WebBrowser1)

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanEligibiltyEmpty(fullnameprint, xtd.getCRN, sbMsg.ToString, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanEligibiltyEmpty(xtd.getFullname, SSStempFile, sbMsg.ToString, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                    printTag = 1

                                    getAffectedTable = "10013"
                                    getAugitLogs()
                                Else
                                    Dim loanValues As String = printF.getLoanAmountv2(_frmWebBrowser.WebBrowser1)
                                    'Dim loanMonth As String = printF.getLoanAmount(_frmWebBrowser.WebBrowser1)
                                    'Dim msc As String = printF.getLoanEligibilityMSC(_frmWebBrowser.WebBrowser1)
                                    'Dim loanAmount As String = printF.getLoanAmountEligibility(_frmWebBrowser.WebBrowser1)
                                    'Dim getPrevBalance As String = printF.getPrevBalance(_frmWebBrowser.WebBrowser1)
                                    'Dim getLoanBalance As String = printF.getLoanBalance(_frmWebBrowser.WebBrowser1)
                                    'Dim getLoanProceeds As String = printF.getLoanProceeds(_frmWebBrowser.WebBrowser1)

                                    Dim loanMonth As String = loanValues.Split("|")(0)
                                    Dim msc As String = loanValues.Split("|")(1)
                                    Dim loanAmount As String = loanValues.Split("|")(2)
                                    Dim getPrevBalance As String = loanValues.Split("|")(3)
                                    Dim getLoanBalance As String = loanValues.Split("|")(4)
                                    Dim getLoanProceeds As String = loanValues.Split("|")(5)

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanEligibilty(fullnameprint, xtd.getCRN, loanMonth, msc, loanAmount, getPrevBalance, getLoanBalance, getLoanProceeds, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtLoanEligibilty(xtd.getFullname, SSStempFile, loanMonth, msc, loanAmount, getPrevBalance, getLoanBalance, getLoanProceeds, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                    printTag = 1

                                    getAffectedTable = "10013"

                                    'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                    getAugitLogs()

                                    'splitContainerControl.Panel2.Controls.Clear()
                                    '_frmPrintForm.TopLevel = False
                                    '_frmPrintForm.Parent = Me.splitContainerControl.Panel2
                                    '_frmPrintForm.Dock = DockStyle.Fill
                                    '_frmPrintForm.formPrint = "Loan Eligibility"
                                    '_frmPrintForm.btag = "Loan Eligibility"
                                    '_frmPrintForm.Show()
                                End If
                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Loan Eligibility", ex.Message)
                            End Try

                        Case "Maternity Benefit"
                            Try
                                Dim getMsgMatBen As String = printF.getMsgMatBen(_frmWebBrowser.WebBrowser1)
                                If getMsgMatBen = True Then

                                    Dim fileTYP As Integer = xtd.checkFileType
                                    If fileTYP = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaterniryClaimNoReturn(fullnameprint, xtd.getCRN, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        '  print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaterniryClaimNoReturn(fullnameprint, SSStempFile, webPageTag, "Print"), DefaultPrinterName)
                                    End If
                                Else
                                    'Dim claimNo As String = printF.getClaimNo(_frmWebBrowser.WebBrowser1)
                                    'Dim delivDate As String = printF.getdelivDate(_frmWebBrowser.WebBrowser1)
                                    'Dim days As String = printF.getdays(_frmWebBrowser.WebBrowser1)
                                    'Dim amountPaid As String = printF.getAmountPaidMat(_frmWebBrowser.WebBrowser1)
                                    'Dim statChkNo As String = printF.getstatChkNo(_frmWebBrowser.WebBrowser1)
                                    'Dim statDate As String = printF.getstatDate(_frmWebBrowser.WebBrowser1)

                                    'revised by edel 08/23/2018 due to changes in SSS page
                                    Dim claimNo As String = ""
                                    Dim delivDate As String = ""
                                    Dim days As String = ""
                                    Dim amountPaid As String = ""
                                    Dim statChkNo As String = ""
                                    Dim statDate As String = ""

                                    printF.getMaternityBenefitVariables(_frmWebBrowser.WebBrowser1, claimNo, delivDate, days, amountPaid, statChkNo, statDate)

                                    Dim fileTYP As Integer = xtd.checkFileType
                                    If fileTYP = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaterniryClaim(fullnameprint, xtd.getCRN, claimNo, delivDate, days, amountPaid, statChkNo, statDate, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        '  print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaterniryClaim(fullnameprint, SSStempFile, claimNo, delivDate, days, amountPaid, statChkNo, statDate, webPageTag, "Print"), DefaultPrinterName)
                                    End If
                                End If

                                printTag = 1

                                getAffectedTable = "10010"

                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                getAugitLogs()


                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Loan Eligibility", ex.Message)
                            End Try
                        Case "Maternity Information"
                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)
                                PrintControls(True)

                                Dim dateFiled As String = printF.getDateFiled(_frmWebBrowser.WebBrowser1)
                                Dim benefitAppStatus As String = printF.getBenefitAppStatus(_frmWebBrowser.WebBrowser1)
                                Dim MaternityDeliveryDate As String = printF.getMaternityDeliveryDate(_frmWebBrowser.WebBrowser1)
                                Dim NoOfDays As String = printF.getNoOfDays(_frmWebBrowser.WebBrowser1)
                                Dim checkDate As String = printF.getCheckDate(_frmWebBrowser.WebBrowser1)
                                Dim amountPaid As String = printF.getAmountPaid(_frmWebBrowser.WebBrowser1)


                                Dim fileTYP As Integer = xtd.checkFileType

                                If fileTYP = 1 Then
                                    ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                    class1.prt(class1.prtMaterniryClaimStatus(fullnameprint, xtd.getCRN, dateFiled, benefitAppStatus, MaternityDeliveryDate, NoOfDays, checkDate, amountPaid, webPageTag, "Print"), DefaultPrinterName)

                                ElseIf fileTYP = 2 Then
                                    'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                    class1.prt(class1.prtMaterniryClaimStatus(xtd.getFullname, SSStempFile, dateFiled, benefitAppStatus, MaternityDeliveryDate, NoOfDays, checkDate, amountPaid, webPageTag, "Print"), DefaultPrinterName)

                                End If

                                printTag = 1

                                getAffectedTable = "10008"
                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Maternity Information", ex.Message)
                            End Try


                        Case "Maternity Eligibility"

                            Try
                                GC.Collect()
                                DisposeForm(_frmPrintForm)

                                Dim getMaternityErr As Boolean = printF.getMaternityErr(_frmWebBrowser.WebBrowser1)
                                If getMaternityErr = True Then
                                    'ARGIE101
                                    Dim getMaternityErr2 As String = printF.getMatEligibilityErr2(_frmWebBrowser.WebBrowser1)
                                    getMaternityErr2 = getMaternityErr2.Replace("&nbsp;", "")
                                    ' getSicknessErr2 = getSicknessErr2.Trim
                                    Dim fileTYPerr As Integer = xtd.checkFileType

                                    If fileTYPerr = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaternityError(fullnameprint, xtd.getCRN, getMaternityErr2, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYPerr = 2 Then
                                        ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaternityError(fullnameprint, SSStempFile, getMaternityErr2, webPageTag, "Print"), DefaultPrinterName)

                                    End If
                                Else

                                    ' Dim dateFiled As String = printF.getMAtTypeBenefit(_frmWebBrowser.WebBrowser1)
                                    Dim MAtTypeBenefit As String = printF.getMAtTypeBenefit(_frmWebBrowser.WebBrowser1)

                                    Dim MatAmtBen As String = printF.getMatAmtBen(_frmWebBrowser.WebBrowser1)

                                    Dim MatDaysClaims As String = printF.getMatDaysClaims(_frmWebBrowser.WebBrowser1)

                                    Dim MatAllowance As String = printF.getMatAllowance(_frmWebBrowser.WebBrowser1)

                                    ' Dim sicknesAmountPaid As String = printF.getSicknesAmountPaid(_frmWebBrowser.WebBrowser1)

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaternityElig(fullnameprint, xtd.getCRN, MAtTypeBenefit, MatAmtBen, MatDaysClaims, MatAllowance, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtMaternityElig(fullnameprint, SSStempFile, MAtTypeBenefit, MatAmtBen, MatDaysClaims, MatAllowance, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                End If
                                'Button2.Enabled = True


                                'printTag = 1

                                'getAffectedTable = "10010"
                                'getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Sickness Benefit", ex.Message)
                            End Try

                        Case "Sickness Eligibility"

                            Try
                                GC.Collect()
                                _frmPrintForm.Dispose()

                                Dim getSicknessErr As Boolean = printF.getSicknessErr(_frmWebBrowser.WebBrowser1)
                                If getSicknessErr = True Then
                                    'ARGIE101
                                    Dim getSicknessErr2 As String = printF.getSicknessErr2(_frmWebBrowser.WebBrowser1)
                                    getSicknessErr2 = getSicknessErr2.Replace("&nbsp", "")
                                    getSicknessErr2 = getSicknessErr2.Replace("&nbsp;", "")
                                    getSicknessErr2 = getSicknessErr2.Trim
                                    Dim fileTYPerr As Integer = xtd.checkFileType

                                    If fileTYPerr = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessError(fullnameprint, xtd.getCRN, getSicknessErr2, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYPerr = 2 Then
                                        '   print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessError(fullnameprint, SSStempFile, getSicknessErr2, webPageTag, "Print"), DefaultPrinterName)

                                    End If
                                Else

                                    ' Dim dateFiled As String = printF.getSicknessDateFiled(_frmWebBrowser.WebBrowser1)
                                    Dim sicknessBenefitAppStatus As String = printF.getSicknessBenefitAppStatus(_frmWebBrowser.WebBrowser1)

                                    Dim DateConfined As String = printF.getDateConfined(_frmWebBrowser.WebBrowser1)

                                    Dim sicknessNoOfDays As String = printF.getSicknessNoOfDays(_frmWebBrowser.WebBrowser1)

                                    Dim sicknessCheckDate As String = printF.getSicknessCheckDate(_frmWebBrowser.WebBrowser1)

                                    Dim sicknesAmountPaid As String = printF.getSicknesAmountPaid(_frmWebBrowser.WebBrowser1)

                                    Dim fileTYP As Integer = xtd.checkFileType

                                    If fileTYP = 1 Then
                                        ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessClaims(fullnameprint, xtd.getCRN, "", sicknessBenefitAppStatus, DateConfined, sicknessNoOfDays, sicknessCheckDate, sicknesAmountPaid, webPageTag, "Print"), DefaultPrinterName)

                                    ElseIf fileTYP = 2 Then
                                        ' print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessClaims(xtd.getFullname, SSStempFile, "", sicknessBenefitAppStatus, DateConfined, sicknessNoOfDays, sicknessCheckDate, sicknesAmountPaid, webPageTag, "Print"), DefaultPrinterName)

                                    End If

                                End If
                                PrintControls(True)
                                'btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")

                                printTag = 1

                                getAffectedTable = "10010"

                                ' print_cnt(xtd.getCRN, Date.Today.ToShortDateString)

                                getAugitLogs()

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Sickness Benefit", ex.Message)
                            End Try

                        Case "Sickness Benefit"
                            Try
                                Dim getSicknessBenefitError As Boolean = printF.getSicknessBenefitError(_frmWebBrowser.WebBrowser1)

                                If getSicknessBenefitError = True Then

                                    Dim fileTYPerr As Integer = xtd.checkFileType
                                    If fileTYPerr = 1 Then
                                        'print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessBenError(fullnameprint, xtd.getCRN, webPageTag, "Print"), DefaultPrinterName)
                                    ElseIf fileTYPerr = 2 Then
                                        'print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicknessBenError(fullnameprint, SSStempFile, webPageTag, "Print"), DefaultPrinterName)
                                    End If

                                End If

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Sickness Benefit", ex.Message)
                            End Try

                        Case "Sickness Benefit Information"
                            Try
                                Dim getSicknessBenefitError As Boolean = printF.getSicknessBenefitError(_frmWebBrowser.WebBrowser1)

                                If getSicknessBenefitError = False Then

                                    Dim GetSickBenInfoDateFiled As String = printF.GetSickBenInfoDateFiled(_frmWebBrowser.WebBrowser1)
                                    Dim GetSickBenRemarks As String = printF.GetSickBenRemarks(_frmWebBrowser.WebBrowser1)
                                    Dim GetSickBenConPeriod As String = printF.GetSickBenConPeriod(_frmWebBrowser.WebBrowser1)
                                    Dim GetSickBenAmtPaid As String = printF.GetSickBenAmtPaid(_frmWebBrowser.WebBrowser1)
                                    Dim GetSickBenStartDate As String = printF.GetSickBenStartDate(_frmWebBrowser.WebBrowser1)
                                    Dim GetSickBenEndDate As String = printF.GetSickBenEndDate(_frmWebBrowser.WebBrowser1)

                                    Dim fileTYPerr As Integer = xtd.checkFileType
                                    If fileTYPerr = 1 Then
                                        '  print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicnkessBenefitInfo(fullnameprint, xtd.getCRN, GetSickBenInfoDateFiled, GetSickBenRemarks, GetSickBenConPeriod,
                                                                                 GetSickBenAmtPaid, GetSickBenStartDate, GetSickBenEndDate, webPageTag, "Print"), DefaultPrinterName)
                                    ElseIf fileTYPerr = 2 Then
                                        '  print_cnt(SSStempFile, Date.Today.ToShortDateString)
                                        class1.prt(class1.prtSicnkessBenefitInfo(fullnameprint, SSStempFile, GetSickBenInfoDateFiled, GetSickBenRemarks, GetSickBenConPeriod,
                                                                                 GetSickBenAmtPaid, GetSickBenStartDate, GetSickBenEndDate, webPageTag, "Print"), DefaultPrinterName)
                                    End If

                                End If

                            Catch ex As Exception
                                MsgBox("SORRY, THIS TERMINAL WAS UNABLE TO PRINT YOUR RECEIPT PROPERLY." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine & "AT FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH.", MsgBoxStyle.Information)
                                SharedFunction.SaveToInfoTerminalLogPrint(Path1, "Sickness Benefit", ex.Message)
                            End Try
                    End Select
                End If
            Case "3", "4", "5"

            Case "17"
                If IsAllowedToPrint() Then

                    Dim note As String
                    Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                    Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                    Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                    Dim getKioskName As String = kioskName
                    If getKioskName <> "" Then getKioskName = getKioskName.Substring(0, 1)

                    'Dim fileTYPerr2 As Integer = xtd.checkFileType
                    'If fileTYPerr2 = 1 Then
                    '    print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                    'ElseIf fileTYPerr2 = 2 Then
                    '    print_cnt(SSStempFile, Date.Today.ToShortDateString)
                    'End If

                    print_cnt(getCRN_SSSNum, Date.Today.ToShortDateString)

                    lastTransNo = readSettings(xml_Filename, xml_path, "lastTransNo")

                    Select Case authentication

                        Case "UA02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & txnTypeRcpt & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtFailedAuth(fullnameprint, xtd.getCRN, procCodeLen3, txnName, "Print"), DefaultPrinterName)


                        Case "MRG02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtRegistrationSub(fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)


                        'Case "SLP01"

                        '    'If lastTransNo = "" Or lastTransNo.Length < 19 Then

                        '    'Else
                        '    '    procCodeLen1 = lastTransNo.Substring(0, 7)
                        '    '    procCodeLen2 = lastTransNo.Substring(9, 10)
                        '    '    procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                        '    'End If

                        '    class1.footer1 = ""
                        '    class1.prt(class1.prtSalaryLoanSub(fullnameprint, xtd.getCRN, _frmLoanSummaryMember_v2.txnNum, "SALARY LOAN", "Print"), DefaultPrinterName)

                        'Case "SLP02"

                        '    If lastTransNo = "" Or lastTransNo.Length < 19 Then

                        '    Else
                        '        procCodeLen1 = lastTransNo.Substring(0, 7)
                        '        procCodeLen2 = lastTransNo.Substring(9, 10)
                        '        procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                        '    End If

                        '    class1.footer1 = ""
                        '    class1.prt(class1.prtSalaryLoanSub2(fullnameprint, xtd.getCRN, _frmLoanSummaryEmployed.employedRefNo, "SALARY LOAN", "Print"), DefaultPrinterName)


                        Case "MNP01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtMatnotif(fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)
                            'nikki003
                        Case "MNP02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtMatnotif2(fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)


                        Case "TRP01"

                            'If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            'Else
                            '    procCodeLen1 = lastTransNo.Substring(0, 7)
                            '    procCodeLen2 = lastTransNo.Substring(9, 10)
                            '    procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            'End If



                            procCodeLen3 = techRetTransNum

                            class1.footer1 = ""
                            class1.prt(class1.prtTechRet(fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                        Case "ACOP02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtAcop(fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)

                        Case "PM01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "PM" & procCodeLen2
                            End If

                            class1.footer1 = ""
                            class1.prt(class1.prtPension(fullnameprint, xtd.getCRN, procCodeLen3, "CHANGE OF ADDRESS/CONTACT INFORMATION", "Print"), DefaultPrinterName)

                            'INELIGIBLE REASONS OR VALIDATION - DATE JUNE 6, 2014

                            'WEB REGISTRATION

                        Case "WR01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            note = "YOU ARE ALREADY REGISTERED. " & vbNewLine &
                                    "YOU MAY LOGIN IN THE SSS WEBSITE USING YOUR ACCOUNT." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)

                        Case "WR02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            note = "MEMBER HAS ALREADY AVAILED OF A FINAL CLAIM. WEB REGISTRATION" & vbNewLine & "NOT ALLOWED." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)

                        Case "WR03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            note = "YOUR SS CARD NUMBER IS INVALID." & vbNewLine & vbNewLine &
                                     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."
                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)

                            'SALARY LOAN

                        Case "SL01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "YOUR APPLICATION IS INELIGIBLE FOR THE FOLLOWING" & vbNewLine &
                                   "REASON/S: " & vbNewLine &
                                   "THE ACCOUNT NUMBER BELONGS TO ANOTHER PERSON." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)

                        Case "SL02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "TRANSACTION DENIED." & vbNewLine &
                                    "YOUR CITIBANK PUN IS INVALID." & vbNewLine & vbNewLine &
                           "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)

                        Case "SL03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "SYSTEM ERROR: BRANCH NAME CANNOT BE FOUND. " & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)

                        Case "SL04"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "YOUR APPLICATION IS INELIGIBLE FOR THE FOLLOWING REASON/S:" & vbNewLine &
                                    "YOU STILL HAVE A PENDING SALARY LOAN APPLICATION. " & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)

                        Case "SL05"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE" & vbNewLine &
                                     "PROCESSING YOUR APPLICATION. PLEASE TRY" & vbNewLine &
                                     "AGAIN LATER." & vbNewLine & vbNewLine &
                            "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)

                        Case "SL06"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "LG" & procCodeLen2
                            End If

                            note = "YOUR APPLICATION IS INELIGIBLE FOR THE FOLLOWING" & vbNewLine &
                                                            "REASON/S: YOU STILL HAVE A PENDING SALARY LOAN " & vbNewLine &
                                                            "APPLICATION." & vbNewLine & vbNewLine &
                                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SALARY LOAN", "Print"), DefaultPrinterName)


                            'MATERNITY NOTIFICATION

                        Case "MN01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE SUBMITTING " & vbNewLine &
                                   "YOUR REQUEST. PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, errorTag, "Print"), DefaultPrinterName)

                        Case "MN02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED " &
                         vbNewLine & "TO SUBMIT THEIR MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."
                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, MEMBER HAS ALREADY AVAILED OF A FINAL CLAIM." & vbNewLine & vbNewLine &
                             "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN04"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, WE DID NOT FIND ANY SUPPORTING DOCUMENT" & vbNewLine &
                                     "SUBMITTED TO SUPPORT YOUR SS FORM E-1." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN05"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOUR SS NUMBER IS INACTIVE." & vbNewLine & vbNewLine &
                                     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN06"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, ONLY FEMALE MEMBERS BETWEEN AGES 14 TO 60" & vbNewLine &
                                     "YEARS OLD ARE ALLOWED TO SUBMIT THEIR MATERNITY" & vbNewLine &
                                     "NOTIFICATION." & vbNewLine & vbNewLine &
                              "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN07"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, THIS FACILITY IS FOR SELF-EMPLOYED/VOLUNTARY" & vbNewLine &
                                     "MEMBERS ONLY. PLEASE SUBMIT YOUR MATERNITY" & vbNewLine &
                                     "NOTIFICATION THROUGH YOUR EMPLOYER" & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MN08"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, THIS FACILITY IS FOR SELF-EMPLOYED/VOLUNTARY" & vbNewLine &
                                     "MEMBERS ONLY. PLEASE SUBMIT YOUR MATERNITY" & vbNewLine &
                                     "NOTIFICATION THROUGH YOUR EMPLOYER" & vbNewLine & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, THE DELIVERY NUMBER YOU INDICATED WAS" & vbNewLine &
                                   "ALREADY SETTLED. YOU CANNOT PROCEED WITH THE" & vbNewLine &
                                   "SUBMISSION OF YOUR MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOUR DATE OF LAST DELIVERY IS INVALID" & vbNewLine &
                       "(AFTER THE CURRENT DATE)." & vbNewLine & vbNewLine &
                                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)
                            '  NIkki 02
                        Case "MNS03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOU HAVE ALREADY REACHED THE MAXIMUM LIMIT" & vbNewLine &
                       "OF FOUR (4) PREGNANCIES." & vbNewLine & vbNewLine &
                        "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."
                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS04"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, EXPECTED DATE OF DELIVERY SHOULD BE ANY" & vbNewLine &
                       "DATE WITHIN 9 MONTHS FROM THE CURRENT DATE." & vbNewLine & vbNewLine &
                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS05"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, EXPECTED DATE OF DELIVERY SHOULD BE ANY" & vbNewLine &
                       "DATE WITHIN 9 MONTHS FROM THE CURRENT DATE." & vbNewLine & vbNewLine &
                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS06"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOUR EXPECTED DATE OF DELIVERY SHOULD HAVE AN" & vbNewLine &
                                   "INTERVAL OF AT LEAST 6 MONTHS FROM YOUR LAST DELIVERY" & vbNewLine &
                       "DATE." & vbNewLine & vbNewLine &
                        "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS07"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOUR DATE FORMAT IS INVALID." & vbNewLine & vbNewLine &
           "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS08"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR " & vbNewLine &
                                     "MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "MNS09"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "MT" & procCodeLen2
                            End If

                            note = "SORRY, YOU HAVE ENTERED AN INVALID NUMBER OF DELIVERIES. " & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "MATERNITY NOTIFICATION", "Print"), DefaultPrinterName)

                        Case "TR01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If


                            note = "SORRY, BUT YOU NEED TO APPLY FOR UMID CARD AND ENROLL " & vbNewLine &
                                   "IT TO ANY OF THE PARTICIPATING BANKS TO FILE " & vbNewLine &
                                   "YOUR RETIREMENT CLAIM USING THIS FACILITY" & vbNewLine & vbNewLine &
                                   "PLEASE GO TO ANY OF THE ACCREDITED BANKS BELOW: " & vbNewLine & vbNewLine &
                                   "PHILIPPINE NATIONAL BANK          www.pnb.com" & vbNewLine &
                                   "UNION BANK OF THE PHILIPPINES   www.unionbankph.com" & vbNewLine &
                                   "SECURITY BANK                          www.securitybank.com" & vbNewLine & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                   "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                   "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                            '  nikki03
                        Case "TR02"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "SORRY, BUT YOU NEED TO ENROLL YOUR UMID CARD TO ANY OF" & vbNewLine &
                                   "THE FOLLOWING PARTICIPATING BANKS." & vbNewLine &
                                   "PHILIPPINE NATIONAL BANK               www.pnb.com" & vbNewLine &
                                   "UNION BANK OF THE PHILIPPINES      www.unionbankph.com" & vbNewLine &
                                   "SECURITY BANK                                   www.securitybank.com" & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                   "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                   "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                        Case "TR05"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "SORRY, ONLY MEMBERS WHO ARE 65 YEARS OLD AND ABOVE" & vbNewLine &
                                   "ARE ALLOWED TO FILE TECHNICAL RETIREMENT BENEFIT" & vbNewLine &
                                   "CLAIM." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)



                        Case "TR06"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR APPLICATION " & vbNewLine &
                                     "FOR RETIREMENT." & vbNewLine & vbNewLine &
                                     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                        Case "TR07"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS BEEN" & vbNewLine &
                                     "CANCELLED." & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)



                        Case "TR08"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "PLEASE BE INFORMED THAT THE EFFECTIVITY OF YOUR" & vbNewLine &
                                     "RETIREMENT IS ON " & _frmUserAuthentication.techRetDate & "." & vbNewLine & vbNewLine &
                                     "THIS DOES NOT FALL UNDER TECHNICAL RETIREMENT." & vbNewLine & vbNewLine &
                                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtTechMsg(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)


                        Case "TR15"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            End If

                            note = "SORRY, YOU WERE ALREADY GRANTED RETIREMENT BENEFIT." & vbNewLine & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)




                            '                   Case "TR03"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "SORRY, YOUR UMID CARD ACCOUNT IS INVALID." & vbNewLine & vbNewLine & _
                            '                            "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)


                            '                   Case "TR04"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "SORRY, BUT YOU NEED TO ENROLL YOUR UMID CARD TO ANY " & vbNewLine & _
                            '                                 "PARTICIPATING BANKS TO FILE YOUR RETIREMENT CLAIM " & vbNewLine & _
                            '                                 "USING THIS FACILITY." & vbNewLine & vbNewLine & _
                            '                                  "PLEASE GO TO ANY OF THE ACCREDITED BANKS BELOW: " & vbNewLine & vbNewLine & _
                            '                                 "PHILIPPINE NATIONAL BANK      www.pnb.com" & vbNewLine & _
                            '                                 "UNION BANK OF THE PHILIPPINES www.unionbankph.com" & vbNewLine & _
                            '                                 "SECURITY BANK                 www.securitybank.com" & vbNewLine & vbNewLine & _
                            '                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                            '                   Case "TR05"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "SORRY, ONLY MEMBERS WHO ARE 65 YEARS OLD AND ABOVE" & vbNewLine & _
                            '                              "ARE ALLOWED TO APPLY FOR TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                            '                               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                            '                   Case "TR06"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR " & vbNewLine & _
                            '                                "TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                            '                                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)


                            '                   Case "TR07"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS BEEN" & vbNewLine & _
                            '                                "CANCELLED." & vbNewLine & vbNewLine & _
                            '                               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)



                            '                   Case "TR08"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "PLEASE BE INFORMED THAT THE EFFECTIVITY OF YOUR" & vbNewLine & _
                            '                                "RETIREMENT IS ON " & _frmUserAuthentication.techRetDate & "." & vbNewLine & vbNewLine & _
                            '                                "THIS DOES NOT FALL UNDER TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                            '                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtTechMsg(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                            '                   Case "TR09"

                            '                       If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            '                       Else
                            '                           procCodeLen1 = lastTransNo.Substring(0, 7)
                            '                           procCodeLen2 = lastTransNo.Substring(9, 10)
                            '                           procCodeLen3 = procCodeLen1 & "RT" & procCodeLen2
                            '                       End If

                            '                       note = "THIS SERVICE IS NOT YET AVAILABLE." & vbNewLine & vbNewLine & _
                            '                              "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                            '                               "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                            '                               "AT THE SSS BRANCH."

                            '                       class1.prt(class1.prtTechMsg(note, fullnameprint, xtd.getCRN, procCodeLen3, "TECHNICAL RETIREMENT", "Print"), DefaultPrinterName)

                            '                       'ACOP
                        Case "ACOP01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            note = "WE REGRET THAT YOU CANNOT PROCEED WITH YOUR ANNUAL " & vbNewLine &
                                     "CONFIRMATION OF PENSIONER DUE TO YOUR POSTED " & vbNewLine &
                                     "CONTRIBUTIONS AFTER YOUR RETIREMENT DATE. " & vbNewLine & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)

                        Case "ACOP03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
                            'Dim getYearBday As String = Date.Today.Year + 1
                            Dim getYearBday As String = db.putSingleValue("select DATEPART(year,max( NXTSUBM)) from SSTRANSACOP  where ssnum ='" & _frmUserAuthentication.tempSSNum & "' GROUP BY SSNUM ")
                            Dim getDayBday As String = bdate.Day
                            Dim getMonthBday As String = MonthName(bdate.Month)
                            getMonthBday = UCase(getMonthBday)
                            Dim finalDate As String = getMonthBday & " " & getDayBday & ", " & getYearBday

                            note = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR ANNUAL CONFIRMATION OF" & vbNewLine &
                                   "PENSIONER COMPLIANCE. YOUR NEXT SCHEDULE WILL BE IN  " & finalDate & "." & vbNewLine &
                                   "YOU MAY ALSO REPORT SIX (6) MONTHS BEFORE THE  SAID DATE" & vbNewLine &
                                   "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE" & vbNewLine &
                           " NUMBER BELOW:" & vbNewLine & vbNewLine

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)

                            'ClearCache2()'e

                        Case "ACOP04"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
                            'Dim getYearBday As String = Date.Today.Year + 1
                            Dim getYearBday As String = db.putSingleValue("select DATEPART(year,max( NXTSUBM)) from SSTRANSACOP  where ssnum ='" & _frmUserAuthentication.tempSSNum & "' GROUP BY SSNUM ")
                            Dim getDayBday As String = bdate.Day
                            Dim getMonthBday As String = MonthName(bdate.Month)
                            getMonthBday = UCase(getMonthBday)
                            Dim finalDate As String = getMonthBday & " " & getDayBday & ", " & getYearBday
                            Dim finalDateAcop1 As String = getMonthBday & " " & getYearBday
                            Dim mon1 As String = bdate.Month
                            Dim numDate As Date = bdate.Month & "/" & bdate.Day & "/" & getYearBday
                            Dim bMonth As Date = DateAdd(DateInterval.Month, -6, numDate)
                            Dim bmnth1 As String = UCase(MonthName(bMonth.Month))
                            Dim bMonth1 As Date = DateAdd(DateInterval.Month, -1, numDate)
                            Dim bmnth2 As String = UCase(MonthName(bMonth1.Month))

                            note = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR ANNUAL" & vbNewLine &
                                     "CONFIRMATION OF PENSIONER (ACOP) COMPLIANCE FOR" & vbNewLine &
                                     "THE CURRENT YEAR. YOUR NEXT SCHEDULE WILL BE IN" & vbNewLine &
                                     finalDateAcop1 & "." & " YOU MAY ALSO REPORT FROM " & bmnth1 & vbNewLine &
                                     "TO " & bmnth2 & " " & getYearBday & " FOR EARLY COMPLIANCE." & vbNewLine &
                                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)

                        Case "ACOP05"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            note = "SORRY, MEMBER HAS ALREADY AVAILED OF A FINAL CLAIM." & vbNewLine & vbNewLine &
                                     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)

                        Case "ACOP07"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "AC" & procCodeLen2
                            End If

                            class1.prt(class1.prtAcopSuspension(fullnameprint, xtd.getCRN, procCodeLen3, "ANNUAL CONFIRMATION OF PENSIONER", "Print"), DefaultPrinterName)


                            'SIMPLIFIED WEB REGISTRATION

                        Case "MRG01"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            note = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE SUBMITING" & vbNewLine &
                                   "YOUR REQUEST. PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                             "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)

                        Case "MRG03"

                            If lastTransNo = "" Or lastTransNo.Length < 19 Then

                            Else
                                procCodeLen1 = lastTransNo.Substring(0, 7)
                                procCodeLen2 = lastTransNo.Substring(9, 10)
                                procCodeLen3 = procCodeLen1 & "WR" & procCodeLen2
                            End If

                            note = "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                    "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                    "AT THE SSS BRANCH."

                            class1.prt(class1.prtValidation(note, fullnameprint, xtd.getCRN, procCodeLen3, "SIMPLIFIED WEB REGISTRATION", "Print"), DefaultPrinterName)


                    End Select


                End If
            Case "13" 'update contact info is disabled due to url approach
                'Dim class1 As New PrintHelper
                'class1.prt(class1.prtUpdateContactInformation_Receipt(fullnameprint, SSStempFile, UpdateCntctInfo_SuccessTxnNo, "UPDATE CONTACT INFORMATION", "Print"), DefaultPrinterName)
                'class1 = Nothing
            Case Else
                MsgBox("SORRY, YOU HAVE NO DETAILS TO PRINT.", MsgBoxStyle.Information, "Information")
        End Select

    End Sub

#End Region

    Private Sub WarningBox1_CloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarningBox1.CloseClick
        WarningBox1.Hide()
        WarningBox2.Hide()
        WarningBox3.Hide()
    End Sub

    Private Sub WarningBox2_CloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarningBox2.CloseClick
        WarningBox1.Hide()
        WarningBox2.Hide()
        WarningBox3.Hide()
    End Sub

    Private Sub WarningBox3_CloseClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles WarningBox3.CloseClick
        WarningBox1.Hide()
        WarningBox2.Hide()
        WarningBox3.Hide()
    End Sub

    Private Sub btnUpdateContactInfo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUpdateContactInfo.Click
        Try
            CleanFormSession({_frmUpdCntcInfv2})

            If SharedFunction.DisablePinOrFingerprint Then
                UpdateContactInformationv3()
            Else
                CurrentTxnType = TxnType.UpdateContactInformation
                xtd.getRawFile()
                _frm2.CardType = CShort(xtd.checkFileType)
                ShowPanelForm(_frm2)
            End If
        Catch ex As Exception
            ShowErrorForm("Update Contact Information", ex.Message)
        End Try
    End Sub

    Public Function IsAllowedToPrint() As Boolean
        Dim getTotalPrintPerDay As String = "1"
        Dim fileTYPerr1 As Integer = xtd.checkFileType
        If fileTYPerr1 = 1 Then
            getTotalPrintPerDay = db.putSingleNumber("SELECT count(SSNUM) from SSTRANSPRNTCNT where SSNUM = '" & xtd.getCRN & "' and ENCODE_DT = '" & Date.Today.ToShortDateString & "' ")
        ElseIf fileTYPerr1 = 2 Then
            getTotalPrintPerDay = db.putSingleNumber("SELECT count(SSNUM) from SSTRANSPRNTCNT where SSNUM = '" & SSStempFile & "' and ENCODE_DT = '" & Date.Today.ToShortDateString & "' ")
        End If

        If getTotalPrintPerDay = 1 Then
            WarningBox1.Show()
            WarningBox1.Visible = True
            WarningBox2.Visible = False
            WarningBox3.Visible = False
        ElseIf getTotalPrintPerDay = 2 Then
            WarningBox2.Show()
            WarningBox1.Visible = False
            WarningBox2.Visible = True
            WarningBox3.Visible = False

        ElseIf getTotalPrintPerDay = 3 Then
            WarningBox3.Show()
            WarningBox1.Visible = False
            WarningBox2.Visible = False
            WarningBox3.Visible = True
        ElseIf getTotalPrintPerDay = 4 Then
            WarningBox1.Hide()
            WarningBox2.Hide()
            WarningBox3.Hide()
        End If

        Dim maxPrint As Integer = db.putSingleNumber("select COUNT_PRINT from SSINFOKIOSKPRNT")
        If getTotalPrintPerDay >= maxPrint Then
            WarningBox1.Hide()
            WarningBox2.Hide()
            WarningBox3.Hide()
            MsgBox("SORRY, YOU HAVE REACHED THE MAXIMUM NUMBER OF FIVE (" & maxPrint & ") PRINT OUTS PER DAY.", MsgBoxStyle.Information, "Information")
            Return False
        Else
            Return True
        End If
    End Function

    'Private Sub getCurrVersionIE()
    '    'revised by edel on Sept 20, 2019. to handle ie 11 requirement()
    '    Dim BrowserKeyPath As String = Microsoft.Win32.Registry.CurrentUser.ToString & "\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION"
    '    'Dim basekey As String = Microsoft.Win32.Registry.CurrentUser.ToString
    '    Dim value As Int32
    '    Dim thisAppsName As String = "*"

    '    value = "11001"

    '    'Microsoft.Win32.Registry.SetValue(Microsoft.Win32.Registry.CurrentUser.ToString & BrowserKeyPath, "*", value, Microsoft.Win32.RegistryValueKind.DWord)
    '    Microsoft.Win32.Registry.SetValue(BrowserKeyPath, "*", value, Microsoft.Win32.RegistryValueKind.DWord)
    'End Sub

    Public Sub AddRemoveIEVersion(ByVal isAdd As Boolean)
        Dim BrowserKeyPath As String = Microsoft.Win32.Registry.CurrentUser.ToString & "\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION"
        Dim thisAppsName As String = "*"
        Dim value As Int32 = "11001"

        If isAdd Then
            Microsoft.Win32.Registry.SetValue(BrowserKeyPath, thisAppsName, value, Microsoft.Win32.RegistryValueKind.DWord)
        Else
            Dim key As Microsoft.Win32.RegistryKey
            key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", True)
            key.DeleteValue(thisAppsName)
        End If
    End Sub

    Public Sub PopulateWebservicesLinks()
        'added by edel on 10/17/2018 for PRN Webservice requirement
        Dim DAL As New DAL_Mssql
        If DAL.SelectQuery("SELECT * FROM tbl_SSS_Webservice") Then
            If DAL.TableResult.DefaultView.Count = 0 Then
                SharedFunction.ShowErrorMessage("FAILED TO GET PRN WEBSERVICE PARAMETERS")
            Else
                Dim rw As DataRow = DAL.TableResult.Rows(0)
                MobileWS2BeanService_URL = rw("MobileWS2BeanService_URL").ToString.Trim
                MobileWS2BeanService_Token = rw("MobileWS2BeanService_Token").ToString.Trim
                MobileWS2BeanService_SessionToken = MobileWS2BeanService_Token
                IPRNImplService_URL = rw("IPRNImplService_URL").ToString.Trim
                IPRNImplService_Token = rw("IPRNImplService_Token").ToString.Trim
                IPRNImplService_SessionToken = IPRNImplService_Token
                UpdateCntctInfoService_URL = rw("UpdateCntctInfoService_URL").ToString.Trim
                UpdateCntctInfoService_Token = rw("UpdateCntctInfoService_Token").ToString.Trim
                UpdateCntctInfoService_SessionToken = UpdateCntctInfoService_Token
                UpdateCntctInfoTokenGenerator_URL = rw("UpdateCntctInfoTokenGenerator_URL").ToString.Trim

                EligibilityWebserviceImplService_URL = rw("EligibilityWebserviceImplService_URL").ToString.Trim
                EligibilityWebserviceImplService_Token = rw("EligibilityWebserviceImplService_Token").ToString.Trim
                SimpleWebRegistrationService_URL = rw("SimpleWebRegistrationService_URL").ToString.Trim
                SimpleWebRegistrationService_Token = rw("SimpleWebRegistrationService_Token").ToString.Trim
                BankWorkflowWebService_URL = rw("BankWorkflowWebService_URL").ToString.Trim
                BankWorkflowWebService_Token = rw("BankWorkflowWebService_Token").ToString.Trim
                DisclosureWebserviceImplService_URL = rw("DisclosureWebserviceImplService_URL").ToString.Trim
                DisclosureWebserviceImplService_Token = rw("DisclosureWebserviceImplService_Token").ToString.Trim
                UmidService_URL = rw("UmidService_URL").ToString.Trim
                UmidService_Token = rw("UmidService_Token").ToString.Trim
                OnlineRetirementWebServiceImplService_URL = rw("OnlineRetirementWebServiceImplService_URL").ToString.Trim
                OnlineRetirementWebServiceImplService_Token = rw("OnlineRetirementWebServiceImplService_Token").ToString.Trim

                ACOPMonthPensionHistory_URL = rw("ACOPMonthPensionHistory_URL").ToString.Trim
                ACOPMonthPensionHistory_UserPass = rw("ACOPMonthPensionHistory_UserPass").ToString.Trim

                SSSListOfBranches_URL = rw("SSSListOfBranches_URL").ToString.Trim
                RetirementBenefitDocReq_URL = rw("RetirementBenefitDocReq_URL").ToString.Trim
            End If
        End If
        DAL.Dispose()
        DAL = Nothing
    End Sub

    Private Sub _frmMainMenu_Resize(sender As Object, e As EventArgs) Handles Me.Resize
        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            'Panel7.Width = Panel7.Width - 5
            'btnLogout.Width = btnLogout.Width - 5
            'Panel14.Width = Panel14.Width - 5
            'Panel15.Width = Panel15.Width - 5
            'Panel17.Width = Panel17.Width - 5

            Button4.Left = Button4.Left - 4
            Button4.Width -= 8
            Button4.Font = New Font(Button4.Font.Name, 13)
            lblMonth.Left = lblMonth.Left - 20
            lblDate.Left = lblDate.Left - 20
            lblDay.Left = lblDay.Left - 20
            picFooterCal.Left = lblMonth.Left

            splitContainerControl.SplitterDistance = 157
        End If
    End Sub

    Dim worker As New System.ComponentModel.BackgroundWorker

    Private Sub CheckTerminalConnections()
        If Not worker.IsBusy Then worker.RunWorkerAsync()
    End Sub

    Private Sub CheckTerminalConnections_DoWork(ByVal sender As System.Object,
                         ByVal e As System.ComponentModel.DoWorkEventArgs)
        'Dim stroutput As String = e.Argument.ToString
        Dim response As Short = SharedFunction.CheckTerminalConnections(xs, db)
        'e.Result = String.Format("{0}|{1}", e.Argument.ToString, response)
        e.Result = response
    End Sub

    Private Sub CheckTerminalConnections_Complete(ByVal sender As Object,
                                   ByVal e As System.ComponentModel.RunWorkerCompletedEventArgs)
        Dim response As String = e.Result.ToString
        If response = "0" Then
            UmidService_URL = db.putSingleValue("select UmidService_URL from tbl_SSS_Webservice")
            UmidService_Token = db.putSingleValue("select UmidService_Token from tbl_SSS_Webservice")
        Else
            SharedFunction.ShowWarningMessage("THE SYSTEM IS NOT CONNECTED TO THE REMOTE SERVER. PLEASE MAKE SURE THE NETWORK CABLE IS CONNECTED THEN RESTART THE APPLICATION.")
        End If
    End Sub

    Private Sub MenuButtonsFontSize()
        Dim sze As Integer = 8
        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.nineteenInch Then sze = 12

        btnInquiry.Font = New Font(btnInquiry.Font.Name, sze, btnInquiry.Font.Style)
        btnRegistration.Font = New Font(btnRegistration.Font.Name, sze, btnRegistration.Font.Style)
        btnLoan.Font = New Font(btnLoan.Font.Name, sze, btnLoan.Font.Style)
        btnMaternity.Font = New Font(btnMaternity.Font.Name, sze, btnMaternity.Font.Style)
        btnRetirement.Font = New Font(btnRetirement.Font.Name, sze, btnRetirement.Font.Style)
        btnACOP.Font = New Font(btnACOP.Font.Name, sze, btnACOP.Font.Style)
        btnUpdateContactInfo.Font = New Font(btnUpdateContactInfo.Font.Name, sze, btnUpdateContactInfo.Font.Style)
        btnPRN.Font = New Font(btnPRN.Font.Name, sze, btnPRN.Font.Style)
        btnChangeUmidPin.Font = New Font(btnChangeUmidPin.Font.Name, sze, btnChangeUmidPin.Font.Style)
        btnMDetails.Font = New Font(btnMDetails.Font.Name, sze, btnMDetails.Font.Style)
        btnEHistory.Font = New Font(btnEHistory.Font.Name, sze, btnEHistory.Font.Style)
        btnApremiums.Font = New Font(btnApremiums.Font.Name, sze, btnApremiums.Font.Style)
        btnMonthlyPension.Font = New Font(btnMonthlyPension.Font.Name, sze, btnMonthlyPension.Font.Style)
        btnFFund.Font = New Font(btnFFund.Font.Name, sze, btnFFund.Font.Style)
        btnClearance.Font = New Font(btnClearance.Font.Name, sze, btnClearance.Font.Style)
        btnlStatus.Font = New Font(btnlStatus.Font.Name, sze, btnlStatus.Font.Style)
        btnBClaims.Font = New Font(btnBClaims.Font.Name, sze, btnBClaims.Font.Style)
        btnMaternityClaim.Font = New Font(btnMaternityClaim.Font.Name, sze, btnMaternityClaim.Font.Style)
        btnSClaims.Font = New Font(btnSClaims.Font.Name, sze, btnSClaims.Font.Style)
        btnELIGIBILITY.Font = New Font(btnELIGIBILITY.Font.Name, sze, btnELIGIBILITY.Font.Style)
        btnLoans.Font = New Font(btnLoans.Font.Name, sze, btnLoans.Font.Style)
        btnSickness.Font = New Font(btnSickness.Font.Name, sze, btnSickness.Font.Style)
        btnFLoans.Font = New Font(btnFLoans.Font.Name, sze, btnFLoans.Font.Style)
        btnDocs.Font = New Font(btnDocs.Font.Name, sze, btnDocs.Font.Style)
        btnDeathClaims.Font = New Font(btnDeathClaims.Font.Name, sze, btnDeathClaims.Font.Style)
        btnDisabiltyClaims.Font = New Font(btnDisabiltyClaims.Font.Name, sze, btnDisabiltyClaims.Font.Style)
        btnEC.Font = New Font(btnEC.Font.Name, sze, btnEC.Font.Style)
        btnFuneralClaims.Font = New Font(btnFuneralClaims.Font.Name, sze, btnFuneralClaims.Font.Style)
        btnMaternityClaims.Font = New Font(btnMaternityClaims.Font.Name, sze, btnMaternityClaims.Font.Style)
        btnRetireClaims.Font = New Font(btnRetireClaims.Font.Name, sze, btnRetireClaims.Font.Style)
        btnSicknessClaims.Font = New Font(btnSicknessClaims.Font.Name, sze, btnSicknessClaims.Font.Style)
    End Sub

End Class