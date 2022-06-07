
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementApplyValidation

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Public status_flag As String = ""
    Public formType As Short = 1

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private pnlEmployerHeight As Integer = 0
    Public avail_18mos_flg As String
    Public certificationTag As String = "0"
    Public no_of_months_flg As Integer

    Private Sub _frmTechRetirementApplyValidation_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
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

    Private Sub _frmTechRetirementApplyValidation_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmTechRetirementApplyMineworker.TopLevel = False
        _frmTechRetirementApplyMineworker.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmTechRetirementApplyMineworker.Dock = DockStyle.Fill
        _frmTechRetirementApplyMineworker.Show()
        '_frmMainMenu.btnRetirement.PerformClick()

        'Select Case formType
        '    Case 1
        '        _frmTechRetirementApplyMineworker.formType = 3
        '        _frmTechRetirementApplyMineworker.ShowForm3()
        '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        '        _frmTechRetirementApplyMineworker.TopLevel = False
        '        _frmTechRetirementApplyMineworker.Parent = _frmMainMenu.splitContainerControl.Panel2
        '        _frmTechRetirementApplyMineworker.Dock = DockStyle.Fill
        '        _frmTechRetirementApplyMineworker.Show()
        '    Case 2
        '        _frmMainMenu.btnInquiry_Click()
        'End Select
    End Sub

    Public Sub ShowForm1()
        btnCancel.Image = Image.FromFile(Application.StartupPath & "\images\previous_back.png")
        btnNext.Image = Image.FromFile(Application.StartupPath & "\images\cancel_x.png")

        Label16.Visible = True
        pnlReject.Visible = True
        pnlQualified.Visible = False
        lblUpdateContactInfo.Visible = True
        'pnlEmployer.Height = pnlEmployer.Height + 120
        'Label15.Text = pnlEmployer.Height
    End Sub

    Public Sub ShowForm2()
        btnCancel.Image = Image.FromFile(Application.StartupPath & "\images\cancel_x.png")
        btnNext.Image = Image.FromFile(Application.StartupPath & "\images\proceed_next.png")

        Label16.Visible = False
        pnlReject.Visible = False
        pnlQualified.Visible = True
        lblUpdateContactInfo.Visible = False
        'pnlEmployer.Height = pnlEmployer.Height + 120
        'Label15.Text = pnlEmployer.Height
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case formType
            Case 1
                If status_flag = "1" Then
                    _frmMainMenu.btnInquiry_Click()
                Else
                    formType = 2
                    employerCert()
                End If
        End Select
    End Sub

    Public Sub employerCert()
        Dim slMobileWS2BeanService As New SalaryLoan.slMobileWS2BeanService
        Dim getContact As New getContactInfo
        If getContact.Exception = "" Then
            If avail_18mos_flg = "0" Then
                _frmTechRetirementApplyBank.Panel5.Visible = True
                _frmTechRetirementConfirm.Label10.Visible = True
                _frmTechRetirementConfirm.Label18.Visible = True
                _frmTechRetirementConfirm.lblAdvancePension.Visible = True
            Else
                _frmTechRetirementApplyBank.Panel5.Visible = False
                _frmTechRetirementConfirm.Label10.Visible = False
                _frmTechRetirementConfirm.Label18.Visible = False
                _frmTechRetirementConfirm.lblAdvancePension.Visible = False
            End If

            Dim isValidateERCerti As Boolean = True

            Select Case CHECK_MEMSTATUS_Settings.ToUpper
                Case "OFW", "VOLUNTARY", "NON-WORKING SPOUSE", "NON WORKING SPOUSE"
                    isValidateERCerti = False
                Case Else
                    If _frmTechRetirementEmpHist.type_of_retirement = "Technical Retirement" Then
                        isValidateERCerti = False
                    Else
                        Dim onlineRetirement As New OnlineRetirement
                        If onlineRetirement.employerCert(SSStempFile, _frmTechRetirementEmpHist.determined_doctg) Then
                            no_of_months_flg = onlineRetirement.memberClaimInformationEntitiesResponse(0).no_of_months_flg

                            If Not SharedFunction.IsDisplayERCertification(no_of_months_flg, _frmTechRetirementEmpHist.type_of_retirement) Then _
                                isValidateERCerti = False
                        End If
                    End If
            End Select

            If isValidateERCerti Then
                certificationTag = "1"

                _frmTechRetirementApplyBank.pnlEmployer.Visible = True
                _frmTechRetirementApplyBank.pnlEmployerSplitter.Visible = True

                _frmTechRetirementConfirm.pnlEmployer.Visible = True
                _frmTechRetirementConfirm.pnlEmployerSplitter.Visible = True

                _frmTechRetirementApplyBank.lblEmployer.Text = _frmTechRetirementEmpHist.employerName
                _frmTechRetirementApplyBank.lblEmployerId.Text = _frmTechRetirementEmpHist.employerSSNumber
                _frmTechRetirementApplyBank.lblSeparationDate.Text = _frmTechRetirementEmpHist.separationDate

                If slMobileWS2BeanService.getMultiEmployerAddress(_frmTechRetirementEmpHist.employerSSNumber) Then
                    _frmTechRetirementApplyBank.PopulateEmployerAddresses()
                End If
            Else
                certificationTag = "0"

                _frmTechRetirementApplyBank.pnlEmployer.Visible = False
                _frmTechRetirementApplyBank.pnlEmployerSplitter.Visible = False

                _frmTechRetirementConfirm.pnlEmployer.Visible = False
                _frmTechRetirementConfirm.pnlEmployerSplitter.Visible = False
            End If

            _frmTechRetirementApplyBank.lblAddress.Text = getContact.MailingAddress
            _frmTechRetirementApplyBank.lblEmailAddress.Text = getContact.Email
            _frmTechRetirementApplyBank.lblMobile.Text = getContact.MobileNos
            _frmTechRetirementApplyBank.lblLandline.Text = getContact.TelephoneNos

            If _frmTechRetirementApplyBank.getBankAccountListBySSNumber() Then
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmTechRetirementApplyBank.TopLevel = False
                _frmTechRetirementApplyBank.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmTechRetirementApplyBank.Dock = DockStyle.Fill
                _frmTechRetirementApplyBank.Show()
            Else
                authentication = "SET002"
                authenticationMsg = "PLEASE BE INFORMED THAT YOU DO NOT HAVE AN EXISTING SAVINGS ACCOUNT NUMBER IN YOUR RECORD."
                _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "NO SAVINGS ACCOUNT", "10028")
            End If
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

End Class