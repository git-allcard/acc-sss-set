Public Class OnlineRetirement

    Private exceptionsValue As String
    Public Property exceptions() As String
        Get
            Return exceptionsValue
        End Get
        Set(ByVal value As String)
            exceptionsValue = value
        End Set
    End Property

    Private txnTokenValue As String
    Public Property txnToken() As String
        Get
            Return txnTokenValue
        End Get
        Set(ByVal value As String)
            txnTokenValue = value
        End Set
    End Property

    Public Function transactionToken() As Boolean
        Dim service As New EligibilityWebserviceImplService.EligibilityWebserviceImplService
        service.Url = EligibilityWebserviceImplService_URL
        Try
            txnTokenValue = service.AuthenticateToken(EligibilityWebserviceImplService_Token).transac_token
            Return True
        Catch ex As Exception
            exceptionsValue = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Private memberClaimInformationEntitiesResponseValue As List(Of OnlineRetirementWebServiceImplService.memberClaimInformationEntity)
    Public Property memberClaimInformationEntitiesResponse() As List(Of OnlineRetirementWebServiceImplService.memberClaimInformationEntity)
        Get
            Return memberClaimInformationEntitiesResponseValue
        End Get
        Set(ByVal value As List(Of OnlineRetirementWebServiceImplService.memberClaimInformationEntity))
            memberClaimInformationEntitiesResponseValue = value
        End Set
    End Property


    Public Function determineDoctg_OnlineRt(ByVal sssNum As String, ByVal filingDate As String, ByVal separationDate As String) As Boolean
        Dim service As New OnlineRetirementWebServiceImplService.OnlineRetirementWebServiceImplService
        service.Url = OnlineRetirementWebServiceImplService_URL
        Try
            Dim txnToken As String = ""
            Try
                memberClaimInformationEntitiesResponseValue = service.autoConnect(OnlineRetirementWebServiceImplService_Token).ToList
                txnToken = memberClaimInformationEntitiesResponseValue(0).tran_token
            Catch ex As Exception
                txnToken = ""
            End Try

            If txnToken = "" Then
                exceptionsValue = "FAILED TO GENERATE TOKEN"
                Return False
            End If

            memberClaimInformationEntitiesResponseValue = service.determineDoctg_OnlineRt(sssNum, filingDate, OnlineRetirementWebServiceImplService_Token, txnToken, separationDate).ToList
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function checkElig_OnlineRt(ByVal sssNum As String, ByVal filingDate As String) As Boolean
        Dim service As New OnlineRetirementWebServiceImplService.OnlineRetirementWebServiceImplService
        service.Url = OnlineRetirementWebServiceImplService_URL
        Try
            Dim txnToken As String = ""
            Try
                memberClaimInformationEntitiesResponseValue = service.autoConnect(OnlineRetirementWebServiceImplService_Token).ToList
                txnToken = memberClaimInformationEntitiesResponseValue(0).tran_token
            Catch ex As Exception
                txnToken = ""
            End Try

            If txnToken = "" Then
                exceptionsValue = "FAILED TO GENERATE TOKEN"
                Return False
            End If

            memberClaimInformationEntitiesResponseValue = service.checkElig_OnlineRt(sssNum, filingDate, OnlineRetirementWebServiceImplService_Token, txnToken).ToList
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function employmentHistory(ByVal sssNum As String, ByVal filingDate As String) As Boolean
        Dim service As New OnlineRetirementWebServiceImplService.OnlineRetirementWebServiceImplService
        service.Url = OnlineRetirementWebServiceImplService_URL
        Try
            Dim txnToken As String = ""
            Try
                memberClaimInformationEntitiesResponseValue = service.autoConnect(OnlineRetirementWebServiceImplService_Token).ToList
                txnToken = memberClaimInformationEntitiesResponseValue(0).tran_token
            Catch ex As Exception
                txnToken = ""
            End Try

            If txnToken = "" Then
                exceptionsValue = "FAILED TO GENERATE TOKEN"
                Return False
            End If

            memberClaimInformationEntitiesResponseValue = service.employmentHistory(sssNum, OnlineRetirementWebServiceImplService_Token, txnToken).ToList

            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function employmentHistoryToken(ByVal sssNum As String, ByRef token As String) As Boolean
        Dim service As New OnlineRetirementWebServiceImplService.OnlineRetirementWebServiceImplService
        service.Url = "https://ww8.sss.gov.ph/TestOnlineRetirement/OnlineRetirementWebServiceImplService?WSDL"
        Try
            Dim txnToken As String = ""
            Try
                memberClaimInformationEntitiesResponseValue = service.autoConnect("TDGYA928R7MXA112817095420").ToList
                token = memberClaimInformationEntitiesResponseValue(0).tran_token
            Catch ex As Exception
                txnToken = ""
            End Try

            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function employerCert(ByVal sssNum As String, ByVal endDate As String) As Boolean
        Dim service As New OnlineRetirementWebServiceImplService.OnlineRetirementWebServiceImplService
        service.Url = OnlineRetirementWebServiceImplService_URL
        Try
            Dim txnToken As String = ""
            Try
                memberClaimInformationEntitiesResponseValue = service.autoConnect(OnlineRetirementWebServiceImplService_Token).ToList
                txnToken = memberClaimInformationEntitiesResponseValue(0).tran_token
            Catch ex As Exception
                txnToken = ""
            End Try

            If txnToken = "" Then
                exceptionsValue = "FAILED TO GENERATE TOKEN"
                Return False
            End If

            memberClaimInformationEntitiesResponseValue = service.employerCert(sssNum, 12, endDate).ToList
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function



End Class
