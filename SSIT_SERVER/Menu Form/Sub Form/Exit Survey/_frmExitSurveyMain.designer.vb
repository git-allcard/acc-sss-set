<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmExitSurveyMain
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
        Me.SplitContainer1 = New System.Windows.Forms.SplitContainer()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        Me.pnlFooter = New System.Windows.Forms.Panel()
        Me.lblStepCntr = New System.Windows.Forms.Label()
        Me.pnlBody = New System.Windows.Forms.Panel()
        Me.btnBack = New System.Windows.Forms.Button()
        Me.btnProceed = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SplitContainer1.Panel1.SuspendLayout()
        Me.SplitContainer1.Panel2.SuspendLayout()
        Me.SplitContainer1.SuspendLayout()
        Me.pnlFooter.SuspendLayout()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'SplitContainer1
        '
        Me.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SplitContainer1.IsSplitterFixed = True
        Me.SplitContainer1.Location = New System.Drawing.Point(0, 0)
        Me.SplitContainer1.Name = "SplitContainer1"
        Me.SplitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal
        '
        'SplitContainer1.Panel1
        '
        Me.SplitContainer1.Panel1.Controls.Add(Me.Label1)
        Me.SplitContainer1.Panel1.Controls.Add(Me.lblHeader)
        '
        'SplitContainer1.Panel2
        '
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlFooter)
        Me.SplitContainer1.Panel2.Controls.Add(Me.pnlBody)
        Me.SplitContainer1.Size = New System.Drawing.Size(898, 598)
        Me.SplitContainer1.SplitterDistance = 128
        Me.SplitContainer1.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Red
        Me.Label1.Location = New System.Drawing.Point(823, 32)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 63)
        Me.Label1.TabIndex = 358
        Me.Label1.Text = "X"
        '
        'lblHeader
        '
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 20.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHeader.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblHeader.Location = New System.Drawing.Point(10, 12)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(781, 107)
        Me.lblHeader.TabIndex = 284
        Me.lblHeader.Text = "SSS Self-Service Express Terminal User Experience Survey"
        '
        'pnlFooter
        '
        Me.pnlFooter.Controls.Add(Me.lblStepCntr)
        Me.pnlFooter.Controls.Add(Me.btnBack)
        Me.pnlFooter.Controls.Add(Me.btnProceed)
        Me.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom
        Me.pnlFooter.Location = New System.Drawing.Point(0, 366)
        Me.pnlFooter.Name = "pnlFooter"
        Me.pnlFooter.Size = New System.Drawing.Size(898, 100)
        Me.pnlFooter.TabIndex = 2
        '
        'lblStepCntr
        '
        Me.lblStepCntr.Font = New System.Drawing.Font("Verdana", 15.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblStepCntr.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblStepCntr.Location = New System.Drawing.Point(12, 19)
        Me.lblStepCntr.Name = "lblStepCntr"
        Me.lblStepCntr.Size = New System.Drawing.Size(195, 63)
        Me.lblStepCntr.TabIndex = 357
        Me.lblStepCntr.Text = "1 / 3"
        Me.lblStepCntr.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pnlBody
        '
        Me.pnlBody.Location = New System.Drawing.Point(0, 4)
        Me.pnlBody.Name = "pnlBody"
        Me.pnlBody.Size = New System.Drawing.Size(900, 340)
        Me.pnlBody.TabIndex = 0
        '
        'btnBack
        '
        Me.btnBack.BackColor = System.Drawing.Color.White
        Me.btnBack.FlatAppearance.BorderSize = 0
        Me.btnBack.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnBack.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnBack.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBack.Font = New System.Drawing.Font("Trebuchet MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBack.ForeColor = System.Drawing.Color.Black
        Me.btnBack.Image = Global.SSIT_SERVER.My.Resources.Resources.BACK_BLUE
        Me.btnBack.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBack.Location = New System.Drawing.Point(623, 3)
        Me.btnBack.Name = "btnBack"
        Me.btnBack.Size = New System.Drawing.Size(140, 88)
        Me.btnBack.TabIndex = 356
        Me.btnBack.Text = "Previous"
        Me.btnBack.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnBack.UseVisualStyleBackColor = False
        '
        'btnProceed
        '
        Me.btnProceed.BackColor = System.Drawing.Color.White
        Me.btnProceed.FlatAppearance.BorderSize = 0
        Me.btnProceed.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White
        Me.btnProceed.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White
        Me.btnProceed.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnProceed.Font = New System.Drawing.Font("Trebuchet MS", 14.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnProceed.ForeColor = System.Drawing.Color.Black
        Me.btnProceed.Image = Global.SSIT_SERVER.My.Resources.Resources.NEXT_BLUE
        Me.btnProceed.ImageAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnProceed.Location = New System.Drawing.Point(748, 3)
        Me.btnProceed.Name = "btnProceed"
        Me.btnProceed.Size = New System.Drawing.Size(140, 88)
        Me.btnProceed.TabIndex = 355
        Me.btnProceed.Text = "Next"
        Me.btnProceed.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.btnProceed.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.SplitContainer1)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(900, 600)
        Me.Panel1.TabIndex = 1
        '
        '_frmExitSurveyMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 19.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(900, 600)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmExitSurveyMain"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "SET SERVER"
        Me.SplitContainer1.Panel1.ResumeLayout(False)
        Me.SplitContainer1.Panel2.ResumeLayout(False)
        CType(Me.SplitContainer1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.SplitContainer1.ResumeLayout(False)
        Me.pnlFooter.ResumeLayout(False)
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents SplitContainer1 As SplitContainer
    Friend WithEvents pnlBody As Panel
    Friend WithEvents lblHeader As Label
    Friend WithEvents pnlFooter As Panel
    Friend WithEvents lblStepCntr As Label
    Friend WithEvents btnBack As Button
    Friend WithEvents btnProceed As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents Panel1 As Panel
End Class
