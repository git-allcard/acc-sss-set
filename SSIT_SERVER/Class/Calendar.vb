Public Class Calendar

    Private dayValue As String = ""
    Public Property Day() As String
        Get
            Return dayValue
        End Get
        Set(ByVal value As String)
            dayValue = value
        End Set
    End Property

    Private monthValue As String = ""
    Public Property Month() As String
        Get
            Return monthValue
        End Get
        Set(ByVal value As String)
            monthValue = value
        End Set
    End Property

    Private yearValue As String = ""
    Public Property Year() As String
        Get
            Return yearValue
        End Get
        Set(ByVal value As String)
            yearValue = value
        End Set
    End Property

    Private dayInput As String = ""
    Private monthInput As String = ""
    Private yearInput As String = ""

    Private validDateStart As Date = Nothing
    Private validDateEnd As Date = Nothing

    Public Sub New(ByVal dayInput As String, ByVal monthInput As String, ByVal yearInput As String, Optional validDateStart As Date = Nothing, Optional validDateEnd As Date = Nothing)
        Me.dayInput = dayInput
        Me.monthInput = monthInput
        Me.yearInput = yearInput
        Me.validDateStart = validDateStart
        Me.validDateEnd = validDateEnd
    End Sub

    Public Sub ShowCalendar()
        Dim selectedDate As Date = Nothing
        Try
            selectedDate = CDate(String.Format("{0}/{1}/{2}", dayInput, monthInput, yearInput))
        Catch ex As Exception
            selectedDate = Now.ToString("MM/dd/yyyy")
        End Try
        Dim calv2 As New _frmCalendarv2(selectedDate, validDateStart.Date, validDateEnd.Date)
        calv2.ShowDialog()
        If calv2.selectedDate <> Nothing Then
            dayValue = calv2.selectedDate.Day.ToString.PadLeft(2, "0")
            monthValue = calv2.selectedDate.Month.ToString.PadLeft(2, "0")
            yearValue = calv2.selectedDate.Year
        End If
    End Sub

End Class
