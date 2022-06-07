
Public Class frmSelectAuth

    Public SelectedAuthType As Short

    Private Sub frmSelectAuth_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        GC.Collect()
        'Dim _userform As New usrfrmFingerprintValidation2("000000000008")
        '_userform.Dock = DockStyle.Fill
        'Me.Controls.Add(_userform)
    End Sub

    Private Sub picPIN_Click(sender As System.Object, e As System.EventArgs) Handles picPIN.Click
        SelectedAuthType = 1
        Close()
    End Sub


    Private Sub picFinger_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles picFinger.Click
        'If DeviceConnectivity.IsFingerprintScannerPresent() Then
        '    SelectedAuthType = 2
        '    Close()
        'Else
        '    SharedFunction.ShowWarningMessage("FINGERSCANNER IS EITHER NOT CONNECTED OF DEFECTIVE.")
        'End If
        SelectedAuthType = 2
        Close()
    End Sub

    Private Sub cmdClosePanel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmdClosePanel.Click
        SelectedAuthType = 0
        Close()
    End Sub
    
End Class