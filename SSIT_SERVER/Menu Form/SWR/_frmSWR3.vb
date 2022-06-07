
Public Class _frmSWR3

    Public Shared isMemberRegistered As Boolean = True

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmSWR3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            If _frmMainMenu.IsAllowedToPrint() Then
                PrintReceipt()
                Dim xtd As New ExtractedDetails
                _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
            End If
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmSWR3_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub PrintReceipt()
        If SharedFunction.ShowMessage("Do you want to print the receipt?") = DialogResult.No Then Return

        Dim class1 As New PrintHelper
        Dim receiptMsg As New System.Text.StringBuilder
        receiptMsg.Append("YOU HAVE SUCCESSFULLY CREATED YOUR WEB " & vbNewLine)
        receiptMsg.Append("ACCOUNT. PLEASE CHECK YOUR EMAIL FOR THE " & vbNewLine)
        receiptMsg.Append("ACTIVATION LINK TO ACTIVATE YOUR ACCOUNT.  " & vbNewLine)
        receiptMsg.Append("IF YOU ARE UNABLE TO RECEIVE YOUR EMAIL  " & vbNewLine)
        receiptMsg.Append("FROM YOUR INBOX, PLEASE CHECK YOUR SPAM " & vbNewLine)
        receiptMsg.Append("OR JUNK FOLDER." & vbNewLine & vbNewLine)
        receiptMsg.Append("TRANSACTION REFERENCE NUMBER: " & _frmUserAuthentication.lblTransactionNo.Text & vbNewLine)

        class1.prt(class1.prtSimplifiedWebRegistration_Receipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, receiptMsg.ToString, "SIMPLIFIED WEB REGISTRATION", "Print"), _frmMainMenu.DefaultPrinterName)
        class1 = Nothing
    End Sub

End Class