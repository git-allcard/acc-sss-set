
Public Class _frm2

    Public CardType As Short
    Private AuthenticationResult As Boolean
    Public Delegate Sub Action()

    Dim ucUMIDCard As usrfrmFingerprintValidation2

    Private Sub UMIDCard()
        ucUMIDCard = New usrfrmFingerprintValidation2(readSettings(xml_Filename, xml_path, "CRN"))
        ucUMIDCard.Dock = DockStyle.Fill
        Me.Controls.Add(ucUMIDCard)
        ucUMIDCard.Parent = Me
    End Sub

    Private Sub _frm2_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Dock = DockStyle.Fill
        Try
            Me.Invoke(New Action(AddressOf UMIDCard))
        Catch ex As Exception

        End Try
    End Sub


End Class