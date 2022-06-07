
Public Class _frmPRN_Generate

    Dim xtd As New ExtractedDetails

    Private Sub _frmPRN_Generate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim response() As String = Nothing

            If TableMemberPRNApplication Is Nothing Then
                TableMemberPRNApplication = New DataTable
                'TableMemberPRNApplication.Columns.Add("Seq", GetType(Short))
                TableMemberPRNApplication.Columns.Add("eename", GetType(String))
                TableMemberPRNApplication.Columns.Add("eenum", GetType(String))
                TableMemberPRNApplication.Columns.Add("fapld", GetType(String))
                TableMemberPRNApplication.Columns.Add("iprnum", GetType(String))
                TableMemberPRNApplication.Columns.Add("repcd", GetType(String))
                TableMemberPRNApplication.Columns.Add("repdt", GetType(String))
                TableMemberPRNApplication.Columns.Add("tapld", GetType(String))
                TableMemberPRNApplication.Columns.Add("provamt", GetType(String))
                TableMemberPRNApplication.Columns.Add("ssamt", GetType(String))
                TableMemberPRNApplication.Columns.Add("ecamt", GetType(String))
                TableMemberPRNApplication.Columns.Add("tsamt", GetType(String))
                TableMemberPRNApplication.Columns.Add("dueDate", GetType(String))
                TableMemberPRNApplication.Columns.Add("ApplicablePeriod", GetType(String))
                TableMemberPRNApplication.Columns.Add("MemberType", GetType(String))
                TableMemberPRNApplication.Columns.Add("MonthlyPayment", GetType(String))
                TableMemberPRNApplication.Columns.Add("FlexiFund", GetType(String))
                TableMemberPRNApplication.Columns.Add("fapld_Date", GetType(Date))
                TableMemberPRNApplication.Columns.Add("dueDate_Date", GetType(Date))
                TableMemberPRNApplication.Columns.Add("pdfPath", GetType(String))
            Else
                TableMemberPRNApplication.Clear()
            End If

            Dim ErrMsg As String = ""
            SharedFunction.Get_getContributionListPRN(New ComboBox, ErrMsg)

            'If SharedFunction.Get_inquireIndividualSSnum(SSStempFile, lblDateofBirth.Text, response) Then
            If SharedFunction.Get_inquireIndividualSSnum2(SSStempFile, response) Then
                'If SharedFunction.Get_inquireIndividualSSnum("0226879523", "11-08-1990", response) Then
                If response(0) = "0" Then

                    'revised on 07/10/2019 by edel due to their New process RE view multiple prn request
                    'PRN_MemberWithPRN = True
                    'PRN = response(5)
                    'PRN_Period_From = response(3)
                    'PRN_Period_To = response(4)
                    'PRN_TotalAmount = response(7)
                    'PRN_DueDate = response(8)
                    'PRN_PDF = response(6)
                    'PRN_MembershipType = SharedFunction.Get_PRN_MembershipTypeDesc_ByPRN(PRN)

                    'RedirectTo_withPRN(_frmPRNApplication_ConfirmFinal)

                    'PRN_MemberWithPRN = True
                    'PRN = TableMemberPRNApplication.Rows(0)("iprnum")
                    'PRN_Period_From = TableMemberPRNApplication.Rows(0)("fapld")
                    'PRN_Period_To = TableMemberPRNApplication.Rows(0)("tapld")
                    'PRN_TotalAmount = TableMemberPRNApplication.Rows(0)("tsamt")
                    'PRN_DueDate = TableMemberPRNApplication.Rows(0)("dueDate")
                    'PRN_PDF = TableMemberPRNApplication.Rows(0)("pdfPath")
                    'PRN_MembershipType = SharedFunction.Get_PRN_MembershipTypeDesc_ByPRN(PRN)

                    'RedirectTo_withPRN(_frmPRNApplication_ConfirmFinal)

                    ''revised on 07/10/2019 by edel due to their new process RE view multiple prn request
                    RedirectTo_withPRN(_frmPRNApplication_ConfirmFinal_List)


                ElseIf response(0) = "1" Then
                    PRN_MemberWithPRN = False
                    lblMessage.Text = lblMessage.Text.Replace("{0}", UsrfrmPageHeader1.lblSSSNo.Text)
                    btnSubmit.Visible = True
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & response(1) & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                Else
                    'lblMessage.Text = "WEBSERVICE PROCESS FAILED"
                    lblMessage.Text = response(1)
                    lblMessage.ForeColor = Color.OrangeRed

                    GC.Collect()
                    authentication = "PRN01"
                    tagPage = "8"
                    Dim transNum As String = ""
                    Dim transDesc As String = response(1)
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10046" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                    End Using

                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & response(1) & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                    btnSubmit.Visible = False
                End If
            Else
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & response(1) & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                lblMessage.Text = "WEBSERVICE COMMUNICATION FAILED"
                lblMessage.ForeColor = Color.OrangeRed
                btnSubmit.Visible = False
            End If

            _frmMainMenu.BackNextControls(True)
            '_frmMainMenu.Button5.Enabled = True
            '_frmMainMenu.Button6.Enabled = True
            _frmMainMenu.PrintControls(False)
            '_frmMainMenu.Button5.Text = "BACK"
            '_frmMainMenu.Button6.Text = "NEXT"
            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

            _frmLoading.Dispose()
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmPRN_Generate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            btnSubmit.Enabled = True
            Cursor = Cursors.WaitCursor

            RedirectTo_withPRN(_frmPRNApplication)

            btnSubmit.Enabled = True
            Cursor = Cursors.Default
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
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




End Class