Imports Oracle.DataAccess.Client

Public Class _frmAcopSummary
    Dim db As New ConnectionString
    Dim inmat As New insertProcedure
    Dim xtd As New ExtractedDetails
    Dim printF As New printModule
    Dim tempSSSHeader As String
    Public dep_stat1 As String
    Public typRPT As String
    Dim relType As String
    Dim txn As New txnNo
    Public acopTrans As String
    Dim at As New auditTrail
    'Dim getTransNum As String
    Dim ssnum As String
    Dim xs As MySettings
    Dim workstation As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            transTag = "AC"

            Dim result As Integer = MessageBox.Show("YOUR ACOP COMPLIANCE WILL BE SUBMITTED FOR PROCESSING." & vbNewLine & vbNewLine & _
                                                    "DO YOU WANT TO CONTINUE? ", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If result = DialogResult.No Then

            ElseIf result = DialogResult.Yes Then

                If SharedFunction.DisablePinOrFingerprint Then
                    submitAcopDep()
                Else
                    CurrentTxnType = TxnType.AcopDep
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

        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Sub submitAcopDep()
        Try
            ' Dim sa As String = tagPage
            _frmMainMenu.PrintControls(True)
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")

            xtd.getRawFile()
            Dim tempSSS As String = xtd.getCRN()
            Dim resultType As Integer = xtd.checkFileType

            _frmUserAuthentication.getTransacNum()

            Dim getBrCode As String = db.putSingleValue("Select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
            Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
            Dim dbComm As OracleCommand
            Dim getDateToday As Date = Date.Today
            Dim getdateToday1 As String = getDateToday.ToString("MM-dd-yyyy")

            Dim tranNum As String = _frmUserAuthentication.lblTransactionNo.Text
            dbConn.Open()
            dbComm = dbConn.CreateCommand
            dbComm.Parameters.Add("pTRANID", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pSSNUM", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pREFNO", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pREFDT", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("pBRANCH", OracleDbType.Varchar2, 30).Direction = ParameterDirection.Input
            dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output

            dbComm.Parameters("pTRANID").Value = _frmUserAuthentication.lblTransactionNo.Text
            dbComm.Parameters("pSSNUM").Value = UsrfrmPageHeader1.lblSSSNo.Text.Replace("-", "")
            dbComm.Parameters("pREFNO").Value = _frmMainMenu.refACOP
            dbComm.Parameters("pREFDT").Value = getdateToday1
            dbComm.Parameters("pBRANCH").Value = getBrCode
            dbComm.CommandText = "PR_IK_INSERT_ACOP"
            dbComm.CommandType = CommandType.StoredProcedure
            dbComm.ExecuteNonQuery()
            dbConn.Close()
            Dim acopMsg As String = dbComm.Parameters("MSG").Value.ToString

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
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
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
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        ' at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

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
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        ' at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

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
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                        Dim acopDate As Date = _frmUserAuthentication.finalDateAcop
                        Select Case _frmACOPdependent.dpt_stat

                            Case 1
                                relType = "M"
                            Case 2
                                relType = "D"
                            Case 3
                                relType = "E"
                            Case Else
                                relType = ""

                        End Select
                        ' inmat.insertAcopSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE)
                        savDep()
                        Dim cd As Date = CDate(acopDate)
                        Dim CDmonth1 As String = cd.Month
                        Dim CDyear1 As String = cd.Year
                        Dim CDdate As String = CDmonth1 & "-" & "1" & "-" & CDyear1
                        inmat.insertAcop(ssnum, Date.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, getbranchCoDE, getkiosk_cluster, getkiosk_group, CDdate, _frmUserAuthentication.lblTransactionNo.Text, kioskID)
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                        '_frmUserAuthentication.lblFooter.Text = "Transaction will be forwarded to the Pension Monitoring Module for processing. Report on marriage/employment/death of dependents will be forwarded to PAD and BenAd Division."
                        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        End Using
                        _frmACOPconfirmationDependent.Dispose()

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
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

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

                        '  at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
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
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
                        End Using
                        'at.getModuleLogs(xtd.getCRN, "10029", tagPage, DateTime.Today.ToShortDateString, TimeOfDay, My.Settings.kioskIP, kioskBranch, kioskCluster, kioskGroup, userType, printTag, transNum, transDesc)

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
                        '_frmMainMenu.Button5.Text = "BACK"
                        '_frmMainMenu.Button6.Text = "NEXT"
                        '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
                        '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")

                        _frmMainMenu.BackNextControls(False)
                        '_frmMainMenu.Button5.Enabled = False
                        '_frmMainMenu.Button6.Enabled = False
                        _frmMainMenu.PrintControls(True)
                        '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
                        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
                        Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
                        Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
                        Dim acopDate As Date = _frmUserAuthentication.finalDateAcop
                        'argie102
                        Select Case _frmACOPdependent.dpt_stat

                            Case 1
                                relType = "M"
                            Case 2
                                relType = "D"
                            Case 3
                                relType = "E"
                            Case Else
                                relType = ""

                        End Select

                        '    inmat.insertAcopSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE)
                        savDep()
                        Dim cd As Date = CDate(acopDate)
                        Dim CDmonth1 As String = cd.Month
                        Dim CDyear1 As String = cd.Year
                        Dim CDdate As String = CDmonth1 & "-" & "1" & "-" & CDyear1
                        '  inmat.insertDependentSSS(_frmUserAuthentication.lblTransactionNo.Text, xtd.getCRN, _frmMainMenu.refACOP, getbranchCoDE, _
                        '_frmMainMenu.depAcop, _frmMainMenu.rcodeAcop, relType, "")
                        inmat.insertAcop(ssnum, Date.Today.ToShortDateString, TimeOfDay, kioskIP, getbranchCoDE, getkiosk_cluster, getkiosk_group, CDdate, _frmUserAuthentication.lblTransactionNo.Text, kioskID)
                        _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "ANNUAL CONFIRMATION OF PENSIONER (ACOP)"
                        '_frmUserAuthentication.lblFooter.Text = "Transaction will be forwarded to the Pension Monitoring Module for processing. Report on marriage/employment/death of dependents will be forwarded to PAD and BenAd Division."
                        Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
                            SW.WriteLine(xtd.getCRN & "|" & "10029" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & My.Settings.kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & "" & "|" & "" & vbNewLine)
                        End Using
                        _frmACOPconfirmationDependent.Dispose()

                    End If
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

    Private Sub savDep()
        Try
            Dim dep_name As String
            Dim depExist As Boolean



            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim dep_date As String
            Dim ctr As Integer
            Dim tme As String = Format(Now, "t")


            For k = 0 To dgvMem.Rows.Count - 1
                dep_name = dgvMem.Rows(k).Cells(0).Value

                If dgvMem.Rows(k).Cells(1).Value = "DATE OF MARRIAGE" Then
                    dep_stat1 = 1
                    typRPT = "DATE OF MARRIAGE"
                ElseIf dgvMem.Rows(k).Cells(1).Value = "DATE OF DEATH" Then
                    dep_stat1 = 2
                    typRPT = "DATE OF DEATH"
                ElseIf dgvMem.Rows(k).Cells(1).Value = "DATE OF EMPLOYMENT" Then
                    dep_stat1 = 3
                    typRPT = "DATE OF EMPLOYMENT"
                Else
                    dep_stat1 = 0
                    typRPT = "NO STATUS"
                End If

                If db.checkExistence("select DPDNAME from SSTRANSACOPAD where DPDNAME = '" & dep_name & "' and SSNUM = '" & ssnum & "' ") = True Then
                    depExist = True
                Else
                    depExist = False
                End If

                dep_date = dgvMem.Rows(k).Cells(2).Value
                If depExist = True Then ' update
                    If (dgvMem.Rows(k).Cells(1).Value <> "" Or dgvMem.Rows(k).Cells(1).Value <> Nothing) And (dgvMem.Rows(k).Cells(2).Value <> "" Or dgvMem.Rows(k).Cells(2).Value <> Nothing) Or (dgvMem.Rows(k).Cells(1).Value <> "" Or dgvMem.Rows(k).Cells(1).Value <> Nothing) Or (dgvMem.Rows(k).Cells(2).Value = "" Or dgvMem.Rows(k).Cells(2).Value = Nothing) Then
                        inmat.updateDep(ssnum, dep_name, dep_stat1, getbranchCoDE, Date.Today, tme, typRPT, dep_date)
                    End If
                Else ' save the dependents details
                    If (dgvMem.Rows(k).Cells(1).Value <> "" Or dgvMem.Rows(k).Cells(1).Value <> Nothing) And (dgvMem.Rows(k).Cells(2).Value <> "" Or dgvMem.Rows(k).Cells(2).Value <> Nothing) Or (dgvMem.Rows(k).Cells(1).Value <> "" Or dgvMem.Rows(k).Cells(1).Value <> Nothing) Or (dgvMem.Rows(k).Cells(2).Value <> "" Or dgvMem.Rows(k).Cells(2).Value <> Nothing) Then
                        If dep_stat1 = 0 Then
                            inmat.insertDep1(ssnum, dep_name, dep_stat1, getbranchCoDE, Date.Today, tme, typRPT, dep_date, _frmUserAuthentication.lblTransactionNo.Text, kioskID)
                            Select Case dep_stat1

                                Case 1
                                    relType = "M"
                                Case 2
                                    relType = "D"
                                Case 3
                                    relType = "E"
                                Case Else
                                    relType = ""

                            End Select
                            workstation = getworkstation()
                            inmat.insertDependentSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE,
                             dep_name, _frmMainMenu.rcodeAcop, relType, dep_date, kioskID)
                        Else
                            inmat.insertDep(ssnum, dep_name, dep_stat1, getbranchCoDE, Date.Today, tme, typRPT, dep_date, _frmUserAuthentication.lblTransactionNo.Text, kioskID)
                            Select Case dep_stat1

                                Case 1
                                    relType = "M"
                                Case 2
                                    relType = "D"
                                Case 3
                                    relType = "E"
                                Case Else
                                    relType = ""

                            End Select
                            workstation = getworkstation()
                            inmat.insertDependentSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refACOP, getbranchCoDE,
                             dep_name, _frmMainMenu.rcodeAcop, relType, dep_date, kioskID)
                        End If

                    End If



                End If
            Next

            '   MsgBox("DEPENDENT DETAILS SUCCESFULLY SUBMITTED", MsgBoxStyle.Information, "PENSIONER DEPENDENTS")


            ' End If





        Catch ex As Exception

        End Try
    End Sub
    Private Sub _frmAcopSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try


        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Try
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmACOPdependent.TopLevel = False
            _frmACOPdependent.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmACOPdependent.Dock = DockStyle.Fill
            _frmACOPdependent.Show()

            _frmMainMenu.BackNextControls(False)

            _frmMainMenu.PrintControls(False)
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Function lblDatedelivery() As Object
        Throw New NotImplementedException
    End Function


End Class