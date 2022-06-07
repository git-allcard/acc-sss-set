Public Class SWR

    Private exceptionsValue As String
    Public Property exceptions() As String
        Get
            Return exceptionsValue
        End Get
        Set(ByVal value As String)
            exceptionsValue = value
        End Set
    End Property

    Private verifyAccountResponseValue As String
    Public Property verifyAccountResponse() As String
        Get
            Return verifyAccountResponseValue
        End Get
        Set(ByVal value As String)
            verifyAccountResponseValue = value
        End Set
    End Property

    Private verifyUserAccountResponseValue As String
    Public Property verifyUserAccountResponse() As String
        Get
            Return verifyUserAccountResponseValue
        End Get
        Set(ByVal value As String)
            verifyUserAccountResponseValue = value
        End Set
    End Property

    Private webResponseValue As SimpleWebRegistrationService.webResponse
    Public Property webResponse() As SimpleWebRegistrationService.webResponse
        Get
            Return webResponseValue
        End Get
        Set(ByVal value As SimpleWebRegistrationService.webResponse)
            webResponseValue = value
        End Set
    End Property

    Private registerUserAccountResponseValue As String
    Public Property registerUserAccountResponse() As String
        Get
            Return registerUserAccountResponseValue
        End Get
        Set(ByVal value As String)
            registerUserAccountResponseValue = value
        End Set
    End Property

    'Public Function verifyAccount(ByVal sssNum As String) As Boolean
    '    Dim service As New SimpleWebRegistrationService.SimpleWebRegistrationService
    '    service.Url = SimpleWebRegistrationService_URL
    '    Try
    '        verifyAccountResponseValue = service.verifyAccount(sssNum)
    '        Return True
    '    Catch ex As Exception
    '        exceptions = ex.Message
    '        Return False
    '    Finally
    '        service.Dispose()
    '    End Try
    'End Function

    'Public Function verifyUserAccount(ByVal userId As String) As Boolean
    '    Dim service As New SimpleWebRegistrationService.SimpleWebRegistrationService
    '    service.Url = SimpleWebRegistrationService_URL 'http://10.0.4.252:3014/SWR/SimpleWebRegistrationService?WSDL
    '    Try
    '        verifyUserAccountResponseValue = service.verifyUserAccount(userId)
    '        Return True
    '    Catch ex As Exception
    '        exceptions = ex.Message
    '        Return False
    '    Finally
    '        service.Dispose()
    '    End Try
    'End Function

    'Public Function registerUserAccount(ByVal userId As String, ByVal emailAdd As String, ByVal sssNumber As String) As Boolean
    '    Dim service As New SimpleWebRegistrationService.SimpleWebRegistrationService
    '    Dim simWebRegInfo As New SimpleWebRegistrationService.simWebRegInfo
    '    service.Url = SimpleWebRegistrationService_URL
    '    Try
    '        simWebRegInfo.emailAdd = emailAdd
    '        simWebRegInfo.sssid = sssNumber
    '        simWebRegInfo.tokenID = SimpleWebRegistrationService_Token
    '        simWebRegInfo.tranToken = ""
    '        simWebRegInfo.userid = userId
    '        registerUserAccountResponseValue = service.registerUserAccount(simWebRegInfo)
    '        Return True
    '    Catch ex As Exception
    '        exceptions = ex.Message
    '        Return False
    '    Finally
    '        service.Dispose()
    '    End Try
    'End Function

    Public Function verifyUserAccount(ByVal sssNum As String, ByVal userId As String) As Short
        Dim service As New SimpleWebRegistrationService.Wes3in1Service
        service.Url = SimpleWebRegistrationService_URL
        Try
            webResponseValue = service.validateUserId(SimpleWebRegistrationService_Token, sssNum, userId)
            Return 0
        Catch ex As TimeoutException
            exceptions = ex.Message
            Return 1
        Catch ex As Exception
            exceptions = ex.Message
            Return SharedFunction.HandleUnableToConnectRemoteServerError(exceptions, 2)
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function insertToWesRegistration(ByVal sssNum As String, ByVal userId As String) As Short
        Dim service As New SimpleWebRegistrationService.Wes3in1Service
        service.Url = SimpleWebRegistrationService_URL
        Try
            webResponseValue = service.insertToWesRegistration(SimpleWebRegistrationService_Token, sssNum, userId)
            Return 0
        Catch ex As TimeoutException
            exceptions = ex.Message
            Return 1
        Catch ex As Exception
            exceptions = ex.Message
            Return SharedFunction.HandleUnableToConnectRemoteServerError(exceptions, 2)
        Finally
            service.Dispose()
        End Try
    End Function


End Class
