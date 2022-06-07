Public Class getContactInfo

    Public Sub New()
        'get contact
        Dim slMobileWS2BeanService As New SalaryLoan.slMobileWS2BeanService
        Select Case slMobileWS2BeanService.getContactInfoAddress(SSStempFile)
            Case 0
                If slMobileWS2BeanService.getContactInfoAddressResponse.processFlag = "1" Then
                    emailValue = GetPropertyValue(slMobileWS2BeanService.getContactInfoAddressResponse.email, isEmailValidValue)
                    mailingAddressValue = GetPropertyValue(slMobileWS2BeanService.getContactInfoAddressResponse.localMailing, isMailingAddressValidValue)
                    telephoneNosValue = GetPropertyValue(slMobileWS2BeanService.getContactInfoAddressResponse.landline, New Boolean)
                    mobileNosValue = GetPropertyValue(slMobileWS2BeanService.getContactInfoAddressResponse.mobile, isMobileNosValidValue)

                    If Not isEmailValidValue Then
                        isContactInfoValidValue = False
                    ElseIf Not isMailingAddressValidValue Then
                        isContactInfoValidValue = False
                    ElseIf Not isMobileNosValidValue Or Not isTelephoneNosValidValue Then
                        isContactInfoValidValue = False
                    End If

                    processFlagValue = slMobileWS2BeanService.getContactInfoAddressResponse.processFlag
                    returnMessageValue = slMobileWS2BeanService.getContactInfoAddressResponse.returnMessage
                Else
                    exceptionValue = "processFlag " & processFlag & ". " & slMobileWS2BeanService.getContactInfoAddressResponse.returnMessage
                    SharedFunction.ShowWarningMessage(slMobileWS2BeanService.getContactInfoAddressResponse.returnMessage & "." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.")
                End If
            Case 1
                exceptionValue = slMobileWS2BeanService.exceptions
                SharedFunction.ShowUnableToConnectToRemoteServerMessage()
            Case Else
                exceptionValue = slMobileWS2BeanService.exceptions
                SharedFunction.ShowAPIResponseMessage(slMobileWS2BeanService.exceptions.ToUpper)
        End Select
        slMobileWS2BeanService = Nothing
    End Sub

    Private Function GetPropertyValue(ByVal apiValue As String, ByRef myObjBln As String) As String
        If apiValue.Trim = "" Or apiValue = "null" Then
            myObjBln = False
            Return ""
        Else
            myObjBln = True
            Return apiValue
        End If
    End Function

    Private exceptionValue As String = ""
    Public Property Exception() As String
        Get
            Return exceptionValue
        End Get
        Set(ByVal value As String)
            exceptionValue = value
        End Set
    End Property

    Private emailValue As String = ""
    Public Property Email() As String
        Get
            Return emailValue
        End Get
        Set(ByVal value As String)
            emailValue = value
        End Set
    End Property

    Private telephoneNosValue As String = ""
    Public Property TelephoneNos() As String
        Get
            Return telephoneNosValue
        End Get
        Set(ByVal value As String)
            telephoneNosValue = value
        End Set
    End Property

    Private mobileNosValue As String = ""
    Public Property MobileNos() As String
        Get
            Return mobileNosValue
        End Get
        Set(ByVal value As String)
            mobileNosValue = value
        End Set
    End Property

    Private mailingAddressValue As String = ""
    Public Property MailingAddress() As String
        Get
            Return mailingAddressValue
        End Get
        Set(ByVal value As String)
            mailingAddressValue = value
        End Set
    End Property

    Private isMailingAddressValidValue As Boolean = True
    Public Property IsMailingAddressValid() As Boolean
        Get
            Return isMailingAddressValidValue
        End Get
        Set(ByVal value As Boolean)
            isMailingAddressValidValue = value
        End Set
    End Property

    Private isEmailValidValue As Boolean = True
    Public Property IsEmailValid() As Boolean
        Get
            Return isEmailValidValue
        End Get
        Set(ByVal value As Boolean)
            isEmailValidValue = value
        End Set
    End Property

    Private isMobileNosValidValue As Boolean = True
    Public Property IsMobileNosValid() As Boolean
        Get
            Return isMobileNosValidValue
        End Get
        Set(ByVal value As Boolean)
            isMobileNosValidValue = value
        End Set
    End Property

    Private isTelephoneNosValidValue As Boolean = True
    Public Property IsTelephoneNosValid() As Boolean
        Get
            Return isTelephoneNosValidValue
        End Get
        Set(ByVal value As Boolean)
            isTelephoneNosValidValue = value
        End Set
    End Property

    Private isContactInfoValidValue As Boolean = True
    Public Property IsContactInfoValid() As Boolean
        Get
            Return isContactInfoValidValue
        End Get
        Set(ByVal value As Boolean)
            isContactInfoValidValue = value
        End Set
    End Property

    Private processFlagValue As String
    Public ReadOnly Property processFlag() As String
        Get
            Return processFlagValue
        End Get
    End Property

    Private returnMessageValue As String
    Public ReadOnly Property returnMessage() As String
        Get
            Return returnMessageValue
        End Get
    End Property

End Class
