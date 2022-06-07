Imports Oracle.DataAccess.Client
Public Class _frmACOPconfirmationDependent

    Dim printF As New printModule
    Dim xtd As New ExtractedDetails
    Dim inMat As New insertProcedure
    Dim db As New ConnectionString
    Dim at As New auditTrail
    Dim tempSSSHeader As String
    Dim txn As New txnNo
    Dim ssnum As String
    Dim xs As New MySettings


    Private Sub _frmACOPconfirmationDependent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            tagPage = "6"

            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.PrintControls(False)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

        'SharedFunction.ShowInfoMessage("SSS#: " & tempSSSHeader & ", lblSSSNo visible: " & lblSSSNo.Visible.ToString)
    End Sub

    Private Sub _frmACOPconfirmationDependent_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            _frmMainMenu.BackNextControls(False)
            _frmMainMenu.DisposeForm(_frmCalendar)
            GC.Collect()
            xtd.getRawFile()
            Dim fileTYP As Integer = xtd.checkFileType

            If fileTYP = 1 Then
                getURL = getPermanentURL & "controller?action=sss&id=" & xtd.getCRN
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & xtd.getCRN)
            ElseIf fileTYP = 2 Then
                getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)
            End If


            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmMainMenu.pnlWeb.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmWebBrowser.TopLevel = False
            _frmWebBrowser.Parent = _frmMainMenu.pnlWeb
            _frmWebBrowser.Dock = DockStyle.Fill
            _frmWebBrowser.Show()

            _frmMainMenu.BackNextControls(False)
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ' nikki03 ACOP
        My.Settings.transTag = "AC"
        Dim msgboxRes As Integer = MessageBox.Show("YOUR ACOP COMPLIANCE WILL BE SUBMITTED FOR PROCESSING." & vbNewLine & vbNewLine &
                                                    "DO YOU WANT TO CONTINUE? ", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If msgboxRes = DialogResult.No Then

        ElseIf msgboxRes = DialogResult.Yes Then
            If SharedFunction.DisablePinOrFingerprint Then
                submitACOP()
            Else
                CurrentTxnType = TxnType.AcopNoDep
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                xtd.getRawFile()
                If Not _frm2 Is Nothing Then _frmMainMenu.DisposeForm(_frm2)
                _frm2.CardType = CShort(xtd.checkFileType)
                _frm2.TopLevel = False
                _frm2.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frm2.Dock = DockStyle.Fill
                _frm2.Show()
            End If
        End If
    End Sub

    Public Sub submitACOP()
        Try


            _frmMainMenu.PrintControls(True)
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")


            _frmUserAuthentication.getTransacNum()
            Dim getBrCode As String = db.putSingleValue("Select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand
            Dim getDateToday As Date = Date.Today
            Dim getdateToday1 As String = getDateToday.ToString("MM-dd-yyyy")
            '_frmUserAuthentication.getTransacNum()
            Dim tranNum As String = _frmUserAuthentication.lblTransactionNo.Text
            'Dim ssnum1 As String = readSettings(xml_Filename, xml_path, "SSStempFile") 'lblSSSNo.Text.Replace("-", "")
            Dim ssnum1 As String = readSettings(xml_Filename, xml_path, "SS_Number")
            Dim ref1 As String = _frmMainMenu.refACOP

            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("pTRANID", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pSSNUM", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pREFNO", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pREFDT", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pBRANCH", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

            dbComm.Parameters("pTRANID").Value = tranNum
            dbComm.Parameters("pSSNUM").Value = ssnum1.Trim
            dbComm.Parameters("pREFNO").Value = ref1.Trim
            dbComm.Parameters("pREFDT").Value = getdateToday1
            dbComm.Parameters("pBRANCH").Value = getBrCode
            dbComm.CommandText = "PR_IK_INSERT_ACOP"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()
            Dim acopMsg As String = dbComm.Parameters("MSG").Value.ToString

            _frmMainMenu.BackNextControls(False)
            '_frmMainMenu.Button5.Enabled = False
            '_frmMainMenu.Button6.Enabled = False
            '_frmMainMenu.Button5.Text = "BACK"
            '_frmMainMenu.Button6.Text = "NEXT"
            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
            Dim tempSSS As String = xtd.getCRN()
            Dim resultType As Integer = xtd.checkFileType
            acopMsg = acopMsg.Trim


            Select Case resultType

                Case 1
                    If acopMsg = "Parameters not complete." Then
                        GC.Collect()
                        authentication = "MRG01"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"

                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        ' at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "INVALID_SSS_OR_CR_NUMBER" Then
                        GC.Collect()
                        authentication = "MRG01"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"
                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        ' at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "Invalid Date of Reporting." Then
                        GC.Collect()
                        authentication = "ACOP03"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"
                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "SUCCESS" Then
                        GC.Collect()
                        Dim dateCreated As String

                        dateCreated = Format(Now, "MM/dd/yyyy")
                        tagPage = "17"
                        authentication = "ACOP02"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")

                        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                        '   inMat.insertAcopSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE)
                        Dim sa As String = _frmUserAuthentication.certainDate
                        Dim cd As Date = CDate(_frmUserAuthentication.certainDate)
                        Dim CDmonth1 As String = cd.Month
                        Dim CDyear1 As String = cd.Year
                        Dim CDdate As String = CDmonth1 & "-" & "1" & "-" & CDyear1
                        inMat.insertAcop(ssnum, Date.Today.ToShortDateString, TimeOfDay, kioskIP, getbranchCoDE, getkiosk_cluster, getkiosk_group, CDdate, _frmUserAuthentication.lblTransactionNo.Text, kioskID)

                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"

                        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                        'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "ACTRANS1Logs.txt", True)
                        '    SW.WriteLine(xtd.getCRN & "," & xtd.getCRN & "," & Date.Today.ToShortDateString & "," & TimeOfDay & "," & kioskIP & "," & _
                        '                 getbranchCoDE & "," & getkiosk_cluster & "," & getkiosk_group & "," & _frmUserAuthentication.certainDate & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & _frmUserAuthentication.lblTransactionNo.Text & vbNewLine)
                        'End Using

                        _frmACOPdependent.Dispose()
                    End If
                Case 2
                    If acopMsg = "Parameters not complete." Then
                        GC.Collect()
                        authentication = "MRG01"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"
                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "INVALID_SSS_OR_CR_NUMBER" Then
                        GC.Collect()
                        authentication = "MRG01"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"
                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "Invalid Date of Reporting." Then
                        GC.Collect()
                        authentication = "ACOP03"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER"
                        Dim transNum As String = _frmUserAuthentication.lblTransactionNo.Text
                        Dim transDesc As String = acopMsg
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

                    ElseIf acopMsg = "SUCCESS" Then
                        GC.Collect()
                        Dim dateCreated As String

                        dateCreated = Format(Now, "MM/dd/yyyy")
                        authentication = "ACOP02"
                        tagPage = "17"
                        _frmMainMenu.btnPrint.PerformClick()
                        tagPage = "6"
                        _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                        _frmUserAuthentication.TopLevel = False
                        _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
                        _frmUserAuthentication.Dock = DockStyle.Fill
                        _frmUserAuthentication.Show()
                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")

                        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                        Dim cd As Date = CDate(_frmUserAuthentication.certainDate)
                        Dim CDmonth1 As String = cd.Month
                        Dim CDyear1 As String = cd.Year
                        Dim CDdate As String = CDmonth1 & "-" & "1" & "-" & CDyear1

                        ' inMat.insertAcopSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE)

                        inMat.insertAcop(SSStempFile, Date.Today.ToShortDateString, TimeOfDay, kioskIP, getbranchCoDE, getkiosk_cluster, getkiosk_group, CDdate, _frmUserAuthentication.lblTransactionNo.Text, kioskID)

                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"

                        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                        'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "ACTRANS1Logs.txt", True)
                        '    SW.WriteLine(xtd.getCRN & "," & xtd.getCRN & "," & Date.Today.ToShortDateString & "," & TimeOfDay & "," & kioskIP & "," & _
                        '                 getbranchCoDE & "," & getkiosk_cluster & "," & getkiosk_group & "," & _frmUserAuthentication.certainDate & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & _frmUserAuthentication.lblTransactionNo.Text & vbNewLine)
                        'End Using

                        _frmACOPdependent.Dispose()
                    End If
            End Select
            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
            End Using
            ' at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, "", "")
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

    End Sub


End Class