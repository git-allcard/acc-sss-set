
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementApplyMineworker

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Public formType As Short = 1

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private pnlEmployerHeight As Integer = 0
    Public mp_amt As String = ""
    Public flg_120 As String = ""
    Public flg_1k As String = ""

    Private Sub _frmTechRetirementApplyMineworker_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub _frmTechRetirementApplyMineworker_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Select Case formType
            Case 1
                GC.Collect()
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmTechRetirementEmpHist.TopLevel = False
                _frmTechRetirementEmpHist.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmTechRetirementEmpHist.Dock = DockStyle.Fill
                _frmTechRetirementEmpHist.Show()
                '_frmMainMenu.btnRetirement.PerformClick()
            Case 2
                If chkMineworkerYes.Checked Then
                    formType = 1
                    ShowForm1()
                End If
            Case 3
                If chkMineworkerNo.Checked Then
                    formType = 2
                    ShowForm1()
                End If
            Case 4
                If chkLegitimateYes.Checked Then
                    formType = 3
                    ShowForm3()
                End If
        End Select
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case formType
            Case 1
                If Not chkMineworkerYes.Checked And Not chkMineworkerNo.Checked Then
                    SharedFunction.ShowWarningMessage("YOU MUST CHOOSE FROM YES OR NO.")
                Else
                    If chkMineworkerYes.Checked Then
                        formType = 2
                        Label10.Text = "Please be informed that you will be required to submit documentary requirements. Hence, the receipt of retirement claim application for underground/ surface mineworker or as a racehorse jockey shall be handled at any SSS Branch near you."
                        ShowForm2()
                    Else
                        formType = 3
                        ShowForm3()
                    End If
                End If
            Case 2

                If chkMineworkerYes.Checked Then
                    'Label10.Text = "Please be informed that you will be required to submit documentary requirements. Hence, the receipt of retirement claim application for underground/ surface mineworker or as a racehorse jockey shall be handled at any SSS Branch near you."
                    'ShowForm2()
                    _frmMainMenu.btnInquiry_Click()
                Else
                        formType = 3
                        ShowForm3()
                    End If

            Case 3
                If Not chkLegitimateYes.Checked And Not chkLegitimateNo.Checked Then
                    SharedFunction.ShowWarningMessage("YOU MUST CHOOSE FROM YES OR NO.")
                Else
                    btnCancel.Enabled = False
                    btnNext.Enabled = False
                    If chkLegitimateYes.Checked Then
                        formType = 4
                        Label10.Text = "Please be informed that you will be required to submit documentary requirements. Hence, the receipt of retirement claim application shall be handled at any SSS Branch near you."
                        ShowForm2()
                    Else
                        GC.Collect()

                        Dim msg1 As String = "Please be informed that the effectivity of your retirement is on @date" & vbNewLine & vbNewLine & "@amount1" & vbNewLine & vbNewLine & "Furthermore, all outstanding loan balances as of your date of retirement, if any, shall be deducted from the proceeds of your retirement benefit." & vbNewLine & vbNewLine & vbNewLine & vbNewLine & "Would you like to proceed with your application for Retirement?"
                        Dim msg2 As String = "Please be informed that you have less than 120 posted monthly contributions prior to the semester of your retirement, which is on @date Hence, you are qualified for a one-time payment of lump-sum benefit with an estimated amount of @amount1" & vbNewLine & vbNewLine & "You may cancel this transaction if you wish to continue paying contributions to qualify for retirement pension, or proceed if you wish to apply for your lump-sum benefit. Should you opt for lump sum, no other benefit is due thereafter, except for funeral grant.  Furthermore, all outstanding loan balances as of your date of retirement, if any, shall be deducted from the proceeds of your lump-sum benefit." & vbNewLine & vbNewLine & vbNewLine & vbNewLine & "Would you like to proceed with your application for Retirement?"

                        Dim strQualifiedMsg As String = "" ' _frmTechRetirementApplyValidation.lblQualified.Text
                        ' including additional @amount1 benefit
                        If flg_120 = "0" Then
                            strQualifiedMsg = msg1
                            strQualifiedMsg = strQualifiedMsg.Replace("@date", _frmTechRetirementEmpHist.determined_doctg)
                            If flg_1k = "0" Then
                                strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Estimated monthly retirement pension benefit, including additional Php 1,000.00 benefit, is Php " & CDec(mp_amt).ToString("N2") & ".")
                            Else
                                strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Estimated retirement pension benefit is Php " & CDec(mp_amt).ToString("N2") & ".")
                            End If
                        ElseIf flg_120 = "1" Then
                            strQualifiedMsg = msg2
                            strQualifiedMsg = strQualifiedMsg.Replace("@date", _frmTechRetirementEmpHist.determined_doctg)
                            strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Php " & CDec(mp_amt).ToString("N2") & ".")
                        End If


                        'strQualifiedMsg = strQualifiedMsg.Replace("@amount2 ", CDec(onlineRetirement.memberClaimInformationEntitiesResponse(0).loan_balance).ToString("N2"))
                        _frmTechRetirementApplyValidation.lblQualified.Text = strQualifiedMsg
                        _frmTechRetirementApplyValidation.ShowForm2()

                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmTechRetirementApplyValidation.TopLevel = False
                        _frmTechRetirementApplyValidation.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmTechRetirementApplyValidation.Dock = DockStyle.Fill
                        _frmTechRetirementApplyValidation.Show()



                        'GC.Collect()
                        'Dim onlineRetirement As New OnlineRetirement
                        'If onlineRetirement.checkElig_OnlineRt(SSStempFile, _frmTechRetirementEmpHist.determined_doctg) Then
                        '    mp_amt = onlineRetirement.memberClaimInformationEntitiesResponse(0).mp_amt
                        '    flg_120 = onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_120
                        '    _frmTechRetirementApplyValidation.status_flag = onlineRetirement.memberClaimInformationEntitiesResponse(0).status_flag
                        '    If onlineRetirement.memberClaimInformationEntitiesResponse(0).status_flag = "1" Then
                        '        Dim sbReasons As New System.Text.StringBuilder
                        '        For Each reason As String In onlineRetirement.memberClaimInformationEntitiesResponse(0).msg_List
                        '            If reason.Trim <> "" Then
                        '                sbReasons.Append(" • " & reason & vbNewLine)
                        '            End If
                        '        Next
                        '        _frmTechRetirementApplyValidation.lblReason.Text = sbReasons.ToString
                        '        _frmTechRetirementApplyValidation.ShowForm1()

                        '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        '        _frmTechRetirementApplyValidation.TopLevel = False
                        '        _frmTechRetirementApplyValidation.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '        _frmTechRetirementApplyValidation.Dock = DockStyle.Fill
                        '        _frmTechRetirementApplyValidation.Show()
                        '    Else
                        '        _frmTechRetirementApplyValidation.avail_18mos_flg = onlineRetirement.memberClaimInformationEntitiesResponse(0).avail_18mos_flg

                        '        Dim msg1 As String = "Please be informed that the effectivity of your retirement is on @date" & vbNewLine & vbNewLine & "@amount1" & vbNewLine & vbNewLine & "Furthermore, all outstanding loan balances as of your date of retirement, if any, shall be deducted from the proceeds of your retirement benefit." & vbNewLine & vbNewLine & vbNewLine & vbNewLine & "Would you like to proceed with your application for Retirement?"
                        '        Dim msg2 As String = "Please be informed that you have less than 120 posted monthly contributions prior to the semester of your retirement, which is on @date Hence, you are qualified for a one-time payment of lump-sum benefit with an estimated amount of @amount." & vbNewLine & vbNewLine & "You may cancel this transaction if you wish to continue paying contributions to qualify for retirement pension, or proceed if you wish to apply for your lump-sum benefit. Should you opt for lump sum, no other benefit is due thereafter, except for funeral grant.  Furthermore, all outstanding loan balances as of your date of retirement, if any, shall be deducted from the proceeds of your lump-sum benefit." & vbNewLine & vbNewLine & vbNewLine & vbNewLine & "Would you like to proceed with your application for Retirement?"

                        '        Dim strQualifiedMsg As String = "" ' _frmTechRetirementApplyValidation.lblQualified.Text
                        '        ' including additional @amount1 benefit
                        '        If flg_120 = "0" Then
                        '            strQualifiedMsg = msg1
                        '            strQualifiedMsg = strQualifiedMsg.Replace("@date", _frmTechRetirementEmpHist.determined_doctg)
                        '            If onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_1k = "0" Then
                        '                strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Estimated monthly retirement pension benefit, including additional Php 1,000.00 benefit, is Php " & CDec(mp_amt).ToString("N2") & ".")
                        '            Else
                        '                strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Estimated retirement pension benefit is Php " & CDec(mp_amt).ToString("N2") & ".")
                        '            End If
                        '        ElseIf flg_120 = "1" Then
                        '            strQualifiedMsg = msg2
                        '            strQualifiedMsg = strQualifiedMsg.Replace("@date", _frmTechRetirementEmpHist.determined_doctg)
                        '            strQualifiedMsg = strQualifiedMsg.Replace("@amount1", "Php " & CDec(mp_amt).ToString("N2") & ".")
                        '        End If


                        '        'strQualifiedMsg = strQualifiedMsg.Replace("@amount2 ", CDec(onlineRetirement.memberClaimInformationEntitiesResponse(0).loan_balance).ToString("N2"))
                        '        _frmTechRetirementApplyValidation.lblQualified.Text = strQualifiedMsg
                        '        _frmTechRetirementApplyValidation.ShowForm2()

                        '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        '        _frmTechRetirementApplyValidation.TopLevel = False
                        '        _frmTechRetirementApplyValidation.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '        _frmTechRetirementApplyValidation.Dock = DockStyle.Fill
                        '        _frmTechRetirementApplyValidation.Show()
                        '    End If
                        'End If
                    End If
                    btnCancel.Enabled = True
                    btnNext.Enabled = True
                End If
            Case 4
                _frmMainMenu.btnInquiry_Click()
        End Select
    End Sub

    Public Sub ShowForm1()



        btnCancel.Image = Image.FromFile(Application.StartupPath & "\images\cancel_x.png")
        btnNext.Image = Image.FromFile(Application.StartupPath & "\images\proceed_next.png")

        pnlRequiredDoumentary.Visible = False
        pnlNote.Visible = pnlRequiredDoumentary.Visible
        pnlMineworker.Visible = True
        pnlLegitimate.Visible = False
        pnlEmployer.Height = pnlEmployer.Height + 120
        'Label15.Text = pnlEmployer.Height


    End Sub

    Public Sub ShowForm2()
        btnCancel.Image = Image.FromFile(Application.StartupPath & "\images\previous_back.png")
        btnNext.Image = Image.FromFile(Application.StartupPath & "\images\cancel_x.png")

        pnlRequiredDoumentary.Visible = True
        pnlNote.Visible = pnlRequiredDoumentary.Visible
        pnlMineworker.Visible = False
        pnlLegitimate.Visible = False
        pnlEmployer.Height = pnlEmployer.Height - 120
        'Label15.Text = pnlEmployer.Height
    End Sub

    Public Sub ShowForm3()
        'btnCancel.Enabled = False
        'btnNext.Enabled = False

        btnCancel.Image = Image.FromFile(Application.StartupPath & "\images\cancel_x.png")
        btnNext.Image = Image.FromFile(Application.StartupPath & "\images\proceed_next.png")

        pnlRequiredDoumentary.Visible = False
        pnlNote.Visible = pnlRequiredDoumentary.Visible
        pnlMineworker.Visible = False
        pnlLegitimate.Visible = True
        pnlEmployer.Height = pnlEmployer.Height + 120
        'Label15.Text = pnlEmployer.Height

        'btnCancel.Enabled = True
        'btnNext.Enabled = True
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