
Imports System.Data.SqlClient

Public Class DAL_Mssql
    Implements IDisposable

    Private ConStr As String = "Server='" & db_server & "';Database='" & db_Name & "';Uid= '" & db_UName & "' ;Pwd= '" & db_Pass & "';"
    'Private ConStr As String = "Server=10.0.202.95;Database=SSIT_SERVER;Uid=sa;Pwd=password2013;"

    Private dtResult As DataTable
    Private objResult As Object
    Private strErrorMessage As String

    Private con As SqlConnection
    Private cmd As SqlCommand
    Private da As SqlDataAdapter

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
        con = New SqlConnection(ConStr)
    End Sub

    Private Sub OpenConnection(ByVal strConStr As String)
        con = New SqlConnection(strConStr)
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
        da = New SqlDataAdapter(cmd)
        Dim _dt As New DataTable
        da.Fill(_dt)
        dtResult = _dt
    End Sub

    Public Function IsConnectionOK(Optional ByVal strConString As String = "") As Boolean
        Try
            If strConString <> "" Then
                OpenConnection(strConString)
            Else
                OpenConnection()
            End If

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

    Public Function IsConnectionOKv2(Optional ByVal strConString As String = "") As Boolean
        If strConString <> "" Then
            OpenConnection(strConString)
        Else
            OpenConnection()
        End If

        Dim task = System.Threading.Tasks.Task.Factory.StartNew(Function()

                                                                    Try
                                                                        con.Open()
                                                                        con.Close()
                                                                        Return True
                                                                    Catch
                                                                        Return False
                                                                    End Try
                                                                End Function)

        If Not (task.Wait(5 * 1000) AndAlso task.Result) Then
            'Throw New Exception("No response")
            strErrorMessage = "Connection timeout"
            Return False
        End If

        Return True
    End Function

    Public Function ExecuteQuery(ByVal strQuery As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand(strQuery, con)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function ExecuteQuery_Scalar(ByVal strQuery As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand(strQuery, con)

            ExecuteScalar(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function CheckIfHavePinChangeToday(ByVal ssNum As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("SELECT COUNT(*) FROM SSTRANSPINCHANGE WHERE SSNUM=@SSNUM AND ENCODE_DT=CAST(GETDATE() as date)", con)

            cmd.Parameters.AddWithValue("@SSNUM", ssNum)

            ExecuteScalar(CommandType.Text)

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
            cmd = New SqlCommand(strQuery, con)

            FillDataAdapter(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function INSERT_PRN_TXN(ByVal SSNUM As String, ByVal BRANCH_IP As String, ByVal BRANCH_CD As String,
                                   ByVal KIOSK_ID As String, ByVal PRN As String, ByVal MEMBERSHIP_TYPE As String,
                                   ByVal APPLICATION_PERIOD As String, ByVal CONTRIBUTION As Decimal, ByVal FLEXI_FUND As Decimal,
                                   ByVal TOTAL_AMOUNT As Decimal, ByVal DUE_DATE As String) As Boolean
        Try
            Dim sbQuery As New System.Text.StringBuilder
            sbQuery.Append("INSERT INTO SSTRANSPRN (SSNUM,ENCODE_DT,ENCODE_TME,BRANCH_IP,BRANCH_CD,KIOSK_ID,PRN,MEMBERSHIP_TYPE,APPLICATION_PERIOD,CONTRIBUTION,FLEXI_FUND,TOTAL_AMOUNT,DUE_DATE, FULL_NAME) ")
            sbQuery.Append("VALUES (@SSNUM,GETDATE(),GETDATE(),@BRANCH_IP,@BRANCH_CD,@KIOSK_ID,@PRN,@MEMBERSHIP_TYPE,@APPLICATION_PERIOD,@CONTRIBUTION,@FLEXI_FUND,@TOTAL_AMOUNT,@DUE_DATE, @FULL_NAME)")

            OpenConnection()
            cmd = New SqlCommand(sbQuery.ToString, con)

            cmd.Parameters.AddWithValue("@SSNUM", SSNUM)
            'cmd.Parameters.AddWithValue("@ENCODE_DT", Now.Date)
            'cmd.Parameters.AddWithValue("@ENCODE_TME", Now.TimeOfDay)
            cmd.Parameters.AddWithValue("@BRANCH_IP", BRANCH_IP)
            cmd.Parameters.AddWithValue("@BRANCH_CD", BRANCH_CD)
            cmd.Parameters.AddWithValue("@KIOSK_ID", KIOSK_ID)
            cmd.Parameters.AddWithValue("@PRN", PRN)
            cmd.Parameters.AddWithValue("@MEMBERSHIP_TYPE", MEMBERSHIP_TYPE)
            cmd.Parameters.AddWithValue("@APPLICATION_PERIOD", APPLICATION_PERIOD)
            cmd.Parameters.AddWithValue("@CONTRIBUTION", CONTRIBUTION)
            cmd.Parameters.AddWithValue("@FLEXI_FUND", FLEXI_FUND)
            cmd.Parameters.AddWithValue("@TOTAL_AMOUNT", TOTAL_AMOUNT)
            cmd.Parameters.AddWithValue("@DUE_DATE", DUE_DATE)
            cmd.Parameters.AddWithValue("@FULL_NAME", HTMLDataExtractor.MemberFullName)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function validateCardBlockBySSNUM(ByVal ssNum As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("ValidateCardBlockBySSNUM", con)

            cmd.Parameters.AddWithValue("@SSNUM", ssNum)

            FillDataAdapter(CommandType.StoredProcedure)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SaveCardBlockBySSNUM(ByVal ssNum As String, ByVal attemptCntr As Short) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("SaveCardBlockBySSNUM", con)

            cmd.Parameters.AddWithValue("@SSNUM", ssNum)
            cmd.Parameters.AddWithValue("@KIOSK_ID", kioskID)
            cmd.Parameters.AddWithValue("@ATTEMPT_CNTR", attemptCntr)

            ExecuteNonQuery(CommandType.StoredProcedure)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function UpdateCounter(ByVal CRN As String, ByVal ATTMPTCNTR As Integer) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("UPDATE SSINFOTERMAUTHFAILED SET ATTMPTCNTR=@ATTMPTCNTR WHERE CRN=@CRN", con)
            cmd.Parameters.AddWithValue("@ATTMPTCNTR", ATTMPTCNTR)
            cmd.Parameters.AddWithValue("@CRN", CRN)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function InsertSSINFOTERMACCESS(ByVal KIOSK_ID As String, ByVal TRANS_TYPE As String, ByVal REF_NUM As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("INSERT INTO SSINFOTERMACCESS (KIOSK_ID,TRANS_TYPE,REF_NUM,TRANS_DT) VALUES (@KIOSK_ID,@TRANS_TYPE,@REF_NUM,GETDATE())", con)
            cmd.Parameters.AddWithValue("@KIOSK_ID", KIOSK_ID)
            cmd.Parameters.AddWithValue("@TRANS_TYPE", TRANS_TYPE)
            cmd.Parameters.AddWithValue("@REF_NUM", REF_NUM)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function InsertAUTHFAILED(ByVal REF_NUM As String, ByVal ATTMPTCNTR As Short, ByVal STATUS As Boolean, ByVal TRANS_TYPE As String) As Boolean
        Try
            OpenConnection()
            'cmd = New SqlCommand("INSERT INTO SSINFOTERMAUTHFAILED (CRN, ATTMPTCNTR, STATUS, DATEPOST) VALUES (@CRN, 1, 1, GETDATE())", con)
            cmd = New SqlCommand("prcInsertAUTHFAILED", con)
            cmd.Parameters.AddWithValue("@REF_NUM", REF_NUM)
            cmd.Parameters.AddWithValue("@ATTMPTCNTR", ATTMPTCNTR)
            cmd.Parameters.AddWithValue("@STATUS", STATUS)
            cmd.Parameters.AddWithValue("@TRANS_TYPE", TRANS_TYPE)

            ExecuteNonQuery(CommandType.StoredProcedure)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function ChangeSSINFOTERMAUTHFAILEDStatus(ByVal REF_NUM As String, ByVal STATUS As Boolean) As Boolean
        Try
            Dim strQuery As String = "UPDATE SSINFOTERMAUTHFAILED SET STATUS=@STATUS, ATTMPTCNTR=0 WHERE REF_NUM=@REF_NUM"

            If Not STATUS Then
                strQuery = "UPDATE SSINFOTERMAUTHFAILED SET STATUS=@STATUS, DATEBLOCKED=GETDATE() WHERE REF_NUM=@REF_NUM"
            End If

            OpenConnection()
            cmd = New SqlCommand(strQuery, con)
            cmd.Parameters.AddWithValue("@STATUS", STATUS)
            cmd.Parameters.AddWithValue("@REF_NUM", REF_NUM)

            ExecuteNonQuery(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function GetAttemptCounterByCRN(ByVal REF_NUM As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("SELECT ATTMPTCNTR FROM SSINFOTERMAUTHFAILED WHERE REF_NUM=@REF_NUM", con)
            cmd.Parameters.AddWithValue("@REF_NUM", REF_NUM)

            ExecuteScalar(CommandType.Text)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Public Function SelectKioskXML(ByVal strKioskID As String) As Boolean
        Try
            OpenConnection()
            cmd = New SqlCommand("SelectKioskXML", con)
            cmd.Parameters.AddWithValue("@kioskID", strKioskID)

            FillDataAdapter(CommandType.StoredProcedure)

            Return True
        Catch ex As Exception
            strErrorMessage = ex.Message
            Return False
        Finally
            CloseConnection()
        End Try
    End Function

    Private disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                ' TODO: free other state (managed objects).
            End If

            cmd = Nothing
            da = Nothing
            con = Nothing

            ' TODO: free your own state (unmanaged objects).
            ' TODO: set large fields to null.
        End If
        Me.disposedValue = True
    End Sub

#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class
