<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmPRNApplication_ConfirmFinal_List
    Inherits DevComponents.DotNetBar.Metro.MetroForm

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmPRNApplication_ConfirmFinal_List))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle11 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle4 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle5 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle6 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle7 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle8 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle9 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Dim DataGridViewCellStyle10 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnGeneratePRN = New System.Windows.Forms.Button()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlMemDet = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.grid = New System.Windows.Forms.DataGridView()
        Me.pbBarcode = New System.Windows.Forms.PictureBox()
        Me.lblPRN = New System.Windows.Forms.Label()
        Me.Panel5 = New System.Windows.Forms.Panel()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UsrfrmPageHeader1 = New SSIT_SERVER.usrfrmPageHeader()
        Me.PRN = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ApplicablePeriod = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ssamt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ecamt = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MonthlyPayment = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.FlexiFund = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.TotalAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DueDate = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.PrintButton = New System.Windows.Forms.DataGridViewLinkColumn()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMemDet.SuspendLayout()
        Me.Panel15.SuspendLayout()
        CType(Me.grid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbBarcode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel5.SuspendLayout()
        Me.Panel2.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(883, 749)
        Me.Panel1.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnGeneratePRN)
        Me.Panel6.Controls.Add(Me.btnSubmit)
        Me.Panel6.Controls.Add(Me.btnCancel)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.Panel10)
        Me.Panel6.Controls.Add(Me.Panel14)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 647)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(883, 102)
        Me.Panel6.TabIndex = 3
        '
        'btnGeneratePRN
        '
        Me.btnGeneratePRN.BackColor = System.Drawing.Color.White
        Me.btnGeneratePRN.FlatAppearance.BorderSize = 0
        Me.btnGeneratePRN.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnGeneratePRN.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnGeneratePRN.Font = New System.Drawing.Font("Verdana", 14.0!)
        Me.btnGeneratePRN.ForeColor = System.Drawing.Color.Black
        Me.btnGeneratePRN.Image = CType(resources.GetObject("btnGeneratePRN.Image"), System.Drawing.Image)
        Me.btnGeneratePRN.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnGeneratePRN.Location = New System.Drawing.Point(382, 3)
        Me.btnGeneratePRN.Name = "btnGeneratePRN"
        Me.btnGeneratePRN.Size = New System.Drawing.Size(179, 63)
        Me.btnGeneratePRN.TabIndex = 375
        Me.btnGeneratePRN.UseVisualStyleBackColor = False
        '
        'btnSubmit
        '
        Me.btnSubmit.BackColor = System.Drawing.Color.White
        Me.btnSubmit.FlatAppearance.BorderSize = 0
        Me.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnSubmit.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnSubmit.ForeColor = System.Drawing.Color.Black
        Me.btnSubmit.Image = CType(resources.GetObject("btnSubmit.Image"), System.Drawing.Image)
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSubmit.Location = New System.Drawing.Point(116, 5)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(92, 85)
        Me.btnSubmit.TabIndex = 354
        Me.btnSubmit.Text = "PRINT STATEMENT OF ACCOUNT (SOA)"
        Me.btnSubmit.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnSubmit.UseVisualStyleBackColor = False
        Me.btnSubmit.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.BackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.BorderSize = 0
        Me.btnCancel.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnCancel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnCancel.Font = New System.Drawing.Font("Verdana", 9.0!)
        Me.btnCancel.ForeColor = System.Drawing.Color.Black
        Me.btnCancel.Image = CType(resources.GetObject("btnCancel.Image"), System.Drawing.Image)
        Me.btnCancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnCancel.Location = New System.Drawing.Point(672, 3)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(74, 85)
        Me.btnCancel.TabIndex = 354
        Me.btnCancel.Text = "EDIT SOA"
        Me.btnCancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.btnCancel.UseVisualStyleBackColor = False
        Me.btnCancel.Visible = False
        '
        'Panel9
        '
        Me.Panel9.Dock = System.Windows.Forms.DockStyle.Left
        Me.Panel9.Location = New System.Drawing.Point(0, 0)
        Me.Panel9.Name = "Panel9"
        Me.Panel9.Size = New System.Drawing.Size(110, 102)
        Me.Panel9.TabIndex = 374
        '
        'Panel10
        '
        Me.Panel10.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel10.Location = New System.Drawing.Point(742, 0)
        Me.Panel10.Name = "Panel10"
        Me.Panel10.Size = New System.Drawing.Size(110, 102)
        Me.Panel10.TabIndex = 370
        '
        'Panel14
        '
        Me.Panel14.Dock = System.Windows.Forms.DockStyle.Right
        Me.Panel14.Location = New System.Drawing.Point(852, 0)
        Me.Panel14.Name = "Panel14"
        Me.Panel14.Size = New System.Drawing.Size(31, 102)
        Me.Panel14.TabIndex = 369
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlMemDet)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 147)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(883, 602)
        Me.Panel3.TabIndex = 2
        '
        'pnlMemDet
        '
        Me.pnlMemDet.Controls.Add(Me.Label1)
        Me.pnlMemDet.Controls.Add(Me.Panel15)
        Me.pnlMemDet.Controls.Add(Me.Panel5)
        Me.pnlMemDet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMemDet.Location = New System.Drawing.Point(0, 0)
        Me.pnlMemDet.Name = "pnlMemDet"
        Me.pnlMemDet.Size = New System.Drawing.Size(883, 602)
        Me.pnlMemDet.TabIndex = 39
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(15, 50)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(846, 74)
        Me.Label1.TabIndex = 440
        Me.Label1.Text = "Note: Pursuant to Circular No. 2020-032 dated 24 November 2020, starting January " &
    "2021, SS contribution includes Workers’ Investment and Savings Program or WISP (" &
    "SSS Provident Fund) contribution."
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.grid)
        Me.Panel15.Controls.Add(Me.pbBarcode)
        Me.Panel15.Controls.Add(Me.lblPRN)
        Me.Panel15.Location = New System.Drawing.Point(15, 127)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(846, 367)
        Me.Panel15.TabIndex = 42
        '
        'grid
        '
        Me.grid.AllowUserToAddRows = False
        Me.grid.AllowUserToDeleteRows = False
        Me.grid.AllowUserToResizeColumns = False
        Me.grid.AllowUserToResizeRows = False
        DataGridViewCellStyle1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grid.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Me.grid.BackgroundColor = System.Drawing.Color.White
        Me.grid.BorderStyle = System.Windows.Forms.BorderStyle.None
        Me.grid.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.[Single]
        DataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        DataGridViewCellStyle2.BackColor = System.Drawing.Color.White
        DataGridViewCellStyle2.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        DataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.WindowText
        DataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.[True]
        Me.grid.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Me.grid.ColumnHeadersHeight = 29
        Me.grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.grid.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.PRN, Me.ApplicablePeriod, Me.ssamt, Me.ecamt, Me.MonthlyPayment, Me.FlexiFund, Me.TotalAmount, Me.DueDate, Me.PrintButton})
        Me.grid.Dock = System.Windows.Forms.DockStyle.Fill
        Me.grid.EnableHeadersVisualStyles = False
        Me.grid.Location = New System.Drawing.Point(0, 0)
        Me.grid.Name = "grid"
        Me.grid.ReadOnly = True
        Me.grid.RowHeadersVisible = False
        Me.grid.RowHeadersWidth = 51
        DataGridViewCellStyle11.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grid.RowsDefaultCellStyle = DataGridViewCellStyle11
        Me.grid.RowTemplate.Height = 45
        Me.grid.Size = New System.Drawing.Size(846, 367)
        Me.grid.TabIndex = 433
        '
        'pbBarcode
        '
        Me.pbBarcode.Location = New System.Drawing.Point(772, 313)
        Me.pbBarcode.Name = "pbBarcode"
        Me.pbBarcode.Size = New System.Drawing.Size(110, 30)
        Me.pbBarcode.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbBarcode.TabIndex = 432
        Me.pbBarcode.TabStop = False
        Me.pbBarcode.Visible = False
        '
        'lblPRN
        '
        Me.lblPRN.Font = New System.Drawing.Font("Verdana", 10.5!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblPRN.Location = New System.Drawing.Point(9, 55)
        Me.lblPRN.Name = "lblPRN"
        Me.lblPRN.Size = New System.Drawing.Size(168, 41)
        Me.lblPRN.TabIndex = 427
        Me.lblPRN.Text = "Payment Reference Number"
        Me.lblPRN.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        Me.lblPRN.Visible = False
        '
        'Panel5
        '
        Me.Panel5.BackColor = System.Drawing.Color.SteelBlue
        Me.Panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel5.Controls.Add(Me.Label9)
        Me.Panel5.Controls.Add(Me.Label14)
        Me.Panel5.Location = New System.Drawing.Point(15, 8)
        Me.Panel5.Name = "Panel5"
        Me.Panel5.Size = New System.Drawing.Size(846, 39)
        Me.Panel5.TabIndex = 39
        '
        'Label9
        '
        Me.Label9.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold)
        Me.Label9.ForeColor = System.Drawing.Color.White
        Me.Label9.Location = New System.Drawing.Point(3, 6)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(833, 25)
        Me.Label9.TabIndex = 25
        Me.Label9.Text = "Payment Ref No. (PRN) - List of Active PRN"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label14
        '
        Me.Label14.AutoSize = True
        Me.Label14.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label14.ForeColor = System.Drawing.Color.White
        Me.Label14.Location = New System.Drawing.Point(3, 6)
        Me.Label14.Name = "Label14"
        Me.Label14.Size = New System.Drawing.Size(0, 32)
        Me.Label14.TabIndex = 24
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
        Me.UsrfrmPageHeader1.Margin = New System.Windows.Forms.Padding(4, 6, 4, 6)
        Me.UsrfrmPageHeader1.Name = "UsrfrmPageHeader1"
        Me.UsrfrmPageHeader1.Size = New System.Drawing.Size(883, 147)
        Me.UsrfrmPageHeader1.TabIndex = 74
        '
        'PRN
        '
        Me.PRN.DataPropertyName = "iprnum"
        DataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.PRN.DefaultCellStyle = DataGridViewCellStyle3
        Me.PRN.HeaderText = "Payment Reference Number"
        Me.PRN.MinimumWidth = 6
        Me.PRN.Name = "PRN"
        Me.PRN.ReadOnly = True
        Me.PRN.Width = 130
        '
        'ApplicablePeriod
        '
        Me.ApplicablePeriod.DataPropertyName = "ApplicablePeriod"
        DataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.ApplicablePeriod.DefaultCellStyle = DataGridViewCellStyle4
        Me.ApplicablePeriod.HeaderText = "Applicable Period"
        Me.ApplicablePeriod.MinimumWidth = 6
        Me.ApplicablePeriod.Name = "ApplicablePeriod"
        Me.ApplicablePeriod.ReadOnly = True
        Me.ApplicablePeriod.Width = 157
        '
        'ssamt
        '
        Me.ssamt.DataPropertyName = "ssamt"
        DataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ssamt.DefaultCellStyle = DataGridViewCellStyle5
        Me.ssamt.HeaderText = "SS Amount"
        Me.ssamt.MinimumWidth = 6
        Me.ssamt.Name = "ssamt"
        Me.ssamt.ReadOnly = True
        Me.ssamt.Width = 80
        '
        'ecamt
        '
        Me.ecamt.DataPropertyName = "ecamt"
        DataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.ecamt.DefaultCellStyle = DataGridViewCellStyle6
        Me.ecamt.HeaderText = "EC Amount"
        Me.ecamt.MinimumWidth = 6
        Me.ecamt.Name = "ecamt"
        Me.ecamt.ReadOnly = True
        Me.ecamt.Width = 80
        '
        'MonthlyPayment
        '
        Me.MonthlyPayment.DataPropertyName = "MonthlyPayment"
        DataGridViewCellStyle7.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.MonthlyPayment.DefaultCellStyle = DataGridViewCellStyle7
        Me.MonthlyPayment.HeaderText = "Monthly Payment"
        Me.MonthlyPayment.MinimumWidth = 6
        Me.MonthlyPayment.Name = "MonthlyPayment"
        Me.MonthlyPayment.ReadOnly = True
        Me.MonthlyPayment.Visible = False
        Me.MonthlyPayment.Width = 6
        '
        'FlexiFund
        '
        Me.FlexiFund.DataPropertyName = "FlexiFund"
        DataGridViewCellStyle8.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.FlexiFund.DefaultCellStyle = DataGridViewCellStyle8
        Me.FlexiFund.HeaderText = "Flexi-Fund Amount"
        Me.FlexiFund.MinimumWidth = 6
        Me.FlexiFund.Name = "FlexiFund"
        Me.FlexiFund.ReadOnly = True
        Me.FlexiFund.Width = 125
        '
        'TotalAmount
        '
        Me.TotalAmount.DataPropertyName = "tsamt"
        DataGridViewCellStyle9.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleRight
        Me.TotalAmount.DefaultCellStyle = DataGridViewCellStyle9
        Me.TotalAmount.HeaderText = "Total Amount"
        Me.TotalAmount.MinimumWidth = 6
        Me.TotalAmount.Name = "TotalAmount"
        Me.TotalAmount.ReadOnly = True
        Me.TotalAmount.Width = 125
        '
        'DueDate
        '
        Me.DueDate.DataPropertyName = "dueDate"
        DataGridViewCellStyle10.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter
        Me.DueDate.DefaultCellStyle = DataGridViewCellStyle10
        Me.DueDate.HeaderText = "Due Date"
        Me.DueDate.MinimumWidth = 6
        Me.DueDate.Name = "DueDate"
        Me.DueDate.ReadOnly = True
        Me.DueDate.Width = 83
        '
        'PrintButton
        '
        Me.PrintButton.HeaderText = ""
        Me.PrintButton.MinimumWidth = 6
        Me.PrintButton.Name = "PrintButton"
        Me.PrintButton.ReadOnly = True
        Me.PrintButton.Resizable = System.Windows.Forms.DataGridViewTriState.[True]
        Me.PrintButton.Text = "PRINT"
        Me.PrintButton.UseColumnTextForLinkValue = True
        Me.PrintButton.Width = 65
        '
        '_frmPRNApplication_ConfirmFinal_List
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 749)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmPRNApplication_ConfirmFinal_List"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlMemDet.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        CType(Me.grid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbBarcode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel5.ResumeLayout(False)
        Me.Panel5.PerformLayout()
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMemDet As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Panel5 As System.Windows.Forms.Panel
    Friend WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents lblPRN As System.Windows.Forms.Label
    Friend WithEvents pbBarcode As System.Windows.Forms.PictureBox
    Friend WithEvents btnGeneratePRN As System.Windows.Forms.Button
    Friend WithEvents Label9 As System.Windows.Forms.Label
    Friend WithEvents Label1 As Label
    Friend WithEvents UsrfrmPageHeader1 As usrfrmPageHeader
    Friend WithEvents grid As DataGridView
    Friend WithEvents PRN As DataGridViewTextBoxColumn
    Friend WithEvents ApplicablePeriod As DataGridViewTextBoxColumn
    Friend WithEvents ssamt As DataGridViewTextBoxColumn
    Friend WithEvents ecamt As DataGridViewTextBoxColumn
    Friend WithEvents MonthlyPayment As DataGridViewTextBoxColumn
    Friend WithEvents FlexiFund As DataGridViewTextBoxColumn
    Friend WithEvents TotalAmount As DataGridViewTextBoxColumn
    Friend WithEvents DueDate As DataGridViewTextBoxColumn
    Friend WithEvents PrintButton As DataGridViewLinkColumn
End Class
