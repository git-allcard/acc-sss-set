
Public Class _frmTechRetirementApplyDate

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim tempSSSHeader As String

    Public formType As Short = 1
    Public mouseFocus As String

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private pnlEmployerHeight As Integer = 0
    Public avail_18mos_flg As String

    Private Sub _frmTechRetirementApplyDate_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub _frmTechRetirementApplyDate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmMainMenu.btnInquiry_Click()
    End Sub

    Public Function getSeparationDate() As String
        Return String.Format("{0}/{1}/{2}", txtdosm.Text, txtdosd.Text, txtdosy.Text)
    End Function

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not IsDate(getSeparationDate) Then
            SharedFunction.ShowWarningMessage("DATE OF SEPARATION IS REQUIRED.")
            Return
        End If

        btnCancel.Enabled = False
        btnNext.Enabled = False

        Select Case formType
            Case 1
                formType = 2
                _frmMainMenu.determineDoctg_OnlineRt(getSeparationDate)
                'Dim onlineRetirement As New OnlineRetirement
                'If onlineRetirement.determineDoctg_OnlineRt(SSStempFile, Date.Now.ToString("MM/dd/yyyy"), getSeparationDate) Then
                '    _frmTechRetirementEmpHist.flg_60 = onlineRetirement.memberClaimInformationEntitiesResponse(0).flg_60
                '    _frmTechRetirementEmpHist.type_of_retirement = onlineRetirement.memberClaimInformationEntitiesResponse(0).type_of_retirement
                '    _frmTechRetirementEmpHist.determined_doctg = onlineRetirement.memberClaimInformationEntitiesResponse(0).determined_doctg


                '    If _frmTechRetirementEmpHist.flg_60 = "0" Then
                '        Console.WriteLine(onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List)

                '        'If Not onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List Is Nothing Then
                '        If onlineRetirement.employmentHistory(SSStempFile, Date.Now.ToString("MM/dd/yyyy")) Then
                '            _frmTechRetirementEmpHist.grid.DataSource = onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List
                '            _frmTechRetirementEmpHist.btnNext.Focus()
                '            GC.Collect()
                '            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                '            _frmTechRetirementEmpHist.TopLevel = False
                '            _frmTechRetirementEmpHist.Parent = _frmMainMenu.splitContainerControl.Panel2
                '            _frmTechRetirementEmpHist.Dock = DockStyle.Fill
                '            _frmTechRetirementEmpHist.Show()
                '        Else
                '            SharedFunction.ShowWarningMessage("FAILED TO GET EMPLOYMENT HISTORY. ")
                '            _frmMainMenu.btnInquiry_Click()
                '        End If
                '        'Else
                '        '    Dim sw As New IO.StreamWriter("D:\WORK\ONLINE_RETIREMENT.txt", True)
                '        '    sw.WriteLine(SSStempFile & ",determineDoctg_OnlineRt,onlineRetirement.memberClaimInformationEntitiesResponse(0).eh_List=nothing")
                '        '    sw.Close()
                '        '    sw.Close()

                '        '    authentication = "SET002"
                '        '    authenticationMsg = "PLEASE BE INFORMED THAT THERE IS A DISCREPANCY IN YOUR EMPLOYMENT DATE. YOU MAY SEEK ASSISTANCE FOR CORRECTION OF YOUR EMPLOYMENT RECORD FROM OUR MEMBER SERVICE REPRESENTATIVE AT THE SERVICE COUNTER OF ANY SSS BRANCH NEAR YOU."
                '        '    _frmMainMenu.RedirectToserAuthenticationForm("ONLINE RETIREMENT APPLICATION", "ONLINE RETIREMENT APPLICATION", "10028")
                '        'End If
                '    End If
                'End If
        End Select

        btnCancel.Enabled = True
        btnNext.Enabled = True
    End Sub

    Private Sub txt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtdosm.Click, txtdosd.Click, txtdosy.Click
        Dim calv2 As New Calendar(txtdosm.Text, txtdosd.Text, txtdosy.Text)
        calv2.ShowCalendar()
        txtdosd.Text = calv2.Day
        txtdosm.Text = calv2.Month
        txtdosy.Text = calv2.Year
    End Sub

End Class