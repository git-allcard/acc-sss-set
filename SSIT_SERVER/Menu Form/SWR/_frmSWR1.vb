
Public Class _frmSWR1

    Public Shared isMemberRegistered As Boolean = True

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmSWR1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim memberNotRegisteredMsg As String = "To continue with the Simplified Web Registration, please make sure that your Contact Information record with SSS is updated. To check, please tap ""Updating Of Contact Information"" at left panel." & vbNewLine & vbNewLine & "If your Contact Information is updated, tap PROCEED."
            If Not isMemberRegistered Then
                lblMessage.Text = memberNotRegisteredMsg
                Panel6.Visible = True
                _frmMainMenu.PrintControls(False)
            End If

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

    Private Sub _frmSWR1_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        '_frmSWR2.TopLevel = False
        '_frmSWR2.Parent = _frmMainMenu.splitContainerControl.Panel2
        '_frmSWR2.Dock = DockStyle.Fill
        '_frmSWR2.Show()
        'Me.Hide()
        _frmSWR2v2.TopLevel = False
        _frmSWR2v2.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmSWR2v2.Dock = DockStyle.Fill
        _frmSWR2v2.Show()
        Me.Hide()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        _frmMainMenu.btnInquiry_Click()
    End Sub

End Class