Public Class SSUmidService

    Private exceptionsValue As String
    Public Property exceptions() As String
        Get
            Return exceptionsValue
        End Get
        Set(ByVal value As String)
            exceptionsValue = value
        End Set
    End Property

    Private webserviceResponseValue As UmidService.webserviceResponse
    Public Property webserviceResponse() As UmidService.webserviceResponse
        Get
            Return webserviceResponseValue
        End Get
        Set(ByVal value As UmidService.webserviceResponse)
            webserviceResponseValue = value
        End Set
    End Property

    Public Function isSSSMember(ByVal crn As String, ByVal dob As String, ByVal firstName As String, ByVal middleName As String, ByVal lastName As String, ByVal suffix As String) As Boolean
        Dim service As New UmidService.UmidService
        'service.Url = "http://m41sv145:7009/TestUmidWSBean/UmidService?WSDL" 'UmidService_URL
        service.Url = UmidService_URL
        Try
            webserviceResponseValue = service.isSSSMember(UmidService_Token, crn, dob, firstName, middleName, lastName, suffix)
            'webserviceResponseValue = service.isSSSMember("TestingSSIT", crn, dob, firstName, middleName, lastName, suffix)
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function isCsnExist(ByVal csn As String) As Boolean
        Dim service As New UmidService.UmidService
        service.Url = UmidService_URL
        Try
            webserviceResponseValue = service.isCsnExist(UmidService_Token, csn)
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

    Public Function insertGSISUmid(ByVal umidData As umid) As Boolean
        Dim service As New UmidService.UmidService
        service.Url = UmidService_URL
        Try
            Dim dob As String = String.Format("{0}{1}{2}", umidData.dateOfBirth.Substring(6, 2), umidData.dateOfBirth.Substring(4, 2), umidData.dateOfBirth.Substring(0, 4))
            Dim ccdt As String = String.Format("{0}{1}{2}", umidData.ccdt.Substring(6, 2), umidData.ccdt.Substring(4, 2), umidData.ccdt.Substring(0, 4))
            webserviceResponseValue = service.insertGSISUmid(umidData.csn, umidData.csn.Substring(0, 2), umidData.sssNumber, umidData.crn, dob, umidData.firstName, umidData.middleName, umidData.lastName, umidData.suffix,
                                                             umidData.gender, umidData.postalCode, umidData.countryCode, umidData.province, umidData.city, umidData.barangay, umidData.subdivision, umidData.streetName, umidData.houseLotBlock,
                                                             umidData.roomFloorUnitBldg, umidData.cityBirth, umidData.provinceBirth, umidData.countryCodeBirth, umidData.maritalStatus, umidData.firstNameFather, umidData.middleNameFather,
                                                             umidData.lastNameFather, umidData.suffixFather, umidData.firstNamemother, umidData.middleNamemother, umidData.lastNamemother, umidData.suffixmother, umidData.leftPrimaryCode,
                                                             umidData.rightPrimaryCode, umidData.leftBackupCode, umidData.rightBackupCode, umidData.height, umidData.weight, umidData.distinguishFeature, umidData.tin, umidData.cardStatusCode,
                                                             ccdt, UmidService_Token)
            Return True
        Catch ex As Exception
            exceptions = ex.Message
            Return False
        Finally
            service.Dispose()
        End Try
    End Function

End Class
