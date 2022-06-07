Public Class TestData2

    Private cardDataValue As List(Of td)
    Public Property cardData() As List(Of td)
        Get
            Return cardDataValue
        End Get
        Set(ByVal value As List(Of td))
            cardDataValue = value
        End Set
    End Property

    Private moduleListValue As List(Of sssModule)
    Public Property ModuleList() As List(Of sssModule)
        Get
            Return moduleListValue
        End Get
        Set(ByVal value As List(Of sssModule))
            moduleListValue = value
        End Set
    End Property

    Public ReadOnly Property ModuleData(ByVal pModule As String) As List(Of td)
        Get
            Return cardDataValue.FindAll(Function(c) c.pModule.Contains(pModule))
        End Get
    End Property

    Class td

        Private crnValue As String
        Public Property crn() As String
            Get
                Return crnValue
            End Get
            Set(ByVal value As String)
                crnValue = value
            End Set
        End Property

        Private sssValue As String
        Public Property sss() As String
            Get
                Return sssValue
            End Get
            Set(ByVal value As String)
                sssValue = value
            End Set
        End Property

        Private ccdtValue As String
        Public Property ccdt() As String
            Get
                Return ccdtValue
            End Get
            Set(ByVal value As String)
                ccdtValue = value
            End Set
        End Property

        Private pModuleValue As String
        Public Property pModule() As String
            Get
                Return pModuleValue
            End Get
            Set(ByVal value As String)
                pModuleValue = value
            End Set
        End Property

        Private pSubModuleValue As String
        Public Property pSubModule() As String
            Get
                Return pSubModuleValue
            End Get
            Set(ByVal value As String)
                pSubModuleValue = value
            End Set
        End Property

    End Class

    Class sssModule

        Private pModuleValue As String
        Public Property pModule() As String
            Get
                Return pModuleValue
            End Get
            Set(ByVal value As String)
                pModuleValue = value
            End Set
        End Property

    End Class

    Public Sub New()
        PopulateTestData()
    End Sub

    Private Sub PopulateTestData()
        cardDataValue = New List(Of td)
        Using sr As New IO.StreamReader(Application.StartupPath & "\test_data2.txt")
            Do While Not sr.EndOfStream
                Dim line As String = sr.ReadLine
                If line.Trim <> "" Then
                    Dim td As New td
                    td.crn = line.Split("|")(0)
                    td.sss = line.Split("|")(1)
                    td.ccdt = line.Split("|")(2)
                    td.pModule = line.Split("|")(3)
                    td.pSubModule = line.Split("|")(4)
                    cardDataValue.Add(td)
                End If
            Loop

            sr.Close()
            sr.Dispose()
        End Using

        Dim modGroup = cardDataValue.GroupBy(Function(x) x.pModule).Select(Function(x) x.First).ToList

        moduleListValue = New List(Of sssModule)
        For Each mg In modGroup
            Dim sssMod As sssModule
            If mg.pModule.Contains(",") Then
                For Each s As String In mg.pModule.Split(",")
                    sssMod = New sssModule
                    If moduleListValue.Find(Function(c) c.pModule.Contains(s)) Is Nothing Then
                        sssMod.pModule = s
                        moduleListValue.Add(sssMod)
                    End If
                Next
            Else
                If moduleListValue.Find(Function(c) c.pModule.Contains(mg.pModule)) Is Nothing Then
                    sssMod = New sssModule
                    sssMod.pModule = mg.pModule
                    moduleListValue.Add(sssMod)
                End If
            End If
        Next


    End Sub

End Class
