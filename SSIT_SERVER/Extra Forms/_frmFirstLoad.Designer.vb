<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmFirstLoad
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
        Me.components = New System.ComponentModel.Container()
        Me.Panel3 = New System.Windows.Forms.Panel()
        Me.CircularProgress1 = New DevComponents.DotNetBar.Controls.CircularProgress()
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.Panel3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel3
        '
        Me.Panel3.Controls.Add(Me.CircularProgress1)
        Me.Panel3.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel3.Location = New System.Drawing.Point(0, 0)
        Me.Panel3.Name = "Panel3"
        Me.Panel3.Size = New System.Drawing.Size(1028, 700)
        Me.Panel3.TabIndex = 7
        '
        'CircularProgress1
        '
        Me.CircularProgress1.AnimationSpeed = 200
        Me.CircularProgress1.BackColor = System.Drawing.Color.AliceBlue
        '
        '
        '
        Me.CircularProgress1.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.CircularProgress1.BackgroundStyle.BorderColor = System.Drawing.Color.White
        Me.CircularProgress1.BackgroundStyle.BorderLeft = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.CircularProgress1.BackgroundStyle.BorderLeftColor = System.Drawing.Color.White
        Me.CircularProgress1.BackgroundStyle.BorderRight = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.CircularProgress1.BackgroundStyle.BorderRightColor = System.Drawing.Color.White
        Me.CircularProgress1.BackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid
        Me.CircularProgress1.BackgroundStyle.BorderTopColor = System.Drawing.Color.White
        Me.CircularProgress1.BackgroundStyle.Class = ""
        Me.CircularProgress1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Rounded
        Me.CircularProgress1.BackgroundStyle.CornerTypeBottomLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.CircularProgress1.BackgroundStyle.CornerTypeBottomRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.CircularProgress1.BackgroundStyle.CornerTypeTopLeft = DevComponents.DotNetBar.eCornerType.Rounded
        Me.CircularProgress1.BackgroundStyle.CornerTypeTopRight = DevComponents.DotNetBar.eCornerType.Rounded
        Me.CircularProgress1.BackgroundStyle.HideMnemonic = True
        Me.CircularProgress1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.CircularProgress1.Enabled = False
        Me.CircularProgress1.Font = New System.Drawing.Font("Trebuchet MS", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CircularProgress1.Location = New System.Drawing.Point(0, 0)
        Me.CircularProgress1.Name = "CircularProgress1"
        Me.CircularProgress1.PieBorderDark = System.Drawing.Color.White
        Me.CircularProgress1.ProgressColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.CircularProgress1.ProgressText = "The Page is Loading. Please wait for a while."
        Me.CircularProgress1.ProgressTextColor = System.Drawing.Color.Black
        Me.CircularProgress1.Size = New System.Drawing.Size(1028, 700)
        Me.CircularProgress1.Style = DevComponents.DotNetBar.eDotNetBarStyle.OfficeXP
        Me.CircularProgress1.TabIndex = 1
        '
        'Timer1
        '
        '
        '_frmFirstLoad
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 700)
        Me.Controls.Add(Me.Panel3)
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmFirstLoad"
        Me.Text = "SET SERVER"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel3.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel3 As System.Windows.Forms.Panel
    Friend WithEvents CircularProgress1 As DevComponents.DotNetBar.Controls.CircularProgress
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
End Class
