
Imports Oracle.DataAccess.Client

Public Class _frmTechRetirementERNotRegistered

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmTechRetirementERNotRegistered_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
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

    Private Sub _frmTechRetirementERNotRegistered_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    'Private Sub btnBack_Click(sender As Object, e As EventArgs)

    'End Sub

    'Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
    '    If grid.Rows.Count > 1 Then
    '        GC.Collect()
    '        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
    '        _frmTechRetirementApplyMineworker.TopLevel = False
    '        _frmTechRetirementApplyMineworker.Parent = _frmMainMenu.splitContainerControl.Panel2
    '        _frmTechRetirementApplyMineworker.Dock = DockStyle.Fill
    '        _frmTechRetirementApplyMineworker.Show()
    '    Else
    '        SharedFunction.ShowWarningMessage("EMPLOYMENT HISTORY IS EMPTY.")
    '    End If
    'End Sub



End Class