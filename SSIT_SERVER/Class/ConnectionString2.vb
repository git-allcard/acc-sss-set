Imports System.Data
Imports Oracle.DataAccess.Client
Imports System.Text.RegularExpressions

Public Class ConnectionString2
    Public sql As String
    Public task As String
    'gie palitan mo nalang connectionstring nito alam mo yan
    Public connstring1 As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & My.Settings.db_Host & ")(PORT=" & My.Settings.db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & My.Settings.db_ServiceName & ")));User Id=" & My.Settings.db_UserID & ";Password=" & My.Settings.db_Password & ";"
    Public conn As OracleConnection = New OracleConnection(connString1)
    Public cmd As New OracleCommand
    Public rtrnValue As Long

    Private _connectionError As String
    Public Property connectionError() As String
        Get
            Return _connectionError
        End Get
        Set(ByVal value As String)
            _connectionError = value
        End Set
    End Property

    Public Function getDataTable(ByVal sql As String, ByVal tbl As String) As DataTable
        Dim dt As New DataTable
        Try

            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Open()
            Else
                conn.Open()
            End If
            Dim da As OracleDataAdapter = New OracleDataAdapter(sql, conn)
            Dim ds As New DataSet
            da.Fill(ds, tbl)
            dt = ds.Tables(tbl)
        Catch ex As Exception
            MsgBox("Error:" & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return dt
    End Function

    Public Sub GetList(ByVal spName As String, ByVal DGV As DataGridView)
        Dim strCon As String = connString1
        Dim strSQL As String = spName
        Dim dataAdapter As New SqlClient.SqlDataAdapter(strSQL, strCon)
        Dim table As New DataTable
        dataAdapter.Fill(table)
        DGV.DataSource = table
    End Sub

    Public Function ExecuteSQLQuery(ByVal SQLQuery As String) As DataTable
        Dim sqlDT As New DataTable
        Try
            Dim sqlCon As New OracleConnection(connString1)
            Dim sqlDA As New OracleDataAdapter(SQLQuery, sqlCon)
            Dim sqlCB As New OracleCommandBuilder(sqlDA)
            sqlDA.Fill(sqlDT)
        Catch ex As Exception
            MsgBox("Program Error: " & ex.Message, MsgBoxStyle.Critical)
        End Try
        Return sqlDT
    End Function

    Public Overridable Function Execute(ByVal strCmd As String, ByVal cmdType As CommandType, Optional ByVal param() As OracleParameter = Nothing) As Long
        conn = New OracleConnection(connstring1)
        Try
            If Not IsNothing(param) Then
                If Not IsNothing(param) Then
                    For Each p As OracleParameter In param
                        cmd.Parameters.Add(p)
                    Next
                End If
            End If

            cmd.CommandText = strCmd
            cmd.Connection = conn
            cmd.CommandType = cmdType
            conn.Open()
            cmd.ExecuteScalar()
            'rtrnValue = cmd.Parameters(rtnprm).Value

            Return rtrnValue

        Catch ex As Exception
            Throw New Exception(ex.Message)
        Finally
            If conn.State = Data.ConnectionState.Open Then
                conn.Close()
                cmd.Parameters.Clear()
            End If
        End Try
    End Function

    Public Sub FillListView(ByVal sqlData As DataTable, ByVal lvList As ListView)
        lvList.Items.Clear()
        lvList.Columns.Clear()
        Dim i As Integer
        Dim j As Integer
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Columns.Add(sqlData.Columns(i).ColumnName)
        Next i
        For i = 0 To sqlData.Rows.Count - 1
            lvList.Items.Add(sqlData.Rows(i).Item(0))
            For j = 1 To sqlData.Columns.Count - 1
                If Not IsDBNull(sqlData.Rows(i).Item(j)) Then
                    lvList.Items(i).SubItems.Add(sqlData.Rows(i).Item(j))
                Else
                    lvList.Items(i).SubItems.Add("")
                End If

            Next j
        Next i
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Columns(i).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        Next i
    End Sub

    Public Function doNonQuery(ByVal sql As String, Optional ByVal process As String = "") As Boolean
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Open()
            Else
                conn.Open()
            End If
            Dim cmd As OracleCommand = New OracleCommand(sql, conn)
            cmd.ExecuteNonQuery()
            doNonQuery = True
        Catch ex As Exception
            doNonQuery = False
        Finally
            conn.Close()
        End Try
    End Function
    Public Function checkExistence(ByVal sql As String) As Boolean
        Dim ans As Boolean = False
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Open()
            Else
                conn.Open()
            End If
            Dim cmd As OracleCommand = New OracleCommand(sql, conn)
            Dim rdr As OracleDataReader = cmd.ExecuteReader
            If rdr.Read Then
                ans = True
            End If
        Catch ex As Exception
            'MsgBox("Unable to connect to server" & vbNewLine & "Please check your database connection", MsgBoxStyle.Exclamation)
            MsgBox("Error on(checkExistence): " & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return ans
    End Function

    Public Function checkExistence2(ByVal sql As String) As Boolean
        Dim ans As Boolean = False
        Try
            If conn.State = ConnectionState.Open Then
                conn.Close()
                conn.Open()
            Else
                conn.Open()
            End If
            Dim cmd As OracleCommand = New OracleCommand(sql, conn)
            Dim rdr As OracleDataReader = cmd.ExecuteReader
            If rdr.Read Then
                ans = True
            End If
        Catch ex As Exception
            MsgBox("Unable to connect to server" & vbNewLine & "Please check your database connection", MsgBoxStyle.Exclamation)
            'MsgBox("Error on(checkExistence): " & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return ans
    End Function

    Public Function putSingleValue(ByVal sql As String, Optional ByVal tbl As String = "") As String
        Dim result As String = ""
        Try
            If tbl <> "" Then
                Dim dt As DataTable = getDataTable(sql, tbl)
                If dt.Rows.Count <> 0 Then
                    If Not IsDBNull(dt.Rows(0)(0)) Then
                        result = dt.Rows(dt.Rows.Count - 1)(0)
                    End If
                End If
            Else
                If conn.State = ConnectionState.Open Then
                    conn.Close()
                    conn.Open()
                Else
                    conn.Open()
                End If
                Dim cmd As OracleCommand = New OracleCommand(sql, conn)
                Dim rdr As OracleDataReader = cmd.ExecuteReader
                If rdr.Read Then
                    If Not IsDBNull(rdr(0)) Then
                        result = rdr(0)
                    End If
                End If
            End If
        Catch ex As Exception
            MsgBox("Error 1: " & ex.ToString)
            result = ""
        Finally
            conn.Close()
        End Try
        Return result
    End Function
    Dim conStr As String
    Function webisconnected(ByVal constring As String) As Boolean
        conStr = constring
        Return isconnected()
    End Function

    Function isconnected() As Boolean
        Try
            If DBConnectionStatus() = True Then
                Return True
            Else
                Return False
            End If

        Catch ex As Exception
            MsgBox(ex.ToString)
        End Try

    End Function

    Private Function DBConnectionStatus() As Boolean
        Try

            Using conStr1 As New OracleConnection(conStr)

                '("Server=" & txtServer.Text & ";" & _
                '"uid=" & txtLogin.Text & ";pwd=" & txtPassword.Text & "")
                conStr1.Open()
                Return (conStr1.State = ConnectionState.Open)
            End Using
        Catch e1 As OracleException
            Return False
        Catch e2 As Exception
            Return False
        End Try
    End Function

    Public Sub FillListBox(ByVal sqlData As DataTable, ByVal lvList As ListBox)
        lvList.Items.Clear()
        Dim i As Integer
        Dim j As Integer
        For i = 0 To sqlData.Columns.Count - 1
            lvList.Items.Add(sqlData.Columns(i).ColumnName)
        Next i
        For i = 0 To sqlData.Rows.Count - 1

            If IsDBNull(sqlData.Rows(i).Item(0)) Then
                lvList.Items.Add("")
            Else
                lvList.Items.Add(sqlData.Rows(i).Item(0))
            End If

            For j = 1 To sqlData.Columns.Count - 1
                If Not IsDBNull(sqlData.Rows(i).Item(j)) Then
                    'lvList.Items(i).SubItems.Add(sqlData.Rows(i).Item(j))
                Else
                    'lvList.Items(i).SubItems.Add("")
                End If

            Next j
        Next i
        'For i = 0 To sqlData.Columns.Count - 1
        '    lvList.Items(i).AutoResize(ColumnHeaderAutoResizeStyle.HeaderSize)
        'Next i
    End Sub

    Public Sub fillComboBox(ByVal dt As DataTable, ByVal cb As ComboBox)
        cb.Items.Clear()
        For row As Integer = 0 To dt.Rows.Count - 1
            cb.Items.Add(dt.Rows(row)(0))
        Next
    End Sub

    Function EmailAddressCheck(ByVal emailAddress As String) As Boolean
        Dim pattern As String = "^[a-zA-Z][\w\.-]*[a-zA-Z0-9]@[a-zA-Z0-9][\w\.-]*[a-zA-Z0-9]\.[a-zA-Z][a-zA-Z\.]*[a-zA-Z]$"
        Dim emailAddressMatch As Match = Regex.Match(emailAddress, pattern)
        If emailAddressMatch.Success Then
            EmailAddressCheck = True
        Else
            EmailAddressCheck = False
        End If
    End Function

    Public Function EncryptText(ByVal sSTR As System.String) As String
        Dim sTmp As System.String
        Dim sResult As System.String
        Dim iCnt As System.Int32

        sTmp = StrReverse(sSTR)
        sResult = ""
        For iCnt = 1 To Len(sTmp)
            sResult = sResult & Chr(Asc(Mid(sTmp, iCnt, 1)) + Asc("g"))
        Next
        EncryptText = sResult
    End Function

    Public Function DecryptText(ByVal sSTR As String) As String

        Dim sTmp As String
        Dim sResult As String
        Dim icnt As Integer

        sTmp = StrReverse(sSTR)

        sResult = ""

        For icnt = 1 To Len(sTmp)
            sResult = sResult & Chr(Asc(Mid(sTmp, icnt, 1)) - Asc("g"))
        Next

        DecryptText = sResult

    End Function

    Public Function putSingleNumber(ByVal sql As String, Optional ByVal tbl As String = "") As String
        Dim result As Double = 0.0
        Try
            If tbl <> "" Then
                Dim dt As DataTable = getDataTable(sql, tbl)
                If dt.Rows.Count <> 0 Then
                    If Not IsDBNull(dt.Rows(0)(0)) Then
                        result = Val(dt.Rows(dt.Rows.Count - 1)(0))
                    End If
                End If
            Else
                conn.Open()
                Dim cmd As OracleCommand = New OracleCommand(sql, conn)
                Dim rdr As OracleDataReader = cmd.ExecuteReader
                If rdr.Read Then
                    If Not IsDBNull(rdr(0)) Then
                        result = rdr(0)
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox("Error 1: " & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return result
    End Function

    Public Function selectTable(ByVal sql As String) As Boolean
        Dim ans As Boolean = False
        Try
            conn.Open()
            Dim cmd As OracleCommand = New OracleCommand(sql, conn)
            Dim rdr As OracleDataReader = cmd.ExecuteReader
            If rdr.Read Then
                ans = True
            End If
        Catch ex As Exception
            MsgBox("Error on(selectTable): " & ex.ToString)
        Finally
            conn.Close()
        End Try
        Return ans
    End Function

    Public Sub FillDataGridView(ByVal selectCommand As String, ByVal dgv As DataGridView)

        Try

            Dim cnn As String = connString1
            Dim da = New OracleDataAdapter(selectCommand, cnn)
            Dim cb As New OracleCommandBuilder(da)
            Dim tbl As New DataTable()
            tbl.Locale = System.Globalization.CultureInfo.InvariantCulture
            da.Fill(tbl)
            dgv.DataSource = tbl
            dgv.AutoResizeColumns( _
                DataGridViewAutoSizeColumnsMode.ColumnHeader)
        Catch ex As OracleException
        End Try
    End Sub

    Public Function GenRecID() As String
        Dim id As String = ""
        For x As Integer = 1 To 99999
            Dim tempID As String = x
            If checkExistence("select * from tbl_UserType where ID='" & tempID & "'") = False Then
                id = tempID
                Exit For
            End If
        Next
        Return id
    End Function

    Public Sub AuditTrail(ByVal user_ID As String, ByVal username As String, ByVal _module As String, ByVal task As String, ByVal affected_Table As String, ByVal status As String, ByVal transaction_Date As String, ByVal transaction_Time As String)
        ExecuteSQLQuery("Insert into tbl_Audit_Trail(User_ID, User_Name, Module, Task, Affected_Table, Status, Transaction_Date, Transaction_Time) values('" & user_ID & "', '" & username & "', '" & _module & "', '" & task & "', '" & affected_Table & "', '" & status & "', '" & transaction_Date & "', '" & transaction_Time & "')")
    End Sub
End Class

