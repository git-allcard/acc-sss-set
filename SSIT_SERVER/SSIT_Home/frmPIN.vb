
Public Class frmPIN

    Public Sub New(ByVal IsAssignPIN As Boolean)

        ' This call is required by the designer.
        InitializeComponent()
        Me.IsAssignPIN = IsAssignPIN

        Label3.Text = "Please enter your " & RequiredDigits() & "-digit PIN"

        ' Add any initialization after the InitializeComponent() call.
        If Not IsAssignPIN Then
            Label1.Text = "Enter PIN"
            Panel1.Size = New Point(290, 93)
            Label2.Visible = False
            TextBox2.Visible = False
        End If
    End Sub

    Private FocusedTxtbox As Short = 1
    Public IsAssignPIN As Boolean

    Public Success As Boolean = False

    Private Sub EnterPressedKey(ByVal strNumber As String)
        If FocusedTxtbox = 1 And TextBox1.Text.Length < RequiredDigits() Then
            TextBox1.Text = TextBox1.Text & strNumber
        ElseIf FocusedTxtbox = 2 And TextBox2.Text.Length < RequiredDigits() Then
            TextBox2.Text = TextBox2.Text & strNumber
        End If

        ControlFocus()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        EnterPressedKey(1)
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        EnterPressedKey(2)
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        EnterPressedKey(3)
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        EnterPressedKey(4)
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        EnterPressedKey(5)
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        EnterPressedKey(6)
    End Sub

    Private Sub Button7_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button7.Click
        EnterPressedKey(7)
    End Sub

    Private Sub Button8_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button8.Click
        EnterPressedKey(8)
    End Sub

    Private Sub Button9_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button9.Click
        EnterPressedKey(9)
    End Sub

    Private Sub Button10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button10.Click
        EnterPressedKey(0)
    End Sub

    Private Sub cmdReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdReset.Click
        TextBox1.Clear()
        TextBox2.Clear()
        TextBox1.Focus()
        TextBox1.BackColor = Color.White
        TextBox2.BackColor = Color.White
    End Sub

    Private Sub frmPIN_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TextBox1.Focus()
    End Sub

    Private Sub TextBox_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles TextBox1.GotFocus, TextBox2.GotFocus
        If CType(sender, TextBox).Name = "TextBox1" Then
            FocusedTxtbox = 1
        Else
            FocusedTxtbox = 2
        End If
    End Sub

    Private Sub TextBox_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TextBox1.KeyPress, TextBox2.KeyPress
        If e.KeyChar = ChrW(Keys.Back) Then
        ElseIf Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        ElseIf CType(sender, TextBox).Text.Length = RequiredDigits() Then
            e.Handled = True
        End If
    End Sub

    Private Sub TextBox_KeyUp(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyUp, TextBox2.KeyUp
        ControlFocus()
    End Sub

    Private Sub ControlFocus()
        Label4.Text = ""

        If IsAssignPIN Then
            If TextBox1.Text.Length = RequiredDigits() And TextBox2.Text.Length < RequiredDigits() Then
                TextBox2.Focus()
                TextBox2.Select(TextBox2.Text.Length, 1)
            ElseIf TextBox2.Text.Length = RequiredDigits() And TextBox1.Text.Length < RequiredDigits() Then
                TextBox1.Focus()
                TextBox1.Select(TextBox1.Text.Length, 1)
            ElseIf TextBox1.Text.Length = RequiredDigits() And TextBox2.Text.Length = RequiredDigits() Then
                cmdSubmit.Focus()
            End If

            If TextBox1.Text.Length = RequiredDigits() Then
                TextBox1.BackColor = Color.LightGreen
            Else
                TextBox1.BackColor = Color.White
            End If
            If TextBox2.Text.Length = RequiredDigits() Then
                TextBox2.BackColor = Color.LightGreen
            Else
                TextBox2.BackColor = Color.White
            End If
        Else
            If TextBox1.Text.Length < RequiredDigits() Then
                TextBox1.Focus()
                TextBox1.Select(TextBox1.Text.Length, 1)
            ElseIf TextBox1.Text.Length = RequiredDigits() Then
                cmdSubmit.Focus()
            End If

            If TextBox1.Text.Length = RequiredDigits() Then
                TextBox1.BackColor = Color.LightGreen
            Else
                TextBox1.BackColor = Color.White
            End If
        End If
    End Sub

    'Private Sub cmdClosePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
    'Application.Exit()
    'End Sub

    Private Function RequiredDigits() As Short
        Return 6

        'Select Case AppletVersion
        '    Case AllcardUMID.UMIDType.UMID_OLD
        '        Return 6
        '    Case AllcardUMID.UMIDType.UMID_NEW
        '        Return 8
        'End Select
    End Function

    Public ATTMPTCNTR As Short = 0

    Private Sub cmdSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdSubmit.Click
        If IsAssignPIN Then
            If TextBox1.Text.Length <> RequiredDigits() And TextBox2.Text.Length <> RequiredDigits() Then
                Label4.Text = "Please enter " & RequiredDigits() & " digits PIN"
            ElseIf TextBox1.Text <> TextBox2.Text Then
                Label4.Text = "Entered PIN does not match"
            Else
                ATTMPTCNTR = 1
                Success = True
            End If
        Else
            If TextBox1.Text.Length <> RequiredDigits() Then
                Label4.Text = "Please enter " & RequiredDigits() & " digits PIN"
            Else
                'Select Case ATTMPTCNTR
                '    Case 3
                '        If SharedFunction.ShowInfoMessage("WARNING!" & vbNewLine & vbNewLine & "THIS WILL BE YOUR 4TH ATTEMPT TO AUTHENTICATE. YOUR UMID CARD WILL BE BLOCKED AND INVALIDATED AFTER FIVE (5) UNSUCESSFUL TRIES." & vbNewLine & vbNewLine & "DO YOU WANT TO CONTINUE?", MessageBoxButtons.YesNo) = DialogResult.No Then
                '            Success = True
                '        Else
                '            ValidatePIN()
                '        End If
                '        'Case 4
                '        '    If SharedFunction.ShowInfoMessage("WARNING!" & vbNewLine & vbNewLine & "THIS WILL BE YOUR 5TH ATTEMPT TO AUTHENTICATE. YOUR UMID CARD WILL BE BLOCKED AND INVALIDATED AFTER THE 5TH ATTEMPT." & vbNewLine & vbNewLine & "DO YOU WANT TO CONTINUE?", MessageBoxButtons.YesNo) = DialogResult.No Then
                '        '        Success = True
                '        '    Else
                '        '        ValidatePIN()
                '        '    End If
                '    Case Else
                '        ValidatePIN()
                'End Select

                ValidatePIN()
            End If
        End If
    End Sub '

    Private cardBlock As CardBlock

    Private Sub ValidatePIN()
        If SharedFunction.DisablePinOrFingerprint Then
            Success = True
            Return
        End If

        'temporary disable.enable when testing with actual card
        CardPIN = readSettings(xml_Filename, xml_path, "CardPin")
        If TextBox1.Text = CardPIN Then
            'If "NO_PIN" = CardPIN Then
            Success = True
        Else
            ATTMPTCNTR += 1
            If cardBlock Is Nothing Then cardBlock = New CardBlock(readSettings(xml_Filename, xml_path, "SS_Number"))
            cardBlock.attempCntr = ATTMPTCNTR
            cardBlock.SaveCardBlockBySSNUM()

            If ATTMPTCNTR <> SharedFunction.FAILED_MATCHING_LIMIT Then
                'SharedFunction.ShowErrorMessage("INVALID PIN." & vbNewLine & vbNewLine & "YOU HAVE " & (5 - ATTMPTCNTR).ToString & " ATTEMPT" & IIf(ATTMPTCNTR = 4, "", "/S") & " REMAINING.")
                SharedFunction.ShowWarningMessage("INVALID PIN." & vbNewLine & vbNewLine & "YOU HAVE " & (SharedFunction.FAILED_MATCHING_LIMIT - ATTMPTCNTR).ToString & " ATTEMPT" & IIf((SharedFunction.FAILED_MATCHING_LIMIT - ATTMPTCNTR) = 1, "", "(S)") & " REMAINING.")

                TextBox1.Text = ""
                TextBox1.Focus()
                TextBox1.BackColor = Color.White
            Else
                Success = True
            End If
        End If
    End Sub

    Private Sub CloseForm()
        Close()
    End Sub

    'Private Sub cmdClosePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
    'usrfrmUMID.c()
    'End Sub

    Private Sub cmdCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdCancel.Click
        SharedFunction.ShowMainDefaultUserForm()
        Invoke(New Action(AddressOf CloseForm))
    End Sub

    Private Sub frmPIN_Shown(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Shown
        TextBox1.Focus()
    End Sub

    Private Sub cmdClosePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub cmdClosePanel_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click, cmdCancel.Click
        If IsAssignPIN Then
            Success = True
        End If

        ATTMPTCNTR = 0
        Close()
    End Sub

End Class