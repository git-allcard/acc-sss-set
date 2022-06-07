Public Class SalaryLoan

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

    Class eligibwebservice
        Inherits SalaryLoan

        Private calleligibilityResponseValue As EligibilityWebserviceImplService.eligibWsResponse
        Public Property calleligibilityResponse() As EligibilityWebserviceImplService.eligibWsResponse
            Get
                Return calleligibilityResponseValue
            End Get
            Set(ByVal value As EligibilityWebserviceImplService.eligibWsResponse)
                calleligibilityResponseValue = value
            End Set
        End Property

        Public Function calleligibility(ByVal ssNum As String, ByVal empId As String, ByVal seqNo As String) As Short
            Dim service As New EligibilityWebserviceImplService.EligibilityWebserviceImplService
            service.Url = EligibilityWebserviceImplService_URL
            Try
                Dim txnToken As String = ""
                Try
                    txnToken = service.AuthenticateToken(EligibilityWebserviceImplService_Token).transac_token
                Catch ex As Exception
                    txnToken = ""
                End Try

                If txnToken = "" Then
                    exceptionsValue = "FAILED TO GENERATE TOKEN"
                    Return 2
                End If

                calleligibilityResponseValue = service.calleligibility(ssNum, "S", empId, seqNo, txnToken, EligibilityWebserviceImplService_Token)
                Return 0
            Catch ex As TimeoutException
                exceptions = ex.Message
                Return 1
            Catch ex As Exception
                exceptions = ex.Message
                Return SharedFunction.HandleUnableToConnectRemoteServerError(exceptions, 3)
            Finally
                service.Dispose()
            End Try
        End Function

    End Class

    Class slMobileWS2BeanService
        Inherits SalaryLoan

        Const wsUserName As String = "SSIT"

        Private getMemberTypeResponseValue As MobileWS2BeanService.memberType
        Public Property getMemberTypeResponse() As MobileWS2BeanService.memberType
            Get
                Return getMemberTypeResponseValue
            End Get
            Set(ByVal value As MobileWS2BeanService.memberType)
                getMemberTypeResponseValue = value
            End Set
        End Property

        Private insertRetirementAppResponseValue As MobileWS2BeanService.webTransactionResponse
        Public Property insertRetirementAppResponse() As MobileWS2BeanService.webTransactionResponse
            Get
                Return insertRetirementAppResponseValue
            End Get
            Set(ByVal value As MobileWS2BeanService.webTransactionResponse)
                insertRetirementAppResponseValue = value
            End Set
        End Property

        Private getContactInfoAddressResponseValue As MobileWS2BeanService.contactInfoAdress
        Public Property getContactInfoAddressResponse() As MobileWS2BeanService.contactInfoAdress
            Get
                Return getContactInfoAddressResponseValue
            End Get
            Set(ByVal value As MobileWS2BeanService.contactInfoAdress)
                getContactInfoAddressResponseValue = value
            End Set
        End Property

        Private submitSalaryLoanApplicationResponseValue As MobileWS2BeanService.salaryLoanApplicationResponse
        Public Property submitSalaryLoanApplicationResponse() As MobileWS2BeanService.salaryLoanApplicationResponse
            Get
                Return submitSalaryLoanApplicationResponseValue
            End Get
            Set(ByVal value As MobileWS2BeanService.salaryLoanApplicationResponse)
                submitSalaryLoanApplicationResponseValue = value
            End Set
        End Property

        Private getEmployerAddressesSLResponseValue As List(Of MobileWS2BeanService.employerAddress)
        Public Property getEmployerAddressesSLResponse() As List(Of MobileWS2BeanService.employerAddress)
            Get
                Return getEmployerAddressesSLResponseValue
            End Get
            Set(ByVal value As List(Of MobileWS2BeanService.employerAddress))
                getEmployerAddressesSLResponseValue = value
            End Set
        End Property

        Private getLatestEmployerResponseValue As List(Of MobileWS2BeanService.employer)
        Public Property getLatestEmployerResponse() As List(Of MobileWS2BeanService.employer)
            Get
                Return getLatestEmployerResponseValue
            End Get
            Set(ByVal value As List(Of MobileWS2BeanService.employer))
                getLatestEmployerResponseValue = value
            End Set
        End Property

        Private getMultiEmployerAddressResponseValue As List(Of MobileWS2BeanService.employerAddress)
        Public Property getMultiEmployerAddressResponse() As List(Of MobileWS2BeanService.employerAddress)
            Get
                Return getMultiEmployerAddressResponseValue
            End Get
            Set(ByVal value As List(Of MobileWS2BeanService.employerAddress))
                getMultiEmployerAddressResponseValue = value
            End Set
        End Property

        Public Function getMemberType(ByVal ssNum As String) As Short
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                getMemberTypeResponseValue = service.getMemberType(MobileWS2BeanService_Token, wsUserName, ssNum)
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

        Public Function getContactInfoAddress(ByVal ssNum As String) As Short
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                getContactInfoAddressResponseValue = service.getContactInfoAddress(MobileWS2BeanService_Token, wsUserName, ssNum)
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

        Public Function getLatestEmployer(ByVal ssNum As String, ByVal tranType As String) As Boolean
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                getLatestEmployerResponse = service.getLatestEmployer(MobileWS2BeanService_Token, wsUserName, ssNum, tranType).ToList
                Return True
            Catch ex As Exception
                exceptionsValue = ex.Message
                Return False
            Finally
                service.Dispose()
            End Try
        End Function

        Public Function getMultiEmployerAddress(ByVal employerSSNum As String) As Boolean
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                getMultiEmployerAddressResponseValue = service.getMultiEmployerAddress(MobileWS2BeanService_Token, wsUserName, employerSSNum).ToList
                Return True
            Catch ex As Exception
                exceptionsValue = ex.Message
                Return False
            Finally
                service.Dispose()
            End Try
        End Function

        Public Function submitSalaryLoanApplication(ByVal ssNum As String, ByVal memberStatus As String, ByVal employerSSNumber As String, ByVal erBrn As String, ByVal loanAmount As String,
                                                    ByVal loanMonth As String, ByVal averageMsc As String, ByVal totalBalance As String, ByVal serviceCharge As String, ByVal netLoan As String,
                                                    ByVal monthlyAmort As String, ByVal disbursementCode As String, ByVal bankCode As String, ByVal brstn As String, ByVal acctNo As String,
                                                    ByVal fundingBank As String, ByVal advanceInterest As String) As Boolean
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL

            Try
                Dim sb As New System.Text.StringBuilder
                sb.Append("URL: " & MobileWS2BeanService_URL & vbNewLine)
                sb.Append("service: submitSalaryLoanApplication" & vbNewLine)
                sb.Append("ssNum: " & ssNum & vbNewLine)
                sb.Append("memberStatus: " & memberStatus & vbNewLine)
                sb.Append("employerSSNumber: " & employerSSNumber & vbNewLine)
                sb.Append("erBrn: " & erBrn & vbNewLine)
                sb.Append("loanAmount: " & loanAmount.Replace(",", "") & vbNewLine)
                sb.Append("loanMonth: " & loanMonth & vbNewLine)
                sb.Append("averageMsc: " & averageMsc & vbNewLine)
                sb.Append("totalBalance: " & totalBalance & vbNewLine)
                sb.Append("serviceCharge: " & serviceCharge & vbNewLine)
                sb.Append("netLoan: " & netLoan & vbNewLine)
                sb.Append("monthlyAmort: " & monthlyAmort & vbNewLine)
                sb.Append("disbursementCode: " & disbursementCode & vbNewLine)
                sb.Append("bankCode: " & bankCode & vbNewLine)
                sb.Append("brstn: " & brstn & vbNewLine)
                sb.Append("acctNo: " & acctNo & vbNewLine)
                sb.Append("fundingBank: " & fundingBank & vbNewLine)
                sb.Append("advanceInterest: " & advanceInterest & vbNewLine)
                IO.File.WriteAllText("D:\WORK\" & ssNum & ".txt", sb.ToString)
            Catch ex As Exception
            End Try

            Console.WriteLine("Halt here")

            Try
                submitSalaryLoanApplicationResponseValue = service.submitSalaryLoanApplication(MobileWS2BeanService_Token, ssNum, memberStatus, employerSSNumber, erBrn, loanAmount.Replace(",", ""), loanMonth, averageMsc, totalBalance, serviceCharge, netLoan,
                                                                                               monthlyAmort, disbursementCode, bankCode, brstn, acctNo, fundingBank, advanceInterest)
                Return True
            Catch ex As Exception
                exceptionsValue = ex.Message
                Return False
            Finally
                service.Dispose()
            End Try
        End Function

        Public Function getEmployerAddressesSL(ByVal employerSSNumber As String) As Short
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                getEmployerAddressesSLResponseValue = service.getEmployerAddressesSL(MobileWS2BeanService_Token, employerSSNumber).ToList
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

        Public Function insertRetirementApp(ByVal certificationTag As String, ByVal ssNumber As String,
                                            ByVal dateOfContingency As String, ByVal address1 As String, ByVal email As String, ByVal landLine As String, ByVal mobileNo As String,
                                            ByVal advance18Months As String, ByVal bankBRSTN As String, ByVal acctNo As String, ByVal dateOfSeparation As String,
                                            ByVal retirementFlag As String, ByVal retirementAmount As String, ByVal appliedFrom As String, ByVal membershipStatus As String,
                                            ByVal employerSSNumber As String, ByVal employerERBR As String) As Boolean
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                'Try
                '    Dim sb As String = IO.File.ReadAllText("or_template.txt")
                '    sb = sb.Replace("@01", certificationTag).Replace("@02", SSStempFile).Replace("@03", dateOfContingency).Replace("@04", address1).Replace("@05", email).Replace("@06", landLine).Replace("@07", mobileNo).Replace("@08", advance18Months.ToUpper().Substring(0, 1)).Replace("@09", bankBRSTN).Replace("@10", acctNo).Replace("@11", dateOfSeparation).Replace("@12", retirementAmount).Replace("@13", membershipStatus.ToUpper.Substring(0, 1)).Replace("@14", employerSSNumber).Replace("@15", employerERBR).Replace("@16", retirementFlag)
                '    IO.File.WriteAllText(SSStempFile & "_or.txt", sb)
                'Catch ex As Exception
                'End Try

                'insertRetirementAppResponseValue = service.insertRetirementApp(MobileWS2BeanService_Token, wsUserName, "", "", certificationTag, ssNumber, dateOfContingency, address1, "", "", email, advance18Months.ToUpper().Substring(0, 1), bankBRSTN, acctNo, landLine, mobileNo, dateOfSeparation, retirementFlag, retirementAmount, appliedFrom, membershipStatus.ToUpper.Substring(0, 1), employerSSNumber, employerERBR)
                Dim membershipStatusCode As String = CHECK_MEMSTATUS_Settings.Substring(0, 1).ToUpper 'membershipStatus.ToUpper.Substring(0, 1).ToUpper
                If membershipStatusCode = "C" Then membershipStatusCode = "E"

                insertRetirementAppResponseValue = service.insertRetirementApp(MobileWS2BeanService_Token, wsUserName, "", "", certificationTag, ssNumber, dateOfContingency, address1, "", SharedFunction.GetPostalCodeFromAddress(address1), email, advance18Months.ToUpper().Substring(0, 1), bankBRSTN, acctNo, landLine, mobileNo, dateOfSeparation, retirementFlag, retirementAmount, appliedFrom, membershipStatusCode, employerSSNumber, employerERBR)

                Return True
            Catch ex As Exception
                exceptionsValue = ex.Message
                Return False
            Finally
                service.Dispose()
            End Try
        End Function


    End Class

    Class webcallingds
        Inherits SalaryLoan

        Private calldisclosureResponseValue As DisclosureWebserviceImplService.disclosureWsResponse
        Public Property calldisclosureResponse() As DisclosureWebserviceImplService.disclosureWsResponse
            Get
                Return calldisclosureResponseValue
            End Get
            Set(ByVal value As DisclosureWebserviceImplService.disclosureWsResponse)
                calldisclosureResponseValue = value
            End Set
        End Property

        Public Function calldisclosure(ByVal ssNum As String, ByVal erNum As String, ByVal loanType As String, ByVal loanAmount As Double, ByVal installTerm As Integer, ByVal urIds As String,
                                       ByVal prevLoanAmount As Double, ByVal serviceCharge As Double, ByVal seqNo As String, ByVal address As String) As Short
            Dim service As New DisclosureWebserviceImplService.DisclosureWebserviceImplService
            service.Url = DisclosureWebserviceImplService_URL
            Try
                Try
                    If transactionToken() Then txnToken = txnTokenValue
                Catch ex As Exception
                    txnToken = ""
                End Try

                If txnToken = "" Then
                    exceptionsValue = "FAILED TO GENERATE TOKEN"
                    Return 2
                End If

                calldisclosureResponseValue = service.calldisclosure(ssNum, erNum, loanType, loanAmount, installTerm, urIds, prevLoanAmount, serviceCharge, txnToken, DisclosureWebserviceImplService_Token, seqNo, address)
                Return 0
            Catch ex As TimeoutException
                exceptions = ex.Message
                Return 1
            Catch ex As Exception
                exceptions = ex.Message
                Return SharedFunction.HandleUnableToConnectRemoteServerError(exceptions, 3)
            Finally
                service.Dispose()
            End Try
        End Function



    End Class

    Class slBankWorkflowWebService
        Inherits SalaryLoan

        Private getBankAccountListBySSNumberResponseValue As List(Of BankWorkflowWebService.accountNumberWorkflow)
        Public Property getBankAccountListBySSNumberResponse() As List(Of BankWorkflowWebService.accountNumberWorkflow)
            Get
                Return getBankAccountListBySSNumberResponseValue
            End Get
            Set(ByVal value As List(Of BankWorkflowWebService.accountNumberWorkflow))
                getBankAccountListBySSNumberResponseValue = value
            End Set
        End Property

        'Private memberBankAcctsValue As List(Of memberBankAcct)
        'Public Property memberBankAccts() As List(Of memberBankAcct)
        '    Get
        '        Return memberBankAcctsValue
        '    End Get
        '    Set(ByVal value As List(Of memberBankAcct))
        '        memberBankAcctsValue = value
        '    End Set
        'End Property

        Public Function getBankAccountListBySSNumber(ByVal ssNum As String) As Boolean
            Dim service As New BankWorkflowWebService.BankWorkflowWebServiceImplService
            service.Url = BankWorkflowWebService_URL
            Try
                getBankAccountListBySSNumberResponseValue = service.getBankAccountListBySSNumber(BankWorkflowWebService_Token, ssNum).ToList
                Return True
            Catch ex As Exception
                exceptionsValue = ex.Message
                Return False
            Finally
                service.Dispose()
            End Try
        End Function

    End Class

    Class memberBankAcct

        Private bankNameValue As String
        Public Property bankName() As String
            Get
                Return bankNameValue
            End Get
            Set(ByVal value As String)
                bankNameValue = value
            End Set
        End Property

        Private bankCodeValue As String
        Public Property bankCode() As String
            Get
                Return bankCodeValue
            End Get
            Set(ByVal value As String)
                bankCodeValue = value
            End Set
        End Property

        Private acctNumberValue As String
        Public Property acctNumber() As String
            Get
                Return acctNumberValue
            End Get
            Set(ByVal value As String)
                acctNumberValue = value
            End Set
        End Property

        Private acctTypeValue As String
        Public Property acctType() As String
            Get
                Return acctTypeValue
            End Get
            Set(ByVal value As String)
                acctTypeValue = value
            End Set
        End Property

        Private brstnValue As String
        Public Property brstn() As String
            Get
                Return brstnValue
            End Get
            Set(ByVal value As String)
                brstnValue = value
            End Set
        End Property

        Private bankDepBankValue As String
        Public Property bankDepBank() As String
            Get
                Return bankDepBankValue
            End Get
            Set(ByVal value As String)
                bankDepBankValue = value
            End Set
        End Property

    End Class


End Class
