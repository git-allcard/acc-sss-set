<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class _frmExitSurveyIntro
    Inherits System.Windows.Forms.Form

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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmExitSurveyIntro))
        Me.pbSurvey = New System.Windows.Forms.PictureBox()
        Me.pbClose = New System.Windows.Forms.PictureBox()
        Me.Panel1 = New System.Windows.Forms.Panel()
        CType(Me.pbSurvey, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'pbSurvey
        '
        Me.pbSurvey.BackColor = System.Drawing.Color.Transparent
        Me.pbSurvey.Location = New System.Drawing.Point(283, 362)
        Me.pbSurvey.Name = "pbSurvey"
        Me.pbSurvey.Size = New System.Drawing.Size(337, 61)
        Me.pbSurvey.TabIndex = 0
        Me.pbSurvey.TabStop = False
        '
        'pbClose
        '
        Me.pbClose.BackColor = System.Drawing.Color.Transparent
        Me.pbClose.Location = New System.Drawing.Point(283, 436)
        Me.pbClose.Name = "pbClose"
        Me.pbClose.Size = New System.Drawing.Size(337, 66)
        Me.pbClose.TabIndex = 1
        Me.pbClose.TabStop = False
        '
        'Panel1
        '
        Me.Panel1.BackgroundImage = CType(resources.GetObject("Panel1.BackgroundImage"), System.Drawing.Image)
        Me.Panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.pbSurvey)
        Me.Panel1.Controls.Add(Me.pbClose)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(900, 600)
        Me.Panel1.TabIndex = 2
        '
        '_frmExitSurveyIntro
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(900, 600)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmExitSurveyIntro"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "_frmExitSurveyIntro"
        CType(Me.pbSurvey, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents pbSurvey As PictureBox
    Friend WithEvents pbClose As PictureBox
    Friend WithEvents Panel1 As Panel
End Class
