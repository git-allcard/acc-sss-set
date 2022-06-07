<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmFutronic
    Inherits System.Windows.Forms.Form

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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmFutronic))
        Me.pbGif = New System.Windows.Forms.PictureBox()
        Me.m_textMessage = New System.Windows.Forms.TextBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.lblMinutiaeCnt = New System.Windows.Forms.Label()
        Me.btnDecrease = New System.Windows.Forms.Button()
        Me.lblImageQ = New System.Windows.Forms.Label()
        Me.lblQT = New System.Windows.Forms.Label()
        Me.txtQT = New System.Windows.Forms.TextBox()
        Me.m_picture = New System.Windows.Forms.PictureBox()
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbGif, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        CType(Me.m_picture, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'pbGif
        '
        Me.pbGif.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbGif.BackColor = System.Drawing.Color.White
        Me.pbGif.Location = New System.Drawing.Point(24, 23)
        Me.pbGif.Margin = New System.Windows.Forms.Padding(4)
        Me.pbGif.Name = "pbGif"
        Me.pbGif.Size = New System.Drawing.Size(214, 250)
        Me.pbGif.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbGif.TabIndex = 18
        Me.pbGif.TabStop = False
        '
        'm_textMessage
        '
        Me.m_textMessage.Location = New System.Drawing.Point(493, -1)
        Me.m_textMessage.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.m_textMessage.Name = "m_textMessage"
        Me.m_textMessage.Size = New System.Drawing.Size(76, 22)
        Me.m_textMessage.TabIndex = 25
        Me.m_textMessage.Visible = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.Color.Transparent
        Me.Panel1.Controls.Add(Me.Label8)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.Panel1.Location = New System.Drawing.Point(0, 360)
        Me.Panel1.Margin = New System.Windows.Forms.Padding(4)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(540, 49)
        Me.Panel1.TabIndex = 26
        '
        'Label8
        '
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.Location = New System.Drawing.Point(12, 13)
        Me.Label8.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(515, 22)
        Me.Label8.TabIndex = 0
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMinutiaeCnt
        '
        Me.lblMinutiaeCnt.AutoSize = True
        Me.lblMinutiaeCnt.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMinutiaeCnt.Location = New System.Drawing.Point(576, 1)
        Me.lblMinutiaeCnt.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMinutiaeCnt.Name = "lblMinutiaeCnt"
        Me.lblMinutiaeCnt.Size = New System.Drawing.Size(19, 20)
        Me.lblMinutiaeCnt.TabIndex = 24
        Me.lblMinutiaeCnt.Text = "0"
        Me.lblMinutiaeCnt.Visible = False
        '
        'btnDecrease
        '
        Me.btnDecrease.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnDecrease.Location = New System.Drawing.Point(194, 301)
        Me.btnDecrease.Name = "btnDecrease"
        Me.btnDecrease.Size = New System.Drawing.Size(37, 31)
        Me.btnDecrease.TabIndex = 20
        Me.btnDecrease.Text = "-"
        Me.btnDecrease.UseVisualStyleBackColor = True
        Me.btnDecrease.Visible = False
        '
        'lblImageQ
        '
        Me.lblImageQ.BackColor = System.Drawing.Color.White
        Me.lblImageQ.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblImageQ.Location = New System.Drawing.Point(351, 306)
        Me.lblImageQ.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblImageQ.Name = "lblImageQ"
        Me.lblImageQ.Size = New System.Drawing.Size(66, 33)
        Me.lblImageQ.TabIndex = 1
        Me.lblImageQ.Text = "0"
        Me.lblImageQ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblQT
        '
        Me.lblQT.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblQT.Location = New System.Drawing.Point(22, 300)
        Me.lblQT.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblQT.Name = "lblQT"
        Me.lblQT.Size = New System.Drawing.Size(111, 38)
        Me.lblQT.TabIndex = 17
        Me.lblQT.Text = "QUALITY THRESHOLD"
        '
        'txtQT
        '
        Me.txtQT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtQT.Enabled = False
        Me.txtQT.Font = New System.Drawing.Font("Microsoft Sans Serif", 20.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtQT.Location = New System.Drawing.Point(141, 294)
        Me.txtQT.Margin = New System.Windows.Forms.Padding(3, 2, 3, 2)
        Me.txtQT.Name = "txtQT"
        Me.txtQT.Size = New System.Drawing.Size(50, 45)
        Me.txtQT.TabIndex = 19
        Me.txtQT.Text = "80"
        Me.txtQT.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'm_picture
        '
        Me.m_picture.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.m_picture.BackColor = System.Drawing.Color.White
        Me.m_picture.Location = New System.Drawing.Point(278, 24)
        Me.m_picture.Margin = New System.Windows.Forms.Padding(4)
        Me.m_picture.Name = "m_picture"
        Me.m_picture.Size = New System.Drawing.Size(227, 282)
        Me.m_picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.m_picture.TabIndex = 14
        Me.m_picture.TabStop = False
        '
        'Panel2
        '
        Me.Panel2.BackColor = System.Drawing.Color.Transparent
        Me.Panel2.Controls.Add(Me.Label1)
        Me.Panel2.Controls.Add(Me.lblImageQ)
        Me.Panel2.Controls.Add(Me.pbGif)
        Me.Panel2.Controls.Add(Me.btnDecrease)
        Me.Panel2.Controls.Add(Me.lblQT)
        Me.Panel2.Controls.Add(Me.txtQT)
        Me.Panel2.Controls.Add(Me.m_picture)
        Me.Panel2.Dock = System.Windows.Forms.DockStyle.Top
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(540, 359)
        Me.Panel2.TabIndex = 27
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(277, 315)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(77, 22)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "SCORE :"
        '
        'Timer1
        '
        Me.Timer1.Interval = 1000
        '
        '_frmFutronic
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(540, 409)
        Me.Controls.Add(Me.m_textMessage)
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.lblMinutiaeCnt)
        Me.Controls.Add(Me.Panel2)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "_frmFutronic"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "SCAN FINGER"
        CType(Me.pbGif, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        CType(Me.m_picture, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Private WithEvents pbGif As PictureBox
    Private WithEvents m_textMessage As TextBox
    Friend WithEvents Panel1 As Panel
    Friend WithEvents Label8 As Label
    Friend WithEvents lblMinutiaeCnt As Label
    Friend WithEvents btnDecrease As Button
    Friend WithEvents lblImageQ As Label
    Friend WithEvents lblQT As Label
    Private WithEvents txtQT As TextBox
    Private WithEvents m_picture As PictureBox
    Friend WithEvents Panel2 As Panel
    Friend WithEvents Timer1 As Timer
    Friend WithEvents Label1 As Label
End Class
