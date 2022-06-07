Public Class _frmPensionMaintenance
    Dim xtd As New ExtractedDetails
    Dim printf As New printModule
    Dim db As New ConnectionString
    Dim inMat As New insertProcedure
    Dim Address1, Address2, Address3 As String
    Dim pcode As String
    ReadOnly ValidChars As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.- "
    ReadOnly ValidCharsCity As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,- "
    ReadOnly ValidCharsNum As String = _
"0123456789 "
    Dim tempSSSHeader As String

    Public Function showDetails(ByVal add1 As String, ByVal add2 As String, ByVal add3 As String, ByVal add4 As String, ByVal landline As String, ByVal mobile As String, ByVal email As String)
        txtAdd1.Text = add1 & " " & add2
        txtAdd3.Text = add3
        txtAdd4.Text = add4
        txtLandline.Text = landline
        txtMobile.Text = mobile
        txtEmail.Text = email
    End Function

    Private Sub _frmPensionMaintenance_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub _frmPensionMaintenance_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            Dim tempListView As New ListView
            Dim contactDetails As String
            Dim eaddlen, contactlen As String

            tagPage = "7"

            xtd.getRawFile()
            Dim result As Integer = xtd.checkFileType
            If result = 1 Then

                tempSSSHeader = xtd.getCRN
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                lblCRNNum.Text = ""

                Dim fname As String = printf.GetFirstName(_frmWebBrowser.WebBrowser1)
                Dim mname As String = printf.GetMiddleName(_frmWebBrowser.WebBrowser1)
                Dim lname As String = printf.GetLastName(_frmWebBrowser.WebBrowser1)
                Dim fullname As String = lname & " " & fname & " " & mname

                lblMemberName.Text = fullname.Replace("�", ChrW(209))

                'contactDetails = xtd.extractInfoDetails(xtd.getCRN)

                'If contactDetails = "" Or contactDetails = Nothing Or contactDetails = "null" Then
                '    Dim address1 As String = db.putSingleValue("select addr1 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                '    address1 = address1.Trim
                '    Dim address2 As String = db.putSingleValue("select addr2 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                '    address2 = address2.Trim
                '    Dim pcode As String = db.putSingleValue("select epcod from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")

                '    txtAdd1.Text = address1
                '    txtAdd3.Text = ""
                '    txtAdd4.Text = address2
                '    Add5.Text = pcode
                'Else


                'Dim address1 As String = db.putSingleValue("select addr1 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                'address1 = address1.Trim
                'Dim address2 As String = db.putSingleValue("select addr2 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                'address2 = address2.Trim
                'Dim pcode As String = db.putSingleValue("select epcod from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")

                'txtAdd1.Text = address1
                'txtAdd3.Text = ""
                'txtAdd4.Text = address2
                'Add5.Text = pcode


                'If contactDetails = "" Or contactDetails = Nothing Or contactDetails = "null" Then

                'Else

                '    Dim _split As String() = contactDetails.Split(New Char() {"/"c})

                '    If _split.Length = 2 Then

                '        Dim tempContact As String = _split(1)

                '        Dim eadd As String = _split(1)

                '        If eadd = "" Then
                '            eadd = ""
                '        Else
                '            eaddlen = eadd.Length
                '            eaddlen = eaddlen - 14
                '            eadd = eadd.Substring(14, eaddlen)
                '            eadd = eadd.Trim
                '        End If

                '        txtEmail.Text = eadd


                '    ElseIf _split.Length > 3 Then

                '        Dim tempContact As String = _split(2)

                '        Dim eadd As String = _split(1)

                '        If eadd = "" Then
                '            eadd = ""
                '        Else
                '            eaddlen = eadd.Length
                '            eaddlen = eaddlen - 14
                '            eadd = eadd.Substring(14, eaddlen)
                '            eadd = eadd.Trim
                '        End If

                '        Dim contACT As String = _split(2)

                '        If contACT = "" Then
                '            contACT = ""
                '        Else
                '            contactlen = contACT.Length
                '            contactlen = contactlen - 14
                '            contACT = contACT.Substring(14, contactlen)
                '            contACT = contACT.Trim
                '        End If

                '        If tempContact.Contains("Mobile") Then
                '            txtEmail.Text = eadd
                '            txtMobile.Text = contACT
                '        ElseIf tempContact.Contains("Office Phone") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        ElseIf tempContact.Contains("Telephone") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        ElseIf tempContact.Contains("Fax") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        End If

                '    ElseIf _split.Length > 4 Then

                '        Dim tempContact As String = _split(3)

                '        Dim eadd As String = _split(1)

                '        If eadd = "" Then
                '            eadd = ""
                '        Else
                '            eaddlen = eadd.Length
                '            eaddlen = eaddlen - 14
                '            eadd = eadd.Substring(14, eaddlen)
                '            eadd = eadd.Trim
                '        End If

                '        Dim contACT As String = _split(2)

                '        If contACT = "" Then
                '            contACT = ""
                '        Else
                '            contactlen = contACT.Length
                '            contactlen = contactlen - 14
                '            contACT = contACT.Substring(14, contactlen)
                '            contACT = contACT.Trim
                '        End If

                '        If tempContact.Contains("Mobile") Then
                '            txtEmail.Text = eadd
                '            txtMobile.Text = contACT
                '        ElseIf tempContact.Contains("Office Phone") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        ElseIf tempContact.Contains("Telephone") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        ElseIf tempContact.Contains("Fax") Then
                '            txtEmail.Text = eadd
                '            txtLandline.Text = contACT
                '        End If

                '    Else
                '        txtEmail.Text = ""
                '        txtLandline.Text = ""
                '        txtMobile.Text = ""
                '    End If

                'End If

                'End If

            ElseIf result = 2 Then

                tempSSSHeader = SSStempFile
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                Dim fname As String = printf.GetFirstName(_frmWebBrowser.WebBrowser1)
                Dim mname As String = printf.GetMiddleName(_frmWebBrowser.WebBrowser1)
                Dim lname As String = printf.GetLastName(_frmWebBrowser.WebBrowser1)
                Dim fullname As String = lname & " " & fname & " " & mname
                lblMemberName.Text = fullname.Replace("�", ChrW(209))
                'lblMemberName.Text = xtd.getFullname
                lblMemberName.Text = lblMemberName.Text.Replace("�", ChrW(209))
                lblCRNNum.Text = xtd.getCRN

                'Address1 = xtd.getHouse() & " " & xtd.getstName
                'Address2 = xtd.getSubd() & " " & xtd.getBarangay
                'Address3 = xtd.getCity
                'pcode = xtd.getPostalCode()


                'If Address1 = "" Then
                '    Address1 = db.putSingleValue("select addr1 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                '    Address1 = Address1.Trim
                'ElseIf Address2 = "" Then
                '    Address2 = db.putSingleValue("select addr2 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                '    Address2 = Address2.Trim
                'ElseIf pcode = "" Then
                '    pcode = db.putSingleValue("select epcod from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")
                'End If

                'txtAdd1.Text = Address1
                'txtAdd3.Text = Address2
                'txtAdd4.Text = Address3
                'Add5.Text = pcode

            End If
            Dim bdate As String = printf.GetDateBith(_frmWebBrowser.WebBrowser1)
            lblDateofBirth.Text = bdate
            Dim coverageStatus As String = printf.GetDateCoverage(_frmWebBrowser.WebBrowser1)
            lblDateofCoverage.Text = coverageStatus

        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

    End Sub

    Private Sub validateEmail(ByVal mail As String)
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If email.IsMatch(mail) Then
            lblError2.Visible = False
        Else
            ' MsgBox("The Email is not Valid", MsgBoxStyle.Information, "Registration")
            ' isValid = False
            lblError2.Visible = False
        End If

    End Sub

    Private Sub Add5_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles Add5.KeyPress
        e.Handled = Not (ValidCharsNum.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))

    End Sub

    Private Sub txtLandline_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLandline.KeyPress
        e.Handled = Not (ValidCharsNum.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtMobile_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMobile.KeyPress
        e.Handled = Not (ValidCharsNum.IndexOf(e.KeyChar) > -1 _
OrElse e.KeyChar = Convert.ToChar(Keys.Back))
    End Sub

    Private Sub txtAdd4_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdd4.KeyPress
        e.Handled = Not (ValidCharsCity.IndexOf(e.KeyChar) > -1 _
               OrElse e.KeyChar = Convert.ToChar(Keys.Back))

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
            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
            '_frmMainMenu.Button5.Text = "BACK"
            '_frmMainMenu.Button6.Text = "NEXT"
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub txtAdd1_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdd1.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
              OrElse e.KeyChar = Convert.ToChar(Keys.Back))

    End Sub

    Private Sub txtAdd3_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAdd3.KeyPress
        e.Handled = Not (ValidChars.IndexOf(e.KeyChar) > -1 _
              OrElse e.KeyChar = Convert.ToChar(Keys.Back))

    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")
            Dim chkPcode As Boolean = xtd.postalCodeChecker(Add5.Text)
            If txtAdd1.Text.Trim = "" Then
                MsgBox("ADDRESS 1 IS REQUIRED.", MsgBoxStyle.Information, "Information")
                txtAdd1.Focus()
            ElseIf txtAdd3.Text.Trim = "" Then
                MsgBox("ADDRESS 2 IS REQUIRED.", MsgBoxStyle.Information, "Information")
                txtAdd3.Focus()
            ElseIf txtAdd4.Text.Trim = "" Then
                MsgBox("CITY/PROVINCE IS REQUIRED.", MsgBoxStyle.Information, "Information")
                txtAdd4.Focus()
            ElseIf Add5.Text.Trim = "" Then
                MsgBox("POSTAL CODE IS REQUIRED.", MsgBoxStyle.Information, "Information")
                Add5.Focus()
            ElseIf chkPcode = False Then
                MsgBox("POSTAL CODE IS INVALID.", MsgBoxStyle.Information, "Information")
                Add5.Focus()
            ElseIf txtLandline.Text.Trim = "" And txtMobile.Text.Trim = "" Then
                MsgBox("LANDLINE NUMBER OR MOBILE NUMBER IS REQUIRED.", MsgBoxStyle.Information, "Information")
                txtLandline.Focus()
            ElseIf txtEmail.Text.Trim = "" Then
                MsgBox("EMAIL ADDRESS IS REQUIRED.", MsgBoxStyle.Information, "Information")
                txtEmail.Focus()
            ElseIf Not email.IsMatch(txtEmail.Text.Trim) Then
                lblError2.Visible = False
                MsgBox("EMAIL ADDRESS IS INVALID.", vbInformation, "Information")
                txtEmail.Focus()

            ElseIf Not txtLandline.Text = "" And txtMobile.Text = "" Then

                If txtLandline.TextLength < 7 Then
                    MsgBox("LANDLINE NUMBER SHOULD HAVE A MINIMUM OF 7 CHARACTERS.", MsgBoxStyle.Information, "Information")
                    txtLandline.Focus()
                Else
                    _frmPensionSummary.lblBranch.Text = kioskBranch
                    _frmPensionSummary.lblTerminalNo.Text = kioskID
                    _frmPensionSummary.txtAdd1.Text = txtAdd1.Text
                    _frmPensionSummary.txtAdd3.Text = txtAdd3.Text
                    _frmPensionSummary.txtAdd4.Text = txtAdd4.Text
                    _frmPensionSummary.Add5.Text = Add5.Text
                    _frmPensionSummary.txtLandline.Text = txtLandline.Text
                    _frmPensionSummary.txtMobile.Text = txtMobile.Text
                    _frmPensionSummary.txtEmail.Text = txtEmail.Text

                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmPensionSummary.TopLevel = False
                    _frmPensionSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmPensionSummary.Dock = DockStyle.Fill
                    _frmPensionSummary.Show()
                    _frmMainMenu.Button5.Enabled = False
                    _frmMainMenu.Button6.Enabled = False
                    _frmMainMenu.PrintControls(False)

                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                End If
            ElseIf Not txtMobile.Text = "" And txtLandline.Text = "" Then

                If txtMobile.TextLength < 11 Then
                    MsgBox("MOBILE NUMBER SHOULD HAVE A MINIMUM OF 11 CHARACTERS.", MsgBoxStyle.Information, "Information")
                    txtMobile.Focus()
                Else
                    _frmPensionSummary.lblBranch.Text = kioskBranch
                    _frmPensionSummary.lblTerminalNo.Text = kioskID
                    _frmPensionSummary.txtAdd1.Text = txtAdd1.Text
                    _frmPensionSummary.txtAdd3.Text = txtAdd3.Text
                    _frmPensionSummary.txtAdd4.Text = txtAdd4.Text
                    _frmPensionSummary.Add5.Text = Add5.Text
                    _frmPensionSummary.txtLandline.Text = txtLandline.Text
                    _frmPensionSummary.txtMobile.Text = txtMobile.Text
                    _frmPensionSummary.txtEmail.Text = txtEmail.Text

                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmPensionSummary.TopLevel = False
                    _frmPensionSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmPensionSummary.Dock = DockStyle.Fill
                    _frmPensionSummary.Show()
                    _frmMainMenu.Button5.Enabled = False
                    _frmMainMenu.Button6.Enabled = False
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                End If
                'ElseIf txtLandline.TextLength < 7 And txtMobile.TextLength < 11 Then
                '    MsgBox("LANDLINE NUMBER SHOULD HAVE A MINIMUM OF 7 CHARACTERS.", MsgBoxStyle.Information, "Information")
                '    txtLandline.Focus()
            ElseIf Not txtMobile.Text = "" And Not txtMobile.Text = Nothing Then

                If txtMobile.TextLength < 11 Then
                    MsgBox("MOBILE NUMBER SHOULD HAVE A MINIMUM OF 11 CHARACTERS.", MsgBoxStyle.Information, "Information")
                    txtMobile.Focus()
                ElseIf txtLandline.TextLength < 7 Then
                    MsgBox("LANDLINE NUMBER SHOULD HAVE A MINIMUM OF 7 CHARACTERS.", MsgBoxStyle.Information, "Information")
                    txtLandline.Focus()
                Else
                    _frmPensionSummary.lblBranch.Text = kioskBranch
                    _frmPensionSummary.lblTerminalNo.Text = kioskID
                    _frmPensionSummary.txtAdd1.Text = txtAdd1.Text
                    _frmPensionSummary.txtAdd3.Text = txtAdd3.Text
                    _frmPensionSummary.txtAdd4.Text = txtAdd4.Text
                    _frmPensionSummary.Add5.Text = Add5.Text
                    _frmPensionSummary.txtLandline.Text = txtLandline.Text
                    _frmPensionSummary.txtMobile.Text = txtMobile.Text
                    _frmPensionSummary.txtEmail.Text = txtEmail.Text

                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmPensionSummary.TopLevel = False
                    _frmPensionSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmPensionSummary.Dock = DockStyle.Fill
                    _frmPensionSummary.Show()
                    _frmMainMenu.Button5.Enabled = False
                    _frmMainMenu.Button6.Enabled = False
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                End If


            Else
                _frmPensionSummary.lblBranch.Text = kioskBranch
                _frmPensionSummary.lblTerminalNo.Text = kioskID
                _frmPensionSummary.txtAdd1.Text = txtAdd1.Text
                _frmPensionSummary.txtAdd3.Text = txtAdd3.Text
                _frmPensionSummary.txtAdd4.Text = txtAdd4.Text
                _frmPensionSummary.Add5.Text = Add5.Text
                _frmPensionSummary.txtLandline.Text = txtLandline.Text
                _frmPensionSummary.txtMobile.Text = txtMobile.Text
                _frmPensionSummary.txtEmail.Text = txtEmail.Text

                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmPensionSummary.TopLevel = False
                _frmPensionSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmPensionSummary.Dock = DockStyle.Fill
                _frmPensionSummary.Show()
                _frmMainMenu.Button5.Enabled = False
                _frmMainMenu.Button6.Enabled = False
                _frmMainMenu.PrintControls(False)
                '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")

            End If

        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub txtEmail_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtEmail.KeyPress
        Dim email As New System.Text.RegularExpressions.Regex("^([0-9a-zA-Z]([-\.\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\w]*[0-9a-zA-Z]\.)+[a-zA-Z]{2,9})$")

        If email.IsMatch(txtEmail.Text.Trim) Then
            lblError2.Visible = False
        End If

    End Sub
End Class