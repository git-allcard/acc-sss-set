<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class _frmUpdCntcInfv2
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmUpdCntcInfv2))
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.pnlWeb = New System.Windows.Forms.Panel()
        Me.WebBrowser1 = New System.Windows.Forms.WebBrowser()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.pbDown = New System.Windows.Forms.PictureBox()
        Me.pbUp = New System.Windows.Forms.PictureBox()
        Me.UsrfrmPageHeader1 = New SSIT_SERVER.usrfrmPageHeader()
        Me.Panel1.SuspendLayout()
        Me.Panel3.SuspendLayout()
        Me.pnlBody.SuspendLayout()
        Me.pnlWeb.SuspendLayout()
        Me.Panel2.SuspendLayout()
        CType(Me.pbDown, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbUp, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel3)
        Me.Panel1.Controls.Add(Me.Panel2)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(883, 749)
        Me.Panel1.TabIndex = 0
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.pnlBody)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 105)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(883, 644)
        Me.Panel3.TabIndex = 2
        '
        'pnlBody
        '
        Me.pnlBody.Controls.Add(Me.pnlWeb)
        Me.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pnlBody.Location = New System.Drawing.Point(0, 0)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Size = New System.Drawing.Size(883, 644)
        Me.pnlBody.TabIndex = 39
        '
        'pnlWeb
        '
        Me.pnlWeb.Controls.Add(Me.WebBrowser1)
        Me.pnlWeb.Location = New System.Drawing.Point(-20, -28)
        Me.pnlWeb.Name = "pnlWeb"
        Me.pnlWeb.Size = New System.Drawing.Size(900, 998)
        Me.pnlWeb.TabIndex = 6
        '
        'WebBrowser1
        '
        Me.WebBrowser1.Location = New System.Drawing.Point(0, 0)
        Me.WebBrowser1.MinimumSize = New System.Drawing.Size(20, 20)
        Me.WebBrowser1.Name = "WebBrowser1"
        Me.WebBrowser1.Size = New System.Drawing.Size(891, 722)
        Me.WebBrowser1.TabIndex = 3
        '
        'Panel2
        '
        Me.Panel2.Controls.Add(Me.pbDown)
        Me.Panel2.Controls.Add(Me.pbUp)
        Me.Panel2.Controls.Add(Me.UsrfrmPageHeader1)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(883, 105)
        Me.Panel2.TabIndex = 1
        '
        'pbDown
        '
        Me.pbDown.BackColor = System.Drawing.Color.Transparent
        Me.pbDown.Image = CType(resources.GetObject("pbDown.Image"), System.Drawing.Image)
        Me.pbDown.Location = New System.Drawing.Point(775, 219)
        Me.pbDown.Name = "pbDown"
        Me.pbDown.Size = New System.Drawing.Size(94, 74)
        Me.pbDown.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbDown.TabIndex = 81
        Me.pbDown.TabStop = False
        Me.pbDown.Visible = False
        '
        'pbUp
        '
        Me.pbUp.BackColor = System.Drawing.Color.Transparent
        Me.pbUp.Image = CType(resources.GetObject("pbUp.Image"), System.Drawing.Image)
        Me.pbUp.Location = New System.Drawing.Point(775, 139)
        Me.pbUp.Name = "pbUp"
        Me.pbUp.Size = New System.Drawing.Size(94, 74)
        Me.pbUp.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbUp.TabIndex = 80
        Me.pbUp.TabStop = False
        Me.pbUp.Visible = False
        '
        'UsrfrmPageHeader1
        '
        Me.UsrfrmPageHeader1.BackColor = System.Drawing.Color.White
        Me.UsrfrmPageHeader1.Location = New System.Drawing.Point(0, 0)
        Me.UsrfrmPageHeader1.Margin = New System.Windows.Forms.Padding(4, 10, 4, 10)
        Me.UsrfrmPageHeader1.Name = "UsrfrmPageHeader1"
        Me.UsrfrmPageHeader1.Size = New System.Drawing.Size(883, 105)
        Me.UsrfrmPageHeader1.TabIndex = 77
        '
        '_frmUpdCntcInfv2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(883, 749)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmUpdCntcInfv2"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
        Me.Panel3.ResumeLayout(False)
        Me.pnlBody.ResumeLayout(False)
        Me.pnlWeb.ResumeLayout(False)
        Me.Panel2.ResumeLayout(False)
        CType(Me.pbDown, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbUp, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents Panel2 As System.Windows.Forms.Panel
    Friend WithEvents pnlBody As System.Windows.Forms.Panel
    Friend WithEvents WebBrowser1 As WebBrowser
    Friend WithEvents UsrfrmPageHeader1 As usrfrmPageHeader
    Friend WithEvents pnlWeb As Panel
    Friend WithEvents pbDown As PictureBox
    Friend WithEvents pbUp As PictureBox
End Class
