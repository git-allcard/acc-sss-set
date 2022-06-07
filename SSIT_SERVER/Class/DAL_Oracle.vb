
Imports System.Data.OleDb


Public Class DAL_Oracle

    'Private ConStr As String = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.101.141.207)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=d00cs1d)));User Id=mowii02;Password=mowii02;"
    '  Private ConStr As String = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.101.141.207)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=d00cs1d)));User Id=gonzalesmm_ik;Password=g0nzalesmm_1k;"
    'Private ConStr As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & My.Settings.db_Host & ")(PORT=" & My.Settings.db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & My.Settings.db_ServiceName & ")));User Id=" & My.Settings.db_UserID & ";Password=" & My.Settings.db_Password & ";"
    'Private ConStr As String = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=10.101.141.217)(PORT=1521))(CONNECT_DATA=(SERVICE_NAME=mo0cs1p)));User Id=mo0ikw01;Password=Mo0ikw01_Ssit;"
    Private ConStr As String = "Provider=OraOLEDB.Oracle;Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"

    Private dtResult As DataTable
    Private objResult As Object
    Private strErrorMessage As String

    Private con As OleDbConnection
    Private cmd As OleDbCommand
    Private da As OleDbDataAdapter
    Private intUserID As Integer = 0

    Public Sub New()
    End Sub

    Public Sub New(ByVal intUserID As Integer)
        Me.intUserID = intUserID
    End Sub

    Public ReadOnly Property ErrorMessage() As String
        Get
            Return strErrorMessage
        End Get
    End Property

    Public ReadOnly Property TableResult() As DataTable
        Get
            Return dtResult
        End Get
    End Property

    Public ReadOnly Property ObjectResult() As Object
        Get
            Return objResult
        End Get
    End Property

    Private Sub OpenConnection()
        Try
            con = New OleDbConnection(ConStr)
        Catch ex As Exception
        End Try
    End Sub

    Private Sub CloseConnection()
        If Not cmd Is Nothing Then cmd.Dispose()
        If Not da Is Nothing Then da.Dispose()
        If con.State = ConnectionState.Open Then con.Close()
    End Sub

    Private Sub ExecuteNonQuery(ByVal cmdType As CommandType)
        cmd.CommandType = cmdType

        con.Open()
        cmd.ExecuteNonQuery()
        con.Close()
    End Sub

    Private Sub ExecuteScalar(ByVal cmdType As CommandType)
        cmd.CommandType = cmdType

        con.Open()
        Dim _obj As Object
        _obj = cmd.ExecuteScalar()
        con.Close()

        objResult = _obj
    End Sub

    Private Sub FillDataAdapter(ByVal cmdType As CommandType)
        cmd.CommandType = cmdType
        da = New OleDbDataAdapter(cmd)
        Dim _dt As New DataTable
        da.Fill(_dt)
        dtResult = _dt
    End Sub

    Private Sub FillDataAdapterDS(ByVal cmdType As CommandType, ByRef ds As DataSet)
        cmd.CommandType = cmdType
        da = New OleDbDataAdapter(cmd)
        Dim _dt As New DataTable
        da.Fill(ds, "PROG_SECURA_DEFAULT")
        'da.Fill(_dt)
        'dtResult = _dt
    End Sub

    Public Function IsConnectionOK(Optional ByVal strConString As String = "") As Boolean
        Try
            If strConString <> "" Then ConStr = strConString
            OpenConnection()

            con.Open()
            con.Close()

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function ExecuteQuery(ByVal strQuery As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand(strQuery, con)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SelectQuery(ByVal strQuery As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SelectData(ByVal strQuery As String) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand(SharedFunction.ORACLE_PROG_SECURA_DEFAULT_FieldsForData & ORACLE_PROG_SECURA_DEFAULT_SOURCE() & strQuery, con)
            cmd = New OleDbCommand("", con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function FNC_BANK() As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("select (BRSTN ||'-'|| BINIT ||'-'|| LOCAL) BRANCHNAME from MBBANKPARTLF WHERE BRSTN IS NOT NULL ORDER BY BRSTN", con)
            'Dim str As String = IO.File.ReadAllText("D:\fnc.txt")
            ''cmd = New OleDbCommand("PKG_IKTECHRET.FNC_BANK", con)
            'cmd = New OleDbCommand(str, con)

            'cmd.CommandTimeout = 0
            'Dim prm = New OleDbParameter("returnvalue", OleDbType.Variant)
            ''prm.Size = 100
            'prm.Direction = ParameterDirection.ReturnValue
            'cmd.Parameters.Add(prm)

            'ExecuteNonQuery(CommandType.Text)
            ''ExecuteScalar(CommandType.StoredProcedure)

            'objResult = prm.Value.ToString

            
            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function Select_Province() As Boolean
        Try
            OpenConnection()
            'Dim strQuery As String = "select province_cd, provincename from RCPROVCRGNLF order by provincename asc"
            Dim strQuery As String = "select '0' As province_cd, '-SELECT PROVINCE-' As provincename from dual union select province_cd, provincename from RCPROVCRGNLF WHERE province_cd <> 'XXXXXXXXX' order by provincename asc"

            cmd = New OleDbCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function Select_ProvinceCity(Optional ByVal province_cd As String = "") As Boolean
        Try
            OpenConnection()
            'Dim strQuery As String = "select city_mun_cd, city_munname, province_cd from RCCITYMUNCLF where province_cd in ('133900000', '137400000', '137500000','137600000') order by city_munname asc"
            Dim strQuery As String = "select '0' As city_mun_cd, '-SELECT CITY-' As city_munname, '-SELECT PROVINCE-' As province_cd from dual union select city_mun_cd, city_munname, province_cd from RCCITYMUNCLF where province_cd in ('133900000', '137400000', '137500000','137600000') order by city_munname asc"
            If province_cd <> "" Then strQuery = String.Format("select '0' As city_mun_cd, '-SELECT CITY-' As city_munname, '-SELECT PROVINCE-' As province_cd from dual union select city_mun_cd, city_munname, province_cd from RCCITYMUNCLF where province_cd = '{0}' order by city_munname asc", province_cd)

            cmd = New OleDbCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function Select_Country() As Boolean
        Try
            OpenConnection()

            Dim strQuery As String = "select '0' As country_cd, '-SELECT COUNTRY-' As country_name from dual union select country_cd, country_name from RCCOUNTRYXLF WHERE country_cd <> 'XXXX' order by country_name"
            cmd = New OleDbCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function Select_CityBarangay(ByVal city_mun_cd As String) As Boolean
        Try
            OpenConnection()
            'Dim strQuery As String = String.Format("select post_cd, barangay_cd, barangayname from RCBRGPOSCDLF where city_mun_cd = '{0}' order by barangayname asc", barangay_cd)
            Dim strQuery As String = String.Format("select '' As post_cd, '-Select Barangay Code-' As barangay_cd, '-SELECT BARANGAY-' As barangayname from dual union select post_cd, barangay_cd, barangayname from RCBRGPOSCDLF where city_mun_cd = '{0}' order by barangayname asc", city_mun_cd)

            cmd = New OleDbCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SelectDataDS(ByVal strQuery As String, ByRef ds As DataSet) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand(SharedFunction.ORACLE_PROG_SECURA_DEFAULT_FieldsForData & ORACLE_PROG_SECURA_DEFAULT_SOURCE() & strQuery, con)
            cmd = New OleDbCommand("", con)

            FillDataAdapterDS(CommandType.Text, ds)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    'Public Function checkIfValid(ByVal tempCRN As String) As Boolean
    '    Try
    '        OpenConnection()
    '        'cmd = New OleDbCommand("PKG_INFOKIOSK.checkIfValid", con)
    '        cmd = New OleDbCommand("PKG_IKIOSK.checkIfValid", con)

    '        cmd.CommandTimeout = 0
    '        Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
    '        prm.Size = 50
    '        prm.Direction = ParameterDirection.ReturnValue
    '        cmd.Parameters.Add(prm)

    '        cmd.Parameters.Add("CRN", OleDbType.VarChar, 50, ParameterDirection.ReturnValue).Value = tempCRN

    '        ExecuteNonQuery(CommandType.StoredProcedure)

    '        objResult = prm.Value.ToString

    '        Return True
    '    Catch ex As Exception
    '        strErrorMessage = ex.Message
    '        Return False
    '    Finally
    '        CloseConnection()
    '    End Try
    'End Function

    Public Function updatePIN(ByVal CRN As String, ByVal CCDT As String, ByVal PIN_DATE As String, ByVal PIN As String) As Boolean
        Try
            Dim strCardCreationDate As String = ""
            If CCDT.Length = 8 Then strCardCreationDate = CCDT.Substring(4, 2) + "-" + CCDT.Substring(6, 2) & "-" & CCDT.Substring(0, 4)

            OpenConnection()
            'cmd = New OleDbCommand("PKG_INFOKIOSK.checkIfValid", con)
            '  cmd = New OleDbCommand("PKG_IKIOSK_TEMP.updatePIN", con)
            cmd = New OleDbCommand("PKG_IKIOSK.updatePIN2", con)

            cmd.CommandTimeout = 0

            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 15
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)


            cmd.Parameters.Add("pCRN", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = CRN.Replace("-", "")
            cmd.Parameters.Add("pcc_date", OleDbType.VarChar, 10, ParameterDirection.ReturnValue).Value = strCardCreationDate
            cmd.Parameters.Add("ppinlastupdate", OleDbType.VarChar, 10, ParameterDirection.ReturnValue).Value = PIN_DATE
            cmd.Parameters.Add("pPIN", OleDbType.VarChar, 6, ParameterDirection.ReturnValue).Value = PIN
            'cmd.Parameters.Add("pPIN", OleDbType.VarChar, 8, ParameterDirection.ReturnValue).Value = PIN
            cmd.Parameters.Add("pBRANCH", OleDbType.VarChar, 3, ParameterDirection.ReturnValue).Value = getbranchCoDE_1
            cmd.Parameters.Add("pTERMINAL", OleDbType.VarChar, 5, ParameterDirection.ReturnValue).Value = kioskID

            'objResult = "UPDATE_SUCCESS"
            'Return True

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function tagUMIDActivated(ByVal tempCRN As String) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand("PKG_INFOKIOSK.tagUMIDActivated", con)
            cmd = New OleDbCommand("PKG_IKIOSK.tagUMIDActivated", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 50
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("CRN", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = tempCRN
            cmd.Parameters.Add("SERIAL", OleDbType.VarChar, 20, ParameterDirection.ReturnValue).Value = readSettings(xml_Filename, xml_path, "CSN") 'My.Settings.CSN

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CHECK_MEMSTATUS(ByVal refNo As String) As Boolean
        'refNo = "0226879523"


        Try
            OpenConnection()
            'cmd = New OleDbCommand("PKG_IK_REGISTRATION.CHECK_MEMSTATUS", con)
            cmd = New OleDbCommand("PKG_IKIOSK.CHECK_MEMSTATUS", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 50
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("SSSCRN", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = refNo

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    '003407883539	3407883539	2011-09-02 00:00:00
    '003407859356	3407859356	2011-09-02 00:00:00
    '003407849450	3407849450	2011-09-02 00:00:00
    '003407833738	3407833738	2011-09-02 00:00:00
    '003407819891	3407819891	2011-09-02 00:00:00
    '003407767228	3407767228	2011-09-02 00:00:00
    '003407745693	3407745693	2011-09-02 00:00:00
    '003407658489	3407658489	2011-09-02 00:00:00
    '003407634306	3407634306	2011-09-02 00:00:00
    '003407609441	3407609441	2011-09-02 00:00:00



    Public Function CHECKIFRECENT(ByVal CRNumber As String, ByVal CCDT As String, ByVal in_steps As String, ByRef in_ssnum As String,
                                  ByRef IN_PIN As String, ByRef msg As String) As Boolean
        Try
            Dim strCardCreationDate As String = ""
            If CCDT.Length = 8 Then strCardCreationDate = CCDT.Substring(4, 2) + "-" + CCDT.Substring(6, 2) & "-" & CCDT.Substring(0, 4)

            'strCardCreationDate = "07-15-2011"

            OpenConnection()
            cmd = New OleDbCommand("PR_IK_CHECKIFRECENT", con)

            cmd.CommandTimeout = 0

            'cmd.Parameters.Add("CRNumber", OleDbType.VarChar, 12, ParameterDirection.Input).Value = "000102569957" 'CRNumber
            cmd.Parameters.Add("CRNumber", OleDbType.VarChar, 12, ParameterDirection.Input).Value = CRNumber
            cmd.Parameters.Add("CCDT", OleDbType.VarChar, 10, ParameterDirection.Input).Value = strCardCreationDate

            Dim prm1 = New OleDbParameter("in_ssnum", OleDbType.VarChar)
            prm1.Size = 10
            prm1.Direction = ParameterDirection.InputOutput
            prm1.Value = in_ssnum
            cmd.Parameters.Add(prm1)

            cmd.Parameters.Add("in_steps", OleDbType.VarChar, 1, ParameterDirection.Input).Value = in_steps

            Dim prm2 = New OleDbParameter("IN_PIN", OleDbType.VarChar)
            prm2.Size = 6
            'prm2.Size = 8
            prm2.Direction = ParameterDirection.Output
            cmd.Parameters.Add(prm2)

            Dim prm3 = New OleDbParameter("msg", OleDbType.VarChar)
            prm3.Size = 50
            prm3.Direction = ParameterDirection.Output
            cmd.Parameters.Add(prm3)

            ExecuteNonQuery(CommandType.StoredProcedure)

            'in_ssnum = "0102569957" 'prm1.Value.ToString
            'IN_PIN = "111111" 'prm2.Value.ToString

            in_ssnum = prm1.Value.ToString
            IN_PIN = prm2.Value.ToString
            msg = prm3.Value.ToString

            Try
                Dim sbTest As New System.Text.StringBuilder
                sbTest.AppendLine("=== START " & Now.ToString() & " ===")
                sbTest.AppendLine("in crn: " & CRNumber)
                sbTest.AppendLine("in ccdt: " & CCDT)
                sbTest.AppendLine("in steps: " & in_steps)
                sbTest.AppendLine("out msg: " & msg)
                sbTest.AppendLine("out sss: " & in_ssnum)

                Dim sw As New System.IO.StreamWriter("D:\WORK\SSS\checkifrecent.txt", True)
                sw.WriteLine(sbTest.ToString)
                sw.Close()
                sw.Dispose()
            Catch ex As Exception

            End Try

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function pr_send_mail(ByVal p_to As String, ByVal p_subject As String, ByVal p_text_msg As String, ByVal p_html_msg As String, ByRef p_returnMsg As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("pr_send_mail", con)


            cmd.CommandTimeout = 0

            cmd.Parameters.Add("p_to", OleDbType.VarChar, 100, ParameterDirection.Input).Value = p_to
            cmd.Parameters.Add("p_subject", OleDbType.VarChar, 300, ParameterDirection.Input).Value = p_subject
            cmd.Parameters.Add("p_text_msg", OleDbType.VarChar, 5000, ParameterDirection.Input).Value = p_text_msg
            cmd.Parameters.Add("p_html_msg", OleDbType.VarChar, 5000, ParameterDirection.Input).Value = p_html_msg

            Dim prm1 = New OleDbParameter("p_returnMsg", OleDbType.VarChar)
            prm1.Size = 100
            prm1.Direction = ParameterDirection.InputOutput
            prm1.Value = p_returnMsg
            cmd.Parameters.Add(prm1)

            ExecuteNonQuery(CommandType.StoredProcedure)

            p_returnMsg = prm1.Value.ToString.Trim

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function PR_MEM_OTHERINFO(ByRef arrParam() As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("PG_IK_MEMADD.PR_MEM_OTHERINFO", con)

            cmd.CommandTimeout = 0

            cmd.Parameters.Add("vSSNUM", OleDbType.VarChar, 10, ParameterDirection.Input).Value = SSStempFile
            cmd.Parameters.Add(CreateOleDbParameter("vHSE_LBLK_NO", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vUNIT_BLDG_NM", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vSTREET", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vSUBDIVISION", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vBARANGAY_CD", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vCITY_MUN_CD", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vPROVINCE_CD", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vPOST_CD", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vCPNUMBER", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vTELNO", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vEMAIL", OleDbType.VarChar, 50, ParameterDirection.Output))

            cmd.Parameters.Add(CreateOleDbParameter("vFOREIGN_ADDR", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vFOREIGN_CITY", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vFOREIGN_ZIP", OleDbType.VarChar, 50, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vCOUNTRY_CD", OleDbType.VarChar, 50, ParameterDirection.Output))

            cmd.Parameters.Add(CreateOleDbParameter("vMSG", OleDbType.VarChar, 50, ParameterDirection.Output))

            ExecuteNonQuery(CommandType.StoredProcedure)

            Dim intParamIndex As Short = 0
            For Each param As OleDbParameter In cmd.Parameters
                If intParamIndex = 0 Then
                ElseIf intParamIndex = cmd.Parameters.Count Then
                Else
                    arrParam(intParamIndex - 1) = IIf(IsDBNull(param.Value), "", param.Value)
                End If
                intParamIndex += 1
            Next

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function PR_MEM_OTHERINFO_v2(ByRef arrParam() As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("PG_IK_MEMADD.PR_MEM_OTHERINFO", con)

            cmd.CommandTimeout = 0

            cmd.Parameters.Add("vSSNUM", OleDbType.VarChar, 10, ParameterDirection.Input).Value = SSStempFile
            cmd.Parameters.Add(CreateOleDbParameter("vMAILING_ADDR", OleDbType.VarChar, 300, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vFOREIGN_ADDR", OleDbType.VarChar, 300, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vCPNUMBER", OleDbType.VarChar, 100, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vTELNO", OleDbType.VarChar, 100, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vEMAIL", OleDbType.VarChar, 100, ParameterDirection.Output))
            cmd.Parameters.Add(CreateOleDbParameter("vMSG", OleDbType.VarChar, 100, ParameterDirection.Output))

            ExecuteNonQuery(CommandType.StoredProcedure)

            Dim intParamIndex As Short = 0
            For Each param As OleDbParameter In cmd.Parameters
                If intParamIndex = 0 Then
                ElseIf intParamIndex = cmd.Parameters.Count Then
                Else
                    arrParam(intParamIndex - 1) = IIf(IsDBNull(param.Value), "", param.Value)
                End If
                intParamIndex += 1
            Next

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function PR_INSERT_MEMADD(ByVal v_SSNUM As String, ByVal v_EMAIL As String, ByVal v_HSE_LBLK_NO As String, ByVal v_UNIT_BLDG_NM As String,
                                     ByVal v_STREET As String, ByVal v_SUBDIVISION As String, ByVal v_CITY_MUN_CD As String, ByVal v_PROVINCE_CD As String,
                                     ByVal v_BARANGAY_CD As String, ByVal v_POST_CD As String, ByRef p_MSG As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("PG_IK_MEMADD.PR_INSERT_MEMADD", con)

            cmd.CommandTimeout = 0

            'cmd.Parameters.Add("v_SSNUM", OleDbType.VarChar, 10, ParameterDirection.Input).Value = v_SSNUM
            'cmd.Parameters.Add("v_EMAIL", OleDbType.VarChar, 50, ParameterDirection.Input).Value = v_EMAIL
            'cmd.Parameters.Add("v_HSE_LBLK_NO", OleDbType.VarChar, 15, ParameterDirection.Input).Value = v_HSE_LBLK_NO
            'cmd.Parameters.Add("v_UNIT_BLDG_NM", OleDbType.VarChar, 40, ParameterDirection.Input).Value = v_UNIT_BLDG_NM
            'cmd.Parameters.Add("v_STREET", OleDbType.VarChar, 40, ParameterDirection.Input).Value = v_STREET
            'cmd.Parameters.Add("v_SUBDIVISION", OleDbType.VarChar, 40, ParameterDirection.Input).Value = v_SUBDIVISION
            'cmd.Parameters.Add("v_CITY_MUN_CD", OleDbType.VarChar, 9, ParameterDirection.Input).Value = v_CITY_MUN_CD
            'cmd.Parameters.Add("v_PROVINCE_CD", OleDbType.VarChar, 9, ParameterDirection.Input).Value = v_PROVINCE_CD
            'cmd.Parameters.Add("v_BARANGAY_CD", OleDbType.VarChar, 9, ParameterDirection.Input).Value = v_BARANGAY_CD
            'cmd.Parameters.Add("v_POST_CD", OleDbType.VarChar, 4, ParameterDirection.Input).Value = v_POST_CD

            cmd.Parameters.Add(CreateOleDbParameter("v_SSNUM", OleDbType.VarChar, 10, ParameterDirection.Input, v_SSNUM))
            cmd.Parameters.Add(CreateOleDbParameter("v_EMAIL", OleDbType.VarChar, 50, ParameterDirection.Input, v_EMAIL))
            cmd.Parameters.Add(CreateOleDbParameter("v_HSE_LBLK_NO", OleDbType.VarChar, 15, ParameterDirection.Input, v_HSE_LBLK_NO))
            cmd.Parameters.Add(CreateOleDbParameter("v_UNIT_BLDG_NM", OleDbType.VarChar, 40, ParameterDirection.Input, v_UNIT_BLDG_NM))
            cmd.Parameters.Add(CreateOleDbParameter("v_STREET", OleDbType.VarChar, 40, ParameterDirection.Input, v_STREET))
            cmd.Parameters.Add(CreateOleDbParameter("v_SUBDIVISION", OleDbType.VarChar, 40, ParameterDirection.Input, v_SUBDIVISION))
            cmd.Parameters.Add(CreateOleDbParameter("v_CITY_MUN_CD", OleDbType.VarChar, 9, ParameterDirection.Input, v_CITY_MUN_CD))
            cmd.Parameters.Add(CreateOleDbParameter("v_PROVINCE_CD", OleDbType.VarChar, 9, ParameterDirection.Input, v_PROVINCE_CD))
            cmd.Parameters.Add(CreateOleDbParameter("v_BARANGAY_CD", OleDbType.VarChar, 9, ParameterDirection.Input, v_BARANGAY_CD))
            cmd.Parameters.Add(CreateOleDbParameter("v_POST_CD", OleDbType.VarChar, 4, ParameterDirection.Input, v_POST_CD))
            cmd.Parameters.Add(CreateOleDbParameter("p_MSG", OleDbType.VarChar, 50, ParameterDirection.Output))

            ExecuteNonQuery(CommandType.StoredProcedure)

            p_MSG = cmd.Parameters("p_MSG").Value

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private Function CreateOleDbParameter(ByVal ParamName As String, ByVal OleDbType As OleDbType, ByVal ParamSize As Integer, ByVal ParamDirection As ParameterDirection, Optional ByVal ParamValue As String = "") As OleDbParameter
        Dim prm As New OleDbParameter(ParamName, OleDbType)
        If ParamSize > 0 Then prm.Size = ParamSize
        If ParamValue <> "" Then prm.Value = ParamValue
        prm.Direction = ParamDirection
        Return prm
    End Function

    Public Function GET_SSNUMBER(ByVal CRNUM As String) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand("PKG_IK_REGISTRATION.CHECK_MEMSTATUS", con)
            cmd = New OleDbCommand("PKG_IKIOSK.GET_SSNUMBER", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 10
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("CRNUM", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = CRNUM

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function checkIfValid(ByVal CRNUM As String, ByVal CCDT As String) As Boolean
        Try
            Dim strCardCreationDate As String = ""
            If CCDT.Length = 8 Then strCardCreationDate = CCDT.Substring(4, 2) + "/" + CCDT.Substring(6, 2) & "/" & CCDT.Substring(0, 4)

            OpenConnection()
            'cmd = New OleDbCommand("PKG_IK_REGISTRATION.CHECK_MEMSTATUS", con)
            'cmd = New OleDbCommand("PKG_IKIOSK.checkIfValid", con)
            cmd = New OleDbCommand("PKG_IKIOSK.checkIfValid2", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 30
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("CRNumber", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = CRNUM
            cmd.Parameters.Add("CCDT", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = strCardCreationDate

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function checkIfWithUMID(ByVal SSNUM As String) As Boolean
        Try
            OpenConnection()
            cmd = New OleDbCommand("PKG_IKIOSK.checkIfWithUMID", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 30
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("sssid", OleDbType.VarChar, 10, ParameterDirection.ReturnValue).Value = SSNUM

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function decryptPIN(ByVal CRN As String, ByVal CSN As String) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand("PKG_IK_REGISTRATION.CHECK_MEMSTATUS", con)
            cmd = New OleDbCommand("PKG_IKIOSK.decryptPIN", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 20
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("CRN", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = CRN
            cmd.Parameters.Add("SERIAL", OleDbType.VarChar, 20, ParameterDirection.ReturnValue).Value = CSN

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function decryptPIN2(ByVal CRN As String) As Boolean
        Try
            OpenConnection()
            'cmd = New OleDbCommand("PKG_IK_REGISTRATION.CHECK_MEMSTATUS", con)
            cmd = New OleDbCommand("PKG_IKIOSK.decryptPIN2", con)

            cmd.CommandTimeout = 0
            Dim prm = New OleDbParameter("returnvalue", OleDbType.VarChar)
            prm.Size = 20
            prm.Direction = ParameterDirection.ReturnValue
            cmd.Parameters.Add(prm)

            cmd.Parameters.Add("CRN", OleDbType.VarChar, 12, ParameterDirection.ReturnValue).Value = CRN

            ExecuteNonQuery(CommandType.StoredProcedure)

            objResult = prm.Value.ToString

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    'Public Function decryptPIN2(ByVal CRN As String) As Boolean
    '    Try
    '        OpenConnection()
    '        'cmd = New OleDbCommand("PKG_INFOKIOSK.tagUMIDActivated", con)
    '        cmd = New OleDbCommand(sbDecrypt.ToString & "SELECT * FROM (select PINNUM from ikumidactvmf WHERE CRNUM = :CRNUM ORDER BY ACTIVATE_DT DESC) WHERE ROWNUM = 1", con)
    '        'cmd.CommandTimeout = 0

    '        cmd.Parameters.AddWithValue(":CRNUM", CRN)

    '        ExecuteScalar(CommandType.Text)

    '        Return True
    '    Catch ex As Exception
    '        strErrorMessage = ex.Message
    '        Return False
    '    Finally
    '        CloseConnection()
    '    End Try
    'End Function

End Class
