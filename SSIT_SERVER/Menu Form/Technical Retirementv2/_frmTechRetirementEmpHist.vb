
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementEmpHist


    Public ac As String
    Public seqNo As String = ""
    Public prevLoanAmount As Double = 0
    Public serviceCharge As Double = 0
    Public memberStatus As String = ""

    Public memberType As String = ""
    Public employerSSNumber As String = ""
    Public employerName As String = ""
    Public employerERBR As String = ""

    Public flg_60 As String = ""
    Public type_of_retirement As String = ""
    Public determined_doctg As String = ""
    Public separationDate As String = ""

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

    Private Sub _frmTechRetirementEmpHist_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub _frmTechRetirementEmpHist_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If grid.Rows.Count > 1 Then
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmTechRetirementApplyMineworker.TopLevel = False
            _frmTechRetirementApplyMineworker.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmTechRetirementApplyMineworker.Dock = DockStyle.Fill
            _frmTechRetirementApplyMineworker.Show()
        Else
            SharedFunction.ShowWarningMessage("EMPLOYMENT HISTORY IS EMPTY.")
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