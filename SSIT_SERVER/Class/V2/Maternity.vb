Public Class Maternity

    Class EnhanceNotification

        Const pSource = "SSIT"

        Private sssNoValue As String = ""
        Public Property sssNo() As String
            Get
                Return sssNoValue
            End Get
            Set(ByVal value As String)
                sssNoValue = value
            End Set
        End Property

        Private exceptionsValue As String = ""
        Public Property exceptions() As String
            Get
                Return exceptionsValue
            End Get
            Set(ByVal value As String)
                exceptionsValue = value
            End Set
        End Property

        Private deliveryDateValue As Date
        Public Property deliveryDate() As Date
            Get
                Return deliveryDateValue
            End Get
            Set(ByVal value As Date)
                deliveryDateValue = value
            End Set
        End Property

        Private firstNameValue As String = ""
        Public Property firstName() As String
            Get
                Return firstNameValue
            End Get
            Set(ByVal value As String)
                firstNameValue = value
            End Set
        End Property

        Private middleNameValue As String = ""
        Public Property middleName() As String
            Get
                Return middleNameValue
            End Get
            Set(ByVal value As String)
                middleNameValue = value
            End Set
        End Property

        Private lastNameValue As String = ""
        Public Property lastName() As String
            Get
                Return lastNameValue
            End Get
            Set(ByVal value As String)
                lastNameValue = value
            End Set
        End Property

        Private suffixValue As String = ""
        Public Property suffix() As String
            Get
                Return suffixValue
            End Get
            Set(ByVal value As String)
                suffixValue = value
            End Set
        End Property

        Private relationshipValue As String = ""
        Public Property relationship() As String
            Get
                Return relationshipValue
            End Get
            Set(ByVal value As String)
                relationshipValue = value
            End Set
        End Property

        Private isAllocateCreditValue As Boolean = False
        Public Property isAllocateCredit() As Boolean
            Get
                Return isAllocateCreditValue
            End Get
            Set(ByVal value As Boolean)
                isAllocateCreditValue = value
            End Set
        End Property

        Private noOfDaysValue As Short = 0
        Public Property noOfDays() As Short
            Get
                Return noOfDaysValue
            End Get
            Set(ByVal value As Short)
                noOfDaysValue = value
            End Set
        End Property

        Public Function IsValid() As Boolean
            Dim sb As New System.Text.StringBuilder
            If Not IsDate(deliveryDate) Then sb.AppendLine("Expected date of delivery is invalid")
            If deliveryDate.Date <= Date.Now.Date Then sb.AppendLine("Expected date of delivery should be later than current date")
            If DateDiff(DateInterval.Day, Date.Now.Date, deliveryDate) > 270 Then sb.AppendLine("Expected date of delivery should be any date within 9 months from now")
            If isAllocateCredit And firstName = "" Then sb.AppendLine("Given Name for allocation must not be empty")
            If isAllocateCredit And lastName = "" Then sb.AppendLine("Last Name for allocation must not be empty")
            If isAllocateCredit And relationship = "-Select Relationship-" Then
                sb.AppendLine("Please choose a relation to allocation")
            ElseIf isAllocateCredit And relationship = "" Then
                sb.AppendLine("Please choose a relation to allocation")
            End If
            If isAllocateCredit And noOfDays = 0 Then sb.AppendLine("Number of allocated leave should not be empty")

            If sb.ToString = "" Then
                Return True
            Else
                exceptionsValue = sb.ToString.ToUpper
                Return False
            End If
        End Function

        Public Function isMatNotifEligible() As MobileWS2BeanService.webResponse
            'Dim service As New MobileWS2BeanService.MobileWS2BeanService
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            Dim local As New MobileWS2BeanService.webResponse
            service.Url = MobileWS2BeanService_URL
            Try
                Return service.isMatNotifEligible(MobileWS2BeanService_Token, sssNoValue, pSource)
            Catch ex As Exception
                local.processFlag = "1"
                local.returnMessage = "Exception " & ex.Message
                Return local
            Finally
                local = Nothing
                service.Dispose()
                service = Nothing
            End Try
        End Function

        Public Function submitMatNotif() As MobileWS2BeanService.webTransactionResponse
            'Dim service As New MobileWS2BeanService.MobileWS2BeanService
            Dim service As New MobileWS2BeanService.MobileWSBeanService
            service.Url = MobileWS2BeanService_URL
            Try
                Dim allocation As String = "N"
                If isAllocateCredit Then allocation = IIf(relationship.Contains("Father"), "F", "C")
                Return service.submitMatNotif(MobileWS2BeanService_Token, sssNoValue, pSource, "", deliveryDate.ToString("MM-dd-yyyy"), allocation, lastName, firstName, middleName, suffix, "ACC", noOfDays)
            Catch ex As Exception
                Console.WriteLine(ex.Message)
            Finally
                service.Dispose()
                service = Nothing
            End Try
        End Function
    End Class




End Class
