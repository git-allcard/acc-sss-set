
Imports Oracle.DataAccess.Client

Public Class _frmPRNApplication

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails

    Dim decFlexiFundMinimumAmount As Decimal = 200

    Private Sub _frmPRNApplication_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            PopulateYears(cboYear_From)
            PopulateYears(cboYear_To)

            cboMonth_From.SelectedIndex = 0
            cboMonth_To.SelectedIndex = 0

            If cboYear_From.Items.Count > 0 Then cboYear_From.SelectedIndex = cboYear_From.FindString(Now.Year)
            If cboYear_To.Items.Count > 0 Then cboYear_To.SelectedIndex = cboYear_To.FindString(Now.Year)

            Dim ErrMsg As String = ""

            'revised during production because employee static information is different Aug19
            If Not SharedFunction.Get_getAllowedMemberTypeListPRN(cboMembershipType, printF.GetCoverageStatus_v2(_frmWebBrowser.WebBrowser1), ErrMsg) Then
                'If Not SharedFunction.Get_getAllowedMemberTypeListPRN(cboMembershipType, printF.GetCoverageStatusv3(_frmWebBrowser.WebBrowser1), ErrMsg) Then
                SharedFunction.ShowErrorMessage(String.Format("getAllowedMemberTypeListPRN(): Failed to get ws list for '{0}'", printF.GetCoverageStatus_v2(_frmWebBrowser.WebBrowser1)))
                cboMembershipType.Items.Clear()
                cboMembershipType.Items.Add("-POPULATION FAILED-")
                cboMembershipType.SelectedIndex = 0
                ErrorHandler(ErrMsg)
                btnSubmit.Enabled = False
            Else
                If cboMembershipType.Items.Count > 0 Then
                    'cboMembershipType.SelectedIndex = 0
                    cboMembershipType.SelectedIndex = cboMembershipType.FindString(MemberType_Mapping(printF.GetCoverageStatus_v2(_frmWebBrowser.WebBrowser1)))
                    If cboMembershipType.SelectedIndex = -1 Then cboMembershipType.SelectedIndex = 0
                End If

                If Not SharedFunction.Get_getContributionListPRN(cboContribution, ErrMsg) Then
                    cboContribution.Items.Clear()
                    cboContribution.Items.Add("-POPULATION FAILED-")
                    cboContribution.SelectedIndex = 0
                    ErrorHandler(ErrMsg)
                    btnSubmit.Enabled = False
                Else
                    btnSubmit.Enabled = True
                    If cboContribution.Items.Count > 0 Then cboContribution.SelectedIndex = 0

                    If PRN_MemberWithPRN Then
                        cboMembershipType.SelectedIndex = cboMembershipType.FindString(SharedFunction.Get_PRN_MembershipTypeDesc_ByPRN(PRN))
                        cboMonth_From.SelectedIndex = CShort(PRN_Period_From.Substring(0, 2))
                        cboYear_From.SelectedIndex = cboYear_From.FindString(PRN_Period_From.Substring(2, 4))
                        cboMonth_To.SelectedIndex = CShort(PRN_Period_To.Substring(0, 2))
                        cboYear_To.SelectedIndex = cboYear_From.FindString(PRN_Period_To.Substring(2, 4))
                        cboContribution.SelectedIndex = cboContribution.FindStringExact(PRN_MonthlyContribution)
                        txtFlexiFundAmount.Text = CDec(PRN_FlexiFund).ToString("N2")
                        txtTotalAmount.Text = CDec(PRN_TotalAmount).ToString("N2")
                        btnCancel.Visible = False
                        Label14.Text = "Edit SOA"
                    Else
                        Label14.Text = "Payment Reference Number (PRN)"
                        btnCancel.Visible = True
                    End If
                End If
            End If

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

    Private Sub _frmPRNApplication_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Function MemberType_Mapping(ByVal memberType As String) As String
        Select Case memberType.ToString.Trim
            Case "SELF-EMPLOYED", "SELF EMPLOYED"
                Return "Self-Employed"
            Case "COVERED-EMPLOYEE", "COVERED EMPLOYEE"
                Return "Employed"
            Case "VOLUNTARY", "VOLUNTARY-PAYING", "VOLUNTARY PAYING", "VOLUNTARY MEMBER"
                Return "Voluntary Paying"
            Case "HOUSEHOLD-HELP", "HOUSEHOLD HELP"
                Return "Household help"
            Case "OFW", "OVERSEAS CONTRACT WORKER"
                Return "Overseas Contract Worker"
        End Select
    End Function


    Private Sub PopulateYears(ByRef cbo As ComboBox)
        Dim intCurrentYear As Integer = Now.Year
        For i As Integer = (intCurrentYear - 1) To (intCurrentYear + 4)
            cbo.Items.Add(i.ToString)
        Next
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

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        lblMembershipTypeAsterisk.Visible = False
        lblFromAsterisk.Visible = False
        lblToAsterisk.Visible = False
        lblContributionAsterisk.Visible = False

        Dim sb As New System.Text.StringBuilder

        If cboMembershipType.SelectedIndex = 0 Then
            sb.Append("Please select Membership Type" & vbNewLine)
            lblMembershipTypeAsterisk.Visible = True
        End If

        If cboMonth_From.SelectedIndex = 0 Then
            sb.Append("Please re-check Applicable Period/ From" & vbNewLine)
            lblFromAsterisk.Visible = True
        End If

        If cboYear_From.SelectedIndex = 0 Then
            sb.Append("Please re-check Applicable Period/ From" & vbNewLine)
            lblFromAsterisk.Visible = True
        End If

        If cboMonth_To.SelectedIndex = 0 Then
            sb.Append("Please re-check Applicable Period/ To" & vbNewLine)
            lblToAsterisk.Visible = True
        End If

        If cboYear_To.SelectedIndex = 0 Then
            sb.Append("Please re-check Applicable Period/ To" & vbNewLine)
            lblToAsterisk.Visible = True
        End If

        Try
            Dim dtFrom As Date = String.Format("{0}/01/{1}", cboMonth_From.SelectedIndex, cboYear_From.Text)
            Dim dtTo As Date = String.Format("{0}/01/{1}", cboMonth_To.SelectedIndex, cboYear_To.Text)
            If dtFrom > dtTo Then
                sb.Append("Applicable Period/ From must be equal or earlier than Application Period/ To" & vbNewLine)
                lblFromAsterisk.Visible = True
            End If
        Catch ex As Exception
        End Try

        If cboContribution.SelectedIndex = 0 Then
            sb.Append("Please select contribution" & vbNewLine)
            lblContributionAsterisk.Visible = True
        End If

        Select Case cboMembershipType.Text.Trim
            Case "OFW", "Overseas Contract Worker"
                If cboContribution.Text = 2600 Then '"1760" Then
                    If CDec(txtFlexiFundAmount.Text) = 0 Then
                    ElseIf CDec(txtFlexiFundAmount.Text) > 0 And CDec(txtFlexiFundAmount.Text) < decFlexiFundMinimumAmount Then
                        txtFlexiFundAmount.Text = "0.00"
                        'sb.Append("Please enter flexi-fund amount not less than P200.00" & vbNewLine)
                        sb.Append(InvalidFlexiFundAmountMsg() & vbNewLine)
                    End If
                End If
        End Select

        'billion is not allowed. revised by edel on 08/15/2019 per advised by Sir Rommel
        If CDec(txtTotalAmount.Text) >= 1000000000 Then
            sb.Append("The Flexi-Fund amount exceeded the maximum limit for this facility." & vbNewLine)
        End If

        If sb.ToString <> "" Then
            SharedFunction.ShowErrorMessage(sb.ToString)
            Return
        End If

        ComputeTotalAmount()

        _frmPRNApplication_Confirm.PopulateSessionValues()

        RedirectTo_withPRN(_frmPRNApplication_Confirm)
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        If SharedFunction.ShowMessage("Are you sure you want to cancel request?") = Windows.Forms.DialogResult.Yes Then
            'RedirectTo_withPRN(_frmPRN_Generate)
            '_frmMainMenu.btnACOP.PerformClick()
            If cboMembershipType.Items.Count > 0 Then cboMembershipType.SelectedIndex = 0
            txtFlexiFundAmount.Text = "0.00"
            txtTotalAmount.Text = "0.00"
            cboMonth_From.SelectedIndex = 0
            cboMonth_To.SelectedIndex = 0
            If cboContribution.Items.Count > 0 Then cboContribution.SelectedIndex = 0
            If cboYear_From.Items.Count > 0 Then cboYear_From.SelectedIndex = cboYear_From.FindString(Now.Year)
            If cboYear_To.Items.Count > 0 Then cboYear_To.SelectedIndex = cboYear_To.FindString(Now.Year)
        End If
    End Sub

    Private Sub txtTotalAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotalAmount.KeyPress
        If (Char.IsNumber(e.KeyChar)) Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub cboMembershipType_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMembershipType.SelectedIndexChanged, cboContribution.SelectedIndexChanged
        FlexiFundControls()
    End Sub

    Private Sub FlexiFundControls()
        Try
            Select Case cboMembershipType.Text.Trim
                Case "OFW", "Overseas Contract Worker"
                    'If cboContribution.Text >= ContributionListPRN_MaxValue Then '"1760" Then
                    If cboContribution.Text >= 2600 Then
                        'lblOFWMsg.Visible = True
                        Label25.Enabled = True
                        txtFlexiFundAmount.Enabled = True
                        LinkLabel1.Visible = True
                        If CDec(txtFlexiFundAmount.Text) = 0 Then txtFlexiFundAmount.Text = decFlexiFundMinimumAmount.ToString("N2")
                    Else
                        GoTo noflexifund
                    End If
                Case Else
noflexifund:
                    'lblOFWMsg.Visible = False
                    txtFlexiFundAmount.Enabled = False
                    Label25.Enabled = False
                    LinkLabel1.Visible = False
                    txtFlexiFundAmount.Text = "0.00"
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ComputeTotalAmount()
        Try
            Dim dtFrom As Date = String.Format("{0}/01/{1}", cboMonth_From.SelectedIndex, cboYear_From.Text)
            Dim dtTo As Date = String.Format("{0}/01/{1}", cboMonth_To.SelectedIndex, cboYear_To.Text)
            Dim intMonthDifference As Integer = DateDiff(DateInterval.Month, dtFrom, dtTo) + 1

            Dim decContribution As Decimal = cboContribution.Text
            Dim decFlexiFund As Decimal = 0
            If txtFlexiFundAmount.Text <> "" Then decFlexiFund = txtFlexiFundAmount.Text

            txtTotalAmount.Text = CDec(CDec(decContribution * intMonthDifference) + decFlexiFund).ToString("N2")
        Catch ex As Exception
            txtTotalAmount.Text = "0.00"
        End Try
    End Sub

    Private Sub cboMonth_From_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMonth_From.SelectedIndexChanged, cboMonth_To.SelectedIndexChanged, cboYear_From.SelectedIndexChanged, cboYear_To.SelectedIndexChanged, cboContribution.SelectedIndexChanged, cboMembershipType.SelectedIndexChanged
        Try
            ComputeTotalAmount()
        Catch ex As Exception
        End Try
    End Sub

    Private Function InvalidFlexiFundAmountMsg() As String
        Return "Flexi-fund minimum amount is Php" & decFlexiFundMinimumAmount.ToString("N2") & " per transaction"
    End Function

    Private Sub UsrfrmPageHeader1_Load(sender As Object, e As EventArgs) Handles UsrfrmPageHeader1.Load

    End Sub

    Private Sub txtFlexiFundAmount_Leave(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtFlexiFundAmount.Leave
        If txtFlexiFundAmount.Text = "" Then
            txtFlexiFundAmount.Text = "0.00"
        ElseIf CDec(txtFlexiFundAmount.Text) = 0 Then
        ElseIf CDec(txtFlexiFundAmount.Text) > 0 And CDec(txtFlexiFundAmount.Text) < decFlexiFundMinimumAmount Then
            SharedFunction.ShowMessage(InvalidFlexiFundAmountMsg, MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtFlexiFundAmount.Text = decFlexiFundMinimumAmount.ToString("N2")
            txtFlexiFundAmount.SelectAll()
            txtFlexiFundAmount.Focus()
        Else
            txtFlexiFundAmount.Text = CDec(txtFlexiFundAmount.Text).ToString("N2")
        End If
        ComputeTotalAmount()
    End Sub

    Private Sub LinkLabel1_LinkClicked(ByVal sender As System.Object, ByVal e As System.Windows.Forms.LinkLabelLinkClickedEventArgs) Handles LinkLabel1.LinkClicked
        txtFlexiFundAmount.Text = ""
        txtFlexiFundAmount.SelectAll()
        txtFlexiFundAmount.Focus()
        ComputeTotalAmount()
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

    Private Sub txtFlexiFundAmount_KeyPress(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFlexiFundAmount.KeyPress
        Select Case e.KeyChar
            Case ChrW(Keys.Back), "."c
            Case Else
                If Char.IsNumber(e.KeyChar) Then
                Else
                    e.Handled = True
                End If
        End Select
    End Sub

    Private Sub cboContribution_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboContribution.Click
        _frmPRNContributions2.GetContributionsFromWS(cboContribution)
        _frmPRNContributions2.ShowDialog()
        If _frmPRNContributions2.SelectedContribution <> "" Then _
            cboContribution.SelectedIndex = cboContribution.FindStringExact(_frmPRNContributions2.SelectedContribution)
    End Sub

    Private Sub txtFlexiFundAmount_Enter(sender As Object, e As EventArgs) Handles txtFlexiFundAmount.Enter
        _frmMainMenu.ShowVirtualKeyboardWithControlFocus(sender)
    End Sub

End Class