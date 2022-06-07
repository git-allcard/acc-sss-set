
Public Class _frmPRNApplication_Confirm

    Dim xtd As New ExtractedDetails

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmPRNApplication_Confirm_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopulateSessionValues()

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

    Public Sub PopulateSessionValues()
        lblMembershipType.Text = _frmPRNApplication.cboMembershipType.Text
        lblApplicationPeriod.Text = String.Format("{0} {1} to {2} {3}", _frmPRNApplication.cboMonth_From.Text, _frmPRNApplication.cboYear_From.Text, _frmPRNApplication.cboMonth_To.Text, _frmPRNApplication.cboYear_To.Text)
        lblMonthlyContri.Text = CDec(_frmPRNApplication.cboContribution.Text).ToString("N2")
        lblTotalAmount.Text = CDec(_frmPRNApplication.txtTotalAmount.Text).ToString("N2")
        lblFlexiFund.Text = _frmPRNApplication.txtFlexiFundAmount.Text
        If CDec(_frmPRNApplication.txtFlexiFundAmount.Text) > 0 Then
            Label22.Enabled = True
            Label25.Enabled = True
            lblFlexiFund.Enabled = True
        Else
            Label22.Enabled = False
            Label25.Enabled = False
            lblFlexiFund.Enabled = False
        End If
    End Sub

    Private Sub RedirectTo_withPRN(ByVal frm As Form)
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

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        RedirectTo_withPRN(_frmPRNApplication)
    End Sub

    Private Sub btnProceed_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnProceed.Click
        Dim response() As String = Nothing
        btnProceed.Enabled = False
        Cursor = Cursors.WaitCursor

        Dim tsAmount As Decimal = CDec(lblTotalAmount.Text) - CDec(lblFlexiFund.Text)

        'If SharedFunction.Get_loadIndividualPRNChange(lblMembershipType.Text, SSStempFile, lblTotalAmount.Text.Replace(",", ""), lblFlexiFund.Text.Replace(",", ""), String.Format("{0}{1}", _frmPRNApplication.cboMonth_From.SelectedIndex.ToString.PadLeft(2, "0"), _frmPRNApplication.cboYear_From.Text), String.Format("{0}{1}", _frmPRNApplication.cboMonth_To.SelectedIndex.ToString.PadLeft(2, "0"), _frmPRNApplication.cboYear_To.Text), response) Then
        If SharedFunction.Get_loadIndividualPRNChange(lblMembershipType.Text, SSStempFile, tsAmount.ToString.Replace(",", ""), lblFlexiFund.Text.Replace(",", ""), String.Format("{0}{1}", _frmPRNApplication.cboMonth_From.SelectedIndex.ToString.PadLeft(2, "0"), _frmPRNApplication.cboYear_From.Text), String.Format("{0}{1}", _frmPRNApplication.cboMonth_To.SelectedIndex.ToString.PadLeft(2, "0"), _frmPRNApplication.cboYear_To.Text), response) Then
            If response(0) = "0" Then
                PRN = response(5)
                PRN_Period_From = response(3)
                PRN_Period_To = response(4)
                PRN_TotalAmount = response(7)
                PRN_DueDate = response(8)
                PRN_PDF = response(6)
                PRN_MembershipType = lblMembershipType.Text

                'PRN_MonthlyContribution = _frmPRNApplication.cboContribution.Text

                _frmPRNApplication_ConfirmFinal.PopulateSessionValues()

                If SharedFunction.Get_insertTransactionPRN(PRN, String.Format("{0}-{1}", PRN_Period_From, PRN_Period_To), SSStempFile, lblMonthlyContri.Text.Replace(",", ""), lblFlexiFund.Text.Replace(",", ""), tsAmount.ToString.Replace(",", ""), lblMembershipType.Text, response) Then
                    If response(0) = "1" Then
                        Dim DAL As New DAL_Mssql
                        DAL.INSERT_PRN_TXN(SSStempFile, kioskIP, kioskBranchCD, kioskID, PRN, lblMembershipType.Text, String.Format("{0}-{1}", PRN_Period_From, PRN_Period_To), lblMonthlyContri.Text, lblFlexiFund.Text, lblTotalAmount.Text, PRN_DueDate)
                        DAL.Dispose()
                        DAL = Nothing

                        If _frmMainMenu.IsAllowedToPrint() Then
                            _frmPRNApplication_ConfirmFinal_List.PrintReceipt(UsrfrmPageHeader1.lblMemberName.Text, PRN, lblMembershipType.Text, "0.00", lblFlexiFund.Text, lblTotalAmount.Text, lblApplicationPeriod.Text, PRN_DueDate)
                            _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
                        End If

                        'revised on 07/15/2019 by edel as per new requirement by Ms. Farrah and Sir Rommel
                        'RedirectTo_withPRN(_frmPRNApplication_ConfirmFinal)
                        'RedirectTo_withPRN(_frmPRN_Generate)
                        newGeneratedPRN = PRN
                        _frmMainMenu.PRN_Application()
                    Else
                        ErrorHandler(response(1))
                        'SharedFunction.ShowErrorMessage("insertTransactionPRN(1) process failed")
                        SharedFunction.ShowMessage(response(1).ToUpper, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    End If
                Else
                    ErrorHandler(response(1))
                    'SharedFunction.ShowErrorMessage("insertTransactionPRN(0) process failed")
                    SharedFunction.ShowMessage(response(1).ToUpper, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                End If
            Else
                ErrorHandler(response(1))
                SharedFunction.ShowMessage(response(1).ToUpper, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            ErrorHandler(response(1))
            'SharedFunction.ShowErrorMessage("loadIndividualPRNChange(0) process failed")
            SharedFunction.ShowMessage(response(1).ToUpper, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If

        btnProceed.Enabled = True
        Cursor = Cursors.Default
    End Sub

    Private Sub ErrorHandler(ByVal response As String)
        GC.Collect()
        authentication = "PRN01"
        tagPage = "8"
        Dim transNum As String = ""
        Dim transDesc As String = response
        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & "10046" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
        End Using

        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & response & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
    End Sub


End Class