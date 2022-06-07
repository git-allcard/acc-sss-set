
Public Class usrfrmContactInfo

    Private Sub usrfrmContactInfo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'PopulateContactInfo()
    End Sub

    Public Sub PopulateContactInfo()
        Dim getContact As New getContactInfo
        'If getContact.Exception = "" Then
        '    lblMailingAddress.Text = getContact.MailingAddress
        '    lblLandline.Text = getContact.TelephoneNos
        '    lblEmailAddress.Text = getContact.Email
        '    lblMobile.Text = getContact.MobileNos
        'Else
        '    lblMailingAddress.Text = getContact.MailingAddress
        '    lblLandline.Text = getContact.TelephoneNos
        '    lblEmailAddress.Text = getContact.Email
        '    lblMobile.Text = getContact.MobileNos
        'End If

        lblMailingAddress.Text = getContact.MailingAddress
        lblLandline.Text = getContact.TelephoneNos
        lblEmailAddress.Text = getContact.Email
        lblMobile.Text = getContact.MobileNos
    End Sub

    Private Sub link1_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles link1.LinkClicked
        _frmMainMenu.btnUpdateContactInfo.PerformClick()
    End Sub

End Class
