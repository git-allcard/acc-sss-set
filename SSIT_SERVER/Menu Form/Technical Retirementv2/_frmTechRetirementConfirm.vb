
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementConfirm

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub pbUnmask_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub _frmTechRetirementConfirm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
            _frmMainMenu.DisposeForm(_frmLoading)

            If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                For Each ctrl As Control In Panel4.Controls
                    If TypeOf ctrl Is Label Then
                        Dim lbl As Label = CType(ctrl, Label)
                        CType(ctrl, Label).Font = New Font(lbl.Font.Name, lbl.Font.Size - 1, lbl.Font.Style)
                    End If
                Next

                For Each ctrl As Control In pnlEmployer.Controls
                    If TypeOf ctrl Is Label Then
                        Dim lbl As Label = CType(ctrl, Label)
                        CType(ctrl, Label).Font = New Font(lbl.Font.Name, lbl.Font.Size - 1, lbl.Font.Style)
                    End If
                Next

                For Each ctrl As Control In pnlContact.Controls
                    If TypeOf ctrl Is Label Then
                        Dim lbl As Label = CType(ctrl, Label)
                        CType(ctrl, Label).Font = New Font(lbl.Font.Name, lbl.Font.Size - 1, lbl.Font.Style)
                    End If
                Next
            End If
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmTechRetirementConfirm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Try
            'salaryLoanTag = readSettings(xml_Filename, xml_path, "salaryLoanTag")

            Dim resultDis As Integer = MessageBox.Show("YOUR ONLINE RETIREMENT APPLICATION WILL BE SUBMITTED FOR PROCESSING." & vbNewLine & vbNewLine _
                                                   & "DO YOU WANT TO CONTINUE?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultDis = DialogResult.Yes Then

                If SharedFunction.DisablePinOrFingerprint Then
                    submitTechnicalRetirementv2()
                Else
                    CurrentTxnType = TxnType.OnlineRetirement
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
            SharedFunction.ShowWarningMessage(ex.Message.ToUpper)
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    'Public Sub submitTechnicalRetirement()
    '    Dim onlineRetirement As New OnlineRetirement
    '    If onlineRetirement.employerCert(SSStempFile, _frmTechRetirementEmpHist.determined_doctg) Then
    '        Dim separationDate As String = IIf(_frmTechRetirementEmpHist.memberType = "Employed", _frmTechRetirementApplyDate.getSeparationDate(), "")
    '        Dim employerERBR As String = "0"
    '        Dim certificationTag As String = "0"
    '        If _frmTechRetirementEmpHist.memberType = "Employed" And
    '                _frmTechRetirementEmpHist.type_of_retirement <> "Technical Retirement" And
    '                onlineRetirement.memberClaimInformationEntitiesResponse(0).no_of_months_flg = "1" Then _
    '                certificationTag = "1"
    '        If Not _frmTechRetirementEmpHist.employerERBR Is Nothing Then employerERBR = _frmTechRetirementEmpHist.employerERBR

    '        Dim sl As New SalaryLoan.slMobileWS2BeanService
    '        Try
    '            Dim appliedFrom As String = String.Format("{0}{1}{2}", "SSET", kioskID, kioskName)
    '            If sl.insertRetirementApp(certificationTag, SSStempFile, _frmTechRetirementEmpHist.determined_doctg, lblAddress.Text, lblEmailAddress.Text, lblLandline.Text, lblMobile.Text, lblAdvancePension.Text, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).brstn, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).acctNumber, separationDate, _frmTechRetirementApplyMineworker.flg_120, _frmTechRetirementApplyMineworker.mp_amt, appliedFrom, _frmTechRetirementEmpHist.memberType, _frmTechRetirementEmpHist.employerSSNumber, _frmTechRetirementEmpHist.employerERBR) Then
    '                If sl.insertRetirementAppResponse.processFlag = "1" Then
    '                    Dim ip As New insertProcedure
    '                    _frmUserAuthentication.getTransacNum()
    '                    ip.insertOnlineRetirement(certificationTag, SSStempFile, _frmTechRetirementEmpHist.determined_doctg, lblAddress.Text, lblEmailAddress.Text, lblLandline.Text, lblMobile.Text, lblAdvancePension.Text, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).acctNumber, _frmTechRetirementApplyBank.txtSavings.Text, separationDate, _frmTechRetirementApplyMineworker.flg_120, _frmTechRetirementApplyMineworker.mp_amt, appliedFrom, _frmTechRetirementEmpHist.memberType.ToUpper.Substring(0, 1), _frmTechRetirementEmpHist.employerSSNumber, employerERBR, _frmUserAuthentication.lblTransactionNo.Text, sl.insertRetirementAppResponse.transactionNo)
    '                    authentication = "SET002"

    '                    Dim addtlEmployerMsg As String = "WE ARE AWAITING THE CERTIFICATION FROM YOUR EMPLOYER. YOUR RETIREMENT CLAIM APPLICATION WILL EXPIRE IF YOUR EMPLOYER FAILS TO CERTIFY ON OR BEFORE " & CDate(Now).ToString("MMMM dd, yyyy") & "."
    '                    If pnlContact.Visible = False Then addtlEmployerMsg = ""

    '                    authenticationMsg = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR RETIREMENT CLAIM APPLICATION TO SSS. " &
    '                                        IIf(addtlEmployerMsg = "", "", vbNewLine & vbNewLine & addtlEmployerMsg) &
    '                                        vbNewLine & vbNewLine & "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" &
    '                                        vbNewLine & vbNewLine & "TRANSACTION REFERENCE NUMBER: " & _frmUserAuthentication.lblTransactionNo.Text
    '                    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")

    '                    If _frmMainMenu.IsAllowedToPrint() Then
    '                        Dim class1 As New PrintHelper
    '                        class1.prt(class1.prtOnlineRetirement_Receipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, lblAdvancePension.Text.ToUpper, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).bankCode, _frmTechRetirementApplyBank.txtSavings.Text, IIf(separationDate = "", "", CDate(separationDate).ToString("MMMM dd, yyyy")), _frmUserAuthentication.lblTransactionNo.Text, "RETIREMENT CLAIM APPLICATION", "Print"), _frmMainMenu.DefaultPrinterName)
    '                        class1 = Nothing
    '                        _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
    '                    End If
    '                Else
    '                    authentication = "SET002"
    '                    authenticationMsg = sl.insertRetirementAppResponse.returnMessage.ToUpper
    '                    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")
    '                End If
    '            End If
    '        Catch ex As Exception
    '            SharedFunction.ShowWarningMessage("FAILED TO SUBMIT RETIREMENT APPLICATION.")
    '        End Try

    '        sl = Nothing
    '    End If
    'End Sub

    Public Sub submitTechnicalRetirementv2()
        Dim sl As New SalaryLoan.slMobileWS2BeanService
        Try
            'Dim appliedFrom As String = String.Format("{0}{1}{2}", "SSET", kioskID, kioskName)
            If sl.insertRetirementApp(_frmTechRetirementApplyValidation.certificationTag, SSStempFile, _frmTechRetirementEmpHist.determined_doctg, lblAddress.Text, lblEmailAddress.Text, lblLandline.Text, lblMobile.Text, lblAdvancePension.Text, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).brstn, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).acctNumber, _frmTechRetirementEmpHist.separationDate, _frmTechRetirementApplyMineworker.flg_120, _frmTechRetirementApplyMineworker.mp_amt, SharedFunction.AppliedFrom, _frmTechRetirementEmpHist.memberType, _frmTechRetirementEmpHist.employerSSNumber, _frmTechRetirementEmpHist.employerERBR) Then
                If sl.insertRetirementAppResponse.processFlag = "1" Then
                    Dim ip As New insertProcedure
                    Dim expiryDate As String = sl.insertRetirementAppResponse.expirationDate
                    If IsDate(expiryDate) Then expiryDate = CDate(sl.insertRetirementAppResponse.expirationDate).ToString("MMMM dd, yyyy").ToUpper
                    _frmUserAuthentication.getTransacNum()
                    Dim membershipStatusCode2 As String = CHECK_MEMSTATUS_Settings.Substring(0, 1).ToUpper 'membershipStatus.ToUpper.Substring(0, 1).ToUpper
                    If membershipStatusCode2 = "C" Then membershipStatusCode2 = "E"
                    ip.insertOnlineRetirement(_frmTechRetirementApplyValidation.certificationTag, SSStempFile, _frmTechRetirementEmpHist.determined_doctg, lblAddress.Text, lblEmailAddress.Text, lblLandline.Text, lblMobile.Text, lblAdvancePension.Text, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).acctNumber, _frmTechRetirementApplyBank.txtSavings.Text, _frmTechRetirementEmpHist.separationDate, _frmTechRetirementApplyMineworker.flg_120, _frmTechRetirementApplyMineworker.mp_amt, SharedFunction.AppliedFrom, membershipStatusCode2, _frmTechRetirementEmpHist.employerSSNumber, _frmTechRetirementEmpHist.employerERBR, _frmUserAuthentication.lblTransactionNo.Text, sl.insertRetirementAppResponse.transactionNo)

                    'authenticationMsg = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR APPLICATION FOR RETIREMENT. " & vbNewLine & vbNewLine & "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine & "TRANSACTION REFERENCE NUMBER: " & _frmUserAuthentication.lblTransactionNo.Text
                    '_frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")

                    Dim separationDate As String = ""
                    If IsDate(_frmTechRetirementEmpHist.separationDate) Then separationDate = CDate(_frmTechRetirementEmpHist.separationDate).ToString("MMMM dd, yyyy")

                    If _frmMainMenu.IsAllowedToPrint() Then
                        Dim class1 As New PrintHelper
                        class1.prt(class1.prtOnlineRetirement_Receipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, lblAdvancePension.Text.ToUpper, _frmTechRetirementApplyBank.memberBankAccts(_frmTechRetirementApplyBank.cboBank.SelectedIndex - 1).bankCode, _frmTechRetirementApplyBank.txtSavings.Text, separationDate, _frmUserAuthentication.lblTransactionNo.Text, sl.insertRetirementAppResponse.transactionNo, "RETIREMENT CLAIM APPLICATION", "Print"), _frmMainMenu.DefaultPrinterName)
                        class1 = Nothing
                        _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                    End If

                    'Dim addtlEmployerMsg As String = "WE ARE AWAITING THE CERTIFICATION FROM YOUR EMPLOYER." & vbNewLine & "YOUR RETIREMENT CLAIM APPLICATION WILL EXPIRE IF YOUR EMPLOYER FAILS TO CERTIFY ON OR BEFORE " & expiryDate & "."
                    Dim addtlEmployerMsg As String = "WE ARE AWAITING THE CERTIFICATION FROM YOUR EMPLOYER." & vbNewLine & "YOUR RETIREMENT CLAIM APPLICATION WILL EXPIRED IF YOUR EMPLOYER FAILS TO CERTIFY ON OR BEFORE " & expiryDate & "."
                    If pnlEmployer.Visible = False Then addtlEmployerMsg = ""

                    authentication = "SET002"
                    authenticationMsg = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR RETIREMENT CLAIM APPLICATION TO SSS. " &
                                        IIf(addtlEmployerMsg = "", "", vbNewLine & vbNewLine & addtlEmployerMsg) &
                                        vbNewLine & vbNewLine & "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBERS BELOW:" &
                                        vbNewLine & vbNewLine & "VERIFICATION TRANSACTION REFERENCE NUMBER: " & sl.insertRetirementAppResponse.transactionNo &
                                        vbNewLine & "SET TRANSACTION REFERENCE NUMBER: " & _frmUserAuthentication.lblTransactionNo.Text

                    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "SUCCESSFULLY SUBMITTED YOUR RETIREMENT CLAIM APPLICATION", "10028")
                Else
                    authentication = "SET002"
                    authenticationMsg = sl.insertRetirementAppResponse.returnMessage.ToUpper
                    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")
                End If
            End If
        Catch ex As Exception
            authentication = "SET002"
            authenticationMsg = "FAILED TO SUBMIT RETIREMENT APPLICATION." & vbNewLine & vbNewLine & ex.Message
            SharedFunction.ShowWarningMessage(authenticationMsg)
            _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "FAILED TO SUBMIT RETIREMENT APPLICATION." & ex.Message, "10028")
        End Try

        sl = Nothing
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

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        GC.Collect()
        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmTechRetirementApplyBank.TopLevel = False
        _frmTechRetirementApplyBank.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmTechRetirementApplyBank.Dock = DockStyle.Fill
        _frmTechRetirementApplyBank.Show()
    End Sub

    Private Sub chk1_1_CheckedChanged(sender As Object, e As EventArgs) Handles chk1_1.CheckedChanged
        btnNext.Enabled = chk1_1.Checked
    End Sub

End Class