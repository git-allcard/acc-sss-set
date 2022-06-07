<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class usrfrmSelectv2
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrfrmSelectv2))
        Me.pbUmid = New System.Windows.Forms.PictureBox()
        Me.pbWebsite = New System.Windows.Forms.PictureBox()
        Me.pbCode = New System.Windows.Forms.PictureBox()
        Me.lblFeedback = New System.Windows.Forms.Label()
        Me.lblCitizenCharter = New System.Windows.Forms.Label()
        Me.pbCitizenCharter = New System.Windows.Forms.PictureBox()
        Me.pbFeedback = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Button1 = New System.Windows.Forms.Button()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.TextBox4 = New System.Windows.Forms.TextBox()
        Me.TextBox5 = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.pbUmid, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbWebsite, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCode, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCitizenCharter, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbFeedback, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbUmid
        '
        Me.pbUmid.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbUmid.BackColor = System.Drawing.Color.Transparent
        Me.pbUmid.Location = New System.Drawing.Point(175, 334)
        Me.pbUmid.Margin = New System.Windows.Forms.Padding(4)
        Me.pbUmid.Name = "pbUmid"
        Me.pbUmid.Size = New System.Drawing.Size(397, 543)
        Me.pbUmid.TabIndex = 15
        Me.pbUmid.TabStop = False
        '
        'pbWebsite
        '
        Me.pbWebsite.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbWebsite.BackColor = System.Drawing.Color.Transparent
        Me.pbWebsite.Location = New System.Drawing.Point(697, 334)
        Me.pbWebsite.Margin = New System.Windows.Forms.Padding(4)
        Me.pbWebsite.Name = "pbWebsite"
        Me.pbWebsite.Size = New System.Drawing.Size(399, 543)
        Me.pbWebsite.TabIndex = 16
        Me.pbWebsite.TabStop = False
        '
        'pbCode
        '
        Me.pbCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.pbCode.BackColor = System.Drawing.Color.Transparent
        Me.pbCode.Location = New System.Drawing.Point(4, 876)
        Me.pbCode.Margin = New System.Windows.Forms.Padding(4)
        Me.pbCode.Name = "pbCode"
        Me.pbCode.Size = New System.Drawing.Size(123, 114)
        Me.pbCode.TabIndex = 18
        Me.pbCode.TabStop = False
        '
        'lblFeedback
        '
        Me.lblFeedback.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFeedback.BackColor = System.Drawing.Color.Transparent
        Me.lblFeedback.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFeedback.ForeColor = System.Drawing.Color.White
        Me.lblFeedback.Location = New System.Drawing.Point(880, 926)
        Me.lblFeedback.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFeedback.Name = "lblFeedback"
        Me.lblFeedback.Size = New System.Drawing.Size(134, 41)
        Me.lblFeedback.TabIndex = 23
        Me.lblFeedback.Text = "SET FEEDBACK"
        Me.lblFeedback.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblCitizenCharter
        '
        Me.lblCitizenCharter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblCitizenCharter.BackColor = System.Drawing.Color.Transparent
        Me.lblCitizenCharter.Font = New System.Drawing.Font("Arial", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCitizenCharter.ForeColor = System.Drawing.Color.White
        Me.lblCitizenCharter.Location = New System.Drawing.Point(1117, 926)
        Me.lblCitizenCharter.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCitizenCharter.Name = "lblCitizenCharter"
        Me.lblCitizenCharter.Size = New System.Drawing.Size(147, 41)
        Me.lblCitizenCharter.TabIndex = 22
        Me.lblCitizenCharter.Text = "SSS CITIZEN'S CHARTER"
        Me.lblCitizenCharter.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbCitizenCharter
        '
        Me.pbCitizenCharter.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCitizenCharter.BackColor = System.Drawing.Color.Transparent
        Me.pbCitizenCharter.BackgroundImage = CType(resources.GetObject("pbCitizenCharter.BackgroundImage"), System.Drawing.Image)
        Me.pbCitizenCharter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbCitizenCharter.Location = New System.Drawing.Point(1062, 912)
        Me.pbCitizenCharter.Margin = New System.Windows.Forms.Padding(4)
        Me.pbCitizenCharter.Name = "pbCitizenCharter"
        Me.pbCitizenCharter.Size = New System.Drawing.Size(59, 55)
        Me.pbCitizenCharter.TabIndex = 21
        Me.pbCitizenCharter.TabStop = False
        '
        'pbFeedback
        '
        Me.pbFeedback.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbFeedback.BackColor = System.Drawing.Color.Transparent
        Me.pbFeedback.BackgroundImage = CType(resources.GetObject("pbFeedback.BackgroundImage"), System.Drawing.Image)
        Me.pbFeedback.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbFeedback.Location = New System.Drawing.Point(838, 912)
        Me.pbFeedback.Margin = New System.Windows.Forms.Padding(4)
        Me.pbFeedback.Name = "pbFeedback"
        Me.pbFeedback.Size = New System.Drawing.Size(59, 55)
        Me.pbFeedback.TabIndex = 19
        Me.pbFeedback.TabStop = False
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Arial", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.DimGray
        Me.Label2.Location = New System.Drawing.Point(230, 792)
        Me.Label2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(755, 40)
        Me.Label2.TabIndex = 24
        Me.Label2.Text = "P  L  E  A  S  E    S  E  L  E  C  T    O  P  T  I  O  N"
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(710, 59)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 26
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(907, 37)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(207, 22)
        Me.TextBox1.TabIndex = 27
        Me.TextBox1.Visible = False
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(907, 65)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(207, 22)
        Me.TextBox2.TabIndex = 28
        Me.TextBox2.Visible = False
        '
        'ComboBox1
        '
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(993, 263)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(251, 24)
        Me.ComboBox1.TabIndex = 29
        Me.ComboBox1.Visible = False
        '
        'TextBox3
        '
        Me.TextBox3.Location = New System.Drawing.Point(907, 93)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.Size = New System.Drawing.Size(207, 22)
        Me.TextBox3.TabIndex = 30
        Me.TextBox3.Visible = False
        '
        'TextBox4
        '
        Me.TextBox4.Location = New System.Drawing.Point(907, 121)
        Me.TextBox4.Name = "TextBox4"
        Me.TextBox4.Size = New System.Drawing.Size(207, 22)
        Me.TextBox4.TabIndex = 31
        Me.TextBox4.Visible = False
        '
        'TextBox5
        '
        Me.TextBox5.Location = New System.Drawing.Point(907, 149)
        Me.TextBox5.Name = "TextBox5"
        Me.TextBox5.Size = New System.Drawing.Size(207, 22)
        Me.TextBox5.TabIndex = 32
        Me.TextBox5.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.Panel1.Location = New System.Drawing.Point(15, 7)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(234, 108)
        Me.Panel1.TabIndex = 33
        Me.Panel1.Visible = False
        '
        'usrfrmSelectv2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.TextBox5)
        Me.Controls.Add(Me.TextBox4)
        Me.Controls.Add(Me.TextBox3)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pbCitizenCharter)
        Me.Controls.Add(Me.pbFeedback)
        Me.Controls.Add(Me.lblFeedback)
        Me.Controls.Add(Me.lblCitizenCharter)
        Me.Controls.Add(Me.pbCode)
        Me.Controls.Add(Me.pbWebsite)
        Me.Controls.Add(Me.pbUmid)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "usrfrmSelectv2"
        Me.Size = New System.Drawing.Size(1280, 993)
        CType(Me.pbUmid, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbWebsite, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCode, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCitizenCharter, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbFeedback, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents pbUmid As System.Windows.Forms.PictureBox
    Friend WithEvents pbWebsite As System.Windows.Forms.PictureBox
    Friend WithEvents pbCode As System.Windows.Forms.PictureBox
    Friend WithEvents lblFeedback As System.Windows.Forms.Label
    Friend WithEvents lblCitizenCharter As System.Windows.Forms.Label
    Friend WithEvents pbCitizenCharter As System.Windows.Forms.PictureBox
    Friend WithEvents pbFeedback As System.Windows.Forms.PictureBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents Button1 As Button
    Friend WithEvents TextBox1 As TextBox
    Friend WithEvents TextBox2 As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents TextBox3 As TextBox
    Friend WithEvents TextBox4 As TextBox
    Friend WithEvents TextBox5 As TextBox
    Friend WithEvents Panel1 As Panel
End Class
