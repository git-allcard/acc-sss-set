Imports System.Net.Mail

Public Class SMTP
    Dim getErrorMsg As String
    Dim result As Integer

    Private SMTP_PORT As Integer = 587

    Public Function sendSmtp(ByVal smtpUser As String, ByVal smtpPass As String, ByVal smtpHost As String, ByVal smtpMailAdd As String _
                             , ByVal smtpSubj As String, ByVal emailAdd As String, ByVal emailBody As String)

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
            Net.NetworkCredential(smtpUser, smtpPass)
            SmtpServer.Port = SMTP_PORT
            SmtpServer.Host = smtpHost
            mail = New MailMessage()
            mail.From = New MailAddress(smtpMailAdd)
            mail.IsBodyHtml = True
            mail.To.Add(emailAdd)
            mail.Bcc.Add("sssmemberassistance@gmail.com")
            mail.Subject = smtpSubj
            mail.Body = emailBody
            'Dim attachment As System.Net.Mail.Attachment
            'attachment = New System.Net.Mail.Attachment(Application.StartupPath & "\files\" & "loan_disclosure.pdf")
            'mail.Attachments.Add(attachment)
            SmtpServer.Send(mail)

            result = 1
            getErrorMsg = result

            SmtpServer.Dispose()

        Catch ex As Exception
            result = 0
            getErrorMsg = result & ex.ToString
        End Try

        Return getErrorMsg
    End Function

    Public Function sendSmtploan(ByVal smtpUser As String, ByVal smtpPass As String, ByVal smtpHost As String, ByVal smtpMailAdd As String _
                           , ByVal smtpSubj As String, ByVal emailAdd As String, ByVal emailBody As String)

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
            Net.NetworkCredential(smtpUser, smtpPass)
            SmtpServer.Port = SMTP_PORT
            SmtpServer.Host = smtpHost
            mail = New MailMessage()
            mail.From = New MailAddress(smtpMailAdd)
            mail.IsBodyHtml = True
            mail.To.Add(emailAdd)
            mail.Bcc.Add("sssmemberassistance@gmail.com")
            mail.Subject = smtpSubj
            mail.Body = emailBody
            'Dim attachment As System.Net.Mail.Attachment
            'attachment = New System.Net.Mail.Attachment(Application.StartupPath & "\files\" & "loan_disclosure.pdf")
            'mail.Attachments.Add(attachment)
            SmtpServer.Send(mail)

            result = 1
            getErrorMsg = result

            SmtpServer.Dispose()

        Catch ex As Exception
            result = 0
            getErrorMsg = result & ex.ToString
        End Try

        Return getErrorMsg
    End Function

    Public Function sendSmtpWebFeedback(ByVal smtpUser As String, ByVal smtpPass As String, ByVal smtpHost As String, ByVal smtpMailAdd As String _
                         , ByVal smtpSubj As String, ByVal emailAdd As String, ByVal emailBody As String)

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New  _
            Net.NetworkCredential(smtpUser, smtpPass)
            SmtpServer.Port = SMTP_PORT
            SmtpServer.Host = smtpHost
            mail = New MailMessage()
            mail.From = New MailAddress(smtpMailAdd)
            mail.IsBodyHtml = True
            mail.To.Add(emailAdd)
            mail.Bcc.Add("sssmemberassistance@gmail.com")
            mail.Bcc.Add("edel_171@yahoo.com")
            mail.Subject = smtpSubj
            mail.Body = emailBody
            SmtpServer.Send(mail)

            result = 1
            getErrorMsg = result

            SmtpServer.Dispose()

        Catch ex As Exception
            result = 0
            getErrorMsg = result & ex.ToString
        End Try

        Return getErrorMsg
    End Function

    Public Sub TestSend(ByVal smtpUser As String, ByVal smtpPass As String, ByVal smtpHost As String, ByVal smtpMailAdd As String _
                           , ByVal smtpSubj As String, ByVal emailAdd As String)

        Try
            Dim SmtpServer As New SmtpClient()
            Dim mail As New MailMessage()
            SmtpServer.Credentials = New Net.NetworkCredential(smtpUser, smtpPass)
            SmtpServer.Port = SMTP_PORT
            SmtpServer.Host = smtpHost
            mail = New MailMessage()
            mail.From = New MailAddress(smtpMailAdd)
            mail.IsBodyHtml = True
            mail.To.Add(emailAdd)
            mail.Subject = smtpSubj
            mail.Body = "TEST"
            SmtpServer.Send(mail)

            result = 1
            getErrorMsg = result

            SmtpServer.Dispose()

        Catch ex As Exception
            result = 0
            getErrorMsg = result & ex.ToString
        End Try
    End Sub

    Public Sub testsend2()
        Try
            Dim mail As New MailMessage
            mail.From = New MailAddress("noreply@sss.gov.ph")
            mail.To.Add("tiucd@sss.gov.ph")
            mail.Subject = "test send"
            mail.Body = "test"

            Dim smtp As New SmtpClient("smtp.gmail.com")
            smtp.UseDefaultCredentials = True
            smtp.Port = 25
            smtp.Send(mail)
            Console.WriteLine("Success")
        Catch ex As Exception
            Console.WriteLine("Failed")
        End Try
    End Sub

End Class
