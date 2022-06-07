Public Class _frmPensionSummary
    Dim inmat As New insertProcedure
    Dim db As New ConnectionString
    Dim xtd As New ExtractedDetails
    Dim printF As New printModule
    Dim tempSSSHeader As String
    Dim ssnum As String
    Dim xs As New MySettings

    Dim workstation As String

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            transTag = "PM"
            Dim resultDis As Integer = MessageBox.Show("DO YOU WANT TO SUBMIT THE CHANGES YOU HAVE MADE? ", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultDis = DialogResult.Yes Then
                CurrentTxnType = TxnType.PensionMaintenance
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
                xtd.getRawFile()
                If Not _frm2 Is Nothing Then _frmMainMenu.DisposeForm(_frm2)
                _frm2.CardType = CShort(xtd.checkFileType)
                _frm2.TopLevel = False
                _frm2.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frm2.Dock = DockStyle.Fill
                _frm2.Show()

          
            Else
            End If
            '_frmMainMenu.Button2.PerformClick()


        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub


    Public Sub submitPensionMaintenance()
        Try
            _frmMainMenu.PrintControls(True)
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")

            GC.Collect()

            Dim getBrCode As String = db.putSingleValue("Select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
            Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
            _frmUserAuthentication.getTransacNum()


            workstation = getworkstation()

            Dim result As Integer = xtd.checkFileType
            If result = 1 Then

                inmat.insertPenSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refPen, getbranchCoDE, txtAdd1.Text,
                                   txtAdd3.Text, Add5.Text, txtEmail.Text, txtLandline.Text, txtMobile.Text, kioskID)
                inmat.insertPension(ssnum, txtAdd1.Text, txtAdd3.Text, txtAdd4.Text, Add5.Text,
                                    txtLandline.Text, txtMobile.Text, txtEmail.Text, Date.Today.ToShortDateString,
                                    TimeOfDay, getbranchCoDE, getkiosk_cluster, getkiosk_group, kioskID)

                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "PM1Logs.txt", True)
                '    SW.WriteLine(ssnum & "," & txtAdd1.Text & "," & txtAdd3.Text & "," & txtAdd4.Text & "," & Add5.Text & "," & txtLandline.Text & _
                '                 "," & txtMobile.Text & "," & txtEmail.Text & "," & Date.Today.ToShortDateString & "," & TimeOfDay & "," & getbranchCoDE & "," & _
                '                 getkiosk_cluster & "," & getkiosk_group & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                'End Using

            ElseIf result = 2 Then
                'argie102
                '  Dim trNo As String = _frmUserAuthentication.lblTransactionNo.Text

                inmat.insertPenSSS(_frmUserAuthentication.lblTransactionNo.Text, ssnum, _frmMainMenu.refPen, getbranchCoDE, txtAdd1.Text,
                          txtAdd3.Text, Add5.Text, txtEmail.Text, txtLandline.Text, txtMobile.Text, kioskID)
                inmat.insertPension(ssnum, txtAdd1.Text, txtAdd3.Text, txtAdd4.Text, Add5.Text,
                                     txtLandline.Text, txtMobile.Text, txtEmail.Text, Date.Today.ToShortDateString, TimeOfDay,
                                     getbranchCoDE, getkiosk_cluster, getkiosk_group, kioskID)

                Dim getdate As String = Date.Today.ToString("ddMMyyyy")
                'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "PM1Logs.txt", True)
                '    SW.WriteLine(xtd.getCRN & "," & txtAdd1.Text & "," & txtAdd3.Text & "," & txtAdd4.Text & "," & Add5.Text & "," & txtLandline.Text & _
                '                 "," & txtMobile.Text & "," & txtEmail.Text & "," & Date.Today.ToShortDateString & "," & TimeOfDay & "," & getbranchCoDE & "," & _
                '                 getkiosk_cluster & "," & getkiosk_group & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
                'End Using

            End If

            Dim dateCreated As String

            dateCreated = Format(Now, "MM/dd/yyyy")
            authentication = "PM01"
            tagPage = "17"
            _frmMainMenu.btnPrint.PerformClick()
            tagPage = "7"
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmUserAuthentication.TopLevel = False
            _frmUserAuthentication.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmUserAuthentication.Dock = DockStyle.Fill
            _frmUserAuthentication.Show()
            _frmMainMenu.Button5.Enabled = False
            _frmMainMenu.Button6.Enabled = False
            _frmPensionMaintenance.Button2.Enabled = True
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")
            _frmUserAuthentication.UsrfrmPageHeader1.lblHeader.Text = "CHANGE OF ADDRESS/CONTACT INFORMATION"
            '_frmUserAuthentication.lblFooter.Text = "System will save transaction record."

            _frmPensionDetails.Dispose()
            _frmPensionMaintenance.Dispose()

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
            _frmPensionMaintenance.TopLevel = False
            _frmPensionMaintenance.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmPensionMaintenance.Dock = DockStyle.Fill
            _frmPensionMaintenance.Show()
            _frmMainMenu.Button5.Enabled = False
            _frmMainMenu.Button6.Enabled = False
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

    Private Sub _frmPensionSummary_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try

            xtd.getRawFile()

            Dim result As Integer = xtd.checkFileType
            If result = 1 Then


                Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
                Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
                Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
                Dim fullname As String = lname & " " & fname & " " & mname

                tempSSSHeader = xtd.getCRN
                ssnum = tempSSSHeader
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                lblCRNNum.Text = ""
           
                lblMemberName.Text = fullname.Replace("�", ChrW(209))
                lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                lblReferenceNo.Text = ""
                Label5.Visible = False

                lblReferenceNo.Visible = False
            ElseIf result = 2 Then

                tempSSSHeader = SSStempFile
                ssnum = tempSSSHeader
                If tempSSSHeader = "" Then

                Else
                    tempSSSHeader = tempSSSHeader.Insert(2, "-")
                    tempSSSHeader = tempSSSHeader.Insert(10, "-")
                End If

                lblSSSNo.Text = tempSSSHeader
                lblCRNNum.Text = xtd.getCRN
                Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
                Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
                Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1)
                Dim fullname As String = lname & " " & fname & " " & mname
                lblMemberName.Text = fullname.Replace("�", ChrW(209))

                'lblMemberName.Text = xtd.getFullname
                lblMemberName.Text = lblMemberName.Text.Replace("�", ChrW(209))
                lblReferenceNo.Text = xtd.getCRN
                lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                Label5.Visible = False

                lblReferenceNo.Visible = False
            End If
            Dim bdate As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
            lblDateofBirth.Text = bdate
            Dim coverageStatus As String = printF.GetDateCoverage(_frmWebBrowser.WebBrowser1)
            lblDateofCoverage.Text = coverageStatus


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