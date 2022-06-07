
Public Class usrfrmPageHeader

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Private Sub usrfrmPageHeader_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1) 'getfName
            Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
            Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
            Dim fullname As String = lname & " " & fname & " " & mname

            'Dim crn As String = HTMLDataExtractor.getCRN(_frmWebBrowser.WebBrowser1)

            lblMemberName.Text = fullname.Replace("�", ChrW(209))
        ElseIf result = 2 Then

            tempSSSHeader = SSStempFile
            If tempSSSHeader = "" Then

            Else
                tempSSSHeader = tempSSSHeader.Insert(2, "-")
                tempSSSHeader = tempSSSHeader.Insert(10, "-")
            End If

            lblSSSNo.Text = tempSSSHeader
            lblCRNNum.Text = xtd.getCRN
            Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
            Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
            Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
            Dim fullname As String = lname & " " & fname & " " & mname
            'Dim crn As String = HTMLDataExtractor.getCRN(_frmWebBrowser.WebBrowser1)
            lblMemberName.Text = fullname.Replace("�", ChrW(209))
        End If
        Dim bdate As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
        lblDateofBirth.Text = bdate
        Dim coverageStatus As String = printF.GetDateCoverage(_frmWebBrowser.WebBrowser1)
        If IsDate(coverageStatus) Then
            lblDateofCoverage.Text = CDate(coverageStatus).ToString("MM-dd-yyyy")
        Else
            lblDateofCoverage.Text = coverageStatus
        End If

        Select Case transTag
            Case "PRN"
                lblHeader.Text = "PAYMENT REFERENCE NUMBER (PRN)"
            Case "LG"
                lblHeader.Text = "SALARY LOAN"
            Case "WR"
                lblHeader.Text = "SIMPLIFIED WEB REGISTRATION"
            Case "PC"
                lblHeader.Text = "PIN CHANGE"
            Case "UCI"
                lblHeader.Text = "UPDATE CONTACT INFORMATION"
            Case "MT"
                lblHeader.Text = "SUBMIT MATERNITY NOTIFICATION"
            Case "RT"
                lblHeader.Text = "SUBMISSION OF APPLICATION FOR RETIREMENT"
            Case "AC"
                lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
            Case "MPPH"
                lblHeader.Text = "MONTHLY PENSION PAYMENT HISTORY"
        End Select

    End Sub

End Class
