Imports System
Imports System.Xml
Imports System.Configuration
Imports System.Reflection
Imports System.IO
Imports System.Xml.XPath
Imports Oracle.DataAccess.Client
Module modSettings
    Public xml_Filename As String = Application.StartupPath & "\MySettings.xml" 'Mysettings.xml
    Public xml_path = "Configuration/Settings"
    Public kioskBranch As String
    Public userType As Integer
    Public kioskCluster, kioskGroup As String
    Public lastOnline, kioskID, kioskIP, kioskBranchCD As String
    Dim db As New ConnectionString
    Public getPermanentURL As String
    Public SSStempFile As String
    Public tagPage As String
    Public getURL As String
    Public kioskName As String
    Public transTag As String
    Public getErNo As String
    Public getNameEr As String
    Public printTag As String
    Public errorTag As String
    Public lastOffline As String
    Public getAdd As Integer = 0
    Public lastTransNo As String
    Public webPageTag As String
    Public BenefitTag1 As String
    Public getLogsWeb As String
    Public authentication As String
    Public db_DSN, db_Name, db_Pass, db_server, db_UName As String
    Public db_Host, db_Port, db_ServiceName, db_UserID, db_Password As String
    Public UMID_settings As Integer '= readSettings(xml_Filename, xml_path, "UMID")
    Public SAM_settings As Integer '= readSettings(xml_Filename, xml_path, "SAM")
    Public PIN_settings As String '= readSettings(xml_Filename, xml_path, "PIN")
    Public CHECK_MEMSTATUS_Settings As String
    Public CardPIN As String 'from the settings
    Public isVPN As Boolean
    Public txnName As String
    Public txnTypeRcpt As String
    Public crn_1 As String
    Public getbranchCoDE_1 As String
    Public cardType As String
    Public _NotfirstRun As Boolean
    Public CoveredStatus = ""
    'Public isVPN As 
    Public CurrentTxnType As TxnType
    Public TxnAuthenticationResult As Boolean

    Public AppletVersion As String

    Public WS_SSNo As String = ""
    Public WS_ErNum As String = ""
    Public WS_LoanType As String = "S"
    Public WS_PrevBalance As Double = 0
    Public WS_LoanAmount As Double = 0
    Public WS_MaxLoanableAmount As Double = 0
    Public WS_NetLoanAmount As Double = 0
    Public WS_LoanableMonth As Integer = 0
    Public WS_InstallmentTerm As Integer = 2
    Public WS_UrIds As String = ""
    Public WS_PrevLoanAmount As Double = 0
    Public WS_ServiceCharge As Double = 0
    Public WS_ErSeqNo As String = ""
    Public WS_Address As String = ""
    Public WS_TransID_TokenID As String = ""

    Public WS_MemberStatus As String = ""

    Public WS_CD_LoanAmount As String = ""
    Public WS_CD_LoanBalance As String = ""
    Public WS_CD_NetProceeds As String = ""
    Public WS_CD_MonthlyAmort As String = ""

    Public WS_CD_PDF_Path As String = ""

    Public PRN_MemberWithPRN As Boolean = False
    Public PRN As String = ""
    Public PRN_MembershipType As String = ""
    Public PRN_Period_From As String = ""
    Public PRN_Period_To As String = ""
    Public PRN_MonthlyContribution As String = "0.00"
    Public PRN_TotalAmount As String = "0.00"
    Public PRN_DueDate As String = ""
    Public PRN_PDF As String = ""
    Public PRN_FlexiFund As String = "0.00"

    Public MobileWS2BeanService_URL As String = "" '"https://wws.sss.gov.ph/MobileWS2Bean/MobileWS2BeanService?wsdl"
    Public MobileWS2BeanService_Token As String = "" '"TestingSSIT"
    Public MobileWS2BeanService_SessionToken As String = "" '"TestingSSIT"

    Public IPRNImplService_URL As String = "" '"https://wws.sss.gov.ph/testsevm/IPRNImplService?WSDL"
    Public IPRNImplService_Token As String = "" '"kWTBXc01KCN75EwvPjvG368llPgnxa3Am2mCq9eZVT0sSBORuJ"
    Public IPRNImplService_SessionToken As String = "" '"kWTBXc01KCN75EwvPjvG368llPgnxa3Am2mCq9eZVT0sSBORuJ"

    Public TableMemberPRNApplication As DataTable = Nothing

    Public UpdateCntctInfoService_URL As String = "" '"http://m01ws800:7101/rcsoap/memberws?wsdl"
    Public UpdateCntctInfoService_Token As String = "" '"ofqgw5gDomKEwY1n3uT1FsF5B6GLMdjNYE1zvwhARFo6wQ2iKi"
    Public UpdateCntctInfoService_SessionToken As String = "" 'UpdateCntctInfoService_Token

    Public UpdateCntctInfoTokenGenerator_URL As String = "" 'http://10.0.4.252:3014/TokenGeneration/TokenDetailsPort?WSDL

    Public EligibilityWebserviceImplService_URL As String = "" '"https://ww8.sss.gov.ph/eligibwebservice/EligibilityWebserviceImplService?wsdl"
    Public EligibilityWebserviceImplService_Token As String = "" '"WESHJZ1Q103017102439"
    Public EligibilityWebserviceImplService_SessionToken As String = ""

    Public SimpleWebRegistrationService_URL As String = "" '"http://10.0.4.252:3014/SWR/SimpleWebRegistrationService?WSDL"
    Public SimpleWebRegistrationService_Token As String = "" '"testing"
    Public SimpleWebRegistrationService_SessionToken As String = ""

    Public BankWorkflowWebService_URL As String = "" '"http://m41sv145:7009/testbemws/BankWorkflowWebServiceImplPort?WSDL"
    Public BankWorkflowWebService_Token As String = "" '"BhIUlWLlFxbIBoLQPeK0Qrh1vuHKocNGC8itwbm1yBlMsrSpPn"
    Public BankWorkflowWebService_SessionToken As String = ""

    Public DisclosureWebserviceImplService_URL As String = "" '"https://ww8.sss.gov.ph/webcallingds/DisclosureWebserviceImplService?WSDL"
    Public DisclosureWebserviceImplService_Token As String = "" '"WESHJZ1Q103017102439"
    Public DisclosureWebserviceImplService_SessionToken As String = ""

    Public UmidService_URL As String = "" '"http://m41sv145:7009/TestUmidWSBean/UmidService?WSDL"
    Public UmidService_Token As String = "" '"TestingSSIT"
    Public UmidService_SessionToken As String = ""

    Public OnlineRetirementWebServiceImplService_URL As String = "" '"http://m41sv145:7009/TestUmidWSBean/UmidService?WSDL"
    Public OnlineRetirementWebServiceImplService_Token As String = "" '"TestingSSIT"
    Public OnlineRetirementWebServiceImplService_SessionToken As String = ""

    Public SSSListOfBranches_URL As String = "" '"https://member.sss.gov.ph/members/portlets/members/salaryLoanApplication/list_branches.jsp"
    Public RetirementBenefitDocReq_URL As String = "" '"https://www.sss.gov.ph/sss/DownloadContent?fileName=Benepisyo_sa_Pagreretiro_July_29_2019.pdf"

    Public ACOPMonthPensionHistory_URL As String = "" '"http://10.0.4.252:8888/sss-internalpp/"
    Public ACOPMonthPensionHistory_UserPass As String = "" '"c06isi06|userws"

    Public UpdateCntctInfo_SuccessTxnNo As String = ""

    Public newGeneratedPRN As String = ""

    Public ContributionListPRN_MaxValue As Decimal = 2600
    Public cboContributionListPRN As ComboBox = Nothing

    Public tacFile As String = Application.StartupPath & "\terms_and_conditions\terms_and_conditions.pdf"
    Public technicalRetirementReqDoc As String = Application.StartupPath & "\techRetirementReqDoc.pdf"

    Public authenticationMsg As String

    Public isShowFutronic As Boolean
    Public umidCard As umid
    Public isGSISCard As Boolean

    Public pblcSC2 As UMIDLibrary.AllCardTech_Smart_Card

    Enum TxnType
        SimplifiedWebRegistration = 1
        ApplicationForSalaryLoanMember
        ApplicationForSalaryLoanEmployed
        MaternityNotification
        TechnicalRetirementPen
        TechnicalRetirementLumpSum
        AcopDep
        AcopNoDep
        PensionMaintenance
        PinChange
        UpdateContactInformation
        OnlineRetirement
    End Enum

    Public Function editSettings(ByVal xml_Filename As String, ByVal xml_path As String, ByVal var_name As String, ByVal value_name As String)

        Try
            Dim xd As New XmlDocument()
            xd.Load(xml_Filename)

            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & var_name)  '"Configuration/Settings/FirstName"
            If nod IsNot Nothing Then
                nod.InnerXml = value_name
            Else
                nod.InnerXml = value_name
            End If

            xd.Save(xml_Filename)
            xd = Nothing
        Catch ex As Exception

        End Try

    End Function

    Public Function readSettings(ByVal xml_filename As String, ByVal xml_path As String, ByVal var_name As String) As String
        Dim return_value As String
        Try
            Dim a As String
            Dim xd As New XmlDocument
            xd.Load(xml_filename)
            Dim nod As XmlNode = xd.SelectSingleNode(xml_path & "/" & var_name)
            If nod IsNot Nothing Then
                a = nod.InnerXml
                return_value = a
            Else
                return_value = Nothing
            End If
            'xd.Save(xml_filename)

            xd = Nothing
        Catch ex As Exception
            return_value = ex.Message
        End Try
        Return return_value

    End Function

    Public Sub getKioskDetails()
        kioskBranch = readSettings(xml_Filename, xml_path, "kioskBranch")
        kioskCluster = readSettings(xml_Filename, xml_path, "kiosk_cluster")
        kioskGroup = readSettings(xml_Filename, xml_path, "kiosk_group")
        kioskID = readSettings(xml_Filename, xml_path, "kioskID")
        kioskIP = readSettings(xml_Filename, xml_path, "kioskIP")
        kioskName = readSettings(xml_Filename, xml_path, "kioskName")
        getbranchCoDE_1 = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
        'UMID_settings = readSettings(xml_Filename, xml_path, "UMID")
        'SAM_settings = readSettings(xml_Filename, xml_path, "SAM")
        PIN_settings = readSettings(xml_Filename, xml_path, "PIN")
        'CardPIN = readSettings(xml_Filename, xml_path, "CardPin")
    End Sub


    Public Function checkConnection()
        Dim IsConnected As Boolean


        db_DSN = readSettings(xml_Filename, xml_path, "db_DSN")
        db_server = readSettings(xml_Filename, xml_path, "db_Server")
        db_Name = readSettings(xml_Filename, xml_path, "db_Name")
        db_UName = readSettings(xml_Filename, xml_path, "db_UName")
        db_Pass = readSettings(xml_Filename, xml_path, "db_Pass")
        Dim connstring1 As String = "DSN=" & db_DSN & ";SERVER=" & db_server & ";DATABASE=" & db_Name & ";UID=" & db_UName & ";PWD=" & db_Pass & ""
        Dim webConnected As Boolean = db.webisconnected(connstring1)

        If webConnected = False Then

        End If

    End Function

    Public Sub msgFailedAuth(ByVal tagPage1 As String)
        GC.Collect()
        authentication = "UA02"
        tagPage = tagPage1
        _frmMainMenu.btnPrint.PerformClick()
        tagPage = tagPage1
        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmUserAuthentication.TopLevel = False
        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmUserAuthentication.Dock = DockStyle.Fill
        _frmUserAuthentication.Show()
        _frmMainMenu.BackNextControls(False)
        '_frmMainMenu.Button5.Enabled = False
        '_frmMainMenu.Button6.Enabled = False
        '_frmMainMenu.Button5.Text = "BACK"
        '_frmMainMenu.Button6.Text = "NEXT"
        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

        _frmMainMenu.PrintControls(True)
        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = txnName

        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
        Dim transDesc As String = "THE SYSTEM HAS FAILED TO AUTHENTICATE THE FINGERPRINT."
        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(crn_1 & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
        End Using
        '_frm2.Dispose()
        '_frm2.Close()
    End Sub

    Public Function getworkstation()
        Try
            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand
            dbConn.Open()

            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
            '  dbComm.Parameters.Add("SSSCRN", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
            ' dbComm.Parameters("SSSCRN").Value = xtd.getCRN
            dbComm.CommandText = "PKG_IKIOSK.GET_HNAME"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()

            Dim workstation As String = dbComm.Parameters("msg").Value.ToString


            Return workstation
        Catch ex As Exception
            Return ex.Message
        End Try
     
    End Function

    'Public Function checkIfVPN()
    '    Dim firstRun As String = readSettings(xml_Filename, xml_path, "firstRun")

    '    Dim kiosk_Ip As String = xs.getIPAddress()

    '    If kiosk_Ip.Contains("192.168") Or Not kiosk_Ip.Contains("10.0") Then
    '        If firstRun <> "3" Then
    '            lblStatus.Visible = True
    '            lblStatus.Text = "* Please Contact Administrator to set kiosk settings! "
    '        End If
    '        isVPN = True
    '    Else
    '        ' If firstRun <> "3" Then 
    '        isVPN = False
    '        xs.getSQL()
    '        xs.getORACLE()
    '        xs.getKioskDetails(xs.getIPAddress)
    '        getKioskDetails()
    '        Me.Hide()
    '        SharedFunction.HomeScreen()
    '        'End If
    '    End If
    'End Function
    Public Sub backUpSettings()
        Try
            Dim pathA As String = Application.StartupPath & "\MySettings.xml" '  source filename
            Dim pathB As String = Application.StartupPath & "\Backup\MySettings.xml" '  destination file name
            ' If Not File.Exists(pathB) Then
            System.IO.File.Delete(pathB)
            System.IO.File.Copy(pathA, pathB)
            ' End If

        Catch ex As Exception

        End Try

    End Sub



End Module
