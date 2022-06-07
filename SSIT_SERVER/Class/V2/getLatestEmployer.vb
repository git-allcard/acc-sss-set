Public Class getLatestEmployer

    Private processFlagValue As String = ""
    Public Property processFlag() As String
        Get
            Return processFlagValue
        End Get
        Set(ByVal value As String)
            processFlagValue = value
        End Set
    End Property

    Private exceptionValue As String = ""
    Public Property Exception() As String
        Get
            Return exceptionValue
        End Get
        Set(ByVal value As String)
            exceptionValue = value
        End Set
    End Property

    Private getLatestEmployersValue As List(Of MobileWS2BeanService.employer)
    Public Property getLatestEmployers() As List(Of MobileWS2BeanService.employer)
        Get
            Return getLatestEmployersValue
        End Get
        Set(ByVal value As List(Of MobileWS2BeanService.employer))
            getLatestEmployersValue = value
        End Set
    End Property

    Public Sub New(ByVal type As String)
        Dim slMobileWS2BeanService As New SalaryLoan.slMobileWS2BeanService
        If slMobileWS2BeanService.getLatestEmployer(SSStempFile, type) Then
            processFlagValue = slMobileWS2BeanService.getLatestEmployerResponse(0).processFlag
            getLatestEmployersValue = slMobileWS2BeanService.getLatestEmployerResponse

            Select Case processFlagValue
                Case "2"
                    exceptionValue = "PLEASE BE INFORMED THAT YOU HAVE MULTIPLE EMPLOYER WITH SIMULTANEOUS EMPLOYMENT DATE. HENCE, YOU WILL BE REQUIRED TO SUBMIT DOCUMENTARY REQUIREMENTS. FOR THIS REASON, THE RECEIPT OF YOUR RETIREMENT CLAIM APPLICATION SHALL BE HANDLED AT ANY SSS BRANCH NEAR YOU."
                Case "3"
                    exceptionValue = "PLEASE BE INFORMED THAT YOUR CERTIFYING EMPLOYER " & slMobileWS2BeanService.getLatestEmployerResponse(0).employerName & " IS NOT REGISTERED IN THE SSS WEBSITE. HENCE, YOU WILL BE REQUIRED TO SUBMIT DOCUMENTARY REQUIREMENTS. FOR THIS REASON, THE RECEIPT OF YOUR RETIREMENT CLAIM APPLICATION SHALL BE HANDLED AT ANY SSS BRANCH NEAR YOU."
            End Select
        Else
            exceptionValue = "UNABLE TO CONNECT TO getLatestEmployer"
            SharedFunction.ShowUnableToConnectToRemoteServerMessage()
        End If
        slMobileWS2BeanService = Nothing
    End Sub

End Class
