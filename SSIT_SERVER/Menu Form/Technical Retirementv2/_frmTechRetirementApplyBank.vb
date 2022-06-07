
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementApplyBank

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub pbUnmask_Click(sender As Object, e As EventArgs) Handles pbUnmask.Click
        If txtSavings.Text = "" Then Return
        pbUnmask.Visible = False
        Invoke(New Action(AddressOf UnmaskAcctNo))
        System.Threading.Thread.Sleep(3000)
        Invoke(New Action(AddressOf MaskAcctNo))
        pbUnmask.Visible = True
    End Sub

    Private Sub _frmTechRetirementApplyBank_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopulateBankAccts()

            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
            _frmMainMenu.DisposeForm(_frmLoading)

            If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                'For Each ctrl As Control In Panel4.Controls
                '    If TypeOf ctrl Is Label Then
                '        Dim lbl As Label = CType(ctrl, Label)
                '        CType(ctrl, Label).Font = New Font(lbl.Font.Name, lbl.Font.Size - 1, lbl.Font.Style)
                '    End If
                'Next

                For Each ctrl As Control In pnlEmployer.Controls
                    ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 1, ctrl.Font.Style)
                Next

                For Each ctrl As Control In pnlAddress.Controls
                    ctrl.Font = New Font(ctrl.Font.Name, ctrl.Font.Size - 1, ctrl.Font.Style)
                Next

                cboBank.Width = cboBank.Width - 5
                cboEmployerAddress.Width = cboEmployerAddress.Width - 5
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

    Private Sub _frmTechRetirementApplyBank_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Public memberBankAccts As List(Of SalaryLoan.memberBankAcct)

    Public Function getBankAccountListBySSNumber() As Boolean
        Dim bln As Boolean = False

        Dim sl As New SalaryLoan.slBankWorkflowWebService
        If sl.getBankAccountListBySSNumber(SSStempFile) Then
            If sl.getBankAccountListBySSNumberResponse(0).REPLY_CODE = "0" Then
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

                If Not memberBankAccts Is Nothing Then bln = True

                'Else
                '    authentication = "SET002"
                '    authenticationMsg = "MEMBER HAVE NO BANK ACCOUNT REGISTERED."
                '    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")
            End If

            If Not memberBankAccts Is Nothing Then bln = True
            'Else
            '    authentication = "SET002"
            '    authenticationMsg = "MEMBER HAVE NO BANK ACCOUNT REGISTERED."
            '    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")
        End If

        Return bln
    End Function

    Public Sub PopulateBankAccts()
        cboBank.Items.Add("-Select-")
        Dim uaAcct = memberBankAccts.Find(Function(x) x.acctType.Contains("UA"))
        If Not uaAcct Is Nothing Then
            cboBank.Items.Add(String.Format("{0} - {1}", uaAcct.bankCode.Trim, uaAcct.bankDepBank))
            cboBank.SelectedIndex = 1
            cboBank.Enabled = False
        Else
            For Each memberBankAcct As SalaryLoan.memberBankAcct In memberBankAccts
                'cboBank.Items.Add(String.Format("{0} - {1}", memberBankAcct.bankCode.Trim, memberBankAcct.bankDepBank))
                cboBank.Items.Add(String.Format("{0}", memberBankAcct.bankName.Trim))
            Next

            If cboBank.Items.Count = 2 Then
                cboBank.SelectedIndex = 1
                cboBank.Enabled = False
            Else
                cboBank.Enabled = True
                cboBank.SelectedIndex = 0
            End If
        End If
    End Sub

    Public employerAddresses As List(Of MobileWS2BeanService.employerAddress)

    Public Sub PopulateEmployerAddresses()
        cboEmployerAddress.Items.Clear()
        cboEmployerAddress.Items.Add("-Select-")
        Dim sl As New SalaryLoan.slMobileWS2BeanService
        If sl.getMultiEmployerAddress(_frmTechRetirementEmpHist.employerSSNumber) Then
            For Each empAddress As MobileWS2BeanService.employerAddress In sl.getMultiEmployerAddressResponse
                If empAddress.processFlag = "1" Then
                    If employerAddresses Is Nothing Then employerAddresses = New List(Of MobileWS2BeanService.employerAddress)
                    employerAddresses.Add(empAddress)
                    'cboEmployerAddress.Items.Add(String.Format("{0} - {1}", empAddress.erbrn.Trim, empAddress.address.Trim))
                    cboEmployerAddress.Items.Add(String.Format("{0}", empAddress.address.Trim))
                End If
            Next
        End If
        If cboEmployerAddress.Items.Count = 2 Then
            cboEmployerAddress.SelectedIndex = 1
            cboEmployerAddress.Enabled = False
        Else
            cboEmployerAddress.Enabled = True
            cboEmployerAddress.SelectedIndex = 0
        End If
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

    Private Sub cboBank_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboBank.SelectedIndexChanged
        Try
            If cboBank.SelectedIndex = 0 Then
                txtSavings.Text = ""
            Else
                MaskAcctNo()
            End If
        Catch ex As Exception
            txtSavings.Text = ""
        End Try
    End Sub

    Private Sub UnmaskAcctNo()
        Dim acctNo As String = memberBankAccts(cboBank.SelectedIndex - 1).acctNumber
        txtSavings.Text = acctNo
        Application.DoEvents()
    End Sub

    Private Sub MaskAcctNo()
        Dim acctNo As String = memberBankAccts(cboBank.SelectedIndex - 1).acctNumber
        txtSavings.Text = String.Format("{0}{1}", Microsoft.VisualBasic.Left("xxxxxxxxxxxxxxxxxxxx", acctNo.Length - 4), Microsoft.VisualBasic.Right(acctNo, 4))
        Application.DoEvents()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmTechRetirementApplyValidation.formType = 1
        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmTechRetirementApplyValidation.TopLevel = False
        _frmTechRetirementApplyValidation.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmTechRetirementApplyValidation.Dock = DockStyle.Fill
        _frmTechRetirementApplyValidation.Show()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not chkYES.Checked And Not chkNO.Checked Then
            SharedFunction.ShowWarningMessage("YOU MUST CHOOSE FROM YES OR NO ON 18-MOS. ADVANCE PENSION.")
        Else
            If pnlEmployer.Visible = True And cboEmployerAddress.SelectedIndex = 0 Then
                SharedFunction.ShowWarningMessage("EMPLOYER BRANCH LOCATION IS NOT VALID.")
            Else
                Try
                    Dim sbLog As New System.Text.StringBuilder
                    sbLog.AppendLine("=== START " & Now.ToString() & " ===")
                    sbLog.AppendLine("crn: " & HTMLDataExtractor.getCRN(_frmWebBrowser.WebBrowser1))
                    sbLog.AppendLine("checkmemstatus: " & CHECK_MEMSTATUS_Settings)
                    sbLog.AppendLine("flg_60: " & _frmTechRetirementEmpHist.flg_60)
                    sbLog.AppendLine("type_of_retirement: " & _frmTechRetirementEmpHist.type_of_retirement)
                    sbLog.AppendLine("determined_doctg: " & _frmTechRetirementEmpHist.determined_doctg)
                    sbLog.AppendLine("mp_amt: " & _frmTechRetirementApplyMineworker.mp_amt)
                    sbLog.AppendLine("flg_120: " & _frmTechRetirementApplyMineworker.flg_120)
                    sbLog.AppendLine("status_flag: " & _frmTechRetirementApplyValidation.status_flag)
                    sbLog.AppendLine("avail_18mos_flg: " & _frmTechRetirementApplyValidation.avail_18mos_flg)
                    sbLog.AppendLine("flg_1k: " & _frmTechRetirementApplyMineworker.flg_1k)
                    sbLog.AppendLine("no_of_months_flg: " & _frmTechRetirementApplyValidation.no_of_months_flg)

                    ''Dim sw As New System.IO.StreamWriter("D:\WORK\SSS\onlineRetirement.txt", True)
                    'Dim sw As New System.IO.StreamWriter(Application.StartupPath & "\onlineRetirement.txt", True)
                    'sw.WriteLine(sbLog.ToString)
                    'sw.Close()
                    'sw.Dispose()
                Catch ex As Exception

                End Try

                If Panel15.Visible = True And chkYES.Checked Then
                    SharedFunction.ShowInfoMessage("PLEASE BE INFORMED THAT THE TOTAL AMOUNT OF ADVANCE PENSION SHALL BE DISCOUNTED AT A PREFERENTIAL RATE OF INTEREST TO BE DETERMINED BY SSS AND TO BE DEDUCTED FROM THE FIRST PAYMENT OF YOUR RETIREMENT BENEFIT.")
                End If

                If cboBank.SelectedIndex = 0 Then
                    SharedFunction.ShowWarningMessage("BANK IS REQUIRED.")
                    Return
                End If

                _frmTechRetirementConfirm.lblBank.Text = cboBank.Text
                    _frmTechRetirementConfirm.lblAcctNo.Text = txtSavings.Text
                    _frmTechRetirementConfirm.lblAdvancePension.Text = IIf(chkYES.Checked, "Yes", "No")

                    _frmTechRetirementConfirm.lblAddress.Text = lblAddress.Text
                    _frmTechRetirementConfirm.lblEmailAddress.Text = lblEmailAddress.Text
                    _frmTechRetirementConfirm.lblMobile.Text = lblMobile.Text
                    _frmTechRetirementConfirm.lblLandline.Text = lblLandline.Text

                    _frmTechRetirementConfirm.lblSeparationDate.Text = lblSeparationDate.Text
                    _frmTechRetirementConfirm.lblEmployerId.Text = lblEmployerId.Text
                    _frmTechRetirementConfirm.lblEmployer.Text = lblEmployer.Text
                    _frmTechRetirementConfirm.lblEmployerAddress.Text = lblEmployerAddress.Text

                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    _frmTechRetirementConfirm.TopLevel = False
                    _frmTechRetirementConfirm.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmTechRetirementConfirm.Dock = DockStyle.Fill
                    _frmTechRetirementConfirm.Show()
                End If

            End If
    End Sub

    Private Sub cboEmployerAddress_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEmployerAddress.SelectedIndexChanged
        Try
            If cboEmployerAddress.SelectedIndex = 0 Then
                lblEmployerAddress.Text = ""
            Else
                lblEmployerAddress.Text = cboEmployerAddress.Text
            End If
            EmployerWithErBrn()
        Catch ex As Exception
            lblEmployerAddress.Text = ""
        End Try
    End Sub

    Private Sub EmployerWithErBrn()
        Dim cboErBrn As String = ""
        If Not employerAddresses Is Nothing Then
            If employerAddresses(cboEmployerAddress.SelectedIndex - 1).erbrn.Trim.Length = 3 Then
                cboErBrn = employerAddresses(cboEmployerAddress.SelectedIndex - 1).erbrn.Trim
            Else
                cboErBrn = employerAddresses(cboEmployerAddress.SelectedIndex - 1).erbrn.Trim.PadLeft(3, "0")
            End If
        End If
        lblEmployerId.Text = String.Format("{0} - {1}", _frmTechRetirementEmpHist.employerSSNumber, cboErBrn)
    End Sub

End Class