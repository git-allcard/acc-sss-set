<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frm2
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
        Me.lblWaitSSS = New DevComponents.DotNetBar.LabelX()
        Me.SuspendLayout()
        '
        'lblWaitSSS
        '
        Me.lblWaitSSS.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblWaitSSS.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        '
        '
        '
        Me.lblWaitSSS.BackgroundStyle.Class = ""
        Me.lblWaitSSS.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblWaitSSS.Font = New System.Drawing.Font("Trebuchet MS", 24.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblWaitSSS.ForeColor = System.Drawing.Color.White
        Me.lblWaitSSS.Location = New System.Drawing.Point(-3, 309)
        Me.lblWaitSSS.Name = "lblWaitSSS"
        Me.lblWaitSSS.Size = New System.Drawing.Size(1024, 104)
        Me.lblWaitSSS.TabIndex = 49
        Me.lblWaitSSS.Text = "PLEASE WAIT WHILE WE PROCESS YOUR TRANSACTION."
        Me.lblWaitSSS.TextAlignment = System.Drawing.StringAlignment.Center
        Me.lblWaitSSS.Visible = False
        Me.lblWaitSSS.WordWrap = True
        '
        '_frm2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1018, 722)
        Me.Controls.Add(Me.lblWaitSSS)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frm2"
        Me.Text = "SET SERVER"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblWaitSSS As DevComponents.DotNetBar.LabelX
End Class
