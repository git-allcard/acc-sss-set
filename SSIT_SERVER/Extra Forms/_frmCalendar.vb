Public Class _frmCalendar
    Dim int As Integer
    Dim cnt As Integer
    Dim cnt1 As Integer
    Dim Month1 As Integer
    Public month3 As String
    Public FORMCHECK As String
    Public Sub checkformMonth()
        Select Case FORMCHECK

            'Case "Maternity Notification"
            '    If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
            '        lblmonth2.Text = lblMonth.Text
            '        _frmMaternityNotification.txtdoby.Text = lblYear.Text
            '        _frmMaternityNotification.txtdobm.Text = month3
            '        _frmMaternityNotification.txtdobd.Text = lblday.Text
            '    ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
            '        lblmonth2.Text = lblMonth.Text
            '        _frmMaternityNotification.txtLDY.Text = lblYear.Text
            '        _frmMaternityNotification.txtLDM.Text = month3
            '        _frmMaternityNotification.txtLDD.Text = lblday.Text
            '    End If
            Case "Enhanced Maternity Notification"
                If _frmEnhanceMaternityNotif.mouseFocus = "txtMonth" = True Or _frmEnhanceMaternityNotif.mouseFocus = "txtDay" = True Or _frmEnhanceMaternityNotif.mouseFocus = "txtYear" = True Then
                    lblmonth2.Text = lblMonth.Text
                    _frmEnhanceMaternityNotif.txtdoby.Text = lblYear.Text
                    _frmEnhanceMaternityNotif.txtdobm.Text = month3
                    _frmEnhanceMaternityNotif.txtdobd.Text = lblday.Text
                End If
            Case "Online Retirement Application"
                If _frmTechRetirementApplyDate.mouseFocus = "txtMonth" = True Or _frmTechRetirementApplyDate.mouseFocus = "txtDay" = True Or _frmTechRetirementApplyDate.mouseFocus = "txtYear" = True Then
                    lblmonth2.Text = lblMonth.Text
                    _frmTechRetirementApplyDate.txtdosy.Text = lblYear.Text
                    _frmTechRetirementApplyDate.txtdosm.Text = month3
                    _frmTechRetirementApplyDate.txtdosd.Text = lblday.Text
                End If
            Case "ACOP"
                'If _frmACOPdependent.mouseFocus = "txtdobm" = True Or _frmACOPdependent.mouseFocus = "txtdobd" = True Or _frmACOPdependent.mouseFocus = "txtdoby" = True Then
                '    lblmonth2.Text = lblMonth.Text
                '    _frmACOPdependent.txtdoby.Text = lblYear.Text
                '    _frmACOPdependent.txtdobm.Text = month3
                '    _frmACOPdependent.txtdobd.Text = lblday.Text
                'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" = True Or _frmACOPdependent.mouseFocus = "txtLDY" = True Or _frmACOPdependent.mouseFocus = "txtLDD" = True Then
                '    lblmonth2.Text = lblMonth.Text
                '    _frmACOPdependent.txtLDY.Text = lblYear.Text
                '    _frmACOPdependent.txtLDM.Text = month3
                '    _frmACOPdependent.txtLDD.Text = lblday.Text
                'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" = True Or _frmACOPdependent.mouseFocus = "txtDDY" = True Or _frmACOPdependent.mouseFocus = "txtDDD" = True Then
                '    lblmonth2.Text = lblMonth.Text
                '    _frmACOPdependent.txtDDY.Text = lblYear.Text
                '    _frmACOPdependent.txtDDM.Text = month3
                '    _frmACOPdependent.txtDDD.Text = lblday.Text
                'End If

        End Select
    End Sub
    Public Sub checkbutton()
        Select Case FORMCHECK

            Case "Maternity Notification"
            Case "Enhanced Maternity Notification"

            Case "ACOP"

        End Select
    End Sub
    Public Sub checkLeap()
        If lblMonth.Text = "February" Then
            If blnLeapYear(lblYear.Text) = True Then
                btn31.Visible = False
                btn30.Visible = False
                btn29.Visible = True
                If lblMonth.Text = "February" And lblday.Text >= 30 Then
                    lblday.Text = "29"
                End If
            Else
                lblday.Text = "28"
                btn31.Visible = False
                btn30.Visible = False
                btn29.Visible = False
            End If
        End If
    End Sub
    Public Sub MonthInt()
        Select Case Month1

            Case 1
                lblMonth.Text = "January"
                month3 = "01"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True
                Button31.Enabled = False

            Case 2
                lblMonth.Text = "February"
                checkLeap()
                month3 = "02"
                Button31.Enabled = True

            Case 3
                lblMonth.Text = "March"
                month3 = "03"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True

            Case 4
                lblMonth.Text = "April"
                month3 = "04"
                btn31.Visible = False
                btn30.Visible = True
                btn29.Visible = True

            Case 5
                lblMonth.Text = "May"
                month3 = "05"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True

            Case 6
                lblMonth.Text = "June"
                month3 = "06"
                btn31.Visible = False
                btn29.Visible = True

            Case 7
                lblMonth.Text = "July"
                month3 = "07"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True

            Case 8
                lblMonth.Text = "August"
                month3 = "08"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True
                lblmonth2.Text = lblMonth.Text
            Case 9
                lblMonth.Text = "September"
                month3 = "09"
                btn31.Visible = False
                btn30.Visible = True
                btn29.Visible = True

            Case 10
                lblMonth.Text = "October"
                month3 = "10"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True

            Case 11
                lblMonth.Text = "November"
                month3 = "11"
                btn31.Visible = False
                btn30.Visible = True
                btn29.Visible = True
                Button32.Enabled = True

            Case 12
                lblMonth.Text = "December"
                month3 = "12"
                btn31.Visible = True
                btn30.Visible = True
                btn29.Visible = True
                Button32.Enabled = False
        End Select

        lblmonth2.Text = lblMonth.Text
        checkformMonth()
    End Sub
    Public Function blnLeapYear(ByVal intYear As Integer) As Boolean
        blnLeapYear = IsDate("2/29/" & intYear)
    End Function
    Public Sub yearInt()

        Select Case cnt

            Case 0
                int -= 1
                lblYear.Text -= 1
                If int < -15 Then
                    lblYear.Text -= 2
                End If
                If int < -20 Then
                    lblYear.Text -= 5
                End If

            Case 1
                int += 1
                lblYear.Text += 1
                If int > 15 Then
                    lblYear.Text += 2
                End If
                If int > 20 Then
                    lblYear.Text += 5
                End If
        End Select
        lblyear2.Text = lblYear.Text
        checkLeap()
        checkformMonth()

    End Sub
    Private Sub Button22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button22.Click
        If cnt = 1 Then
            cnt = 0
            int = 0
            yearInt()
        Else
            cnt = 0
            yearInt()
        End If
    End Sub

    Private Sub Button23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button23.Click
        If cnt = 0 Then
            cnt = 1
            int = 0
            yearInt()
        Else
            cnt = 1
            yearInt()
        End If
    End Sub
    Private Sub _frmCalendar_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If e.Alt = True And e.KeyCode = Keys.F4 Or e.Control = True And e.KeyCode = Keys.Escape Then

            e.Handled = True
        End If
    End Sub

    Private Sub _frmCalendar_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        lblYear.Text = Date.Now.Year
        lblyear2.Text = Date.Now.Year
        If _frmWebBrowser.mouseFocus = "Confend" Then
            lblConfineStart.Text = "Confinement End"
        ElseIf _frmWebBrowser.mouseFocus = "Confst" Then
            lblConfineStart.Text = "Confinement Start"
        Else
            lblConfineStart.Text = "Delivery Date"
        End If
        'lblday.Text = "01"
        lblday.Text = IIf(Date.Now.Day < 10, "0" & Date.Now.Day.ToString, Date.Now.Day.ToString)
        'Month1 = 1
        Month1 = Date.Now.Month.ToString
        MonthInt()
        checkformMonth()
        Me.Size = New Drawing.Size(499, 517)

    End Sub

    Private Sub Button32_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button32.Click
        If Month1 = 12 Then
            MonthInt()
            Button32.Visible = False
        Else
            Month1 += 1
            MonthInt()
            Button32.Visible = True
        End If


    End Sub

    Private Sub Button31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button31.Click
        If Month1 = 0 Then
            MonthInt()
            Button31.Visible = False
        Else
            Month1 -= 1
            MonthInt()
            Button31.Visible = True
        End If

    End Sub

    'Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click
    '    lblday.Text = "01"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "01"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "01"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "01"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "01"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "01"
    '            'End If

    '    End Select
    'End Sub
    'Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click

    '    Select Case Button1.Text
    '        Case "6"
    '            Me.Size = New Drawing.Size(499, 94)
    '            Button1.Text = "5"
    '        Case "5"
    '            Me.Size = New Drawing.Size(499, 517)
    '            Button1.Text = "6"
    '    End Select
    'End Sub
    'Private Sub btn2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn2.Click
    '    lblday.Text = "02"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "02"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "02"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "02"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "02"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "02"
    '            'End If

    '    End Select
    'End Sub

    'Private Sub btn3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn3.Click
    '    lblday.Text = "03"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "03"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "03"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "03"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "03"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "03"
    '            'End If

    '    End Select
    'End Sub
    'Private Sub btn4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn4.Click
    '    lblday.Text = "04"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "04"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "04"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "04"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "04"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "04"
    '            'End If

    '    End Select
    'End Sub
    'Private Sub btn5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn5.Click
    '    lblday.Text = "05"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "05"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "05"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "05"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "05"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "05"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn6.Click
    '    lblday.Text = "06"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "06"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "06"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "06"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "06"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "06"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn7.Click
    '    lblday.Text = "07"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "07"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "07"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "07"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "07"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "07"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn8.Click
    '    lblday.Text = "08"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "08"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "08"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "08"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "08"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "08"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn9.Click
    '    lblday.Text = "09"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "09"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "09"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "09"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "09"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "09"
    '            'End If

    '    End Select
    'End Sub
    'Private Sub btn10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn10.Click
    '    lblday.Text = "10"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "10"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "10"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "10"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "10"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "10"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn11_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn11.Click
    '    lblday.Text = "11"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "11"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "11"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "11"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "11"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "11"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn13.Click
    '    lblday.Text = "13"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "13"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "13"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "13"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "13"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "13"
    '            '    'End If
    '    End Select
    'End Sub
    'Private Sub btn14_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn14.Click
    '    lblday.Text = "14"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "14"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "14"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "14"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "14"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "14"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn15_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn15.Click
    '    lblday.Text = "15"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "15"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "15"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "15"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "15"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "15"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn16_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn16.Click
    '    lblday.Text = "16"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "16"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "16"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "16"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "16"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "16"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn17_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn17.Click
    '    lblday.Text = "17"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "17"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "17"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "17"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "17"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "17"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn18_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn18.Click
    '    lblday.Text = "18"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "18"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "18"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "18"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "18"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "18"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn19_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn19.Click
    '    lblday.Text = "19"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "19"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "19"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "19"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "19"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "19"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn20_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn20.Click
    '    lblday.Text = "20"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "20"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "20"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "20"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "20"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "20"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn21_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn21.Click
    '    lblday.Text = "21"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "21"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "21"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "21"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "21"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "21"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn22_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn22.Click
    '    lblday.Text = "22"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "22"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "22"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "22"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "22"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "22"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn23_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn23.Click
    '    lblday.Text = "23"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "23"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "23"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "23"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "23"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "23"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn24_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn24.Click
    '    lblday.Text = "24"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "24"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "24"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "24"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "24"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "24"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn25_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn25.Click
    '    lblday.Text = "25"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "25"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "25"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "25"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "25"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "25"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn26_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn26.Click
    '    lblday.Text = "26"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "26"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "26"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "26"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "26"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "26"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn27_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn27.Click
    '    lblday.Text = "27"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "27"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "27"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "27"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "27"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "27"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn28_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn28.Click
    '    lblday.Text = "28"
    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "28"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "28"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "28"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "28"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "28"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn12_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn12.Click
    '    lblday.Text = "12"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "12"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "12"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "12"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "12"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "12"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn29_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn29.Click
    '    lblday.Text = "29"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "29"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "29"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "29"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "29"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "29"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn30.Click
    '    lblday.Text = "30"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "30"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "30"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "30"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "30"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "30"
    '            'End If
    '    End Select
    'End Sub
    'Private Sub btn31_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn31.Click
    '    lblday.Text = "31"

    '    Select Case FORMCHECK
    '        Case "Maternity Notification"
    '            If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtdoby.Text = lblYear.Text
    '                _frmMaternityNotification.txtdobm.Text = month3
    '                _frmMaternityNotification.txtdobd.Text = "31"
    '            ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
    '                lblmonth2.Text = lblMonth.Text
    '                _frmMaternityNotification.txtLDY.Text = lblYear.Text
    '                _frmMaternityNotification.txtLDM.Text = month3
    '                _frmMaternityNotification.txtLDD.Text = "31"
    '            End If
    '        Case "ACOP"
    '            'If _frmACOPdependent.mouseFocus = "txtdobm" Or _frmACOPdependent.mouseFocus = "txtdoby" Or _frmACOPdependent.mouseFocus = "txtdobd" Then
    '            '    _frmACOPdependent.txtdoby.Text = lblYear.Text
    '            '    _frmACOPdependent.txtdobm.Text = month3
    '            '    _frmACOPdependent.txtdobd.Text = "31"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtLDM" Or _frmACOPdependent.mouseFocus = "txtLDD" Or _frmACOPdependent.mouseFocus = "txtLDY" Then
    '            '    _frmACOPdependent.txtLDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtLDM.Text = month3
    '            '    _frmACOPdependent.txtLDD.Text = "31"
    '            'ElseIf _frmACOPdependent.mouseFocus = "txtDDM" Or _frmACOPdependent.mouseFocus = "txtDDD" Or _frmACOPdependent.mouseFocus = "txtDDY" Then
    '            '    _frmACOPdependent.txtDDY.Text = lblYear.Text
    '            '    _frmACOPdependent.txtDDM.Text = month3
    '            '    _frmACOPdependent.txtDDD.Text = "31"
    '            'End If
    '    End Select
    'End Sub
    Private Sub Button2_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
        _frmBlock.Close()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncalsub.Click
        Dim doc As HtmlDocument
        Dim elem As HtmlElement

        Select Case _frmWebBrowser.mouseFocus

            Case "Confend"
                Dim confen As String = month3 & "-" & lblday.Text & "-" & lblYear.Text
                doc = _frmWebBrowser.WebBrowser1.Document
                _frmWebBrowser.confend.Text = confen
                For Each elem In doc.All
                    If elem.GetAttribute("name") = "confen" Then
                        elem.SetAttribute("value", confen)
                    End If
                Next

            Case "deldt"
                Dim confen As String = month3 & "-" & lblday.Text & "-" & lblYear.Text
                doc = _frmWebBrowser.WebBrowser1.Document
                _frmWebBrowser.deldt.Text = confen
                For Each elem In doc.All
                    If elem.GetAttribute("name") = "deldt" Then
                        elem.SetAttribute("value", confen)
                    End If
                Next

            Case "Confst"
                Dim confst As String = month3 & "-" & lblday.Text & "-" & lblYear.Text
                _frmWebBrowser.Confst.Text = confst
                doc = _frmWebBrowser.WebBrowser1.Document
                For Each elem In doc.All
                    If elem.GetAttribute("name") = "confst" Then
                        elem.SetAttribute("value", confst)
                    End If
                Next


        End Select


        If _frmACOPdependent.mouseFocus = "ACOP DEP" Then
            Dim Date1 As String = month3 & "/" & lblday.Text & "/" & lblYear.Text
            _frmACOPdependent.dgvMem.CurrentRow.Cells(2).Value = ""
            _frmACOPdependent.dgvMem.CurrentRow.Cells(2).Value = Date1

        Else

        End If

        Me.Close()
    End Sub

    Private Sub btn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btn1.Click, btn2.Click, btn3.Click, btn4.Click, btn5.Click, btn6.Click, btn7.Click, btn8.Click, btn9.Click, btn10.Click,
                                                                                       btn11.Click, btn12.Click, btn13.Click, btn14.Click, btn15.Click, btn16.Click, btn17.Click, btn18.Click, btn19.Click, btn20.Click,
                                                                                       btn21.Click, btn22.Click, btn23.Click, btn24.Click, btn25.Click, btn26.Click, btn27.Click, btn28.Click, btn29.Click, btn30.Click, btn31.Click
        Dim selectedDay As String = CType(sender, Button).Text

        If CShort(selectedDay) < 10 Then selectedDay = "0" & selectedDay

        lblday.Text = selectedDay

        Select Case FORMCHECK
            'Case "Maternity Notification"
            '    If _frmMaternityNotification.mouseFocus = "txtMonth" = True Or _frmMaternityNotification.mouseFocus = "txtDay" = True Or _frmMaternityNotification.mouseFocus = "txtYear" = True Then
            '        lblmonth2.Text = lblMonth.Text
            '        _frmMaternityNotification.txtdoby.Text = lblYear.Text
            '        _frmMaternityNotification.txtdobm.Text = month3
            '        _frmMaternityNotification.txtdobd.Text = selectedDay
            '    ElseIf _frmMaternityNotification.mouseFocus = "txtLDy" = True Or _frmMaternityNotification.mouseFocus = "txtLDd" = True Or _frmMaternityNotification.mouseFocus = "txtLDm" = True Then
            '        lblmonth2.Text = lblMonth.Text
            '        _frmMaternityNotification.txtLDY.Text = lblYear.Text
            '        _frmMaternityNotification.txtLDM.Text = month3
            '        _frmMaternityNotification.txtLDD.Text = selectedDay
            '    End If
            Case "Enhanced Maternity Notification"
                If _frmEnhanceMaternityNotif.mouseFocus = "txtMonth" = True Or _frmEnhanceMaternityNotif.mouseFocus = "txtDay" = True Or _frmEnhanceMaternityNotif.mouseFocus = "txtYear" = True Then
                    lblmonth2.Text = lblMonth.Text
                    _frmEnhanceMaternityNotif.txtdoby.Text = lblYear.Text
                    _frmEnhanceMaternityNotif.txtdobm.Text = month3
                    _frmEnhanceMaternityNotif.txtdobd.Text = selectedDay
                End If
            Case "Online Retirement Application"
                If _frmTechRetirementApplyDate.mouseFocus = "txtMonth" = True Or _frmTechRetirementApplyDate.mouseFocus = "txtDay" = True Or _frmTechRetirementApplyDate.mouseFocus = "txtYear" = True Then
                    lblmonth2.Text = lblMonth.Text
                    _frmTechRetirementApplyDate.txtdosy.Text = lblYear.Text
                    _frmTechRetirementApplyDate.txtdosm.Text = month3
                    _frmTechRetirementApplyDate.txtdosd.Text = lblday.Text
                End If
        End Select
    End Sub


End Class