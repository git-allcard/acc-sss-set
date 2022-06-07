<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class usrfrmPageHeader
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(usrfrmPageHeader))
        Me.Panel2 = New System.Windows.Forms.Panel()
        Me.lblCRNNum = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.lblDateofCoverage = New System.Windows.Forms.Label()
        Me.lblMemberName = New System.Windows.Forms.Label()
        Me.lblDateofBirth = New System.Windows.Forms.Label()
        Me.lblSSSNo = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.lblHeader = New System.Windows.Forms.Label()
        'Me.ShapeContainer1 = New Microsoft.VisualBasic.PowerPacks.ShapeContainer()
        Me.Panel2.SuspendLayout()
        Me.SuspendLayout()
        '
        'Panel2
        '
        Me.Panel2.BackgroundImage = CType(resources.GetObject("Panel2.BackgroundImage"), System.Drawing.Image)
        Me.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Panel2.Controls.Add(Me.lblCRNNum)
        Me.Panel2.Controls.Add(Me.Label15)
        Me.Panel2.Controls.Add(Me.lblDateofCoverage)
        Me.Panel2.Controls.Add(Me.lblMemberName)
        Me.Panel2.Controls.Add(Me.lblDateofBirth)
        Me.Panel2.Controls.Add(Me.lblSSSNo)
        Me.Panel2.Controls.Add(Me.Label4)
        Me.Panel2.Controls.Add(Me.Label5)
        Me.Panel2.Controls.Add(Me.Label6)
        Me.Panel2.Controls.Add(Me.Label13)
        Me.Panel2.Controls.Add(Me.lblHeader)
        'Me.Panel2.Controls.Add(Me.ShapeContainer1)
        Me.Panel2.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.8!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Panel2.Location = New System.Drawing.Point(0, 0)
        Me.Panel2.Name = "Panel2"
        Me.Panel2.Size = New System.Drawing.Size(1173, 123)
        Me.Panel2.TabIndex = 2
        '
        'lblCRNNum
        '
        Me.lblCRNNum.AutoSize = True
        Me.lblCRNNum.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblCRNNum.ForeColor = System.Drawing.Color.Black
        Me.lblCRNNum.Location = New System.Drawing.Point(185, 38)
        Me.lblCRNNum.Name = "lblCRNNum"
        Me.lblCRNNum.Size = New System.Drawing.Size(0, 20)
        Me.lblCRNNum.TabIndex = 72
        '
        'Label15
        '
        Me.Label15.AutoSize = True
        Me.Label15.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label15.ForeColor = System.Drawing.Color.Black
        Me.Label15.Location = New System.Drawing.Point(8, 38)
        Me.Label15.Name = "Label15"
        Me.Label15.Size = New System.Drawing.Size(56, 17)
        Me.Label15.TabIndex = 71
        Me.Label15.Text = "C.R.N."
        '
        'lblDateofCoverage
        '
        Me.lblDateofCoverage.AutoSize = True
        Me.lblDateofCoverage.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDateofCoverage.ForeColor = System.Drawing.Color.Black
        Me.lblDateofCoverage.Location = New System.Drawing.Point(954, 88)
        Me.lblDateofCoverage.Name = "lblDateofCoverage"
        Me.lblDateofCoverage.Size = New System.Drawing.Size(185, 20)
        Me.lblDateofCoverage.TabIndex = 65
        Me.lblDateofCoverage.Text = "Date of Coverage :"
        '
        'lblMemberName
        '
        Me.lblMemberName.AutoSize = True
        Me.lblMemberName.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblMemberName.ForeColor = System.Drawing.Color.Black
        Me.lblMemberName.Location = New System.Drawing.Point(185, 88)
        Me.lblMemberName.Name = "lblMemberName"
        Me.lblMemberName.Size = New System.Drawing.Size(158, 20)
        Me.lblMemberName.TabIndex = 64
        Me.lblMemberName.Text = "Member Name :"
        '
        'lblDateofBirth
        '
        Me.lblDateofBirth.AutoSize = True
        Me.lblDateofBirth.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblDateofBirth.ForeColor = System.Drawing.Color.Black
        Me.lblDateofBirth.Location = New System.Drawing.Point(954, 63)
        Me.lblDateofBirth.Name = "lblDateofBirth"
        Me.lblDateofBirth.Size = New System.Drawing.Size(144, 20)
        Me.lblDateofBirth.TabIndex = 63
        Me.lblDateofBirth.Text = "Date of Birth :"
        '
        'lblSSSNo
        '
        Me.lblSSSNo.AutoSize = True
        Me.lblSSSNo.Font = New System.Drawing.Font("Verdana", 9.75!, System.Drawing.FontStyle.Bold)
        Me.lblSSSNo.ForeColor = System.Drawing.Color.Black
        Me.lblSSSNo.Location = New System.Drawing.Point(185, 63)
        Me.lblSSSNo.Name = "lblSSSNo"
        Me.lblSSSNo.Size = New System.Drawing.Size(139, 20)
        Me.lblSSSNo.TabIndex = 62
        Me.lblSSSNo.Text = "SSS Number :"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label4.ForeColor = System.Drawing.Color.Black
        Me.Label4.Location = New System.Drawing.Point(775, 88)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(143, 17)
        Me.Label4.TabIndex = 61
        Me.Label4.Text = "Date of Coverage"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label5.ForeColor = System.Drawing.Color.Black
        Me.Label5.Location = New System.Drawing.Point(8, 88)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(118, 17)
        Me.Label5.TabIndex = 60
        Me.Label5.Text = "Member Name"
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label6.ForeColor = System.Drawing.Color.Black
        Me.Label6.Location = New System.Drawing.Point(775, 63)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(107, 17)
        Me.Label6.TabIndex = 59
        Me.Label6.Text = "Date of Birth"
        '
        'Label13
        '
        Me.Label13.AutoSize = True
        Me.Label13.Font = New System.Drawing.Font("Verdana", 8.25!, System.Drawing.FontStyle.Bold)
        Me.Label13.ForeColor = System.Drawing.Color.Black
        Me.Label13.Location = New System.Drawing.Point(8, 63)
        Me.Label13.Name = "Label13"
        Me.Label13.Size = New System.Drawing.Size(94, 17)
        Me.Label13.TabIndex = 58
        Me.Label13.Text = "SS Number"
        '
        'lblHeader
        '
        Me.lblHeader.AutoSize = True
        Me.lblHeader.Font = New System.Drawing.Font("Verdana", 12.0!, System.Drawing.FontStyle.Bold)
        Me.lblHeader.ForeColor = System.Drawing.Color.DarkBlue
        Me.lblHeader.Location = New System.Drawing.Point(8, 5)
        Me.lblHeader.Name = "lblHeader"
        Me.lblHeader.Size = New System.Drawing.Size(106, 25)
        Me.lblHeader.TabIndex = 35
        Me.lblHeader.Text = "HEADER"
        '
        'ShapeContainer1
        '
        'Me.ShapeContainer1.Location = New System.Drawing.Point(0, 0)
        'Me.ShapeContainer1.Margin = New System.Windows.Forms.Padding(0)
        'Me.ShapeContainer1.Name = "ShapeContainer1"
        'Me.ShapeContainer1.Size = New System.Drawing.Size(1173, 123)
        'Me.ShapeContainer1.TabIndex = 66
        'Me.ShapeContainer1.TabStop = False
        '
        'usrfrmPageHeader
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.Controls.Add(Me.Panel2)
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "usrfrmPageHeader"
        Me.Size = New System.Drawing.Size(1176, 125)
        Me.Panel2.ResumeLayout(False)
        Me.Panel2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Panel2 As Panel
    Friend WithEvents lblCRNNum As Label
    Friend WithEvents Label15 As Label
    Friend WithEvents lblDateofCoverage As Label
    Friend WithEvents lblMemberName As Label
    Friend WithEvents lblDateofBirth As Label
    Friend WithEvents lblSSSNo As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents Label13 As Label
    Friend WithEvents lblHeader As Label
    'Friend WithEvents ShapeContainer1 As PowerPacks.ShapeContainer
End Class
