<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmConError
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
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.Panel4 = New System.Windows.Forms.Panel()
        Me.LabelX2 = New DevComponents.DotNetBar.LabelX()
        Me.ShapeContainer3 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.RectangleShape2 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape4 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.RectangleShape3 = New Microsoft.VisualBasic.PowerPacks.RectangleShape()
        Me.Panel1.SuspendLayout()
        Me.Panel4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.Panel4)
        Me.Panel1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Panel1.Location = New System.Drawing.Point(0, 0)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(1028, 712)
        Me.Panel1.TabIndex = 1
        '
        'Panel4
        '
        Me.Panel4.BackColor = System.Drawing.Color.Transparent
        Me.Panel4.Controls.Add(Me.LabelX2)
        Me.Panel4.Controls.Add(Me.ShapeContainer3)
        Me.Panel4.Location = New System.Drawing.Point(201, 214)
        Me.Panel4.Name = "Panel4"
        Me.Panel4.Size = New System.Drawing.Size(621, 300)
        Me.Panel4.TabIndex = 9
        '
        'LabelX2
        '
        Me.LabelX2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        '
        '
        '
        Me.LabelX2.BackgroundStyle.Class = ""
        Me.LabelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.LabelX2.Font = New System.Drawing.Font("Trebuchet MS", 18.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LabelX2.ForeColor = System.Drawing.Color.White
        Me.LabelX2.Location = New System.Drawing.Point(264, 62)
        Me.LabelX2.Name = "LabelX2"
        Me.LabelX2.Size = New System.Drawing.Size(331, 161)
        Me.LabelX2.TabIndex = 8
        Me.LabelX2.Text = "Sorry the Information Terminal is currently unavailable. Please ask for assisstan" & _
            "ce."
        Me.LabelX2.WordWrap = True
        '
        'ShapeContainer3
        '
        Me.ShapeContainer3.Location = New System.Drawing.Point(0, 0)
        Me.ShapeContainer3.Margin = New System.Windows.Forms.Padding(0)
        Me.ShapeContainer3.Name = "ShapeContainer3"
        Me.ShapeContainer3.Shapes.AddRange(New Microsoft.VisualBasic.PowerPacks.Shape() {Me.RectangleShape3, Me.RectangleShape2, Me.RectangleShape4})
        Me.ShapeContainer3.Size = New System.Drawing.Size(621, 300)
        Me.ShapeContainer3.TabIndex = 0
        Me.ShapeContainer3.TabStop = False
        '
        'RectangleShape2
        '
        Me.RectangleShape2.BackColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.RectangleShape2.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.RectangleShape2.CornerRadius = 5
        Me.RectangleShape2.FillColor = System.Drawing.Color.FromArgb(CType(CType(0, Byte), Integer), CType(CType(51, Byte), Integer), CType(CType(102, Byte), Integer))
        Me.RectangleShape2.FillGradientColor = System.Drawing.Color.Black
        Me.RectangleShape2.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape2.Location = New System.Drawing.Point(4, 10)
        Me.RectangleShape2.Name = "RectangleShape2"
        Me.RectangleShape2.Size = New System.Drawing.Size(602, 278)
        '
        'RectangleShape4
        '
        Me.RectangleShape4.BackColor = System.Drawing.Color.DimGray
        Me.RectangleShape4.BorderStyle = System.Drawing.Drawing2D.DashStyle.Custom
        Me.RectangleShape4.CornerRadius = 5
        Me.RectangleShape4.FillColor = System.Drawing.Color.FromArgb(CType(CType(48, Byte), Integer), CType(CType(147, Byte), Integer), CType(CType(198, Byte), Integer))
        Me.RectangleShape4.FillGradientColor = System.Drawing.Color.Black
        Me.RectangleShape4.FillStyle = Microsoft.VisualBasic.PowerPacks.FillStyle.Solid
        Me.RectangleShape4.Location = New System.Drawing.Point(15, 17)
        Me.RectangleShape4.Name = "RectangleShape4"
        Me.RectangleShape4.Size = New System.Drawing.Size(598, 277)
        '
        'RectangleShape3
        '
        Me.RectangleShape3.BackgroundImage = Global.SSIT_SERVER.My.Resources.Resources.System_Error_icon_white
        Me.RectangleShape3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.RectangleShape3.BorderColor = System.Drawing.Color.Transparent
        Me.RectangleShape3.CornerRadius = 10
        Me.RectangleShape3.Location = New System.Drawing.Point(32, 40)
        Me.RectangleShape3.Name = "RectangleShape1"
        Me.RectangleShape3.Size = New System.Drawing.Size(233, 216)
        '
        '_frmConError
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1028, 712)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Name = "_frmConError"
        Me.Text = "SET SERVER"
        Me.Panel1.ResumeLayout(False)
        Me.Panel4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents Panel4 As System.Windows.Forms.Panel
    Friend WithEvents LabelX2 As DevComponents.DotNetBar.LabelX
    Friend WithEvents ShapeContainer3 As Microsoft.VisualBasic.PowerPacks.ShapeContainer
    Friend WithEvents RectangleShape3 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape2 As Microsoft.VisualBasic.PowerPacks.RectangleShape
    Friend WithEvents RectangleShape4 As Microsoft.VisualBasic.PowerPacks.RectangleShape
End Class
