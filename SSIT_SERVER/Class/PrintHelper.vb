Imports System.IO
Imports System.Drawing.Printing
Imports System.Text.UTF8Encoding
Imports Oracle.DataAccess.Client
Public Class PrintHelper
    ' Dim txtDisclaimer As String
    Dim tRep, name, ssnum As String
    Dim br, termNum, ssNo, refNo, memName As String
    Public WithEvents printDoc As New Printing.PrintDocument
    Public footer1 As String
    Friend TextToBePrinted As String
    Friend txtToBepRinted As New RichTextBox
    Dim mainInfo As String
    Dim ifLoan As Boolean
    Dim rtTitle As String
    Dim haveDisclaimer As Integer ' 0 FOR NO, 1 FOR YES
    Dim temp1, temp2, temp3, temp4 As String
    Dim intLenMsg1, intLenMsg2 As Integer
    Dim tagTrans As Integer = 0
    Dim printF As New printModule
    Dim DateOfCov, dateOfBirth As String
    Public Sub prt(ByVal text As String, ByVal printer As String)
        Try
            txtToBepRinted.Text = text
            txtDisclaimer()
            'OtherInfo(tRep, name, ssnum)
            'Dim prn As New Printing.PrintDocument
            Using (printDoc)
                printDoc.PrinterSettings.PrinterName = printer
                AddHandler printDoc.PrintPage,
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
                PrintDocument()
            End Using
        Catch ex As Exception
            MessageBox.Show("Print: " + ex.Message, "PRINTER", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

    End Sub

    Private Sub PrintPageHandler(ByVal sender As Object,
         ByVal args As Printing.PrintPageEventArgs)
        Try
            Dim myFont As New Font("Segoe UI", 9.5) '11
            Dim bodyFont As New Font("Segoe UI", 8) '9
            Dim smallFont As New Font("Segoe UI", 6)
            Dim titleFont As New Font("Segoe UI", 10) '12
            txtToBepRinted.Width = 100
            txtToBepRinted.WordWrap = True
            Dim sample1 As String
            sample1 = txtToBepRinted.Text

            Dim x As Single = 11 '15
            ' args.Graphics.DrawString(sample1, bodyFont, Brushes.Black, New RectangleF(0, 0, 420, 300))


            args.Graphics.DrawString(receiptTitle, New Font(titleFont, FontStyle.Bold),
                           Brushes.Black, x, 98)
            args.Graphics.DrawString(sssHeader, New Font(myFont, FontStyle.Bold),
                                        Brushes.Black, 92, 13) '102
            args.Graphics.DrawString(sssWebLink, New Font(bodyFont, FontStyle.Bold),
                                       Brushes.Black, 92, 66) '102, 73
            If ifLoan = True Then
                args.Graphics.DrawString(txtDisclaimer, New Font(smallFont, FontStyle.Bold),
                     Brushes.Black, x, 347) '350
                args.Graphics.DrawString(sample1,
          New Font(bodyFont, FontStyle.Regular),
          Brushes.Black, New RectangleF(x, 130, 420, 300))

            ElseIf haveDisclaimer = 1 Then
                args.Graphics.DrawString(txtDisclaimer, New Font(smallFont, FontStyle.Bold),
                     Brushes.Black, x, 322) '325
                args.Graphics.DrawString(sample1,
          New Font(bodyFont, FontStyle.Regular),
          Brushes.Black, New RectangleF(x, 130, 420, 300))

            Else
                Dim sample As String
                sample = vbNewLine
                args.Graphics.DrawString(sample1,
               New Font(bodyFont, FontStyle.Regular),
               Brushes.Black, New RectangleF(x, 130, 420, 300))

                args.Graphics.DrawString("_", New Font(smallFont, FontStyle.Bold),
                   Brushes.AntiqueWhite, x, 325)
            End If
        Catch ex As Exception
            MessageBox.Show("Print: " + ex.Message, "PRINTER", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try


    End Sub

    Public BarcodeImage As String = ""

    Public Sub printImage(ByVal sender As Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles printDoc.PrintPage
        Dim pcBox As New PictureBox
        Dim pcBox2 As PictureBox
        Dim newMargins As System.Drawing.Printing.Margins
        pcBox.Image = System.Drawing.Bitmap.FromFile((Application.StartupPath & "\IMAGES\" & "SSS_LOGO_1.png"))
        If BarcodeImage <> "" Then
            pcBox2 = New PictureBox
            pcBox2.Image = System.Drawing.Bitmap.FromFile(BarcodeImage)
        End If

        newMargins = New System.Drawing.Printing.Margins(93, 200, 0, 0)
        'newMargins = New System.Drawing.Printing.Margins(88, 200, 0, 0)


        printDoc.DefaultPageSettings.Margins = newMargins

        e.Graphics.DrawImage(pcBox.Image, 8, 10)
        If BarcodeImage <> "" Then
            'e.Graphics.DrawImage(pcBox2.Image, 85, 295, 250, 40)
            e.Graphics.DrawImage(pcBox2.Image, 25, 295, 250, 40)
            BarcodeImage = ""
        End If
    End Sub

    Public Function txtDisclaimer() As String
        'txtDisclaimer = vbNewLine & "Disclaimer: Information printed on this receipt may differ from the actual amount of benefit" & vbNewLine & "                   or privilege that is due and payable to the member."
        'Return vbNewLine & "Disclaimer: Information printed on this receipt may differ from the actual amount of benefit" & vbNewLine & "                   or privilege that is due and payable to the member."
        Return vbNewLine & "Disclaimer: Information printed on this receipt may differ from" & vbNewLine & "the actual amount of benefit or privilege that is due and payable to" & vbNewLine & "the member."
    End Function

    Public Function sssHeader() As String
        'Dim DateToday As String = Format(Now)
        Dim DateToday As String = Now.ToString("MM/dd/yyyy hh:mm:ss tt")

        'sssHeader = "Social Security System" & vbNewLine & _
        '    "Self-Service Information Terminal " & vbNewLine & _
        ' DateToday

        Return String.Format("{0}{1}{2}{3}{4}", "Social Security System", vbNewLine, SharedFunction.SET_PROJECT_NAME, vbNewLine, DateToday)
    End Function

    Public Function sssWebLink() As String
        Return "Website : www.sss.gov.ph"
    End Function

    Private Function receiptTitle()
        receiptTitle = rtTitle
        Return receiptTitle
    End Function

    Public Function OtherInfo(ByVal tReport As String, ByVal Name As String, ByVal SSnum As String, ByVal dateOfCov As String, _
                              ByVal dateOfBirth As String)
        '  Dim 

        Dim trep, mName, memSSnum, dateLoc, dateCov
        ' trep = tReport & vbNewLine & vbNewLine
        Name = Name.Replace("�", ChrW(209))
        'Name = Name.Replace("�", ChrW(241))
        mName = "Name : " & Name & vbNewLine
        If SSnum = "" Then

        Else
            If SSnum.Length > 10 Then
                SSnum = SSStempFile
                SSnum = SSnum.Insert(2, "-")
                SSnum = SSnum.Insert(10, "-")
                memSSnum = "SS Number : " & SSnum & vbNewLine
            Else
                SSnum = SSnum.Insert(2, "-")
                SSnum = SSnum.Insert(10, "-")
                memSSnum = "SS Number : " & SSnum & vbNewLine
            End If
        End If
        memSSnum = "SS Number : " & SSnum & vbNewLine
        dateLoc = "Date of Birth: " & dateOfBirth & "         " & "Branch Name: " & kioskBranch & vbNewLine
        dateCov = "Date of Coverage: " & dateOfCov & "       " & "Terminal No: " & kioskID & vbNewLine & vbNewLine
        OtherInfo = trep & mName & memSSnum & dateLoc & dateCov
        Return OtherInfo

    End Function


    Public Function OtherInfoTrans(ByVal tReport As String, ByVal Name As String, ByVal SSnum As String, ByVal brName As String, ByVal brID As String)

        Dim trep, mName, memSSnum, brField
        ' trep = tReport & vbNewLine & vbNewLine
        Name = Name.Replace("�", ChrW(209))
        'Name = Name.Replace("�", ChrW(241))

        mName = "Name : " & Name & vbNewLine

        If SSnum = "" Then

        Else
            If SSnum.Length > 10 Then
                SSnum = SSStempFile
                SSnum = SSnum.Insert(2, "-")
                SSnum = SSnum.Insert(10, "-")
                memSSnum = "SS Number : " & SSnum & vbNewLine
            Else
                SSnum = SSnum.Insert(2, "-")
                SSnum = SSnum.Insert(10, "-")
                memSSnum = "SS Number : " & SSnum & vbNewLine
            End If

        End If

        brField = "Branch Name: " & brName & "          " & "Terminal No: " & brID & vbNewLine & vbNewLine
        OtherInfoTrans = trep & mName & memSSnum & brField

        Return OtherInfoTrans

    End Function

    Public Function prtActualPremium(ByVal Name As String, ByVal SSnumber As String, ByVal dateBirth As String, ByVal dateCoverage As String,
                                 ByVal totalNoContribution As String, ByVal totalAmountContribution As String, ByVal headerName As String, ByVal prntOrFrm As String)

        ifLoan = False
        mainInfo = "Total Number of Contribution : " & totalNoContribution & vbNewLine &
                "Total Amount of Contribution : " & totalAmountContribution & vbNewLine
        If prntOrFrm = "Print" Then
            rtTitle = UCase(headerName)
            tRep = rtTitle
            Name = Name
            ssnum = SSnumber
            DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
            dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
            prtActualPremium = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
            haveDisclaimer = 0
        ElseIf prntOrFrm = "Form" Then
            prtActualPremium = mainInfo
        End If
        mainInfo = Nothing

        Return prtActualPremium
    End Function

    Public Function prtEmpHistory(ByVal Name As String, ByVal SSnumber As String, ByVal employerID As String, ByVal employerName As String, _
                             ByVal reportingDate As String, ByVal employmentDate As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        mainInfo = "CURRENT EMPLOYER" & vbNewLine & _
             "Employer ID : " & employerID & vbNewLine & _
             "Name : " & employerName & vbNewLine & _
             "Reporting Date : " & reportingDate & vbNewLine & _
             "Employment Date : " & employmentDate & vbNewLine
        If prntOrFrm = "Print" Then
            prtEmpHistory = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtEmpHistory = mainInfo
        End If
        Return prtEmpHistory
    End Function

    Public Function prtNOEmpHistory(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, _
                              ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        mainInfo = msg
        If prntOrFrm = "Print" Then
            prtNOEmpHistory = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtNOEmpHistory = mainInfo
        End If
        Return prtNOEmpHistory
    End Function
    Public Function prtFlexiFund(ByVal Name As String, ByVal SSnumber As String, ByVal enrolLDate As String, ByVal memberSince As String, _
                         ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = 
          msg

        If prntOrFrm = "Print" Then
            prtFlexiFund = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtFlexiFund = mainInfo
        End If
        Return prtFlexiFund
    End Function

    Public Function prtFlexiFundEmpty(ByVal Name As String, ByVal SSnumber As String, ByVal resultMsg As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = headerName
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason : " & vbNewLine & _
             resultMsg


        If prntOrFrm = "Print" Then
            prtFlexiFundEmpty = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtFlexiFundEmpty = mainInfo
        End If
        Return prtFlexiFundEmpty
    End Function

    Public Function prtLoanStatus(ByVal Name As String, ByVal SSnumber As String, ByVal loanType01 As String, ByVal AppDate01 As String, _
                         ByVal loanAppStat01 As String, ByVal checkDate01 As String, ByVal loanAmount01 As String, ByVal monthlyAmort As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = True
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        ifLoan = False
        mainInfo = "LOAN APPLICATION DETAILS" & vbNewLine & _
             "Loan Type : " & loanType01 & vbNewLine & _
             "Encoding Date : " & AppDate01 & vbNewLine & _
             "Loan Application Status : " & loanAppStat01 & vbNewLine & _
             "Monthly Amortization : " & monthlyAmort & vbNewLine & _
             "Check Date : " & checkDate01 & vbNewLine & _
             "Loan Amount : " & loanAmount01

        If prntOrFrm = "Print" Then
            prtLoanStatus = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanStatus = mainInfo
        End If


        mainInfo = Nothing
        Return prtLoanStatus
    End Function

    Public Function prtLoanStatusError(ByVal Name As String, ByVal SSnumber As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = True
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason(s) : " & vbNewLine & _
            "MEMBER HAS NO LOAN APPLICATION FILED !" & vbNewLine & _
              "MEMBER HAS NO LOAN RECORD IN DATABASE"

        If prntOrFrm = "Print" Then
            prtLoanStatusError = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanStatusError = mainInfo
        End If


        mainInfo = Nothing
        Return prtLoanStatusError
    End Function

    Public Function prtLoanStatusError2(ByVal Name As String, ByVal SSnumber As String, ByVal loanType As String, ByVal loanDate As String _
                                        , ByVal VouchNum As String, ByVal loanAmt As String, ByVal CertifyEmpID As String, _
                                        ByVal headerName As String, ByVal prntOrFrm As String)
        'Public Function prtLoanStatusError2(ByVal Name As String, ByVal SSnumber As String, _
        '                                        ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = True
        haveDisclaimer = 0
        rtTitle = UCase(HeaderName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "MEMBER HAS NO LOAN APPLICATION FILED !" & vbNewLine & _
                    "List of Availed Loan(s)" & vbNewLine & _
                "Loan Type: " & loanType & vbNewLine & _
                 "Check Date / Loan Date: " & loanDate & vbNewLine & _
                  "Check Number / Voucher Number: " & VouchNum & vbNewLine & _
                "Loan Amount: " & loanAmt & vbNewLine & _
                   "Certifying Employer ID: " & CertifyEmpID


        If prntOrFrm = "Print" Then
            prtLoanStatusError2 = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanStatusError2 = mainInfo
        End If


        mainInfo = Nothing
        Return prtLoanStatusError2
    End Function

    Public Function prtLoanStatusError3(ByVal Name As String, ByVal SSnumber As String, ByVal loanType As String, ByVal appDate As String, _
                                        ByVal loanAppStat As String, ByVal checkDate As String, ByVal loanAmt As String, ByVal monthlyAmort As String, _
                                        ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = True
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        'original
        'mainInfo = "LOAN APPLICATION DETAILS and LOAN INFORMATION DETAILS" & vbNewLine & _
        '    "Loan Type: " & loanType & "   " & "Monthly Amrt: " & monthlyAmort & vbNewLine & _
        '    "Encoding Date: " & appDate & vbNewLine & _
        '    "Loan Application Status: " & loanAppStat & vbNewLine & _
        '    "Loan Amount: " & loanAmt & vbNewLine & _
        '    "Check Date: " & checkDate & vbNewLine & _
        '    "MEMBER HAS NO LOAN RECORD IN DATABASE"

        'revised per grace advised 02/16/2017
        mainInfo = "LOAN APPLICATION DETAILS and LOAN INFORMATION DETAILS" & vbNewLine & _
             "Loan Type: " & loanType & vbNewLine & _
             "Encoding Date: " & appDate & vbNewLine & _
             "Loan Application Status: " & loanAppStat & vbNewLine & _
             "MEMBER HAS NO LOAN RECORD IN DATABASE"





        If prntOrFrm = "Print" Then
            prtLoanStatusError3 = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanStatusError3 = mainInfo
        End If


        mainInfo = Nothing
        Return prtLoanStatusError3
    End Function

    Public Function prtLoanStatusCredited(ByVal Name As String, ByVal SSnumber As String, ByVal credLoanType As String, ByVal credCheckDate As String, _
                     ByVal credLoanAmt As String, ByVal stateAccAsOf As String, ByVal amtDue As String, ByVal amtNotYetDue As String, _
                     ByVal totAmtOblig As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber

        '    mainInfo = "LOAN APPLICATION DETAILS" & vbNewLine & _
        '         "Loan Type : " & credLoanType & vbNewLine & _
        '         "Check Date : " & credCheckDate & vbNewLine & _
        '         "Loan Amount : " & credLoanAmt & vbNewLine & _
        '          stateAccAsOf & vbNewLine & _
        '          "Amount Due : " & amtDue & vbNewLine & _
        '          "Amount Not Yet Due : " & amtNotYetDue & vbNewLine & _
        '"Total Amt of Obligation : " & totAmtOblig

        mainInfo = "LOAN APPLICATION DETAILS" & vbNewLine & _
            "Loan Type : " & credLoanType & vbNewLine & _
            "Loan Date : " & credCheckDate & vbNewLine & _
            "Loan Amount : " & credLoanAmt & vbNewLine & _
             stateAccAsOf & vbNewLine & _
             "Total Amount Due : " & amtDue & vbNewLine & _
   "Total Amt of Obligation : " & totAmtOblig



        If prntOrFrm = "Print" Then
            prtLoanStatusCredited = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanStatusCredited = mainInfo
        End If

        mainInfo = Nothing
        Return prtLoanStatusCredited
    End Function

    Public Function prtMemDetails(ByVal Name As String, ByVal SSnumber As String, ByVal memberDetails As String, ByVal dateOfBirth As String, _
                                   ByVal coverageStatus As String, ByVal recordLocation As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        haveDisclaimer = 0
        ssnum = SSnumber
        mainInfo = "SS Number Status : " & memberDetails & vbNewLine & _
                                   "Record Location : " & recordLocation

        If prntOrFrm = "Print" Then
            prtMemDetails = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMemDetails = mainInfo
        End If
        mainInfo = Nothing


    End Function


    Public Function prtSSidClearance(ByVal Name As String, ByVal SSnumber As String, ByVal serialNo As String, ByVal capturedON As String, _
                                ByVal captureDate As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        mainInfo = "Card / Serial No / Job No : " & serialNo & vbNewLine & vbNewLine & _
              "Remarks" & vbNewLine & _
             "Captured ON : " & capturedON & vbNewLine & _
             "Capture Site : " & captureDate
        If prntOrFrm = "Print" Then
            prtSSidClearance = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSSidClearance = mainInfo
        End If
        mainInfo = Nothing
        Return prtSSidClearance
    End Function

    'Public Function prtSSidClearance(ByVal Name As String, ByVal SSnumber As String, ByVal serialNo As String, ByVal capturedON As String, _
    '                        ByVal generatedON As String, ByVal captureDate As String, ByVal headerName As String, ByVal prntOrFrm As String)

    '    DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
    '    dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
    '    ifLoan = False
    '    rtTitle = UCase(headerName)
    '    tRep = rtTitle
    '    Name = Name
    '    ssnum = SSnumber
    '    haveDisclaimer = 0
    '    mainInfo = "Card / Serial No / Job No : " & serialNo & vbNewLine & _
    '          "Remarks" & vbNewLine & _
    '         "Captured ON : " & capturedON & vbNewLine & _
    '         "Generated ON : " & generatedON & vbNewLine & _
    '         "Capture Site : " & captureDate
    '    If prntOrFrm = "Print" Then
    '        prtSSidClearance = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
    '    ElseIf prntOrFrm = "Form" Then
    '        prtSSidClearance = mainInfo
    '    End If
    '    mainInfo = Nothing
    '    Return prtSSidClearance
    'End Function


    Public Function prtSSidClearanceEmpty(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        haveDisclaimer = 0
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason : " & vbNewLine & msg
        If prntOrFrm = "Print" Then
            prtSSidClearanceEmpty = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSSidClearanceEmpty = mainInfo
        End If
        mainInfo = Nothing
        Return prtSSidClearanceEmpty
    End Function

    Public Function prtDDREligibilityError1(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = msg

        If prntOrFrm = "Print" Then
            prtDDREligibilityError1 = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDDREligibilityError1 = mainInfo
        End If
        mainInfo = Nothing
        Return prtDDREligibilityError1

    End Function

    Public Function prtDDREligibilityError2(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = msg

        If prntOrFrm = "Print" Then
            prtDDREligibilityError2 = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDDREligibilityError2 = mainInfo
        End If
        mainInfo = Nothing
        Return prtDDREligibilityError2

    End Function

    Public Function prtDDREligibility(ByVal Name As String, ByVal SSnumber As String, ByVal retirementDate As String, ByVal postContri As String, _
                        ByVal lumpContri As String, ByVal dmPaidContri As String, ByVal semContingency As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Earliest Retirement Date : " & retirementDate & vbNewLine &
             "Total Number of Posted Contributions : " & postContri & vbNewLine &
             "Total Number of Lumped Contributions : " & lumpContri & vbNewLine &
             "Total Number of Deemed Paid Contributions : " & dmPaidContri & vbNewLine &
             "Total Number of Contributions Prior to Semester of" & vbNewLine &
             "Contingency: " & semContingency

        '"Total Number of Contributions Prior to Semester of Contingency: " & semContingency

        If prntOrFrm = "Print" Then
            prtDDREligibility = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDDREligibility = mainInfo
        End If
        mainInfo = Nothing
        Return prtDDREligibility

    End Function

    Public Function prtSSFuneral(ByVal Name As String, ByVal SSnumber As String, ByVal typeOfBenefit As String, ByVal amtbenefit As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "DDRF CLAIM ELIGIBILITY" & vbNewLine &
            "Type of Benefit : " & typeOfBenefit & vbNewLine & "Amount of Benefit : " & amtbenefit


        If prntOrFrm = "Print" Then
            prtSSFuneral = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSSFuneral = mainInfo
        End If
        mainInfo = Nothing
        Return prtSSFuneral

    End Function

    Public Function prtSSFuneralErr(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal msg2 As String, ByVal msg3 As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason(s) :" & vbNewLine & _
           msg2 & vbNewLine & _
           msg3

        If prntOrFrm = "Print" Then
            prtSSFuneralErr = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSSFuneralErr = mainInfo
        End If
        mainInfo = Nothing
        Return prtSSFuneralErr

    End Function

    Public Function prtDisabilityPartialEligibility(ByVal Name As String, ByVal SSnumber As String, ByVal AMSC As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = AMSC

        If prntOrFrm = "Print" Then
            prtDisabilityPartialEligibility = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDisabilityPartialEligibility = mainInfo
        End If
        mainInfo = Nothing
        Return prtDisabilityPartialEligibility

    End Function

    Public Function prtBenEligDeath(ByVal Name As String, ByVal SSnumber As String, ByVal averageMSC As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber


        mainInfo = averageMSC

        If prntOrFrm = "Print" Then
            prtBenEligDeath = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenEligDeath = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenEligDeath

    End Function

    Public Function prtBenEligDeathErr(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal msg2 As String, ByVal msg3 As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber


        mainInfo = "Reason(s) :" & vbNewLine & _
        msg & vbNewLine & _
        msg2 & vbNewLine & msg3

        If prntOrFrm = "Print" Then
            prtBenEligDeathErr = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenEligDeathErr = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenEligDeathErr

    End Function

    Public Function prtBenEligTotDisable(ByVal Name As String, ByVal SSnumber As String, ByVal AveMSC As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = "BENEFITS ELIGIBILITY"
        tRep = rtTitle
        haveDisclaimer = 1
        Name = Name
        ssnum = SSnumber
        mainInfo = AveMSC

        prtBenEligTotDisable = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo


        If prntOrFrm = "Print" Then
            prtBenEligTotDisable = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenEligTotDisable = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenEligTotDisable
    End Function

    Public Function prtLoanEligibilty(ByVal Name As String, ByVal SSnumber As String, ByVal loanMonth As String, ByVal msc As String, _
                              ByVal loanAmount As String, ByVal prevLoanBal As String, ByVal serviceFee As String, ByVal loanProceeds As String, _
                              ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Salary Loan" & vbNewLine &
             "Loanable Month : " & loanMonth & vbNewLine &
             "Average Monthly Salary Credit  : " & msc & vbNewLine &
             "(A) Loanable Amount : " & loanAmount & vbNewLine &
             "(less) (B) Previous Loan Balances : " & prevLoanBal & vbNewLine &
             "(less) (C) Service Fee (1% of (A) Loanable" & vbNewLine &
             "           Amount) : " & serviceFee & vbNewLine &
             "Loan Proceeds (A) - (B) - (C) : " & loanProceeds

        If prntOrFrm = "Print" Then
            prtLoanEligibilty = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanEligibilty = mainInfo
        End If
        mainInfo = Nothing
        Return prtLoanEligibilty

    End Function

    Public Function prtLoanEligibiltyEmpty(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        haveDisclaimer = 0
        Name = Name
        ssnum = SSnumber

        mainInfo = msg

        If prntOrFrm = "Print" Then
            prtLoanEligibiltyEmpty = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtLoanEligibiltyEmpty = mainInfo
        End If
        mainInfo = Nothing
        Return prtLoanEligibiltyEmpty

    End Function

    Public Function prtMaterniryClaim(ByVal Name As String, ByVal SSnumber As String, ByVal claimNo As String, ByVal delivDate As String, ByVal days As String, _
                          ByVal amtPaid As String, ByVal statChkNo As String, ByVal statDate As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber

        Dim statusDate As String = "Status Date : " & statDate
        If statDate = "" Then statusDate = ""

        '"Status/CHK No. : " & statChkNo & vbNewLine & _

        mainInfo = "Claim No. : " & claimNo & vbNewLine &
             "Delivery Date : " & delivDate & vbNewLine &
             "Days : " & days & vbNewLine & vbNewLine &
             "Amount Paid : " & amtPaid & vbNewLine &
             "Status : " & statChkNo & vbNewLine &
             statusDate
        haveDisclaimer = 1
        If prntOrFrm = "Print" Then
            prtMaterniryClaim = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMaterniryClaim = mainInfo
        End If
        mainInfo = Nothing
        Return prtMaterniryClaim

    End Function

    Public Function prtMaterniryClaimNoReturn(ByVal Name As String, ByVal SSnumber As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason : " & vbNewLine & _
             "Member has no maternity claim filed as of date."
        haveDisclaimer = 0
        If prntOrFrm = "Print" Then
            prtMaterniryClaimNoReturn = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMaterniryClaimNoReturn = mainInfo
        End If
        mainInfo = Nothing
        Return prtMaterniryClaimNoReturn

    End Function

    Public Function prtMaterniryClaimStatus(ByVal Name As String, ByVal SSnumber As String, ByVal dateFiled As String, ByVal BenefitAppStat As String, _
                           ByVal deliveryDate As String, ByVal NoOfDays As String, ByVal checkDate As String, ByVal amount As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "MATERNITY APPLICATION DETAILS" & vbNewLine & _
             "Date Filed: " & dateFiled & vbNewLine & _
             "Benefit app status: " & BenefitAppStat & vbNewLine & _
             "Delivery Date: " & deliveryDate & vbNewLine & _
             "Check Date: " & checkDate & vbNewLine & _
             "Number of Days: " & NoOfDays & vbNewLine & _
             "Amount: " & amount & vbNewLine

        If BenefitAppStat = "SETTLED" Then
            haveDisclaimer = 0
        Else
            haveDisclaimer = 1
        End If

        If prntOrFrm = "Print" Then
            prtMaterniryClaimStatus = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMaterniryClaimStatus = mainInfo
        End If
        mainInfo = Nothing
        Return prtMaterniryClaimStatus

    End Function

    Public Function prtSicknessClaims(ByVal Name As String, ByVal SSnumber As String, ByVal dateFiled As String, ByVal BenefitAppStat As String, _
                          ByVal dateConfined As String, ByVal NoOfDays As String, ByVal checkDate As String, ByVal amount As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "AVAILABLE BENEFITS" & vbNewLine & _
            "Type of Benefit : " & BenefitAppStat & vbNewLine & _
            "Confinement Start : " & dateConfined & vbNewLine & _
             "Number of Days Claimed : " & NoOfDays & vbNewLine & _
             "Confinement End : " & checkDate & vbNewLine & _
             "Amount of Benefit : " & amount & vbNewLine
        If prntOrFrm = "Print" Then
            prtSicknessClaims = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSicknessClaims = mainInfo
        End If
        mainInfo = Nothing
        Return prtSicknessClaims
    End Function

    Public Function prtSicknessEligiblity(ByVal Name As String, ByVal SSnumber As String, ByVal confinementStart As String, ByVal confinementEnd As String, _
                      ByVal totalMonthluSalaryCredit As String, ByVal DailySicknessAllowance As String, ByVal AmountOfBenefit As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        haveDisclaimer = 1
        Name = Name
        ssnum = SSnumber
        mainInfo = "Confinement Start: " & confinementStart & vbNewLine & _
             "Confinement End: " & confinementEnd & vbNewLine & _
             "Total Monthly Salary Credit: " & totalMonthluSalaryCredit & vbNewLine & _
             "Daily Sickness  Allowance: " & DailySicknessAllowance & vbNewLine & _
             "Amount of  Benefit: " & AmountOfBenefit & vbNewLine
        If prntOrFrm = "Print" Then
            prtSicknessEligiblity = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSicknessEligiblity = mainInfo
        End If
        mainInfo = Nothing
        Return prtSicknessEligiblity

    End Function

    Public Function prtSicknessError(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = msg


        If prntOrFrm = "Print" Then
            prtSicknessError = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSicknessError = mainInfo
        End If
        mainInfo = Nothing
        Return prtSicknessError
    End Function

    Public Function prtSicknessBenError(ByVal Name As String, ByVal SSnumber As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason :" & vbNewLine & _
            "Member has no Sickness Claim!"


        If prntOrFrm = "Print" Then
            prtSicknessBenError = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSicknessBenError = mainInfo
        End If
        mainInfo = Nothing
        Return prtSicknessBenError
    End Function

    Public Function prtMaternityError(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")



        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = msg.Trim


        If prntOrFrm = "Print" Then
            prtMaternityError = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMaternityError = mainInfo
        End If
        mainInfo = Nothing
        Return prtMaternityError
    End Function
    ' class1.prt(class1.prtMaternityElig(fullnameprint, xtd.getCRN, "", MAtTypeBenefit, MatAmtBen, MatDaysClaims, MatAllowance, My.Settings.webPageTag, "Print"), DefaultPrinterName)
    Public Function prtSicnkessBenefitInfo(ByVal Name As String, ByVal SSnumber As String, ByVal dateFiled As String, ByVal remarks As String, _
                  ByVal confinePeriod As String, ByVal amtPaid As String, ByVal startDate As String, ByVal endDate As String, _
                 ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")


        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "SS SICKNESS CLAIM INFORMATION" & vbNewLine & _
             "Date Filed: " & dateFiled & vbNewLine & _
             "Remarks : " & remarks & vbNewLine & _
             "Start Date : " & startDate & vbNewLine & _
             "End Date : " & endDate & vbNewLine & _
             "Confinement Period : " & confinePeriod & vbNewLine & _
             "Amount Paid : " & amtPaid

        If prntOrFrm = "Print" Then
            prtSicnkessBenefitInfo = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSicnkessBenefitInfo = mainInfo
        End If
        mainInfo = Nothing
        Return prtSicnkessBenefitInfo
    End Function

    Public Function prtMaternityElig(ByVal Name As String, ByVal SSnumber As String, ByVal getMatType As String, ByVal getAmtBen As String, _
                  ByVal GetNumDays As String, ByVal getDailyMatAllow As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "AVAILABLE BENEFITS" & vbNewLine & _
             "Type of Benefit : " & getMatType & vbNewLine & _
             "Amount of Benefit : " & getAmtBen & vbNewLine & _
             "Number of Days Claimed : " & GetNumDays & vbNewLine & _
             "Daily Maternity Allowance : " & getDailyMatAllow & vbNewLine
        If prntOrFrm = "Print" Then
            prtMaternityElig = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMaternityElig = mainInfo
        End If
        mainInfo = Nothing
        Return prtMaternityElig
    End Function
    Public Function prtBenClaim(ByVal Name As String, ByVal SSnumber As String, ByVal GetBenClaim As String, ByVal GetBenDateCon As String, ByVal GetBenStatus As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "List of Availed Benefit Claim(s)" & vbNewLine & _
              "Claim Type : " & GetBenClaim & vbNewLine & _
             "Date of Contingency : " & GetBenDateCon & vbNewLine & _
             "Status : " & GetBenStatus
        If prntOrFrm = "Print" Then
            prtBenClaim = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenClaim = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenClaim
    End Function

    Public Function prtBenClaimApplication(ByVal Name As String, ByVal SSnumber As String, ByVal claimFiled As String, ByVal dateFiled As String, ByVal filedAt As String, ByVal statusOf As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Benefit Claim Application Details" & vbNewLine & _
              "Claim Filed : " & claimFiled & vbNewLine & _
             "Date Filed : " & dateFiled & vbNewLine & _
            "Filed at : " & filedAt & vbNewLine & _
             "Status as of : " & statusOf

        If prntOrFrm = "Print" Then
            prtBenClaimApplication = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenClaimApplication = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenClaimApplication
    End Function

    Public Function prtBenClaimInformation(ByVal Name As String, ByVal SSnumber As String, ByVal GetBenDateCon As String, ByVal GetBenStatus As String, ByVal GetBenSetdate As String, ByVal GetBenDateName As String, ByVal GetBenDate As String, _
                                            ByVal GetBenDatePen As String, ByVal amtIntBen As String, ByVal totMP As String, ByVal headerName As String, ByVal prntOrFrm As String)


        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber

        If GetBenDateName = "1" Then
            GetBenDateName = "Withdrawal Date : "

            mainInfo = "RETIREMENT CLAIM INFORMATION" & vbNewLine & _
                         "Date of Contingency : " & GetBenDateCon & vbNewLine & _
             "Claim Status : " & GetBenStatus & vbNewLine & _
            "Settlement Date : " & GetBenSetdate & vbNewLine & _
            GetBenDateName & GetBenDate & vbNewLine & _
            "Pension Start Date : " & GetBenDatePen & vbNewLine & _
            "Amount of Initial Benefit : " & amtIntBen & vbNewLine & _
            "Total Monthly Pension : " & totMP
        Else
            GetBenDateName = "Pension Start Date : "

            mainInfo = "SSS PARTIAL PERMANENT DISABILITY CLAIM INFORMATION" & vbNewLine & _
                         "Date of Contingency : " & GetBenDateCon & vbNewLine & _
             "Claim Status : " & GetBenStatus & vbNewLine & _
            "Settlement Date : " & GetBenSetdate & vbNewLine & _
            GetBenDateName & GetBenDatePen & vbNewLine & _
            "Amount of Initial Benefit : " & amtIntBen & vbNewLine & _
            "Basic Monthly Pension : " & totMP
        End If


        If prntOrFrm = "Print" Then
            prtBenClaimInformation = OtherInfo(tRep, Name, ssnum, DateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenClaimInformation = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenClaimInformation
    End Function

    Public Function prtBenClaimNoReturn(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Reason : " & vbNewLine & msg

        If prntOrFrm = "Print" Then
            prtBenClaimNoReturn = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtBenClaimNoReturn = mainInfo
        End If
        mainInfo = Nothing
        Return prtBenClaimNoReturn
    End Function

    Public Function prtDisabilityEligibilty(ByVal Name As String, ByVal SSnumber As String, ByVal AMSC As String, ByVal TOB As String, ByVal MAB As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")
        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Disability Eligibility " & vbNewLine & _
            "Average Monthly Salary Credit : " & AMSC & vbNewLine & _
             "Type of Benefit : " & TOB & vbNewLine & _
         "Minimum  Amount of Benefit : " & MAB

        If prntOrFrm = "Print" Then
            prtDisabilityEligibilty = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDisabilityEligibilty = mainInfo
        End If
        mainInfo = Nothing
        Return prtDisabilityEligibilty

    End Function

    Public Function prtDisabilityEligibiltyError(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal msg2 As String, ByVal msg3 As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Error(s) : " & vbNewLine & _
            msg & vbNewLine & _
        msg2 & vbNewLine & _
        msg3

        If prntOrFrm = "Print" Then
            prtDisabilityEligibiltyError = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtDisabilityEligibiltyError = mainInfo
        End If
        mainInfo = Nothing
        Return prtDisabilityEligibiltyError

    End Function

    Public Function prtTotalEligibilty(ByVal Name As String, ByVal SSnumber As String, ByVal AMSC As String, ByVal TOB As String, ByVal MAB As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Disability Eligibility " & vbNewLine & _
            "Average Monthly Salary Credit : " & AMSC & vbNewLine & _
             "Type of Benefit : " & TOB & vbNewLine & _
         "Minimum  Amount of Benefit : " & MAB

        If prntOrFrm = "Print" Then
            prtTotalEligibilty = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtTotalEligibilty = mainInfo
        End If
        mainInfo = Nothing
        Return prtTotalEligibilty

    End Function

    Public Function prtTotalEligibiltyError(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal msg2 As String, ByVal msg3 As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Error(s) : " & vbNewLine & _
            msg & vbNewLine & _
        msg2 & vbNewLine & _
        msg3

        If prntOrFrm = "Print" Then
            prtTotalEligibiltyError = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtTotalEligibiltyError = mainInfo
        End If
        mainInfo = Nothing
        Return prtTotalEligibiltyError

    End Function

    Public Function prtRetirementEligibilty(ByVal Name As String, ByVal SSnumber As String, ByVal msg As String, ByVal headerName As String, ByVal prntOrFrm As String)

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        haveDisclaimer = 1
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "Retirement Eligibility" & vbNewLine & _
            msg

        If prntOrFrm = "Print" Then
            prtRetirementEligibilty = OtherInfo(tRep, Name, ssnum, dateOfCov, dateOfBirth) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtRetirementEligibilty = mainInfo
        End If
        mainInfo = Nothing
        Return prtRetirementEligibilty

    End Function

    ' ADDED 04/22/2014 - PRINTING FOR THE RECEIPT OF MATERNITY SLIDE NUMBERS 26,27,32 and 40
    Public Sub prtAllValidation(ByVal text As String, ByVal printer As String)
        TextToBePrinted = text
        Using (printDoc)
            printDoc.PrinterSettings.PrinterName = printer
            AddHandler printDoc.PrintPage, _
             AddressOf Me.PrintPageHandler2
            Dim ppsize As New PaperSize("Report Size", 400, 400)
            printDoc.DefaultPageSettings.Margins.Top = 100
            printDoc.DefaultPageSettings.PaperSize = ppsize
            printDoc.ToString()
            PrintDocument()

        End Using
    End Sub

    ' ADDED 05/19/2014 - PRINTING FOR THE RECEIPT OF ACOP
    Public Sub prtAcopRec(ByVal text As String, ByVal printer As String)
        TextToBePrinted = text
        Using (printDoc)
            printDoc.PrinterSettings.PrinterName = printer
            AddHandler printDoc.PrintPage, _
             AddressOf Me.PrintPageHandler2
            Dim ppsize As New PaperSize("Report Size", 400, 400)
            printDoc.DefaultPageSettings.Margins.Top = 100
            printDoc.DefaultPageSettings.PaperSize = ppsize
            printDoc.ToString()
            PrintDocument()

        End Using
    End Sub

    Public Sub prtPensionRec(ByVal text As String, ByVal printer As String)
        TextToBePrinted = text
        Using (printDoc)
            printDoc.PrinterSettings.PrinterName = printer
            AddHandler printDoc.PrintPage, _
             AddressOf Me.PrintPageHandler2
            Dim ppsize As New PaperSize("Report Size", 400, 400)
            printDoc.DefaultPageSettings.Margins.Top = 100
            printDoc.DefaultPageSettings.PaperSize = ppsize
            printDoc.ToString()
            PrintDocument()

        End Using
    End Sub

    Public Function prtReciept(ByVal note As String, ByVal transNum As String, ByVal getDate As String, ByVal getBranch As String, ByVal getKioskID As String, ByVal getSSNumber As String, ByVal getRefNo As String, ByVal getName As String)
        Dim notes, transNo, footers As String
        Dim dateToday As String


        notes = note
        transNo = "TRANSACTION REFERENCE NUMBER: " & transNum
        ' footers = vbNewLine & footer1
        prtReciept = vbNewLine & statInfo(getBranch, getKioskID, getSSNumber, getRefNo, getName) & vbNewLine & vbNewLine & notes & vbNewLine & vbNewLine & transNo '& footers

        Return prtReciept

    End Function

    'TRANSACTION SUBMISSION RECEIPT

    Public Function prtSalaryLoanSub(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN " & vbNewLine & _
                                 "APPLICATION. KINDLY CHECK YOUR EMAIL FOR THE COPY OF" & vbNewLine & _
                                 "YOUR LOAN DISCLOSURE STATEMENT. PLEASE TAKE NOTE OF" & vbNewLine & _
                                 "YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtSalaryLoanSub = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSalaryLoanSub = mainInfo
        End If
        mainInfo = Nothing
        Return prtSalaryLoanSub
    End Function

    Public Function prtSalaryLoanSub2(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        'Dim getDay As String = Date.Today.Day + 3
        'Dim getMonthBday As String = MonthName(Date.Today.Month)
        'getMonthBday = UCase(getMonthBday)

        Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
        Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
        Dim dbComm As OracleCommand
        dbConn.Open()
        dbComm = dbConn.CreateCommand
        dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
        dbComm.CommandText = "PR_LOANVALIDITY"
        dbComm.CommandType = CommandType.StoredProcedure
        dbComm.ExecuteNonQuery()
        dbConn.Close()
        Dim getdateSL As Date
        getdateSL = dbComm.Parameters("MSG").Value.ToString
        ' Dim getdateSL As Date = _frmUserAuthentication.getSalDay
        Dim getMonthBday As String = MonthName(getdateSL.Month)
        Dim getDay As String = getdateSL.Day
        Dim getYear As String = getdateSL.Year
        Dim finalSLdate As String = getMonthBday & " " & getDay & ", " & getYear

        'mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN" & vbNewLine & _
        '                   "APPLICATION. KINDLY CHECK YOUR EMAIL FOR THE COPY OF " & vbNewLine & _
        '                   "YOUR LOAN DISCLOSURE STATEMENT. YOUR APPLICATION MUST" & vbNewLine & _
        '                   "BE CERTIFIED BY YOUR EMPLOYER ON OR BEFORE " & UCase(finalSLdate) & "." & vbNewLine & _
        '                   "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE" & vbNewLine & _
        '                   "NUMBER BELOW:" & vbNewLine & vbNewLine & _
        '                         "TRANSACTION REFERENCE NUMBER: " & transNum




        mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN." & vbNewLine & _
                   "YOUR APPLICATION SHOULD BE CERTIFIED BY YOUR EMPLOYER" & vbNewLine & _
                   "ON OR BEFORE " & UCase(finalSLdate) & " THROUGH ITS SSS" & vbNewLine & _
                   "WEBSITE ACCOUNT. OTHERWISE, IT WILL EXPIRE, THUS, " & vbNewLine & _
                   "YOU NEED TO RE-SUBMIT YOUR APPLICATION." & vbNewLine & vbNewLine & _
                   "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE" & vbNewLine & _
                   "NUMBER BELOW:" & vbNewLine & vbNewLine & _
                  "TRANSACTION REFERENCE NUMBER: " & transNum


        If prntOrFrm = "Print" Then
            prtSalaryLoanSub2 = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSalaryLoanSub2 = mainInfo
        End If
        mainInfo = Nothing
        Return prtSalaryLoanSub2
    End Function

    Public Function prtMatnotif(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        'mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR MATERNITY " & vbNewLine &
        '                         "NOTIFICATION APPLICATION. PLEASE TAKE NOTE OF YOUR " & vbNewLine &
        '                         "TRANSACTION REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine &
        '                         "TRANSACTION REFERENCE NUMBER: " & transNum

        mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR " & vbNewLine &
                   "MATERNITY NOTIFICATION APPLICATION." & vbNewLine &
                   "PLEASE TAKE NOTE OF YOUR TRANSACTION" & vbNewLine &
                   "REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & vbNewLine & transNum

        If prntOrFrm = "Print" Then
            prtMatnotif = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMatnotif = mainInfo
        End If
        mainInfo = Nothing
        Return prtMatnotif
    End Function
    Public Function prtMatnotif2(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        'nikki003
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        '          "YOU HAVE SUCCESSFULLY SUBMITTED YOUR APPLICATION FOR" & vbNewLine & _
        mainInfo = "WE HAVE ACCEPTED YOUR MATERNITY NOTIFICATION, " & vbNewLine & _
                   "HOWEVER, A DISCREPANCY WAS NOTED, " & vbNewLine & _
                   "HENCE, PLEASE UPDATE/CORRECT YOUR RECORDS IN " & vbNewLine & _
                   "OUR FILE PRIOR TO FILING OF MATERNITY REIMBURSEMENT." & vbNewLine & vbNewLine & _
                   "-MEMBER LACKS THE REQUIRED QUALIFYING CONTRIBUTIONS." & vbNewLine & vbNewLine & _
                   "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE" & vbNewLine & _
                    "NUMBER BELOW: " & vbNewLine & _
                    "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtMatnotif2 = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtMatnotif2 = mainInfo
        End If
        mainInfo = Nothing
        Return prtMatnotif2
    End Function
    Public Function prtTechRet(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR APPLICATION FOR" & vbNewLine & _
                    "TECHNICAL RETIREMENT. PLEASE TAKE NOTE OF YOUR " & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtTechRet = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtTechRet = mainInfo
        End If
        mainInfo = Nothing
        Return prtTechRet
    End Function

    Public Function prtAcop(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
        Dim getYearBday As String = Date.Today.Year + 1
        Dim getDayBday As String = bdate.Day
        Dim getMonthBday As String = MonthName(bdate.Month)
        getMonthBday = UCase(getMonthBday)
        Dim finalDate As String = getMonthBday & " " & getDayBday & ", " & getYearBday
        Dim finalDateAcop1 As String = getMonthBday & " " & getYearBday
        Dim mon1 As String = bdate.Month
        Dim numDate As Date = bdate.Month & "/" & bdate.Day & "/" & getYearBday
        Dim bMonth As Date = DateAdd(DateInterval.Month, -6, numDate)
        Dim bmnth1 As String = UCase(MonthName(bMonth.Month))
        Dim bMonth1 As Date = DateAdd(DateInterval.Month, -1, numDate)
        Dim bmnth2 As String = UCase(MonthName(bMonth1.Month))


        mainInfo = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR ANNUAL " & vbNewLine &
                    "CONFIRMATION OF PENSIONER (ACOP) COMPLIANCE." & vbNewLine &
                    "YOUR NEXT SCHEDULE WILL BE IN " & finalDateAcop1 & "." & vbNewLine &
                    "YOU MAY ALSO REPORT FROM " & bmnth1 & " TO " & vbNewLine &
                    bmnth2 & " " & getYearBday & " FOR EARLY COMPLIANCE." & vbNewLine &
                    "PLEASE TAKE NOTE OF YOUR TRANSACTION " & vbNewLine &
                    "REFERENCE NUMBER BELOW:" & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & vbNewLine & transNum

        If prntOrFrm = "Print" Then
            prtAcop = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtAcop = mainInfo
        End If
        mainInfo = Nothing
        Return prtAcop
    End Function

    Public Function prtPension(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0
        Dim certainDate As String
        Dim getYear As String = Date.Today.Year + 1
        Dim getDay As String = Date.Today.Day
        Dim getMonth As String = Date.Today.Month
        certainDate = getMonth & "/" & getDay & "/" & getYear
        mainInfo = "YOUR MAILING ADDRESS/CONTACT INFORMATION WAS " & vbNewLine & _
                                 "SUCCESSFULLY UPDATED. PLEASE TAKE NOTE OF YOUR " & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER BELOW. " & vbNewLine & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtPension = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtPension = mainInfo
        End If
        mainInfo = Nothing
        Return prtPension
    End Function


    Public Function prtFailedAuth(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        'mainInfo = "THE SYSTEM HAS FAILED TO AUTHENTICATE YOUR" & vbNewLine & _
        '                         "FINGERPRINT. PLEASE TRY AGAIN." & vbNewLine & vbNewLine & _
        '                         "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE" & vbNewLine & _
        '                         "FROM OUR MEMBER SERVICE REPRESENTATIVE AT" & vbNewLine & _
        '                         "OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
        '                         "TRANSACTION REFERENCE NUMBER: " & transNum

        If Not isGSISCard Then
            mainInfo = "THE SYSTEM HAS FAILED TO AUTHENTICATE YOUR" & vbNewLine & "CARD. YOU CANNOT PROCEED." & vbNewLine & "You may access your account on the".ToUpper & vbNewLine & "FOLLOWING DAY.".ToUpper & vbNewLine & vbNewLine &
                                "TRANSACTION REFERENCE NUMBER: " & vbNewLine & transNum
        Else
            mainInfo = "FAILED AUTHENTICATION, PLEASE SEEK ASSISTANCE" & vbNewLine & "FROM OUR FRONTLINE SERVICE COUNTER OF THE" & vbNewLine & "NEAREST SSS BRANCH." & vbNewLine & vbNewLine &
                                "TRANSACTION REFERENCE NUMBER: " & vbNewLine & transNum
        End If


        If prntOrFrm = "Print" Then
                prtFailedAuth = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
            ElseIf prntOrFrm = "Form" Then
                prtFailedAuth = mainInfo
            End If
            mainInfo = Nothing
            Return prtFailedAuth
    End Function


    Public Function prtRegistrationSub(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        mainInfo = "YOU HAVE SUCCESSFULLY CREATED YOUR WEB ACCOUNT." & vbNewLine &
                                 "PLEASE CHECK YOUR EMAIL FOR THE VERIFICATION LINK" & vbNewLine &
                                 "TO ACTIVATE YOUR ACCOUNT. IF YOU ARE UNABLE TO" & vbNewLine &
                                 "RECEIVE YOUR EMAIL FROM YOUR INBOX, PLEASE CHECK" & vbNewLine &
                                 " YOUR SPAM OR JUNK FOLDER." & vbNewLine & vbNewLine &
                                 "TRANSACTION REFERENCE NUMBER: " & vbNewLine & transNum
        If prntOrFrm = "Print" Then
            prtRegistrationSub = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtRegistrationSub = mainInfo
        End If
        mainInfo = Nothing
        Return prtRegistrationSub
    End Function

    Public Function prtValidation(ByVal note As String, ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = note & vbNewLine & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtValidation = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtValidation = mainInfo
        End If
        mainInfo = Nothing
        Return prtValidation
    End Function

    Public Function prtTechMsg(ByVal note As String, ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = note & vbNewLine & vbNewLine & _
                                 "TRANSACTION REFERENCE NUMBER: " & transNum

        If prntOrFrm = "Print" Then
            prtTechMsg = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtTechMsg = mainInfo
        End If
        mainInfo = Nothing
        Return prtTechMsg
    End Function
    Public Function prtAcopSuspension(ByVal Name As String, ByVal SSnumber As String, ByVal transNum As String, ByVal headerName As String, ByVal prntOrFrm As String)
        ifLoan = False
        haveDisclaimer = 0
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber


        mainInfo = "WE REGRET THAT YOU CANNOT PROCEED WITH YOUR" & vbNewLine & _
                   "ANNUAL CONFIRMATION OF PENSIONER DUE TO YOUR" & vbNewLine & _
                   "POSTED CONTRIBUTIONS AFTER YOUR RETIREMENT" & vbNewLine & _
                   "DATE. FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE" & vbNewLine & _
                   "FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR" & vbNewLine & _
                   "SERVICE COUNTER OR AT THE SSS BRANCH, OTHERWISE " & vbNewLine & _
                   "YOUR PENSION WILL BE SUSPENDED." & vbNewLine & vbNewLine & _
                    "TRANSACTION REFERENCE NUMBER: " & transNum
        If prntOrFrm = "Print" Then
            prtAcopSuspension = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtAcopSuspension = mainInfo
        End If
        mainInfo = Nothing
        Return prtAcopSuspension
    End Function

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


    Private Sub PrintPageHandler2(ByVal sender As Object, _
   ByVal args As Printing.PrintPageEventArgs)
        Dim myFont As New Font("Segoe UI", 12)
        Dim smallFont As New Font("Segoe UI", 10)
        Dim noteFont As New Font("Segoe UI", 8)
        args.Graphics.DrawString(TextToBePrinted, _
           New Font(smallFont, FontStyle.Regular), _
           Brushes.Black, 10, 80)
        args.Graphics.DrawString(sssHeader, New Font(myFont, FontStyle.Bold), _
                                    Brushes.Black, 100, 20)
        args.Graphics.DrawString(footnote, New Font(noteFont, FontStyle.Bold), _
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

    Public Function prtAuthFailedPrint_UMID(ByVal NAME As String, ByVal CRN As String)
        Dim sb As New System.Text.StringBuilder

        rtTitle = UCase("UMID CARD AUTHENTICATION")
        tRep = rtTitle

        sb.AppendLine("Name : " & NAME)
        sb.AppendLine("CRN : " & CRN & vbNewLine)
        'sb.AppendLine("Authentication failed. Your fingerprints did not match." & vbNewLine & vbNewLine & "Please seek assistance from the Member Service Representative (MSR)" & vbNewLine & " at our frontline service counter immediately." & vbNewLine & vbNewLine & "Thank you very much.")
        sb.AppendLine("YOUR UMID CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE (MSR) AT OUR FRONTLINE SERVICE COUNTER OR" & vbNewLine & "GO TO THE NEAREST SSS BRANCH.")

        Return sb.ToString
    End Function

    Public Function prtAuthFailedPrint_SSS(ByVal SSSNum As String)
        Dim sb As New System.Text.StringBuilder

        rtTitle = UCase("SSS CARD AUTHENTICATION")
        tRep = rtTitle

        sb.AppendLine("SS Number : " & SSSNum.Substring(0, 2) & "-" & SSSNum.Substring(2, 7) & "-" & SSSNum.Substring(9, 1) & vbNewLine)
        'sb.AppendLine("Authentication failed. Your fingerprints did not match." & vbNewLine & vbNewLine & "Please seek assistance from the Member Service Representative (MSR)" & vbNewLine & " at our frontline service counter immediately." & vbNewLine & vbNewLine & "Thank you very much.")
        sb.AppendLine("YOUR SSS CARD HAS BEEN BLOCKED FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & "REPRESENTATIVE (MSR) AT OUR FRONTLINE SERVICE COUNTER OR" & vbNewLine & "GO TO THE NEAREST SSS BRANCH.")

        Return sb.ToString
    End Function

    Public Function prtPRN_Receipt(ByVal Name As String, ByVal SSnumber As String,
                                   ByVal PRN As String, ByVal MembershipType As String, ByVal MonthlyContribution As String,
                                   ByVal FlexiFund As String, ByVal TotalAmount As String, ByVal Period As String,
                                   ByVal DueDate As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        Dim sb As New System.Text.StringBuilder
        sb.Append("Payment Reference Number: " & PRN & vbNewLine)
        sb.Append("Membership Type: " & MembershipType & vbNewLine)
        If CDec(FlexiFund) = 0 Then
            'sb.Append("Monthly Contribution: " & MonthlyContribution & vbNewLine)
        Else
            'sb.Append("Monthly Contribution: " & MonthlyContribution & "   ")
            sb.Append("Flexi-Fund Amount: " & FlexiFund & vbNewLine)
        End If
        sb.Append("Total Amount: " & TotalAmount & vbNewLine)
        sb.Append("Period: " & Period & vbNewLine)
        sb.Append("Due Date: " & DueDate & vbNewLine)
        sb.Append(vbNewLine)
        sb.Append("Please pay on or before the due date at any SSS" & vbNewLine)
        sb.Append("accredited collecting partner/ bank or SSS Office" & vbNewLine)
        sb.Append("with Tellering Facility")

        mainInfo = sb.ToString

        If prntOrFrm = "Print" Then
            prtPRN_Receipt = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtPRN_Receipt = mainInfo
        End If
        mainInfo = Nothing

        Return prtPRN_Receipt
    End Function

    Public Function prtOnlineRetirement_Receipt(ByVal Name As String, ByVal SSnumber As String,
                                   ByVal avail18Months As String, ByVal bank As String, ByVal acctNumber As String,
                                   ByVal retirementDate As String, ByVal setTransNo As String, ByVal ssTxnNo As String, ByVal headerName As String, ByVal prntOrFrm As String)
        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        Dim sb As New System.Text.StringBuilder
        sb.Append("Availment of the 18-mos advance: " & avail18Months & vbNewLine)
        sb.Append("Name of Disbursement Bank: " & bank & vbNewLine)
        sb.Append("Savings Account Number: " & acctNumber & vbNewLine)
        sb.Append("Date of Retirement: " & retirementDate)
        sb.Append(vbNewLine & vbNewLine)
        sb.Append("YOU HAVE SUCCESSFULLY SUBMITTED YOUR" & vbNewLine)
        sb.Append("APPLICATION FOR RETIREMENT")
        sb.Append(vbNewLine & vbNewLine)
        sb.Append("VERIFICATION TRANSACTION REFERENCE NUMBER:" & vbNewLine)
        sb.Append(ssTxnNo & vbNewLine)
        sb.Append("SET TRANSACTION REFERENCE NUMBER:" & vbNewLine)
        sb.Append(setTransNo)

        mainInfo = sb.ToString

        If prntOrFrm = "Print" Then
            prtOnlineRetirement_Receipt = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtOnlineRetirement_Receipt = mainInfo
        End If
        mainInfo = Nothing

        Return prtOnlineRetirement_Receipt
    End Function

    Public Function prtUpdateContactInformation_Receipt(ByVal Name As String, ByVal SSnumber As String,
                                   ByVal TxnNo1 As String,
                                   ByVal headerName As String, ByVal prntOrFrm As String)
        'ByVal TxnNo2 As String, ByVal TxnNo3 As String,

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        Dim sb As New System.Text.StringBuilder

        If TxnNo1 <> "" Then
            sb.Append("ONLINE DATA CHANGE REQUEST" & vbNewLine & vbNewLine)
            sb.Append("Transaction Number: " & TxnNo1 & vbNewLine)
        End If

        'If TxnNo2 <> "" Then
        '    sb.Append("LOCAL ADDRESS UPDATE" & vbNewLine)
        '    sb.Append("Transaction Number: " & TxnNo2 & vbNewLine)
        'End If

        'If TxnNo3 <> "" Then
        '    sb.Append("TELEPHONE NO., MOBILE NO. AND EMAIL UPDATE" & vbNewLine)
        '    sb.Append("Transaction Number: " & TxnNo3 & vbNewLine)
        'End If

        'sb.Append(vbNewLine & vbNewLine)
        'sb.Append("Thank you for updating your contact information using Info Kiosk" & vbNewLine)
        '
        mainInfo = sb.ToString

        If prntOrFrm = "Print" Then
            prtUpdateContactInformation_Receipt = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtUpdateContactInformation_Receipt = mainInfo
        End If
        mainInfo = Nothing

        Return prtUpdateContactInformation_Receipt
    End Function

    Public Function prtSimplifiedWebRegistration_Receipt(ByVal Name As String, ByVal SSnumber As String,
                                   ByVal receiptMsg As String,
                                   ByVal headerName As String, ByVal prntOrFrm As String)
        'ByVal TxnNo2 As String, ByVal TxnNo3 As String,

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = receiptMsg

        If prntOrFrm = "Print" Then
            prtSimplifiedWebRegistration_Receipt = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtSimplifiedWebRegistration_Receipt = mainInfo
        End If
        mainInfo = Nothing

        Return prtSimplifiedWebRegistration_Receipt
    End Function

    Public Function prtPrintReceipt(ByVal Name As String, ByVal SSnumber As String,
                                   ByVal receiptMsg As String,
                                   ByVal headerName As String, ByVal prntOrFrm As String)
        'ByVal TxnNo2 As String, ByVal TxnNo3 As String,

        DateOfCov = readSettings(xml_Filename, xml_path, "dateOfCov")
        dateOfBirth = readSettings(xml_Filename, xml_path, "dateOfBirth")

        ifLoan = False
        rtTitle = UCase(headerName)
        tRep = rtTitle
        Name = Name
        ssnum = SSnumber
        haveDisclaimer = 0

        mainInfo = receiptMsg

        If prntOrFrm = "Print" Then
            prtPrintReceipt = OtherInfoTrans(tRep, Name, ssnum, kioskBranch, kioskID) & mainInfo
        ElseIf prntOrFrm = "Form" Then
            prtPrintReceipt = mainInfo
        End If
        mainInfo = Nothing

        Return prtPrintReceipt
    End Function


    Private Sub PrintDocument()
        If File.Exists(Application.StartupPath & "\noprint.txt") Then
            PrintPreview()
        Else
            printDoc.Print()
        End If
        'PrintPreview()
    End Sub

    Private Sub PrintPreview()
        Dim ppDlg As New PrintPreviewDialog
        ppDlg.Document = printDoc
        ppDlg.Show()
    End Sub

End Class
