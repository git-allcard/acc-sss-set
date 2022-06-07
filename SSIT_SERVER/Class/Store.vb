Public Class Store
    Public review(20) As String
    Public num(10) As Integer
    Public Function GetName(ByVal name As String)
        review(0) = name

        Return name
    End Function

    Public Function GetEmail(ByVal email As String)

        review(1) = email

        Return email
    End Function
    Public Function GetHBaddress(ByVal hbaddress As String)
        review(2) = hbaddress
        Return hbaddress
    End Function
    Public Function GetStreet(ByVal street As String)
        review(3) =
        street

        Return street
    End Function

    Public Function GetCity(ByVal city As String)

        review(4) = city
        Return city
    End Function
    Public Function GetZipCode(ByVal zipcode As String)

        review(5) = zipcode
        Return zipcode

    End Function

    Public Function GetCountry(ByVal country As String)
        review(6) = country
        Return country
    End Function
    Public Function GetNum1(ByVal num1 As Double)

        num(0) = num1
        Return num1
    End Function
    Public Function GetNum2(ByVal num2 As Double)

        num(1) = num2
        Return num2
    End Function
    Public Function GetNum3(ByVal num3 As Double)

        num(2) = num3
        Return num3
    End Function
    Public Function GetNum4(ByVal num4 As Double)

        num(3) = num4
        Return num4
    End Function
    Public Function GetNum5(ByVal num5 As Double)

        num(4) = num5
        Return num5
    End Function
    Public Function GetNum6(ByVal num6 As Double)
        num(5) = num6
        Return num6
    End Function
    Public Function GetNum7(ByVal num7 As Double)

        num(6) = num7
        Return num7
    End Function
    Public Function GetAnswer(ByVal ynu As String)

        review(14) = ynu
        Return ynu
    End Function
    Public Function GetWhy(ByVal why As String)

        review(15) = why
        Return why
    End Function

    Public Function GetWhat(ByVal what As String)

        review(16) = what
        Return what
    End Function
    Public Function GetIf(ByVal iff As String)
        review(17) = iff
        Return iff
    End Function

End Class
