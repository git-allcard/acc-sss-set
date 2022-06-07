Imports System
Imports System.Xml
Imports System.Configuration
Imports System.Reflection
Imports System.IO
Imports System.Xml.XPath
Public Class MySettings
    Dim db As New ConnectionString
    Dim xml_Filename As String = Application.StartupPath & "\MySettings.xml"   '"Mysettings.xml"
    Dim xml_path = "Configuration/Settings"

    Public Shared settings As New XmlWriterSettings()
    ' var_name  = name in the setting.
    ' value_nanme =  the value of the specified setting.
    'xml_filename  =  the filename of the settings.
    ' xml_path  = absoulute path.

    ' writeSettings =  write the file and variables in it.
    ' editSettings = edit values of the variables
    ' readSettings =  read the value of the variable.

    ' Public db_Host, db_Port, db_ServiceName, db_UserID, db_Password As String
  
#Region "Settings"
    Public Sub writeSettings(ByVal xml_Filename As String)
        ' Dim doc As XmlDocument = loadconfig


        'File.Delete(xml_Filename)
        If Not File.Exists(xml_Filename) Then
            Dim XmlWrt As XmlWriter = XmlWriter.Create(xml_Filename, settings)


            With XmlWrt
                .WriteStartDocument()

                .WriteComment("XML Settings.")
                .WriteStartElement("Configuration")

                .WriteStartElement("Settings")


                ' START DECLARING VARIABLES FOR THE SETTINGS ...

                .WriteStartElement("getURL")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("dbSettingPath")
                .WriteString("D:\SSIT\Settings\")
                .WriteEndElement()

                .WriteStartElement("tagPage")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("webPageTag")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getPermanentURL")
                .WriteString("http://prs:7777/sss-ssitserve/")
                .WriteEndElement()

                .WriteStartElement("tagInquiry")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("BenefitTag1")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("Setting")
                .WriteString("Dsn=SSIT;uid=sa;pwd=password2013")
                .WriteEndElement()

                .WriteStartElement("BenefitTag2")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getLogsWeb")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("autoGenID")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("lastIDmaternity")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getAdd")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("pageIncremement")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("sssWebsiteLink")
                .WriteString("https://www.sss.gov.ph")
                .WriteEndElement()

                .WriteStartElement("kioskID")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("kioskIP")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("kioskName")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("db_Server")
                .WriteString("M01SV431\SSIT")
                .WriteEndElement()

                .WriteStartElement("db_UName")
                .WriteString("allcard")
                .WriteEndElement()

                .WriteStartElement("db_Pass")
                .WriteString("HelloSSS#88")
                .WriteEndElement()

                .WriteStartElement("db_Name")
                .WriteString("SSIT_SERVER")
                .WriteEndElement()

                .WriteStartElement("db_DSN")
                .WriteString("ssit")
                .WriteEndElement()

                .WriteStartElement("kioskBranch")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("lastOnline")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("lastOffline")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("firstRun")
                .WriteString("3")
                .WriteEndElement()

                .WriteStartElement("userType")
                .WriteString("1")
                .WriteEndElement()

                .WriteStartElement("printTag")
                .WriteString("0")
                .WriteEndElement()

                .WriteStartElement("db_Host")
                .WriteString("m01sv112")
                .WriteEndElement()

                .WriteStartElement("db_Port")
                .WriteString("1521")
                .WriteEndElement()

                .WriteStartElement("db_ServiceName")
                .WriteString("mo0cs1p")
                .WriteEndElement()

                .WriteStartElement("db_UserID")
                .WriteString("mo0ikw01")
                .WriteEndElement()

                .WriteStartElement("db_Password")
                .WriteString("Mo0ikw01_Ssit")
                .WriteEndElement()

                .WriteStartElement("kiosk_cluster")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("kiosk_group")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("authentication")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getUmidCardStatus")
                .WriteString("4")
                .WriteEndElement()

                .WriteStartElement("pathLoc")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("salaryLoanTag")
                .WriteString("0")
                .WriteEndElement()

                .WriteStartElement("getErNo")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getNameEr")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getEmpStatus")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("kioskAdminPass")
                .WriteString("allcardtech@2014!")
                .WriteEndElement()

                .WriteStartElement("kioskAdminUser")
                .WriteString("ssitsssgov")
                .WriteEndElement()

                .WriteStartElement("SSStempFile")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("printerPort")
                .WriteString("USB002")
                .WriteEndElement()

                .WriteStartElement("errorLoadTag")
                .WriteString("0")
                .WriteEndElement()

                .WriteStartElement("dateOfBirth")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("dateOfCov")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("lastTransNo")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("disclosureURL")
                .WriteString("http://prs:7777/")
                .WriteEndElement()

                .WriteStartElement("errorTag")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("transTag")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("getLumpSum")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("UMID")
                .WriteString("1")
                .WriteEndElement()

                .WriteStartElement("SAM")
                .WriteString("0")
                .WriteEndElement()

                .WriteStartElement("SmartCardReader")
                .WriteString("OMNIKEY CardMan 5x21-CL 0")
                .WriteEndElement()

                .WriteStartElement("CRN")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("PIN")
                .WriteString("123456")
                .WriteEndElement()

                .WriteStartElement("SSSCard_SessionTime")
                .WriteString("7")
                .WriteEndElement()

                .WriteStartElement("UMIDCard_SessionTime")
                .WriteString("12")
                .WriteEndElement()

                .WriteStartElement("webFeedEmail")
                .WriteString("onlineserviceassistance@sss.gov.ph")
                .WriteEndElement()

                .WriteStartElement("CHECK_MEMSTATUS")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("IsCRNJustActivated")
                .WriteString("False")
                .WriteEndElement()

                .WriteStartElement("lastContribution")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("CSN")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("CCDT")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("CardPin")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("CARD_DATA")
                .WriteString("1|0332501316|")
                .WriteEndElement()

                .WriteStartElement("BenefitTag2")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("BenefitTag1")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("authentication")
                .WriteString("")
                .WriteEndElement()

                .WriteStartElement("SS_Number")
                .WriteString("")
                .WriteEndElement()

                '.WriteStartElement("autoGenID")
                '.WriteString("")
                '.WriteEndElement()

                ' END DECLARING VARIABLES FOR THE SETTINGS ...

                .WriteEndDocument()
                .Close()
            End With
        End If
    End Sub
#End Region

    '#Region "Edit Settings"
    '    Public Function editSettings(ByVal xml_Filename As String, ByVal xml_path As String, ByVal var_name As String, ByVal value_name As String)

    '        Try
    '            Dim xd As New XmlDocument()
    '            xd.Load(xml_Filename)

    '            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & var_name)  '"Configuration/Settings/FirstName"
    '            If nod IsNot Nothing Then
    '                nod.InnerXml = value_name
    '            Else
    '                nod.InnerXml = value_name
    '            End If

    '            xd.Save(xml_Filename)
    '        Catch ex As Exception

    '        End Try

    '    End Function
    '#End Region
    '#Region "Read Settings"
    '    Public Function readSettings(ByVal xml_filename As String, ByVal xml_path As String, ByVal var_name As String) As String
    '        Dim return_value As String
    '        Try
    '            Dim a As String
    '            Dim xd As New XmlDocument
    '            xd.Load(xml_filename)
    '            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & var_name)
    '            If nod IsNot Nothing Then
    '                a = nod.InnerXml
    '                return_value = a
    '            Else
    '                return_value = Nothing
    '            End If
    '            xd.Save(xml_filename)
    '        Catch ex As Exception
    '            return_value = ex.Message
    '        End Try
    '        Return return_value

    '    End Function

    '#End Region

#Region "Setting Configuration"


#Region "Get SQL SETTINGS"
    Public Function getSQL()
        Dim siStr, siStr2, SQLdsn, SQLserver, SQLuid, SQLpassword, SQLdb As String

        Dim settingsInfo As String
        Dim downloadedPath As String
        downloadedPath = readSettings(xml_Filename, xml_path, "dbSettingPath")
        downloadedPath = downloadedPath & "SQLSettings.txt"

        Using SW As New IO.StreamReader(downloadedPath, False)
            settingsInfo = SW.ReadToEnd
            If settingsInfo <> "" Or settingsInfo <> Nothing Then

                Dim _split3 As String() = settingsInfo.Split(New Char() {"|"c})

                For J = 0 To _split3.Length - 1
                    siStr2 = _split3(J)

                    If siStr2.Contains("DSN") Then
                        siStr2 = siStr2.Trim
                        SQLdsn = siStr2.Remove(0, 5)
                    ElseIf siStr2.Contains("Server") Then
                        siStr2 = siStr2.Trim
                        SQLserver = siStr2.Remove(0, 8)
                    ElseIf siStr2.Contains("Username") Then
                        siStr2 = siStr2.Trim
                        SQLuid = siStr2.Remove(0, 10)
                    ElseIf siStr2.Contains("Password") Then
                        siStr2 = siStr2.Trim
                        SQLpassword = siStr2.Remove(0, 10)
                    ElseIf siStr2.Contains("Database") Then
                        siStr2 = siStr2.Trim
                        SQLdb = siStr2.Remove(0, 10)
                    End If

                Next
                SW.Close()


                editSettings(xml_Filename, xml_path, "db_Server", SQLserver)
                editSettings(xml_Filename, xml_path, "db_UName", SQLuid)
                editSettings(xml_Filename, xml_path, "db_Pass", SQLpassword)
                editSettings(xml_Filename, xml_path, "db_Name", SQLdb)
                editSettings(xml_Filename, xml_path, "db_DSN", SQLdsn)

                'editSettings(xml_Filename, xml_path, "firstRun", "1")

                'editSettings(xml_Filename, xml_path, "FirstRun", "1")




                'db.db_DSN = readSettings(xml_Filename, xml_path, "db_DSN")
                'db.db_server = readSettings(xml_Filename, xml_path, "db_Server")
                'db.db_Name = readSettings(xml_Filename, xml_path, "db_Name")
                'db.db_UName = readSettings(xml_Filename, xml_path, "db_UName")
                'db.db_Pass = readSettings(xml_Filename, xml_path, "db_Pass")

                '  getORACLE()
            End If
        End Using
    End Function


#End Region
#Region "Get ORACLE SETTINGS"
    Public Function getORACLE()
        Dim siStr, siStr2, oraHost, oraService, oraUID, oraPass, oraPort As String

        Dim settingsInfo As String
        Dim downloadedPath As String
        downloadedPath = readSettings(xml_Filename, xml_path, "dbSettingPath")
        downloadedPath = downloadedPath & "ORASettings.txt"

        Using SW As New IO.StreamReader(downloadedPath, False)

            settingsInfo = SW.ReadToEnd
            settingsInfo = settingsInfo.Trim
            'MsgBox(settingsInfo)
            If settingsInfo <> "" Or settingsInfo <> Nothing Then
                Dim _split3 As String() = settingsInfo.Split(New Char() {"|"c})

                For J = 0 To _split3.Length - 1
                    siStr2 = _split3(J)

                    If siStr2.Contains("Host") Then
                        siStr2 = siStr2.Trim
                        oraHost = siStr2.Remove(0, 6)
                    ElseIf siStr2.Contains("Port") Then
                        siStr2 = siStr2.Trim
                        oraPort = siStr2.Remove(0, 6)
                    ElseIf siStr2.Contains("UserID") Then
                        siStr2 = siStr2.Trim
                        oraUID = siStr2.Remove(0, 8)
                    ElseIf siStr2.Contains("Password") Then
                        siStr2 = siStr2.Trim
                        oraPass = siStr2.Remove(0, 10)
                    ElseIf siStr2.Contains("Service") Then
                        siStr2 = siStr2.Trim
                        oraService = siStr2.Remove(0, 9)

                    End If

                Next
                SW.Close()

                editSettings(xml_Filename, xml_path, "db_Host", oraHost)
                editSettings(xml_Filename, xml_path, "db_Port", oraPort)
                editSettings(xml_Filename, xml_path, "db_ServiceName", oraService)
                editSettings(xml_Filename, xml_path, "db_UserID", oraUID)
                editSettings(xml_Filename, xml_path, "db_Password", oraPass)

                db_Host = readSettings(xml_Filename, xml_path, "db_Host")
                db_Port = readSettings(xml_Filename, xml_path, "db_Port")
                db_ServiceName = readSettings(xml_Filename, xml_path, "db_ServiceName")
                db_UserID = readSettings(xml_Filename, xml_path, "db_UserID")
                db_Password = readSettings(xml_Filename, xml_path, "db_Password")
            Else
                db_Host = readSettings(xml_Filename, xml_path, "db_Host")
                db_Port = readSettings(xml_Filename, xml_path, "db_Port")
                db_ServiceName = readSettings(xml_Filename, xml_path, "db_ServiceName")
                db_UserID = readSettings(xml_Filename, xml_path, "db_UserID")
                db_Password = readSettings(xml_Filename, xml_path, "db_Password")

            End If

        End Using

        ' getIPAddress()
    End Function
#End Region

#Region "Get IP ADDRESS"

    Public Function getIPAddress() As String
        Try
            Dim strHostName As String
            Dim strIPadd As String


            strHostName = System.Net.Dns.GetHostName
            strIPadd = System.Net.Dns.GetHostByName(strHostName).AddressList(0).ToString


            getIPAddress = strIPadd

            'getIPAddress = "10.0.0.59"
            'getIPAddress = "10.0.0.57"
        Catch ex As Exception
            MsgBox("Not Connected")
        End Try



        '  getKioskDetails(getIPAddress)
    End Function
#End Region

    Public Function getKioskDetails(ByVal IPadd As String)
        'Dim kioskID, kioskIP, kioskName, kioskBranch, brnchCode, kioskCluster, clstrCode, kioskGroup, grpCode, kioskPrintPort As String

        Dim clstrCode, grpCode, kioskPrintPort

        kioskID = db.putSingleValue("select KIOSK_ID FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskName = db.putSingleValue("select KIOSK_NM  FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskIP = db.putSingleValue("select BRANCH_IP FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskBranchCD = db.putSingleValue("select BRANCH_CD FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        grpCode = db.putSingleValue("select DIVSN FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        clstrCode = db.putSingleValue("select clstr FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")


        kioskBranch = db.putSingleValue("select BRANCH_NM from SSINFOTERMBR where BRANCH_CD = '" & kioskBranchCD & "'")
        kioskCluster = db.putSingleValue("select CLSTR_NM from SSINFOTERMCLSTR where CLSTR_CD = '" & clstrCode & "'")
        kioskGroup = db.putSingleValue("select GROUP_NM from SSINFOTERMGROUP where GROUP_CD = '" & grpCode & "'")
        kioskPrintPort = "USB002"
        getbranchCoDE_1 = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")

        editSettings(xml_Filename, xml_path, "kioskIP", kioskIP)
        editSettings(xml_Filename, xml_path, "kioskID", kioskID)
        editSettings(xml_Filename, xml_path, "kioskName", kioskName)
        editSettings(xml_Filename, xml_path, "kioskBranch", kioskBranch)
        editSettings(xml_Filename, xml_path, "kiosk_cluster", kioskCluster)
        editSettings(xml_Filename, xml_path, "kiosk_group", kioskGroup)
        editSettings(xml_Filename, xml_path, "printerPort", kioskPrintPort)
        editSettings(xml_Filename, xml_path, "firstRun", "3")

        db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set TAG = '" & 1 & "' where KIOSK_NM ='" & kioskName & "'")
    End Function

    Public Function getKioskDetailsByHostName(ByVal IPadd As String)
        'Dim kioskID, kioskIP, kioskName, kioskBranch, brnchCode, kioskCluster, clstrCode, kioskGroup, grpCode, kioskPrintPort As String

        Dim clstrCode, grpCode, kioskPrintPort

        kioskID = db.putSingleValue("select KIOSK_ID FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskName = db.putSingleValue("select KIOSK_NM  FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskIP = db.putSingleValue("select BRANCH_IP FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        kioskBranchCD = db.putSingleValue("select BRANCH_CD FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        grpCode = db.putSingleValue("select DIVSN FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")
        clstrCode = db.putSingleValue("select clstr FROM SSINFOTERMKIOSK WHERE BRANCH_IP = '" & IPadd & "' ")


        kioskBranch = db.putSingleValue("select BRANCH_NM from SSINFOTERMBR where BRANCH_CD = '" & kioskBranchCD & "'")
        kioskCluster = db.putSingleValue("select CLSTR_NM from SSINFOTERMCLSTR where CLSTR_CD = '" & clstrCode & "'")
        kioskGroup = db.putSingleValue("select GROUP_NM from SSINFOTERMGROUP where GROUP_CD = '" & grpCode & "'")
        kioskPrintPort = "USB002"
        getbranchCoDE_1 = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")

        editSettings(xml_Filename, xml_path, "kioskIP", kioskIP)
        editSettings(xml_Filename, xml_path, "kioskID", kioskID)
        editSettings(xml_Filename, xml_path, "kioskName", kioskName)
        editSettings(xml_Filename, xml_path, "kioskBranch", kioskBranch)
        editSettings(xml_Filename, xml_path, "kiosk_cluster", kioskCluster)
        editSettings(xml_Filename, xml_path, "kiosk_group", kioskGroup)
        editSettings(xml_Filename, xml_path, "printerPort", kioskPrintPort)
        editSettings(xml_Filename, xml_path, "firstRun", "3")

        db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set TAG = '" & 1 & "' where KIOSK_NM ='" & kioskName & "'")
    End Function

    Public Function retrieveFile()
        Dim ifFirstRun As Boolean
        Dim statusSettings = CheckSettings(xml_Filename, xml_path, "runConfig")
        If statusSettings = "1" Then '  Corrupted My Settings
            Dim pathA As String = Application.StartupPath & "\Backup\MySettings.xml"  '  source filename
            Dim pathB As String = Application.StartupPath & "\MySettings.xml" '  destination file name
            If Not File.Exists(pathA) Then
                File.Delete(xml_Filename)
                writeSettings(xml_Filename)
                backUpSettings()
            Else
                System.IO.File.Delete(pathB)
                System.IO.File.Copy(pathA, pathB)
            End If
        Else ' not corrupted
            ' If ifFirstRun = True Then
            backUpSettings()
            'End If
        End If
        ifFirstRun = True
        Return ifFirstRun
    End Function

    Private Sub createFolder()
        Try
            'Dim Path1 As String
            'Path1 = (Application.StartupPath & "\Backup")
            'If (Not System.IO.Directory.Exists(Path1)) Then
            '    System.IO.Directory.CreateDirectory(Path1)
            'End If

            If Not System.IO.Directory.Exists(Application.StartupPath & "\Backup") Then _
                System.IO.Directory.CreateDirectory(Application.StartupPath & "\Backup")
        Catch ex As Exception

        End Try

    End Sub
    Public Function CheckSettings(ByVal xml_filame As String, ByVal xml_path As String, ByVal value_name As String) As String
        Dim return_value As String

        Dim a As String
        Dim xd As New XmlDocument
        createFolder()

        'inText = xd.
        Try
            xd.Load(xml_filame)
            return_value = "0"
        Catch ex As Exception

            return_value = "1" 'ex.Message

            ' MsgBox(ex.Message, MsgBoxStyle.Information, "READ SETTINGS")
            Dim pathA As String = Application.StartupPath & "\Backup\MySettings.xml"  '  source filename
            Dim pathB As String = Application.StartupPath & "\MySettings.xml" '  destination file name

            If File.Exists(pathB) Then
                System.IO.File.Delete(pathB)
                '  System.IO.File.Copy(pathA, pathB)
            End If

        End Try

        Return (return_value)

    End Function
#End Region
End Class
