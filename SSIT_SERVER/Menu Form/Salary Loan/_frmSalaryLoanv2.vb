
Public Class _frmSalaryLoanv2

    Public employerSSNumber As String = ""
    Public seqNo As String = ""
    Public prevLoanAmount As Double = 0
    Public loanAmount As Double = 0
    Public serviceCharge As Double = 0
    Public memberStatus As String = ""

    Public memberBankAccts As List(Of SalaryLoan.memberBankAcct)
    Public employerAddresses As List(Of MobileWS2BeanService.employerAddress)
    Public employers As List(Of MobileWS2BeanService.employer)

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmSalaryLoanv2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopulateBankAccts()

            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
            _frmMainMenu.DisposeForm(_frmLoading)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmSalaryLoanv2_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Public Sub ShowTermsAndConditions(ByVal displayType As Short)
        Try
            _frmMainMenu.DisposeForm(_frmTerms)

            _frmTerms.displayType = displayType
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmTerms.TopLevel = False
            _frmTerms.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmTerms.Dock = DockStyle.Fill
            _frmTerms.Show()
            _frmMainMenu.Button5.Enabled = False
            _frmMainMenu.Button6.Enabled = False
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If pnlEmployer.Visible = True Then
            If cboEmployer.SelectedIndex = 0 Then
                SharedFunction.ShowWarningMessage("EMPLOYER IS REQUIRED.")
                Return
            ElseIf cboBranchOffice.SelectedIndex = 0 Then
                SharedFunction.ShowWarningMessage("BRANCH OFFICE IS REQUIRED.")
                Return
            End If
        End If

        If cboAccount.SelectedIndex = 0 Then
            SharedFunction.ShowWarningMessage("DISBURSEMENT ACCOUNT IS REQUIRED.")
            Return
        End If

        If Not ckBoxTerms.Checked Then
            SharedFunction.ShowInfoMessage("YOU MUST AGREE TO THE TERMS AND CONDITIONS. ")
            Return
        Else
            SharedFunction.ShowInfoMessage("MAKE SURE THAT YOUR CONTACT INFORMATION WITH SSS IS UPDATED. TAP THE OK BUTTON TO CONTINUE YOUR APPLICATION.")

            Dim sl As New SalaryLoan.webcallingds
            Dim address As String = ""
            Dim erBrn As String = "0"
            If pnlEmployer.Visible = True Then
                address = employerAddresses(cboBranchOffice.SelectedIndex - 1).address
                erBrn = employerAddresses(cboBranchOffice.SelectedIndex - 1).erbrn
            End If

            Dim computedServiceCharge As Double = CDec(cboAmount.Text) * 0.01
            If computedServiceCharge = serviceCharge Then computedServiceCharge = serviceCharge

            btnCancel.Enabled = False
            btnNext.Enabled = False

            Select Case sl.calldisclosure(SSStempFile, employerSSNumber, "S", cboAmount.Text, 2, "", prevLoanAmount, computedServiceCharge, seqNo, address)
                Case 0
                    _frmMainMenu.DisposeForm(_frmSalaryLoanDisclosurev2)
                    _frmSalaryLoanDisclosurev2.WebPageLoaded1()

                    _frmSalaryLoanDisclosurev2.memberStatus = memberStatus
                    _frmSalaryLoanDisclosurev2.employerSSNumber = employerSSNumber
                    _frmSalaryLoanDisclosurev2.employerName = lblEmployerName.Text
                    _frmSalaryLoanDisclosurev2.employerAddress = address
                    _frmSalaryLoanDisclosurev2.erBrn = erBrn 'sl.calldisclosureResponse.er_no
                    _frmSalaryLoanDisclosurev2.loanAmount = loanAmount
                    _frmSalaryLoanDisclosurev2.loanAmount2 = cboAmount.Text
                    _frmSalaryLoanDisclosurev2.loanMonth = 2
                    _frmSalaryLoanDisclosurev2.averageMsc = loanAmount 'sl.calldisclosureResponse.ln_amt
                    _frmSalaryLoanDisclosurev2.totalBalance = sl.calldisclosureResponse.loanbal
                    _frmSalaryLoanDisclosurev2.serviceCharge = computedServiceCharge
                    _frmSalaryLoanDisclosurev2.netLoan = sl.calldisclosureResponse.net_proceeds
                    _frmSalaryLoanDisclosurev2.monthlyAmort = sl.calldisclosureResponse.monthly_amort
                    _frmSalaryLoanDisclosurev2.disbursementCode = 1
                    _frmSalaryLoanDisclosurev2.selectedBankAcct = cboAccount.Text

                    'Dim index As Short = 1
                    'If cboAccount.Text.Contains("UMID") Then index = 2
                    Dim bankDetail = memberBankAccts.Find(Function(x) x.acctNumber.Contains(cboAccount.Text.Split("-")(cboAccount.Text.Split("-").Count - 1).Trim))

                    _frmSalaryLoanDisclosurev2.bankCode = bankDetail.bankCode
                    _frmSalaryLoanDisclosurev2.brstn = bankDetail.brstn
                    _frmSalaryLoanDisclosurev2.acctNo = bankDetail.acctNumber
                    _frmSalaryLoanDisclosurev2.fundingBank = bankDetail.bankDepBank
                    _frmSalaryLoanDisclosurev2.advanceInterest = sl.calldisclosureResponse.adv_interest

                    _frmSalaryLoanDisclosurev2.WebBrowser1.Navigate(SharedFunction.ViewPDF(sl.calldisclosureResponse.path))
                    _frmSalaryLoanDisclosurev2.TopLevel = False
                    _frmSalaryLoanDisclosurev2.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmSalaryLoanDisclosurev2.Dock = DockStyle.Fill
                    _frmSalaryLoanDisclosurev2.Show()
                    Me.Hide()
                Case 1
                    SharedFunction.ShowUnableToConnectToRemoteServerMessage()
                Case Else
                    SharedFunction.ShowAPIResponseMessage(sl.exceptions.ToUpper)
            End Select
            'Me.btnInquiry_Click()

            btnCancel.Enabled = True
            btnNext.Enabled = True
        End If


    End Sub

    Public Function URLExists(ByVal url As String) As Boolean
        Dim result As Boolean = False
        Dim webRequest As System.Net.WebRequest = webRequest.Create(url)
        webRequest.Timeout = 1200
        webRequest.Method = "HEAD"
        Dim response As System.Net.HttpWebResponse = Nothing

        Try
            response = CType(webRequest.GetResponse(), System.Net.HttpWebResponse)
            result = True
        Catch webException As System.Net.WebException
            Console.Write(url & " doesn't exist: " & webException.Message)
        Finally

            If response IsNot Nothing Then
                response.Close()
            End If
        End Try

        Return result
    End Function

    Public Sub PopulateLonabaleAmount(ByVal decAmount As Decimal)
        Dim decRunningAmount As Decimal = decAmount
        cboAmount.Items.Add(decAmount.ToString("N0"))

        Do While decRunningAmount > 4000
            decRunningAmount -= 1000
            cboAmount.Items.Add(decRunningAmount.ToString("N0"))
        Loop

        cboAmount.Items.Add(decRunningAmount.ToString("N0"))

        Do While decRunningAmount > 2000
            decRunningAmount -= 500
            cboAmount.Items.Add(decRunningAmount.ToString("N0"))
        Loop
        If cboAmount.Items.Count > 0 Then cboAmount.SelectedIndex = 0
    End Sub

    Private Sub Redirect(ByVal frm As Form)
        Try
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            frm.TopLevel = False
            frm.Parent = _frmMainMenu.splitContainerControl.Panel2
            frm.Dock = DockStyle.Fill
            frm.Show()
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub cboBranchOffice_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBranchOffice.SelectedIndexChanged
        Try
            If cboBranchOffice.SelectedIndex = 0 Then
                lblBranchOffice.Text = ""
            Else
                lblBranchOffice.Text = cboBranchOffice.Text
                EmployerWithErBrn()
            End If
        Catch ex As Exception
            lblBranchOffice.Text = ""
        End Try
    End Sub

    Private Sub cboAmount_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboAmount.SelectedIndexChanged

    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmMainMenu.btnInquiry_Click()
    End Sub

    Private Sub Label12_Click(sender As Object, e As EventArgs) Handles lblTerms.Click
        ShowTermsAndConditions(1)
    End Sub

    Private Sub ckBoxTerms_CheckedChanged(sender As Object, e As EventArgs) Handles ckBoxTerms.CheckedChanged
        If pnlEmployer.Visible = True Then
            If cboBranchOffice.SelectedIndex = 0 Then
                SharedFunction.ShowWarningMessage("BRANCH OFFICE IS REQUIRED.")
                Return
            End If
        End If

        If cboAccount.SelectedIndex = 0 Then
            SharedFunction.ShowWarningMessage("DISBURSEMENT ACCOUNT IS REQUIRED.")
            Return
        End If

        If ckBoxTerms.Checked Then ShowTermsAndConditions(1)
    End Sub

    Public Sub getBankAccountListBySSNumber()
        Dim sl As New SalaryLoan.slBankWorkflowWebService
        If sl.getBankAccountListBySSNumber(SSStempFile) Then
            For Each anw As BankWorkflowWebService.accountNumberWorkflow In sl.getBankAccountListBySSNumberResponse
                If anw.REPLY_CODE = "0" Then
                    If memberBankAccts Is Nothing Then memberBankAccts = New List(Of SalaryLoan.memberBankAcct)
                    Dim memberBankAcct As New SalaryLoan.memberBankAcct
                    memberBankAcct.bankName = anw.BANK_NAME
                    memberBankAcct.bankCode = anw.BANK_CODE
                    memberBankAcct.acctNumber = anw.ACCNT_NUMBER
                    memberBankAcct.acctType = anw.ACCNT_TYPE
                    memberBankAcct.brstn = anw.BRSTN
                    memberBankAcct.bankDepBank = anw.BANK_DEPBNK
                    memberBankAccts.Add(memberBankAcct)
                End If
            Next
        End If
    End Sub

    Public Sub PopulateBankAccts()
        cboAccount.Items.Add("-Select-")
        Dim uaAcct = memberBankAccts.Find(Function(x) x.acctType.Contains("UA"))
        If Not uaAcct Is Nothing Then
            'cboAccount.Items.Add(String.Format("{0} {1} - {2}", uaAcct.bankCode.Trim, AccountTypeDesc(uaAcct.acctType), uaAcct.acctNumber))
            cboAccount.Items.Add(String.Format("{0} {1} - {2}", AccountTypeDesc(uaAcct.acctType), uaAcct.bankCode.Trim, uaAcct.acctNumber))
            cboAccount.SelectedIndex = 1
            cboAccount.Enabled = False
            lblReminder.Visible = True
            lblReminder2.Visible = False
            link2.Visible = False
        Else
            For Each memberBankAcct As SalaryLoan.memberBankAcct In memberBankAccts
                cboAccount.Items.Add(String.Format("{0} {1} - {2}", memberBankAcct.bankCode.Trim, AccountTypeDesc(memberBankAcct.acctType), memberBankAcct.acctNumber))
            Next
            If cboAccount.Items.Count = 2 Then
                cboAccount.SelectedIndex = 1
                cboAccount.Enabled = False
            Else
                cboAccount.Enabled = True
                cboAccount.SelectedIndex = 0
            End If
            lblReminder.Visible = False
            lblReminder2.Visible = True
            link2.Visible = True
        End If
    End Sub

    Public Function AccountTypeDesc(ByVal acctType As String) As String
        Select Case acctType
            Case "UA"
                Return "UMID-ATM"
            Case "SA"
                Return "SAVINGS ACCOUNT"
            Case "CA"
                Return "CHECKING ACCOUNT"
            Case "CC", "QC"
                Return "QUICK CARD"
            Case Else
                Return acctType
        End Select
    End Function

    Public Sub PopulateEmployers()
        cboEmployer.Items.Clear()
        cboEmployer.Items.Add("-Select-")

        For Each employer In employers
            cboEmployer.Items.Add(String.Format("{0}", employer.employerName))
        Next

        SharedFunction.DropdownSelectedValue(cboEmployer)
    End Sub

    Public Sub PopulateEmployerAddressesSL()
        cboBranchOffice.Items.Clear()
        cboBranchOffice.Items.Add("-Select-")
        Dim sl As New SalaryLoan.slMobileWS2BeanService
        Select Case sl.getEmployerAddressesSL(employerSSNumber)
            Case 0
                For Each empAddress As MobileWS2BeanService.employerAddress In sl.getEmployerAddressesSLResponse
                    If empAddress.processFlag = "1" Then
                        If employerAddresses Is Nothing Then employerAddresses = New List(Of MobileWS2BeanService.employerAddress)
                        employerAddresses.Add(empAddress)
                        'cboBranchOffice.Items.Add(String.Format("{0} - {1}", empAddress.erbrn.Trim, empAddress.address.Trim))
                        cboBranchOffice.Items.Add(String.Format("{0}", empAddress.address.Trim))
                    End If
                Next

                SharedFunction.DropdownSelectedValue(cboBranchOffice)
            Case 1
                SharedFunction.ShowUnableToConnectToRemoteServerMessage()
            Case Else
                SharedFunction.ShowAPIResponseMessage(sl.exceptions.ToUpper)
        End Select
    End Sub


    Private Sub cboEmployer_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployer.SelectedIndexChanged
        Try
            If cboEmployer.SelectedIndex = 0 Then
                lblEmployerId.Text = ""
                lblEmployerName.Text = ""
                cboBranchOffice.Items.Clear()
                lblBranchOffice.Text = ""
            Else
                lblBranchOffice.Text = cboBranchOffice.Text
                employerSSNumber = employers(cboEmployer.SelectedIndex - 1).employerSSNumber
                EmployerWithErBrn()
                lblEmployerName.Text = employers(cboEmployer.SelectedIndex - 1).employerName
                PopulateEmployerAddressesSL()
                'lblBranchOffice.Text = ""
            End If
        Catch ex As Exception
            lblEmployerId.Text = ""
            lblEmployerName.Text = ""
            'lblBranchOffice.Text = ""
        End Try
    End Sub

    Private Sub EmployerWithErBrn()
        Dim cboErBrn As String = ""
        If Not employerAddresses Is Nothing Then cboErBrn = SharedFunction.FormatERBRN(employerAddresses(cboBranchOffice.SelectedIndex - 1).erbrn)
        lblEmployerId.Text = String.Format("{0} - {1}", employerSSNumber, cboErBrn)
    End Sub

    Private Sub link1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles link1.LinkClicked
        _frmMainMenu.btnUpdateContactInfo.PerformClick()
    End Sub

    Private Sub link2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles link2.LinkClicked
        ShowTermsAndConditions(2)
    End Sub

    Public Function GetQuickCardNotes(ByVal bln As Boolean) As String
        link2.Visible = bln
        If bln Then
            Return "
Choose your preferred ELECTRONIC LOAN DISBURSEMENTS below:

PESONet Participating Bank – Active single account in any PESONet accredited banks under your name. Joint account is not acceptable.

SSS ISSUED UBP Quick Card – To avail of a Free UBP Quick Card, please tap          to see the list of SSS branches where you can avail.

Note: SSS issued UBP Quick Card starts with “"10"” and composed of 12 digits only. Enrollment and nomination of UBP accounts linked with other government offices (ex. Pag-ibig Loyalty Card) is not allowed.

REMINDERS:

Make sure that your Contact Information with SSS is updated. To check, please tap ""Update Contact Information"" at left panel.

Nomination of invalid/ incorrect account number, closed account and dormant account will result to unsuccessful crediting of your proceeds, and shall be subject for cancellation of approved loan. Hence, a re-submission of a new application loan application will be required."
        Else
            Return "
Choose your preferred ELECTRONIC LOAN DISBURSEMENTS below:

PESONet Participating Bank – Active single account in any PESONet accredited banks under your name. Joint account is not acceptable.

REMINDERS:

Make sure that your Contact Information with SSS is updated. To check, please tap ""Update Contact Information"" at left panel.

Nomination of invalid/ incorrect account number, closed account and dormant account will result to unsuccessful crediting of your proceeds, and shall be subject for cancellation of approved loan. Hence, a re-submission of a new application loan application will be required."
        End If
    End Function

End Class