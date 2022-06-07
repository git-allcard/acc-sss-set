<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FindInfo
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
        Me.chk1_1 = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.lblOne = New System.Windows.Forms.Label()
        Me.lblEasy = New DevComponents.DotNetBar.LabelX()
        Me.lblAsterisk = New DevComponents.DotNetBar.LabelX()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.chk1_2 = New DevComponents.DotNetBar.Controls.CheckBoxX()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'chk1_1
        '
        Me.chk1_1.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chk1_1.BackgroundStyle.Class = ""
        Me.chk1_1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chk1_1.CausesValidation = False
        Me.chk1_1.CheckSignSize = New System.Drawing.Size(35, 35)
        Me.chk1_1.Location = New System.Drawing.Point(75, 80)
        Me.chk1_1.Name = "chk1_1"
        Me.chk1_1.Size = New System.Drawing.Size(57, 49)
        Me.chk1_1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chk1_1.TabIndex = 8
        '
        'lblOne
        '
        Me.lblOne.AutoSize = True
        Me.lblOne.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold)
        Me.lblOne.Location = New System.Drawing.Point(156, 87)
        Me.lblOne.Name = "lblOne"
        Me.lblOne.Size = New System.Drawing.Size(69, 32)
        Me.lblOne.TabIndex = 44
        Me.lblOne.Text = "Yes"
        '
        'lblEasy
        '
        '
        '
        '
        Me.lblEasy.BackgroundStyle.Class = ""
        Me.lblEasy.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblEasy.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblEasy.Location = New System.Drawing.Point(26, 17)
        Me.lblEasy.Name = "lblEasy"
        Me.lblEasy.Size = New System.Drawing.Size(747, 46)
        Me.lblEasy.TabIndex = 51
        Me.lblEasy.Text = "Do you find the information you were looking for?"
        Me.lblEasy.WordWrap = True
        '
        'lblAsterisk
        '
        '
        '
        '
        Me.lblAsterisk.BackgroundStyle.Class = ""
        Me.lblAsterisk.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.lblAsterisk.Font = New System.Drawing.Font("Verdana", 14.0!, System.Drawing.FontStyle.Bold)
        Me.lblAsterisk.ForeColor = System.Drawing.Color.Red
        Me.lblAsterisk.Location = New System.Drawing.Point(749, 17)
        Me.lblAsterisk.Name = "lblAsterisk"
        Me.lblAsterisk.Size = New System.Drawing.Size(63, 46)
        Me.lblAsterisk.TabIndex = 54
        Me.lblAsterisk.Text = "*"
        Me.lblAsterisk.WordWrap = True
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.chk1_2)
        Me.Panel1.Controls.Add(Me.Label1)
        Me.Panel1.Controls.Add(Me.chk1_1)
        Me.Panel1.Controls.Add(Me.lblAsterisk)
        Me.Panel1.Controls.Add(Me.lblEasy)
        Me.Panel1.Controls.Add(Me.lblOne)
        Me.Panel1.Location = New System.Drawing.Point(16, 16)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(916, 271)
        Me.Panel1.TabIndex = 55
        '
        'chk1_2
        '
        Me.chk1_2.BackColor = System.Drawing.Color.Transparent
        '
        '
        '
        Me.chk1_2.BackgroundStyle.Class = ""
        Me.chk1_2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square
        Me.chk1_2.CausesValidation = False
        Me.chk1_2.CheckSignSize = New System.Drawing.Size(35, 35)
        Me.chk1_2.Location = New System.Drawing.Point(75, 132)
        Me.chk1_2.Name = "chk1_2"
        Me.chk1_2.Size = New System.Drawing.Size(57, 49)
        Me.chk1_2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled
        Me.chk1_2.TabIndex = 55
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Verdana", 15.75!, System.Drawing.FontStyle.Bold)
        Me.Label1.Location = New System.Drawing.Point(156, 139)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 32)
        Me.Label1.TabIndex = 56
        Me.Label1.Text = "No"
        '
        'FindInfo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel1)
        Me.Name = "FindInfo"
        Me.Size = New System.Drawing.Size(935, 371)
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents chk1_1 As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents lblOne As Label
    Friend WithEvents lblEasy As DevComponents.DotNetBar.LabelX
    Friend WithEvents lblAsterisk As DevComponents.DotNetBar.LabelX
    Friend WithEvents Panel1 As Panel
    Friend WithEvents chk1_2 As DevComponents.DotNetBar.Controls.CheckBoxX
    Friend WithEvents Label1 As Label
End Class
