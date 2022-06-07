Public Class _frmHomeScreen
    Dim xs As New MySettings
    Dim firstRun As String '= readSettings(xml_Filename, xml_path, "firstRun")
    Dim xtd As New ExtractedDetails
    Dim tagDead As Integer
    'Dim sh As SharedFunction
    Public Function createFile()
        Dim filepath As String = Application.StartupPath & "\REF_NUM" ' & "\" & "Ref_Num\" & "REF_NUM.txt"  '  "C:\Users\Nikki Cassandra\Desktop\sample.txt"
        If System.IO.Directory.Exists(filepath) = False Then
            System.IO.Directory.CreateDirectory(filepath)
            System.IO.File.Create(filepath & "\REF_NUM.txt").Dispose()
        End If

    End Function


    Private Sub _frmHomeScreen_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        firstRun = readSettings(xml_Filename, xml_path, "firstRun")

        Dim kiosk_Ip As String = xs.getIPAddress()
        'Dim kiosk_Ip As String = xs.getIPAddress()
        Dim newKioskIP As String = kiosk_Ip.Substring(0, 3)

        If Not newKioskIP = "10." Then
            If firstRun <> "3" Then
                lblStatus.Visible = True
                lblStatus.Text = "* Please Contact Administrator to set kiosk settings! "
            End If
            isVPN = True
        Else
            ' If firstRun <> "3" Then 
            isVPN = False
            xs.getSQL()
            xs.getORACLE()
            xs.getKioskDetails(xs.getIPAddress)
            getKioskDetails()
            Me.Hide()
            SharedFunction.HomeScreen()
            'End If
        End If
    End Sub

    Private Sub _frmHomeScreen_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If (e.Alt AndAlso (e.KeyCode = Keys.S)) Then
            Panel4.Visible = True
        End If
        If (e.Alt AndAlso (e.KeyCode = Keys.C)) Then
            Panel4.Visible = False
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click

        Dim kioskAdminUser As String = readSettings(xml_Filename, xml_path, "kioskAdminUser")
        Dim kioskAdminPass As String = readSettings(xml_Filename, xml_path, "kioskAdminPass")

        If txtUser.Text = kioskAdminUser And txtPass.Text = kioskAdminPass Then
            GC.Collect()
            lblError.Visible = False

            Panel4.Visible = False
            txtUser.Text = ""
            txtPass.Text = ""

            '_frmBlock.Show()
            '_frmKioskSettings.ShowDialog()
            '_frmKioskSettings.TopMost = True
            '_frmBlock.Close()

            Panel3.Controls.Clear()
            _frmKioskSettings.TopLevel = False
            _frmKioskSettings.Parent = Panel3
            _frmKioskSettings.Dock = DockStyle.Fill
            _frmKioskSettings.Show()
        Else
            lblError.Visible = True
            lblError.Text = "* Username or Password is Incorrect! "
        End If

    End Sub
End Class