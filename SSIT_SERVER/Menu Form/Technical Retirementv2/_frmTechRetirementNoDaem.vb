
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementNoDaem


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        If Not _frmTechRetirementApplyDate Is Nothing Then
            _frmTechRetirementApplyDate.formType = 1
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmTechRetirementApplyDate.TopLevel = False
            _frmTechRetirementApplyDate.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmTechRetirementApplyDate.Dock = DockStyle.Fill
            _frmTechRetirementApplyDate.Show()
        Else
            _frmMainMenu.btnInquiry_Click()
        End If
    End Sub

    Private Sub _frmTechRetirementNoDaem_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub _frmTechRetirementNoDaem_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub RequirementLink()
        Dim link As String = "https://www.sss.gov.ph/sss/DownloadContent?fileName=Benepisyo_sa_Pagreretiro_July_29_2019.pdf"
    End Sub

    Private Sub link2_LinkClicked(sender As Object, e As EventArgs) Handles link2.LinkClicked
        _frmSalaryLoanv2.ShowTermsAndConditions(3)
    End Sub

    Private Sub link2_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles link2.LinkClicked

    End Sub
End Class