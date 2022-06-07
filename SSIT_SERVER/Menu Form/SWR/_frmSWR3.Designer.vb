<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class _frmSWR3
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmSWR3))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlMemDet = New System.Windows.Forms.Panel()
        Me.Panel15 = New System.Windows.Forms.Panel()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.UsrfrmPageHeader1 = New SSIT_SERVER.usrfrmPageHeader()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlMemDet.SuspendLayout()
        Me.Panel15.SuspendLayout()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(883, 694)
        Me.Panel1.TabIndex = 0
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
        Me.Panel15.Location = New System.Drawing.Point(15, 22)
        Me.Panel15.Name = "Panel15"
        Me.Panel15.Size = New System.Drawing.Size(915, 461)
        Me.Panel15.TabIndex = 42
        '
        'lblMessage
        '
        Me.lblMessage.Font = New System.Drawing.Font("Verdana", 12.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.Black
        Me.lblMessage.Location = New System.Drawing.Point(6, 5)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(837, 374)
        Me.lblMessage.TabIndex = 282
        Me.lblMessage.Text = resources.GetString("lblMessage.Text")
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
        Me.UsrfrmPageHeader1.Margin = New System.Windows.Forms.Padding(4, 10, 4, 10)
        Me.UsrfrmPageHeader1.Name = "UsrfrmPageHeader1"
        Me.UsrfrmPageHeader1.Size = New System.Drawing.Size(883, 147)
        Me.UsrfrmPageHeader1.TabIndex = 77
        '
        '_frmSWR3
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 694)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmSWR3"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
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
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents UsrfrmPageHeader1 As usrfrmPageHeader
End Class
