<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class _frmVirtualKeyboard
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(_frmVirtualKeyboard))
        Me.virtualKeyboard1 = New Keyboard.VirtualKeyboard()
        Me.pbSelectAll = New System.Windows.Forms.PictureBox()
        Me.pbCopy = New System.Windows.Forms.PictureBox()
        Me.pbPaste = New System.Windows.Forms.PictureBox()
        Me.pbClose = New System.Windows.Forms.PictureBox()
        CType(Me.pbSelectAll, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbCopy, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbPaste, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'virtualKeyboard1
        '
        Me.virtualKeyboard1.AltGrState = False
        Me.virtualKeyboard1.AltState = False
        Me.virtualKeyboard1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.virtualKeyboard1.BackColor = System.Drawing.Color.Transparent
        Me.virtualKeyboard1.BackgroundColor = System.Drawing.Color.White
        Me.virtualKeyboard1.BeginGradientColor = System.Drawing.Color.FromArgb(CType(CType(184, Byte), Integer), CType(CType(206, Byte), Integer), CType(CType(241, Byte), Integer))
        Me.virtualKeyboard1.BorderColor = System.Drawing.Color.Black
        Me.virtualKeyboard1.ButtonBorderColor = System.Drawing.Color.DarkGray
        Me.virtualKeyboard1.ButtonRectRadius = 5
        Me.virtualKeyboard1.CapsLockState = False
        Me.virtualKeyboard1.ColorPressedState = System.Drawing.Color.FromArgb(CType(CType(254, Byte), Integer), CType(CType(145, Byte), Integer), CType(CType(78, Byte), Integer))
        Me.virtualKeyboard1.CtrlState = False
        Me.virtualKeyboard1.DecimalSeparator = Global.Microsoft.VisualBasic.ChrW(46)
        Me.virtualKeyboard1.EndGradientColor = System.Drawing.Color.FromArgb(CType(CType(134, Byte), Integer), CType(CType(170, Byte), Integer), CType(CType(230, Byte), Integer))
        Me.virtualKeyboard1.FontColor = System.Drawing.Color.Black
        Me.virtualKeyboard1.FontColorShiftDisabled = System.Drawing.Color.DimGray
        Me.virtualKeyboard1.FontColorSpecialKey = System.Drawing.Color.DarkBlue
        Me.virtualKeyboard1.IsNumeric = False
        Me.virtualKeyboard1.LabelFont = New System.Drawing.Font("Tahoma", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.virtualKeyboard1.LabelFontShiftDisabled = New System.Drawing.Font("Tahoma", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.virtualKeyboard1.LabelFontSpecialKey = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(204, Byte))
        Me.virtualKeyboard1.LanguageButtonBottomText = ""
        Me.virtualKeyboard1.LanguageButtonImage = Nothing
        Me.virtualKeyboard1.LanguageButtonTopText = ""
        Me.virtualKeyboard1.LayoutSettings = Nothing
        Me.virtualKeyboard1.Location = New System.Drawing.Point(2, 1)
        Me.virtualKeyboard1.Margin = New System.Windows.Forms.Padding(4)
        Me.virtualKeyboard1.Name = "virtualKeyboard1"
        Me.virtualKeyboard1.NumLockState = False
        Me.virtualKeyboard1.ShadowColor = System.Drawing.Color.DarkGray
        Me.virtualKeyboard1.ShadowShift = 6
        Me.virtualKeyboard1.ShiftState = False
        Me.virtualKeyboard1.ShowBackground = True
        Me.virtualKeyboard1.ShowFunctionButtons = False
        Me.virtualKeyboard1.ShowLanguageButton = False
        Me.virtualKeyboard1.ShowNumericButtons = False
        Me.virtualKeyboard1.Size = New System.Drawing.Size(1102, 347)
        Me.virtualKeyboard1.TabIndex = 1
        Me.virtualKeyboard1.Text = "virtualKeyboard1"
        '
        'pbSelectAll
        '
        Me.pbSelectAll.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbSelectAll.Image = CType(resources.GetObject("pbSelectAll.Image"), System.Drawing.Image)
        Me.pbSelectAll.Location = New System.Drawing.Point(1113, 12)
        Me.pbSelectAll.Name = "pbSelectAll"
        Me.pbSelectAll.Size = New System.Drawing.Size(153, 55)
        Me.pbSelectAll.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbSelectAll.TabIndex = 5
        Me.pbSelectAll.TabStop = False
        '
        'pbCopy
        '
        Me.pbCopy.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbCopy.Image = CType(resources.GetObject("pbCopy.Image"), System.Drawing.Image)
        Me.pbCopy.Location = New System.Drawing.Point(1113, 73)
        Me.pbCopy.Name = "pbCopy"
        Me.pbCopy.Size = New System.Drawing.Size(153, 55)
        Me.pbCopy.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbCopy.TabIndex = 6
        Me.pbCopy.TabStop = False
        '
        'pbPaste
        '
        Me.pbPaste.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbPaste.Image = CType(resources.GetObject("pbPaste.Image"), System.Drawing.Image)
        Me.pbPaste.Location = New System.Drawing.Point(1113, 134)
        Me.pbPaste.Name = "pbPaste"
        Me.pbPaste.Size = New System.Drawing.Size(153, 55)
        Me.pbPaste.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbPaste.TabIndex = 7
        Me.pbPaste.TabStop = False
        '
        'pbClose
        '
        Me.pbClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pbClose.Image = CType(resources.GetObject("pbClose.Image"), System.Drawing.Image)
        Me.pbClose.Location = New System.Drawing.Point(1113, 195)
        Me.pbClose.Name = "pbClose"
        Me.pbClose.Size = New System.Drawing.Size(153, 55)
        Me.pbClose.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.pbClose.TabIndex = 8
        Me.pbClose.TabStop = False
        '
        '_frmVirtualKeyboard
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(8.0!, 16.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1505, 345)
        Me.Controls.Add(Me.pbClose)
        Me.Controls.Add(Me.pbPaste)
        Me.Controls.Add(Me.pbCopy)
        Me.Controls.Add(Me.pbSelectAll)
        Me.Controls.Add(Me.virtualKeyboard1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "_frmVirtualKeyboard"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "KEYBOARD"
        Me.TopMost = True
        CType(Me.pbSelectAll, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbCopy, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbPaste, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbClose, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Private WithEvents virtualKeyboard1 As Keyboard.VirtualKeyboard
    Friend WithEvents pbSelectAll As PictureBox
    Friend WithEvents pbCopy As PictureBox
    Friend WithEvents pbPaste As PictureBox
    Friend WithEvents pbClose As PictureBox
End Class
