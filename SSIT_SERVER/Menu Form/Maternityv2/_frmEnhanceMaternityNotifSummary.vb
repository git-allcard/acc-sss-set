
Public Class _frmEnhanceMaternityNotifSummary

    Dim xtd As New ExtractedDetails
    Public transMatnotif As String
    Dim getAffectedTable As String
    Dim db As New ConnectionString

    Private Sub _frmEnhanceMaternityNotifSummary_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            xtd.getRawFile()
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnYes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnYes.Click
        Try
            'Procedure PR_MATNOTIF2 by ms.myla from SSS - april 3, 2014
            Dim result As Integer = MessageBox.Show("YOUR MATERNITY NOTIFICATION WILL BE SUBMITTED FOR POSTING. " & vbNewLine & vbNewLine & "DO YOU WANT TO CONTINUE? ", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then
            ElseIf result = DialogResult.Yes Then
                If SharedFunction.DisablePinOrFingerprint Then
                    submitMaternity()
                Else
                    CurrentTxnType = TxnType.MaternityNotification
                    _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                    xtd.getRawFile()
                    If Not _frm2 Is Nothing Then _frmMainMenu.DisposeForm(_frm2)
                    _frm2.CardType = CShort(xtd.checkFileType)
                    _frm2.TopLevel = False
                    _frm2.Parent = _frmMainMenu.splitContainerControl.Panel2
                    _frm2.Dock = DockStyle.Fill
                    _frm2.Show()
                End If
            Else

                GC.Collect()
                authentication = "MN01"
                tagPage = "17"
                _frmMainMenu.btnPrint.PerformClick()
                tagPage = "4"
                errorTag = "SUBMISSION OF MATERNITY NOTIFICATION"
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()
                _frmMainMenu.Button5.Enabled = False
                _frmMainMenu.Button6.Enabled = False

                _frmMainMenu.PrintControls(True)
                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
                '_frmUserAuthentication.lblFooter.Text = "Please contact administrator regarding this matter. "
                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = "AN ERROR ENCOUNTERED DURING REQUEST."
                Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
                'at.getModuleLogs(xtd.getCRN, "10027", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

            End If
            '_frmMaternityNotification.matnotifTag = 0

        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim
            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Form: Maternity Notificaion" & "', '" & "Click Maternity Notification Submit button Failed" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)
            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Maternity Notification" & "|" & "Click Maternity Notification Submit button Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try

    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNo.Click
        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        _frmEnhanceMaternityNotif.TopLevel = False
        _frmEnhanceMaternityNotif.Parent = _frmMainMenu.splitContainerControl.Panel2
        _frmEnhanceMaternityNotif.Dock = DockStyle.Fill
        _frmEnhanceMaternityNotif.Show()
    End Sub


    Public Sub submitMaternity()
        Try
            '_frmMaternityNotification.matnotifTag = 1

            xtd.getRawFile()

            'Dim getYear As DateTime = Date.Today.ToString("MM/dd/yyyy")
            'Dim getBrCode As String = db.putSingleValue("Select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            'Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
            'Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
            'Dim getKioskName As String = kioskName

            'getKioskName = getKioskName.Substring(0, 1)
            Dim txn As New txnNo

            Dim submitMatNotif = _frmEnhanceMaternityNotif.men.submitMatNotif
            If submitMatNotif.processFlag = "1" Then
                transMatnotif = txn.getnum(Date.Today.ToString("yyMMdd"), "MT")
                _frmUserAuthentication.getTransacNum()

                Dim noOfDays As Short = 0
                If lblNoOfDays.Text <> "" Then noOfDays = lblNoOfDays.Text

                Dim inMat As New insertProcedure
                inMat.insertEnhancedMatnotif(SSStempFile, _frmUserAuthentication.lblTransactionNo.Text, lblDatedelivery.Text, lblName.Text, lblRelationship.Text.Replace("'", "''"), noOfDays, submitMatNotif.transactionNo)

                Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                Dim getdate As String = Date.Today.ToString("ddMMyyyy")

                getAffectedTable = "10027"

                Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(SSStempFile & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                End Using

                authentication = "MNP01"
                tagPage = "17"
                _frmMainMenu.btnPrint.PerformClick()
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()

                _frmMainMenu.BackNextControls(False)
                _frmMainMenu.PrintControls(True)

                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"
            Else
                GC.Collect()
                authentication = "EMN01"
                authenticationMsg = submitMatNotif.returnMessage.ToUpper
                tagPage = "17"
                _frmMainMenu.btnPrint.PerformClick()
                tagPage = "4"
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                _frmUserAuthentication.TopLevel = False
                _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmUserAuthentication.Dock = DockStyle.Fill
                _frmUserAuthentication.Show()

                _frmMainMenu.BackNextControls(False)
                _frmMainMenu.PrintControls(True)

                _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "MATERNITY NOTIFICATION"

                Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                Dim transDesc As String = submitMatNotif.returnMessage.ToUpper
                Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                    SW.WriteLine(xtd.getCRN & "|" & "10027" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                End Using
            End If

            '_frmMaternityNotification.matnotifTag = 0
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()

            Dim errorLogs As String = ex.Message
            errorLogs = errorLogs.Trim

            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Form: Maternity Notification" & "|" & "Click Maternity Notification Submit button Failed" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & "**")
            End Using
        End Try

    End Sub

    Private Sub Panel3_Paint(sender As Object, e As PaintEventArgs) Handles Panel3.Paint

    End Sub

    Private Sub Label18_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label14_Click(sender As Object, e As EventArgs) Handles Label14.Click

    End Sub

    Private Sub Label10_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Label8_Click_1(sender As Object, e As EventArgs) Handles Label8.Click

    End Sub
End Class