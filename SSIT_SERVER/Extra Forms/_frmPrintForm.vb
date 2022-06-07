Public Class _frmPrintForm
    Dim pf As New PrintHelper
    Public lbl As New Label
    Public formPrint As String
    Public btag As String
    Dim printF As New printModule
    Dim clss As New PrintHelper
    Dim fnt As Font
    Dim xtd As New ExtractedDetails
    Dim db As New ConnectionString
    Dim txn As New txnNo
    Dim tempSSSHeader As String

    Private Sub _frmPrintForm_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        xtd.getRawFile()
        getMemDetails()



    End Sub
    Private Sub getMemDetails()
        Try

            Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
            Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
            Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
            Dim fullnameprint As String = lname & " " & fname & " " & mname
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getKioskName As String = kioskName
            getKioskName = getKioskName.Substring(getKioskName.Length - 3)


            lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
            lblBranch.Text = kioskBranch
            lblTerminalNo.Text = kioskID

            Dim fileTYP As Integer = xtd.checkFileType
            If fileTYP = 1 Then

                tempSSSHeader = xtd.getCRN
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                'lblReferenceNo.Text = ""
            ElseIf fileTYP = 2 Then

                tempSSSHeader = SSStempFile
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                'lblReferenceNo.Text = xtd.getCRN
            End If

            'argie101
            Dim getTransNo As String = db.putSingleValue("select max(SEQUENCE_NUM) from SSTRANSREFNO")
            If getTransNo = "" Then
                getTransNo = "0001"
            Else
                ' getTransNo = +1
                getTransNo = getTransNo.PadLeft(4, "0") + 1
                getTransNo = getTransNo.PadLeft(4, "0")
            End If


            'lblReferenceNo.Text = txn.GETTXN(getbranchCoDE, getKioskName, "PF", Date.Today.ToString("yyMMdd"), getTransNo)



            lblMemberName.Text = fullnameprint
            authenticate()

        Catch ex As Exception

        End Try
    End Sub
    Public Sub authenticate()
        lbl.Parent = Panel6
        lbl.Visible = True
        lbl.AutoSize = True
        fnt = New Drawing.Font("Segoe UI", 12, FontStyle.Bold)
        lbl.Font = fnt
        lbl.ForeColor = Color.DimGray
        lbl.Dock = DockStyle.Top
        Dim txt As String


        Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
        Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
        Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
        Dim fullnameprint As String = lname & " " & fname & " " & mname

        Select Case formPrint
            Case "Employee Static Information"
                Dim memberDetails As String = printF.GetSSNumberStatus(_frmWebBrowser.WebBrowser1)
                Dim coverageStatus As String = printF.GetCoverageStatus(_frmWebBrowser.WebBrowser1)
                Dim recordLocation As String = printF.GetRecordLocation(_frmWebBrowser.WebBrowser1)
                Dim bdate As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)

                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = pf.prtMemDetails(fullnameprint, xtd.getCRN, memberDetails, bdate, coverageStatus, recordLocation, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = pf.prtMemDetails(xtd.getFullname, SSStempFile, bdate, memberDetails, coverageStatus, recordLocation, webPageTag, "Form")
                    lbl.Text = txt
                End If

            Case "Actual Premiums"


                Dim dateBirth As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
                Dim dateCoverage As String = printF.GetDateCoverage(_frmWebBrowser.WebBrowser1)
                Dim totalNoContribution As Integer = printF.GetNumbOfContribution(_frmWebBrowser.WebBrowser1)
                Dim totalAmountContribution As String = printF.GetTotalAmountContribution(_frmWebBrowser.WebBrowser1)

                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtActualPremium(fullnameprint, xtd.getCRN, dateBirth, dateCoverage, totalNoContribution, totalAmountContribution, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtActualPremium(xtd.getFullname, SSStempFile, dateBirth, dateCoverage, totalNoContribution, totalAmountContribution, webPageTag, "Form")
                    lbl.Text = txt
                End If



            Case "Benefit Claim"
                '  NO BENEFIT CLAIM AS OF NOW.
            Case "Employment History"
                Dim employerID As String = printF.GetEmployerID(_frmWebBrowser.WebBrowser1)
                Dim employerName As String = printF.GetEmployerName(_frmWebBrowser.WebBrowser1)
                Dim reportingDate As String = printF.GetReportingDate(_frmWebBrowser.WebBrowser1)
                Dim employmentDate As String = printF.getEmploymentDate(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtEmpHistory(fullnameprint, xtd.getCRN, employerID, employerName, reportingDate, employmentDate, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtEmpHistory(xtd.getFullname, SSStempFile, employerID, employerName, reportingDate, employmentDate, webPageTag, "Form")
                    lbl.Text = txt
                End If

            Case "Flixi-Fund Subsidiary Ledger"

            Case "Loan Status"
                Dim loanType01 As String = printF.GetLoanType01(_frmWebBrowser.WebBrowser1)
                Dim AppDate01 As String = printF.GetAppDate01(_frmWebBrowser.WebBrowser1)
                Dim loanAppStat01 As String = printF.GetLoanAppStat01(_frmWebBrowser.WebBrowser1)

                Dim checkDate01 As String = printF.GetCheckDate01(_frmWebBrowser.WebBrowser1)
                Dim loanAmount01 As String = printF.GetLoanAmount01(_frmWebBrowser.WebBrowser1)
                Dim monthlyAmort As String = printF.GetMonthlyAmort(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtLoanStatus(fullnameprint, xtd.getCRN, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtLoanStatus(xtd.getFullname, SSStempFile, loanType01, AppDate01, loanAppStat01, checkDate01, loanAmount01, monthlyAmort, webPageTag, "Form")
                    lbl.Text = txt
                End If

            Case "Credit Loan Payments"
                Dim credLoanType As String = printF.getCredLoanType(_frmWebBrowser.WebBrowser1)
                Dim credCheckDate As String = printF.getCredCheckDate(_frmWebBrowser.WebBrowser1)
                Dim credLoanAmt As String = printF.getCredLoanAmt(_frmWebBrowser.WebBrowser1)

                Dim getCertEmpID As String = printF.getCertEmpID(_frmWebBrowser.WebBrowser1)
                Dim amtDue As String = printF.getCerEmpName(_frmWebBrowser.WebBrowser1)
                Dim amtNotYetDue As String = printF.getLoanMonthLoan(_frmWebBrowser.WebBrowser1)
                Dim totAmtOblig As String = printF.getTotAmtOblig(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtLoanStatusCredited(fullnameprint, xtd.getCRN, credLoanType, credCheckDate, credLoanAmt, getCertEmpID, amtDue, amtNotYetDue, totAmtOblig, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtLoanStatusCredited(xtd.getFullname, SSStempFile, credLoanType, credCheckDate, credLoanAmt, getCertEmpID, amtDue, amtNotYetDue, totAmtOblig, webPageTag, "Form")
                    lbl.Text = txt
                End If

            Case "SSS / UMID Card Information"
                Dim serialNo As String = printF.GetCSJNumber(_frmWebBrowser.WebBrowser1)
                Dim capturedON As String = printF.GetCapturedON(_frmWebBrowser.WebBrowser1)
                Dim generatedON As String = printF.GetGeneratedON(_frmWebBrowser.WebBrowser1)
                Dim GetCapturedDate As String = printF.GetCapturedDate(_frmWebBrowser.WebBrowser1)

                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtSSidClearance(fullnameprint, xtd.getCRN, serialNo, capturedON, GetCapturedDate, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtSSidClearance(xtd.getFullname, SSStempFile, serialNo, capturedON, GetCapturedDate, webPageTag, "Form")
                    lbl.Text = txt
                End If

            Case "Benefits Eligibility"
                Select Case btag
                    Case "Death Benefit Eligibility"
                        GC.Collect()
                        Dim averageMSC As String = printF.getAverageMSC(_frmWebBrowser.WebBrowser1)
                        averageMSC = averageMSC.Trim
                        averageMSC = averageMSC.Replace("&nbsp", "")


                        Dim fileTYP As Integer = xtd.checkFileType

                        If fileTYP = 1 Then
                            txt = clss.prtBenEligDeath(fullnameprint, xtd.getCRN, averageMSC, webPageTag, "Form")
                            lbl.Text = txt
                        ElseIf fileTYP = 2 Then
                            txt = clss.prtBenEligDeath(xtd.getFullname, SSStempFile, averageMSC, webPageTag, "Form")
                            lbl.Text = txt
                        End If


                    Case "Total Disability Eligibility"
                        Dim getDisabilityAMSC As String = printF.getDisabilityAMSC(_frmWebBrowser.WebBrowser1)
                        getDisabilityAMSC = getDisabilityAMSC.Trim
                        getDisabilityAMSC = getDisabilityAMSC.Replace("&nbsp", "")

                        Dim fileTYP As Integer = xtd.checkFileType

                        If fileTYP = 1 Then
                            txt = clss.prtBenEligTotDisable(fullnameprint, xtd.getCRN, getDisabilityAMSC, "Form")
                            lbl.Text = txt
                        ElseIf fileTYP = 2 Then
                            txt = clss.prtBenEligTotDisable(xtd.getFullname, SSStempFile, getDisabilityAMSC, "Form")
                            lbl.Text = txt
                        End If


                End Select
            Case "Loan Eligibility"
                Dim loanMonth As String = printF.getLoanAmount(_frmWebBrowser.WebBrowser1)
                Dim msc As String = printF.getLoanEligibilityMSC(_frmWebBrowser.WebBrowser1)
                Dim loanAmount As String = printF.getLoanAmountEligibility(_frmWebBrowser.WebBrowser1)
                Dim getPrevBalance As String = printF.getPrevBalance(_frmWebBrowser.WebBrowser1)
                Dim getLoanBalance As String = printF.getLoanBalance(_frmWebBrowser.WebBrowser1)
                Dim getLoanProceeds As String = printF.getLoanProceeds(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtLoanEligibilty(fullnameprint, xtd.getCRN, loanMonth, msc, loanAmount, getPrevBalance, getLoanBalance, getLoanProceeds, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtLoanEligibilty(xtd.getFullname, SSStempFile, loanMonth, msc, loanAmount, getPrevBalance, getLoanBalance, getLoanProceeds, webPageTag, "Form")
                    lbl.Text = txt
                End If


            Case "Maternity Benefit"
            Case "Maternity Information"
                Dim dateFiled As String = printF.getDateFiled(_frmWebBrowser.WebBrowser1)
                Dim benefitAppStatus As String = printF.getBenefitAppStatus(_frmWebBrowser.WebBrowser1)
                Dim MaternityDeliveryDate As String = printF.getMaternityDeliveryDate(_frmWebBrowser.WebBrowser1)
                Dim NoOfDays As String = printF.getNoOfDays(_frmWebBrowser.WebBrowser1)
                Dim checkDate As String = printF.getCheckDate(_frmWebBrowser.WebBrowser1)
                Dim amountPaid As String = printF.getAmountPaid(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtMaterniryClaimStatus(fullnameprint, xtd.getCRN, dateFiled, benefitAppStatus, MaternityDeliveryDate, NoOfDays, checkDate, amountPaid, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtMaterniryClaimStatus(xtd.getFullname, SSStempFile, dateFiled, benefitAppStatus, MaternityDeliveryDate, NoOfDays, checkDate, amountPaid, webPageTag, "Form")
                    lbl.Text = txt
                End If


            Case "Sickness Benefit"
                Dim sicknessBenefitAppStatus As String = printF.getSicknessBenefitAppStatus(_frmWebBrowser.WebBrowser1)
                Dim DateConfined As String = printF.getDateConfined(_frmWebBrowser.WebBrowser1)
                Dim sicknessNoOfDays As String = printF.getSicknessNoOfDays(_frmWebBrowser.WebBrowser1)
                Dim sicknessCheckDate As String = printF.getSicknessCheckDate(_frmWebBrowser.WebBrowser1)
                Dim sicknesAmountPaid As String = printF.getSicknesAmountPaid(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtSicknessClaims(fullnameprint, xtd.getCRN, "", sicknessBenefitAppStatus, DateConfined, sicknessNoOfDays, sicknessCheckDate, sicknesAmountPaid, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtSicknessClaims(xtd.getFullname, SSStempFile, "", sicknessBenefitAppStatus, DateConfined, sicknessNoOfDays, sicknessCheckDate, sicknesAmountPaid, webPageTag, "Form")
                    lbl.Text = txt
                End If


            Case "Sickness/Maternity Eligibility"
            Case "Sickness Eligibility"
                Dim confinementStart As String = printF.getConfinementStart(_frmWebBrowser.WebBrowser1)
                Dim confinementEnd As String = printF.getConfinementEnd(_frmWebBrowser.WebBrowser1)
                Dim dailySicknessAllowance As String = printF.getDailySicknessAllowance(_frmWebBrowser.WebBrowser1)
                Dim sicknessAmountBenefit As String = printF.getSicknessAmountBenefit(_frmWebBrowser.WebBrowser1)


                Dim fileTYP As Integer = xtd.checkFileType

                If fileTYP = 1 Then
                    txt = clss.prtSicknessEligiblity(fullnameprint, xtd.getCRN, confinementStart, confinementEnd, "", dailySicknessAllowance, sicknessAmountBenefit, webPageTag, "Form")
                    lbl.Text = txt
                ElseIf fileTYP = 2 Then
                    txt = clss.prtSicknessEligiblity(xtd.getFullname, SSStempFile, confinementStart, confinementEnd, "", dailySicknessAllowance, sicknessAmountBenefit, webPageTag, "Form")
                    lbl.Text = txt
                End If

        End Select
        lblFooter.Text = pf.txtDisclaimer
    End Sub

    Private Sub _frmPrintForm_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class