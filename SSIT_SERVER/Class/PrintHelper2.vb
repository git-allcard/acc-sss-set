
Imports System.IO
Imports System.Drawing.Printing

Public Class PrintHelper2
    ' Dim txtDisclaimer As String
    Dim tRep, name, ssnum As String
    Dim br, termNum, ssNo, refNo, memName As String
    Public WithEvents printDoc As New Printing.PrintDocument
    Public footer1 As String
    Friend TextToBePrinted As String
    'Public prntOrFrm As String
    Dim mainInfo As String
    Dim ifLoan As Boolean
    Dim rtTitle As String
    Dim haveDisclaimer As Integer ' 0 FOR NO, 1 FOR YES

    Public Function prtAuthFailedPrint(ByVal NAME As String, ByVal CRN As String)
        Dim sb As New System.Text.StringBuilder

        rtTitle = UCase("UMID CARD AUTHENTICATION")
        tRep = rtTitle

        sb.AppendLine("Name : " & NAME)
        sb.AppendLine("CRN : " & CRN & vbNewLine)
        'sb.AppendLine("YOUR UMID CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE (MSR) AT OUR FRONTLINE SERVICE COUNTER OR" & vbNewLine & "GO TO THE NEAREST SSS BRANCH.")
        'sb.AppendLine("YOUR UMID CARD HAS BEEN BLOCKED AND INVALIDATED." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.")
        sb.AppendLine("The system has failed to authenticate your card. You cannot proceed.".ToUpper & vbNewLine & "You may access your account on the following day.”.ToUpper)

        Return sb.ToString
    End Function

    Public Function prtAuthFailedPrint2(ByVal SSSNum As String)
        Dim sb As New System.Text.StringBuilder

        rtTitle = UCase("SSS CARD AUTHENTICATION")
        tRep = rtTitle

        sb.AppendLine("SS Number : " & SSSNum.Substring(0, 2) & "-" & SSSNum.Substring(2, 7) & "-" & SSSNum.Substring(9, 1) & vbNewLine)
        'sb.AppendLine("Authentication failed. Your fingerprints did not match." & vbNewLine & vbNewLine & "Please seek assistance from the Member Service Representative (MSR)" & vbNewLine & " at our frontline service counter immediately." & vbNewLine & vbNewLine & "Thank you very much.")
        'sb.AppendLine("YOUR SS CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE (MSR) AT OUR FRONTLINE SERVICE COUNTER OR" & vbNewLine & "GO TO THE NEAREST SSS BRANCH.")
        sb.AppendLine("YOUR SS CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.")

        Return sb.ToString
    End Function

    Public Sub prt(ByVal text As String, ByVal printer As String)
        TextToBePrinted = text
        txtDisclaimer()
        'OtherInfo(tRep, name, ssnum)
        'Dim prn As New Printing.PrintDocument
        Using (printDoc)
            printDoc.PrinterSettings.PrinterName = printer
            AddHandler printDoc.PrintPage, _
             AddressOf Me.PrintPageHandler

            If ifLoan = True Then
                Dim ppsize As New PaperSize("Report Size", 430, 430)
                printDoc.DefaultPageSettings.PaperSize = ppsize
            Else
                Dim ppsize As New PaperSize("Report Size", 430, 375)
                printDoc.DefaultPageSettings.PaperSize = ppsize
            End If

            'Dim marginRight, marginLeft, marginTop, marginBottom
            printDoc.OriginAtMargins = True
            printDoc.DefaultPageSettings.Margins.Left = 0
            printDoc.DefaultPageSettings.Margins.Top = 0

            printDoc.ToString()
            Dim dteGet As String = DateTime.Today.ToShortDateString & " " & DateTime.Today.ToShortTimeString
            dteGet = dteGet.Replace("/", "")
            dteGet = dteGet.Replace(":", "")
            printDoc.DocumentName = tRep & " " & ssnum & " " & dteGet
            printDoc.Print()
        End Using
    End Sub

    Private Sub PrintPageHandler(ByVal sender As Object, _
          ByVal args As Printing.PrintPageEventArgs)
        Dim myFont As New Font("Segoe UI", 11)
        Dim bodyFont As New Font("Segoe UI", 9)
        Dim smallFont As New Font("Segoe UI", 6)
        Dim titleFont As New Font("Segoe UI", 12)

        args.Graphics.DrawString(receiptTitle, New Font(titleFont, FontStyle.Bold), _
                       Brushes.Black, 15, 95)
        args.Graphics.DrawString(sssHeader, New Font(myFont, FontStyle.Bold), _
                                    Brushes.Black, 102, 13)

        If haveDisclaimer = 1 Then
            args.Graphics.DrawString(txtDisclaimer, New Font(smallFont, FontStyle.Bold), _
                 Brushes.Black, 20, 325)
            args.Graphics.DrawString(TextToBePrinted, _
      New Font(bodyFont, FontStyle.Regular), _
      Brushes.Black, 20, 130)
        Else
            Dim sample As String
            sample = vbNewLine
            args.Graphics.DrawString(TextToBePrinted, _
           New Font(bodyFont, FontStyle.Regular), _
           Brushes.Black, 20, 130)

            args.Graphics.DrawString("_", New Font(smallFont, FontStyle.Bold), _
               Brushes.AntiqueWhite, 20, 325)
        End If

    End Sub
    Public Function txtDisclaimer() As String
        txtDisclaimer = vbNewLine & "Disclaimer: Information printed on this receipt may differ from the Actual Amount of benefit" & vbNewLine &
                                    "                   or privilege that is due and payable to the member."
        Return txtDisclaimer
    End Function

    Public Function sssHeader() As String
        Dim DateToday As String = Format(Now)

        'sssHeader = "Social Security System" & vbNewLine & _
        '    "Self-Service Information Terminal " & vbNewLine & _
        ' DateToday
        'Return sssHeader

        Return String.Format("{0}{1}{2}{3}{4}", "Social Security System", vbNewLine, SharedFunction.SET_PROJECT_NAME, vbNewLine, DateToday)
    End Function

    Private Function receiptTitle()
        receiptTitle = rtTitle
        Return receiptTitle
    End Function

    Public Function OtherInfo(ByVal tReport As String, ByVal Name As String, ByVal SSnum As String)
        Dim trep, mName, memSSnum
        ' trep = tReport & vbNewLine & vbNewLine
        mName = "Name : " & Name & vbNewLine
        memSSnum = "SSNumber : " & SSnum & vbNewLine & vbNewLine
        OtherInfo = trep & mName & memSSnum
        Return OtherInfo
    End Function

    Public Sub printImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        Dim pcBox As New PictureBox
        Dim newMargins As System.Drawing.Printing.Margins
        pcBox.Image = System.Drawing.Bitmap.FromFile((Application.StartupPath & "\IMAGES\" & "SSS_LOGO_1.png"))
        newMargins = New System.Drawing.Printing.Margins(93, 200, 0, 0)


        printDoc.DefaultPageSettings.Margins = newMargins

        e.Graphics.DrawImage(pcBox.Image, 8, 3)
    End Sub


    Public Function statInfo(ByVal branch As String, ByVal termNo As String, ByVal SSnum As String, ByVal refNum As String, ByVal name As String)
        Dim br, termNum, ssNo, refNo, memName As String

        br = "Branch: " & branch
        termNum = "Terminal No.: " & termNo
        ssNo = "SS Number: " & SSnum
        refNo = "Common Reference Number: " & refNum
        memName = "Name: " & name
        statInfo = vbNewLine & br & vbNewLine & termNum & vbNewLine & ssNo & vbNewLine & refNo & vbNewLine & memName
        Return statInfo

    End Function


    Private Sub PrintPageHandler2(ByVal sender As Object,
   ByVal args As Printing.PrintPageEventArgs)
        Dim myFont As New Font("Segoe UI", 12)
        Dim smallFont As New Font("Segoe UI", 10)
        Dim noteFont As New Font("Segoe UI", 8)
        args.Graphics.DrawString(TextToBePrinted,
           New Font(smallFont, FontStyle.Regular),
           Brushes.Black, 10, 80)
        args.Graphics.DrawString(sssHeader, New Font(myFont, FontStyle.Bold),
                                    Brushes.Black, 100, 20)
        args.Graphics.DrawString(footnote, New Font(noteFont, FontStyle.Bold),
                                Brushes.Black, 10, 353)
    End Sub

    Public Function footnote() As String
        footnote = footer1
        Return footnote
    End Function

    'Public Function sssHeader() As String
    '    Dim DateToday As String
    '    DateToday = Format(Now)

    '    sssHeader = "Social Security System" & vbNewLine & _
    '        " Information Terminal Project " & vbNewLine & _
    '        "  " & DateToday & vbNewLine & vbNewLine
    '    Return sssHeader
    'End Function

End Class
