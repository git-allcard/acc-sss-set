
Public Class _frmSalaryLoanEmployerv2

    Dim xtd As New ExtractedDetails

    Private Sub _frmSalaryLoanEmployerv2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub _frmSalaryLoanEmployer_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnSubmit_Click(sender As Object, e As EventArgs) Handles btnSubmit.Click
        Try
            'salaryLoanTag = readSettings(xml_Filename, xml_path, "salaryLoanTag")

            Dim resultDis As Integer = MessageBox.Show("YOUR SALARY LOAN WILL BE SUBMITTED FOR PROCESSING." & vbNewLine & vbNewLine _
                                                   & "DO YOU WANT TO CONTINUE?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultDis = DialogResult.Yes Then

                If SharedFunction.DisablePinOrFingerprint Then
                    submitSalaryLoanMemberV3()
                Else
                    CurrentTxnType = TxnType.ApplicationForSalaryLoanMember
                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    xtd.getRawFile()
                    If Not _frm2 Is Nothing Then _frmMainMenu.DisposeForm(_frm2)
                    _frm2.CardType = CShort(xtd.checkFileType)
                    _frm2.TopLevel = False
                    _frm2.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frm2.Dock = DockStyle.Fill
                    _frm2.Show()
                End If
            ElseIf resultDis = DialogResult.No Then

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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Try
            '_frmSalaryLoanDisclosurev2.WebBrowser1.Navigate(SL.calldisclosureResponse.path)
            _frmSalaryLoanDisclosurev2.TopLevel = False
            _frmSalaryLoanDisclosurev2.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmSalaryLoanDisclosurev2.Dock = DockStyle.Fill
            _frmSalaryLoanDisclosurev2.Show()
            Me.Hide()

        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Sub submitSalaryLoanMemberV3()
        Try
            'Dim employerAddtlMsg As String = "WE ARE AWAITING THE CERTIFICATION FROM YOUR EMPLOYER. YOUR SALARY LOAN APPLICATION WILL EXPIRE IF YOUR EMPLOYER FAILS TO CERTIFY ON OR BEFORE "

            'Console.WriteLine(IIf(_frmSalaryLoanDisclosurev2.employerAddress = "", "" & vbNewLine & vbNewLine, employerAddtlMsg & vbNewLine & vbNewLine))

            'Return
            Dim sa As String = tagPage

            transTag = "LG"

            xtd.getRawFile()

            Dim sl As New SalaryLoan.slMobileWS2BeanService
            If sl.submitSalaryLoanApplication(SSStempFile, _frmSalaryLoanDisclosurev2.memberStatus, _frmSalaryLoanDisclosurev2.employerSSNumber, SharedFunction.FormatERBRN(_frmSalaryLoanDisclosurev2.erBrn), _frmSalaryLoanDisclosurev2.loanAmount2, _frmSalaryLoanDisclosurev2.loanMonth, _frmSalaryLoanDisclosurev2.averageMsc, _frmSalaryLoanDisclosurev2.totalBalance, _frmSalaryLoanDisclosurev2.serviceCharge, _frmSalaryLoanDisclosurev2.netLoan, _frmSalaryLoanDisclosurev2.monthlyAmort, _frmSalaryLoanDisclosurev2.disbursementCode, _frmSalaryLoanDisclosurev2.bankCode, _frmSalaryLoanDisclosurev2.brstn, _frmSalaryLoanDisclosurev2.acctNo, _frmSalaryLoanDisclosurev2.fundingBank, _frmSalaryLoanDisclosurev2.advanceInterest) Then
                If sl.submitSalaryLoanApplicationResponse.processFlag = "1" Then
                    _frmUserAuthentication.getTransacNum()

                    Dim ip As New insertProcedure
                    ip.insertSalaryLoan(SSStempFile, HTMLDataExtractor.MemberFullName, _frmSalaryLoanDisclosurev2.memberStatus, _frmSalaryLoanDisclosurev2.loanAmount2, _frmSalaryLoanDisclosurev2.netLoan, _frmSalaryLoanv2.cboAccount.Text, _frmSalaryLoanDisclosurev2.employerSSNumber, _frmSalaryLoanDisclosurev2.employerName, _frmSalaryLoanDisclosurev2.employerAddress, sl.submitSalaryLoanApplicationResponse.transactionNo, _frmUserAuthentication.lblTransactionNo.Text)

                    Dim employerAddtlMsg As String = "WE ARE AWAITING THE CERTIFICATION FROM YOUR EMPLOYER." & vbNewLine & "YOUR SALARY LOAN APPLICATION WILL EXPIRE IF YOUR EMPLOYER FAILS TO CERTIFY ON OR BEFORE " & CDate(sl.submitSalaryLoanApplicationResponse.expiryDt).ToString("MMMM dd, yyyy") & "."
                    'Console.WriteLine(HTMLDataExtractor.MemberFullName)
                    _frmUserAuthentication.getTransacNum()
                    authentication = "SET002"
                    Dim dateTimePosted As String = Now.ToString("MMMM dd, yyyy hh:mm tt").ToUpper
                    authenticationMsg = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN APPLICATION TO SSS ON " & dateTimePosted & "." & vbNewLine & vbNewLine &
                                                           "LOAN PROCEEDS WILL BE CREDITED TO YOUR " & _frmSalaryLoanDisclosurev2.selectedBankAcct.ToUpper & "." & vbNewLine & vbNewLine &
                                                           IIf(_frmSalaryLoanDisclosurev2.employerAddress = "", "" & vbNewLine & vbNewLine, employerAddtlMsg & vbNewLine & vbNewLine) &
                                                           "PLEASE TAKE NOTE OF YOUR TRANSACTION NUMBERS: " & vbNewLine & vbNewLine &
                                                           "VERIFICATION TRANSACTION NUMBER: " & sl.submitSalaryLoanApplicationResponse.transactionNo & vbNewLine &
                                                           "SET TRANSACTION NUMBER: " & _frmUserAuthentication.lblTransactionNo.Text

                    _frmMainMenu.RedirectToserAuthenticationForm("SALARY LOAN APPLICATION", "SUCCESSFULLY SUBMITTED SALARY LOAN APPLICATION", "10032")

                    If _frmMainMenu.IsAllowedToPrint() Then
                        PrintReceipt(dateTimePosted, _frmSalaryLoanDisclosurev2.selectedBankAcct.ToUpper, _frmUserAuthentication.lblTransactionNo.Text, sl.submitSalaryLoanApplicationResponse.transactionNo)
                        _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                    End If
                Else
                    authentication = "SET002"
                    authenticationMsg = sl.submitSalaryLoanApplicationResponse.returnMessage.ToUpper
                    _frmMainMenu.RedirectToserAuthenticationForm("SALARY LOAN APPLICATION", sl.submitSalaryLoanApplicationResponse.returnMessage.ToUpper, "10032")

                    'SharedFunction.ShowWarningMessage(sl.submitSalaryLoanApplicationResponse.returnMessage.ToUpper)
                End If
            Else
                SharedFunction.ShowWarningMessage("FAILED TO SUBMIT SALARY LOAN APPLICATION." & sl.exceptions.ToString())
            End If
        Catch ex As Exception
            'MessageBox.Show(ex.Message)
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Sub PrintReceipt(ByVal dateTimePosted As String, ByVal bankAcct As String, ByVal setTxnNo As String, ByVal ssTxnNo As String)
        If SharedFunction.ShowMessage("Do you want to print the receipt?") = DialogResult.No Then Return

        Dim class1 As New PrintHelper
        Dim receiptMsg As New System.Text.StringBuilder
        receiptMsg.Append("YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY" & vbNewLine)
        receiptMsg.Append("LOAN APPLICATION TO SSS ON " & CDate(dateTimePosted).ToString("MMMM dd, yyyy").ToUpper & vbNewLine)
        receiptMsg.Append(CDate(dateTimePosted).ToString("hh:mm tt").ToUpper & "." & vbNewLine & vbNewLine)
        receiptMsg.Append("LOAN PROCEEDS WILL BE CREDITED TO YOUR" & vbNewLine)
        receiptMsg.Append(bankAcct & "." & vbNewLine & vbNewLine)
        receiptMsg.Append("PLEASE TAKE NOTE OF YOUR TRANSACTION NUMBERS " & vbNewLine & vbNewLine)
        receiptMsg.Append("VERIFICATION TRANSACTION NUMBER:" & vbNewLine)
        receiptMsg.Append(ssTxnNo & vbNewLine)
        receiptMsg.Append("SET TRANSACTION NUMBER:" & vbNewLine)
        receiptMsg.Append(setTxnNo)

        class1.prt(class1.prtSimplifiedWebRegistration_Receipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, receiptMsg.ToString, "SALARY LOAN APPLICATION", "Print"), _frmMainMenu.DefaultPrinterName)
        class1 = Nothing
    End Sub


End Class