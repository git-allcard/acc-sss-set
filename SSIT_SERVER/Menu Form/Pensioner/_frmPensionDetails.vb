Public Class _frmPensionDetails

    Dim xtd As New ExtractedDetails
    Dim printf As New printModule
    Dim db As New ConnectionString
    Dim tempSSSHeader As String

    Private Sub _frmPensionDetails_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            'My.Settings.tagPage = "7"

            xtd.getRawFile()


            Dim fileTYP As Integer = xtd.checkFileType

            If fileTYP = 1 Then

                lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                lblBranch.Text = kioskBranch
                lblTerminalNo.Text = kioskID

                tempSSSHeader = xtd.getCRN
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

                lblMemberName.Text = fullname

                'lblAdd1.Text = xtd.getHouse & " " & xtd.getstName & vbNewLine & _
                'xtd.getSubd & vbNewLine & _
                'xtd.getCity & vbNewLine & _
                'xtd.getPostalCode
                Dim address1 As String = db.putSingleValue("select addr1 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")

                Dim address2 As String = db.putSingleValue("select addr2 from TEMPOLDSSSDETAILS where ssnum = '" & xtd.getCRN & "'")

                lblAdd1.Text = address1 & ", " & address2
                'lblSubd.Text = xtd.getSubd
                'lblCity.Text = xtd.getCity
                'lblZipCode.Text = xtd.getPostalCode
                lblLandline.Text = ""
                lblMobile.Text = ""
                lblEmail.Text = ""

                lblReferenceNo.Text = ""
                Label5.Visible = False

                lblReferenceNo.Visible = False

            ElseIf fileTYP = 2 Then

                lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                lblBranch.Text = kioskBranch
                lblTerminalNo.Text = kioskID

                tempSSSHeader = SSStempFile
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                lblReferenceNo.Text = xtd.getCRN
                Dim fname As String = printf.GetFirstName(_frmWebBrowser.WebBrowser1)
                Dim mname As String = printf.GetMiddleName(_frmWebBrowser.WebBrowser1)
                Dim lname As String = printf.GetLastName(_frmWebBrowser.WebBrowser1)
                Dim fullname As String = lname & " " & fname & " " & mname
                lblMemberName.Text = fullname.Replace("�", ChrW(209))
                'lblMemberName.Text = xtd.getFullname

                lblAdd1.Text = xtd.getHouse & ", " & xtd.getstName & ", " & vbNewLine & _
                xtd.getSubd & ", " & vbNewLine & _
                xtd.getCity & ", " & vbNewLine & _
                xtd.getPostalCode

                'lblSubd.Text = xtd.getSubd
                'lblCity.Text = xtd.getCity
                'lblZipCode.Text = xtd.getPostalCode
                lblLandline.Text = ""
                lblMobile.Text = ""
                lblEmail.Text = ""

                Label5.Visible = True
                lblReferenceNo.Visible = True

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


    Private Sub _frmPensionDetails_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class