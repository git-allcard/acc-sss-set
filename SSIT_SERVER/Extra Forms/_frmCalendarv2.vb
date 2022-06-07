Public Class _frmCalendarv2

    Private monthStart As Date
    Private daysInMonth As Short
    Private curDate As Date
    Public selectedDate As Date = Nothing

    Private validDateStart As Date = Nothing
    Private validDateEnd As Date = Nothing


    Public Sub New(ByVal selectedDate As Date, Optional validDateStart As Date = Nothing, Optional validDateEnd As Date = Nothing)

        ' This call is required by the designer.
        InitializeComponent()
        Me.selectedDate = selectedDate
        Me.validDateStart = validDateStart
        Me.validDateEnd = validDateEnd

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub _frmCalendarv2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DoubleBuffered = True
        AddButtonEvent()
        If selectedDate = Nothing Then
            curDate = Now.ToString("MM/dd/yyyy") 'CDate(String.Format("{0}/01/{1}", Now.Month, Now.Year))
        Else
            curDate = selectedDate.Date
        End If
        lblDate.Text = curDate.ToString("MM/dd/yyyy")
        BindCalendar()
        HighlightSelected(curDate.Day)
    End Sub

    Private Function GetButtonValue(ByRef val As Short, ByRef btn As Button, Optional dayDesc As String = "") As String
        Dim origValue As Short = val
        Dim dayBtn As New dayBtn
        dayBtn.btnId = origValue
        dayBtn.btnName = btn.Name
        dayBtn.btnBackColor = Color.Transparent

        Try
            Select Case val
                Case 1
                    If dayDesc = monthStart.ToString("ddd") Then
                        btn.Enabled = True
                        val += 1
                        dayBtn.dayNumber = origValue
                        dayBtn.btnDate = CDate(String.Format("{0}/{1}/{2}", curDate.Month, origValue, curDate.Year))
                        Return origValue
                    Else
                        btn.Enabled = False
                        dayBtn.dayNumber = ""
                        dayBtn.btnDate = Nothing
                        Return ""
                    End If
                Case Else
                    If val > daysInMonth Then
                        btn.Enabled = False
                        dayBtn.dayNumber = ""
                        dayBtn.btnDate = Nothing
                        Return ""
                    Else
                        btn.Enabled = True
                        dayBtn.dayNumber = origValue
                        dayBtn.btnDate = CDate(String.Format("{0}/{1}/{2}", curDate.Month, origValue, curDate.Year))
                        val += 1
                        Return origValue
                    End If
            End Select
        Catch ex As Exception
        Finally
            dayBtns.Add(dayBtn)
        End Try
    End Function

    Private Sub HighlightSelected(ByVal val As String)
        Dim selectedBtn = dayBtns.Find(Function(d) d.dayNumber = val)
        Me.TableLayoutPanel1.Controls.Item(selectedBtn.btnName).BackColor = Color.PapayaWhip

        Dim notSelectedBtn = dayBtns.FindAll(Function(d) d.dayNumber <> val)

        For Each dayBtn In notSelectedBtn
            'If Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).BackColor <> Color.Transparent Then
            Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).BackColor = dayBtn.btnBackColor
            'End If
        Next
    End Sub

    Private Sub PopulateDays()
        dayBtns.Clear()
        Dim val As String = 1

        btn1.Text = GetButtonValue(val, btn1, "Sun")
        btn2.Text = GetButtonValue(val, btn2, "Mon")
        btn3.Text = GetButtonValue(val, btn3, "Tue")
        btn4.Text = GetButtonValue(val, btn4, "Wed")
        btn5.Text = GetButtonValue(val, btn5, "Thu")
        btn6.Text = GetButtonValue(val, btn6, "Fri")
        btn7.Text = GetButtonValue(val, btn7, "Sat")

        For i As Short = 8 To 42
            Dim btn As Button = Me.TableLayoutPanel1.Controls.Item("btn" & i.ToString)
            btn.Text = GetButtonValue(val, btn)
            'Me.TableLayoutPanel1.Controls.Item("btn" & i.ToString)
        Next

        Console.WriteLine("test")
    End Sub

    Private Sub DisableDays()
        If validDateStart = Nothing Then Return

        For Each dayBtn In dayBtns
            If Not IsValidDate(dayBtn.btnDate) Then
                Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).BackColor = Color.Gainsboro
                Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).Enabled = False
            Else
                Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).BackColor = Color.Transparent
                Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).Enabled = True
            End If
            dayBtns.Find(Function(d) d.btnName = dayBtn.btnName).btnBackColor = Me.TableLayoutPanel1.Controls.Item(dayBtn.btnName).BackColor
        Next


    End Sub

    Private Sub AddButtonEvent()
        For i As Short = 1 To 42
            Dim btn As Button = Me.TableLayoutPanel1.Controls.Item("btn" & i.ToString)
            AddHandler btn.Click, AddressOf btnDays_Click
        Next
    End Sub

    Private dayBtns As New List(Of dayBtn)

    Class dayBtn

        Private dayNumberValue As String
        Public Property dayNumber() As String
            Get
                Return dayNumberValue
            End Get
            Set(ByVal value As String)
                dayNumberValue = value
            End Set
        End Property

        Private btnIdValue As Short
        Public Property btnId() As Short
            Get
                Return btnIdValue
            End Get
            Set(ByVal value As Short)
                btnIdValue = value
            End Set
        End Property

        Private btnNameValue As String
        Public Property btnName() As String
            Get
                Return btnNameValue
            End Get
            Set(ByVal value As String)
                btnNameValue = value
            End Set
        End Property

        Private btnDateValue As Date
        Public Property btnDate() As Date
            Get
                Return btnDateValue
            End Get
            Set(ByVal value As Date)
                btnDateValue = value
            End Set
        End Property

        Private btnBackColorValue As Color
        Public Property btnBackColor() As Color
            Get
                Return btnBackColorValue
            End Get
            Set(ByVal value As Color)
                btnBackColorValue = value
            End Set
        End Property

    End Class

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        BindCalendar()
    End Sub

    Private Sub BindCalendar()
        monthStart = CDate(String.Format("{0}/01/{1}", curDate.Month, curDate.Year))

        daysInMonth = System.DateTime.DaysInMonth(monthStart.Year, monthStart.Month)

        lblMonth.Text = curDate.ToString("MMMM")
        lblYear.Text = curDate.Year

        PopulateDays()
        DisableDays()
    End Sub

    Private Sub btnMonthPrev_Click(sender As Object, e As EventArgs) Handles btnMonthPrev.Click
        Dim curMonth As Short = curDate.Month
        If curMonth <> 1 Then
            'Dim _curDay As Integer = curDate.Day
            'Dim _curMonth As Integer = curMonth - 1
            'Dim tempDateStr As String = String.Format("{0}/{1}/{2}", _curMonth, _curDay, Now.Year)
            'Do While Not IsDate(tempDateStr)
            '    _curDay -= 1
            '    tempDateStr = String.Format("{0}/{1}/{2}", _curMonth, _curDay, Now.Year)
            'Loop

            ''If Not IsDate(tempDateStr) Then tempDateStr = String.Format("{0}/{1}/{2}", curMonth - 1, curDate.Day - 1, Now.Year)

            Dim _curDay As Integer = curDate.Day
            Dim _curMonth As Integer = curMonth - 1
            Dim tempDateStr As String = String.Format("{0}/{1}/{2}", _curMonth, _curDay, CInt(lblYear.Text))
            Do While Not IsDate(tempDateStr)
                _curDay -= 1
                tempDateStr = String.Format("{0}/{1}/{2}", _curMonth, _curDay, CInt(lblYear.Text))
            Loop

            'If Not IsDate(tempDateStr) Then tempDateStr = String.Format("{0}/{1}/{2}", curMonth - 1, curDate.Day - 1, Now.Year)

            Dim tempDate As Date = CDate(tempDateStr)
            If IsValidDate(tempDate) Then
                curDate = tempDate
                BindCalendar()
                HighlightSelected(_curDay)
            End If
        End If
    End Sub

    Private Sub btnMonthNext_Click(sender As Object, e As EventArgs) Handles btnMonthNext.Click
        Dim curMonth As Short = curDate.Month
        If curMonth <> 12 Then
            'Dim _curDay As Integer = curDate.Day
            'Dim _curMonth As Integer = curMonth + 1
            'Dim tempDateStr As String = String.Format("{0}/{1}/{2}", _curMonth, _curDay, Now.Year)
            'Do While Not IsDate(tempDateStr)
            '    _curDay -= 1
            '    tempDateStr = String.Format("{0}/{1}/{2}", _curMonth, _curDay, Now.Year)
            'Loop
            ''If Not IsDate(tempDateStr) Then tempDateStr = String.Format("{0}/{1}/{2}", curMonth + 1, curDate.Day - 1, Now.Year)

            Dim _curDay As Integer = curDate.Day
            Dim _curMonth As Integer = curMonth + 1
            Dim tempDateStr As String = String.Format("{0}/{1}/{2}", _curMonth, _curDay, CInt(lblYear.Text))
            Do While Not IsDate(tempDateStr)
                _curDay -= 1
                tempDateStr = String.Format("{0}/{1}/{2}", _curMonth, _curDay, CInt(lblYear.Text))
            Loop
            'If Not IsDate(tempDateStr) Then tempDateStr = String.Format("{0}/{1}/{2}", curMonth + 1, curDate.Day - 1, Now.Year)

            Dim tempDate As Date = CDate(tempDateStr)
            If IsValidDate(tempDate) Then
                curDate = tempDate
                BindCalendar()
                HighlightSelected(_curDay)
            End If
        End If
    End Sub

    Private Sub btnYearPrev_Click(sender As Object, e As EventArgs) Handles btnYearPrev.Click
        Dim tempDate As Date = CDate(String.Format("{0}/{1}/{2}", curDate.Month, curDate.Day, curDate.Year - 1))
        If IsValidYear(tempDate, tempDate, False) Then
            curDate = tempDate
            BindCalendar()
        End If
    End Sub

    Private Sub btnYearNext_Click(sender As Object, e As EventArgs) Handles btnYearNext.Click
        Dim tempDate As Date = CDate(String.Format("{0}/{1}/{2}", curDate.Month, curDate.Day, curDate.Year + 1))
        If IsValidYear(tempDate, tempDate) Then
            curDate = tempDate
            BindCalendar()
        End If
    End Sub

    Private Function IsValidDate(ByVal value As Date) As Boolean
        Dim bln As Boolean = False
        If validDateStart = Nothing Then Return True
        If value.Date >= validDateStart.Date And value.Date <= validDateEnd.Date Then
            bln = True
        Else
            bln = False
        End If

        Return bln
    End Function

    Private Function IsValidYear(ByVal value As Date, ByRef tempDate As Date, Optional isAdd As Boolean = True) As Boolean
        Dim bln As Boolean = False
        If validDateStart = Nothing Then Return True
        If value.Date.Year >= validDateStart.Date.Year And value.Date.Year <= validDateEnd.Date.Year Then
            If Not IsValidDate(value) Then
                If Not isAdd Then
                    tempDate = CDate(String.Format("{0}/{1}/{2}", validDateStart.Month, curDate.Day, validDateStart.Date.Year))
                Else
                    tempDate = CDate(String.Format("{0}/{1}/{2}", validDateEnd.Month, curDate.Day, validDateEnd.Date.Year))
                End If
            End If

            bln = True
        Else
            bln = False
        End If

        Return bln
    End Function

    Private Sub btnReset_Click(sender As Object, e As EventArgs) Handles btnReset.Click
        curDate = CDate(String.Format("{0}/01/{1}", Now.Month, Now.Year))
        BindCalendar()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        selectedDate = Nothing
        lblDate.Text = ""
        Close()
    End Sub

    Private Sub btnSelect_Click(sender As Object, e As EventArgs) Handles btnSelect.Click
        Close()
    End Sub

    Private Sub btnDays_Click(sender As Object, e As EventArgs)
        selectedDate = CDate(String.Format("{0}/{1}/{2}", curDate.Month, CType(sender, Button).Text, curDate.Year))
        lblDate.Text = selectedDate.ToString("MM/dd/yyyy")
        HighlightSelected(CType(sender, Button).Text)
    End Sub

    Private Function GetMonthInNumeric() As Short
        Dim monthNumber = DateTime.ParseExact(lblMonth.Text, "MMMM", Globalization.CultureInfo.CurrentCulture).Month
        Return monthNumber
    End Function

End Class