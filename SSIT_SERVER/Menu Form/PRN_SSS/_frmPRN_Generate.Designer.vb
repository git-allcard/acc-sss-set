<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmPRN_Generate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmPRN_Generate))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel6 = New System.Windows.Forms.Panel()
        Me.btnSubmit = New System.Windows.Forms.Button()
        Me.Panel9 = New System.Windows.Forms.Panel()
        Me.Panel10 = New System.Windows.Forms.Panel()
        Me.Panel14 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlMemDet = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UsrfrmPageHeader1 = New SSIT_SERVER.usrfrmPageHeader()
        Me.Panel1.SuspendLayout()
        Me.Panel6.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMemDet.SuspendLayout()
        Me.Panel15.SuspendLayout()
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
        Me.Panel1.Size = New System.Drawing.Size(883, 694)
        Me.Panel1.TabIndex = 0
        '
        'Panel6
        '
        Me.Panel6.Controls.Add(Me.btnSubmit)
        Me.Panel6.Controls.Add(Me.Panel9)
        Me.Panel6.Controls.Add(Me.Panel10)
        Me.Panel6.Controls.Add(Me.Panel14)
        Me.Panel6.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel6.Location = New System.Drawing.Point(0, 592)
        Me.Panel6.Name = "Panel6"
        Me.Panel6.Size = New System.Drawing.Size(883, 102)
        Me.Panel6.TabIndex = 3
        '
        'btnSubmit
        '
        Me.btnSubmit.BackColor = System.Drawing.Color.White
        Me.btnSubmit.FlatAppearance.BorderSize = 0
        Me.btnSubmit.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnSubmit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnSubmit.Font = New System.Drawing.Font("Verdana", 13.0!)
        Me.btnSubmit.ForeColor = System.Drawing.Color.Black
        Me.btnSubmit.Image = CType(resources.GetObject("btnSubmit.Image"), System.Drawing.Image)
        Me.btnSubmit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnSubmit.Location = New System.Drawing.Point(382, 3)
        Me.btnSubmit.Name = "btnSubmit"
        Me.btnSubmit.Size = New System.Drawing.Size(179, 63)
        Me.btnSubmit.TabIndex = 354
        Me.btnSubmit.UseVisualStyleBackColor = False
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
        Me.Panel3.Size = New System.Drawing.Size(883, 547)
        Me.Panel3.TabIndex = 2
        '
        'pnlMemDet
        '
        Me.pnlMemDet.Controls.Add(Me.Panel15)
        Me.pnlMemDet.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlMemDet.Location = New System.Drawing.Point(0, 0)
        Me.pnlMemDet.Name = "pnlMemDet"
        Me.pnlMemDet.Size = New System.Drawing.Size(883, 547)
        Me.pnlMemDet.TabIndex = 39
        '
        'Panel15
        '
        Me.Panel15.Controls.Add(Me.lblMessage)
        Me.Panel15.Controls.Add(Me.Label37)
        Me.Panel15.Location = New System.Drawing.Point(15, 22)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(856, 401)
        Me.Panel15.TabIndex = 42
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(4, 133)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(833, 78)
        Me.lblMessage.TabIndex = 283
        Me.lblMessage.Text = "SS Number {0} has no active Payment Reference Number (PRN)"
        Me.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label37
        '
        Me.Label37.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label37.ForeColor = System.Drawing.Color.Black
        Me.Label37.Location = New System.Drawing.Point(6, 5)
        Me.Label37.Name = "Label37"
        Me.Label37.Size = New System.Drawing.Size(831, 121)
        Me.Label37.TabIndex = 282
        Me.Label37.Text = resources.GetString("Label37.Text")
        Me.Label37.Visible = False
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
        '_frmPRN_Generate
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 694)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmPRN_Generate"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
        Me.Panel6.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlMemDet.ResumeLayout(False)
        Me.Panel15.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlMemDet As System.Windows.Forms.Panel
    Friend WithEvents Panel15 As System.Windows.Forms.Panel
    Friend WithEvents Label37 As System.Windows.Forms.Label
    Friend WithEvents Panel6 As System.Windows.Forms.Panel
    Friend WithEvents btnSubmit As System.Windows.Forms.Button
    Friend WithEvents Panel9 As System.Windows.Forms.Panel
    Friend WithEvents Panel10 As System.Windows.Forms.Panel
    Friend WithEvents Panel14 As System.Windows.Forms.Panel
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents UsrfrmPageHeader1 As usrfrmPageHeader
End Class
