
Imports Oracle.DataAccess.Client

Public Class _frmACOPMonthPensionHistory

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Public ac As String
    Dim tempSSSHeader As String


    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        'wb.ScriptErrorsSuppressed = False
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmACOPMonthPensionHistory_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            SuppressScriptErrorsOnly()

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

    Private Sub _frmACOPMonthPensionHistory_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private docType As Short = 1

    Public Sub LoadInitialPage()
        docType = 2
        wb.Document.GetElementById("userName").SetAttribute("value", ACOPMonthPensionHistory_UserPass.Split("|")(0))
        wb.Document.GetElementById("password").SetAttribute("value", ACOPMonthPensionHistory_UserPass.Split("|")(1))
        wb.Document.All("Submit").InvokeMember("click")
    End Sub

    Public Sub LoadMemberStaticPage()
        docType = 3
        wb.Navigate(ACOPMonthPensionHistory_URL & "controller?action=memberDetails&print_ind=&messages=null&id=" & SSStempFile & "&search=Search")
    End Sub

    Private Sub LoadMonthlyPension()
        docType = 4
        wb.Navigate(ACOPMonthPensionHistory_URL & "controller?action=paymentHistory")

    End Sub

    'Private Sub wb_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted
    '    AddHandler(CType(sender, WebBrowser)).Document.Window.[Error], AddressOf Window_Error
    '    If docType = 1 Then
    '        Invoke(New Action(AddressOf LoadInitialPage))
    '    ElseIf docType = 2 Then
    '        Invoke(New Action(AddressOf LoadMemberStaticPage))
    '    ElseIf docType = 3 Then
    '        Invoke(New Action(AddressOf LoadMonthlyPension))
    '    End If
    'End Sub

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

    Private Sub SuppressScriptErrorsOnly()
        wb.ScriptErrorsSuppressed = True
        AddHandler wb.DocumentCompleted, AddressOf browser_DocumentCompleted
    End Sub

    Private Sub browser_DocumentCompleted(ByVal sender As Object, ByVal e As WebBrowserDocumentCompletedEventArgs)
        'AddHandler(CType(sender, WebBrowser)).Document.Window.[Error], AddressOf Window_Error
        'If docType = 1 Then
        '    Invoke(New Action(AddressOf LoadInitialPage))
        'ElseIf docType = 2 Then
        '    Invoke(New Action(AddressOf LoadMemberStaticPage))
        'ElseIf docType = 3 Then
        '    Invoke(New Action(AddressOf LoadMonthlyPension))
        'End If
    End Sub

    Private Sub Window_Error(ByVal sender As Object, ByVal e As HtmlElementErrorEventArgs)
        e.Handled = True
    End Sub

    Private Sub wb_Navigated(sender As Object, e As WebBrowserNavigatedEventArgs) Handles wb.Navigated
        wb.IsWebBrowserContextMenuEnabled = False
    End Sub

    Private Sub wb_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted

    End Sub
End Class