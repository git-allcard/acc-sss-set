<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSelectAuth
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSelectAuth))
        Me.picPIN = New System.Windows.Forms.PictureBox()
        Me.picFinger = New System.Windows.Forms.PictureBox()
        Me.cmdClosePanel = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.picPIN, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picFinger, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.cmdClosePanel, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'picPIN
        '
        Me.picPIN.BackColor = System.Drawing.Color.Transparent
        Me.picPIN.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picPIN.Location = New System.Drawing.Point(46, 117)
        Me.picPIN.Margin = New System.Windows.Forms.Padding(4)
        Me.picPIN.Name = "picPIN"
        Me.picPIN.Size = New System.Drawing.Size(395, 374)
        Me.picPIN.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picPIN.TabIndex = 0
        Me.picPIN.TabStop = False
        '
        'picFinger
        '
        Me.picFinger.BackColor = System.Drawing.Color.Transparent
        Me.picFinger.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.picFinger.Location = New System.Drawing.Point(510, 117)
        Me.picFinger.Margin = New System.Windows.Forms.Padding(4)
        Me.picFinger.Name = "picFinger"
        Me.picFinger.Size = New System.Drawing.Size(398, 374)
        Me.picFinger.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.picFinger.TabIndex = 27
        Me.picFinger.TabStop = False
        '
        'cmdClosePanel
        '
        Me.cmdClosePanel.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmdClosePanel.BackColor = System.Drawing.Color.Transparent
        Me.cmdClosePanel.BackgroundImage = CType(resources.GetObject("cmdClosePanel.BackgroundImage"), System.Drawing.Image)
        Me.cmdClosePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.cmdClosePanel.Location = New System.Drawing.Point(882, 7)
        Me.cmdClosePanel.Margin = New System.Windows.Forms.Padding(4)
        Me.cmdClosePanel.Name = "cmdClosePanel"
        Me.cmdClosePanel.Size = New System.Drawing.Size(58, 42)
        Me.cmdClosePanel.TabIndex = 42
        Me.cmdClosePanel.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(194, 495)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(91, 41)
        Me.Label1.TabIndex = 43
        Me.Label1.Text = "PIN"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Verdana", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(572, 495)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(289, 41)
        Me.Label2.TabIndex = 44
        Me.Label2.Text = "FINGERPRINT"
        '
        'frmSelectAuth
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(948, 607)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cmdClosePanel)
        Me.Controls.Add(Me.picFinger)
        Me.Controls.Add(Me.picPIN)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "frmSelectAuth"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SET SERVER"
        CType(Me.picPIN, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picFinger, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.cmdClosePanel, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents picPIN As System.Windows.Forms.PictureBox
    Friend WithEvents picFinger As System.Windows.Forms.PictureBox
    Friend WithEvents cmdClosePanel As System.Windows.Forms.PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
