Imports System.Runtime.InteropServices

Public Class _frmACOPdependent
    Dim db As New ConnectionString
    Public depStat As Integer
    Public depCode As String
    Public depDate As Date
    Dim xtd As New ExtractedDetails
    Dim printf As New printModule
    Dim inMat As New insertProcedure
    Public mouseFocus As String
    Dim tagCorrect As Integer
    Public dpt_stat As Integer
    Dim tempSSSHeader As String
    Private combo As ComboBox
    Dim typrpt1 As String
    '
    '  0 - MARRIAGE, 1- DEATH, 2- EMPLOYMENT
    Private Const EM_SETCUEBANNER As Integer = &H1501
    <DllImport("user32.dll", EntryPoint:="SendMessageW")>
    Private Shared Function SendMessageW(ByVal hWnd As IntPtr, ByVal Msg As UInt32, ByVal wParam As Boolean, <MarshalAs(UnmanagedType.LPWStr)> ByVal lParam As String) As IntPtr
    End Function
    Private Sub _frmACOPdependent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        tagPage = "6.1"

        Console.WriteLine("test")

        Try
            'getDetails()

            'If RESULTACOP = 0 Then
            '    db.FillDataGridView("select DPDNAME as 'Name of Dependent Children' from SSTRANSACOPAD", DataGridViewX1)
            'Else

            'End If
            _frmMainMenu.BackNextControls(False)
            xtd.getRawFile()


            If tempSSSHeader = "" Then
            Else
                tempSSSHeader = tempSSSHeader.Replace("-", "")
            End If
            Dim listbox1 As New ListView
            dgvMem.Rows.Clear()
            listbox1.Items.Clear()
            'db.FillListView(db.ExecuteSQLQuery("select name as 'NAME' from TEMPSSSDEPD where SSNUM = '" & tempSSSHeader & "'"), listbox1)
            db.FillListView(db.ExecuteSQLQuery("select name as 'NAME' from TEMPSSSDEPD where SSNUM = '" & SSStempFile & "'"), listbox1)
            ' db.FillDataGridView("select name as 'NAME' from TEMPSSSDEPD where SSNUM = '" & tempSSSHeader & "'", dgvMem)
            cbSTATUS1.Items.Add("-PLEASE SELECT STATUS-")

            Dim dt As New DataTable

            dt = db.ExecuteSQLQuery("Select DISTINCT TYPE, ID from SSINFOACOPTYPE ORDER BY ID")

            For i As Integer = 0 To dt.Rows.Count - 1
                Dim x As String = dt.Rows(i)(0).ToString
                cbSTATUS1.Items.Add(x)
            Next
            cbSTATUS1.Items.Add("NO STATUS CHANGE")
            Dim tx As String
            For k = 0 To listbox1.Items.Count - 1
                tx = listbox1.Items(k).Text
                dgvMem.Rows.Add(tx)
                dgvMem.Rows(k).Cells(1).Value = cbSTATUS1.Items(0)
                dgvMem.Rows(k).Cells(2).Value = "MM/DD/YYYY"
            Next
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

    End Sub

    Private Sub switchColumns()
        Dim columnCollection As DataGridViewColumnCollection =
        dgvMem.Columns
        Dim firstVisibleColumn As DataGridViewColumn =
        columnCollection.GetFirstColumn(DataGridViewElementStates.Visible)
        Dim lastVisibleColumn As DataGridViewColumn =
            columnCollection.GetLastColumn(DataGridViewElementStates.Visible,
            Nothing)
        Dim MiddleVisibleColumn As DataGridViewColumn =
          columnCollection.GetNextColumn(firstVisibleColumn, DataGridViewElementStates.Visible,
          Nothing)

        Dim firstColumn_sIndex As Integer = firstVisibleColumn.DisplayIndex

        lastVisibleColumn.DisplayIndex = firstColumn_sIndex

    End Sub

    Private Sub dtpMarriage_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        depStat = 0
    End Sub

    Private Sub dtpEmployment_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        depStat = 1
    End Sub

    Private Sub dtpDeath_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        depStat = 2
    End Sub

    Private Sub _frmACOPdependent_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Try
            _frmCalendar.Dispose()
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
            '_frmMainMenu.Button5.Enabled = False
            '_frmMainMenu.Button6.Enabled = False
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub


    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Try
            Dim ctr As Integer
            ctr = Nothing
            'If dpt_stat = 0 Then
            '  MsgBox("PLEASE SELECT A STATUS FOR THE DEPENDENT", vbInformation, "PENSIONER DEPENDENT")

            ' Else
            For k = 0 To dgvMem.Rows.Count - 1

                If dgvMem.Rows(k).Cells(1).Value = "NO STATUS CHANGE" And dgvMem.Rows(k).Cells(2).Value = "MM/DD/YYYY" Then
                    ctr = 3 '  okay
                ElseIf dgvMem.Rows(k).Cells(1).Value <> "-PLEASE SELECT STATUS-" And dgvMem.Rows(k).Cells(2).Value <> "MM/DD/YYYY" Then
                    ctr = 3 '  okay
                ElseIf dgvMem.Rows(k).Cells(1).Value <> "-PLEASE SELECT STATUS-" And dgvMem.Rows(k).Cells(2).Value = "MM/DD/YYYY" Then
                    ctr = 1 ' VALID DATE
                    Exit For
                ElseIf dgvMem.Rows(k).Cells(1).Value = "NO STATUS CHANGE" And dgvMem.Rows(k).Cells(2).Value <> "MM/DD/YYYY" Then
                    ctr = 2 ' SELECT A STATUS.
                    Exit For
                ElseIf dgvMem.Rows(k).Cells(1).Value = "-PLEASE SELECT STATUS-" And dgvMem.Rows(k).Cells(2).Value <> "MM/DD/YYYY" Then
                    ctr = 2 'SELECT A STATUS.
                    Exit For
                ElseIf dgvMem.Rows(k).Cells(1).Value = "-PLEASE SELECT STATUS-" And dgvMem.Rows(k).Cells(2).Value = "MM/DD/YYYY" Then
                    ctr = 2 'SELECT A STATUS.
                    Exit For
                Else
                    ctr = 0
                End If
            Next

            If ctr = 1 Then
                MsgBox("PLEASE ENTER A VALID DATE.", MsgBoxStyle.Information, "PENSIONER DEPENDENTS")
            ElseIf ctr = 2 Then
                MsgBox("PLEASE SELECT A STATUS.", MsgBoxStyle.Information, "PENSIONER DEPENDENTS")

            ElseIf ctr = 3 Or ctr = 0 Then
                _frmCalendar.Dispose()
                _frmAcopSummary.lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
                _frmAcopSummary.lblBranch.Text = kioskBranch
                _frmAcopSummary.lblTerminalNo.Text = kioskID
                _frmAcopSummary.dgvMem.Columns.Clear()
                _frmAcopSummary.dgvMem.Rows.Clear()
                _frmAcopSummary.dgvMem.Columns.Add("txtNAME", "NAME")
                _frmAcopSummary.dgvMem.Columns.Add("txtStatus", "STATUS")
                _frmAcopSummary.dgvMem.Columns.Add("txtDate", "DATE")

                '_frmAcopSummary.dep_stat1 = dpt_stat
                '_frmAcopSummary.typRPT = typrpt1
                For Each dr In Me.dgvMem.Rows
                    _frmAcopSummary.dgvMem.Rows.Add(dr.Cells(0).Value, dr.Cells(1).Value, dr.Cells(2).Value)
                Next

                If Me.dpt_stat = 0 Then
                    For k = 0 To _frmAcopSummary.dgvMem.Rows.Count - 1
                        If (_frmAcopSummary.dgvMem.Rows(k).Cells(1).Value = "-PLEASE SELECT STATUS-") Then
                            _frmAcopSummary.dgvMem.Rows(k).Cells(1).Value = ""
                        End If
                        If (_frmAcopSummary.dgvMem.Rows(k).Cells(2).Value = "MM/DD/YYYY") Then
                            _frmAcopSummary.dgvMem.Rows(k).Cells(2).Value = ""
                        End If
                        If (_frmAcopSummary.dgvMem.Rows(k).Cells(1).Value = "NO STATUS CHANGE") Then
                            _frmAcopSummary.dgvMem.Rows(k).Cells(2).Value = ""
                        End If
                    Next
                End If
                _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()

                _frmAcopSummary.TopLevel = False
                _frmAcopSummary.Parent = _frmMainMenu.splitContainerControl.Panel2
                _frmAcopSummary.Dock = DockStyle.Fill
                _frmAcopSummary.Show()

                _frmMainMenu.BackNextControls(False)
                '_frmMainMenu.Button5.Enabled = False
                '_frmMainMenu.Button6.Enabled = False
                _frmMainMenu.PrintControls(False)
                '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT_DISABLED.png")
                'End If
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

    Private Sub Combo_click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        cbSTATUS1.Items.Clear()

        Dim dt As New DataTable

        dt = db.ExecuteSQLQuery("Select DISTINCT TYPE, ID from SSINFOACOPTYPE ORDER BY ID")
        cbSTATUS1.Items.Add("PLEASE SELECT STATUS")
        For i As Integer = 0 To dt.Rows.Count - 1
            Dim x As String = dt.Rows(i)(0).ToString
            cbSTATUS1.Items.Add(x)
        Next
    End Sub
    Private Sub Combo_SelectedIndexChange(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If DirectCast(sender, ComboBox).SelectedIndex <> -1 Then
            If DirectCast(sender, ComboBox).SelectedItem = "DATE OF MARRIAGE" Then
                dpt_stat = 1
                typrpt1 = "DATE OF MARRIAGE"
                'MessageBox.Show("MARRIAGE")
            ElseIf DirectCast(sender, ComboBox).SelectedItem = "DATE OF EMPLOYMENT" Then
                dpt_stat = 3
                typrpt1 = "DATE OF EMPLOYMENT"
            ElseIf DirectCast(sender, ComboBox).SelectedItem = "DATE OF DEATH" Then
                dpt_stat = 2
                typrpt1 = "DATE OF DEATH"
                'MessageBox.Show("DEATH")
            ElseIf DirectCast(sender, ComboBox).SelectedItem = "PLEASE SELECT STATUS" Then
                dpt_stat = 0

            End If
        End If
    End Sub




    Private Sub dgvMem_CellClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvMem.CellClick
        If e.ColumnIndex = 2 Then
            mouseFocus = "ACOP DEP"
            _frmCalendar.Close()
            _frmCalendar.Show()
            _frmCalendar.FORMCHECK = "ACOP DEP"
            _frmCalendar.btncalsub.Visible = True
        End If
    End Sub

    Private Sub dgvMem_EditingControlShowing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewEditingControlShowingEventArgs) Handles dgvMem.EditingControlShowing
        'If Me.dgvMem.CurrentCell.ColumnIndex = 1 Then 'Your ColumnCB index
        '    If combo Is Nothing Then
        '        combo = CType(e.Control, ComboBox)

        '        RemoveHandler combo.SelectedIndexChanged, New EventHandler(AddressOf Combo_SelectedIndexChange)
        '        AddHandler combo.SelectedIndexChanged, New EventHandler(AddressOf Combo_SelectedIndexChange)
        '        AddHandler combo.Click, New EventHandler(AddressOf Combo_click)
        '    End If
        'End If
    End Sub


End Class