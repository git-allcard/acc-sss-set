Public Class CardBlock

    Private sssNumberValue As String
    Public Property sssNumber() As String
        Get
            Return sssNumberValue
        End Get
        Set(ByVal value As String)
            sssNumberValue = value
        End Set
    End Property

    Private attempCntrValue As Short = 0
    Public Property attempCntr() As Short
        Get
            Return attempCntrValue
        End Get
        Set(ByVal value As Short)
            attempCntrValue = value
        End Set
    End Property

    Private isBlockedValue As Boolean = False
    Public Property isBlocked() As Boolean
        Get
            Return isBlockedValue
        End Get
        Set(ByVal value As Boolean)
            isBlockedValue = value
        End Set
    End Property

    Public Sub New(ByVal ssNum As String)
        Try
            sssNumberValue = ssNum
            Dim dal As New DAL_Mssql()
            If dal.validateCardBlockBySSNUM(ssNum) Then
                Dim rw As DataRow = dal.TableResult.Rows(0)
                isBlockedValue = rw("isBlocked")
                attempCntrValue = rw("attemptCntr")
            End If
            dal.Dispose()
            dal = Nothing
        Catch ex As Exception
            SharedFunction.ShowErrorMessage("error in cardBlock new - ss " & ssNum)
        End Try


    End Sub

    Public Sub SaveCardBlockBySSNUM()
        Dim dal As New DAL_Mssql
        dal.SaveCardBlockBySSNUM(sssNumberValue, attempCntrValue)
        dal.Dispose()
        dal = Nothing
    End Sub

End Class
