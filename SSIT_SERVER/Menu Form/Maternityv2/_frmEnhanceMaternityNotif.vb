
Imports System.Runtime.InteropServices

Public Class _frmEnhanceMaternityNotif

    'Private Const EM_SETCUEBANNER As Integer = &H1501
    '<DllImport("user32.dll", EntryPoint:="SendMessageW")>
    'Private Shared Function SendMessageW(ByVal hWnd As IntPtr, ByVal Msg As UInt32, ByVal wParam As Boolean, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    'End Function
    Dim mouseDirection As String
    Dim db As New ConnectionString
    Dim orcl As New ConnectionString2
    Dim dateofDelivery As String
    Dim lastDelivery As String
    Dim fullnameFromCard As String
    Dim getfirstID As String
    Dim at As New auditTrail
    Dim getDetailstask As String
    Dim getTask As String
    Dim getModule As String
    Dim getAffectedTable As String
    Dim prevdevstr, prevdevdatestr, dateofdeliverystr As String
    Dim xtd As New ExtractedDetails
    Dim printF As New printModule
    Dim inMat As New insertProcedure
    Dim txn As New txnNo
    Public mouseFocus As String
    Dim tempSSSHeader As String
    Public transMatnotif As String
    Public matnotifTag As Integer = 0
    Dim xs As New MySettings

    Private Sub _frmEnhanceMaternityNotif_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ''papalitan to sa program ni sir adel hugutin ko sa card
        'SendMessageW(txtdobd.Handle, EM_SETCUEBANNER, False, "DD")
        'SendMessageW(txtdobm.Handle, EM_SETCUEBANNER, False, "MM")
        'SendMessageW(txtdoby.Handle, EM_SETCUEBANNER, False, "YYYY")
        ''SendMessageW(txtLDM.Handle, EM_SETCUEBANNER, False, "MM")
        ''SendMessageW(txtLDD.Handle, EM_SETCUEBANNER, False, "DD")
        ''SendMessageW(txtLDY.Handle, EM_SETCUEBANNER, False, "YYYY")

        Try
            cboRelationship.SelectedIndex = 0
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

    End Sub


    'Private Sub ButtonX1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If txtNumber.Text = "4" Then
    '    Else
    '        txtNumber.Text = txtNumber.Text + 1
    '    End If

    '    If txtNumber.Text = 0 Then

    '        txtLDM.Enabled = False
    '        txtLDD.Enabled = False
    '        txtLDY.Enabled = False
    '    Else
    '        txtLDM.Enabled = True
    '        txtLDD.Enabled = True
    '        txtLDY.Enabled = True

    '    End If

    '    If txtNumber.Text = 0 Then
    '        ButtonX2.Enabled = False
    '    Else
    '        ButtonX2.Enabled = True
    '    End If
    '    If txtNumber.Text = 4 Then
    '        ButtonX1.Enabled = False
    '    Else
    '        ButtonX1.Enabled = True
    '    End If

    'End Sub

    'Private Sub ButtonX2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    If txtNumber.Text = "0" Then
    '    Else
    '        txtNumber.Text = txtNumber.Text - 1
    '    End If

    '    If txtNumber.Text = 0 Then

    '        txtLDM.Enabled = False
    '        txtLDD.Enabled = False
    '        txtLDY.Enabled = False

    '        txtLDM.Text = ""
    '        txtLDD.Text = ""
    '        txtLDY.Text = ""
    '    Else
    '        txtLDM.Enabled = True
    '        txtLDD.Enabled = True
    '        txtLDY.Enabled = True

    '    End If

    '    If txtNumber.Text = 0 Then
    '        ButtonX2.Enabled = False
    '    Else
    '        ButtonX2.Enabled = True
    '    End If
    '    If txtNumber.Text = 4 Then
    '        ButtonX1.Enabled = False
    '    Else
    '        ButtonX1.Enabled = True
    '    End If

    'End Sub

    Private Sub _frmMaternityNotification_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    'Private Sub txtdobm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdobm.Click
    '    mouseFocus = "txtMonth"
    '    _frmCalendar.Close()
    '    _frmCalendar.Show()
    '    _frmCalendar.FORMCHECK = "Enhanced Maternity Notification"
    '    _frmCalendar.btncalsub.Visible = True
    '    '  txtdobm.Text = ""
    'End Sub
    'Private Sub txtdobd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdobd.Click
    '    mouseFocus = "txtDay"
    '    _frmCalendar.Close()
    '    _frmCalendar.Show()
    '    _frmCalendar.FORMCHECK = "Enhanced Maternity Notification"
    '    _frmCalendar.btncalsub.Visible = True
    '    '  txtdobd.Text = ""
    'End Sub
    'Private Sub txtdoby_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdoby.Click
    '    mouseFocus = "txtYear"
    '    _frmCalendar.Close()
    '    _frmCalendar.Show()
    '    _frmCalendar.FORMCHECK = "Enhanced Maternity Notification"
    '    _frmCalendar.btncalsub.Visible = True
    '    ' txtdoby.Text = ""
    'End Sub

    Private Sub txt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdobm.Click, txtdobd.Click, txtdoby.Click
        Dim startDate As Date = Now.Date
        Dim endDate As Date = DateAdd(DateInterval.Month, 9, startDate)
        Dim calv2 As New Calendar(txtdobm.Text, txtdobd.Text, txtdoby.Text, startDate.Date, endDate.Date)
        calv2.ShowCalendar()
        txtdobm.Text = calv2.Month
        txtdobd.Text = calv2.Day
        txtdoby.Text = calv2.Year
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            GC.Collect()
            xtd.getRawFile()
            Dim fileTYP As Integer = xtd.checkFileType

            If fileTYP = 1 Then
                getURL = getPermanentURL & "controller?action=sss&id=" & xtd.getCRN
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & xtd.getCRN)
            ElseIf fileTYP = 2 Then
                getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)
            End If


            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmMainMenu.pnlWeb.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmWebBrowser.TopLevel = False
            _frmWebBrowser.Parent = _frmMainMenu.pnlWeb
            _frmWebBrowser.Dock = DockStyle.Fill
            _frmWebBrowser.Show()
            _frmMainMenu.Button5.Enabled = False
            _frmMainMenu.Button6.Enabled = False
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub chk1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_1.CheckedChanged, chk1_2.CheckedChanged
        cboRelationship.Enabled = chk1_1.Checked
        txtFirstName.Enabled = chk1_1.Checked
        txtLastName.Enabled = chk1_1.Checked
        txtMiddleName.Enabled = chk1_1.Checked
        txtSuffix.Enabled = chk1_1.Checked
    End Sub

    Public Shared men As New Maternity.EnhanceNotification

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            transTag = "MT"
            xtd.getRawFile()

            Dim date1 As String = txtdobm.Text & txtdobd.Text & txtdoby.Text

            If txtdobd.Text.Trim = "" Or txtdobm.Text.Trim = "" Or txtdoby.Text.Trim = "" Then
                lblerror1.Visible = True
                SharedFunction.ShowWarningMessage("EXPECTED DATE OF DELIVERY IS NOT VALID.")
            ElseIf Not chk1_1.Checked And Not chk1_2.Checked Then
                lblerror1.Visible = True
                SharedFunction.ShowWarningMessage("SELECT IF YOU WANT TO ALLOCATE OR AVAIL FULL MATERNITY BENEFIT.")
                'ElseIf Not chk1_1.Checked And Not cboRelationship.SelectedIndex = 0 Then
                '    lblerror1.Visible = True
                '    SharedFunction.ShowWarningMessage("SELECT RELATIONSHIP.")
                'ElseIf chk1_1.Checked And txtNoOfDays.Text.trim = "" Then
                '    lblerror1.Visible = True
                '    SharedFunction.ShowWarningMessage("ENTER NO OF DAYS TO ALLOCATE.")
            ElseIf chk1_1.Checked And txtNoOfDays.Text = "" Then
                lblerror1.Visible = True
                SharedFunction.ShowWarningMessage("NUMBER OF DAYS FOR ALLOCATION IS NOT VALID.")
            ElseIf chk1_1.Checked And CShort(txtNoOfDays.Text) = 0 Then
                lblerror1.Visible = True
                SharedFunction.ShowWarningMessage("NUMBER OF DAYS FOR ALLOCATION IS NOT VALID.")
            ElseIf chk1_1.Checked And CShort(txtNoOfDays.Text) > 7 Then
                lblerror1.Visible = True
                SharedFunction.ShowWarningMessage("MAXIMUM NUMBER OF DAYS FOR ALLOCATION IS SEVEN (7) DAYS ONLY.")
            Else
                Dim dob As Date = txtdobm.Text & "/" & txtdobd.Text & "/" & txtdoby.Text
                Dim dob2 As String = dob.ToString("MM-dd-yyyy")

                'Dim men As New Maternity.EnhanceNotification
                men.sssNo = SSStempFile
                men.deliveryDate = dob
                men.firstName = txtFirstName.Text
                men.middleName = txtMiddleName.Text
                men.lastName = txtLastName.Text
                men.suffix = txtSuffix.Text
                men.noOfDays = txtNoOfDays.Text
                men.isAllocateCredit = chk1_1.Checked
                men.relationship = cboRelationship.Text
                If men.IsValid Then
                    _frmEnhanceMaternityNotifSummary.lblDatedelivery.Text = dob2
                    _frmEnhanceMaternityNotifSummary.lblName.Text = String.Format("{0}{1} {2} {3}", txtFirstName.Text, IIf(txtMiddleName.Text = "", "", " " & txtMiddleName.Text), txtLastName.Text, txtSuffix.Text)
                    _frmEnhanceMaternityNotifSummary.lblRelationship.Text = IIf(cboRelationship.SelectedIndex = 0, "", cboRelationship.Text)
                    _frmEnhanceMaternityNotifSummary.lblNoOfDays.Text = IIf(txtNoOfDays.Text = "0", "", txtNoOfDays.Text)

                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmEnhanceMaternityNotifSummary.TopLevel = False
                    _frmEnhanceMaternityNotifSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmEnhanceMaternityNotifSummary.Dock = DockStyle.Fill
                    _frmEnhanceMaternityNotifSummary.Show()
                    matnotifTag = 0
                Else
                    SharedFunction.ShowWarningMessage(men.exceptions)
                End If

            End If
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Maternity Notification" & "|" & "Click Maternity Notification Submit button Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try
    End Sub

    Private Sub txtName_Enter(sender As Object, e As EventArgs) Handles txtFirstName.Enter, txtMiddleName.Enter, txtLastName.Enter, txtSuffix.Enter, txtNoOfDays.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

End Class