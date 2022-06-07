Imports System
Imports System.Net.Security
Imports System.Net
Imports System.Security.Cryptography.X509Certificates
Imports System.Threading
Public Class _frmSSSwebsite
    Public trd As Thread
    Dim tabPageInc As New TabPage
    'Private urlString As String = "https://www.sss.gov.ph/sss/Section_View"
    Private urlString As String '= sssWebsiteLink
    Dim at As New auditTrail
    Dim getModule As String
    Dim getTask As String
    Dim getAffectedTable As String
    Dim db As New ConnectionString
    Dim xtd As New ExtractedDetails
    Dim navError As Integer
    Dim WithEvents hDoc As HtmlDocument

    Dim isRefresh As Boolean
    Dim brws As New WebBrowser
    Dim xs As New MySettings
    Dim sssWebsiteLink As String = readSettings(xml_Filename, xml_path, "sssWebsiteLink")
    Dim webBusy As Integer = 0
#Region "LINKS"
#Region "links Home"

    Dim linkhome As String = sssWebsiteLink & "sss/Section_View"
    Dim linklogin As String = sssWebsiteLink & "sss/login.jsp"

#End Region
#Region "MY.SSS"
    'My.SSS
    Dim linkMySSS As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=1"
    Dim linkregister As String = sssWebsiteLink & "sss/register.jsp"
    Dim linkeservices As String = sssWebsiteLink & "sss/index2.jsp?secid=743&cat=1&pg=null"
    Dim linkepaymentlinks As String = sssWebsiteLink & "sss/index2.jsp?secid=961&cat=1&pg=null"
    Dim linkaboutsss As String = sssWebsiteLink & "sss/index2.jsp?secid=744&cat=1&pg=null"
#End Region
#Region "membership"
    'Membership
    Dim linkMembership As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=2"
    Dim linkMemCoverage As String = sssWebsiteLink & "sss/index2.jsp?secid=106&cat=2&pg=null"
    Dim linkMemCoveEmpr As String = sssWebsiteLink & "sss/index2.jsp?secid=107&cat=2&pg=null"
    Dim linkMemCoveEmpees As String = sssWebsiteLink & "sss/index2.jsp?secid=108&cat=2&pg=null"
    Dim linkMemCoveSelfEmp As String = sssWebsiteLink & "sss/index2.jsp?secid=112&cat=2&pg=null"
    Dim linkMemCoveVoluntary As String = sssWebsiteLink & "sss/index2.jsp?secid=113&cat=2&pg=null"
    Dim linkMemProcedure As String = sssWebsiteLink & "sss/index2.jsp?secid=109&cat=2&pg=null"
    Dim linkMemDutyandRes As String = sssWebsiteLink & "sss/index2.jsp?secid=110&cat=2&pg=null"
    Dim linkMemSchedofCon As String = sssWebsiteLink & "sss/index2.jsp?secid=111&cat=2&pg=null"
    Dim linkMemEffofCov As String = sssWebsiteLink & "sss/index2.jsp?secid=114&cat=2&pg=null"
    Dim linkMemFaqs As String = sssWebsiteLink & "sss/index2.jsp?secid=121&cat=2&pg=null"
#End Region
#Region "Loans"
#Region "Member Loans"
    Dim linkLoans As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=3"
    'Salary Loan
    Dim linkMemberLoans As String = sssWebsiteLink & "sss/index2.jsp?secid=755&cat=3&pg=null"
    Dim linkMemSalLoans As String = sssWebsiteLink & "sss/index2.jsp?secid=759&cat=3&pg=null"
    Dim linkMemSalLoansPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=80&cat=3&pg=null"
    Dim linkMemSalLoansPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=81&cat=3&pg=null"
    'Emergency Loan
    Dim linkEmergencyLoans As String = sssWebsiteLink & "sss/index2.jsp?secid=761&cat=3&pg=null"
    Dim linkEmLoansPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=396&cat=3&pg=null"
    Dim linkEmLoansPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=395&cat=3&pg=null"
#End Region
#Region "Housing Loans"
    'Pari-Passu
    Dim linkHousingLoans As String = sssWebsiteLink & "sss/index2.jsp?secid=756&cat=3&pg=null"
    Dim linkHouseParPassu As String = sssWebsiteLink & "sss/index2.jsp?secid=762&cat=3&pg=null"
    Dim linkHouseParPassuPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=90&cat=3&pg=null"
    Dim linkHouseParPassuPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=92&cat=3&pg=null"
    'Housing Loan for Repairs and Improvement
    Dim linkHLforRepairAndImpr As String = sssWebsiteLink & "sss/index2.jsp?secid=765&cat=3&pg=null"
    Dim linkHLforRepairAndImprPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=93&cat=3&pg=null"
    Dim linkHLforRepairAndImprPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=765&cat=3&pg=null"
    'Corporate Housing Program
    Dim linkCorpHouseProg As String = sssWebsiteLink & "sss/index2.jsp?secid=771&cat=3&pg=null"
    Dim linkCorpHouseProgPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=95&cat=3&pg=null"
    Dim linkCorpHouseProgPT2 As String = sssWebsiteLink & "/sss/index2.jsp?secid=96&cat=3&pg=null"
    Dim linkCorpHouseProgPT3 As String = sssWebsiteLink & "sss/index2.jsp?secid=97&cat=3&pg=null"
    'Individual Housing
    Dim linkIndiviHousing As String = sssWebsiteLink & "sss/index2.jsp?secid=772&cat=3&pg=null"
    Dim linkIndiviHousingPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=98&cat=3&pg=null"
    Dim linkIndiviHousingPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=99&cat=3&pg=null"
    'Housing Development Loan
    Dim linkHousingDevLoan As String = sssWebsiteLink & "sss/index2.jsp?secid=773&cat=3&pg=null"
    Dim linkHousingDevLoanPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=100&cat=3&pg=null"
    Dim linkHousingDevLoanPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=101&cat=3&pg=null"
    'Apartment and Dormitory
    Dim linkApartmenandDorm As String = sssWebsiteLink & "sss/index2.jsp?secid=774&cat=3&pg=null"
    Dim linkApartmenandDormPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=102&cat=3&pg=null"
    Dim linkApartmenandDormPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=103&cat=3&pg=null"
    'Housing Loan for OFWs
    Dim linkHouseLoanforOFW As String = sssWebsiteLink & "sss/index2.jsp?secid=775&cat=3&pg=null"
    Dim linkHouseLoanforOFWPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=104&cat=3&pg=null"
    Dim linkHouseLoanforOFWPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=105&cat=3&pg=null"
    'Housing Loan for Workers Organization Members
    Dim linkHouseLoanforWOMem As String = sssWebsiteLink & "sss/index2.jsp?secid=776&cat=3&pg=null"
    Dim linkHouseLoanforWOMemPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=115&cat=3&pg=null"
    Dim linkHouseLoanforWOMemPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=116&cat=3&pg=null"
    'Assumption of Mortgage
    Dim linkAssumpofMortage As String = sssWebsiteLink & "sss/index2.jsp?secid=777&cat=3&pg=null"
    Dim linkAssumpofMortagePT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=117&cat=3&pg=null"
    Dim linkAssumpofMortagePT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=118&cat=3&pg=null"
    'Direct Housing Development Plan
    Dim linkDirectHouseDevPlan As String = sssWebsiteLink & "sss/index2.jsp?secid=778&cat=3&pg=null"
    Dim linkDirectHouseDevPlanPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=119&cat=3&pg=null"
    Dim linkDirectHouseDevPlanPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=120&cat=3&pg=null"
#End Region
#Region "Business Loans"
    Dim linkBusinessLoan As String = sssWebsiteLink & "sss/index2.jsp?secid=757&cat=3&pg=null"
    Dim linkBusiSulongProgram As String = sssWebsiteLink & "sss/index2.jsp?secid=785&cat=3&pg=null"
    Dim linkBusiSulongProgramPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=135&cat=3&pg=null"
    Dim linkBusiSulongProgramPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=136&cat=3&pg=null"
    Dim linkBusiSocDevLoanFacility As String = sssWebsiteLink & "sss/index2.jsp?secid=2205&cat=3&pg=null"
    Dim linkBusiDevLoanFacility As String = sssWebsiteLink & "sss/index2.jsp?secid=2224&cat=3&pg=null"
#End Region

#End Region
#Region "benefits"
    Dim linkBenefits As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=4"
    Dim linkSocSecBenefits As String = sssWebsiteLink & "sss/index2.jsp?secid=786&cat=4&pg=null"
    'Social Security Benefits
#Region "benefits - Sickness"
    Dim linkSicknessBenefits As String = sssWebsiteLink & "sss/index2.jsp?secid=788&cat=4&pg=null"
    Dim linkSicknessBenefitsPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=147&cat=4&pg=null"
    Dim linkSicknessBenefitsPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=44&cat=4&pg=null"
#End Region
#Region "benefits - Maternity"
    Dim linkMaterntity As String = sssWebsiteLink & "sss/index2.jsp?secid=789&cat=4&pg=null"
    Dim linkMaterntityPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=53&cat=4&pg=null"
    Dim linkMaterntityPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=54&cat=4&pg=null"
#End Region
#Region "benefits - Retirement"
    Dim linkRetirement As String = sssWebsiteLink & "sss/index2.jsp?secid=790&cat=4&pg=null"
    Dim linkRetirementPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=73&cat=4&pg=null"
    Dim linkRetirementPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=75&cat=4&pg=null"
#End Region
#Region "benefits - Disability"
    Dim linkDisability As String = sssWebsiteLink & "sss/index2.jsp?secid=791&cat=4&pg=null"
    Dim linkDisabilityPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=67&cat=4&pg=null"
    Dim linkDisabilityPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=68&cat=4&pg=null"
#End Region
#Region "benefits - Death and Funeral"
    Dim linkDeathandFuneral As String = sssWebsiteLink & "sss/index2.jsp?secid=792&cat=4&pg=null"
    Dim linkDeathandFuneralPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=77&cat=4&pg=null"
    Dim linkDeathandFuneralPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=79&cat=4&pg=null"
#End Region
    '-------------------------
    'Employees Compensation
    Dim linkEmployeesCompensation As String = sssWebsiteLink & "sss/index2.jsp?secid=824&cat=4&pg=null"
#Region "benefits - Social Security"
    Dim linkEmployeesCompensationPT1 As String = sssWebsiteLink & "sss/index2.jsp?secid=338&cat=4&pg=null"
    Dim linkEmployeesCompensationPT2 As String = sssWebsiteLink & "sss/index2.jsp?secid=339&cat=4&pg=null"
    Dim linkEmployeesCompensationPT3 As String = sssWebsiteLink & "sss/index2.jsp?secid=340&cat=4&pg=null"
#End Region
    '------------------------

#End Region
#Region "Corporate Profile"
    Dim linkCorporateProfile As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=5"
    Dim linkManagementDirectory As String = sssWebsiteLink & "sss/index2.jsp?secid=397&cat=5&pg=null"
    Dim linkSSSMandate As String = sssWebsiteLink & "sss/index2.jsp?secid=796&cat=5&pg=null"
    Dim linkLegistativeHistory As String = sssWebsiteLink & "sss/index2.jsp?secid=797&cat=5&pg=null"
    Dim linkBranchAndOffices As String = sssWebsiteLink & "sss/index2.jsp?secid=800&cat=5&pg=null"
    Dim linkRegionalListing As String = sssWebsiteLink & "sss/index2.jsp?secid=801&cat=5&pg=null"
    Dim linkOrganization As String = sssWebsiteLink & "sss/index2.jsp?secid=1025&cat=5&pg=null"
    Dim linkPublication As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=6"
    Dim linkPublishPrintAds As String = sssWebsiteLink & "sss/index2.jsp?secid=166&cat=6&pg=null"
    Dim linkMultimediaMaterials As String = sssWebsiteLink & "sss/index2.jsp?secid=582&cat=6&pg=null"
    Dim linkLawsAndRegulations As String = sssWebsiteLink & "sss/index2.jsp?secid=802&cat=6&pg=null"
    Dim linknewsAndUpdates As String = sssWebsiteLink & "sss/index2.jsp?secid=804&cat=6&pg=null"
    Dim linkFactsAndFigures As String = sssWebsiteLink & "sss/index2.jsp?secid=805&cat=6&pg=null"
    Dim linkAnnualReport As String = sssWebsiteLink & "sss/index2.jsp?secid=807&cat=6&pg=null"
    Dim linkSSCommission As String = sssWebsiteLink & "sss/index2.jsp?secid=1465&cat=6&pg=null"
    Dim linkSSSnewsLetter As String = sssWebsiteLink & "sss/index2.jsp?secid=1884&cat=6&pg=null"
    Dim linkMessageFromThePCEO As String = sssWebsiteLink & "sss/index2.jsp?secid=2285&cat=6&pg=null"
    Dim linkOtherServices As String = sssWebsiteLink & "sss/index2.jsp?secid=0&cat=7"
    Dim linkSSSnet As String = sssWebsiteLink & "sss/index2.jsp?secid=125&cat=7&pg=null"
    Dim linkAutoDebitArrangement As String = sssWebsiteLink & "sss/index2.jsp?secid=126&cat=7&pg=null"
    Dim linkSSSflexiFund As String = sssWebsiteLink & "sss/index2.jsp?secid=812&cat=7&pg=null"
    Dim linkSSSid As String = sssWebsiteLink & "sss/index2.jsp?secid=813&cat=7&pg=null"


#End Region
#Region "Download Forms"
    Dim linkDownloadSSSForms As String = sssWebsiteLink & "sss/index2.jsp?secid=1&cat=8&pg=null"
    Dim linkDownloadForm As String = sssWebsiteLink & "sss/uploaded_images/forms/normal/0169882509.PDF"
    Dim linkFillForm As String = sssWebsiteLink & "sss/uploaded_images/forms/editable/0169882509.PDF"
    Dim linkLmsProject As String = sssWebsiteLink & "sss/index2.jsp?secid=1&cat=101&pg=null"
#End Region


#End Region

#Region "Clear fields"
    Public Sub clearFeedbackWebsite()
        _frmFeedbackWebsite.txtName.Clear()
        _frmFeedbackWebsite.txtEmail.Clear()
        _frmFeedbackWebsite.txtAddress1.Clear()

        _frmFeedbackWebsite.txtZipCode.Clear()
        _frmFeedbackWebsite.txtAddress2.Clear()
        '_frmFeedbackWebsite.num1 = Nothing  
        '_frmFeedbackWebsite.num2 = 0
        '_frmFeedbackWebsite.num3 = 0
        '_frmFeedbackWebsite.num4 = 0
        '_frmFeedbackWebsite.num5 = 0
        '_frmFeedbackWebsite.num6 = 0
        '_frmFeedbackWebsite.num7 = 0
        _frmFeedbackWebsite3.rtbWhy.Clear()
        _frmFeedbackWebsite3.rtbWhat.Clear()
        _frmFeedbackWebsite4.rtbIf.Clear()
        _frmFeedbackWebsite.rbtHomeAddress.Checked = True
        _frmFeedbackWebsite3.rbtNo.Checked = True
    End Sub
#End Region

    Function Fileexists(ByVal fname) As Boolean
        Try
            If Dir(fname) <> "" Then _
        Fileexists = True _
        Else Fileexists = False
        Catch ex As Exception

        End Try
    End Function

    Public Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function



    Sub DeleteFile(ByVal FileToDelete As String)
        Try
            If Fileexists(FileToDelete & "\*.*") Then 'See above
                SetAttr(FileToDelete, vbNormal)
                Kill(FileToDelete & "\*.*")
            End If
        Catch ex As Exception

        End Try
    End Sub

    Public Sub getAugitLogs()
        xtd.getRawFile()
        ' at.getModuleLogs(xtd.getCRN, getAffectedTable, tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")

        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
        End Using
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try

            tagPage = "15"
            printTag = 0
            '  db.ExecuteSQLQuery("Update SSINFOTERMKIOSK set status = '" & "False" & "', Offline_Date = '" & Date.Today.ToShortDateString & _
            '                    "', Offline_Time = '" & TimeOfDay & "' where IPAddress = '" & kioskIP & "' and Kiosk_ID = '" & kioskID & "'")
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
            Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")

            'db.sql = "Insert into SSINFOTERMCONSTAT (BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,OFFLINE_DT,OFFLINE_TME,DATESTAMP) values('" & kioskIP & "','" & getbranchCoDE & "','" & getkiosk_cluster & _
            '   "','" & getkiosk_group & "','" & Date.Today.ToShortDateString & "','" & TimeOfDay & "','" & Today & "')"
            'db.ExecuteSQLQuery(db.sql)

            Dim getLastofflinedate As DateTime = Date.Today.ToShortDateString
            Dim getlastofflinetime As DateTime = TimeOfDay
            Dim finaldate As DateTime
            finaldate = getLastofflinedate & " " & getlastofflinetime
            lastOffline = finaldate

            'SharedFunction.ShowAppMainForm(Me)


        Catch ex As Exception
        Finally
            _frmMainMenu.IsMainMenuActive = False
            _frmMainMenu.Hide()
            'Me.Close()
            SharedFunction.ShowMainDefaultUserForm()
            Main.Show()
        End Try
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'getAdd = 0
        'Me.WebBrowser1.Document.Window.ScrollTo(getAdd, 0)
        Try


            If getAdd = 0 Then

            Else
                getAdd -= 10
                Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                'My.Settings.Save()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        Try


            Select Case tagPage

                Case "13.1"
                    'chicha2 += 10
                    Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    If getAdd = 0 Then
                        getAdd += 10
                        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                        'My.Settings.Save()
                    Else
                        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                        getAdd += 10
                        'My.Settings.Save()
                    End If
                Case "13.2"
                    Dim posY As Integer
                    If posY > Panel1.VerticalScroll.Maximum - 0 Then
                        posY = Panel1.VerticalScroll.Maximum - 450
                    Else
                        posY += 450
                        Panel1.AutoScrollPosition = New Point(0, posY)
                    End If
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try


            Select Case tagPage
                Case "13.1"
                    WebBrowser1.GoBack()
                Case "13.2"
                    'Button6.Text = "NEXT"
                    'Button5.Text = "BACK"
                    Button7.Text = "REFRESH"
                    'Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK.png")
                    'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT.png")
                    _frmFeedbackWebsite.Dispose()
                    Panel3.Controls.Clear()
                    TabControl1.Top = False
                    TabControl1.Parent = Me.Panel3
                    TabControl1.Dock = DockStyle.Fill
                    TabControl1.Show()
                    tagPage = "13.1"
            End Select
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try


            Select Case tagPage
                Case "13.1"
                    WebBrowser1.GoForward()
                Case "13.2"
                    Button7.Text = "SUBMIT"
                    If _frmFeedbackWebsite.txtName.Text = "" Or _frmFeedbackWebsite.txtName.Text = Nothing Then
                        _frmFeedbackWebsite.txtName.Focus()
                        _frmFeedbackWebsite.lblname.Visible = True
                    ElseIf _frmFeedbackWebsite.txtEmail.Text = "" Or _frmFeedbackWebsite.txtEmail.Text = Nothing Then
                        _frmFeedbackWebsite.txtEmail.Focus()
                        _frmFeedbackWebsite.lblEmail.Visible = True
                        'ElseIf _frmFeedbackWebsite.txtStreet.Text = "" Or _frmFeedbackWebsite.txtStreet.Text = Nothing Then
                        '    _frmFeedbackWebsite.txtStreet.Focus()
                        '    _frmFeedbackWebsite.lblStreet.Visible = True
                        'ElseIf _frmFeedbackWebsite.txtCity.Text = "" Or _frmFeedbackWebsite.txtCity.Text = Nothing Then
                        '    _frmFeedbackWebsite.txtCity.Focus()
                        '    _frmFeedbackWebsite.lblCity.Visible = True
                        'ElseIf _frmFeedbackWebsite.txtZipCode.Text = "" Or _frmFeedbackWebsite.txtZipCode.Text = Nothing Then
                        '    _frmFeedbackWebsite.txtZipCode.Focus()
                        '    _frmFeedbackWebsite.lblZip.Visible = True
                        'ElseIf _frmFeedbackWebsite.txtCountry.Text = "" Or _frmFeedbackWebsite.txtCountry.Text = Nothing Then
                        '    _frmFeedbackWebsite.txtCountry.Focus()
                        '    _frmFeedbackWebsite.lblCountry.Visible = True

                    Else

                        '_frmFeedbackWebsite.autogenID = db.putSingleNumber("select max(autogenIDfeedback) from tbl_Kiosk_feedback")
                        'If _frmFeedbackWebsite.autogenID = "" Or _frmFeedbackWebsite.autogenID = 0 Then
                        '    _frmFeedbackWebsite.autogenID = My.Settings.firstGen
                        'Else
                        '    _frmFeedbackWebsite.autogenID = db.GenFeedback(_frmFeedbackWebsite.autogenID)
                        'End If

                        If _frmFeedbackWebsite.rbtHomeAddress.Checked = True Then
                            _frmFeedbackWebsite.rbtTypeAdd = "0"
                        End If
                        If _frmFeedbackWebsite.rbtBusinesAdd.Checked = True Then
                            _frmFeedbackWebsite.rbtTypeAdd = "1"
                        End If
                        If _frmFeedbackWebsite3.rbtYes.Checked = True Then
                            _frmFeedbackWebsite.rbtTagVisit = "1"
                        End If
                        If _frmFeedbackWebsite3.rbtNo.Checked = True Then
                            _frmFeedbackWebsite.rbtTagVisit = "0"
                        End If
                        If _frmFeedbackWebsite3.rbtUndc.Checked = True Then
                            _frmFeedbackWebsite.rbtTagVisit = "2"
                        End If

                        'db.sql = "insert into SSTRANSINFOTERMFB (SSNUM,EMAIL,ADDRESS_TYP,STREET,BRGAY,POST_CD,CNTRY,SSRATE1,SSRATE2,SSRATE3,SSRATE4,SSRATE5,SSRATE6,SSRATE7,VST_TAG,REASN_TAG,INFO_TAG,COMMNT_TAG,ENCODE_DT) values ('" & xtd.getCRN & "','" & _frmFeedbackWebsite.txtEmail.Text & _
                        '"','" & _frmFeedbackWebsite.rbtTypeAdd & "','" & _frmFeedbackWebsite.txtName.Text & "','" & _frmFeedbackWebsite.txtCity.Text & "','" & _frmFeedbackWebsite.txtZipCode.Text & "','" & _frmFeedbackWebsite.txtCountry.Text & _
                        '"','" & _frmFeedbackWebsite.num1 & "','" & _frmFeedbackWebsite.num2 & "','" & _frmFeedbackWebsite.num3 & "','" & _frmFeedbackWebsite.num4 & "','" & _frmFeedbackWebsite.num5 & "','" & _frmFeedbackWebsite.num6 & _
                        '"','" & _frmFeedbackWebsite.num7 & "','" & _frmFeedbackWebsite.rbtTagVisit & "','" & _frmFeedbackWebsite3.rtbWhy.Text & "','" & _frmFeedbackWebsite3.rtbWhat.Text & "','" & _frmFeedbackWebsite4.rtbIf.Text & "','" & Date.Today & "') "
                        'db.ExecuteSQLQuery(db.sql)

                        MsgBox("Thank you for your time! ", MsgBoxStyle.Information)
                        _frmFeedbackWebsite.pangClear()


                        getAffectedTable = "10025"

                        getAugitLogs()
                        clearFeedbackWebsite()
                    End If
            End Select
        Catch ex As Exception

        End Try
    End Sub
    Private Enum execOpt
        OLECMDEXECOPT_DODEFAULT = 0
        OLECMDEXECOPT_PROMPTUSER = 1
        OLECMDEXECOPT_DONTPROMPTUSER = 2
        OLECMDEXECOPT_SHOWHELP = 3

    End Enum

    Private Enum Exec
        OLECMDID_OPTICAL_ZOOM = 50
    End Enum
    Private Sub _frmSSSwebsite_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            'getCurrVersionIE()
            ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications

            'ServicePointManager.ServerCertificateValidationCallback = AddressOf ValidateServerCertificate

            'Control.CheckForIllegalCrossThreadCalls = False
            'trd = New Thread(AddressOf ThreadTask)
            'trd.IsBackground = True
            'trd.Start()

            'sssWebsiteLink = readSettings(xml_Filename, xml_path, "sssWebsiteLink")
            urlString = sssWebsiteLink
            getAdd = 0
            newTab()
            tagPage = "13.1"
            If _frmLoading.Visible = True Or _frmErrorFormWeb.Visible = True Then
            Else
                'Dim wbInstance As Object = WebBrowser1.ActiveXInstance
                'wbInstance.ExecWB(Exec.OLECMDID_OPTICAL_ZOOM, execOpt.OLECMDEXECOPT_DONTPROMPTUSER, 50, DBNull.Value)
            End If

            If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                Panel17.Width = Panel17.Width - 23
                Panel7.Width = Panel7.Width - 23
                Panel9.Width = Panel9.Width - 23
                Panel10.Width = Panel10.Width - 23
                Panel11.Width = Panel11.Width - 23
                Panel12.Width = Panel12.Width - 23
                Panel13.Width = Panel13.Width - 23
                Panel15.Width = Panel15.Width - 23


                Button2.Font = New Font(Button2.Font.Name, Button2.Font.Size - 3)
                Button1.Font = New Font(Button1.Font.Name, Button1.Font.Size - 3)
                Button7.Font = New Font(Button7.Font.Name, Button7.Font.Size - 3)
                Button5.Font = New Font(Button5.Font.Name, Button5.Font.Size - 3)
                Button6.Font = New Font(Button6.Font.Name, Button6.Font.Size - 3)
                Button9.Font = New Font(Button9.Font.Name, Button9.Font.Size - 3)
                Button8.Font = New Font(Button8.Font.Name, Button8.Font.Size - 3)
                Button4.Font = New Font(Button4.Font.Name, Button4.Font.Size - 3)

                Panel2.Height = Panel2.Height - 10
                Label1.Font = New Font(Label1.Font.Name, Label1.Font.Size - 3, Label1.Font.Style)
                Label1.Height = Label1.Height - 15
            End If

        Catch ex As Exception

        End Try
        WebPageLoaded1()


    End Sub

    Private Const BrowserKeyPath As String = "\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION"
    Private Sub getCurrVersionIE()
        'If My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", "*", Nothing) Is Nothing Then
        '    '    MsgBox("No value")
        '    Dim basekey As String = Microsoft.Win32.Registry.CurrentUser.ToString
        '    Dim value As Int32
        '    Dim thisAppsName As String = "*"

        '    value = "2328"

        '    Microsoft.Win32.Registry.SetValue(Microsoft.Win32.Registry.CurrentUser.ToString & BrowserKeyPath, _
        '                      "*", value, Microsoft.Win32.RegistryValueKind.DWord)
        '    '  MsgBox("Done Adding")
        'Else
        '    ' MsgBox("Already Added")
        'End If

        'revised by edel on Sept 20, 2019. to handle ie 11 requirement
        Dim basekey As String = Microsoft.Win32.Registry.CurrentUser.ToString
        Dim value As Int32
        Dim thisAppsName As String = "*"

        value = "11001"

        Microsoft.Win32.Registry.SetValue(Microsoft.Win32.Registry.CurrentUser.ToString & BrowserKeyPath,
                          "*", value, Microsoft.Win32.RegistryValueKind.DWord)
    End Sub
    Public Sub axbrowser_NavigateError1(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
        Try
            ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            If _frmFeedbackWebsite.Visible = True Then
                _frmLoading.Dispose()
                _frmErrorFormWeb.Dispose()
            Else
                _frmLoading.Dispose()
                '    _frmFeedbackWebsite.Visible = True
                If statusCode.ToString = -2146697211 Or statusCode = 504 Or statusCode = 404 Or statusCode = 7 Then
                    'MsgBox("")
                    'Panel3.Controls.Clear()
                    _frmErrorFormWeb.TopLevel = False
                    _frmErrorFormWeb.Parent = Panel3
                    _frmErrorFormWeb.Dock = DockStyle.Fill
                    TabControl1.Hide()
                    _frmErrorFormWeb.Show()
                    navError = 1
                End If
            End If


        Catch ex As Exception

        End Try
    End Sub


    Public Sub newTab()
        'Creates a new instance of the webbrowser
        'WebBrowser2 = New WebBrowser

        'Sets the webbrowser's properties

        TabControl1.Parent = Panel3
        TabControl1.Show()
        With WebBrowser1
            .Navigate(urlString)
            .Dock = DockStyle.Fill
        End With

        'Declare a new tabpage and add it to the tabcontrol
        'Dim tp As New TabPage("Tab Number " & TabControl1.TabCount + 1)
        'TabControl1.Controls.Add(tp)

        'Add the webbrowser to the tabpage
        TabPage1.Controls.Add(WebBrowser1)
        TabPage1.Text = "SSS WEBSITE"

        'Create a new instance of the NewWindow for the new webbrowser
        'AddHandler WebBrowser1.NewWindow, AddressOf BrowserNewWindow


        'AddHandler (WebBrowser1.Navigating), AddressOf WebPageLoaded1
        'Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        'AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1

        WebPageLoaded1()
    End Sub




    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        'Dim newUrl = WebBrowser1.Url.ToString
        'WebBrowser1.Navigate(newUrl)
        Try
            Select Case tagPage

                Case "13.1"
                    Try
                        Button7.Text = "REFRESH"
                        If _frmLoading.Visible = True Then
                            MsgBox("THE PAGE IS CURRENTLY LOADING", MsgBoxStyle.Information, "Information")
                        Else
                            Button7.Text = "REFRESH"
                            newTab()
                            WebBrowser1.Refresh(WebBrowserRefreshOption.Completely)
                            Dim newUrl = urlString
                            Dim AxWebBrowser2 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
                            AxWebBrowser2.Navigate(newUrl)
                            AxWebBrowser2.ExecWB(SHDocVw.OLECMDID.OLECMDID_REFRESH, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DODEFAULT)
                            navError = 0
                            isRefresh = True
                        End If

                    Catch ex As Exception

                    End Try
                Case "13.2"
                    _frmFeedbackWebsite.txtName.Clear()
                    _frmFeedbackWebsite.txtEmail.Clear()
                    _frmFeedbackWebsite.txtAddress1.Clear()
                    _frmFeedbackWebsite.txtZipCode.Clear()
                    _frmFeedbackWebsite.txtAddress2.Clear()
                    _frmFeedbackWebsite.txtCP.Clear()
                    _frmFeedbackWebsite.rbtBusinesAdd.Checked = False
                    _frmFeedbackWebsite.rbtHomeAddress.Checked = False
                Case "13.2.1"
                    _frmFeedbackWebsite.txtName.Clear()
                    _frmFeedbackWebsite.txtEmail.Clear()
                    _frmFeedbackWebsite.txtAddress1.Clear()
                    _frmFeedbackWebsite.txtZipCode.Clear()
                    _frmFeedbackWebsite.txtAddress2.Clear()
                    _frmFeedbackWebsite.txtCP.Clear()
                    _frmFeedbackWebsite.rbtBusinesAdd.Checked = False
                    _frmFeedbackWebsite.rbtHomeAddress.Checked = False

                Case "13.2.2"
                    _frmFeedbackWebsite1.chk1_1.Checked = False
                    _frmFeedbackWebsite1.chk1_2.Checked = False
                    _frmFeedbackWebsite1.chk1_3.Checked = False
                    _frmFeedbackWebsite1.chk1_4.Checked = False
                    _frmFeedbackWebsite1.chk1_5.Checked = False

                    _frmFeedbackWebsite1.chk2_1.Checked = False
                    _frmFeedbackWebsite1.chk2_2.Checked = False
                    _frmFeedbackWebsite1.chk2_3.Checked = False
                    _frmFeedbackWebsite1.chk2_4.Checked = False
                    _frmFeedbackWebsite1.chk2_5.Checked = False

                    _frmFeedbackWebsite1.chk3_1.Checked = False
                    _frmFeedbackWebsite1.chk3_2.Checked = False
                    _frmFeedbackWebsite1.chk3_3.Checked = False
                    _frmFeedbackWebsite1.chk3_4.Checked = False
                    _frmFeedbackWebsite1.chk3_5.Checked = False

                    _frmFeedbackWebsite1.chk4_1.Checked = False
                    _frmFeedbackWebsite1.chk4_2.Checked = False
                    _frmFeedbackWebsite1.chk4_3.Checked = False
                    _frmFeedbackWebsite1.chk4_4.Checked = False
                    _frmFeedbackWebsite1.chk4_5.Checked = False
                Case "13.2.3"
                    _frmFeedbackWebsite2.chk5_1.Checked = False
                    _frmFeedbackWebsite2.chk5_2.Checked = False
                    _frmFeedbackWebsite2.chk5_3.Checked = False
                    _frmFeedbackWebsite2.chk5_4.Checked = False
                    _frmFeedbackWebsite2.chk5_5.Checked = False

                    _frmFeedbackWebsite2.chk6_1.Checked = False
                    _frmFeedbackWebsite2.chk6_2.Checked = False
                    _frmFeedbackWebsite2.chk6_3.Checked = False
                    _frmFeedbackWebsite2.chk6_4.Checked = False
                    _frmFeedbackWebsite2.chk6_5.Checked = False

                    _frmFeedbackWebsite2.chk7_1.Checked = False
                    _frmFeedbackWebsite2.chk7_2.Checked = False
                    _frmFeedbackWebsite2.chk7_3.Checked = False
                    _frmFeedbackWebsite2.chk7_4.Checked = False
                    _frmFeedbackWebsite2.chk7_5.Checked = False
                Case "13.2.4"
                    _frmFeedbackWebsite3.rtbWhy.Clear()
                    _frmFeedbackWebsite3.rtbWhat.Clear()
                    _frmFeedbackWebsite3.rbtUndc.Checked = False
                    _frmFeedbackWebsite3.rbtYes.Checked = False
                    _frmFeedbackWebsite3.rbtNo.Checked = False
                Case "13.2.5"
                    _frmFeedbackWebsite4.rtbIf.Clear()
                Case Else


            End Select




        Catch ex As Exception

        End Try
    End Sub



    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            xtd.getRawFile()
            ' at.getModuleLogs(xtd.getCRN, "10025", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                SW.WriteLine(xtd.getCRN & "|" & getAffectedTable & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
            End Using
            Select Case tagPage

                Case "13.1"

                    Button6.Text = "SUBMIT"
                    Button5.Text = "CANCEL"
                    Button7.Text = "CLEAR"
                    Button8.Enabled = False
                    Button9.Enabled = False
                    Button5.Image = Image.FromFile(Application.StartupPath & "\images\CANCEL5555.png")
                    Button6.Image = Image.FromFile(Application.StartupPath & "\images\CHECK.png")
                    tagPage = "13.2"

                    Panel3.Controls.Clear()
                    _frmFeedbackWebsite.TopLevel = False
                    _frmFeedbackWebsite.Parent = Me.Panel3
                    _frmFeedbackWebsite.Dock = DockStyle.Fill
                    _frmFeedbackWebsite.Show()

                Case "13.2"

                    'Button6.Text = "NEXT"
                    'Button5.Text = "BACK"
                    Button7.Text = "REFRESH"
                    Button8.Enabled = True
                    Button9.Enabled = True
                    'Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK.png")
                    'Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT.png")
                    tagPage = "13.1"

                    Panel3.Controls.Clear()
                    TabControl1.Top = False
                    TabControl1.Parent = Me.Panel3
                    TabControl1.Dock = DockStyle.Fill
                    TabControl1.Show()


                Case Else

            End Select

        Catch ex As Exception
            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim
            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Form: Feedback SSS Website" & "', '" & "Click SSS Website Feedback button Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)

            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Feedback SSS Website" & "|" & "Click SSS Website Feedback button Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        'Panel3.Controls.Clear()

        Select Case tagPage

            Case "13.1"
                _frmLoading.Dispose()
                _frmWebBrowser.Dispose()

                TabControl1.Visible = False
                Panel3.Controls.Clear()

                _frmFeedbackWebsite.TopLevel = False
                _frmFeedbackWebsite.Parent = Me.Panel3
                _frmFeedbackWebsite.Dock = DockStyle.Fill
                _frmFeedbackWebsite.Show()
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\WEBICON.png")
                Button2.Text = "WEBSITE"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\CLEAR.png")

                Button7.Text = "CLEAR"
                Button5.Enabled = False
                Button8.Enabled = False
                Button9.Enabled = False
                Button6.Enabled = False


                tagPage = "13.2"

            Case "13.2"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                newTab()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"

            Case "13.2.1"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"
            Case "13.2.2"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"
            Case "13.2.3"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"
            Case "13.2.4"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"
            Case "13.2.5"
                Button1.Enabled = True
                Button2.Image = Image.FromFile(Application.StartupPath & "\images\FEEDBACK.png")
                Button2.Text = "FEEDBACK"
                Button7.Image = Image.FromFile(Application.StartupPath & "\images\REFRESH.png")
                Button7.Text = "REFRESH"
                Button5.Enabled = True
                Button8.Enabled = True
                Button9.Enabled = True
                Button6.Enabled = True
                ' Panel3.Controls.Clear()
                _frmFeedbackWebsite.Hide()
                TabControl1.Show()
                WebBrowser1.Navigate(urlString)
                WebBrowser1.Show()

                tagPage = "13.1"

        End Select



    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'For Each procT In System.Diagnostics.Process.GetProcessesByName("VBSoftKeyboard")
        '    procT.Kill()
        'Next
        'System.Diagnostics.Process.Start(Application.StartupPath & "\keyboard\" & "VBSoftKeyboard.exe")
        _frmMainMenu.ShowVirtualKeyboard(1)
    End Sub



    Private Sub VScrollBarAdv1_Scroll(ByVal sender As System.Object, ByVal e As System.Windows.Forms.ScrollEventArgs)
        'VScrollBarAdv1.Value = VScrollBarAdv1.Value
        'If VScrollBarAdv1.Value = 0 Or VScrollBarAdv1.Value > 0 Then
        '    Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        '    If getAdd = 0 Then
        '        getAdd += 10
        '        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        '        'My.Settings.Save()
        '    ElseIf VScrollBarAdv1.Value = 0 Or VScrollBarAdv1.Value > 0 Then
        '        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        '        getAdd += 10
        '    Else
        '        getAdd -= 10
        '        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        '        'My.Settings.Save()
        '    End If
        'End If


        AddHandler(WebBrowser1.Navigating), AddressOf WebPageLoaded1
        Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1
    End Sub

    Private Sub WebBrowser1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles WebBrowser1.GotFocus
        hDoc = WebBrowser1.Document
    End Sub

    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        'ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
        WebPageLoaded1()
        'WebBrowser1.ScriptErrorsSuppressed = True
        Dim obj1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        Dim zoomFactor As Integer = 90 '115
        Try
            'If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then zoomFactor = 100
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, zoomFactor, IntPtr.Zero)
        Catch ex As Exception
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(zoomFactor), CObj(IntPtr.Zero))
        End Try
        'obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, zoomFactor, IntPtr.Zero)

    End Sub

    Private Sub WebPageLoaded1()

        _frmLoading.Dispose()

        If _frmFeedbackWebsite.Visible = True Then
            WebBrowser1.Hide()

        Else
            WebBrowser1.Show()
            'WebBrowser1.ScriptErrorsSuppressed = True

            If WebBrowser1.ReadyState = WebBrowserReadyState.Interactive Then
                If navError = 0 Then
                    _frmMainMenu.DisposeForm(_frmLoading)
                    _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                    TabControl1.Dock = DockStyle.Fill
                    TabControl1.Show()
                    ' WebBrowser1.Parent = TabControl1
                    WebBrowser1.Show()
                    WebBrowser1.Dock = DockStyle.Fill

                    navError = 0


                End If

            ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
                If WebBrowser1.IsBusy = True Then ' And webBusy = 0 Then
                    _frmMainMenu.DisposeForm(_frmLoading)
                    _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                    _frmLoading.TopLevel = False
                    _frmLoading.Parent = Panel3
                    _frmLoading.Dock = DockStyle.Fill
                    _frmLoading.Show()
                    ' webBusy = 0 ' MEANS THE WEB IS STILL LOADING
                Else

                    _frmMainMenu.DisposeForm(_frmLoading)
                    _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                    TabControl1.Visible = True
                    TabControl1.Dock = DockStyle.Fill
                    TabControl1.Show()

                End If


            ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Loaded Then
                MsgBox("Loaded")
            ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                _frmLoading.TopLevel = False
                _frmLoading.Parent = Panel3
                _frmLoading.Dock = DockStyle.Fill
                _frmLoading.Show()

            ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Uninitialized Then
                TabControl1.Hide()
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                _frmLoading.TopLevel = False
                _frmLoading.Parent = Panel3
                _frmLoading.Dock = DockStyle.Fill
                _frmLoading.Show()
            Else
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorFormWeb)
                TabControl1.Visible = True
            End If


            Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
            AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1
        End If



    End Sub

    Private Sub DisposeForm(ByVal frm As Form)
        Try
            If Not frm Is Nothing Then frm.Dispose()
        Catch ex As Exception
        End Try
    End Sub


    Private Sub _frmSSSwebsite_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub


    Private Sub WebBrowser1_NewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles WebBrowser1.NewWindow

        Dim newUrl = sender.statustext

        e.Cancel = True
        With WebBrowser1
            .Navigate(newUrl)
            .Dock = DockStyle.Fill
        End With

        'Declare a new tabpage and add it to the tabcontrol


        'Dim tp As New TabPage("Tab Number " & TabControl1.TabCount + 1)
        'Dim tp2 As New TabPage("Tab Number " & TabControl1.TabCount - 1)
        'TabControl1.Controls.Remove(tp)
        'TabControl1.Controls.Add(tp)

        'Add the webbrowser to the tabpage

        TabPage1.Controls.Add(WebBrowser1)
        TabPage1.Text = "SSS WEBSITE"

        'Create a new instance of the NewWindow for the new webbrowser
        'AddHandler WebBrowser1.NewWindow, AddressOf BrowserNewWindow

        'AddHandler (WebBrowser1.Navigating), AddressOf WebPageLoaded1
        'Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        'AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1

    End Sub
    Private Sub BrowserNewWindow(ByVal sender As Object, ByVal e As System.ComponentModel.CancelEventArgs)

        ' newTab()
    End Sub



    Private Sub WebBrowser1_BeforeNavigate2(ByVal pDisp As Object, ByVal URL As Object, ByVal Flags As Object, ByVal TargetFrameName As Object, ByVal PostData As Object, ByVal Headers As Object, ByVal Cancel As Boolean)
        hDoc = Nothing
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        Try
            ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            WebBrowser1.Show()


            webBusy = 0


            'Dim href = WebBrowser1.Document
            'getSSandFullnameCardValdiation()


            getAdd = 0
            Dim newUrl = WebBrowser1.Url.ToString
            'MsgBox(newUrl)
            newUrl = newUrl.Trim

            AddHandler WebBrowser1.Document.Body.MouseDown, AddressOf Body_MouseDown

            WebBrowser1.IsWebBrowserContextMenuEnabled = False

            WebPageLoaded1()




            Select Case newUrl
                Case "http://done/"
                Case linkhome
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "View Homepage", "Home", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10016", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10016" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                Case linklogin
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "View Login page", "Login", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10030", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10030" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                Case linkregister
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "View Registration page", "Registration", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10031", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    'My.SSS
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10031" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                Case linkMySSS
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked My.SSS Link", "My.SSS", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10017", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10017" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                Case linkeservices
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked E-Service Link", "E-Service", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkepaymentlinks
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked E-Payment Link", "E-Payment", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkaboutsss
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked About My.SSS Link", "About My.SSS", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Membership
                Case linkMembership
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Membership Link", "Membership", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    ' at.getModuleLogs(xtd.getTempSSS, "10018", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10018" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                Case linkMemCoverage
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Coverage Link", "Coverage", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemCoveEmpr
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employers Link", "Employers", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemCoveEmpees
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employees Link", "Employees", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemCoveSelfEmp
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Self-Employed Link", "Self-Employed", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemCoveVoluntary
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Voluntary Link", "Voluntary", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemProcedure
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Procedure Link", "Procedure", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemDutyandRes
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Duties and Responsibilities Link", "Duties and Responsibilities", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemSchedofCon
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Schedule of Contribution Link", "Schedule of Contribution", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemEffofCov
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Effectivity of Covearage Link", "Effectivity of Covearage", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemFaqs
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked FAQS Link", "FAQS", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    '------------------

                    'Loans
                Case linkLoans
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Loans Link", "Loans", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10019", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10019" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using

                    'Member Loans
                Case linkMemberLoans
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Member Loans Link", "Member Loans", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                Case linkMemSalLoans
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Salary Loan Link", "Salary Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkMemSalLoansPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Salary Loan - Part 1 Link", "Salary Loan - Part 1", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkMemSalLoansPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Salary Loan - Part 2 Link", "Salary Loan - Part 2", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    '    'Emergency Loans
                Case linkEmergencyLoans
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Emergency Loan Link", "Emergency Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkEmLoansPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Emergency Loan - Part 1 Link", "Loans / Member Loan / Emergency Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkEmLoansPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Emergency Loan - Part 2 Link", "Loans / Member Loan / Emergency Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    '    '-------------------
                    'Housing Loans
                Case linkHousingLoans
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loans Link", "Housing Loans", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Pari-passu
                Case linkHouseParPassu
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Pari-passu Link", "Pari-passu", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkHouseParPassuPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Pari-passu - Part 1 Link", "Loans / Housing Loan / Pari-passu", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkHouseParPassuPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Pari-passu - Part 2 Link", "Loans / Housing Loan / Pari-passu", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    '    'Housing for Repairs and Improvement
                Case linkHLforRepairAndImpr
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan from Repairs and Improvement Link", "Housing Loan from Repairs and Improvement", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkHLforRepairAndImprPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan from Repairs and Improvement - Part 1 Link", "Loans / Housing Loan / Housing Loan from Repairs and Improvement", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkHLforRepairAndImprPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan from Repairs and Improvement  - Part 2 Link", "Loans / Housing Loan / Housing Loan from Repairs and Improvement", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    '    'Corporate Housing Program
                Case linkCorpHouseProg
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Corporate Housing Program Link", "Corporate Housing Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkCorpHouseProgPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Corporate Housing Program - Part 1 Link", "Loans / Housing Loan / Corporate Housing Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkCorpHouseProgPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Corporate Housing Program - Part 2 Link", "Loans / Housing Loan / Corporate Housing Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkCorpHouseProgPT3
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Corporate Housing Program - Part 3 Link", "Loans / Housing Loan / Corporate Housing Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    '    'Individual Housing
                Case linkIndiviHousing
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Individual Housing Link", "Individual Housing", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkIndiviHousingPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Individual Housing - Part 1 Link", "Loans / Housing Loan / Individual Housing", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkIndiviHousingPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Individual Housing - Part 2 Link", "Loans / Housing Loan / Individual Housing", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Housing Development Loan
                Case linkHousingDevLoan
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Development Loan Link", "Housing Development Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkHousingDevLoanPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Development Loan - Part 1 Link", "Loans / Housing Loan / Housing Development Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkHousingDevLoanPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Development Loan - Part 2 Link", "Loans / Housing Loan / Housing Development Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Apartment and Dormitory
                Case linkApartmenandDorm
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Apartment and Dormitory Link", "Apartment and Dormitory", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkApartmenandDormPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Apartment and Dormitory - Part 1 Link", "Loans / Housing Loan / Apartment and Dormitory", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkApartmenandDormPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Apartment and Dormitory - Part 2 Link", "Loans / Housing Loan / Apartment and Dormitory", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Housing Loan for OFW
                Case linkHouseLoanforOFW
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for OFW Link", "Housing Loan for OFW", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkHouseLoanforOFWPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for OFW - Part 1 Link", "Loans / Housing Loan / Housing Loan for OFW", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkHouseLoanforOFWPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for OFW - Part 2 Link", "Loans / Housing Loan / Housing Loan for OFW", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Housing Loan for Workers Organization Members
                Case linkHouseLoanforWOMem
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for Workers Organization Members Link", "Housing Loan for Workers Organization Members", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkHouseLoanforWOMemPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for Workers Organization Members - Part 1 Link", "Loans / Housing Loan / Housing Loan for Workers Organization Members", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkHouseLoanforWOMemPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Housing Loan for Workers Organization Members - Part 2 Link", "Loans / Housing Loan / Housing Loan for Workers Organization Members", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Assumption of Mortage
                Case linkAssumpofMortage
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Assumption of Mortage Link", "Assumption of Mortage", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkAssumpofMortagePT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Assumption of Mortage - Part 1 Link", "Loans / Housing Loan / Assumption of Mortage", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkAssumpofMortagePT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Assumption of Mortage - Part 2 Link", "Loans / Housing Loan / Assumption of Mortage", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Direct Housing Development Plan
                Case linkDirectHouseDevPlan
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Direct Housing Development Plan Link", "Direct Housing Development Plan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkDirectHouseDevPlanPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Direct Housing Development Plan - Part 1 Link", "Loans / Housing Loa / Direct Housing Development Plan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkDirectHouseDevPlanPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Direct Housing Development Plan - Part 2 Link", "Loans / Housing Loan / Direct Housing Development Plan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Business Loans
                Case linkBusinessLoan
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Business Loan Link", "Business Loan", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Sulong Program
                Case linkBusiSulongProgram
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sulong Program Link", "Sulong Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkBusiSulongProgramPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sulong Program - Part 1 Link", "Loans / Business Loan / Sulong Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkBusiSulongProgramPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sulong Program - Part 2 Link", "Loans / Business Loan / Sulong Program", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Social Development Loan Facility
                Case linkBusiSocDevLoanFacility
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Social Development Loan Facility Link", "Social Development Loan Facility", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Business Development Loan Facility
                Case linkBusiDevLoanFacility
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Business Development Loan Facility Link", "Business Development Loan Facility", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Benefits
                Case linkBenefits
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Benefits Link", "Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    ' at.getModuleLogs(xtd.getTempSSS, "10020", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10020" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using

                    'Social Security Benefits
                Case linkSocSecBenefits
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Social Security Benefits Link", "Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Social Security Benefits - Sickness Benefit
                Case linkSicknessBenefits
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sickness Benefit Link", "Sickness Benefit", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkSicknessBenefitsPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sickness Benefit - Part 1 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkSicknessBenefitsPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Sickness Benefit - Part 2 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Maternity
                Case linkMaterntity
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Maternity Link", "Maternity", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkMaterntityPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Maternity - Part 1 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkMaterntityPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Maternity - Part 2 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Retirement
                Case linkRetirement
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Retirement Link", "Retirement", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkRetirementPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Retirement - Part 1 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkRetirementPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Retirement - Part 2 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Disability
                Case linkDisability
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Disability Link", "Disability", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkDisabilityPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Disability - Part 1 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkDisabilityPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Disability - Part 2 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Death and Funeral
                Case linkDeathandFuneral
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Death and Funeral Link", "Death and Funeral", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkDeathandFuneralPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Death and Funeral - Part 1 Link", "Benefits / Social Security Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkDeathandFuneralPT2
                    'Employees Compensation
                Case linkEmployeesCompensation
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employees Compensation Link", "Employees Compensation", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Case linkEmployeesCompensationPT1
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employees Compensation - Part 1 Link", "Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkEmployeesCompensationPT2
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employees Compensation - Part 2 Link", "Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Case linkEmployeesCompensationPT3
                    '    at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Employees Compensation - Part 3 Link", "Benefits", tagPage, DateTime.Today.ToShortDateString, TimeOfDay)
                    'Corporate Profile
                Case linkCorporateProfile
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Corporate Profile Link", "Corporate Profile", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10021", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10021" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using

                    'Management Directory
                Case linkManagementDirectory
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Management Directory Link", "Management Directory", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS Mandate
                Case linkSSSMandate
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSS Mandate Link", "SSS Mandate", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Legislative History
                Case linkLegistativeHistory
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Legislative History Link", "Legislative History", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Branch And Offices
                Case linkBranchAndOffices
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Branch And Offices Link", "Branch And Offices", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Regional Listing
                Case linkRegionalListing
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Regional Listing Link", "Regional Listing", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Organization Links
                Case linkOrganization
                    'at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Organization Link", "Organization", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Publish Print Ads
                Case linkPublication
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Publication Link", "Publications", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'at.getModuleLogs(xtd.getTempSSS, "10022", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10022" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using
                    'Publish Print Ads
                Case linkPublishPrintAds
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Publish Print Ads Link", "Publish Print Ads", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Multimedia Materials
                Case linkMultimediaMaterials
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Multimedia Materials Link", "Multimedia Materials", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Laws and Regulations
                Case linkLawsAndRegulations
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Laws and Regulations Link", "Laws and Regulations", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'News and Updates
                Case linknewsAndUpdates
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked News and Updates Link", "News and Updates", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Facts and Figures
                Case linkFactsAndFigures
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Facts and Figures Link", "Facts and Figures", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Annual Report
                Case linkAnnualReport
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Annual Report Link", "Annual Report", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SS Commission
                Case linkSSCommission
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SS Commission Link", "SS Commission", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS Newsletter
                Case linkSSSnewsLetter
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSS Newsletter Link", "SSS Newsletter", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Message from the PCEO
                Case linkMessageFromThePCEO
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Message from the PCEO Link", "Message from the PCEO", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Other Services
                Case linkOtherServices
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Other Services Link", "Other Services", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    '  at.getModuleLogs(xtd.getTempSSS, "10023", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
                    Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                        SW.WriteLine(xtd.getCRN & "|" & "10023" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                    End Using

                    'SSSNet
                Case linkSSSnet
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSSNet Link", "SSSNet", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'Auto Debit Arrangement
                Case linkAutoDebitArrangement
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked Auto Debit Arrangement Link", "Auto Debit Arrangement", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS Flexi - Fund
                Case linkSSSflexiFund
                    '  at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSS Flexi - Fund Link", "SSS Flexi - Fund", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS ID
                Case linkSSSid
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSS ID Link", "SSS ID", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS DOWNLOAD FORMS
                Case linkDownloadSSSForms
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked SSS DOWNLOAD FORMS Link", "SSS DOWNLOAD FORMS", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'SSS FORM
                Case linkDownloadSSSForms
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Downloading SSS Form", "Downloading SSS FORM", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'FILL UP FORM
                Case linkFillForm
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Filling up SSS form", "FILLING UP FORM", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)
                    'R3 and LMS Project
                Case linkLmsProject
                    ' at.getModuleLogs(xtd.getTempSSS, fullnameFromCard, "FORM : SSS WEBSITE", "Clicked R3 and LMS Project Button", "FILLING UP FORM", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskName, kioskBranch, kioskIP, userType, kioskCluster, kioskGroup, printTag)



            End Select
            '   WebPageLoaded1()
        Catch ex As Exception
            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim
            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Form: SSS Website" & "', '" & "Clicking Link Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)

            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: SSS Website" & "|" & "Clicking Link Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try
    End Sub

    Private Sub WebBrowser1_Navigating(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles WebBrowser1.Navigating
        ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
        WebPageLoaded1()
    End Sub

    Private Sub Body_MouseDown(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        Select Case e.MouseButtonsPressed
            Case MouseButtons.Left
                Dim element As HtmlElement = WebBrowser1.Document.GetElementFromPoint(e.ClientMousePosition)

                Select Case element.Id
                    Case "lc_mem"
                        urlString = "https://member.sss.gov.ph/members/"
                        WebBrowser1.Navigate(urlString)
                    Case "lc_emp"
                        urlString = "http://employer.sss.gov.ph/employer/"
                        WebBrowser1.Navigate(urlString)
                    Case "lc_sbws"
                        urlString = "http://sbws.sss.gov.ph/sbws/"
                        WebBrowser1.Navigate(urlString)
                End Select
        End Select
    End Sub

End Class