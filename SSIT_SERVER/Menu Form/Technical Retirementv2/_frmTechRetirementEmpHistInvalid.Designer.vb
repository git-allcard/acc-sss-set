<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmTechRetirementEmpHistInvalid
    Inherits DevComponents.DotNetBar.Metro.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmTechRetirementEmpHistInvalid))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlMemDet = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.lblMsg = New System.Windows.Forms.Label()
        Me.Panel12 = New System.Windows.Forms.Panel()
        Me.pnlEmployer = New System.Windows.Forms.Panel()
        Me.Panel7 = New System.Windows.Forms.Panel()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.pnlEmployerSplitter = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.Panel8 = New System.Windows.Forms.Panel()
        Me.lblMsg9999 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UsrfrmPageHeader1 = New SSIT_SERVER.usrfrmPageHeader()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.emp_name = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.emp_date = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMemDet.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.pnlEmployer.SuspendLayout()
        Me.Panel7.SuspendLayout()
        Me.Panel5.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel6)
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(883, 911)
        Me.Panel1.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnCancel)
        Me.Panel6.Controls.Add(Me.btnNext)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.Panel10)
        Me.Panel6.Controls.Add(Me.Panel14)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 840)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(883, 71)
        Me.Panel6.TabIndex = 3
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.Location = New System.Drawing.Point(390, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(119, 63)
        Me.btnCancel.TabIndex = 377
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnCancel.UseVisualStyleBackColor = False
        Me.btnCancel.Visible = False
        '
        'btnNext
        '
        Me.btnNext.BackColor = System.Drawing.Color.White
        Me.btnNext.FlatAppearance.BorderSize = 0
        Me.btnNext.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnNext.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnNext.Font = New System.Drawing.Font("Verdana", 12.0!)
        Me.btnNext.ForeColor = System.Drawing.Color.Black
        Me.btnNext.Image = CType(resources.GetObject("btnNext.Image"), System.Drawing.Image)
        Me.btnNext.Location = New System.Drawing.Point(525, 3)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(119, 63)
        Me.btnNext.TabIndex = 376
        Me.btnNext.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnNext.UseVisualStyleBackColor = False
        Me.btnNext.Visible = False
        '
        'Panel9
        '
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(110, 71)
        Me.Panel9.TabIndex = 374
        '
        'Panel10
        '
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel10.Location = New System.Drawing.Point(742, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(110, 71)
        Me.Panel10.TabIndex = 370
        '
        'Panel14
        '
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel14.Location = New System.Drawing.Point(852, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(31, 71)
        Me.Panel14.TabIndex = 369
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlMemDet)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 147)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(883, 764)
        Me.Panel3.TabIndex = 2
        '
        'pnlMemDet
        '
        Me.pnlMemDet.AutoScroll = True
        Me.pnlMemDet.Controls.Add(Me.Panel15)
        Me.pnlMemDet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMemDet.Location = New System.Drawing.Point(0, 0)
        Me.pnlMemDet.Name = "pnlMemDet"
        Me.pnlMemDet.Size = New System.Drawing.Size(883, 764)
        Me.pnlMemDet.TabIndex = 39
        '
        'Panel15
        '
        Me.Panel15.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel15.Controls.Add(Me.lblMsg)
        Me.Panel15.Controls.Add(Me.Panel12)
        Me.Panel15.Controls.Add(Me.pnlEmployer)
        Me.Panel15.Controls.Add(Me.pnlEmployerSplitter)
        Me.Panel15.Controls.Add(Me.Panel4)
        Me.Panel15.Controls.Add(Me.Panel8)
        Me.Panel15.Controls.Add(Me.lblMsg9999)
        Me.Panel15.Location = New System.Drawing.Point(13, 6)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(858, 620)
        Me.Panel15.TabIndex = 42
        '
        'lblMsg
        '
        Me.lblMsg.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg.Location = New System.Drawing.Point(14, 358)
        Me.lblMsg.Name = "lblMsg"
        Me.lblMsg.Size = New System.Drawing.Size(825, 230)
        Me.lblMsg.TabIndex = 445
        Me.lblMsg.Text = "MESSAGE"
        '
        'Panel12
        '
        Me.Panel12.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel12.Location = New System.Drawing.Point(0, 329)
        Me.Panel12.Name = "Panel12"
        Me.Panel12.Size = New System.Drawing.Size(858, 13)
        Me.Panel12.TabIndex = 438
        '
        'pnlEmployer
        '
        Me.pnlEmployer.Controls.Add(Me.Panel7)
        Me.pnlEmployer.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEmployer.Location = New System.Drawing.Point(0, 71)
        Me.pnlEmployer.Name = "pnlEmployer"
        Me.pnlEmployer.Size = New System.Drawing.Size(858, 258)
        Me.pnlEmployer.TabIndex = 437
        '
        'Panel7
        '
        Me.Panel7.Controls.Add(Me.grid)
        Me.Panel7.Controls.Add(Me.Panel5)
        Me.Panel7.Location = New System.Drawing.Point(26, 11)
        Me.Panel7.Name = "Panel7"
        Me.Panel7.Size = New System.Drawing.Size(832, 241)
        Me.Panel7.TabIndex = 45
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel5.Location = New System.Drawing.Point(0, 0)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(832, 32)
        Me.Panel5.TabIndex = 45
        '
        'Label14
        '
        Me.Label14.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Label14.Font = New System.Drawing.Font("Verdana", 10.2!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(0, 0)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(830, 30)
        Me.Label14.TabIndex = 24
        Me.Label14.Text = "EMPLOYMENT HISTORY"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pnlEmployerSplitter
        '
        Me.pnlEmployerSplitter.Dock = System.Windows.Forms.DockStyle.Top
        Me.pnlEmployerSplitter.Location = New System.Drawing.Point(0, 61)
        Me.pnlEmployerSplitter.Name = "pnlEmployerSplitter"
        Me.pnlEmployerSplitter.Size = New System.Drawing.Size(858, 10)
        Me.pnlEmployerSplitter.TabIndex = 436
        '
        'Panel4
        '
        Me.Panel4.Controls.Add(Me.Label25)
        Me.Panel4.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel4.Location = New System.Drawing.Point(0, 10)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(858, 51)
        Me.Panel4.TabIndex = 435
        '
        'Label25
        '
        Me.Label25.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.Location = New System.Drawing.Point(14, 7)
        Me.Label25.Name = "Label25"
        Me.Label25.Size = New System.Drawing.Size(639, 32)
        Me.Label25.TabIndex = 420
        Me.Label25.Text = "Please review your employment history as follows:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel8
        '
        Me.Panel8.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel8.Location = New System.Drawing.Point(0, 0)
        Me.Panel8.Name = "Panel8"
        Me.Panel8.Size = New System.Drawing.Size(858, 10)
        Me.Panel8.TabIndex = 43
        '
        'lblMsg9999
        '
        Me.lblMsg9999.Font = New System.Drawing.Font("Verdana", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMsg9999.ForeColor = System.Drawing.Color.Red
        Me.lblMsg9999.Location = New System.Drawing.Point(15, 358)
        Me.lblMsg9999.Name = "lblMsg9999"
        Me.lblMsg9999.Size = New System.Drawing.Size(825, 42)
        Me.lblMsg9999.TabIndex = 446
        Me.lblMsg9999.Text = "PLEASE BE INFORMED THAT THERE IS A DISCREPANCY IN YOUR EMPLOYMENT DATE."
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.UsrfrmPageHeader1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(883, 147)
        Me.Panel2.TabIndex = 1
        '
        'UsrfrmPageHeader1
        '
        Me.UsrfrmPageHeader1.BackColor = System.Drawing.Color.White
        Me.UsrfrmPageHeader1.Location = New System.Drawing.Point(0, 0)
        Me.UsrfrmPageHeader1.Margin = New System.Windows.Forms.Padding(4, 17, 4, 17)
        Me.UsrfrmPageHeader1.Name = "UsrfrmPageHeader1"
        Me.UsrfrmPageHeader1.Size = New System.Drawing.Size(883, 147)
        Me.UsrfrmPageHeader1.TabIndex = 80
        '
        'grid
        '
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.BackgroundColor = System.Drawing.Color.White
        Me.grid.BorderStyle = System.Windows.Forms.BorderStyle.None
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Control
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grid.ColumnHeadersHeight = 27
        Me.grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.emp_name, Me.emp_date})
        Me.grid.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.grid.GridColor = System.Drawing.Color.LightGray
        Me.grid.Location = New System.Drawing.Point(0, 34)
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowHeadersWidth = 51
        Me.grid.RowTemplate.Height = 24
        Me.grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.grid.Size = New System.Drawing.Size(832, 207)
        Me.grid.TabIndex = 46
        Me.grid.TabStop = False
        '
        'emp_name
        '
        Me.emp_name.DataPropertyName = "emp_name"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle3.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.emp_name.DefaultCellStyle = DataGridViewCellStyle3
        Me.emp_name.HeaderText = "EMPLOYER NAME"
        Me.emp_name.MinimumWidth = 6
        Me.emp_name.Name = "emp_name"
        Me.emp_name.ReadOnly = True
        Me.emp_name.Width = 610
        '
        'emp_date
        '
        Me.emp_date.DataPropertyName = "emp_date"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.emp_date.DefaultCellStyle = DataGridViewCellStyle4
        Me.emp_date.HeaderText = "EMPLOYMENT DATE (MM-YYYY)"
        Me.emp_date.MinimumWidth = 6
        Me.emp_date.Name = "emp_date"
        Me.emp_date.ReadOnly = True
        Me.emp_date.Width = 220
        '
        'frmTechRetirementEmpHistInvalid
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 911)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "frmTechRetirementEmpHistInvalid"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlMemDet.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.pnlEmployer.ResumeLayout(False)
        Me.Panel7.ResumeLayout(False)
        Me.Panel5.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMemDet As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents btnNext As Button
    Friend WithEvents Panel12 As Panel
    Friend WithEvents pnlEmployer As Panel
    Friend WithEvents pnlEmployerSplitter As Panel
    Friend WithEvents Panel4 As Panel
    Friend WithEvents Label25 As Label
    Friend WithEvents Panel8 As Panel
    Friend WithEvents lblMsg As Label
    Friend WithEvents btnCancel As Button
    Friend WithEvents Panel7 As Panel
    Friend WithEvents Panel5 As Panel
    Friend WithEvents Label14 As Label
    Friend WithEvents UsrfrmPageHeader1 As usrfrmPageHeader
    Friend WithEvents lblMsg9999 As Label
    Friend WithEvents grid As DataGridView
    Friend WithEvents emp_name As DataGridViewTextBoxColumn
    Friend WithEvents emp_date As DataGridViewTextBoxColumn
End Class
