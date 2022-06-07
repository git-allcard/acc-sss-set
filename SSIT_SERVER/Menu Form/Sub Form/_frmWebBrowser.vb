Imports VB = Microsoft.VisualBasic
Imports Oracle.DataAccess.Client

Public Class _frmWebBrowser
    Dim xtd As New ExtractedDetails
    Dim intLoad As Integer
    'argie101

#Region "Funcation and Declaration"
    Dim printF As New printModule
    Dim at As New auditTrail
    Dim userMovement As String
    Dim getAffectedTable As Integer
    Dim db As New ConnectionString
    Dim getDetailstask As String
    Dim oi As String = "Online Inquiry"
    Dim printZero As Integer = 0


#End Region
    Public empNumber, empName, empsTatus As String
    Public lview, lview2 As New ListView
    Public mouseFocus As String
    'Public getLoanMonth, getLoanBalance As Integer
    'Public getAVGMSC As String
    Public getLoanMonth, getLoanBalance, getAVGMSC, getLoanProceeds, getLoanInt As Integer

    'Public WsResponse_ServiceFee, WsResponse_Totalbalance, WsResponse_Netloan, WsResponse_Appl_st, WsResponse_Monthly_amort, WsResponse_Rejlist, WsResponse_LoanableAmount, WsResponse_Loan_month, WsResponse_MaxLoanableAmount As String

    Public getPrevBalance As Double
    Public getContributions As String
    Dim tagMat As Integer
    Dim navError As Integer
    Dim webBusy As Integer = 0
    Public getLumpSum As String
    Public techTag As Integer = 0
    Dim tempssDate As String
    Dim tagTechRetSal As Integer = 0
    Dim GetTotalAmtObligation As String
    Dim tagLoopTech As Integer = 0
    Dim tagLoopTech2 As Integer = 0
    Dim xs As New MySettings
    Dim lastContribution As String
#Region "LINKS"

    Dim link1 As String = getPermanentURL & "controller?action=benefitClaim"
    Dim link2 As String = getPermanentURL & "controller?action=actualPremiums"
    Dim link3 As String = getPermanentURL & "controller?action=employmentHistory"
    Dim link4 As String = getPermanentURL & "controller?action=flexiFund"
    Dim link5 As String = getPermanentURL & "controller?action=loanStatus"
    Dim link5v1 As String = getPermanentURL & "controller?action=loanDetails&rckdte=031003&rlntyp=S"
    Dim link5v2 As String = getPermanentURL & "controller?action=creditedPayments&paymentParameter=3315543822031003S&saDate=03-26-2014"
    Dim link6 As String = getPermanentURL & "controller?action=Maternity"
    Dim link7 As String = getPermanentURL & "controller?action=memberDetails2"
    Dim link8 As String = getPermanentURL & "controller?action=sicknessHistory"
    Dim link9 As String = getPermanentURL & "controller?action=sssId"
    Dim link10 As String = getPermanentURL & "controller?action=ddrEligibility"
    Dim link10v1 As String = getPermanentURL & "controller?action=DSP"
    Dim link10v2 As String = getPermanentURL & "controller?action=FSS"
    Dim link10v3 As String = getPermanentURL & "controller?action=PRP"
    Dim link10v4 As String = getPermanentURL & "controller?action=TPR"
    Dim link11 As String = getPermanentURL & "controller?action=loanComputation"
    Dim link12 As String = getPermanentURL & "controller?action=sicMatEligibility"
    Dim link13 As String = getPermanentURL & "controller?action=SVS"
    Dim link14 As String = getPermanentURL & "controller?action=SES"
    Dim link15 As String = getPermanentURL & "controller?action=SVM"
    Dim link16 As String = getPermanentURL & "controller?action=checklist"
    Dim link17 As String = getPermanentURL & "controller?action=SSS"
    Dim link18 As String = getPermanentURL & "controller?action=SSE"
    Dim link19 As String = getPermanentURL & "controller?action=SEE"
    Dim link20 As String = getPermanentURL & "controller?action=MAT"
    Dim link21 As String = getPermanentURL & "controller?action=RRL"
    Dim link21v2 As String = getPermanentURL & "controller?action=RRP"
    Dim link22 As String = getPermanentURL & "controller?action=ddrClaimInfo"
    Dim link23 As String = getPermanentURL & "controller?action=paymentHistory"
    Dim link24 As String = getPermanentURL & "controller?action=sssId"
#End Region
    Public Enum BrowserNavigationFlags
        navOpenInNewWindow = 1      ' Open the resource or file in a new window.
        navNoHistory = 2            ' Do not add the resource or file to the history list. The new page replaces the current page in the list.
        navNoReadFromCache = 4      ' Do not read from the disk cache for this navigation.
        navNoWriteToCache = 8       ' Do not write the results of this navigation to the disk cache.
    End Enum
    Public Sub autoClickSubmit()


        Dim doc As HtmlDocument
        Dim elem As HtmlElement


        doc = WebBrowser1.Document
        For Each elem In doc.All
            If elem.GetAttribute("type") = "checkbox" Then
                elem.SetAttribute("checked", "checked")
            End If
        Next
        For Each elem In doc.All
            If elem.GetAttribute("type") = "text" Then
                ' elem.SetAttribute("value", My.Settings.getErNo)
                'elem.SetAttribute("value", "8888888006")
                Dim ssempID As String = _frmMainMenu.tempSalId
                ssempID = ssempID.Replace("-", "")
                If ssempID = "" Or ssempID = Nothing Then
                    ssempID = "8888888006"
                ElseIf ssempID = "00-0000000-0" Then
                    ssempID = "8888888006"
                Else
                    ssempID = ssempID
                End If
                elem.SetAttribute("value", ssempID)
            End If
        Next
        For Each elem In doc.All
            If elem.GetAttribute("type") = "submit" Then
                elem.InvokeMember("click")
            End If
        Next
    End Sub

    Public Sub autoclickTechnical()


        Dim doc As HtmlDocument
        Dim elem As HtmlElement

        doc = WebBrowser1.Document
        'For Each elem In doc.All
        '    If elem.GetAttribute("type") = "checkbox" Then
        '        elem.SetAttribute("checked", "checked")
        '    End If
        'Next
        Dim getTechDate As String = Date.Today.ToString("MM-dd-yyyy")
        For Each elem In doc.All
            If elem.GetAttribute("type") = "text" Then
                ' elem.SetAttribute("value", My.Settings.getErNo)
                elem.SetAttribute("value", getTechDate)
            End If
        Next

        For Each elem In doc.All
            If elem.GetAttribute("type") = "submit" Then
                elem.InvokeMember("click")
            End If
        Next

    End Sub

    Private Sub _frmWebBrowser_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Panel1.Show()
            WebBrowser1.Refresh(WebBrowserRefreshOption.Completely)
            System.Threading.Thread.Sleep(10)
            WebBrowser1.Navigate(getURL)
            ' WebBrowser1.ScriptErrorsSuppressed = True
            ''tempBrowser.Navigate(getPermanentURL & "controller?action=RRL")

            Dim axbrowser As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
            AddHandler axbrowser.NavigateError, AddressOf axbrowser_NavigateError

            'AddHandler tempBrowser.DocumentCompleted, AddressOf tempBrowser_DocumentCompleted
            '   _frmMainMenu.isLoading = 1


        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Sub axbrowser_NavigateError(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
        _frmLoading.Dispose()
        If statusCode.ToString = -2146697211 Or statusCode.ToString = 500 Then
            'MsgBox("")

            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            'Me.Dispose()
            _frmErrorForm.Show()
            ' My.Settings.errorLoadTag = 1

            editSettings(xml_Filename, xml_path, "errorLoadTag", "1")




        Else
            ' My.Settings.errorLoadTag = 0
            editSettings(xml_Filename, xml_path, "errorLoadTag", "0")
        End If

    End Sub

    Private Sub WebBrowser1_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        WebBrowser1.IsWebBrowserContextMenuEnabled = False
        AddHandler(WebBrowser1.DocumentCompleted), AddressOf WebPageLoaded
        Dim axbrowser As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser.NavigateError, AddressOf axbrowser_NavigateError

        'If WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
        '    '  MsgBox(WebBrowser1.StatusText)
        '    MsgBox("")
        '    '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        '    '    _frmMainMenu.Panel2.Parent = _frmMainMenu.splitContainerControl.Panel2
        '    '    _frmLoading.TopLevel = False
        '    '    _frmLoading.Parent = _frmMainMenu.Panel2
        '    '    _frmLoading.Dock = DockStyle.Fill
        '    '    _frmLoading.Show()

        'End If

        'Dim getHeaderName As String = printF.GetHeaderForm(WebBrowser1)
        'webPageTag = getHeaderName


        'Dim getBenefitHeader As String = printF.GetBenefit(WebBrowser1)
        'My.Settings.BenefitTag = getBenefitHeader



        'Dim getBenefitHeaderTotalDisble As String = printF.GetBenefitHeaderTotalDisable(WebBrowser1)
        'My.Settings.totalDisableBenefitTag = getBenefitHeaderTotalDisble

        'If webPageTag = "Loan Information" Or webPageTag = "Benefit Claim" Or webPageTag = "Flexi-Fund Subsidiary Ledger" Or webPageTag = "Sickness Benefit" Then
        '    _frmMainMenu.Button2.Enabled = False
        'Else
        '    _frmMainMenu.Button2.Enabled = True
        'End If



    End Sub

    Private Sub WebPageLoaded()
        _frmLoading.Dispose()
        ' Me.Dispose()
        ' Me.Hide()

        If WebBrowser1.ReadyState = WebBrowserReadyState.Uninitialized Then

            'MsgBox("Erorr Loading!")
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmMainMenu.pnlWeb.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmLoading.TopLevel = False
            _frmLoading.Parent = _frmMainMenu.pnlWeb
            _frmLoading.Dock = DockStyle.Fill
            _frmLoading.Show()
            '_frmMainMenu.Panel3.Controls.Clear()
            '_frmSSSwebsite.TabControl1.Visible = False
            '_frmLoading.TopLevel = False
            '_frmLoading.Parent = _frmSSSwebsite.Panel3
            '_frmLoading.Dock = DockStyle.Fill
            '_frmLoading.Show()

        Else
            '  _frmErrorForm.Dispose()
            Me.Show()

        End If




    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

        Try
            getAdd = 0
            Dim getWebbrowserHeight As Integer = WebBrowser1.Document.Window.Size.Height
            Dim getBodyHeight As Integer = WebBrowser1.Document.Body.ScrollRectangle.Height.ToString
            If getBodyHeight <= getWebbrowserHeight Then
                Panel2.Dock = DockStyle.None
                Panel2.Visible = False
            Else
                Panel2.Dock = DockStyle.Bottom
                Panel2.Parent = Panel3
                Panel2.Show()
            End If


            tagMat = 0
            '_frmMainMenu.Button5.Enabled = True
            '_frmMainMenu.Button6.Enabled = True

            lblDisclaimer.Visible = False

            Dim getHeaderName As String = printF.GetHeaderForm(WebBrowser1)
            Dim returnMat As Boolean = printF.checKButtonMat(WebBrowser1)

            webPageTag = getHeaderName

            getLogsWeb = vbCr & e.Url.ToString() & vbCr
            userMovement = vbCr & e.Url.ToString() & vbCr
            userMovement = userMovement.Trim

            Dim defaultPage As String = printF.defaultPage(WebBrowser1)

            If defaultPage = "To inquire on" Then

                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmErrorForm.TopLevel = False
                _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmErrorForm.Dock = DockStyle.Fill
                _frmErrorForm.Show()

            Else


                If webPageTag = "Loan Information" Or webPageTag = "Employee Inquiry Module" Then
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                ElseIf _frmMainMenu.salPageTag = 1 Then
                    btnLoanEligibility.Visible = False
                ElseIf webPageTag = "Maternity Information" Then
                    _frmMainMenu.PrintControls(True)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                ElseIf returnMat = True Then
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                ElseIf printF.checkIfMaternityForMedicalEvalutionWithIllnessCode(WebBrowser1) Then
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                Else
                    _frmMainMenu.PrintControls(True)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                End If

                'If webPageTag = "Loan Eligibility" Then
                '    'btnLoanEligibility.Visible = True
                '    _frmFingerPrintMatch.Show()

                '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                '    _frmFingerPrintMatch.TopLevel = False
                '    _frmFingerPrintMatch.Parent = _frmMainMenu.splitContainerControl.Panel2
                '    _frmFingerPrintMatch.Dock = DockStyle.Fill
                '    _frmFingerPrintMatch.Show()
                'Else
                '    '    btnLoanEligibility.Visible = False

                'End If
                Dim tempUserMovement As String
                If userMovement.Contains("controller?action=ddrClaimInf") Then
                    userMovement = link22
                    _frmMainMenu.PrintControls(True)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                ElseIf userMovement.Contains("action=paymentHistory") Then
                    userMovement = link23
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                End If
                '  Dim s As String = link24
                '     userMovement = userMovement.Trim

                Select Case userMovement
                    Case link13, link14
                        Confst.Location = New Point(437, 155)
                        confend.Location = New Point(437, 200)
                        deldt.Location = New Point(437, 200)
                    Case link15, link20
                        Confst.Location = New Point(553, 205)
                        confend.Location = New Point(553, 233)
                        deldt.Location = New Point(553, 233)
                    Case Else
                        Confst.Location = New Point(437, 179)
                        confend.Location = New Point(437, 225)
                        deldt.Location = New Point(437, 225)
                End Select

                Select Case userMovement
                    Case link1
                        printTag = printZero

                        getAffectedTable = "10004"

                        getAugitLogs()

                    Case link2
                        printTag = printZero

                        getAffectedTable = "10003"

                        getAugitLogs()
                    Case link3
                        printTag = printZero

                        getAffectedTable = "10005"

                        getAugitLogs()


                    Case link4
                        printTag = printZero

                        getAffectedTable = "10006"

                        getAugitLogs()

                    Case link5
                        printTag = printZero

                        getAffectedTable = "10007"

                        getAugitLogs()
                        'Case link5v1

                        '    getAffectedTable = "Loan Status || Loan Information"

                        '    getAugitLogs()
                        'Case link5v2

                        '    getAffectedTable = "Loan Status || Loan Information || Credited Loan Payments"

                        '    getAugitLogs()

                        If _frmMainMenu.techTag = 0 Then
                        Else

                            btnTechnicalRetirementLoanStatusFinal.PerformClick()

                        End If

                    Case link6
                        printTag = printZero

                        getAffectedTable = "10008"

                        getAugitLogs()

                    Case link7
                        printTag = printZero

                        getAffectedTable = "10009"

                        getAugitLogs()

                    Case link8
                        printTag = printZero

                        getAffectedTable = "10010"

                        getAugitLogs()

                    Case link9
                        printTag = printZero

                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")

                        getAffectedTable = "10011"

                        getAugitLogs()
                    Case link10
                        printTag = printZero

                        getAffectedTable = "10012"

                        'If _frmMainMenu.tagLoanbtn = 1 Then
                        autoclickTechnical()
                        'Else
                        'End If

                        getAugitLogs()

                    Case link10v1
                        printTag = printZero

                        getAffectedTable = "10033"


                        BenefitTag1 = "Death Benefit Eligibility"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."

                        getAugitLogs()

                    Case link10v2
                        printTag = printZero

                        getAffectedTable = "10034"


                        BenefitTag1 = "SS FUNERAL"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."

                        getAugitLogs()

                    Case link10v3
                        printTag = printZero

                        getAffectedTable = "10035"

                        BenefitTag1 = "Disability Eligibility"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."

                        getAugitLogs()

                    Case link10v4
                        printTag = printZero

                        getAffectedTable = "10036"

                        BenefitTag1 = "Total Disability Eligibility"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."

                        getAugitLogs()

                    Case link11
                        autoClickSubmit()
                        printTag = printZero

                        getAffectedTable = "10013"

                        getAugitLogs()

                        '_frmMainMenu.Button2.Enabled = False

                    Case link12
                        printTag = printZero

                        getAffectedTable = "10014"

                        'lblDisclaimer.Visible = True
                        'lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."


                        getAugitLogs()

                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")

                    Case link13
                        printTag = printZero

                        getAffectedTable = "10037"

                        getAugitLogs()

                        'For Each procT In System.Diagnostics.Process.GetProcessesByName("VBSoftKeyboard")
                        '    procT.Kill()
                        'Next
                        'System.Diagnostics.Process.Start(Application.StartupPath & "\keyboard\" & "VBSoftKeyboard.exe")
                        tagMat = 1

                        _frmCalendar.Close()
                        confend.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"



                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")





                    Case link14
                        printTag = printZero

                        getAffectedTable = "10038"

                        getAugitLogs()

                        tagMat = 1
                        _frmCalendar.Close()
                        confend.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"
                    Case link15
                        printTag = printZero

                        getAffectedTable = "10039"

                        getAugitLogs()

                        tagMat = 1

                        _frmMainMenu.PrintControls(False)
                        _frmCalendar.Close()
                        confend.Visible = True
                        deldt.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Date Delivery"

                    Case link16
                        printTag = printZero
                        getAffectedTable = "10015"

                        getAugitLogs()
                    Case link17
                        printTag = printZero

                        getAffectedTable = "10043"

                        getAugitLogs()
                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        tagMat = 1
                        _frmCalendar.Close()
                        confend.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"

                    Case link18
                        printTag = printZero

                        getAffectedTable = "10044"

                        getAugitLogs()
                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        tagMat = 1
                        _frmCalendar.Close()
                        confend.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"

                    Case link19
                        printTag = printZero

                        getAffectedTable = "10043"

                        getAugitLogs()
                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        tagMat = 1
                        _frmCalendar.Close()
                        confend.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"

                    Case link20
                        printTag = printZero

                        getAffectedTable = "10014"

                        getAugitLogs()
                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        tagMat = 1
                        _frmCalendar.Close()
                        deldt.Visible = True
                        Confst.Visible = True
                        _frmCalendar.lblConfineStart.Visible = True
                        _frmCalendar.btncalsub.Visible = True
                        _frmCalendar.lblConfineStart.Text = "Confinement Start"


                    Case link21
                        printTag = printZero

                        getAffectedTable = "10033"


                        BenefitTag1 = "Retirement Eligibility"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."

                        'If _frmMainMenu.techTag = 0 Then
                        'Else

                        '    Try

                        '        printTag = printZero

                        '        getAffectedTable = "10012"
                        '        getPrevBalance = printF.getPrevBalanceTechRet(WebBrowser1)
                        '        ' Dim getLumpSum As String = printF.getLumpSum(tempBrowser)
                        '        getLumpSum = printF.getLumpSum(WebBrowser1)



                        '        If getLumpSum = "" Or getLumpSum = Nothing Then
                        '            'MsgBox("Loading of lump sum ")
                        '            _frmTechnicalRetirementEffectivity.lblLumpAmt.Text = "0.00"
                        '        Else
                        '            'getLumpSum = getLumpSum.Trim
                        '            ' MsgBox("MERON")

                        '            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
                        '            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
                        '            Dim dbComm As OracleCommand

                        '            xtd.getRawFile()
                        '            dbConn.Open()

                        '            dbComm = dbConn.CreateCommand
                        '            dbComm.Parameters.Add("CRNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
                        '            dbComm.Parameters.Add("BNAME", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("BBRANCH", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("ACCNTNO", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("STATUS", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output

                        '            dbComm.Parameters("CRNUM").Value = xtd.getCRN
                        '            dbComm.CommandText = "PR_IK_CHECK_UMID_ATMS"
                        '            dbComm.CommandType = CommandType.StoredProcedure
                        '            dbComm.ExecuteNonQuery()
                        '            dbConn.Close()

                        '            Dim getbname As String = dbComm.Parameters("BNAME").Value.ToString
                        '            'Dim getbname As String = "ENROLMENT ACTIVE"
                        '            Dim getBBRANCH As String = dbComm.Parameters("BBRANCH").Value.ToString
                        '            Dim getACCNTNO As String = dbComm.Parameters("ACCNTNO").Value.ToString
                        '            Dim getSTATUS As String = dbComm.Parameters("STATUS").Value.ToString
                        '            'Dim getSTATUS As String = "ENROLMENT ACTIVE"



                        '            Dim techRetirememtIdent As String = printF.getTechincalRetirement2(WebBrowser1)
                        '            If techRetirememtIdent.Contains("PENSION") Or techRetirememtIdent.Contains("pension") Or techRetirememtIdent.Contains("Pension") Then

                        '                GC.Collect()

                        '                If getbname = "null" Or getbname = "" Or getbname = Nothing Then
                        '                    _frmTechnicalRetirement.txtUMIDBank.Text = ""
                        '                ElseIf _frmMainMenu.getACCNTNO = "null" Or _frmMainMenu.getACCNTNO = "" Or _frmMainMenu.getACCNTNO = Nothing Then
                        '                    _frmTechnicalRetirement.txtAccountNo.Text = ""
                        '                Else
                        '                    Dim getLen As Integer = _frmMainMenu.getACCNTNO.Length

                        '                    _frmTechnicalRetirement.txtAccountNo.Text = Microsoft.VisualBasic.Right(_frmMainMenu.getACCNTNO, 4).PadLeft(getLen, "x")
                        '                    _frmTechnicalRetirement.txtUMIDBank.Text = getbname
                        '                End If

                        '                _frmTechnicalRetirementEffectivity.lblTag.Text = "Estimated pension retirement benefit"
                        '                _frmTechnicalRetirementEffectivity.lblpen1.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen2.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen3.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Visible = False
                        '                _frmTechnicalRetirementEffectivity.LineShape2.Visible = False

                        '                '_frmTechnicalRetirementEffectivity.ckBoxDC.Visible = False
                        '                '_frmTechnicalRetirementEffectivity.LabelX2.Visible = False
                        '                _frmTechnicalRetirementEffectivity.panelLumpSum.Visible = False
                        '                _frmTechnicalRetirementEffectivity.ckBoxDC.Checked = True

                        '                _frmTechnicalRetirementEffectivity.panelPension.Visible = True
                        '                _frmTechnicalRetirementEffectivity.Label4.Visible = False
                        '                _frmTechnicalRetirementEffectivity.Label6.Visible = False
                        '                If GetTotalAmtObligation = "" Or GetTotalAmtObligation = Nothing Then
                        '                    GetTotalAmtObligation = "0.00"
                        '                End If

                        '                _frmTechnicalRetirementEffectivity.lblLoanAmtPen.Text = GetTotalAmtObligation & "."
                        '                techTag = 0

                        '                'dateBirth = dateBirthConv & "."
                        '                'dateBirth = dateBirth.Replace("/", "-")

                        '                _frmMainMenu.Button5.Enabled = False
                        '                _frmMainMenu.Button6.Enabled = False
                        '                '_frmMainMenu.Button5.Text = "BACK"
                        '                '_frmMainMenu.Button6.Text = "NEXT"
                        '                _frmMainMenu.Button2.Enabled = False
                        '                _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        '                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

                        '                _frmTechnicalRetirementEffectivity.TopLevel = False
                        '                _frmTechnicalRetirementEffectivity.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '                _frmTechnicalRetirementEffectivity.Dock = DockStyle.Fill
                        '                _frmTechnicalRetirementEffectivity.Show()

                        '                Dim bdate As String = printF.GetDateBith(WebBrowser1)
                        '                _frmTechnicalRetirementEffectivity.lblDateofBirth.Text = bdate

                        '                Dim getDetails As String = xtd.technicalRetirementLumpSum(xtd.getCRN, bdate)
                        '                Dim _split2 As String() = getDetails.Split(New Char() {"|"c})
                        '                If getDetails.Contains("|") Then


                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Text = _split2(1).ToString
                        '                Else
                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                End If

                        '                If _split2(0) = "" Then
                        '                Else
                        '                    tempssDate = _split2(0).ToString
                        '                    _frmTechnicalRetirementEffectivity.lbl65date.Text = tempssDate & "."
                        '                End If

                        '                Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        '                    SW.WriteLine(xtd.getCRN & "|" & "10028" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        '                End Using
                        '                'at.getModuleLogs(xtd.getCRN, "10028", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

                        '            ElseIf techRetirememtIdent.Contains("LUMP SUM") Or techRetirememtIdent.Contains("LUMPSUM") Or techRetirememtIdent.Contains("Lump Sum") Or techRetirememtIdent.Contains("lump sum") Then
                        '                'Dim dateBirth As String = printF.GetDateBirth2(WebBrowser1)
                        '                'Dim dateBirthConv As Date = dateBirth
                        '                'dateBirthConv = dateBirthConv.AddYears(65)

                        '                GC.Collect()

                        '                If getbname = "null" Or getbname = "" Or getbname = Nothing Then
                        '                    _frmTechnicalRetirementLumpSum.txtUMIDBank.Text = ""
                        '                ElseIf _frmMainMenu.getACCNTNO = "null" Or _frmMainMenu.getACCNTNO = "" Or _frmMainMenu.getACCNTNO = Nothing Then
                        '                    _frmTechnicalRetirementLumpSum.txtAccountNo.Text = ""
                        '                Else
                        '                    Dim getLen As Integer = _frmMainMenu.getACCNTNO.Length

                        '                    _frmTechnicalRetirementLumpSum.txtAccountNo.Text = Microsoft.VisualBasic.Right(_frmMainMenu.getACCNTNO, 4).PadLeft(getLen, "x")
                        '                    _frmTechnicalRetirementLumpSum.txtUMIDBank.Text = getbname
                        '                End If



                        '                _frmTechnicalRetirementWillAvailLumpSum.txtBank.Text = getbname
                        '                '_frmTechnicalRetirementWillAvail.txtBranch.Text = getBBRANCH

                        '                _frmTechnicalRetirementEffectivity.lblTag.Text = "Estimated lump sum retirement benefit"
                        '                _frmTechnicalRetirementEffectivity.lblpen1.Visible = True
                        '                _frmTechnicalRetirementEffectivity.lblpen2.Visible = True
                        '                _frmTechnicalRetirementEffectivity.lblpen3.Visible = True
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Visible = True
                        '                _frmTechnicalRetirementEffectivity.LineShape2.Visible = True

                        '                '_frmTechnicalRetirementEffectivity.ckBoxDC.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.LabelX2.Visible = True
                        '                _frmTechnicalRetirementEffectivity.panelLumpSum.Visible = True
                        '                _frmTechnicalRetirementEffectivity.ckBoxDC.Checked = False

                        '                _frmTechnicalRetirementEffectivity.panelPension.Visible = False
                        '                _frmTechnicalRetirementEffectivity.Label4.Visible = True
                        '                _frmTechnicalRetirementEffectivity.Label6.Visible = True

                        '                techTag = 1


                        '                _frmMainMenu.Button5.Enabled = False
                        '                _frmMainMenu.Button6.Enabled = False
                        '                '_frmMainMenu.Button5.Text = "BACK"
                        '                '_frmMainMenu.Button6.Text = "NEXT"
                        '                _frmMainMenu.Button2.Enabled = False
                        '                _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        '                '_frmTechnicalRetirementEffectivity.lbl65date.Text = dateBirthConv
                        '                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        '                _frmTechnicalRetirementEffectivity.TopLevel = False
                        '                _frmTechnicalRetirementEffectivity.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '                _frmTechnicalRetirementEffectivity.Dock = DockStyle.Fill
                        '                _frmTechnicalRetirementEffectivity.Show()

                        '                Dim bdate As String = printF.GetDateBith(WebBrowser1)
                        '                _frmTechnicalRetirementEffectivity.lblDateofBirth.Text = bdate

                        '                Dim getDetails As String = xtd.technicalRetirementLumpSum(xtd.getCRN, bdate)
                        '                Dim _split2 As String() = getDetails.Split(New Char() {"|"c})

                        '                If getDetails.Contains("|") Then

                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Text = _split2(1).ToString
                        '                Else
                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False

                        '                End If

                        '                If _split2(0) = "" Then
                        '                Else
                        '                    tempssDate = _split2(0).ToString
                        '                    _frmTechnicalRetirementEffectivity.lbl65date.Text = tempssDate & "."
                        '                End If



                        '            End If

                        '            editSettings(xml_Filename, xml_path, "getLumpSum", getLumpSum)
                        '            'My.Settings.getLumpSum = getLumpSum

                        '            _frmTechnicalRetirementEffectivity.lblLumpAmt.Text = getLumpSum '"₱ " & getLumpSum

                        '            If GetTotalAmtObligation = "" Or GetTotalAmtObligation = Nothing Then
                        '                GetTotalAmtObligation = "0.00"
                        '            End If
                        '            ' nikki0001
                        '            _frmTechnicalRetirementEffectivity.lblpen2.Text = GetTotalAmtObligation '"₱ " & GetTotalAmtObligation
                        '            _frmTechnicalRetirementEffectivity.lumpAmt = getLumpSum
                        '            _frmTechnicalRetirementEffectivity.pen2 = GetTotalAmtObligation
                        '            'If _frmTechnicalRetirementEffectivity.lblpen2.Text = "₱ 0" Or _frmTechnicalRetirementEffectivity.lblpen2.Text = "₱ 0.00" Then
                        '            '    _frmTechnicalRetirementEffectivity.lblpen2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen1.Visible = False

                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Text = "₱ " & _frmTechnicalRetirementEffectivity.lblLumpAmt.Text
                        '            '    _frmTechnicalRetirementEffectivity.lblpen3.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.LineShape2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Visible = False
                        '            'Else
                        '            '    _frmTechnicalRetirementEffectivity.lblpen2.Visible = True
                        '            '    _frmTechnicalRetirementEffectivity.lblpen1.Visible = True
                        '            '    _frmTechnicalRetirementEffectivity.lblpen3.Visible = True
                        '            '    _frmTechnicalRetirementEffectivity.LineShape2.Visible = True
                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Visible = True

                        '            If _frmTechnicalRetirementEffectivity.lumpAmt = "" Or _frmTechnicalRetirementEffectivity.pen2 = "" Then
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Text = "0.00"
                        '            Else
                        '  _frmTechnicalRetirementEffectivity.lblpen4.Text = FormatNumber((_frmTechnicalRetirementEffectivity.lumpAmt) - (_frmTechnicalRetirementEffectivity.pen2))

                        '            End If
                        '            'End If
                        '            'at.getModuleLogs(xtd.getCRN, "10028", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                        '            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        '                SW.WriteLine(xtd.getCRN & "|" & "10028" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        '            End Using
                        '            If tagLoopTech = 1 Then
                        '            Else
                        '                btnTechnicalRetirementLoanStatus.PerformClick()
                        '            End If


                        '        End If



                        '    Catch ex As Exception
                        '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        '        _frmErrorForm.TopLevel = False
                        '        _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '        _frmErrorForm.Dock = DockStyle.Fill
                        '        _frmErrorForm.Show()
                        '    End Try


                        'End If

                        getAugitLogs()

                    Case link21v2
                        printTag = printZero

                        getAffectedTable = "10033"


                        BenefitTag1 = "Retirement Eligibility"

                        lblDisclaimer.Visible = False
                        lblDisclaimer.Text = "Disclaimer: Information printed may differ from the actual amount of benefit or privilege that is due and payable to the member."


                        'If _frmMainMenu.techTag = 0 Then
                        'Else

                        '    Try
                        '        printTag = printZero

                        '        getAffectedTable = "10012"
                        '        getPrevBalance = printF.getPrevBalanceTechRet(WebBrowser1)
                        '        ' Dim getLumpSum As String = printF.getLumpSum(tempBrowser)
                        '        getLumpSum = printF.getLumpSum(WebBrowser1)



                        '        If getLumpSum = "" Or getLumpSum = Nothing Then
                        '            'MsgBox("Loading of lump sum ")
                        '            _frmTechnicalRetirementEffectivity.lblLumpAmt.Text = "0.00"
                        '        Else
                        '            'getLumpSum = getLumpSum.Trim
                        '            ' MsgBox("MERON")

                        '            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
                        '            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
                        '            Dim dbComm As OracleCommand

                        '            xtd.getRawFile()
                        '            dbConn.Open()

                        '            dbComm = dbConn.CreateCommand
                        '            dbComm.Parameters.Add("CRNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
                        '            dbComm.Parameters.Add("BNAME", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("BBRANCH", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("ACCNTNO", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output
                        '            dbComm.Parameters.Add("STATUS", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Output

                        '            dbComm.Parameters("CRNUM").Value = xtd.getCRN
                        '            dbComm.CommandText = "PR_IK_CHECK_UMID_ATMS"
                        '            dbComm.CommandType = CommandType.StoredProcedure
                        '            dbComm.ExecuteNonQuery()
                        '            dbConn.Close()

                        '            Dim getbname As String = dbComm.Parameters("BNAME").Value.ToString
                        '            'Dim getbname As String = "ENROLMENT ACTIVE"
                        '            Dim getBBRANCH As String = dbComm.Parameters("BBRANCH").Value.ToString
                        '            Dim getACCNTNO As String = dbComm.Parameters("ACCNTNO").Value.ToString
                        '            Dim getSTATUS As String = dbComm.Parameters("STATUS").Value.ToString
                        '            'Dim getSTATUS As String = "ENROLMENT ACTIVE"



                        '            Dim techRetirememtIdent As String = printF.getTechincalRetirement2(WebBrowser1)
                        '            If techRetirememtIdent.Contains("PENSION") Or techRetirememtIdent.Contains("pension") Or techRetirememtIdent.Contains("Pension") Then

                        '                GC.Collect()

                        '                If getbname = "null" Or getbname = "" Or getbname = Nothing Then
                        '                    _frmTechnicalRetirement.txtUMIDBank.Text = ""
                        '                ElseIf _frmMainMenu.getACCNTNO = "null" Or _frmMainMenu.getACCNTNO = "" Or _frmMainMenu.getACCNTNO = Nothing Then
                        '                    _frmTechnicalRetirement.txtAccountNo.Text = ""
                        '                Else
                        '                    Dim getLen As Integer = _frmMainMenu.getACCNTNO.Length
                        '                    _frmTechnicalRetirement.txtAccountNo.Text = Microsoft.VisualBasic.Right(_frmMainMenu.getACCNTNO, 4).PadLeft(getLen, "x")
                        '                    _frmTechnicalRetirement.txtUMIDBank.Text = getbname
                        '                End If

                        '                _frmTechnicalRetirementEffectivity.lblTag.Text = "Estimated pension retirement benefit"
                        '                _frmTechnicalRetirementEffectivity.lblpen1.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen2.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen3.Visible = False
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Visible = False
                        '                _frmTechnicalRetirementEffectivity.LineShape2.Visible = False

                        '                '_frmTechnicalRetirementEffectivity.ckBoxDC.Visible = False
                        '                '_frmTechnicalRetirementEffectivity.LabelX2.Visible = False
                        '                _frmTechnicalRetirementEffectivity.panelLumpSum.Visible = False
                        '                _frmTechnicalRetirementEffectivity.ckBoxDC.Checked = True

                        '                _frmTechnicalRetirementEffectivity.LBLX1.Visible = False
                        '                _frmTechnicalRetirementEffectivity.LBLX2.Visible = False

                        '                _frmTechnicalRetirementEffectivity.panelPension.Visible = True
                        '                _frmTechnicalRetirementEffectivity.Label4.Visible = False
                        '                _frmTechnicalRetirementEffectivity.Label6.Visible = False
                        '                If GetTotalAmtObligation = "" Or GetTotalAmtObligation = Nothing Then
                        '                    GetTotalAmtObligation = "0.00"
                        '                End If

                        '                _frmTechnicalRetirementEffectivity.lblLoanAmtPen.Text = GetTotalAmtObligation & "."
                        '                techTag = 0

                        '                'dateBirth = dateBirthConv & "."
                        '                'dateBirth = dateBirth.Replace("/", "-")

                        '                _frmMainMenu.Button5.Enabled = False
                        '                _frmMainMenu.Button6.Enabled = False
                        '                '_frmMainMenu.Button5.Text = "BACK"
                        '                '_frmMainMenu.Button6.Text = "NEXT"
                        '                _frmMainMenu.Button2.Enabled = False
                        '                _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        '                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

                        '                _frmTechnicalRetirementEffectivity.TopLevel = False
                        '                _frmTechnicalRetirementEffectivity.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '                _frmTechnicalRetirementEffectivity.Dock = DockStyle.Fill
                        '                _frmTechnicalRetirementEffectivity.Show()

                        '                Dim bdate As String = printF.GetDateBith(WebBrowser1)
                        '                _frmTechnicalRetirementEffectivity.lblDateofBirth.Text = bdate

                        '                Dim getDetails As String = xtd.technicalRetirementLumpSum(xtd.getCRN, bdate)
                        '                Dim _split2 As String() = getDetails.Split(New Char() {"|"c})
                        '                If getDetails.Contains("|") Then


                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Text = _split2(1).ToString
                        '                Else
                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                End If

                        '                If _split2(0) = "" Then
                        '                Else
                        '                    tempssDate = _split2(0).ToString
                        '                    _frmTechnicalRetirementEffectivity.lbl65date.Text = tempssDate & "."
                        '                End If

                        '                Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        '                    SW.WriteLine(xtd.getCRN & "|" & "10028" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        '                End Using
                        '                ' at.getModuleLogs(xtd.getCRN, "10028", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

                        '            ElseIf techRetirememtIdent.Contains("LUMP SUM") Or techRetirememtIdent.Contains("LUMPSUM") Or techRetirememtIdent.Contains("Lump Sum") Or techRetirememtIdent.Contains("lump sum") Then
                        '                'Dim dateBirth As String = printF.GetDateBirth2(WebBrowser1)
                        '                'Dim dateBirthConv As Date = dateBirth
                        '                'dateBirthConv = dateBirthConv.AddYears(65)

                        '                GC.Collect()

                        '                If getbname = "null" Or getbname = "" Or getbname = Nothing Then
                        '                    _frmTechnicalRetirementLumpSum.txtUMIDBank.Text = ""
                        '                ElseIf _frmMainMenu.getACCNTNO = "null" Or _frmMainMenu.getACCNTNO = "" Or _frmMainMenu.getACCNTNO = Nothing Then
                        '                    _frmTechnicalRetirementLumpSum.txtAccountNo.Text = ""
                        '                Else
                        '                    Dim getLen As Integer = _frmMainMenu.getACCNTNO.Length

                        '                    _frmTechnicalRetirementLumpSum.txtAccountNo.Text = Microsoft.VisualBasic.Right(_frmMainMenu.getACCNTNO, 4).PadLeft(getLen, "x")
                        '                    _frmTechnicalRetirementLumpSum.txtUMIDBank.Text = getbname
                        '                End If



                        '                _frmTechnicalRetirementWillAvailLumpSum.txtBank.Text = getbname
                        '                '_frmTechnicalRetirementWillAvail.txtBranch.Text = getBBRANCH

                        '                _frmTechnicalRetirementEffectivity.lblTag.Text = "Estimated lump sum retirement benefit"
                        '                '_frmTechnicalRetirementEffectivity.lblpen1.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.lblpen2.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.lblpen3.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.lblpen4.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.LineShape2.Visible = True

                        '                '_frmTechnicalRetirementEffectivity.ckBoxDC.Visible = True
                        '                '_frmTechnicalRetirementEffectivity.LabelX2.Visible = True
                        '                _frmTechnicalRetirementEffectivity.panelLumpSum.Visible = True
                        '                _frmTechnicalRetirementEffectivity.ckBoxDC.Checked = False

                        '                _frmTechnicalRetirementEffectivity.LBLX1.Visible = True
                        '                _frmTechnicalRetirementEffectivity.LBLX2.Visible = True

                        '                _frmTechnicalRetirementEffectivity.panelPension.Visible = False
                        '                _frmTechnicalRetirementEffectivity.Label4.Visible = True
                        '                _frmTechnicalRetirementEffectivity.Label6.Visible = True
                        '                techTag = 1


                        '                _frmMainMenu.Button5.Enabled = False
                        '                _frmMainMenu.Button6.Enabled = False
                        '                '_frmMainMenu.Button5.Text = "BACK"
                        '                '_frmMainMenu.Button6.Text = "NEXT"
                        '                _frmMainMenu.Button2.Enabled = False
                        '                _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                        '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        '                '_frmTechnicalRetirementEffectivity.lbl65date.Text = dateBirthConv
                        '                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

                        '                _frmTechnicalRetirementEffectivity.TopLevel = False
                        '                _frmTechnicalRetirementEffectivity.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '                _frmTechnicalRetirementEffectivity.Dock = DockStyle.Fill
                        '                _frmTechnicalRetirementEffectivity.Show()

                        '                Dim bdate As String = printF.GetDateBith(WebBrowser1)
                        '                _frmTechnicalRetirementEffectivity.lblDateofBirth.Text = bdate

                        '                Dim getDetails As String = xtd.technicalRetirementLumpSum(xtd.getCRN, bdate)
                        '                Dim _split2 As String() = getDetails.Split(New Char() {"|"c})

                        '                If getDetails.Contains("|") Then

                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Text = _split2(1).ToString
                        '                Else
                        '                    _frmTechnicalRetirementEffectivity.lblNote1.Visible = False
                        '                    _frmTechnicalRetirementEffectivity.lblNote2.Visible = False

                        '                End If

                        '                If _split2(0) = "" Then
                        '                Else
                        '                    tempssDate = _split2(0).ToString
                        '                    _frmTechnicalRetirementEffectivity.lbl65date.Text = tempssDate & "."
                        '                End If



                        '            End If

                        '            ' My.Settings.getLumpSum = getLumpSum
                        '            editSettings(xml_Filename, xml_path, "getLumpSum", getLumpSum)

                        '            _frmTechnicalRetirementEffectivity.lblLumpAmt.Text = getLumpSum

                        '            ' nikki0001

                        '            If GetTotalAmtObligation = "" Or GetTotalAmtObligation = Nothing Then
                        '                GetTotalAmtObligation = "0.00"
                        '            End If

                        '            _frmTechnicalRetirementEffectivity.lblpen2.Text = GetTotalAmtObligation
                        '            _frmTechnicalRetirementEffectivity.lumpAmt = getLumpSum
                        '            _frmTechnicalRetirementEffectivity.pen2 = GetTotalAmtObligation
                        '            'If _frmTechnicalRetirementEffectivity.lblpen2.Text = "₱ 0" Or _frmTechnicalRetirementEffectivity.lblpen2.Text = "₱ 0.00" Then
                        '            '    _frmTechnicalRetirementEffectivity.lblpen2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen1.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Text = "₱ " & _frmTechnicalRetirementEffectivity.lblLumpAmt.Text
                        '            '    _frmTechnicalRetirementEffectivity.lblpen3.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.LineShape2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Visible = False
                        '            'Else
                        '            '    _frmTechnicalRetirementEffectivity.lblpen2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen1.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen3.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.LineShape2.Visible = False
                        '            '    _frmTechnicalRetirementEffectivity.lblpen4.Visible = False

                        '            If _frmTechnicalRetirementEffectivity.lumpAmt = "" Or _frmTechnicalRetirementEffectivity.pen2 = "" Then
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Text = "₱ 0.00"
                        '            Else
                        '                _frmTechnicalRetirementEffectivity.lblpen4.Text = "₱ " & FormatNumber((_frmTechnicalRetirementEffectivity.lumpAmt) - (_frmTechnicalRetirementEffectivity.pen2))

                        '            End If
                        '            'End If

                        '            ' at.getModuleLogs(xtd.getCRN, "10028", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                        '            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        '                SW.WriteLine(xtd.getCRN & "|" & "10028" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        '            End Using
                        '            If tagLoopTech = 1 Then
                        '            Else
                        '                btnTechnicalRetirementLoanStatus.PerformClick()
                        '            End If



                        '        End If

                        '    Catch ex As Exception
                        '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        '        _frmErrorForm.TopLevel = False
                        '        _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
                        '        _frmErrorForm.Dock = DockStyle.Fill
                        '        _frmErrorForm.Show()
                        '    End Try


                        'End If

                        getAugitLogs()

                    Case link22
                        printTag = printZero

                        getAffectedTable = "10004"

                        getAugitLogs()
                        _frmCalendar.Close()
                    Case link23
                        printTag = printZero

                        getAffectedTable = "10004"

                        _frmMainMenu.PrintControls(False)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")

                        getAugitLogs()
                        _frmCalendar.Close()


                        'Case 24
                        '    printTag = printZero

                        '    getAffectedTable = "10004"

                        '    _frmMainMenu.Button2.Enabled = False
                        '    _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")

                        '    getAugitLogs()
                        '    _frmCalendar.Close()


                    Case Else
                        If webPageTag = "Loan Eligibility" Then

                            _frmMainMenu.PrintControls(True)
                            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        ElseIf webPageTag = "Benefits Eligibility" Then
                            'lblDisclaimer.Visible = True
                            'lblDisclaimer.Text = "Disclaimer: Information Printed may differ  from the Actual Amount of benefit or privilege that is due and payable to the member"

                        ElseIf webPageTag = "SSS / UMID Card Information" Then
                            _frmMainMenu.PrintControls(True)
                            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        Else
                            '_frmMainMenu.Button2.Enabled = False

                            Try
                                'empsTatus = printF.GetCoverageStatus(WebBrowser1)

                                empsTatus = printF.GetCoverageStatus_v2(WebBrowser1)
                                'My.Settings.getEmpStatus = empsTatus

                                editSettings(xml_Filename, xml_path, "getEmpStatus", empsTatus)
                            Catch ex As Exception

                            End Try

                        End If
                        BenefitTag1 = ""

                        Try
                            CoveredStatus = printF.GetCoverageStatus_v2(WebBrowser1)
                        Catch ex As Exception
                            CoveredStatus = ""
                        End Try
                End Select

                'If webPageTag = "Loan Status" Then
                '    getURL = "http://m01ws758:8888/sss-ssitserve/controller?action=sss&id=" & getID
                '    WebBrowser1.Navigate("http://m01ws758:8888/sss-ssitserve/controller?action=sss&id=" & getID)
                'Else
                '    getURL = "http://prs:7777/sss-ssitserve/controller?action=sss&id=" & getID
                '    WebBrowser1.Navigate("http://prs:7777/sss-ssitserve/controller?action=sss&id=" & getID)
                'End If

                'Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
                'Dim loanAmount As String = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableAmount")
                Dim loanAmount As String = printF.getLoanAmountv2(WebBrowser1).Split("|")(2)
                If loanAmount = Nothing Or loanAmount = "" Then
                    btnLoanEligibility.Visible = False
                    'GroupPanel1.Visible = False
                ElseIf tagPage = "3" And _frmMainMenu.salPageTag = 1 Then
                    'btnLoanEligibility.Visible = True
                    btnLoanEligibility.Visible = False
                    'GroupPanel1.Visible = True
                ElseIf tagPage = "3" Then
                    btnLoanEligibility.Visible = True
                End If

                Dim trimPath As String = getLogsWeb

                If trimPath = getPermanentURL & "controller?action=loanComputation" Then
                    _frmMainMenu.PrintControls(False)
                    '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                Else

                End If

                Dim techRetirememt As String = printF.getTechincalRetirement(WebBrowser1)

                If techRetirememt = "" Or _frmMainMenu.techTag = 0 Then
                    'btnTechnicalRetirement.Visible = False
                Else
                    'btnTechnicalRetirement.Visible = True

                    btnTechnicalRetirement.PerformClick()

                    'btnTechnicalRetirementLoanStatus.PerformClick()

                End If

                If userMovement.Contains("http://prs:7777/sss-ssitserve/controller?action=creditedPayments&paymentParameter") And _frmMainMenu.techTag = 1 Then
                    tagTechRetSal = 1
                    btnTechnicalRetirement.PerformClick()
                Else


                End If

                'nikki0000
                If tagMat = 1 Then
                    confend.Text = ""
                    Confst.Text = ""
                    deldt.Text = ""
                    confend.Visible = True
                    Confst.Visible = True
                Else
                    confend.Text = ""
                    Confst.Text = ""
                    deldt.Text = ""
                    confend.Visible = False
                    Confst.Visible = False
                    deldt.Visible = False
                    _frmCalendar.Dispose()

                End If


                If _frmMainMenu.ifFirstLoad = 1 Then
                    _frmMainMenu.ifFirstLoad = 0
                End If

                'added by edel 01/03/2017
                Try
                    Dim fname As String = printF.GetFirstName(WebBrowser1)
                    Dim lName As String = printF.GetLastName(WebBrowser1)
                    Dim mName As String = printF.GetMiddleName(WebBrowser1)
                    Dim mDateOfBirth As String = printF.GetDateBith(WebBrowser1)


                    xtd.getMemInfo1(SSStempFile, fname, mName, lName, mDateOfBirth)
                Catch ex5 As Exception
                    Console.WriteLine(ex5.Message)
                End Try

            End If
            '_frmLoading.Dispose()
            '_frmMainMenu.trd.Abort()

            _frmMainMenu.salPageTag = 0

        Catch ex As Exception
            '_frmLoading.Dispose()
            '_frmMainMenu.trd.Abort()

            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub WebBrowser1_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating


        AddHandler(WebBrowser1.Navigating), AddressOf WebPageLoaded
        Dim axbrowser As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser.NavigateError, AddressOf axbrowser_NavigateError


        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmMainMenu.pnlWeb.Parent = _frmMainMenu.splitContainerControl.Panel2
        Me.TopLevel = False
        Me.Parent = _frmMainMenu.pnlWeb
        Me.Dock = DockStyle.Fill
        Me.Show()


    End Sub

    Public Sub getAugitLogs()
        xtd.getRawFile()
        ';at.getModuleLogs(xtd.getCRN, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
        End Using

    End Sub

    Private Sub DisposeForm(ByVal frm As Form)
        Try
            If Not frm Is Nothing Then frm.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    'Private Sub btnLoanEligibility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLoanEligibility.Click
    '    Try
    '        'Dim errorMsg As String = ""
    '        'Select Case StarIOPrinter.CheckPrinterAvailability(errorMsg)

    '        '    Case "00"
    '        '        submitLoans()
    '        '    Case "01"
    '        '        Dim result As Integer = MessageBox.Show("THIS TERMINAL IS UNABLE TO PRINT THE RECEIPT DUE TO THE FOLLOWING REASON/S. " & vbNewLine & "PRINTER IS OFFLINE." & vbNewLine & vbNewLine & "DO YOU WANT TO PROCEED?" & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
    '        '        If result = DialogResult.No Then
    '        '        ElseIf result = DialogResult.Yes Then
    '        '            submitLoans()
    '        '        End If
    '        '    Case "02"
    '        '        Dim result As Integer = MessageBox.Show("THIS TERMINAL IS UNABLE TO PRINT THE RECEIPT DUE TO THE FOLLOWING REASON/S. " & vbNewLine & "PRINTER COVER IS OPEN." & vbNewLine & vbNewLine & "DO YOU WANT TO PROCEED?" & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
    '        '        If result = DialogResult.No Then
    '        '        ElseIf result = DialogResult.Yes Then
    '        '            submitLoans()
    '        '        End If

    '        '    Case "03"
    '        '        Dim result As Integer = MessageBox.Show("THIS TERMINAL IS UNABLE TO PRINT THE RECEIPT DUE TO THE FOLLOWING REASON/S. " & vbNewLine & "PRINTER IS OUT OF PAPER." & vbNewLine & vbNewLine & "DO YOU WANT TO PROCEED?" & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
    '        '        If result = DialogResult.No Then
    '        '        ElseIf result = DialogResult.Yes Then
    '        '            submitLoans()
    '        '        End If
    '        '    Case Else
    '        '        Dim result As Integer = MessageBox.Show("THIS TERMINAL IS UNABLE TO PRINT THE RECEIPT DUE TO THE FOLLOWING REASON/S. " & vbNewLine & "PRINTER IS NOT CONNECTED" & vbNewLine & "DO YOU WANT TO PROCEED?" & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information)
    '        '        If result = DialogResult.No Then
    '        '        ElseIf result = DialogResult.Yes Then
    '        '            submitLoans()
    '        '        End If
    '        'End Select

    '        If StarIOPrinter.CheckPrinterAvailabilityv2() Then submitLoansv2()

    '    Catch ex As Exception
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmErrorForm.TopLevel = False
    '        _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmErrorForm.Dock = DockStyle.Fill
    '        _frmErrorForm.Show()
    '    End Try
    'End Sub

#Region "LOAN APPLCATION"

    'Public Sub submitLoans()

    '    DisposeForm(_frmSalaryLoanEmployer)
    '    DisposeForm(_frmSalaryLoanMember_v2)
    '    DisposeForm(_frmSalaryLoanDisclosure)
    '    DisposeForm(_frmLoanSummaryEmployed)
    '    DisposeForm(_frmLoanSummaryMember_v2)

    '    transTag = "LG"

    '    xtd.getRawFile()
    '    Dim checkType As String = xtd.checkFileType
    '    Dim tempSS As String
    '    If checkType = 1 Then
    '        tempSS = xtd.getCRN
    '    ElseIf checkType = 2 Then
    '        tempSS = SSStempFile
    '    End If

    '    Dim MyConnection1 As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
    '    Dim dbConn1 As OracleConnection = New OracleConnection(MyConnection1)
    '    Dim dbComm1 As OracleCommand

    '    xtd.getRawFile()
    '    dbConn1.Open()

    '    dbComm1 = dbConn1.CreateCommand
    '    dbComm1.CommandTimeout = 0
    '    dbComm1.Parameters.Add("SSNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
    '    'dbComm1.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '    dbComm1.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '    dbComm1.Parameters("SSNUM").Value = tempSS
    '    dbComm1.CommandText = "PR_CHECKLOAN"
    '    dbComm1.CommandType = CommandType.StoredProcedure
    '    dbComm1.ExecuteNonQuery()
    '    dbConn1.Close()

    '    Dim salaryMsg1 As String = dbComm1.Parameters("MSG").Value.ToString
    '    tagPage = "3"

    '    'temp disabled for grace testing 01/26/2017
    '    If db.checkExistence("select in_ssnbr from SSTRANSAPPSL where in_ssnbr = '" & SSStempFile & "'") = True Then
    '        'If db.checkExistence("select in_ssnbr from SSTRANSAPPSL where in_ssnbr = '123456789123456789'") = True Then
    '        'MsgBox("WITH EXISTING APPLICATION BEING PROCESSED", MsgBoxStyle.Information, "Information")

    '        GC.Collect()
    '        _frmUserAuthentication.getTransacNum()
    '        authentication = "SL04"
    '        tagPage = "3"
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmUserAuthentication.TopLevel = False
    '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmUserAuthentication.Dock = DockStyle.Fill
    '        _frmUserAuthentication.Show()
    '        _frmMainMenu.Button5.Enabled = False
    '        _frmMainMenu.Button6.Enabled = False
    '        '_frmMainMenu.Button5.Text = "BACK"
    '        '_frmMainMenu.Button6.Text = "NEXT"
    '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        _frmMainMenu.PrintControls(True)
    '        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        Dim transDesc As String = salaryMsg1

    '        ' at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        End Using
    '        'ElseIf db.checkExistence("select STRSSSID from SSTRANSAPPSLEMP where STRSSSID = '" & tempSS & "'") = True Then
    '        '    'MsgBox("WITH EXISTING APPLICATION BEING PROCESSED", MsgBoxStyle.Information, "Information")

    '        '    GC.Collect()
    '        '    tagPage = "3"
    '        '    _frmUserAuthentication.getTransacNum()
    '        '    authentication = "SL04"
    '        '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        '    _frmUserAuthentication.TopLevel = False
    '        '    _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        '    _frmUserAuthentication.Dock = DockStyle.Fill
    '        '    _frmUserAuthentication.Show()
    '        '    _frmMainMenu.Button5.Enabled = False
    '        '    _frmMainMenu.Button6.Enabled = False
    '        '    '_frmMainMenu.Button5.Text = "BACK"
    '        '    '_frmMainMenu.Button6.Text = "NEXT"
    '        '    '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '    '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        '    _frmMainMenu.Button2.Enabled = True
    '        '    _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        '    _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        '    Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        '    Dim transDesc As String = "THE MEMBER HAS A PENDING APPLICATION."
    '        '    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '        '        SW.WriteLine(xtd.getCRN & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        '    End Using
    '        '    'at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)


    '    ElseIf Not salaryMsg1 = "OK" Then

    '        GC.Collect()
    '        tagPage = "3"
    '        _frmUserAuthentication.getTransacNum()
    '        authentication = "SL04"
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmUserAuthentication.TopLevel = False
    '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmUserAuthentication.Dock = DockStyle.Fill
    '        _frmUserAuthentication.Show()
    '        _frmMainMenu.Button5.Enabled = False
    '        _frmMainMenu.Button6.Enabled = False
    '        '_frmMainMenu.Button5.Text = "BACK"
    '        '_frmMainMenu.Button6.Text = "NEXT"
    '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        _frmMainMenu.PrintControls(True)
    '        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        Dim transDesc As String = salaryMsg1

    '        'at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        End Using


    '    ElseIf salaryMsg1 = "OK" Then


    '        Dim tempId As String = _frmMainMenu.tempSalId
    '        tempId = tempId.Replace("-", "")

    '        Dim arrResponse() As String

    '        If tempId = "8888888006" Or tempId = "0000000000" Then

    '            'Dim response As String = ""
    '            'If SharedFunction.GetSalaryLoanEligibilityWS(tempSS, tempId, "S", SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "erbrn"), response) Then
    '            '    arrResponse = response.Split("|")

    '            '    If arrResponse(0) = "Error" Then
    '            '        'GetSalaryLoanEligibilityWS returned error
    '            '        GC.Collect()
    '            '        _frmUserAuthentication.getTransacNum()
    '            '        authentication = "SL04"
    '            '        tagPage = "3"
    '            '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '            '        _frmUserAuthentication.TopLevel = False
    '            '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '            '        _frmUserAuthentication.Dock = DockStyle.Fill
    '            '        _frmUserAuthentication.Show()
    '            '        _frmMainMenu.Button5.Enabled = False
    '            '        _frmMainMenu.Button6.Enabled = False
    '            '        '_frmMainMenu.Button5.Text = "BACK"
    '            '        '_frmMainMenu.Button6.Text = "NEXT"
    '            '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '            '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '            '        _frmMainMenu.Button2.Enabled = True
    '            '        _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '            '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '            '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text

    '            '        ' at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '            '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & arrResponse(1) & vbNewLine)
    '            '        End Using

    '            '        Exit Sub
    '            '    ElseIf arrResponse(4).ToUpper.Trim = "DENIED" Then
    '            '        GC.Collect()
    '            '        _frmUserAuthentication.getTransacNum()
    '            '        authentication = "SL04"
    '            '        tagPage = "3"
    '            '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '            '        _frmUserAuthentication.TopLevel = False
    '            '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '            '        _frmUserAuthentication.Dock = DockStyle.Fill
    '            '        _frmUserAuthentication.Show()
    '            '        _frmMainMenu.Button5.Enabled = False
    '            '        _frmMainMenu.Button6.Enabled = False
    '            '        '_frmMainMenu.Button5.Text = "BACK"
    '            '        '_frmMainMenu.Button6.Text = "NEXT"
    '            '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '            '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '            '        _frmMainMenu.Button2.Enabled = True
    '            '        _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '            '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '            '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text

    '            '        ' at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '            '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & response & vbNewLine)
    '            '        End Using

    '            '        Exit Sub
    '            '    End If
    '            'Else
    '            '    'GetSalaryLoanEligibilityWS failed
    '            '    arrResponse = response.Split("|")

    '            '    GC.Collect()
    '            '    _frmUserAuthentication.getTransacNum()
    '            '    authentication = "SL04"
    '            '    tagPage = "3"
    '            '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '            '    _frmUserAuthentication.TopLevel = False
    '            '    _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '            '    _frmUserAuthentication.Dock = DockStyle.Fill
    '            '    _frmUserAuthentication.Show()
    '            '    _frmMainMenu.Button5.Enabled = False
    '            '    _frmMainMenu.Button6.Enabled = False
    '            '    '_frmMainMenu.Button5.Text = "BACK"
    '            '    '_frmMainMenu.Button6.Text = "NEXT"
    '            '    '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '            '    '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '            '    _frmMainMenu.Button2.Enabled = True
    '            '    _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '            '    _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '            '    Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text

    '            '    ' at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '            '    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            '        SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & arrResponse(1) & vbNewLine)
    '            '    End Using

    '            '    Exit Sub
    '            'End If

    '            ''no error in webservice if past beyond here                
    '            'WsResponse_ServiceFee = arrResponse(1)
    '            'WsResponse_Totalbalance = arrResponse(2)
    '            'WsResponse_Netloan = arrResponse(3)
    '            'WsResponse_Appl_st = arrResponse(4)
    '            'WsResponse_Monthly_amort = arrResponse(5)
    '            'WsResponse_Rejlist = arrResponse(6)
    '            'WsResponse_LoanableAmount = arrResponse(7)
    '            'WsResponse_Loan_month = arrResponse(8)
    '            'WsResponse_MaxLoanableAmount = arrResponse(9)

    '            WS_SSNo = tempSS
    '            WS_ErNum = tempId
    '            WS_LoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableAmount")
    '            WS_MaxLoanableAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "maxloanableAmount")
    '            WS_NetLoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "netloan")
    '            WS_LoanableMonth = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableMonth")
    '            WS_PrevBalance = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "totalbalance")
    '            WS_ServiceCharge = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "servicefee")
    '            WS_ErSeqNo = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "erbrn")
    '            WS_TransID_TokenID = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "transId")
    '            WS_MemberStatus = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "memberStatus")

    '            'Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)

    '            'getLoanProceeds = loanProceeds
    '            'Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)

    '            'Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)

    '            'loanMonth = loanMonth.Substring(0, 1)
    '            'getLoanMonth = loanMonth
    '            'Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)

    '            'getAVGMSC = loanAVGMSC
    '            'Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)

    '            'getLoanBalance = loanBalance
    '            'Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)

    '            'getPrevBalance = prevBalance

    '            _frmMainMenu.Button5.Enabled = False
    '            _frmMainMenu.Button6.Enabled = False
    '            '_frmMainMenu.Button5.Text = "BACK"
    '            '_frmMainMenu.Button6.Text = "NEXT"
    '            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '            tagPage = "3.1"

    '            'Dim ERNo As String
    '            '_frmMainMenu.empNumber = _frmMainMenu.empNumber.Replace("-", "")
    '            'ERNo = _frmMainMenu.empNumber

    '            'Select Case ERNo
    '            'Dim tempId As String = _frmMainMenu.tempSalId
    '            'tempId = tempId.Replace("-", "")
    '            Select Case tempId

    '                Case "8888888006"

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                        '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        '_frmSalaryLoanEmployer.TopLevel = False
    '                        '_frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        '_frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                        '_frmSalaryLoanEmployer.Show()
    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    End If

    '                Case "0000000000"

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    End If

    '                Case Else

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanEmployer.TopLevel = False
    '                        _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                        _frmSalaryLoanEmployer.Show()

    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanEmployer.TopLevel = False
    '                        _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                        _frmSalaryLoanEmployer.Show()

    '                    End If

    '            End Select

    '        Else

    '            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
    '            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
    '            Dim dbComm As OracleCommand
    '            Dim ds As New DataSet

    '            ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
    '            dbConn.Open()
    '            dbComm = dbConn.CreateCommand
    '            dbComm.Parameters.Add("V_EMPID", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
    '            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '            'dbComm.Parameters.Add("BRANCHES", OracleDbType.RefCursor, 32767).Direction = ParameterDirection.Output
    '            dbComm.Parameters.Add("BRANCHES", OracleDbType.RefCursor).Direction = ParameterDirection.Output
    '            Dim employerID As String = _frmMainMenu.tempSalId
    '            employerID = employerID.Replace("-", "")
    '            dbComm.Parameters("V_EMPID").Value = employerID

    '            dbComm.CommandText = "PR_IK_GETWESER"
    '            dbComm.CommandType = CommandType.StoredProcedure
    '            dbComm.ExecuteNonQuery()

    '            Dim salaryMsg As String = dbComm.Parameters("MSG").Value.ToString

    '            If salaryMsg = "Ok" Or salaryMsg = "null" Or salaryMsg = " " Then
    '                WS_SSNo = tempSS
    '                WS_ErNum = tempId
    '                WS_LoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableAmount")
    '                WS_MaxLoanableAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "maxloanableAmount")
    '                WS_NetLoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "netloan")
    '                WS_LoanableMonth = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableMonth")
    '                WS_PrevBalance = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "totalbalance")
    '                WS_ServiceCharge = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "servicefee")
    '                WS_ErSeqNo = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "erbrn")
    '                WS_TransID_TokenID = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "transId")
    '                WS_MemberStatus = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "memberStatus")

    '                'Dim memContri As String = printF.getContrib(WebBrowser1)
    '                'getContributions = memContri
    '                ''Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)
    '                'Dim loanProceeds As String = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "netloan")
    '                'getLoanProceeds = loanProceeds
    '                ''Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
    '                'Dim loanAmount As String = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "maxloanableAmount")

    '                'Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)

    '                ''loanMonth = loanMonth.Substring(0, 1)
    '                'loanMonth = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableMonth")
    '                'getLoanMonth = loanMonth
    '                'Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)

    '                'getAVGMSC = loanAVGMSC
    '                'Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)

    '                'getLoanBalance = loanBalance
    '                ''Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)
    '                'Dim prevBalance As String = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "totalbalance")

    '                'getPrevBalance = prevBalance

    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"

    '                'Dim ERNo As String
    '                '_frmMainMenu.empNumber = _frmMainMenu.empNumber.Replace("-", "")
    '                'ERNo = _frmMainMenu.empNumber

    '                'Select Case ERNo
    '                'Dim tempId As String = _frmMainMenu.tempSalId
    '                'tempId = tempId.Replace("-", "")
    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                            '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            '_frmSalaryLoanEmployer.TopLevel = False
    '                            '_frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            '_frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            '_frmSalaryLoanEmployer.Show()
    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long


    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select

    '            ElseIf employerID = "8888888006" And salaryMsg = "Not in WES" Then
    '                Dim memContri As String = printF.getContrib(WebBrowser1)
    '                getContributions = memContri
    '                Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)
    '                getLoanProceeds = loanProceeds
    '                Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
    '                Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)
    '                loanMonth = loanMonth.Substring(0, 1)
    '                getLoanMonth = loanMonth
    '                Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)
    '                getAVGMSC = loanAVGMSC
    '                Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)
    '                getLoanBalance = loanBalance
    '                Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)
    '                getPrevBalance = prevBalance


    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"

    '                'Dim ERNo As String
    '                '_frmMainMenu.empNumber = _frmMainMenu.empNumber.Replace("-", "")
    '                'ERNo = _frmMainMenu.empNumber

    '                'Select Case ERNo
    '                'Dim tempId As String = _frmMainMenu.tempSalId
    '                'tempId = tempId.Replace("-", "")
    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                            '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            '_frmSalaryLoanEmployer.TopLevel = False
    '                            '_frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            '_frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            '_frmSalaryLoanEmployer.Show()
    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select
    '            ElseIf employerID = "0000000000" And salaryMsg = "Not in WES" Then
    '                Dim memContri As String = printF.getContrib(WebBrowser1)
    '                getContributions = memContri
    '                Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)
    '                getLoanProceeds = loanProceeds
    '                Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
    '                Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)
    '                loanMonth = loanMonth.Substring(0, 1)
    '                getLoanMonth = loanMonth
    '                Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)
    '                getAVGMSC = loanAVGMSC
    '                Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)
    '                getLoanBalance = loanBalance
    '                Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)
    '                getPrevBalance = prevBalance


    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"

    '                'Dim ERNo As String
    '                '_frmMainMenu.empNumber = _frmMainMenu.empNumber.Replace("-", "")
    '                'ERNo = _frmMainMenu.empNumber

    '                'Select Case ERNo
    '                'Dim tempId As String = _frmMainMenu.tempSalId
    '                'tempId = tempId.Replace("-", "")
    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                            '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            '_frmSalaryLoanEmployer.TopLevel = False
    '                            '_frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            '_frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            '_frmSalaryLoanEmployer.Show()
    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select
    '            Else
    '                MsgBox("Cannot proceed to loan application: " & salaryMsg, MsgBoxStyle.Information, "Information")
    '            End If

    '        End If
    '    End If
    'End Sub

    'Public Sub submitLoansv2()

    '    DisposeForm(_frmSalaryLoanEmployer)
    '    DisposeForm(_frmSalaryLoanMember_v2)
    '    DisposeForm(_frmSalaryLoanDisclosure)
    '    DisposeForm(_frmLoanSummaryEmployed)
    '    DisposeForm(_frmLoanSummaryMember_v2)

    '    transTag = "LG"

    '    xtd.getRawFile()
    '    Dim checkType As String = xtd.checkFileType
    '    Dim tempSS As String
    '    If checkType = 1 Then
    '        tempSS = xtd.getCRN
    '    ElseIf checkType = 2 Then
    '        tempSS = SSStempFile
    '    End If

    '    Dim MyConnection1 As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
    '    Dim dbConn1 As OracleConnection = New OracleConnection(MyConnection1)
    '    Dim dbComm1 As OracleCommand

    '    xtd.getRawFile()
    '    dbConn1.Open()

    '    dbComm1 = dbConn1.CreateCommand
    '    dbComm1.CommandTimeout = 0
    '    dbComm1.Parameters.Add("SSNUM", OracleDbType.Varchar2, 12).Direction = ParameterDirection.Input
    '    'dbComm1.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '    dbComm1.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '    dbComm1.Parameters("SSNUM").Value = tempSS
    '    dbComm1.CommandText = "PR_CHECKLOAN"
    '    dbComm1.CommandType = CommandType.StoredProcedure
    '    dbComm1.ExecuteNonQuery()
    '    dbConn1.Close()

    '    Dim salaryMsg1 As String = dbComm1.Parameters("MSG").Value.ToString
    '    tagPage = "3"

    '    'temp disabled for grace testing 01/26/2017
    '    If db.checkExistence("select in_ssnbr from SSTRANSAPPSL where in_ssnbr = '" & SSStempFile & "'") = True Then
    '        'If db.checkExistence("select in_ssnbr from SSTRANSAPPSL where in_ssnbr = '123456789123456789'") = True Then
    '        'MsgBox("WITH EXISTING APPLICATION BEING PROCESSED", MsgBoxStyle.Information, "Information")

    '        GC.Collect()
    '        _frmUserAuthentication.getTransacNum()
    '        authentication = "SL04"
    '        tagPage = "3"
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmUserAuthentication.TopLevel = False
    '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmUserAuthentication.Dock = DockStyle.Fill
    '        _frmUserAuthentication.Show()
    '        _frmMainMenu.Button5.Enabled = False
    '        _frmMainMenu.Button6.Enabled = False
    '        '_frmMainMenu.Button5.Text = "BACK"
    '        '_frmMainMenu.Button6.Text = "NEXT"
    '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        _frmMainMenu.PrintControls(True)
    '        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        Dim transDesc As String = salaryMsg1

    '        ' at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        End Using
    '        'ElseIf db.checkExistence("select STRSSSID from SSTRANSAPPSLEMP where STRSSSID = '" & tempSS & "'") = True Then
    '        '    'MsgBox("WITH EXISTING APPLICATION BEING PROCESSED", MsgBoxStyle.Information, "Information")

    '        '    GC.Collect()
    '        '    tagPage = "3"
    '        '    _frmUserAuthentication.getTransacNum()
    '        '    authentication = "SL04"
    '        '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        '    _frmUserAuthentication.TopLevel = False
    '        '    _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        '    _frmUserAuthentication.Dock = DockStyle.Fill
    '        '    _frmUserAuthentication.Show()
    '        '    _frmMainMenu.Button5.Enabled = False
    '        '    _frmMainMenu.Button6.Enabled = False
    '        '    '_frmMainMenu.Button5.Text = "BACK"
    '        '    '_frmMainMenu.Button6.Text = "NEXT"
    '        '    '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '    '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        '    _frmMainMenu.Button2.Enabled = True
    '        '    _frmMainMenu.Button2.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        '    _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        '    Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        '    Dim transDesc As String = "THE MEMBER HAS A PENDING APPLICATION."
    '        '    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '        '        SW.WriteLine(xtd.getCRN & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        '    End Using
    '        '    'at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)


    '    ElseIf Not salaryMsg1 = "OK" Then

    '        GC.Collect()
    '        tagPage = "3"
    '        _frmUserAuthentication.getTransacNum()
    '        authentication = "SL04"
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmUserAuthentication.TopLevel = False
    '        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmUserAuthentication.Dock = DockStyle.Fill
    '        _frmUserAuthentication.Show()
    '        _frmMainMenu.Button5.Enabled = False
    '        _frmMainMenu.Button6.Enabled = False
    '        '_frmMainMenu.Button5.Text = "BACK"
    '        '_frmMainMenu.Button6.Text = "NEXT"
    '        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
    '        _frmMainMenu.PrintControls(True)
    '        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
    '        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "SALARY LOAN"

    '        Dim transNum1 As String = _frmUserAuthentication.lblTransactionNo.Text
    '        Dim transDesc As String = salaryMsg1

    '        'at.getModuleLogs(xtd.getCRN, "10032", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum1, transDesc)
    '        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
    '            SW.WriteLine(SSStempFile & "|" & "10032" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum1 & "|" & transDesc & vbNewLine)
    '        End Using


    '    ElseIf salaryMsg1 = "OK" Then


    '        Dim tempId As String = _frmMainMenu.tempSalId
    '        tempId = tempId.Replace("-", "")

    '        Dim arrResponse() As String

    '        If tempId = "8888888006" Or tempId = "0000000000" Then



    '            WS_SSNo = tempSS
    '            WS_ErNum = tempId
    '            WS_LoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableAmount")
    '            WS_MaxLoanableAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "maxloanableAmount")
    '            WS_NetLoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "netloan")
    '            WS_LoanableMonth = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableMonth")
    '            WS_PrevBalance = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "totalbalance")
    '            WS_ServiceCharge = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "servicefee")
    '            WS_ErSeqNo = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "erbrn")
    '            WS_TransID_TokenID = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "transId")
    '            WS_MemberStatus = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "memberStatus")

    '            _frmMainMenu.Button5.Enabled = False
    '            _frmMainMenu.Button6.Enabled = False
    '            '_frmMainMenu.Button5.Text = "BACK"
    '            '_frmMainMenu.Button6.Text = "NEXT"
    '            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '            tagPage = "3.1"


    '            Select Case tempId

    '                Case "8888888006"

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()
    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    End If

    '                Case "0000000000"

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanMember_v2.TopLevel = False
    '                        _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                        _frmSalaryLoanMember_v2.Show()

    '                    End If

    '                Case Else

    '                    Dim j As Long

    '                    'If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then
    '                    If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview.Items.Add(value)
    '                        Next
    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanEmployer.TopLevel = False
    '                        _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                        _frmSalaryLoanEmployer.Show()

    '                    Else

    '                        'For value As Integer = loanAmount To 0 Step -500
    '                        For value As Integer = WS_LoanAmount To 0 Step -500
    '                            lview2.Items.Add(value)
    '                        Next

    '                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                        _frmSalaryLoanEmployer.TopLevel = False
    '                        _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                        _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                        _frmSalaryLoanEmployer.Show()

    '                    End If

    '            End Select

    '        Else

    '            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
    '            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
    '            Dim dbComm As OracleCommand
    '            Dim ds As New DataSet

    '            ' dbConn.ConnectionString = "Provider=MSDAORA;User ID=xxx;Password=xxx;Data Source=xxx;"
    '            dbConn.Open()
    '            dbComm = dbConn.CreateCommand
    '            dbComm.Parameters.Add("V_EMPID", OracleDbType.Varchar2, 10).Direction = ParameterDirection.Input
    '            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
    '            'dbComm.Parameters.Add("BRANCHES", OracleDbType.RefCursor, 32767).Direction = ParameterDirection.Output
    '            dbComm.Parameters.Add("BRANCHES", OracleDbType.RefCursor).Direction = ParameterDirection.Output
    '            Dim employerID As String = _frmMainMenu.tempSalId
    '            employerID = employerID.Replace("-", "")
    '            dbComm.Parameters("V_EMPID").Value = employerID

    '            dbComm.CommandText = "PR_IK_GETWESER"
    '            dbComm.CommandType = CommandType.StoredProcedure
    '            dbComm.ExecuteNonQuery()

    '            Dim salaryMsg As String = dbComm.Parameters("MSG").Value.ToString

    '            If salaryMsg = "Ok" Or salaryMsg = "null" Or salaryMsg = " " Then
    '                WS_SSNo = tempSS
    '                WS_ErNum = tempId
    '                WS_LoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableAmount")
    '                WS_MaxLoanableAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "maxloanableAmount")
    '                WS_NetLoanAmount = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "netloan")
    '                WS_LoanableMonth = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "loanableMonth")
    '                WS_PrevBalance = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "totalbalance")
    '                WS_ServiceCharge = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "servicefee")
    '                WS_ErSeqNo = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "erbrn")
    '                WS_TransID_TokenID = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "transId")
    '                WS_MemberStatus = SharedFunction.GetDocumentValueByTag2(WebBrowser1.DocumentText, "memberStatus")

    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"

    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()
    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long


    '                        If WS_PrevBalance = 0 Or WS_LoanAmount = "0" Or WS_PrevBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = WS_LoanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select

    '            ElseIf employerID = "8888888006" And salaryMsg = "Not in WES" Then
    '                Dim memContri As String = printF.getContrib(WebBrowser1)
    '                getContributions = memContri
    '                Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)
    '                getLoanProceeds = loanProceeds
    '                Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
    '                Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)
    '                loanMonth = loanMonth.Substring(0, 1)
    '                getLoanMonth = loanMonth
    '                Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)
    '                getAVGMSC = loanAVGMSC
    '                Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)
    '                getLoanBalance = loanBalance
    '                Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)
    '                getPrevBalance = prevBalance


    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"


    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()
    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select
    '            ElseIf employerID = "0000000000" And salaryMsg = "Not in WES" Then
    '                Dim memContri As String = printF.getContrib(WebBrowser1)
    '                getContributions = memContri
    '                Dim loanProceeds As String = printF.getLoanProceeds(WebBrowser1)
    '                getLoanProceeds = loanProceeds
    '                Dim loanAmount As String = printF.getLoanAmountEligibility(WebBrowser1)
    '                Dim loanMonth As String = printF.getLoanAmount(WebBrowser1)
    '                loanMonth = loanMonth.Substring(0, 1)
    '                getLoanMonth = loanMonth
    '                Dim loanAVGMSC As String = printF.getLoanEligibilityMSC(WebBrowser1)
    '                getAVGMSC = loanAVGMSC
    '                Dim loanBalance As String = printF.getLoanBalance(WebBrowser1)
    '                getLoanBalance = loanBalance
    '                Dim prevBalance As String = printF.getPrevBalance(WebBrowser1)
    '                getPrevBalance = prevBalance


    '                _frmMainMenu.Button5.Enabled = False
    '                _frmMainMenu.Button6.Enabled = False
    '                '_frmMainMenu.Button5.Text = "BACK"
    '                '_frmMainMenu.Button6.Text = "NEXT"
    '                '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
    '                '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

    '                tagPage = "3.1"

    '                Select Case tempId

    '                    Case "8888888006"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case "0000000000"

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanMember_v2.TopLevel = False
    '                            _frmSalaryLoanMember_v2.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanMember_v2.Dock = DockStyle.Fill
    '                            _frmSalaryLoanMember_v2.Show()

    '                        End If

    '                    Case Else

    '                        Dim j As Long

    '                        If loanBalance = 0 Or loanAmount = "0" Or loanBalance < 500 Or _frmSalaryLoanEmployer.txtLoanAmt.Text >= 16000 Then

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview.Items.Add(value)
    '                            Next
    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        Else

    '                            For value As Integer = loanAmount To 0 Step -500
    '                                lview2.Items.Add(value)
    '                            Next

    '                            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '                            _frmSalaryLoanEmployer.TopLevel = False
    '                            _frmSalaryLoanEmployer.Parent = _frmMainMenu.splitContainerControl.Panel2
    '                            _frmSalaryLoanEmployer.Dock = DockStyle.Fill
    '                            _frmSalaryLoanEmployer.Show()

    '                        End If

    '                End Select
    '            Else
    '                MsgBox("Cannot proceed to loan application: " & salaryMsg, MsgBoxStyle.Information, "Information")
    '            End If

    '        End If
    '    End If
    'End Sub

#End Region
    Private Sub _frmWebBrowser_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub



    Private Sub btnTechnicalRetirement_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTechnicalRetirement.Click
        Try

            tagLoopTech2 = 0

            If tagTechRetSal = 1 Then

                GetTotalAmtObligation = printF.GetTotalAmtObligation(WebBrowser1)

                If GetTotalAmtObligation = "" Or GetTotalAmtObligation = Nothing Then
                    GetTotalAmtObligation = "0.00"
                End If
                tagTechRetSal = 0
            End If

            Dim getTotNumContriSemContin As String = printF.getTotNumContriSemContin(WebBrowser1)

            If getTotNumContriSemContin = "" Then

            Else
                lastContribution = getTotNumContriSemContin

            End If

            If lastContribution >= 120 Then
                WebBrowser1.Navigate(getPermanentURL & "controller?action=RRP")

            ElseIf lastContribution < 120 Then
                WebBrowser1.Navigate(getPermanentURL & "controller?action=RRL")


            End If



        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    'Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
    '    Confst.Location = New Point(CInt(Confst.Text.Split(",")(0)), CInt(Confst.Text.Split(",")(1)))
    '    deldt.Location = New Point(CInt(deldt.Text.Split(",")(0)), CInt(deldt.Text.Split(",")(1)))
    'End Sub

    Private Sub confend_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles confend.Click
        mouseFocus = "Confend"
        _frmCalendar.Close()
        _frmCalendar.lblConfineStart.Visible = True
        _frmCalendar.btncalsub.Visible = True
        _frmCalendar.lblConfineStart.Text = "Confinement Start"
        _frmCalendar.ShowDialog()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        txt1_X.Text = Confst.Location.X
        txt1_Y.Text = Confst.Location.Y
        txt2_X.Text = confend.Location.X
        txt2_Y.Text = confend.Location.Y
        MessageBox.Show(userMovement)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        Confst.Location = New Point(CInt(txt1_X.Text), CInt(txt1_Y.Text))
        confend.Location = New Point(CInt(txt2_X.Text), CInt(txt2_Y.Text))
    End Sub

    Private Sub Confst_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Confst.Click
        mouseFocus = "Confst"
        _frmCalendar.Close()
        _frmCalendar.lblConfineStart.Visible = True
        _frmCalendar.btncalsub.Visible = True
        _frmCalendar.lblConfineStart.Text = "Confinement Start"
        _frmCalendar.ShowDialog()
    End Sub

    Private Sub deldt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles deldt.Click
        mouseFocus = "deldt"
        _frmCalendar.Close()
        _frmCalendar.lblConfineStart.Visible = True
        _frmCalendar.btncalsub.Visible = True
        _frmCalendar.lblConfineStart.Text = "Delivery Date"
        _frmCalendar.ShowDialog()
    End Sub

    Private Sub btnUP_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnUP.Click
        If getAdd = 0 Then

        Else
            getAdd -= 80
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)



            'My.Settings.Save()

        End If
    End Sub

    Private Sub btnDown_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDown.Click
        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        If getAdd = 0 Then
            getAdd += 80
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)

            'My.Settings.Save()
        Else
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
            getAdd += 80
            'My.Settings.Save()
        End If
    End Sub

    Private Sub btnTechnicalRetirementLoanStatus_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTechnicalRetirementLoanStatus.Click
        Try
            tagLoopTech = 1
            WebBrowser1.Navigate(getPermanentURL & "controller?action=loanStatus")

        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnTechnicalRetirementLoanStatusFinal_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnTechnicalRetirementLoanStatusFinal.Click
        Try
            Dim goToLoanDate As String = printF.goToLoanDate(WebBrowser1)
            Dim getLoanDate As String
            Dim goToLoanDatestr As Date
            If goToLoanDate = "" Then
                getLoanDate = ""

                If lastContribution >= 120 Then
                    WebBrowser1.Navigate(getPermanentURL & "controller?action=RRP")

                ElseIf lastContribution < 120 Then
                    WebBrowser1.Navigate(getPermanentURL & "controller?action=RRL")


                End If
            Else
                goToLoanDatestr = goToLoanDate
                getLoanDate = goToLoanDatestr.ToString("yy-MM-dd")

                getLoanDate = getLoanDate.Replace("-", "")
                Dim dateToday As String = Date.Today.ToString("MM-dd-yyyy")
                WebBrowser1.Navigate(getPermanentURL & "controller?action=creditedPayments&paymentParameter=" & SSStempFile & getLoanDate & "S&saDate=" & dateToday)

            End If


        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub



    Private Sub Body_MouseDown(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        Select Case e.MouseButtonsPressed
            Case MouseButtons.Left
                Dim element As HtmlElement = WebBrowser1.Document.GetElementFromPoint(e.ClientMousePosition)

                Select Case element.Id
                    Case "cancelAction", "doneAction"
                        'If SharedFunction.ShowMessage("Are you sure you want to leave?") = DialogResult.Yes Then
                        '    _frmMainMenu.btnInquiry_Click()
                        'End If
                        _frmMainMenu.btnInquiry_Click()
                End Select

                'If element IsNot Nothing AndAlso "submit".Equals(element.GetAttribute("type"), StringComparison.OrdinalIgnoreCase) Then
                '    MessageBox.Show("button is clicked")
                'End If
        End Select
    End Sub

End Class