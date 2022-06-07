Imports System.IO
Imports Oracle.DataAccess.Client

Public Class ExtractedDetails
    Dim fileName1 As String
    Dim rawFile, cardType As New RichTextBox
    Dim ListViewMerchant As New ListView
    Dim getMarker As String
    Public memInfo() As String
    Dim var As Long
    Dim getTempCRN As String
    Dim tagFilePath As Integer
    Dim DB As New ConnectionString
    Dim xs As New MySettings
    Public contDT As String
    Public Function checkFileType() As Integer
        Try
            Dim status As Integer = 0

            'getMarker = My.Settings.CARD_DATA.Substring(0, 1)
            getMarker = readSettings(xml_Filename, xml_path, "CARD_DATA")
            getMarker = getMarker.Substring(0, 1)
            Select Case getMarker

                Case 1
                    status = 1
                Case 2
                    status = 2
                Case 3
                    status = 3
                Case 4
                    status = 4
                Case 5
                    status = 5
                Case 6
                    status = 6
                Case Else
                    status = 0
            End Select

            Return status

            'For Each foundFile As String In My.Computer.FileSystem.GetFiles(My.Settings.filePath & "\")
            'cardType.LoadFile(foundFile, RichTextBoxStreamType.PlainText)

            'cardType.LoadFile("Temp\CARD_DATA", RichTextBoxStreamType.PlainText)
            'If cardType.Lines.Count = 0 Then
            'Else
            '    For Each s As String In cardType.Lines
            '        If s.ToString = "" Then
            '            'status = 0
            '        Else
            '            getMarker = s.Substring(0, 1)

            '            Select Case getMarker

            '                Case 1
            '                    status = 1
            '                Case 2
            '                    status = 2
            '                Case 3
            '                    status = 3
            '                Case 4
            '                    status = 4
            '                Case 5
            '                    status = 5
            '                Case 6
            '                    status = 6
            '                Case Else
            '                    status = 0
            '            End Select

            '        End If
            '    Next
            'End If
            ''Next

            'Return status
        Catch ex As Exception

        End Try
    End Function
    Public Sub getRawFile()
        Try


            Dim sum1 As String

            'Dim s2 As String = My.Settings.CARD_DATA
            Dim s2 As String = readSettings(xml_Filename, xml_path, "CARD_DATA")
            Dim _split2 As String() = s2.Split(New Char() {"|"c})
            ' Dim _splits2 As String

            ReDim memInfo(_split2.Length)
            For var = 1 To _split2.Length
                memInfo(var - 1) = _split2(var - 1).ToString
                '  Form1.ListBox1.Items.Add(memInfo(var - 1))

            Next



            tagFilePath = 1


        Catch ex As Exception
            ' MsgBox("error sa extracted")
        End Try
    End Sub

    Public Function getMemSSSNum(ByVal SSNUM As String)
        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand
        Dim getSSSall As String = SSNUM

        dbConn.Open()

        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("msg", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("CRNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

        dbComm.Parameters("CRNUM").Value = getSSSall
        dbComm.CommandText = "PKG_IKIOSK.GET_SSNUMBER"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim SSNUMBER1 As String = dbComm.Parameters("SSNUM").Value.ToString


        '  Return SSNUMBER1
        'getMemInfo1(SSNUMBER1)
    End Function



    Public Function getMemInfo1(ByVal SSNUM As String, ByVal fName As String, ByVal mName As String, ByVal lName As String, ByVal memDoB As String)
        Try
            Dim memStr
            Dim memStrEmpty
            Dim cData As String
            Dim memAdd, memPostal, memBrgy, memMncpl, memProv, memEml, memMob, memTel As String



            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("SSNUM2", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
            dbComm.Parameters("SSNUM2").Value = SSNUM
            dbComm.CommandText = "PR_IK_MEMINFO"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()

            Dim msgReturn As String = dbComm.Parameters("MSG").Value.ToString
            msgReturn = msgReturn.Trim

            'Dim sw As New StreamWriter(Application.StartupPath & "\PR_IK_MEMINFO.txt", True)
            'sw.WriteLine(Now.ToString("MM/dd/yy hh:mm:ss ") & SSNUM & vbTab & fName & vbTab & mName & vbTab & lName & vbTab & msgReturn)
            'sw.Close()
            'sw.Dispose()

            If msgReturn.Contains("no data found") Then
                Dim cData1 As String
                cData1 = fName & "|" & mName & "|" & lName & "|" & memDoB & "|" & "" & "|" & "" & "|" & "" & "|" & "" & _
               "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|"

                Dim card_info1 As String = readSettings(xml_Filename, xml_path, "CARD_DATA")

                ' My.Settings.CARD_DATA = card_info1 & cData1
                editSettings(xml_Filename, xml_path, "CARD_DATA", card_info1 & cData1)


                getRawFile()

            Else
                Dim _split3 As String() = msgReturn.Split(New Char() {"|"c})

                For J = 0 To _split3.Length - 1
                    memStr = _split3(J)

                    'If memStr.Contains("Member Address") Then
                    '    memStr = memStr.Trim
                    '    memAdd = memStr.Remove(0, 16)

                    'ElseIf memStr.Contains("Postal") Then
                    '    memStr = memStr.Trim
                    '    memPostal = memStr.Remove(0, 13)
                    'ElseIf memStr.Contains("Barangay") Then
                    '    memStr = memStr.Trim
                    '    memBrgy = memStr.Remove(0, 10)
                    'ElseIf memStr.Contains("Municipal") Then
                    '    memStr = memStr.Trim
                    '    memMncpl = memStr.Remove(0, 11)
                    'ElseIf memStr.Contains("Province") Then
                    '    memStr = memStr.Trim
                    '    memProv = memStr.Remove(0, 11)
                    'ElseIf memStr.Contains("Email") Then
                    '    memStr = memStr.Trim
                    '    memEml = memStr.Remove(0, 16)
                    'ElseIf memStr.Contains("Mobile") Then
                    '    memStr = memStr.Trim
                    '    memMob = memStr.Remove(0, 15)
                    'ElseIf memStr.Contains("Telephone") Then
                    '    memStr = memStr.Trim
                    '    memTel = memStr.Remove(0, 20)
                    'Else
                    '    memStr = memStr.Trim
                    '    memStrEmpty = ""
                    'End If

                    memStr = memStr.Trim

                    If memStr.Contains("Member Address") Then
                        memAdd = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Postal") Then
                        memPostal = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Barangay") Then
                        memBrgy = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Municipal") Then
                        memMncpl = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Province") Then
                        memProv = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Email") Then
                        memEml = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Mobile") Then
                        memMob = memStr.ToString.Split(":")(1).Trim
                    ElseIf memStr.Contains("Telephone") Then
                        memTel = memStr.ToString.Split(":")(1).Trim
                    Else
                        memStrEmpty = ""
                    End If

                Next

                Dim address2 As String = memBrgy & " " & memMncpl


                'cData = "|" & fName & "|" & mName & "|" & lName & "|" & "" & "|" & "" & "|" & "" & "|" & memAdd & "|" & "" & _
                '   "|" & "" & "|" & "" & "|" & address2 & "|" & memProv & "|" & memPostal & "|" & memPostal & "|" & "" & "|" & "" & "|" & "" & "|" & _
                '     "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & "" & "|" & _
                '"" & "|" & "" & "|" & "" & "|" & memEml & "|" & memMob & "|" & memTel & "|"

                cData = fName & "|" & mName & "|" & lName & "|" & memDoB & "|" & "" & "|" & "" & "|" & memAdd & "|" & "" & _
                 address2 & "|" & "" & "|" & memProv & "|" & memProv & "|" & memPostal & "|" & "" & "|" & "" & "|" & "" & "|" & memEml & "|" & memMob & "|" & memTel & "|" & "" & "|"

                ' Dim card_info As String = My.Settings.CARD_DATA
                Dim card_info As String = readSettings(xml_Filename, xml_path, "CARD_DATA")
                editSettings(xml_Filename, xml_path, "CARD_DATA", card_info & cData)

                'My.Settings.CARD_DATA = card_info & cData
                'My.Settings.Save()
                getRawFile()


            End If
        Catch ex As Exception
        End Try
    End Function

    Public Function rtTagPath() As Integer

        getRawFile()

        If tagFilePath = 0 Then
            tagFilePath = 0
        Else
            tagFilePath = 1
        End If

        Return tagFilePath
    End Function

    Public Function getCRN()
        If Not memInfo Is Nothing Then
            getCRN = memInfo(1)
        Else
            getCRN = HTMLDataExtractor.getCRN(_frmWebBrowser.WebBrowser1)
        End If

        Return getCRN
    End Function
    Public Function getfName()

        getfName = memInfo(2)
        getfName = getfName.Replace("�", ChrW(209))
        Return getfName
    End Function
    Public Function getmName()
        getmName = memInfo(3)
        'getmName = getmName.Replace("�", ChrW(209))
        Return getmName

    End Function
    Public Function getlName()
        getlName = memInfo(4)
        getlName = getlName.ToString.Replace(",", "")
        getlName = getlName.Replace("�", ChrW(209))
        Return getlName
    End Function
    Public Function getbDate()
        If Not memInfo Is Nothing Then
            getbDate = memInfo(5)
        Else
            getbDate = HTMLDataExtractor.GetDateBith()
        End If

        Return getbDate
    End Function
    Public Function getRoom()
        getRoom = memInfo(6)
        Return getRoom
    End Function

    Public Function getHouse()
        getHouse = memInfo(7)
        Return getHouse
    End Function
    Public Function getstName()
        getstName = memInfo(8)
        Return getstName
    End Function
    Public Function getSubd()
        getSubd = memInfo(9)
        Return getSubd
    End Function
    Public Function getBarangay()
        getBarangay = memInfo(10)
        Return getBarangay
    End Function

    Public Function getCity()
        getCity = memInfo(11)
        Return getCity
    End Function
    Public Function getProvince()
        getProvince = memInfo(12)
        Return getProvince
    End Function
    Public Function getPostalCode()
        getPostalCode = memInfo(13)
        Return getPostalCode
    End Function

    Public Function getmFirName()
        getmFirName = memInfo(14)
        Return getmFirName
    End Function
    Public Function getmMidName()
        getmMidName = memInfo(15)
        Return getmMidName
    End Function
    Public Function getmLasName()
        getmLasName = memInfo(16)
        Return getmLasName
    End Function
    Public Function getEmail()

        getEmail = memInfo(17)
        Return getEmail
    End Function
    Public Function getMobile()

        getMobile = memInfo(18)
        Return getMobile
    End Function
    Public Function getTelephone1()
        getTelephone1 = memInfo(19)
        Return getTelephone1
    End Function

    Public Function getFullname()
        getRawFile()
        getFullname = getlName() & ", " & getfName() & " " & getmName()

        Return getFullname

    End Function

    Public Function getTempSSS() As String
        getRawFile()
        getTempCRN = getCRN()

        If checkFileType() = 2 Then
            getTempCRN = Microsoft.VisualBasic.Right(getTempCRN, 10)
        ElseIf checkFileType() = 1 Then

        Else

        End If

        Return getTempCRN
    End Function

    Public Function getCRNumber() As String

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        'Dim dbConn As New OracleConnection
        Dim dbComm As OracleCommand
        ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
        dbConn.Open()
        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("MSG", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("SSSID", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

        dbComm.Parameters("SSSID").Value = getCRN()
        dbComm.CommandText = "PKG_IKIOSK.GET_CRNUMBER"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()
        Dim crnNumber As String = dbComm.Parameters("MSG").Value.ToString

        Return crnNumber

    End Function

    Public Function getSSSNumber() As String

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        'Dim dbConn As New OracleConnection
        Dim dbComm As OracleCommand
        ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
        dbConn.Open()
        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("MSG", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("CRNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
        dbComm.Parameters("CRNUM").Value = getCRN()
        dbComm.CommandText = "PKG_IKIOSK.GET_SSNUMBER"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()
        Dim SSSNumber As String = dbComm.Parameters("MSG").Value.ToString

        Return SSSNumber

    End Function

    Public Function cardInfo(ByVal SSNUM As String)

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim DtSet As System.Data.DataSet
        Dim conn As OracleConnection = New OracleConnection(MyConnection)
        Dim mycommand As New OracleDataAdapter
        mycommand = New OracleDataAdapter("select SSNUM,SURNM,GIVNM,MIDNM,DOBTH from RCEESTATICMF where ROWNUM < 1000000 AND ssnum ='" & SSNUM & "'", MyConnection)
        DtSet = New System.Data.DataSet
        mycommand.Fill(DtSet)

        For Each Drr As DataRow In DtSet.Tables(0).Rows

            Try

                'DB.ExecuteSQLQuery("Delete from TEMPSSSINFO where SSNUM = '" & SSNUM & "'")
                If DB.checkExistence("select ssnum from TEMPSSSINFO where ssnum = '" & SSNUM & "'  ") = True Then
                    DB.ExecuteSQLQuery("update TEMPOLDSSSDETAILS set SURNM = '" & Drr(1).ToString & "', GIVNM = '" & Drr(2).ToString & _
                                       "', MIDNM = '" & Drr(3).ToString & "', DOBTH = '" & Drr(4).ToString & "' where ssnum = '" & SSNUM & "'")
                Else
                    DB.sql = "INSERT INTO TEMPSSSINFO VALUES('" & Drr(0).ToString & "','" & Drr(1).ToString & "','" & Drr(2).ToString & "','" & Drr(3).ToString & "','" & Drr(4).ToString & "')"
                    DB.ExecuteSQLQuery(DB.sql)
                End If
            Catch ex As Exception

            End Try

        Next

    End Function


    Public Function getDependents(ByVal SSNUM As String) As String

        'Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SID=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        'Dim DtSet As System.Data.DataSet
        'Dim conn As OracleConnection = New OracleConnection(MyConnection)
        'Dim mycommand As New OracleDataAdapter
        'mycommand = New OracleDataAdapter("select SSNUM,NAME$,DPDOB from PNBENEFDEPMF where ROWNUM < 1000000 AND ssnum ='" & SSNUM & "'", MyConnection)
        'DtSet = New System.Data.DataSet
        'mycommand.Fill(DtSet)

        'For Each Drr As DataRow In DtSet.Tables(0).Rows

        '    Try

        '        DB.sql = "INSERT INTO TEMPSSSDEPD VALUES('" & Drr(0).ToString & "','" & Drr(1).ToString & "','" & Drr(2).ToString & "')"
        '        DB.ExecuteSQLQuery(DB.sql)

        '    Catch ex As Exception

        '    End Try

        'Next

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        'Dim dbConn As New OracleConnection
        Dim dbComm As OracleCommand
        ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
        dbConn.Open()
        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("SSNUM2", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
        dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

        dbComm.Parameters("SSNUM2").Value = SSNUM
        dbComm.CommandText = "PR_IK_ACOP_DEPEN"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim penMain As String = dbComm.Parameters("MSG").Value.ToString
        Return penMain
    End Function

    'Public Function getPensionDetails(ByVal webBrowserPath) As String

    '    Try
    '        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SID=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
    '        Dim DtSet As System.Data.DataSet
    '        Dim conn As OracleConnection = New OracleConnection(MyConnection)
    '        Dim mycommand As New OracleDataAdapter
    '        mycommand = New OracleDataAdapter("select SSNUM,SURNM,GIVNM,MIDNM,DOBTH from RCEESTATICMF where ROWNUM < 1000000 AND ssnum ='" & SSNUM & "'", MyConnection)
    '        DtSet = New System.Data.DataSet
    '        mycommand.Fill(DtSet)

    '        For Each Drr As DataRow In DtSet.Tables(0).Rows


    '            _frmMainMenu.MemdateOfBirth = ""
    '            _frmMainMenu.memberLastName = ""
    '            _frmMainMenu.memberFirstName = ""
    '            _frmMainMenu.memberMidName = ""
    '            _frmMainMenu.memSSNUM = ""
    '        Next



    '    Catch ex As Exception

    '    End Try

    '       Next

    'End Function


    Public Function chkPostalCode(ByVal pcode As String) As Integer

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand
        dbConn.Open()
        dbComm = dbConn.CreateCommand
        dbComm.CommandTimeout = 0
        dbComm.Parameters.Add("msg", OracleDbType.Long, 500, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("V_POSTALCODE", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

        dbComm.Parameters("V_POSTALCODE").Value = pcode
        dbComm.CommandText = "PKG_IKIOSK.CHECK_POSTALCODE"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim resultPostal As String = dbComm.Parameters("msg").Value.ToString
        If resultPostal = "EXISTS" Then
            chkPostalCode = 1
        Else
            chkPostalCode = 0
        End If

    End Function

    Public Function getMemStat(ByVal ssnum As String) As Integer

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand

        dbConn.Open()

        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("msg", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("SSSCRN", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input

        dbComm.Parameters("SSSCRN").Value = ssnum
        dbComm.CommandText = "PKG_IKIOSK.CHECK_MEMSTATUS"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim rtnMsg As String = dbComm.Parameters("msg").Value.ToString
        If rtnMsg = "WITH_FINAL_CLAIM" Then
            getMemStat = 1
        End If

        Return getMemStat
    End Function


    Public Function extractInfoDetails(ByVal ssnum As String) As String

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand

        dbConn.Open()

        dbComm = dbConn.CreateCommand

        dbComm.Parameters.Add("SSNUM2", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
        dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 500).Direction = ParameterDirection.Output
        dbComm.Parameters("SSNUM2").Value = ssnum
        dbComm.CommandText = "PR_IK_MEMCON_INFO"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim extractMsg As String = dbComm.Parameters("MSG").Value.ToString


        Return extractMsg

    End Function

    Public Function employerEmail(ByVal erid As String, ByVal erbrcode As String) As String

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand

        dbConn.Open()

        dbComm = dbConn.CreateCommand

        dbComm.Parameters.Add("ERID", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input
        dbComm.Parameters.Add("ERBRCODE", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input
        dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 100).Direction = ParameterDirection.Output
        dbComm.Parameters("ERID").Value = erid
        dbComm.Parameters("ERBRCODE").Value = erbrcode
        dbComm.CommandText = "PR_IK_GETEREMAIL"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim extractMsg As String = dbComm.Parameters("MSG").Value.ToString


        Return extractMsg

    End Function

    Public Function postalCodeChecker(ByVal pcode As String) As Boolean

        postalCodeChecker = False

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand

        dbConn.Open()

        dbComm = dbConn.CreateCommand

        dbComm.Parameters.Add("msg", OracleDbType.Long, 25, Nothing, ParameterDirection.ReturnValue)
        dbComm.Parameters.Add("V_POSTALCODE", OracleDbType.Varchar2, 20).Direction = ParameterDirection.Input
        dbComm.Parameters("V_POSTALCODE").Value = pcode
        dbComm.CommandText = "PKG_IKIOSK.CHECK_POSTALCODE"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()

        Dim extractMsg As String = dbComm.Parameters("msg").Value.ToString
        If extractMsg = "NOT_EXISTS" Or extractMsg.Contains("NOT") Then
            postalCodeChecker = False
        ElseIf extractMsg = "EXISTS" Then
            postalCodeChecker = True
        End If

        Return postalCodeChecker

    End Function

    Public Function technicalRetirementLumpSum(ByVal ssnum As String, ByVal dob As String)

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

        Dim noteMsg As String = dbComm.Parameters("RETDT").Value.ToString


        contDT = noteMsg
        Dim ageMsg As String = dbComm.Parameters("MSG").Value.ToString
        ageMsg = ageMsg.Trim
        If ageMsg = "" Or ageMsg = " " Or ageMsg = Nothing Then
            Return noteMsg
        ElseIf ageMsg <> "" Then
            Return noteMsg & "|" & ageMsg
        End If



    End Function

End Class
