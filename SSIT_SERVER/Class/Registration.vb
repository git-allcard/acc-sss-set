
Public Class Registration
#Region "For Members"


    Public memberdetails(18) As String

    Public Function getSSSNumber()
        Dim ssNum As String
        ssNum = memberdetails(1)
        Return ssNum
    End Function
    Public Function getfirstname()
        Dim fname As String
        fname = memberdetails(2)
        Return fname
    End Function

    Public Function getMiddleName()
        Dim MName As String
        MName = memberdetails(3)
        Return MName
    End Function
    Public Function getLastName()
        Dim lastName As String
        lastName = memberdetails(4)
        Return lastName
    End Function
    Public Function getBday()
        Dim Bday As String
        Bday = memberdetails(5)
        Return Bday
    End Function
    Public Function getMothersMaidenName()
        Dim mothersMName As String
        mothersMName = memberdetails(6)
        Return mothersMName

    End Function
    Public Function getAddress1()
        Dim address1 As String
        address1 = memberdetails(7)
        Return address1
    End Function
    Public Function getAddress2()
        Dim address2 As String
        address2 = memberdetails(8)
        Return (address2)
    End Function
    Public Function getCity()
        Dim City As String
        City = memberdetails(9)
        Return City
    End Function
    Public Function getPostalCode()
        Dim postalCode As String
        postalCode = memberdetails(10)
        Return postalCode

    End Function
    Public Function getLandline()
        Dim Landline As String
        Landline = memberdetails(11)
        Return Landline
    End Function
    Public Function getMobile()
        Dim Mobile As String
        Mobile = memberdetails(12)
        Return Mobile
    End Function
    Public Function getEmail()
        Dim email As String
        email = memberdetails(13)
        Return email
    End Function
    Public Function getConfirmEmail()
        Dim cEmail As String
        cEmail = memberdetails(14)
        Return cEmail
    End Function
    Public Function getUserID()
        Dim userID As String
        userID = memberdetails(15)
        Return userID

    End Function

    Public Function getPassword()
        Dim Pass As String
        Pass = memberdetails(16)
        Return Pass
    End Function
    Public Function getConfirmPass()
        Dim cPass As String
        cPass = memberdetails(17)
        Return cPass
    End Function
    Public Function getMStatus()
        Dim mStatus As String
        mStatus = memberdetails(18)
        Return mStatus
    End Function
#End Region
#Region "For Pensioners"
    Public Pensionerdetails(18) As String

    Public Function getPensionersSSSNumber()
        Dim ssNUm As String
        ssNum = Pensionerdetails(1)
        Return ssNum
    End Function
    Public Function getPensionersfirstname()
        Dim fname As String
        fname = Pensionerdetails(2)
        Return fname
    End Function

    Public Function getPensionersMiddleName()
        Dim MName As String
        MName = Pensionerdetails(3)
        Return MName
    End Function
    Public Function getPensionersLastName()
        Dim lastName As String
        lastName = Pensionerdetails(4)
        Return lastName
    End Function
    Public Function getPensionersBday()
        Dim Bday As String
        Bday = Pensionerdetails(5)
        Return Bday
    End Function
    Public Function getPensionersMothersMaidenName()
        Dim mothersMName As String
        mothersMName = Pensionerdetails(6)
        Return mothersMName

    End Function
    Public Function getPensionersAddress1()
        Dim address1 As String
        address1 = Pensionerdetails(7)
        Return address1
    End Function
    Public Function getPensionersAddress2()
        Dim address2 As String
        address2 = Pensionerdetails(8)
        Return (address2)
    End Function
    Public Function getPensionersCity()
        Dim City As String
        City = Pensionerdetails(9)
        Return City
    End Function
    Public Function getPensionersPostalCode()
        Dim postalCode As String
        postalCode = Pensionerdetails(10)
        Return postalCode

    End Function
    Public Function getPensionersLandline()
        Dim Landline As String
        Landline = Pensionerdetails(11)
        Return Landline
    End Function
    Public Function getPensionersMobile()
        Dim Mobile As String
        Mobile = Pensionerdetails(12)
        Return Mobile
    End Function
    Public Function getPensionersEmail()
        Dim email As String
        email = Pensionerdetails(13)
        Return email
    End Function
    Public Function getPensionersConfirmEmail()
        Dim cEmail As String
        cEmail = Pensionerdetails(14)
        Return cEmail
    End Function
    Public Function getPensionersUserID()
        Dim userID As String
        userID = Pensionerdetails(15)
        Return userID

    End Function

    Public Function getPensionersPassword()
        Dim Pass As String
        Pass = Pensionerdetails(16)
        Return Pass
    End Function
    Public Function getPensionersConfirmPass()
        Dim cPass As String
        cPass = Pensionerdetails(17)
        Return cPass
    End Function
    Public Function getPensionersMStatus()
        Dim mStatus As String
        mStatus = Pensionerdetails(18)
        Return mStatus
    End Function
#End Region
   
End Class
