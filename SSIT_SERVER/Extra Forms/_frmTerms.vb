
Public Class _frmTerms

    Public displayType As Short = 1 '1=terms and conditions, 2=list of branches, 3=documentary requirements for retirement
    Private currrentPage As Short = 1

    Private Enum execOpt
        OLECMDEXECOPT_DODEFAULT = 0
        OLECMDEXECOPT_PROMPTUSER = 1
        OLECMDEXECOPT_DONTPROMPTUSER = 2
        OLECMDEXECOPT_SHOWHELP = 3

    End Enum

    Private Enum Exec
        OLECMDID_OPTICAL_ZOOM = 50
    End Enum

    Dim zoomFactor As Integer = 100

    Private Sub _frmTerms_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'WebBrowser1.Navigate(Application.StartupPath & "\terms_and_conditions\terms_and_conditions.txt")
        'WebBrowser1.Navigate(tacFile)

        SharedFunction.ZoomFunction(True)

        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch Then
            WebBrowser1.Size = New Size(Panel4.Width - 5, Panel4.Height)
        Else
            WebBrowser1.Dock = DockStyle.Fill
        End If

        Select Case displayType
            Case 1
                pnlHide.Visible = False
                Label36.Text = "TERMS AND CONDITIONS"
                WebBrowser1.Navigate(SharedFunction.ViewPDF(tacFile))
            Case 2
                pnlHide.Visible = True
                Label36.Text = "LIST OF BRANCHES"
                WebBrowser1.Navigate(SharedFunction.ViewPDF(SSSListOfBranches_URL))
            Case 3
                pnlHide.Visible = False
                Label36.Text = "LIST OF DOCUMENTARY REQUIREMENTS FOR RETIREMENT BENEFIT"
                WebBrowser1.Navigate(SharedFunction.ViewPDF2(technicalRetirementReqDoc,, 100))
                'WebBrowser1.Height = 820
                'WebBrowser1.Navigate("https://www.sss.gov.ph/sss/DownloadContent?fileName=Benepisyo_sa_Pagreretiro_July_29_2019.pdf")
        End Select

        getAdd = 0
        Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try

            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

            '_frmSalaryLoan.TopLevel = False
            '_frmSalaryLoan.Parent = _frmMainMenu.splitContainerControl.Panel2
            '_frmSalaryLoan.Dock = DockStyle.Fill
            '_frmSalaryLoan.Show()

            Select Case displayType
                Case 1, 2
                    _frmSalaryLoanv2.TopLevel = False
                    _frmSalaryLoanv2.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmSalaryLoanv2.Dock = DockStyle.Fill
                    _frmSalaryLoanv2.Show()
                Case 3
                    _frmTechRetirementNoDaem.TopLevel = False
                    _frmTechRetirementNoDaem.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmTechRetirementNoDaem.Dock = DockStyle.Fill
                    _frmTechRetirementNoDaem.Show()
            End Select

        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub ExitForm()
        Try

            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

            '_frmSalaryLoan.TopLevel = False
            '_frmSalaryLoan.Parent = _frmMainMenu.splitContainerControl.Panel2
            '_frmSalaryLoan.Dock = DockStyle.Fill
            '_frmSalaryLoan.Show()

            Select Case displayType
                Case 1, 2
                    _frmSalaryLoanv2.TopLevel = False
                    _frmSalaryLoanv2.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmSalaryLoanv2.Dock = DockStyle.Fill
                    _frmSalaryLoanv2.Show()
                Case 3
                    _frmTechRetirementNoDaem.TopLevel = False
                    _frmTechRetirementNoDaem.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frmTechRetirementNoDaem.Dock = DockStyle.Fill
                    _frmTechRetirementNoDaem.Show()
            End Select

        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        'If currrentPage < 2 Then Return

        'Try
        '    currrentPage += 1
        '    WebBrowser1.Navigate(SharedFunction.ViewPDF2(technicalRetirementReqDoc, currrentPage, 100))
        'Catch ex As Exception
        'End Try


        ''Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        If getAdd = 0 Then
            getAdd += 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)


        Else
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
            getAdd += 130

        End If

    End Sub



    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        'If currrentPage < 2 Then Return

        'currrentPage -= 1
        'WebBrowser1.Navigate(SharedFunction.ViewPDF2(technicalRetirementReqDoc, currrentPage, 100))

        '''Dim htmlDoc As HtmlDocument = WebBrowser1.Document
        '''Dim scrollTop As Integer = htmlDoc.GetElementsByTagName("HTML")(0).ScrollLeft

        '''WebBrowser1.Document.Body.ScrollLeft = scrollTop
        ''WebBrowser1.Document.Body.FirstChild.ScrollIntoView(True)
        ''WebBrowser1.Document.Body.c

        If getAdd = 0 Then

        Else
            getAdd -= 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        End If
    End Sub

    Private IsAlreadyZoomIn As Boolean = False

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted

        'Me.WebBrowser1.Document.Window.ScrollTo(2, 0)

        'If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch And Not IsAlreadyZoomOut Then
        'If Not IsAlreadyZoomIn Then
        '    IsAlreadyZoomIn = True
        '    zoomFactor += 50
        '    PerformZoom(zoomFactor)
        '    'SharedFunction.ZoomFunction(False)
        'End If

        'End If
    End Sub

    Private Sub WebBrowser1_Navigated(ByVal sender As Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        Dim obj1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)

        'Try
        '    obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, zoomFactor, IntPtr.Zero)
        'Catch ex As Exception
        '    obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(zoomFactor), CObj(IntPtr.Zero))
        'End Try

        'Select Case displayType
        '    Case 1, 2
        '        obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, zoomFactor, IntPtr.Zero)
        '    Case 3
        '        obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(zoomFactor), CObj(IntPtr.Zero))
        'End Select
    End Sub

    Private Sub _frmTerms_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        SharedFunction.ZoomFunction(False)
    End Sub

    Dim InitialZoom As Integer = 100

    'Public Enum Exec
    '    OLECMDID_OPTICAL_ZOOM = 63
    'End Enum

    'Private Enum execOpt
    '    OLECMDEXECOPT_DODEFAULT = 0
    '    OLECMDEXECOPT_PROMPTUSER
    '    OLECMDEXECOPT_DONTPROMPTUSER
    '    OLECMDEXECOPT_SHOWHELP
    'End Enum

    Private Sub PerformZoom(ByVal Value As Integer)
        Dim obj1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)

        Try
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(Value), CObj(IntPtr.Zero))
        Catch ex As Exception
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(Value), CObj(IntPtr.Zero))
        End Try        '
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        'pnlHide.Top += 10
        'Label36.Text = String.Format("X: {0} Y: {1}", pnlHide.Location.X, pnlHide.Location.Y)
        ''zoomFactor += 50
        ''PerformZoom(zoomFactor)

        ''WebBrowser1.Navigate(SharedFunction.ViewPDF2(technicalRetirementReqDoc, 1, 100))
        'MessageBox.Show("Panel4 size " & Panel4.Size.ToString & " Webform size " & WebBrowser1.Size.ToString)
        'WebBrowser1.Height = WebBrowser1.Height - 30

        If getAdd = 0 Then

        Else
            getAdd -= 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        End If
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ''pnlHide.Left += 10
        ''Label36.Text = String.Format("X: {0} Y: {1}", pnlHide.Location.X, pnlHide.Location.Y)
        '''WebBrowser1.Navigate(SharedFunction.ViewPDF2(technicalRetirementReqDoc, 2, 100))
        'WebBrowser1.Height = WebBrowser1.Height + 30

        If getAdd = 0 Then
            getAdd += 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)


        Else
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
            getAdd += 130

        End If
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        If getAdd = 0 Then

        Else
            getAdd -= 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        End If
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        If getAdd = 0 Then
            getAdd += 130
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)


        Else
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
            getAdd += 130

        End If
    End Sub

End Class