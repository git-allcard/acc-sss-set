
Imports System.IO
Imports System.Text

Public Class _frmSalaryLoanDisclosurev2

    Public memberStatus As String = ""
    Public employerSSNumber As String = ""
    Public employerName As String = ""
    Public employerAddress As String = ""
    Public erBrn As String = ""
    Public loanAmount As String = ""
    Public loanAmount2 As String = ""
    Public loanMonth As String = ""
    Public averageMsc As String = ""
    Public totalBalance As String = ""
    Public serviceCharge As String = ""
    Public netLoan As String = ""
    Public monthlyAmort As String = ""
    Public disbursementCode As String = ""
    Public bankCode As String = ""
    Public brstn As String = ""
    Public acctNo As String = ""
    Public fundingBank As String = ""
    Public advanceInterest As String = ""
    Public selectedBankAcct As String = ""

    Dim navError As Integer
    Dim webBusy As Integer = 0

    Private Sub _frmSalaryLoanDisclosurev2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            WebPageLoaded1()

            Me.KeyPreview = True

            'tagPage = "3.2"
            tagPage = "9"

            getAdd = 0

            If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
                WebBrowser1.Size = New Size(Panel6.Width - 5, Panel6.Height)
            Else
                WebBrowser1.Dock = DockStyle.Fill
            End If

            _frmMainMenu.PrintControls(False)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Hide()
        _frmBlock.Close()
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
        _frmBlock.Close()
        _frmMainMenu.IsMainMenuActive = True
        _frmMainMenu.Show()
    End Sub


    Private Sub Label4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        ShowTermsAndConditions()
    End Sub

    Public Sub ShowTermsAndConditions()
        Try
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmTerms.TopLevel = False
            _frmTerms.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmTerms.Dock = DockStyle.Fill
            _frmTerms.Show()
            _frmMainMenu.BackNextControls(False)
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            Dim sb As New StringBuilder
            sb.Append("Loanable Amount   P " & CDec(loanAmount2).ToString("N2") & vbNewLine & vbNewLine)
            sb.Append("Net Proceeds        P " & CDec(netLoan).ToString("N2") & vbNewLine & vbNewLine)
            sb.Append("Loan proceeds will be credited to your " & _frmSalaryLoanv2.cboAccount.Text & vbNewLine & vbNewLine & vbNewLine & vbNewLine)
            sb.Append("CERTIFICATION, AGREEMENT And PROMISSORY NOTE")

            '_frmSalaryLoanEmployerv2.lblMsg.Text = _frmSalaryLoanEmployerv2.lblMsg.Text.Replace("@amount", CDec(loanAmount2).ToString("N2")).Replace("@proceed", CDec(netLoan).ToString("N2")).Replace("@account", acctNo)
            _frmSalaryLoanEmployerv2.lblMsg.Text = sb.ToString
            _frmSalaryLoanEmployerv2.TopLevel = False
            _frmSalaryLoanEmployerv2.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmSalaryLoanEmployerv2.Dock = DockStyle.Fill
            _frmSalaryLoanEmployerv2.Show()
            Me.Hide()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            '_frmSalaryLoan.TopLevel = False
            '_frmSalaryLoan.Parent = _frmMainMenu.splitContainerControl.Panel2
            '_frmSalaryLoan.Dock = DockStyle.Fill
            '_frmSalaryLoan.Show()

            _frmSalaryLoanv2.TopLevel = False
            _frmSalaryLoanv2.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmSalaryLoanv2.Dock = DockStyle.Fill
            _frmSalaryLoanv2.Show()
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        'convertToText()
        'txt.getLoanDisclosure()
    End Sub
    'Private Sub convertToText()
    '    Try
    '        Dim richtextbox1 As New RichTextBox
    '        Dim ListBox1 As New ListBox
    '        WebBrowser1.ScriptErrorsSuppressed = True

    '        richtextbox1.Text = WebBrowser1.Document.Body.InnerText
    '        webText = Split(richtextbox1.Text, vbNewLine)
    '        Dim sb As New StringBuilder()
    '        For k = LBound(webText) To UBound(webText)
    '            ListBox1.Items.Add(webText(k))
    '        Next

    '        Using outfile As New StreamWriter(Application.StartupPath & "\sample.txt")
    '            For k = 1 To ListBox1.Items.Count - 1
    '                Dim itms As String
    '                itms = CStr(ListBox1.Items(k))
    '                outfile.WriteLine(itms)
    '            Next

    '        End Using
    '    Catch ex As Exception

    '    End Try
    'End Sub

    Public Sub WebPageLoaded1()
        WebBrowser1.ScriptErrorsSuppressed = True

        If WebBrowser1.ReadyState = WebBrowserReadyState.Interactive Then
            If navError = 0 Then
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorForm)
                Me.Show()
                navError = 0
            End If

        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Complete Then
            If WebBrowser1.IsBusy = False And webBusy = 0 Then
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorForm)
                _frmLoading.TopLevel = False
                Me.Hide()
                _frmLoading.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmLoading.Dock = DockStyle.Fill
                _frmLoading.Show()
                webBusy = 0 ' MEANS THE WEB IS STILL LOADING
            Else

                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorForm)
                Me.Show()
                webBusy = 1

            End If


        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Loaded Then
            MsgBox("Loaded")
        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Loading Then
            If navError = 0 Then
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorForm)

                Me.Show()
                navError = 0
            End If
        ElseIf WebBrowser1.ReadyState = WebBrowserReadyState.Uninitialized Then

            _frmMainMenu.DisposeForm(_frmLoading)
            _frmMainMenu.DisposeForm(_frmErrorForm)
            'Me.Hide()
            '_frmLoading.TopLevel = False
            '_frmLoading.Parent = _frmMainMenu.splitContainerControl.Panel2

            '_frmLoading.Dock = DockStyle.Fill
            '_frmLoading.Show()
            ''MsgBox("Uninitialized")
        Else
            _frmMainMenu.DisposeForm(_frmLoading)
            _frmMainMenu.DisposeForm(_frmErrorForm)
            'TabControl1.Dock = DockStyle.Fill
            Me.Show()
        End If

        Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1
        '  End If
    End Sub

    Public Sub axbrowser_NavigateError1(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
        Try

            _frmMainMenu.DisposeForm(_frmLoading)

            If statusCode.ToString = -2146697211 Or statusCode = 504 Then
                'MsgBox("")
                Panel6.Controls.Clear()
                _frmErrorForm.TopLevel = False
                _frmErrorForm.Parent = Panel6
                _frmErrorForm.Dock = DockStyle.Fill
                _frmErrorForm.Show()
            End If
            'End If

        Catch ex As Exception

        End Try
    End Sub


End Class