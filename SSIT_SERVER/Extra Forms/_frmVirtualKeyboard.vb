Imports System.ComponentModel

Public Class _frmVirtualKeyboard


    Const WS_EX_NOACTIVATE As Integer = &H8000000
    Const WS_EX_TOPMOST As Integer = &H8
    Const WS_CHILD As Integer = &H40000000
    Const WS_BORDER As Integer = &H800000
    Const WS_DLGFRAME As Integer = &H400000
    Const WS_CAPTION As Integer = WS_BORDER Or WS_DLGFRAME
    Const WS_SYSMENU As Integer = &H80000
    Const WS_MAXIMIZEBOX As Integer = &H10000
    Const WS_MINIMIZEBOX As Integer = &H20000
    Const WS_THICKFRAME As Integer = &H40000
    Const WS_SIZEBOX As Integer = WS_THICKFRAME


    Private Const en As String = "En"
    Private langType As Integer = 1

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim ret As CreateParams = MyBase.CreateParams
            ret.Style = WS_CAPTION Or WS_SIZEBOX Or WS_SYSMENU Or WS_CHILD
            ret.ExStyle = ret.ExStyle Or (WS_EX_NOACTIVATE Or WS_EX_TOPMOST)
            Return ret
        End Get
    End Property

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

    End Sub


    Public Sub New(ByRef ctrl As Control)

        ' This call is required by the designer.
        InitializeComponent()
        ctrl.Focus()

        ' Add any initialization after the InitializeComponent() call.

    End Sub

    Private Sub btnSelectAll_Click(sender As Object, e As EventArgs)
        virtualKeyboard1.SelectAllCommand()
    End Sub

    Private Sub btnCopy_Click(sender As Object, e As EventArgs)
        virtualKeyboard1.CopyCommand()
    End Sub

    Private Sub btnPaste_Click(sender As Object, e As EventArgs)
        virtualKeyboard1.PasteCommand()
    End Sub

    Private Sub _frmVirtualKeyboard_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ''ResetLayout()
        Me.Width = Me.Width + 30

        'MessageBox.Show(SharedFunction.GetMonitorInch.ToString())

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            ''virtualKeyboard1.Width = virtualKeyboard1.Width - 20
            ''pbSelectAll.Left = pbSelectAll.Left - 20
            ''pbCopy.Left = pbCopy.Left - 20
            ''pbPaste.Left = pbPaste.Left - 20
            ''pbClose.Left = pbClose.Left - 20

            'virtualKeyboard1.Width = virtualKeyboard1.Width - 180
            virtualKeyboard1.Width = virtualKeyboard1.Width + 60
            pbSelectAll.Left = virtualKeyboard1.Width + 5
            pbCopy.Left = virtualKeyboard1.Width + 5
            pbPaste.Left = virtualKeyboard1.Width + 5
            pbClose.Left = virtualKeyboard1.Width + 5

            'pbSelectAll.Width = pbSelectAll.Width + 20
            'pbCopy.Width = pbCopy.Width + 20
            'pbPaste.Width = pbPaste.Width + 20
            'pbClose.Width = pbClose.Width + 20

        End If
    End Sub

    Private Sub ResetLayout()
        virtualKeyboard1.LayoutSettings = virtualKeyboard1.GetDefaultLayout()
    End Sub

    Private Sub pbSelectAll_Click(sender As Object, e As EventArgs) Handles pbSelectAll.Click
        virtualKeyboard1.SelectAllCommand()
    End Sub

    Private Sub pbCopy_Click(sender As Object, e As EventArgs) Handles pbCopy.Click
        virtualKeyboard1.CopyCommand()
    End Sub

    Private Sub pbPaste_Click(sender As Object, e As EventArgs) Handles pbPaste.Click
        virtualKeyboard1.PasteCommand()
    End Sub

    Private Sub _frmVirtualKeybousrfrmSelectv2ard_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        '_frmMainMenu,usrfrmSelectv2
        If Not _frmMainMenu.osk Is Nothing Then _frmMainMenu.osk = Nothing
    End Sub

    Private Sub pbClose_Click(sender As Object, e As EventArgs) Handles pbClose.Click
        Close()
    End Sub

End Class