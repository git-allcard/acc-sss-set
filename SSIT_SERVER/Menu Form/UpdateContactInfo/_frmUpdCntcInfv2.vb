
Public Class _frmUpdCntcInfv2

    Dim navError As Integer
    Dim webBusy As Integer = 0

    Private scrollVar As Integer = 80

    Private webBrowserOrigTop As Integer = 0
    Private webBrowserOrigHeight As Integer = 0

    Private IsAlreadyZoomOut As Boolean = False
    Private pnlWebInitialTop As Integer = 0

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmUpdCntcInfv2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            webBrowserOrigTop = WebBrowser1.Top
            webBrowserOrigHeight = WebBrowser1.Height

            'pnlWebInitialTop = pnlWeb.Top
            'If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch And Not IsAlreadyZoomOut Then
            '    pbUp.Visible = True
            '    pbDown.Visible = True
            'End If
            'SharedFunction.ZoomFunction(True)

            Dim response() As String = Nothing

            'WebBrowser1.Navigate("http://10.101.141.196:3012/members/mdcr/pages/indexE4WES.jsp?token=" & SharedFunction.tokenDetailsResponse(SSStempFile).tokenid)
            'WebBrowser1.Navigate("http://10.141.249.18:8010/members/mdcr/pages/indexE4WES.jsp?token=" & SharedFunction.tokenDetailsResponse(SSStempFile).tokenid)

            'getAdd = 0

            If UpdateCntctInfoService_URL.Contains("token=") Then
                WebBrowser1.Navigate(UpdateCntctInfoService_URL & SharedFunction.tokenDetailsResponse(SSStempFile).tokenid)
            Else
                WebBrowser1.Navigate(UpdateCntctInfoService_URL)
            End If

            WebPageLoaded1()


            '_frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
            _frmMainMenu.DisposeForm(_frmLoading)

            '_frmMainMenu.BackNextControls(True, True)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
            Console.WriteLine(ex.Message)
        End Try
    End Sub

    Private Sub _frmUpdCntcInfv2_Generate_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub WebBrowser1_Navigated_1(sender As Object, e As WebBrowserNavigatedEventArgs) Handles WebBrowser1.Navigated
        WebBrowser1.IsWebBrowserContextMenuEnabled = False
    End Sub

    Private Sub WebBrowser1_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs)
        WebPageLoaded1()
    End Sub

    Private Sub WebBrowser1_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs)
        WebPageLoaded1()
    End Sub

    Private Sub WebPageLoaded1()
        '  _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        '
        ' If _frmFeedbackWebsite.Visible = True Then
        'WebBrowser1.Hide()

        ' Else
        'WebBrowser1.Show()
        WebBrowser1.ScriptErrorsSuppressed = True

        If WebBrowser1.ReadyState = WebBrowserReadyState.Interactive Then
            If navError = 0 Then
                _frmMainMenu.DisposeForm(_frmLoading)
                _frmMainMenu.DisposeForm(_frmErrorForm)
                Me.Show()
                navError = 0
            End If
            'MsgBox("Interactive")
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

                'UpdateCntctInfo_SuccessTxnNo = printF.GetFirstName(WebBrowser1)
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
            Me.Hide()
            _frmLoading.TopLevel = False
            _frmLoading.Parent = _frmMainMenu.splitContainerControl.Panel2

            _frmLoading.Dock = DockStyle.Fill
            _frmLoading.Show()
            'MsgBox("Uninitialized")
        Else
            _frmMainMenu.DisposeForm(_frmLoading)
            _frmMainMenu.DisposeForm(_frmErrorForm)
            Me.Show()
        End If


        Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1
        '  End If
    End Sub

    Public Sub axbrowser_NavigateError1(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
        Try
            'If _frmFeedbackWebsite.Visible = True Then
            '    _frmLoading.Dispose()
            '    _frmErrorForm.Dispose()
            'Else
            _frmMainMenu.DisposeForm(_frmLoading)

            ' _frmFeedbackWebsite.Visible = True
            If statusCode.ToString = -2146697211 Or statusCode = 504 Then
                'MsgBox("")
                pnlBody.Controls.Clear()
                _frmErrorForm.TopLevel = False
                _frmErrorForm.Parent = pnlBody
                _frmErrorForm.Dock = DockStyle.Fill
                _frmErrorForm.Show()
            End If
            'End If

        Catch ex As Exception

        End Try
    End Sub

    Private Sub WebBrowser1_DocumentCompleted(sender As Object, e As WebBrowserDocumentCompletedEventArgs) Handles WebBrowser1.DocumentCompleted
        AddHandler WebBrowser1.Document.Body.MouseDown, AddressOf Body_MouseDown

        If WebBrowser1.Url.AbsolutePath = "/members/mdcr/wesConfirm.html" Then
            'WebBrowser1.Top = WebBrowser1.Top - 0
            WebBrowser1.Height = webBrowserOrigHeight
        Else
            WebBrowser1.Top = 0
            WebBrowser1.Height = webBrowserOrigHeight
        End If

        'If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.twelveInch And Not IsAlreadyZoomOut Then
        '    'pnlWeb.Top -= 15
        '    'pnlWeb.Left -= 60
        '    IsAlreadyZoomOut = True
        '    InitialZoom -= 30
        '    PerformZoom(InitialZoom)
        'End If
    End Sub

    Private Sub Body_MouseDown(ByVal sender As Object, ByVal e As HtmlElementEventArgs)
        Select Case e.MouseButtonsPressed
            Case MouseButtons.Left
                Dim element As HtmlElement = WebBrowser1.Document.GetElementFromPoint(e.ClientMousePosition)
                Console.WriteLine(element.Id)
                Select Case element.Id
                    Case "cancelAction", "doneAction"
                        'WebBrowser1.document.Body.InnerHtml
                        'If SharedFunction.ShowMessage("Are you sure you want to leave?") = DialogResult.Yes Then
                        '    _frmMainMenu.btnInquiry_Click()
                        'End If
                        _frmMainMenu.btnInquiry_Click()
                        'Case "nextAction"
                        '    WebBrowser1.Top = 10
                        '    WebBrowser1.Height = webBrowserOrigHeight
                        'Case "chkAddrMail", "chkFrgnMail", "chkPhone", "chkMobile", "chkEmail"
                End Select

                'If element IsNot Nothing AndAlso "submit".Equals(element.GetAttribute("type"), StringComparison.OrdinalIgnoreCase) Then
                '    MessageBox.Show("button is clicked")
                'End If
        End Select
    End Sub

    Dim InitialZoom As Integer = 100

    Public Enum Exec
        OLECMDID_OPTICAL_ZOOM = 63
    End Enum

    Private Enum execOpt
        OLECMDEXECOPT_DODEFAULT = 0
        OLECMDEXECOPT_PROMPTUSER
        OLECMDEXECOPT_DONTPROMPTUSER
        OLECMDEXECOPT_SHOWHELP
    End Enum

    Private Sub PerformZoom(ByVal Value As Integer)
        Dim obj1 As SHDocVw.WebBrowser = DirectCast(Me.WebBrowser1.ActiveXInstance, SHDocVw.WebBrowser)

        Try
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(Value), CObj(IntPtr.Zero))
        Catch ex As Exception
            obj1.ExecWB(SHDocVw.OLECMDID.OLECMDID_OPTICAL_ZOOM, SHDocVw.OLECMDEXECOPT.OLECMDEXECOPT_DONTPROMPTUSER, CObj(Value), CObj(IntPtr.Zero))
        Finally
            SharedFunction.ZoomFunction(False)
        End Try        '
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs)
        ScrollUp()

        Return
        InitialZoom += 10
        PerformZoom(InitialZoom)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs)
        ScrollDown()
        Return

        InitialZoom -= 10
        PerformZoom(InitialZoom)
    End Sub

    Private Sub ScrollUp()
        'getAdd = 0
        'Me.WebBrowser1.Document.Window.ScrollTo(getAdd, 0)
        Try


            If getAdd = 0 Then

            Else
                getAdd -= 10
                Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                'My.Settings.Save()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub ScrollDown()
        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
        If getAdd = 0 Then
            getAdd += 10
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)

        Else
            Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
            getAdd += 10

        End If
        Return


        Try


            Select Case tagPage

                Case "13.1"
                    'chicha2 += 10
                    Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                    If getAdd = 0 Then
                        getAdd += 10
                        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                        'My.Settings.Save()
                    Else
                        Me.WebBrowser1.Document.Window.ScrollTo(0, getAdd)
                        getAdd += 10
                        'My.Settings.Save()
                    End If
                Case "13.2"
                    Dim posY As Integer
                    If posY > Panel1.VerticalScroll.Maximum - 0 Then
                        posY = Panel1.VerticalScroll.Maximum - 450
                    Else
                        posY += 450
                        Panel1.AutoScrollPosition = New Point(0, posY)
                    End If
            End Select
        Catch ex As Exception

        End Try

    End Sub

    Private Sub pbUp_Click(sender As Object, e As EventArgs) Handles pbUp.Click
        pnlWeb.Top += 20
        If pnlWebInitialTop = pnlWeb.Top Then
        ElseIf pnlWebInitialTop > pnlWeb.Top + 20 Then
        Else
            pnlWeb.Top += 20
        End If
    End Sub

    Private Sub pbDown_Click(sender As Object, e As EventArgs) Handles pbDown.Click
        pnlWeb.Top -= 20
    End Sub

    Private Sub Button1_Click_1(sender As Object, e As EventArgs)
        ScrollUpv2()
    End Sub

    Private Sub Button2_Click_1(sender As Object, e As EventArgs)
        ScrollDownv2()
    End Sub

    Private Sub _frmUpdCntcInfv2_FormClosing(sender As Object, e As FormClosingEventArgs) Handles Me.FormClosing
        _frmMainMenu.BackNextControls(False)
    End Sub

    Public Sub ScrollUpv2()
        WebBrowser1.Top = WebBrowser1.Top - scrollVar
        WebBrowser1.Height = WebBrowser1.Height + scrollVar
    End Sub

    Public Sub ScrollDownv2()
        If WebBrowser1.Top < webBrowserOrigTop Then
            WebBrowser1.Top = WebBrowser1.Top + scrollVar
            WebBrowser1.Height = WebBrowser1.Height - scrollVar
        End If
    End Sub


End Class