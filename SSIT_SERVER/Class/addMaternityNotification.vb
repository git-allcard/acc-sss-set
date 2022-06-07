Imports Oracle.DataAccess.Client
Public Class addMaternityNotification

    Inherits ConnectionString2

    Private mIN_SSNBR As String
    Public Property IN_SSNBR() As String
        Get
            Return mIN_SSNBR
        End Get
        Set(ByVal value As String)
            mIN_SSNBR = value
        End Set
    End Property

    Private mIN_BRSNM As String
    Public Property IN_BRSNM() As String
        Get
            Return mIN_BRSNM
        End Get
        Set(ByVal value As String)
            mIN_BRSNM = value
        End Set
    End Property

    Private mIN_BRFNM As String
    Public Property IN_BRFNM() As String
        Get
            Return mIN_BRFNM
        End Get
        Set(ByVal value As String)
            mIN_BRFNM = value
        End Set
    End Property

    Private mIN_BRMID As String
    Public Property IN_BRMID() As String
        Get
            Return mIN_BRMID
        End Get
        Set(ByVal value As String)
            mIN_BRMID = value
        End Set
    End Property

    Private mIN_HOME1 As String
    Public Property IN_HOME1() As String
        Get
            Return mIN_HOME1
        End Get
        Set(ByVal value As String)
            mIN_HOME1 = value
        End Set
    End Property

    Private mIN_HOME2 As String
    Public Property IN_HOME2() As String
        Get
            Return mIN_HOME2
        End Get
        Set(ByVal value As String)
            mIN_HOME2 = value
        End Set
    End Property

    Private mIN_POSCD As String
    Public Property IN_POSCD() As String
        Get
            Return mIN_POSCD
        End Get
        Set(ByVal value As String)
            mIN_POSCD = value
        End Set
    End Property

    Private mIN_ERID As String
    Public Property IN_ERID() As String
        Get
            Return mIN_ERID
        End Get
        Set(ByVal value As String)
            mIN_ERID = value
        End Set
    End Property

    Private mIN_LNTYP As String
    Public Property IN_LNTYP() As String
        Get
            Return mIN_LNTYP
        End Get
        Set(ByVal value As String)
            mIN_LNTYP = value
        End Set
    End Property

    Private mIN_ENCDR As String
    Public Property IN_ENCDR() As String
        Get
            Return mIN_ENCDR
        End Get
        Set(ByVal value As String)
            mIN_ENCDR = value
        End Set
    End Property

    Private mIN_ERBRN As String
    Public Property IN_ERBRN() As String
        Get
            Return mIN_ERBRN
        End Get
        Set(ByVal value As String)
            mIN_ERBRN = value
        End Set
    End Property

    Private mIN_TUITN As Integer
    Public Property IN_TUITN() As Integer
        Get
            Return mIN_TUITN
        End Get
        Set(ByVal value As Integer)
            mIN_TUITN = value
        End Set
    End Property

    Private mIN_TCONT As Integer
    Public Property IN_TCONT() As Integer
        Get
            Return mIN_TCONT
        End Get
        Set(ByVal value As Integer)
            mIN_TCONT = value
        End Set
    End Property

    Private mIN_HIMSC As Integer
    Public Property IN_HIMSC() As Integer
        Get
            Return mIN_HIMSC
        End Get
        Set(ByVal value As Integer)
            mIN_HIMSC = value
        End Set
    End Property

    Private mIN_LNBAL As Integer
    Public Property IN_LNBAL() As Integer
        Get
            Return mIN_LNBAL
        End Get
        Set(ByVal value As Integer)
            mIN_LNBAL = value
        End Set
    End Property

    Private mIN_SRFEE As Integer
    Public Property IN_SRFEE() As Integer
        Get
            Return mIN_SRFEE
        End Get
        Set(ByVal value As Integer)
            mIN_SRFEE = value
        End Set
    End Property

    Private mIN_SCFEE As Integer
    Public Property IN_SCFEE() As Integer
        Get
            Return mIN_SCFEE
        End Get
        Set(ByVal value As Integer)
            mIN_SCFEE = value
        End Set
    End Property

    Private mIN_AMONT As Integer
    Public Property IN_AMONT() As Integer
        Get
            Return mIN_AMONT
        End Get
        Set(ByVal value As Integer)
            mIN_AMONT = value
        End Set
    End Property

    Private mIN_CITNM As String
    Public Property IN_CITNM() As String
        Get
            Return mIN_CITNM
        End Get
        Set(ByVal value As String)
            mIN_CITNM = value
        End Set
    End Property

    Private mIN_LADTE As String
    Public Property IN_LADTE() As String
        Get
            Return mIN_LADTE
        End Get
        Set(ByVal value As String)
            mIN_LADTE = value
        End Set
    End Property

    Private mIN_STYPE As String
    Public Property IN_STYPE() As String
        Get
            Return mIN_STYPE
        End Get
        Set(ByVal value As String)
            mIN_STYPE = value
        End Set
    End Property

    Private mIN_IPADD As String
    Public Property IN_IPADD() As String
        Get
            Return mIN_IPADD
        End Get
        Set(ByVal value As String)
            mIN_IPADD = value
        End Set
    End Property

    Public Sub DeleteIfUnsuccessful(ByVal Employeeno As String)
        Execute("Delete from tbl_Employeeinformation_Hdr where fld_Employeeno = '" & Employeeno & "'", CommandType.Text)
    End Sub

End Class
