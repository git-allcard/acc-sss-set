<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class usrfrmFingerprintValidation
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrfrmFingerprintValidation))
        Me.textQuality = New System.Windows.Forms.Label()
        Me.lblMessage = New System.Windows.Forms.Label()
        Me.pbCode = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.pbCode, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'textQuality
        '
        Me.textQuality.AutoSize = True
        Me.textQuality.BackColor = System.Drawing.SystemColors.Control
        Me.textQuality.Location = New System.Drawing.Point(605, 359)
        Me.textQuality.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.textQuality.Name = "textQuality"
        Me.textQuality.Size = New System.Drawing.Size(0, 17)
        Me.textQuality.TabIndex = 39
        '
        'lblMessage
        '
        Me.lblMessage.Anchor = System.Windows.Forms.AnchorStyles.Left
        Me.lblMessage.AutoSize = True
        Me.lblMessage.BackColor = System.Drawing.Color.Transparent
        Me.lblMessage.Font = New System.Drawing.Font("Arial", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMessage.ForeColor = System.Drawing.Color.White
        Me.lblMessage.Location = New System.Drawing.Point(16, 31)
        Me.lblMessage.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblMessage.Name = "lblMessage"
        Me.lblMessage.Size = New System.Drawing.Size(1272, 46)
        Me.lblMessage.TabIndex = 38
        Me.lblMessage.Text = "Place your RIGHT INDEX on the fingerprint scanner for validation..."
        '
        'pbCode
        '
        Me.pbCode.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCode.BackColor = System.Drawing.Color.Transparent
        Me.pbCode.BackgroundImage = CType(resources.GetObject("pbCode.BackgroundImage"), System.Drawing.Image)
        Me.pbCode.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.pbCode.Location = New System.Drawing.Point(1545, -10)
        Me.pbCode.Margin = New System.Windows.Forms.Padding(4)
        Me.pbCode.Name = "pbCode"
        Me.pbCode.Size = New System.Drawing.Size(157, 143)
        Me.pbCode.TabIndex = 40
        Me.pbCode.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.Control
        Me.Label1.Location = New System.Drawing.Point(1557, 89)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(121, 29)
        Me.Label1.TabIndex = 42
        Me.Label1.Text = "CANCEL"
        '
        'usrfrmFingerprintValidation
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.lblMessage)
        Me.Controls.Add(Me.pbCode)
        Me.Controls.Add(Me.textQuality)
        Me.DoubleBuffered = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "usrfrmFingerprintValidation"
        Me.Size = New System.Drawing.Size(1707, 1260)
        CType(Me.pbCode, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents textQuality As System.Windows.Forms.Label
    Friend WithEvents lblMessage As System.Windows.Forms.Label
    Friend WithEvents pbCode As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As System.Windows.Forms.Label

End Class
