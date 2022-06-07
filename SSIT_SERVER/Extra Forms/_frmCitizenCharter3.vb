
'Imports DevExpress.XtraPdfViewer.PdfViewer
Imports System.Threading
Imports System.Windows.Forms

Public Class _frmCitizenCharter3
    Public trd As Thread
    Dim isLoading As Integer

    Dim navError As Integer
    Dim webBusy As Integer = 0

    '1=CITIZEN CHARTER, 2=UPDATE CONTACT INFO
    Public DOC_TYPE As Short = 1
    Public PDF_URL As String = ""

    Public Sub New()

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.

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
        Try
            Dim Res As Object = Nothing
            Dim MyWeb As Object
            MyWeb = Me.wb.ActiveXInstance
            MyWeb.ExecWB(Exec.OLECMDID_OPTICAL_ZOOM, execOpt.OLECMDEXECOPT_DONTPROMPTUSER, CObj(Value), CObj(IntPtr.Zero))
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
    End Sub

    Public Sub runTime()
        Dim getTime As String = TimeOfDay.ToString("tt hh:mm:ss")
        Dim getTimeTT As String = TimeOfDay.ToString("tt hh:mm:ss")
        getTimeTT = getTimeTT.Substring(0, 2)
        getTime = getTime.Substring(3, 8)

        Button3.Text = getTimeTT & " " & getTime

        '_frmFeedbackKiosk.Button2.Text = getTimeTT & " " & getTime

        Dim getDate As String = Date.Today.Day
        Dim getMonth As String = Date.Today.ToString("MMMM")
        getMonth = getMonth.Substring(0, 3)
        Dim getDay As String = Date.Today.ToString("dddd")
        lblDate.Text = getDate
        lblMonth.Text = getMonth
        lblDay.Text = getDay

        lblDate.Text = getDate
        lblMonth.Text = getMonth
        lblDay.Text = getDay
    End Sub

    Private Sub ThreadTask()
        Do
            Try

                'If isLoading = 0 Then
                '    '_frmBlock.Show()
                '    _frmFirstLoad.ShowDialog()
                '    ' _frmFirstLoad.TopMost = True
                '    '_frmBlock.Close()
                'ElseIf isLoading = 1 Then

                '    _frmFirstLoad.Dispose()
                'End If
                runTime()

            Catch ex As Exception

            End Try
        Loop
    End Sub



    Private Sub _frmCitizenCharter3_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        'Try
        '    Invoke(New Action(AddressOf WebPageLoaded1))
        '    'Me.KeyPreview = True

        '    'DoubleBuffered = True

        '    GC.Collect()
        '    getAdd = 0
        '    'Control.CheckForIllegalCrossThreadCalls = False
        '    'trd = New Thread(AddressOf ThreadTask)
        '    'trd.IsBackground = True
        '    'trd.Start()

        '    DoubleBuffered = True
        '    isLoading = 1
        'Catch ex As Exception
        '    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        '    _frmErrorForm.TopLevel = False
        '    _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
        '    _frmErrorForm.Dock = DockStyle.Fill
        '    _frmErrorForm.Show()
        'End Try


        Try
            DoubleBuffered = True
            GC.Collect()
            getAdd = 0
            Control.CheckForIllegalCrossThreadCalls = False
            trd = New Thread(AddressOf ThreadTask)
            trd.IsBackground = True
            trd.Start()

            'wb.ShowPropertiesDialog()
            If DOC_TYPE = 1 Then
                Label36.Text = "SSS CITIZEN'S CHARTER"
                wb.Navigate(Application.StartupPath & "\charter\CITIZENS_CHARTER.pdf")
                'wb.Navigate(SharedFunction.ViewPDF(Application.StartupPath & "\charter\CITIZENS_CHARTER.pdf"))
                'wb.Navigate(Application.StartupPath & "\viewPDF2.html")
            ElseIf DOC_TYPE = 2 Then
                Label36.Text = "ONLINE DATA CHANGE REQUEST"
                wb.Navigate(PDF_URL)
            End If

            Dim axbrowser As SHDocVw.WebBrowser = DirectCast(Me.wb.ActiveXInstance, SHDocVw.WebBrowser)
            AddHandler axbrowser.NavigateError, AddressOf axbrowser_NavigateError
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub wb_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles wb.Navigating


        AddHandler (wb.Navigating), AddressOf WebPageLoaded
        Dim axbrowser As SHDocVw.WebBrowser = DirectCast(Me.wb.ActiveXInstance, SHDocVw.WebBrowser)
        AddHandler axbrowser.NavigateError, AddressOf axbrowser_NavigateError


        '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        '_frmMainMenu.Panel2.Parent = _frmMainMenu.splitContainerControl.Panel2
        'Me.TopLevel = False
        'Me.Parent = _frmMainMenu.Panel2
        'Me.Dock = DockStyle.Fill
        'Me.Show()


    End Sub

    Public Sub axbrowser_NavigateError(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
        _frmLoading.Dispose()
        If statusCode.ToString = -2146697211 Or statusCode.ToString = 500 Then
            ''MsgBox("")

            '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            '_frmErrorForm.TopLevel = False
            '_frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            '_frmErrorForm.Dock = DockStyle.Fill
            ''Me.Dispose()
            '_frmErrorForm.Show()
            '' My.Settings.errorLoadTag = 1

            'editSettings(xml_Filename, xml_path, "errorLoadTag", "1")




        Else
            ' My.Settings.errorLoadTag = 0
            editSettings(xml_Filename, xml_path, "errorLoadTag", "0")
        End If

    End Sub
   
    Private Sub WebPageLoaded1()
        wb.ScriptErrorsSuppressed = True

        If wb.ReadyState = WebBrowserReadyState.Interactive Then
            If navError = 0 Then
                _frmLoading.Dispose()
                _frmErrorForm.Dispose()
                Me.Show()
                navError = 0
            End If
            'MsgBox("Interactive")
        ElseIf wb.ReadyState = WebBrowserReadyState.Complete Then
            If wb.IsBusy = False And webBusy = 0 Then
                _frmLoading.Dispose()
                _frmErrorForm.Dispose()
                _frmLoading.TopLevel = False
                Me.Hide()
                _frmLoading.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmLoading.Dock = DockStyle.Fill
                _frmLoading.Show()
                webBusy = 0 ' MEANS THE WEB IS STILL LOADING
            Else

                _frmLoading.Dispose()
                _frmErrorForm.Dispose()
                Me.Show()
                webBusy = 1
                'MsgBox("Complete")
            End If


        ElseIf wb.ReadyState = WebBrowserReadyState.Loaded Then
            MsgBox("Loaded")
        ElseIf wb.ReadyState = WebBrowserReadyState.Loading Then
            If navError = 0 Then
                _frmLoading.Dispose()
                _frmErrorForm.Dispose()
                '  TabControl1.Dock = DockStyle.Fill
                Me.Show()
                navError = 0
            End If
        ElseIf wb.ReadyState = WebBrowserReadyState.Uninitialized Then

            _frmLoading.Dispose()
            _frmErrorForm.Dispose()
            Me.Hide()
            _frmLoading.TopLevel = False
            _frmLoading.Parent = _frmMainMenu.splitContainerControl.Panel2

            _frmLoading.Dock = DockStyle.Fill
            _frmLoading.Show()
            'MsgBox("Uninitialized")
        Else
            _frmLoading.Dispose()
            _frmErrorForm.Dispose()
            'TabControl1.Dock = DockStyle.Fill
            Me.Show()
        End If

        'Dim axbrowser1 As SHDocVw.WebBrowser = DirectCast(wb.ActiveXInstance, SHDocVw.WebBrowser)
        'AddHandler axbrowser1.NavigateError, AddressOf axbrowser_NavigateError1
    End Sub

    Private Sub WebPageLoaded()
        _frmLoading.Dispose()
        ' Me.Dispose()
        ' Me.Hide()

        If wb.ReadyState = WebBrowserReadyState.Uninitialized Then


            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmMainMenu.pnlWeb.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmLoading.TopLevel = False
            _frmLoading.Parent = _frmMainMenu.pnlWeb
            _frmLoading.Dock = DockStyle.Fill
            _frmLoading.Show()
        Else
            Me.Show()

        End If




    End Sub

    'Public Sub axbrowser_NavigateError1(ByVal pDisp As Object, ByRef URL As Object, ByRef Frame As Object, ByRef statusCode As Object, ByRef Cancel As Boolean)
    '    Try
    '        _frmLoading.Dispose()
    '        If statusCode.ToString = -2146697211 Or statusCode = 504 Then
    '            pnlPDF.Controls.Clear()
    '            _frmErrorForm.TopLevel = False
    '            _frmErrorForm.Parent = pnlPDF
    '            _frmErrorForm.Dock = DockStyle.Fill
    '            _frmErrorForm.Show()
    '        End If
    '    Catch ex As Exception
    '    End Try
    'End Sub

    'Private Sub wb_Navigating(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatingEventArgs) Handles wb.Navigating
    '    WebPageLoaded1()
    'End Sub

    'Private Sub WebBrowser1_Navigated(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserNavigatedEventArgs) Handles wb.Navigated
    '    WebPageLoaded1()
    'End Sub

    Private Sub WebBrowser1_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs) Handles wb.DocumentCompleted

    End Sub
   
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If DOC_TYPE = 1 Then
            _frmMainMenu.IsMainMenuActive = False
            _frmMainMenu.Hide()
            'Me.Close()
            SharedFunction.ShowMainDefaultUserForm()
            Main.Show()
        ElseIf DOC_TYPE = 2 Then
            Close()
        End If
    End Sub

    Private Sub btnZoomIN_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomIN.Click
        InitialZoom += 10
        PerformZoom(InitialZoom)
    End Sub

    Private Sub btnZoomOUT_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnZoomOUT.Click
        InitialZoom -= 10
        PerformZoom(InitialZoom)
    End Sub

End Class