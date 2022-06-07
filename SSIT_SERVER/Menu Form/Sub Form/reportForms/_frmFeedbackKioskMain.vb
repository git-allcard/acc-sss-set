Imports System.Threading
Public Class _frmFeedbackKioskMain
    Public trd As Thread
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


                '_frmBlock.Show()
                '_frmLoading.ShowDialog()
                '_frmLoading.TopMost = True
                '_frmBlock.Close()
                runTime()

            Catch ex As Exception
                'MsgBox("Time Settings is not Updated! ", MsgBoxStyle.Information, "Information")
                'Application.Exit()
                'System.Diagnostics.Process.Start(My.Settings.cardPath & "SSIT_HOME.exe")
            End Try
        Loop
    End Sub

    Private Sub _frmFeedbackKioskMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GC.Collect()
        Control.CheckForIllegalCrossThreadCalls = False
        trd = New Thread(AddressOf ThreadTask)
        trd.IsBackground = True
        trd.Start()

        Me.Panel4.Controls.Clear()
        _frmFeedbackKiosk.TopLevel = False
        _frmFeedbackKiosk.Parent = Me.Panel4
        _frmFeedbackKiosk.Dock = DockStyle.Fill
        _frmFeedbackKiosk.Show()

        tagPage = "16.1.0"

    End Sub
    Function Fileexists(ByVal fname) As Boolean
        Try
            If Dir(fname) <> "" Then _
        Fileexists = True _
        Else Fileexists = False
        Catch ex As Exception

        End Try
    End Function

    Sub DeleteFile(ByVal FileToDelete As String)
        Try
            If Fileexists(FileToDelete & "\*.*") Then 'See above
                SetAttr(FileToDelete, vbNormal)
                Kill(FileToDelete & "\*.*")
            End If
        Catch ex As Exception

        End Try
    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            _frmFeedbackKiosk.clearAll()
            Dim pathX1 As String = Application.StartupPath & "\" & "temp" & "\"
            DeleteFile(pathX1)

            trd.Abort()

            'Application.Exit()
            '' System.Diagnostics.Process.Start(Application.StartupPath & "\" & "SSIT_Home" & "\" & "SSIT_HOME.exe")
            'Dim startInfo As New ProcessStartInfo(Application.StartupPath & "\" & "SSIT_Home" & "\" & "SSIT_HOME.exe")
            ''startInfo.WindowStyle = ProcessWindowStyle.Hidden
            'startInfo.Arguments = 2
            'Process.Start(startInfo)
            'SharedFunction.ShowAppMainForm(Me)

            '_frmMainMenu.IsMainMenuActive = False
            '_frmMainMenu.Hide()
            'SharedFunction.ShowMainDefaultUserForm()
            'Main.Show()
        Catch ex As Exception
        Finally
            _frmMainMenu.IsMainMenuActive = False
            _frmMainMenu.Hide()
            'Me.Close()
            SharedFunction.ShowMainDefaultUserForm()
            Main.Show()
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        'For Each procT In System.Diagnostics.Process.GetProcessesByName("VBSoftKeyboard")
        '    procT.Kill()
        'Next
        'System.Diagnostics.Process.Start(Application.StartupPath & "\keyboard\" & "VBSoftKeyboard.exe")
        _frmMainMenu.ShowVirtualKeyboard(1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click

        Select Case tagPage

            Case "16.1.0"

                _frmFeedbackKiosk.txtName.Clear()
                _frmFeedbackKiosk.txtAddress1.Clear()
                _frmFeedbackKiosk.txtAddress2.Clear()
                _frmFeedbackKiosk.txtCP.Clear()
                _frmFeedbackKiosk.txtZipCode.Clear()
                _frmFeedbackKiosk.txtEmail.Clear()
                _frmFeedbackKiosk.rbtHomeAddress.CheckState = CheckState.Unchecked
                _frmFeedbackKiosk.rbtBusinesAdd.CheckState = CheckState.Unchecked


            Case "16.1.1"

                _frmFeedbackKiosk.txtName.Clear()
                _frmFeedbackKiosk.txtAddress1.Clear()
                _frmFeedbackKiosk.txtAddress2.Clear()
                _frmFeedbackKiosk.txtCP.Clear()
                _frmFeedbackKiosk.txtZipCode.Clear()
                _frmFeedbackKiosk.txtEmail.Clear()
                _frmFeedbackKiosk.rbtHomeAddress.CheckState = CheckState.Unchecked
                _frmFeedbackKiosk.rbtBusinesAdd.CheckState = CheckState.Unchecked

            Case "16.1.2"

                _frmFeedbackKiosk1.cba1.Checked = False
                _frmFeedbackKiosk1.cba2.Checked = False
                _frmFeedbackKiosk1.cba3.Checked = False
                _frmFeedbackKiosk1.cba4.Checked = False
                _frmFeedbackKiosk1.cba5.Checked = False

                _frmFeedbackKiosk1.cbb1.Checked = False
                _frmFeedbackKiosk1.cbb2.Checked = False
                _frmFeedbackKiosk1.cbb3.Checked = False
                _frmFeedbackKiosk1.cbb4.Checked = False
                _frmFeedbackKiosk1.cbb5.Checked = False

                _frmFeedbackKiosk1.cbc1.Checked = False
                _frmFeedbackKiosk1.cbc2.Checked = False
                _frmFeedbackKiosk1.cbc3.Checked = False
                _frmFeedbackKiosk1.cbc4.Checked = False
                _frmFeedbackKiosk1.cbc5.Checked = False

                _frmFeedbackKiosk1.cbd1.Checked = False
                _frmFeedbackKiosk1.cbd2.Checked = False
                _frmFeedbackKiosk1.cbd3.Checked = False
                _frmFeedbackKiosk1.cbd4.Checked = False
                _frmFeedbackKiosk1.cbd5.Checked = False

                _frmFeedbackKiosk1.cbe1.Checked = False
                _frmFeedbackKiosk1.cbe2.Checked = False
                _frmFeedbackKiosk1.cbe3.Checked = False
                _frmFeedbackKiosk1.cbe4.Checked = False
                _frmFeedbackKiosk1.cbe5.Checked = False

            Case "16.1.3"
                _frmFeedbackKiosk2.chkHelpNo.Checked = False
                _frmFeedbackKiosk2.chkHelpYes.Checked = False

                _frmFeedbackKiosk2.cbf1.Checked = False
                _frmFeedbackKiosk2.cbf2.Checked = False
                _frmFeedbackKiosk2.cbf3.Checked = False
                _frmFeedbackKiosk2.cbf4.Checked = False
                _frmFeedbackKiosk2.cbf5.Checked = False

                _frmFeedbackKiosk2.rtbComments.Clear()


            Case "16.1.4"

            Case "16.1.5"


        End Select

    End Sub
End Class