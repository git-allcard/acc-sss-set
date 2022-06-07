Imports VB = Microsoft.VisualBasic
Imports System.Text.RegularExpressions

Public Class printModule
    ReadOnly specialC As String = "</>\:;[]"
    Dim db As New ConnectionString
    Dim dt As New DataTable
    Dim matchpattern As String = "<(?:[^>=]|='[^']*'|=""[^""]*""|=[^'""][^\s>]*)*>"
    Dim replacementstring As String = "|"
    Dim rchtextbox As New RichTextBox

#Region "SSS Inquiry System"

    Public Function defaultPage(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("To inquire on")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(5), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                getEmpID = getEmpID.Trim
                defaultPage = getEmpID
                Exit Function
            End If
        Next

        Return defaultPage
    End Function
#End Region

#Region "Declaration / Function"
    Public sHtlm
#End Region

#Region "Navigating Click"


    '-------------- Title Page of Form --------------------
    Public Function GetHeaderForm(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim headerName() As String
        Dim sHtlm
        Dim getHead As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("miniTitle")) > 0 Then
                headerName = Split(Tmp(i), ">")
                nme = headerName.Length - 3
                getHead = headerName(nme)
                headerName = Split(getHead, "</p")
                nme = headerName.Length - 2
                GetHeaderForm = headerName(nme)
                GetHeaderForm = GetHeaderForm.Trim
                Exit Function
            End If
        Next
        Return GetHeaderForm
    End Function

    Public Function getBenefitChoicesTotalDisable(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Average Monthly Salary Credit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(114), "tableDataLite")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - empID.Length
                getEmpID = empID(nme)

                getBenefitChoicesTotalDisable = empID(nme)
                getBenefitChoicesTotalDisable = getBenefitChoicesTotalDisable.Trim
                Exit Function
            End If
        Next
        Return getBenefitChoicesTotalDisable
    End Function

#End Region

#Region "FullName"

    Public Function getFullName(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim FNam() As String
        Dim MNam() As String
        Dim LNam() As String
        Dim fname, mname, lname As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim nme As Integer
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("Member Name")) > 0 Then


                FNam = Split(Tmp(i + 2), ">")
                nme = FNam.Length - 1
                fname = FNam(nme)

                MNam = Split(Tmp(i + 3), ">")
                nme = MNam.Length - 1
                mname = MNam(nme)

                LNam = Split(Tmp(i + 1), ">")
                nme = LNam.Length - 1
                lname = LNam(nme)



                getFullName = lname.Trim & " " & fname.Trim & " " & mname.Trim

                Exit Function

            End If

        Next

    End Function

    Public Function GetFirstName(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim nme As Integer
        Dim FNam() As String
        Dim fname As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
              If InStr(1, LCase(Tmp(i)), LCase("Member Name")) > 0 Then

                FNam = Split(Tmp(i + 2), ">")
                nme = FNam.Length - 1
                fname = FNam(nme)

                GetFirstName = fname.Trim
                Exit Function

            End If

        Next

    End Function

    Public Function GetMiddleName(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim nme As Integer
        Dim MNam() As String
        Dim mname As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member Name")) > 0 Then

                MNam = Split(Tmp(i + 3), ">")
                nme = MNam.Length - 1
                mname = MNam(nme)

                GetMiddleName = mname.Trim
                Exit Function

            End If

        Next
    End Function

    Public Function GetLastName(ByVal webBrowserPath) As String
       Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim nme As Integer
        Dim LNam() As String
        Dim lname As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member Name")) > 0 Then
                LNam = Split(Tmp(i + 1), ">")
                nme = LNam.Length - 1
                lname = LNam(nme)

                GetLastName = lname.Trim
                Exit Function

            End If

        Next
    End Function



#End Region
    ' ******* OLD GETSSNUMBERSTATUS 
    ' **NOTE: PLEASE TEST THIS BEFORE UPLOADING.
    'Public Function GetSSNumberStatus(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim ssStatus() As String
    '    Dim sHtlm
    '    Dim getssStatus As String
    '    Dim i As Integer
    '    Dim nme As Integer
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("ss_stat")) > 0 Then
    '            ssStatus = Split(Tmp(i + 1), ">")
    '            nme = ssStatus.Length - 2
    '            getssStatus = ssStatus(nme)
    '            ssStatus = Split(getssStatus, "&nbsp;</td")
    '            nme = ssStatus.Length - 2
    '            GetSSNumberStatus = ssStatus(nme)

    '            GetSSNumberStatus = GetSSNumberStatus.Trim
    '            'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
    '            'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetSSNumberStatus
    'End Function
    ' ******* OLD GetRecordLocation

    'Public Function GetRecordLocation(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim recLocation() As String
    '    Dim sHtlm
    '    Dim getrecLocation As String
    '    Dim i As Integer
    '    Dim nme As Integer
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("rec_loc")) > 0 Then
    '            recLocation = Split(Tmp(i + 1), ">")
    '            nme = recLocation.Length - 2
    '            getrecLocation = recLocation(nme)
    '            recLocation = Split(getrecLocation, "&nbsp;</td")
    '            nme = recLocation.Length - 2
    '            GetRecordLocation = recLocation(nme)

    '            GetRecordLocation = GetRecordLocation.Trim
    '            'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
    '            'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetRecordLocation
    'End Function

    '' ******* OLD GetEmployerIDSal

    'Public Function GetEmployerIDSal(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim ssStatus() As String
    '    Dim sHtlm
    '    Dim getssStatus As String
    '    Dim i As Integer
    '    Dim nme As Integer
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("Latest ER ID:")) > 0 Then
    '            ssStatus = Split(Tmp(i + 1), ">")
    '            nme = ssStatus.Length - 2
    '            getssStatus = ssStatus(nme)
    '            ssStatus = Split(getssStatus, "&nbsp;</td")
    '            nme = ssStatus.Length - 2
    '            GetEmployerIDSal = ssStatus(nme)

    '            GetEmployerIDSal = GetEmployerIDSal.Trim
    '            'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
    '            'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetEmployerIDSal
    'End Function
    ' *********************** OLD GET EMPLOYER NAME

    'Public Function GetEmployerNameSal(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim ssStatus() As String
    '    Dim sHtlm
    '    Dim getssStatus As String
    '    Dim i As Integer
    '    Dim nme As Integer
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("Latest ER Name:")) > 0 Then
    '            ssStatus = Split(Tmp(i + 1), ">")
    '            nme = ssStatus.Length - 2
    '            getssStatus = ssStatus(nme)
    '            ssStatus = Split(getssStatus, "&nbsp;</td")
    '            nme = ssStatus.Length - 2
    '            GetEmployerNameSal = ssStatus(nme)

    '            GetEmployerNameSal = GetEmployerNameSal.Trim
    '            'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
    '            'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetEmployerNameSal
    'End Function


    ' ****** END OF COMMENT
#Region "Employee Static Information /  Member Details"

    '-------------- Member Details --------------------
    Public Function GetSSNumberStatus(ByVal webBrowserPath) As String
        Return GetSSNumberStatusv2(webBrowserPath)

        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim ssStatus() As String
        Dim sHtlm
        Dim getssStatus As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        Tmp = Split(sHtlm, "&nbsp;")
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ss_stat_val")) > 0 Then
                ssStatus = Split(Tmp(i + 1), "<")
                nme = 0
                getssStatus = ssStatus(nme)
                ssStatus = Split(getssStatus, "&nbsp")
                nme = ssStatus.Length - 1
                GetSSNumberStatus = ssStatus(nme)

                GetSSNumberStatus = GetSSNumberStatus.Trim
                'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
                'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetSSNumberStatus
    End Function

    Public Function GetSSNumberStatusv2(ByVal webBrowserPath) As String
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("SS Number Status")

        If varPosition <> -1 Then
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(2).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Trim
        Else
            Return ""
        End If
    End Function


    '-------------- Coverage Status --------------------
    Public Function GetCoverageStatus(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim covStatus() As String
        Dim sHtlm
        Dim getcovStatus As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
      
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Coverage Status:")) > 0 Then
                covStatus = Split(Tmp(i + 1), ">")
                nme = covStatus.Length - 2
                getcovStatus = covStatus(nme)
                covStatus = Split(getcovStatus, "&nbsp;</td")
                nme = covStatus.Length - 2
                GetCoverageStatus = covStatus(nme)

                GetCoverageStatus = GetCoverageStatus.Trim
                'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
                'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCoverageStatus
    End Function

    Public Function GetCoverageStatus_v2(ByVal webBrowserPath) As String
        'Dim Tmp() As String
        'Dim empID() As String
        'Dim sHtlm
        'Dim getEmpID As String
        'Dim i As Integer
        'Dim nme As Integer

        'Dim haha As Integer = 1
        'sHtlm = webBrowserPath.Documenttext

        'Dim varPosition As Integer = sHtlm.IndexOf("Coverage Status")

        'If varPosition <> -1 Then
        '    Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")
        '    'Dim arr2() As String = arr1(1).Split(">")

        '    '&nbsp;&nbspCOVERED EMPLOYEE</td
        '    Return arr1(2).Replace("</td", "").Replace("&nbsp", "").Replace(";", "")
        'Else
        '    Return CoveredStatus
        'End If

        Return GetCoverageStatusv3(webBrowserPath)
    End Function

    Public Function GetCoverageStatusv3(ByVal webBrowserPath) As String
        'Public Function GetCoverageStatusv3(ByVal webBrowserPath) As String
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("Membership Type")

        If varPosition <> -1 Then
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(2).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Trim
        Else
            Return CoveredStatus
        End If
    End Function

    '-------------- Record Location --------------------
    Public Function GetRecordLocation(ByVal webBrowserPath) As String
        Return GetRecordLocationv2(webBrowserPath)

        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim recLocation() As String
        Dim sHtlm
        Dim getrecLocation As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        Tmp = Split(sHtlm, "&nbsp;")

        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rec_loc_val")) > 0 Then
                recLocation = Split(Tmp(i + 1), "<")
                nme = 0
                getrecLocation = recLocation(nme)
                recLocation = Split(getrecLocation, "&nbsp")
                nme = recLocation.Length - 1
                GetRecordLocation = recLocation(nme)

                GetRecordLocation = GetRecordLocation.Trim
                'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
                'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetRecordLocation
    End Function

    Public Function GetRecordLocationv2(ByVal webBrowserPath) As String
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("Record Location")

        If varPosition <> -1 Then
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(2).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Trim
        Else
            Return ""
        End If
    End Function
    '  new GETEMPID

    Public Function GetEmployerIDSal(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim ssStatus() As String
        Dim sHtlm
        Dim getssStatus As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        Tmp = Split(sHtlm, "&nbsp;")
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Latest ER ID")) > 0 Then
                ssStatus = Split(Tmp(i + 1), "&nbsp")
                nme = ssStatus.Length - 1
                getssStatus = ssStatus(nme)
                Dim testStat As String = (Regex.Replace(getssStatus, matchpattern, "", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline)) 'getssStatus.Replace(matchpattern, "")
                'ssStatus = Split(getssStatus, "&nbsp")
                'nme = ssStatus.Length - 2
                GetEmployerIDSal = testStat

                GetEmployerIDSal = GetEmployerIDSal.Trim
                ' GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
                'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetEmployerIDSal
    End Function
    'Public Function GetEmployerIDSal(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim ssStatus() As String
    '    Dim sHtlm
    '    Dim getssStatus As String
    '    Dim i As Integer
    '    Dim nme As Integer
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("Latest ER ID:")) > 0 Then
    '            ssStatus = Split(Tmp(i + 1), ">")
    '            nme = ssStatus.Length - 2
    '            getssStatus = ssStatus(nme)
    '            ssStatus = Split(getssStatus, "&nbsp;</td")
    '            nme = ssStatus.Length - 2
    '            GetEmployerIDSal = ssStatus(nme)

    '            GetEmployerIDSal = GetEmployerIDSal.Trim
    '            'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
    '            'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetEmployerIDSal
    'End Function

    Public Function GetEmployerNameSal(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim ssStatus() As String
        Dim sHtlm
        Dim getssStatus As String
        Dim i As Integer
        Dim nme As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, "&nbsp;")
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Latest ER Name")) > 0 Then
                ssStatus = Split(Tmp(i + 1), "&nbsp")
                nme = ssStatus.Length - 1
                getssStatus = ssStatus(nme)
                'ssStatus = Split(getssStatus, "&nbsp;</td")
                'nme = ssStatus.Length - 2
                'GetEmployerNameSal = ssStatus(nme)
                Dim testStat As String = (Regex.Replace(getssStatus, matchpattern, "", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline)) 'getssStatus.Replace(matchpattern, "")
                'ssStatus = Split(getssStatus, "&nbsp")
                'nme = ssStatus.Length - 2
                GetEmployerNameSal = testStat

                GetEmployerNameSal = GetEmployerNameSal.Trim
                'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
                'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetEmployerNameSal
    End Function

#End Region

#Region "Actual Premium"

    '-------------- SSS Number --------------------
    Public Function GetSSSnumber(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim ssNo() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("SS Number")) > 0 Then
                ssNo = Split(Tmp(i + 1), ">")
                GetSSSnumber = Left(ssNo(1), Len(ssNo(1)) - 4)
                GetSSSnumber = GetSSSnumber.Trim
                Exit Function
            End If
        Next

        Return GetSSSnumber
    End Function

    '-------------- Date of Birth --------------------
    Public Function GetDateBith(ByVal webBrowserPath) As String
        ''added on Aug16,2021 due to error in production
        'Dim dateOfBirth As String = ""

        ''On Error GoTo errHdlr
        'Dim Tmp() As String
        'Dim ssStatus() As String
        'Dim sHtlm
        'Dim getssStatus As String
        'Dim i As Integer
        'Dim nme As Integer
        'sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        'For i = LBound(Tmp) To UBound(Tmp)
        '    If InStr(1, LCase(Tmp(i)), LCase("dob")) > 0 Then
        '        ssStatus = Split(Tmp(i + 1), ">")
        '        nme = ssStatus.Length - 2
        '        getssStatus = ssStatus(nme)
        '        ssStatus = Split(getssStatus, "</td")
        '        nme = ssStatus.Length - 2
        '        dateOfBirth = ssStatus(nme)
        '        dateOfBirth = dateOfBirth.Trim

        '        'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
        '        'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
        '        Exit Function

        '    End If
        'Next
        ''Exit Function
        ''errHdlr:
        ''logs

        ''added on Aug16, 2021 due to error in production
        'If dateOfBirth = "" Then
        '    Dim dobLine As String = Tmp(62).Replace(vbLf, "").Trim
        '    Dim startIndex As Integer = dobLine.IndexOf(">")
        '    Dim endIndex As Integer = dobLine.IndexOf("<", startIndex)
        '    dateOfBirth = dobLine.Substring(startIndex + 1, (endIndex - startIndex) - 1).Trim
        '    GetDateBith = dateOfBirth
        'End If

        'Return GetDateBith

        Return GetDateBirth2(webBrowserPath)
    End Function

    Public Function GetDateBirth2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim arr() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date of Birth")) > 0 Then
                arr = Split(Tmp(i + 1), ">")
                GetDateBirth2 = Left(arr(1), Len(arr(1)) - 4)
                GetDateBirth2 = GetDateBirth2.Trim
                Exit Function
            End If
        Next

        Return GetDateBirth2
    End Function

    '-------------- Date Coverage --------------------
    Public Function GetDateCoverage(ByVal webBrowserPath) As String
        ''added on Aug16,2021 due to error in production
        'Dim dateOfCoverage As String = ""

        ''On Error GoTo errHdlr
        'Dim Tmp() As String
        'Dim ssStatus() As String
        'Dim sHtlm
        'Dim getssStatus As String
        'Dim i As Integer
        'Dim nme As Integer
        'sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        'For i = LBound(Tmp) To UBound(Tmp)
        '    If InStr(1, LCase(Tmp(i)), LCase("docov")) > 0 Then
        '        ssStatus = Split(Tmp(i + 1))
        '        nme = ssStatus.Length - 1
        '        getssStatus = ssStatus(nme)
        '        ssStatus = Split(getssStatus, "</td")
        '        nme = ssStatus.Length - 2
        '        GetDateCoverage = ssStatus(nme)
        '        GetDateCoverage = GetDateCoverage.Trim
        '        'GetHeaderForm = Left(headerName(1), Len(headerName(1)) + 9)
        '        'GetHeaderForm = StrConv(GetHeaderForm, vbProperCase)
        '        Exit Function
        '    End If
        'Next
        ''Exit Function
        ''errHdlr:
        ''logs

        ''added on Aug16,2021 due to error in production
        'If dateOfCoverage = "" Then
        '    Dim dobLine As String = Tmp(71).Replace(vbLf, "").Trim
        '    Dim startIndex As Integer = dobLine.IndexOf(">")
        '    Dim endIndex As Integer = dobLine.IndexOf("<", startIndex)
        '    dateOfCoverage = dobLine.Substring(startIndex + 1, (endIndex - startIndex) - 1).Trim
        '    GetDateCoverage = dateOfCoverage
        'End If

        'Return GetDateCoverage

        Return GetDateCoveragev2(webBrowserPath)
    End Function

    '-------------- Date Coverage --------------------
    Public Function GetDateCoveragev2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim arr() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date of Coverage")) > 0 Then
                arr = Split(Tmp(i + 1), ">")
                GetDateCoveragev2 = Left(arr(1), Len(arr(1)) - 4)
                GetDateCoveragev2 = GetDateCoveragev2.Trim
                Exit Function
            End If
        Next

        Return GetDateCoveragev2
    End Function

    '-------------- Total Number of Contributions --------------------
    Public Function GetNumbOfContribution(ByVal webBrowserPath) As Integer

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim C As Integer
        Dim txtVal As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("tot_contri")) > 0 Then
                For C = 1 To Len(Tmp(i + 1))
                    If Not InStr("0123456789.,", Mid(Tmp(i + 1), C, 1)) = 0 Then
                        txtVal = txtVal & Mid(Tmp(i + 1), C, 1)
                        txtVal = txtVal.Trim
                    End If
                Next C
                GetNumbOfContribution = Right(txtVal, Len(txtVal) - 2)
                'Dim getNum As String
                'getNum = GetNumbOfContribution
                'getNum = getNum.Trim
                Exit Function
            End If
        Next

        Return GetNumbOfContribution
    End Function

    '-------------- Total Amount of Contributions --------------------
    Public Function GetTotalAmountContribution(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim C As Integer
        Dim txtVal As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("tot_amt_contri")) > 0 Then
                For C = 1 To Len(Tmp(i + 1))
                    If Not InStr("0123456789.,", Mid(Tmp(i + 1), C, 1)) = 0 Then
                        txtVal = txtVal & Mid(Tmp(i + 1), C, 1)
                        txtVal = txtVal.Trim
                    End If
                Next C
                GetTotalAmountContribution = Right(txtVal, Len(txtVal) - 2)
                GetTotalAmountContribution = GetTotalAmountContribution.Trim
                Exit Function
            End If
        Next

        Return GetTotalAmountContribution
    End Function

#End Region

#Region "Benefit Claims"

    Public Function GetBenClaimsChecking1(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        GetBenClaimsChecking1 = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("List of Availed Benefit Claim(s)")) > 0 Then
                GetBenClaimsChecking1 = True
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimsChecking2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        GetBenClaimsChecking2 = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Benefit Claim Application Details")) > 0 Then
                GetBenClaimsChecking2 = True
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaims(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Claim Type")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(20), "Claim Type")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 2
                GetBenClaims = empID(nme)
                GetBenClaims = GetBenClaims.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenClaims
    End Function


    Public Function GetBenRefNo(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Reference Number")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(24), "Reference Number")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetBenRefNo = empID(nme)
                GetBenRefNo = GetBenRefNo.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenRefNo
    End Function

    Public Function GetBenCheckDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Check Date")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(26), "Check Date")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetBenCheckDate = empID(nme)
                GetBenCheckDate = GetBenCheckDate.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenCheckDate
    End Function

    Public Function GetBenDateCon(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date of Contingency")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(23), "Date of Contingency")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetBenDateCon = empID(nme)
                GetBenDateCon = GetBenDateCon.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenDateCon
    End Function

    Public Function GetBenStatus(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("List of Availed Benefit Claim(s)")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(25), "Status")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetBenStatus = empID(nme)
                empID = Split(getEmpID, "&")
                nme = empID.Length - 2
                GetBenStatus = empID(nme)
                GetBenStatus = GetBenStatus.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenStatus
    End Function

    Public Function GetEligibStatus(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("alertType")) > 0 Then
                empID = Split(Tmp(i), ">")

                'empID = Split(empID(3), "alertType")
                ''nme = empID.Length 
                'getEmpID = empID(nme)
                'empID = Split(getEmpID, "</td")
                'nme = empID.Length - 2
                'GetEligibStatus = empID(nme)
                'GetEligibStatus = GetEligibStatus.Trim

                'added by edel Nov2020
                GetEligibStatus = empID(9).Replace("</td", "").Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetEligibStatus
    End Function
#End Region

#Region "Benefit Claims Application"

    Public Function GetBenClaimsClaimFiled(ByVal webBrowserPath) As String
        'argie103
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Claim Filed:")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimsClaimFiled = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimsClaimFiled = GetBenClaimsClaimFiled.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimsDateFiled(ByVal webBrowserPath) As String
        'argie103
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date Filed:")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimsDateFiled = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimsDateFiled = GetBenClaimsDateFiled.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimsFiledAt(ByVal webBrowserPath) As String
        'argie103
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date Filed:")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimsFiledAt = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimsFiledAt = GetBenClaimsFiledAt.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimsStatusAsOf(ByVal webBrowserPath) As String
        'argie103
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Status as of:")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimsStatusAsOf = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimsStatusAsOf = GetBenClaimsStatusAsOf.Trim
                Exit Function
            End If
        Next
    End Function

#End Region

#Region "Benefit Claim Information"

    Public Function GetBenClaimsDOC(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date of Contingency :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(24), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetBenClaimsDOC = empID(nme)
                GetBenClaimsDOC = GetBenClaimsDOC.Trim

                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenClaimsDOC
    End Function

    Public Function GetBenClaimsStat(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Claim Status :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(30), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 2
                GetBenClaimsStat = empID(nme)
                GetBenClaimsStat = GetBenClaimsStat.Trim

                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetBenClaimsStat
    End Function

    Public Function GetBenSetDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Settlement Date")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenSetDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenSetDate = GetBenSetDate.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimWithDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Withdrawal Date")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimWithDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimWithDate = GetBenClaimWithDate.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimPenDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Pension Start")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimPenDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimPenDate = GetBenClaimPenDate.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimAmtIB(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Initial Benefit")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimAmtIB = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimAmtIB = GetBenClaimAmtIB.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetBenClaimTotMPen(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Basic Monthly Pension")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetBenClaimTotMPen = Left(GLAD(1), Len(GLAD(1)) - 4)
                GetBenClaimTotMPen = GetBenClaimTotMPen.Trim
                Exit Function
            End If
        Next
    End Function

#End Region

#Region "Employment History"
    '-------------- Employer ID --------------------
    Public Function GetEmployerID(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("er_id")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(18), "er_id_val1")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetEmployerID = empID(nme)
                GetEmployerID = GetEmployerID.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetEmployerID
    End Function

    '-------------- Employer Name --------------------
    Public Function GetEmployerName(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empName() As String
        Dim sHtlm
        Dim getEmpName As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("er_id")) > 0 Then
                empName = Split(Tmp(i), "</td>")

                empName = Split(empName(1), "er_name_val1")
                nme = empName.Length - 1
                getEmpName = empName(nme)
                empName = Split(getEmpName, "> ")
                nme = empName.Length - 1
                empName = Split(empName(nme), "&nbsp")
                nme = empName.Length - 2
                GetEmployerName = empName(nme)
                GetEmployerName = GetEmployerName.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetEmployerName
    End Function

    Public Function GetEmployerNamev2(ByVal webBrowserPath) As String
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("Membership Type")

        If varPosition <> -1 Then
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(2).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Trim
        Else
            Return CoveredStatus
        End If
    End Function

    Public Function GetReportingDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim RepDate() As String
        Dim sHtlm
        Dim getEmpName As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("er_id")) > 0 Then
                RepDate = Split(Tmp(i), "</td>")

                RepDate = Split(RepDate(2), "er_repdt_val1")
                nme = RepDate.Length - 1
                getEmpName = RepDate(nme)
                RepDate = Split(getEmpName, ">")
                nme = RepDate.Length - 1
                GetReportingDate = RepDate(nme)
                GetReportingDate = GetReportingDate.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetReportingDate
    End Function
    Public Function getEmploymentDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empDate() As String
        Dim sHtlm
        Dim getEmpDate As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("er_id")) > 0 Then
                empDate = Split(Tmp(i), "</td>")

                empDate = Split(empDate(3), "er_empdt_val1")
                nme = empDate.Length - 1
                getEmpDate = empDate(nme)
                empDate = Split(getEmpDate, ">")
                nme = empDate.Length - 1
                getEmploymentDate = empDate(nme)
                getEmploymentDate = getEmploymentDate.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getEmploymentDate
    End Function

    Public Function getMsgEmployment(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empDate() As String
        Dim sHtlm
        Dim getEmpDate As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("align=""center"" >")) > 0 Then
                empDate = Split(Tmp(i), "</td>")

                empDate = Split(empDate(0), "align=""center"" >")
                nme = empDate.Length - 1
                getEmpDate = empDate(nme)
                'empDate = Split(getEmpDate, ">")
                'nme = empDate.Length - 1
                getMsgEmployment = empDate(nme)
                getMsgEmployment = UCase(getMsgEmployment.Trim) & "."
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getMsgEmployment
    End Function


#End Region

#Region "Flexi Fund"
    '-------------- Enrollment Date --------------------
    Public Function getEnrollDate(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Flexi-Fund Enrollment Date")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(5), "Flexi-Fund Enrollment Date")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getEnrollDate = empID(nme)
                getEnrollDate = getEnrollDate.Trim
                Exit Function
            End If
        Next

        Return getEnrollDate
    End Function

    '-------------- Member Since --------------------
    Public Function getMemberSince(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Flexi-Fund Member Since")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(13), "Flexi-Fund Member Since")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMemberSince = empID(nme)
                getMemberSince = getMemberSince.Trim
                Exit Function
            End If
        Next

        Return getMemberSince
    End Function

    '-------------- Total Contribution --------------------
    Public Function getTotalContribution(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Dim str1(7) As String
        Dim ctr As Integer

        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim rchbx As New RichTextBox

        ' str1(0) = ""
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("SUMMARY")) > 0 Then

                GLAD = Split(Tmp(i), "SUMMARY")

                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        ctr = ctr
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p)
                            ctr = ctr + 1
                        End If

                        'rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.Text = Nothing
        rchbx.Text = str1(0) & " : " & str1(1) & vbNewLine & str1(2).Trim & " : " & str1(3) & vbNewLine & str1(4).Trim & " : " & str1(5) & vbNewLine & str1(6).Trim & " : " & str1(7)
        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    '-------------- Total Annual Incentive Benefit --------------------
    Public Function getTotAnlIncentBenefit(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Operating Expense")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(109), "Total Operating Expense")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTotAnlIncentBenefit = empID(nme)
                getTotAnlIncentBenefit = getTotAnlIncentBenefit.Trim
                Exit Function
            End If
        Next

        Return getTotAnlIncentBenefit
    End Function


    '-------------- Total Earnings --------------------
    Public Function getTotalEarnings(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Earnings")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(103), "Total Earnings")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTotalEarnings = empID(nme)
                getTotalEarnings = getTotalEarnings.Trim
                Exit Function
            End If
        Next

        Return getTotalEarnings
    End Function


    '-------------- Total Management Fee --------------------
    Public Function getTotalMgmtFee(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Net Ending Balance")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(115), "Net Ending Balance")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTotalMgmtFee = empID(nme)
                getTotalMgmtFee = getTotalMgmtFee.Trim
                Exit Function
            End If
        Next

        Return getTotalMgmtFee
    End Function

#End Region

#Region "Flexi Fund No Data"

    Public Function getMsgFFErr(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getMsgFFErr = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("not enrolled")) > 0 Then
                getMsgFFErr = True
                Exit Function
            End If
        Next

    End Function

    Public Function getMsg(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("alertType")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(2), "alertType")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMsg = empID(nme)
                getMsg = getMsg.Trim
                Exit Function
            End If
        Next

        Return getMsg
    End Function

#End Region

#Region "Loan Status"
    '---------- check if no loan ---------------

    Public Function getLoanErr(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("height")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(3), "height")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getLoanErr = empID(nme)
                getLoanErr = getLoanErr.Trim
                Exit Function
            End If
        Next

        Return getLoanErr
    End Function

    Public Function NoLoanStat(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        NoLoanStat = False
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member has no loan application filed")) > 0 Then
                NoLoanStat = True
                Exit Function
            End If
        Next

        Return NoLoanStat
    End Function

    Public Function NoCreditLoan(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        NoCreditLoan = False
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("MEMBER HAS NO LOAN RECORD IN DATABASE")) > 0 Then
                NoCreditLoan = True
                Exit Function
            End If
        Next

        Return NoCreditLoan
    End Function

    Public Function EncodedVal(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.WebBrowser1.Document.documentElement.outerHTML
        Tmp = Split(sHtlm, Chr(13))
        EncodedVal = False
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("encoded for validation")) > 0 Then
                EncodedVal = True
                Exit Function
            End If
        Next
    End Function

    Public Function ListAvailableLoan(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        ListAvailableLoan = False
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("List of Availed Loan")) > 0 Then
                ListAvailableLoan = True
                Exit Function
            End If
        Next
    End Function

    Public Function goToStatementLoan(ByVal webBrowserPath) As String
        Dim sHtlm

        Dim Tmp() As String
        Dim LoadLStatus() As String
        Dim getLStatus As String
        Dim nme As Integer
        Dim APremiumPath As String


        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("LoanDetails")) > 0 Then
                LoadLStatus = Split(Tmp(i), ">")

                LoadLStatus = Split(LoadLStatus(25), "Loan Type")
                nme = LoadLStatus.Length - 1
                getLStatus = LoadLStatus(nme)
                LoadLStatus = Split(getLStatus, "HREF=")
                LoadLStatus = Split(getLStatus, """")
                nme = LoadLStatus.Length - 2
                APremiumPath = LoadLStatus(nme)
                APremiumPath = APremiumPath.Trim
                webBrowserPath.Navigate(getPermanentURL & APremiumPath)

            End If
        Next
        Return goToStatementLoan
    End Function

#Region "Members has loan and Available loans"
    Public Function GetLoanType01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Loan Type :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(35), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetLoanType01 = empID(nme)
                GetLoanType01 = GetLoanType01.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetLoanType01
    End Function

    Public Function GetAppDate01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            'If InStr(1, LCase(Tmp(i)), LCase("Application Date :")) > 0 Then
            If InStr(1, LCase(Tmp(i)), LCase("Encoding Date :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(41), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                GetAppDate01 = empID(nme)
                GetAppDate01 = GetAppDate01.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetAppDate01
    End Function

    Public Function GetAppDate01_v2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("encdt_val")
        Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")
        'Dim arr2() As String = arr1(1).Split(">")

        Return arr1(1).Replace("</td", "")
    End Function

    Public Function GetAppDate01_v3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext

        Dim varPosition As Integer = sHtlm.IndexOf("Encoding Date")
        Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split("</td>")
        Dim arr2() As String = arr1(2).Split(">")

        Return arr2(1).Trim
    End Function

    Public Function GetLoanAppStat01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Loan Application Status :")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                'empID = Split(empID(19), ">")

                'revised by edel 02/02/2017 due to changes in loan status page
                empID = Split(empID(17), ">")

                nme = empID.Length - 1
                getEmpID = empID(nme)
                ' empID = Split(getEmpID, "</td")
                ' nme = empID.Length - 2
                GetLoanAppStat01 = empID(nme)
                GetLoanAppStat01 = GetLoanAppStat01.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetLoanAppStat01
    End Function

    Public Function GetCheckDate01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ckdte")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(1), "ckdte_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                GetCheckDate01 = empID(nme)
                GetCheckDate01 = GetCheckDate01.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCheckDate01
    End Function

    Public Function GetLoanAmount01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_amt")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(7), "loan_amt_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                GetLoanAmount01 = empID(nme)
                GetLoanAmount01 = GetLoanAmount01.Trim
                'Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetLoanAmount01
    End Function

    ' ----------------- 2nd header ---------------------- '

    Public Function GetAmountDue01(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Amount Due:")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(86), "")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&")
                nme = empID.Length - 3
                GetAmountDue01 = empID(nme)
                GetAmountDue01 = GetAmountDue01.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetAmountDue01
    End Function

    Public Function GetMonthlyAmort(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Monthly Amortization :")) > 0 Then
                empID = Split(Tmp(i), ">")

                'revised by edel 02/02/2017 due to changes in loan status page
                'empID = Split(empID(47), "")
                empID = Split(empID(51), "")

                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                GetMonthlyAmort = empID(nme)
                GetMonthlyAmort = GetMonthlyAmort.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetMonthlyAmort
    End Function

    Public Function goToCreditLoan(ByVal webBrowserPath) As String
            Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Account as of")) > 0 Then
                GLAD = Split(Tmp(i), ">")
                goToCreditLoan = Left(GLAD(2), Len(GLAD(2)) - 4)
                goToCreditLoan = goToCreditLoan.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetTotalAmtDue(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
      For i = LBound(Tmp) To UBound(Tmp)
            'If InStr(1, LCase(Tmp(i)), LCase("Total Amount Due")) > 0 Then
            If InStr(1, LCase(Tmp(i)), LCase("Total Amount Due : ")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                'GetTotalAmtDue = Left(GLAD(2), Len(GLAD(2)) - 16)
                GetTotalAmtDue = Left(GLAD(1), Len(GLAD(1)) - 16)
                GetTotalAmtDue = GetTotalAmtDue.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetAmountNotYetDue(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i + 1)), LCase("ee_amt_due_val")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                GetAmountNotYetDue = Left(GLAD(2), Len(GLAD(2)) - 16)
                GetAmountNotYetDue = GetAmountNotYetDue.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetTotalAmtObligation(ByVal webBrowserPath) As String
      Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))

        ''old
        'For i = LBound(Tmp) To UBound(Tmp)
        '    If InStr(1, LCase(Tmp(i + 1)), LCase("ee_tot_obl_val")) > 0 Then
        '        GLAD = Split(Tmp(i + 1), " >")
        '        GetTotalAmtObligation = Left(GLAD(2), Len(GLAD(2)) - 16)
        '        GetTotalAmtObligation = GetTotalAmtObligation.Trim
        '        Exit Function
        '    End If
        'Next

        'new
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i + 1)), LCase("Total Amount of Obligation : ")) > 0 Then
                GLAD = Split(Tmp(i + 2), ">")
                GetTotalAmtObligation = Left(GLAD(1), Len(GLAD(1)) - 16)
                GetTotalAmtObligation = GetTotalAmtObligation.Trim
                Exit Function
            End If
        Next

        Return GetTotalAmtObligation
    End Function

#End Region

#Region "Members has no loan and has available loans"
    Public Function goToLoanType(ByVal webBrowserPath) As String
        Try
            Dim Tmp() As String
            Dim sHtlm
            Dim i As Integer
            Dim GLAD() As String
            Dim haha As Integer = 1
            sHtlm = webBrowserPath.Document.body.outerHtml
            Tmp = Split(sHtlm, Chr(13))
            For i = LBound(Tmp) To UBound(Tmp)
                If InStr(1, LCase(Tmp(i)), LCase("Loan Type")) > 0 Then
                    GLAD = Split(Tmp(i + 8), ">")
                    goToLoanType = Left(GLAD(1), Len(GLAD(1)) - 10)
                    goToLoanType = goToLoanType.Trim
                    Exit Function
                End If
            Next
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function goToLoanTypev2(ByVal webBrowserPath) As String
        sHtlm = webBrowserPath.Document.body.outerHtml
        Dim varPosition As Integer = sHtlm.IndexOf("Loan Type")
        Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

        Return arr1(1).ToUpper.Replace("</TD", "")
    End Function

    Public Function goToLoanDate(ByVal webBrowserPath) As String
        Try
            Dim Tmp() As String
            Dim sHtlm
            Dim i As Integer
            Dim GLAD() As String
            Dim haha As Integer = 1
            sHtlm = webBrowserPath.Document.body.outerHtml
            Tmp = Split(sHtlm, Chr(13))
            For i = LBound(Tmp) To UBound(Tmp)
                'If InStr(1, LCase(Tmp(i)), LCase("Check Date / Loan Date")) > 0 Then
                If InStr(1, LCase(Tmp(i)), LCase("Loan Date")) > 0 Then
                    GLAD = Split(Tmp(i + 8), ">")
                    goToLoanDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                    goToLoanDate = goToLoanDate.Trim
                    Exit Function
                End If
            Next
        Catch ex As Exception
            Return ""
        End Try


    End Function


    Public Function goToLoanDateTechRet(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Check Date / Loan Date")) > 0 Then
                GLAD = Split(Tmp(i + 8), ">")
                goToLoanDateTechRet = Left(GLAD(1), Len(GLAD(1)) - 4)
                goToLoanDateTechRet = goToLoanDateTechRet.Trim
                Exit Function
            ElseIf InStr(1, LCase(Tmp(i)), LCase("Check Date /<br> Loan Date")) > 0 Then
                GLAD = Split(Tmp(i + 8), ">")
                goToLoanDateTechRet = Left(GLAD(1), Len(GLAD(1)) - 4)
                goToLoanDateTechRet = goToLoanDateTechRet.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function goToVoucherNumber(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Check Number / Voucher Number")) > 0 Then
                GLAD = Split(Tmp(i + 8), ">")
                goToVoucherNumber = Left(GLAD(1), Len(GLAD(1)) - 4)
                goToVoucherNumber = goToVoucherNumber.Trim
                Exit Function
            ElseIf InStr(1, LCase(Tmp(i)), LCase("Check Number /<BR>Voucher No /<BR>Loan AcctNo")) > 0 Then
                GLAD = Split(Tmp(i + 8), ">")
                goToVoucherNumber = Left(GLAD(1), Len(GLAD(1)) - 4)
                goToVoucherNumber = goToVoucherNumber.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function goToVoucherNumber_v2(ByVal webBrowserPath) As String
        Try
            Dim Tmp() As String
            Dim sHtlm
            Dim i As Integer
            Dim GLAD() As String
            Dim haha As Integer = 1
            sHtlm = webBrowserPath.Document.body.outerHtml

            Dim varPosition As Integer = sHtlm.IndexOf("voucher_val")
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(1).Replace("</TD", "")
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function GetLoanDateValue_V2(ByVal webBrowserPath) As String
        'Public Function GetLoanDateValue_V2(ByVal s As String) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        'sHtlm = s

        'class="tableData" width ="160%" valign="top" >CONSTRUCTION STRATEGIES & MGT</td></tr><tr><td class="litetableData" width ="40%" valign="top" >  Loan Date : </td><td class="litetableData" width ="160%" valign="top" ><input type="hidden" id="lndte_val">09-23-2009</td></tr><tr><td class="tableData" width ="40%" valign="top" > <input
        Dim varPosition As Integer = sHtlm.IndexOf("lndte_val")
        Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

        Return arr1(1).ToUpper.Replace("</TD", "")
    End Function

    Public Function GetLoanAmountValue_V2(ByVal webBrowserPath) As String
        'Public Function GetLoanAmountValue_V2(ByVal s As String) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        'sHtlm = s

        'class="tableData" width ="160%" valign="top" >CONSTRUCTION STRATEGIES & MGT</td></tr><tr><td class="litetableData" width ="40%" valign="top" >  Loan Date : </td><td class="litetableData" width ="160%" valign="top" ><input type="hidden" id="lndte_val">09-23-2009</td></tr><tr><td class="tableData" width ="40%" valign="top" > <input
        Dim varPosition As Integer = sHtlm.IndexOf("loan_amt_val")
        Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

        Return arr1(1).ToUpper.Replace("</TD", "")
    End Function

    Public Function IsRegisteredInSSSWebsite(ByVal webBrowserPath) As Boolean
        sHtlm = webBrowserPath.Document.body.outerHtml

        Dim varPosition As Integer = sHtlm.IndexOf("REGISTERED in the SSS WEBSITE")
        If varPosition = -1 Then Return False Else Return True
    End Function

    Public Function goToLoanAmount(ByVal webBrowserPath) As String
        Try
            Dim Tmp() As String
            Dim sHtlm
            Dim i As Integer
            Dim GLAD() As String
            Dim haha As Integer = 1
            sHtlm = webBrowserPath.Document.body.outerHtml
            Tmp = Split(sHtlm, Chr(13))
            For i = LBound(Tmp) To UBound(Tmp)
                If InStr(1, LCase(Tmp(i)), LCase("Loan Amount")) > 0 Then
                    GLAD = Split(Tmp(i + 8), ">")
                    goToLoanAmount = Left(GLAD(1), Len(GLAD(1)) - 4)
                    goToLoanAmount = goToLoanAmount.Trim
                    Exit Function
                End If
            Next
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Function CertifyingEmployerId(ByVal webBrowserPath) As String
        Try
            Dim Tmp() As String
            Dim sHtlm
            Dim i As Integer
            Dim GLAD() As String
            Dim haha As Integer = 1
            sHtlm = webBrowserPath.Document.body.outerHtml
            Tmp = Split(sHtlm, Chr(13))
            For i = LBound(Tmp) To UBound(Tmp)
                If InStr(1, LCase(Tmp(i)), LCase("Certifying Employer Id")) > 0 Then
                    GLAD = Split(Tmp(i + 8), ">")
                    CertifyingEmployerId = Left(GLAD(1), Len(GLAD(1)) - 4)
                    CertifyingEmployerId = CertifyingEmployerId.Trim
                    Exit Function
                End If
            Next
        Catch ex As Exception
            Return ""
        End Try

    End Function

#End Region

#Region "Credited Loan Payments"
    Public Function getCredLoanType(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_type")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(3), "loan_type_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getCredLoanType = empID(nme)
                getCredLoanType = getCredLoanType.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getCredLoanType
    End Function

    Public Function getCredCheckDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ckdte")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(13), "ckdte_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getCredCheckDate = empID(nme)
                getCredCheckDate = getCredCheckDate.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getCredCheckDate
    End Function

    Public Function getCredLoanAmt(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_amt")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(19), "loan_amt_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getCredLoanAmt = empID(nme)
                getCredLoanAmt = getCredLoanAmt.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getCredLoanAmt
    End Function

    Public Function getCertEmpID(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Certifying Employer Id :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(35), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - empID.Length
                getCertEmpID = empID(nme)

                getCertEmpID = getCertEmpID.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getCertEmpID
    End Function

    Public Function getCerEmpName(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Certifying Employer Name :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(41), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                getEmpID = empID(nme)

                getCerEmpName = empID(nme)
                getCerEmpName = getCerEmpName.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getCerEmpName
    End Function

    Public Function getLoanMonthLoan(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Loan Month :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(75), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                getEmpID = empID(nme)

                getLoanMonthLoan = empID(nme)
                getLoanMonthLoan = getLoanMonthLoan.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getLoanMonthLoan
    End Function

    Public Function getTotAmtOblig(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ee_tot_obl")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(42), "ee_tot_obl_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;&nbsp;")
                nme = empID.Length - empID.Length
                getEmpID = empID(nme)
                getTotAmtOblig = empID(nme)
                getTotAmtOblig = getTotAmtOblig.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return getTotAmtOblig
    End Function

#End Region

#End Region

#Region "SSS / UMID Card Information"

    Public Function GetcheckSSSIDClearance(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        GetcheckSSSIDClearance = False
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member Qualified for an SSS ID Card")) > 0 Then
                GetcheckSSSIDClearance = True
                Exit Function
            End If
        Next
    End Function

    Public Function getMsgSSSIDClearance(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("headerTable")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text
        'Dim Tmp() As String
        'Dim empID() As String
        'Dim sHtlm
        'Dim getEmpID As String
        'Dim i As Integer
        'Dim nme As Integer

        'Dim haha As Integer = 1
        'sHtlm = webBrowserPath.Documenttext
        'Tmp = Split(sHtlm, Chr(13))
        'For i = LBound(Tmp) To UBound(Tmp)
        '    If InStr(1, LCase(Tmp(i)), LCase("headerTable")) > 0 Then
        '        empID = Split(Tmp(i), ">")

        '        empID = Split(empID(3), "headerTable")
        '        nme = empID.Length 
        '        getEmpID = empID(nme)
        '        empID = Split(getEmpID, "</th")
        '        nme = empID.Length - 2
        '        getMsgSSSIDClearance = empID(nme)
        '        getMsgSSSIDClearance = getMsgSSSIDClearance.Trim
        '        Exit Function
        '    End If
        'Next

        'Return getMsgSSSIDClearance
    End Function

    Public Function GetCSJNumber(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("card_ser_val")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(1), "card_ser_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, """>")
                nme = empID.Length - 1


                GetCSJNumber = empID(nme)
                GetCSJNumber = GetCSJNumber.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCSJNumber
    End Function

    Public Function GetCSJNumberv2(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Card Serial No./Job No :")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(1), "Card Serial No./Job No :")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, """>")
                nme = empID.Length - 1


                GetCSJNumberv2 = empID(nme)
                GetCSJNumberv2 = GetCSJNumberv2.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return ""
    End Function

    '****** OLD GET GetCSJNumber
    'Public Function GetCSJNumber(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim empID() As String
    '    Dim sHtlm
    '    Dim getEmpID As String
    '    Dim i As Integer
    '    Dim nme As Integer

    '    Dim haha As Integer = 1
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("card_ser")) > 0 Then
    '            empID = Split(Tmp(i), "</td>")

    '            empID = Split(empID(1), "card_ser_val")
    '            nme = empID.Length - 1
    '            getEmpID = empID(nme)
    '            empID = Split(getEmpID, "> ")
    '            nme = empID.Length - 1
    '            GetCSJNumber = empID(nme)
    '            GetCSJNumber = GetCSJNumber.Trim
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetCSJNumber
    'End Function



    ' **************** old getCapturedON


    'Public Function GetCapturedON(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim empID() As String
    '    Dim sHtlm
    '    Dim getEmpID As String
    '    Dim i As Integer
    '    Dim nme As Integer

    '    Dim haha As Integer = 1
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("cap_on")) > 0 Then
    '            empID = Split(Tmp(i), "</td>")

    '            empID = Split(empID(15), "cap_on_val")
    '            nme = empID.Length - 1
    '            getEmpID = empID(nme)
    '            empID = Split(getEmpID, ">")
    '            getEmpID = empID(nme)
    '            empID = Split(getEmpID, "CAPTURED  ON")
    '            getEmpID = empID(nme)
    '            'empID = Split(getEmpID, "<")
    '            ' nme = empID.Length - 2
    '            GetCapturedON = getEmpID
    '            GetCapturedON = GetCapturedON.Trim
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetCapturedON
    'End Function

    ' ******************* OLD   GetCapturedDate


    'Public Function GetCapturedDate(ByVal webBrowserPath) As String
    '    'On Error GoTo errHdlr
    '    Dim Tmp() As String
    '    Dim empID() As String
    '    Dim sHtlm
    '    Dim getEmpID As String
    '    Dim i As Integer
    '    Dim nme As Integer

    '    Dim haha As Integer = 1
    '    sHtlm = webBrowserPath.Documenttext
    '    Tmp = Split(sHtlm, Chr(13))
    '    For i = LBound(Tmp) To UBound(Tmp)
    '        If InStr(1, LCase(Tmp(i)), LCase("cap_site_val")) > 0 Then
    '            empID = Split(Tmp(i), "</td>")

    '            empID = Split(empID(19), "cap_site_val")
    '            nme = empID.Length - 1
    '            getEmpID = empID(nme)

    '            empID = Split(getEmpID, ">")
    '            nme = empID.Length - 1
    '            GetCapturedDate = empID(nme)
    '            GetCapturedDate = GetCapturedDate.Trim
    '            Exit Function
    '        End If
    '    Next
    '    'Exit Function
    '    'errHdlr:
    '    'logs
    '    Return GetCapturedDate
    'End Function

    Public Function GetCapturedON(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("cap_on_val")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(17), "<li>")
                nme = empID.Length - 2
                getEmpID = empID(nme)

                'empID = Split(getEmpID, "</li>")
                'getEmpID = empID(0)
                'empID = Split(getEmpID, "Data Captured on")
                'getEmpID = empID(1)
                'empID = Split(getEmpID, "<")
                ' nme = empID.Length - 2
                ' GetCapturedON = getEmpID

                GetCapturedON = (Regex.Replace(getEmpID, matchpattern, "", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                GetCapturedON = GetCapturedON.Replace("Data Captured on", "")
                GetCapturedON = GetCapturedON.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCapturedON
    End Function

    Public Function GetCapturedONv2(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext

        Dim sel As Short
        If sHtlm.ToString.Contains("Data Captured on") Then sel = 1
        If sHtlm.ToString.Contains("Data captured on") Then sel = 2

        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If sel = 1 Then
                If InStr(1, LCase(Tmp(i)), LCase("Data Captured on")) > 0 Then
                    empID = Split(Tmp(i), "</td>")

                    empID = Split(empID(17), "<li>")
                    For Each v As String In empID
                        If v.Contains("Data Captured on") Then
                            getEmpID = v
                            Exit For
                        End If
                    Next

                    GetCapturedONv2 = (Regex.Replace(getEmpID, matchpattern, "", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                    GetCapturedONv2 = GetCapturedONv2.Replace("Data Captured on", "")
                    GetCapturedONv2 = GetCapturedONv2.Trim
                    'Exit Function
                End If
            Else
                If GetCapturedONv2 = Nothing Then
                    If InStr(1, LCase(Tmp(i)), LCase("Data captured on")) > 0 Then
                        empID = Split(Tmp(i), "</td>")

                        empID = Split(empID(17), "<li>")
                        For Each v As String In empID
                            If v.Contains("Data captured on") Then
                                getEmpID = v
                                Exit For
                            End If
                        Next

                        GetCapturedONv2 = (Regex.Replace(getEmpID, matchpattern, "", RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                        GetCapturedONv2 = GetCapturedONv2.Replace("Data captured on", "")
                        GetCapturedONv2 = GetCapturedONv2.Trim
                        Exit Function
                    End If
                End If
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCapturedONv2
    End Function

    Public Function GetGeneratedON(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("gen_on_val")) > 0 Then
                empID = Split(Tmp(i), "</td>")
                empID = Split(empID(17), "gen_on_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "GENERATED ON")
                nme = empID.Length - 1
                ' empID = Split(getEmpID, ">")
                GetGeneratedON = empID(nme)
                GetGeneratedON = GetGeneratedON.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetGeneratedON
    End Function

    Public Function GetGeneratedONv2(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Card Generated on")) > 0 Then
                empID = Split(Tmp(i), "</td>")
                empID = Split(empID(17), "Card Generated on")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "GENERATED ON")
                nme = empID.Length - 1
                ' empID = Split(getEmpID, ">")
                GetGeneratedONv2 = empID(nme)
                GetGeneratedONv2 = GetGeneratedONv2.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetGeneratedONv2
    End Function

    Public Function GetCapturedDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("card_site")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(13), "card_site_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)

                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                GetCapturedDate = empID(nme)
                GetCapturedDate = GetCapturedDate.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCapturedDate
    End Function

    Public Function GetCapturedDatev2(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Capture Site :")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(13), "Capture Site :")
                nme = empID.Length - 1
                getEmpID = empID(nme)

                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                GetCapturedDatev2 = empID(nme)
                GetCapturedDatev2 = GetCapturedDatev2.Trim
                Exit Function
            End If
        Next
        'Exit Function
        'errHdlr:
        'logs
        Return GetCapturedDatev2
    End Function

#End Region

#Region "DDR FUNERAL"

    Public Function errBenEligibility(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        errBenEligibility = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                errBenEligibility = True
                Exit Function
            End If
        Next

    End Function

    Public Function getMsgBenEligibility(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getDDRFuneralMsg(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        ' Dim rchbx As New RichTextBox
        Dim rchbx As String
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("tableDataHigh")) > 0 Then

                'GLAD = Split(Tmp(i), "rejected due to the following reason(s):")
                GLAD = Split(Tmp(i), "following reason(s):")


                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        Dim _split2 As String
                        _split2 = _split(p).Replace("&nbsp;", vbNewLine)
                        rchbx = rchbx & vbNewLine & _split2 ' rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        'rchbx.WordWrap = True

        '  rchbx.Text = rchbx.Text & "* sample1 " & vbNewLine & "* sample2 "

        ' Dim _msgSplit = rchbx.Text.Split("*")


        Return rchbx
    End Function

    Public Function errBenEligibility2(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        errBenEligibility2 = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("can not proceed")) > 0 Then
                errBenEligibility2 = True
                Exit Function
            End If
        Next

    End Function

    Public Function getMsgBenEligibility2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getEarlyRetireDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Earliest Retirement Date")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getEarlyRetireDate = empID(nme)
                getEarlyRetireDate = getEarlyRetireDate.Trim
                Exit Function
            End If
        Next
        Return getEarlyRetireDate
    End Function

    Public Function getTotNumContri(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Number of Posted Contributions")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(15), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getTotNumContri = empID(nme)
                getTotNumContri = getTotNumContri.Trim
                Exit Function
            End If
        Next
        Return getTotNumContri
    End Function

    Public Function getTotNumLumpContri(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Number of Lumped Contributions")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(21), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getTotNumLumpContri = empID(nme)
                getTotNumLumpContri = getTotNumLumpContri.Trim
                Exit Function
            End If
        Next
        Return getTotNumLumpContri
    End Function

    Public Function getTotNumDmPaidContri(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Number of Deemed Paid Contributions")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(27), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getTotNumDmPaidContri = empID(nme)
                getTotNumDmPaidContri = getTotNumDmPaidContri.Trim
                Exit Function
            End If
        Next
        Return getTotNumDmPaidContri
    End Function

    Public Function getTotNumContriSemContin(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Number of Contributions Prior to Semester of Contingency")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(33), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getTotNumContriSemContin = empID(nme)
                getTotNumContriSemContin = getTotNumContriSemContin.Trim
                Exit Function
            End If
        Next
        Return getTotNumContriSemContin
    End Function

    Public Function getTotNumContriSemContinSSFunertal(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("&nbsp &nbsp Total Number of Contributions Prior to Semester of Contingency")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(27), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;")
                nme = empID.Length - 3
                getTotNumContriSemContinSSFunertal = empID(nme)
                getTotNumContriSemContinSSFunertal = getTotNumContriSemContinSSFunertal.Trim
                Exit Function
            End If
        Next
        Return getTotNumContriSemContinSSFunertal
    End Function

    Public Function getLumpSum(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getLumpSum = Left(GLAD(1), Len(GLAD(1)) - 4)
                getLumpSum = getLumpSum.Trim
                Exit Function
            End If
        Next
    End Function

#End Region

#Region "Benefits Eligibility Death"

    Public Function getBenEligibilityErr(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getBenEligibilityErr = empID(nme)
                getBenEligibilityErr = getBenEligibilityErr.Trim

                Exit Function
            End If
        Next
        Return getBenEligibilityErr
    End Function

    Public Function getBenEligibilityErr2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(13), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getBenEligibilityErr2 = empID(nme)
                getBenEligibilityErr2 = getBenEligibilityErr2.Trim

                Exit Function
            End If
        Next
        Return getBenEligibilityErr2
    End Function

    Public Function getBenEligibilityErr3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(17), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getBenEligibilityErr3 = empID(nme)
                getBenEligibilityErr3 = getBenEligibilityErr3.Trim

                Exit Function
            End If
        Next
        Return getBenEligibilityErr3
    End Function

    Public Function getAverageMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim str1(9) As String
        Dim rchbx As New RichTextBox
        Dim ctr As Integer
        ctr = Nothing

        For i = LBound(Tmp) To UBound(Tmp)
            '   Dim matchpattern As String = "<(?:[^>=]|='[^']*'|=""[^""]*""|=[^'""][^\s>]*)*>  <(?:[^>=]|='[^']*'|=""[^""]*""|=[^'""][^\s>]*)*>"
            str1(0) = "Average Monthly Salary Credit : "
            If InStr(1, LCase(Tmp(i)), LCase("Death Benefit Eligibility")) > 0 Then
                GLAD = Split(Tmp(i), "Average Monthly Salary Credit")

                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})
                For p = 0 To _split.Length - 1
                    '  rchbx.Text = Nothing
                    If _split(p).Trim = "" Then
                    Else
                        ctr = ctr + 1
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p)
                        End If
                    End If
                Next
            End If

        Next
        rchbx.Text = str1(0) & str1(1) & vbNewLine & str1(2).Trim & " : " & str1(3) & vbNewLine & str1(4).Trim & " : " & str1(5) & vbNewLine & str1(6).Trim & " : " & str1(7) & vbNewLine & str1(8).Trim & " : " & str1(9)

        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getPrimary(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Primary Beneficiary Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(150), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getPrimary = empID(nme)
                getPrimary = getPrimary.Trim
                Exit Function
            End If
        Next
        Return getPrimary
    End Function

    Public Function getMinAmtBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Minimum amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(156), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMinAmtBenefit = empID(nme)
                getMinAmtBenefit = getMinAmtBenefit.Trim
                Exit Function
            End If
        Next
        Return getMinAmtBenefit
    End Function

    Public Function getSecondary(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Secondary Beneficiary Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(162), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSecondary = empID(nme)
                getSecondary = getSecondary.Trim
                Exit Function
            End If
        Next
        Return getSecondary
    End Function

    Public Function getAmountBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(168), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getAmountBenefit = empID(nme)
                getAmountBenefit = getAmountBenefit.Trim
                Exit Function
            End If
        Next
        Return getAmountBenefit
    End Function

#End Region

#Region "Benefits Eligibility Total Disability"
    Public Function getAverageMSC2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim str1(5) As String
        Dim ctr As Integer
        Dim textxx As String
        Dim rchbx As New RichTextBox

        str1(0) = "Average Monthly Salary Credit : "

        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("Total Disability Eligibility")) > 0 Then

                GLAD = Split(Tmp(i), "Average Monthly Salary Credit")

                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        ctr = ctr + 1
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p)
                        End If
                        ' rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.Text = str1(0) & str1(1) & vbNewLine & str1(2).Trim & " : " & str1(3) & vbNewLine & str1(4).Trim & " : " & str1(5)

        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getTypeofBenefitSSFuneral(ByVal webBrowserPath) As String
        ' nikki01
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(37), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTypeofBenefitSSFuneral = empID(nme)
                getTypeofBenefitSSFuneral = getTypeofBenefitSSFuneral.Trim
                Exit Function
            End If
        Next
        Return getTypeofBenefitSSFuneral
    End Function

    Public Function getMinAmtBenefit2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(142), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMinAmtBenefit2 = empID(nme)
                getMinAmtBenefit2 = getMinAmtBenefit2.Trim
                Exit Function
            End If
        Next
        Return getMinAmtBenefit2
    End Function

#End Region

#Region "Loan Eligibility"

    Public Function getMsgLoan(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getMsgLoan = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                getMsgLoan = True
                Exit Function
            End If
        Next

    End Function

    Public Function getMsgLoan2(ByVal webBrowserPath) As String


        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim rchbx As New RichTextBox
        rchbx.Width = 400
        rchtextbox.Text = ""
        rchbx.Text = ""
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getMsgLoan3(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Salary Loan")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(17), "")
                getEmpID = empID(nme)

                If getEmpID.Contains("<") Or getEmpID.Contains("<") Or getEmpID.Contains("/") Or getEmpID.Contains("table") Or getEmpID.Contains("td") Or getEmpID.Contains("tr") Or getEmpID = "" Or getEmpID = Nothing Then
                    getEmpID = ""
                Else

                    empID = Split(getEmpID, "</td")
                    nme = empID.Length - 2
                    getMsgLoan3 = empID(nme)
                    getMsgLoan3 = getMsgLoan3.Trim

                End If
            End If
        Next
        If getMsgLoan3 = "" Or getMsgLoan3 = Nothing Then
            getMsgLoan3 = ""
            Return getMsgLoan3
        Else
            Return getMsgLoan3

        End If
    End Function

    Public Function getLoanAmount(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_month")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(2), "loan_month_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp ")
                nme = empID.Length - empID.Length
                getLoanAmount = empID(nme)
                getLoanAmount = getLoanAmount.Trim
                Exit Function
            End If
        Next
        Return getLoanAmount
    End Function

    Public Function getLoanAmountv2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        'Dim sHtlm
        'Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim str1(5) As String
        'Dim textxx As String
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)
            Dim ctr As Integer
            'str1(0) = "Average Monthly Salary Credit : "

            If InStr(1, LCase(Tmp(i)), LCase("Salary Loan")) > 0 Then

                GLAD = Split(Tmp(i), "Average Monthly Salary Credit")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New String() {"|"}, StringSplitOptions.RemoveEmptyEntries).Where(Function(s) Not String.IsNullOrWhiteSpace(s)).ToArray()
                'rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        If ctr >= str1.Length Then
                            ReDim Preserve str1(ctr)
                        End If
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p).Replace("&nbsp ", "").Replace("&nbsp", "")
                        End If
                        ctr = ctr + 1
                    End If
                Next

            End If

        Next

        Dim loanMonth As String = ""
        Dim msc As String = ""
        Dim loanAmount As String = ""
        Dim getPrevBalance As String = ""
        Dim getLoanBalance As String = ""
        Dim getLoanProceeds As String = ""

        For i As Short = 0 To str1.Length - 1
            If str1(i) <> Nothing Then
                If str1(i).Contains("Loanable Month") Then
                    loanMonth = str1(i + 1)
                ElseIf str1(i).Contains("Max Loanable Amount") Then
                    loanAmount = str1(i + 1)
                ElseIf str1(i).Contains("Previous Loan Balances") Then
                    getPrevBalance = str1(i + 1)
                ElseIf str1(i).Contains("Service Fee") Then
                    getLoanBalance = str1(i + 1)
                ElseIf str1(i).Contains("Loan Proceeds") Then
                    getLoanProceeds = str1(i + 1)
                End If
            End If
        Next

        Return String.Format("{0}|{1}|{2}|{3}|{4}|{5}", loanMonth, msc, loanAmount, getPrevBalance, getLoanBalance, getLoanProceeds)
    End Function

    Public Function getLoanEligibilityMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ave_sal")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(4), "ave_sal_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp ")
                nme = empID.Length - empID.Length
                getLoanEligibilityMSC = empID(nme)
                getLoanEligibilityMSC = getLoanEligibilityMSC.Trim
                Exit Function
            End If
        Next
        Return getLoanEligibilityMSC
    End Function

    Public Function getLoanAmountEligibility(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_amt")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(6), "loan_amt_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp")
                nme = empID.Length - empID.Length
                getLoanAmountEligibility = empID(nme)
                getLoanAmountEligibility = getLoanAmountEligibility.Trim
                Exit Function
            End If
        Next
        Return getLoanAmountEligibility
    End Function

    Public Function getLoanBalance(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("ser_fee")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(10), "ser_fee_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp")
                nme = empID.Length - empID.Length

                getLoanBalance = empID(nme)
                getLoanBalance = getLoanBalance.Trim
                Exit Function
            End If
        Next
        Return getLoanBalance
    End Function

    Public Function getPrevBalance(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("prev_loan_balances")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getPrevBalance = Left(GLAD(2), Len(GLAD(2)) - 17)
                getPrevBalance = getPrevBalance.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function getPrevBalanceTechRet(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("prev_loan_balances")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getPrevBalanceTechRet = Left(GLAD(1), Len(GLAD(1)) - 4)
                getPrevBalanceTechRet = getPrevBalanceTechRet.Trim
                Exit Function
            End If
        Next
    End Function


    Public Function getContrib(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Posted Contributions</td")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getContrib = Left(GLAD(1), Len(GLAD(1)) - 4)
                getContrib = getContrib.Trim
                Exit Function
            End If
        Next

        Return getContrib
    End Function

    Public Function getLoanProceeds(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim i As Integer
        Dim nme As Integer
        Dim getEmpID As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("loan_pro")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(12), "loan_pro_val")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp")
                nme = empID.Length - empID.Length
                getLoanProceeds = empID(nme)
                getLoanProceeds = getLoanProceeds.Trim
                Exit Function
            End If
        Next
        Return getLoanProceeds
    End Function

#End Region

#Region "Maternity Claims"

    Public Function getDateFiled(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date Filed:")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(16), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getDateFiled = empID(nme)
                getDateFiled = getDateFiled.Trim
                Exit Function
            End If
        Next
        Return getDateFiled
    End Function

    Public Function getBenefitAppStatus(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Status:")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(64), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getBenefitAppStatus = empID(nme)
                getBenefitAppStatus = getBenefitAppStatus.Trim
                Exit Function
            End If
        Next
        Return getBenefitAppStatus
    End Function

    Public Function getMaternityDeliveryDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Delivery Date :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(46), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getMaternityDeliveryDate = empID(nme)
                getMaternityDeliveryDate = getMaternityDeliveryDate.Trim
                Exit Function
            End If
        Next
        Return getMaternityDeliveryDate
    End Function

    Public Function getNoOfDays(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement Period :")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(34), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 3
                getNoOfDays = empID(nme)
                getNoOfDays = getNoOfDays.Trim
                Exit Function
            End If
        Next
        Return getNoOfDays
    End Function

    Public Function getCheckDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Settlement Date:")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(70), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getCheckDate = empID(nme)
                getCheckDate = getCheckDate.Trim
                Exit Function
            End If
        Next
        Return getCheckDate
    End Function

    Public Function getAmountPaid(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount Paid:")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(76), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getAmountPaid = empID(nme)
                getAmountPaid = getAmountPaid.Trim
                Exit Function
            End If
        Next
        Return getAmountPaid
    End Function

#End Region

#Region "Sickness Claims"

    Public Function getSicknessBenefitError(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getSicknessBenefitError = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member has no sickness claim")) > 0 Then
                getSicknessBenefitError = True
                Exit Function
            End If
        Next

    End Function

    Public Function getSicknessBenefitError2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("alertType")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(4), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</tr")
                nme = empID.Length - 2
                getSicknessBenefitError2 = empID(nme)
                getSicknessBenefitError2 = getSicknessBenefitError2.Trim
                Exit For
            End If
        Next
        If getSicknessBenefitError2 = "" Or getSicknessBenefitError2 = Nothing Then
            getSicknessBenefitError2 = ""
            Return getSicknessBenefitError2
        Else
            Return getSicknessBenefitError2
        End If
    End Function

    Public Function getSicknessBenefitError3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("alertType")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(5), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</table")
                nme = empID.Length - 2
                getSicknessBenefitError3 = empID(nme)
                getSicknessBenefitError3 = getSicknessBenefitError3.Trim
                Exit For
            End If
        Next
        If getSicknessBenefitError3 = "" Or getSicknessBenefitError3 = Nothing Then
            getSicknessBenefitError3 = ""
            Return getSicknessBenefitError3
        Else
            Return getSicknessBenefitError3
        End If
    End Function

    Public Function getSicknessBenefitAppStatus(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(9), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSicknessBenefitAppStatus = empID(nme)
                getSicknessBenefitAppStatus = getSicknessBenefitAppStatus.Trim
                Exit Function
            End If
        Next
        Return getSicknessBenefitAppStatus
    End Function

    Public Function getDateConfined(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement Start")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(21), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getDateConfined = empID(nme)
                getDateConfined = getDateConfined.Trim
                Exit Function
            End If
        Next
        Return getDateConfined
    End Function

    Public Function getSicknessNoOfDays(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Number of Days Claimed")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(33), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSicknessNoOfDays = empID(nme)
                getSicknessNoOfDays = getSicknessNoOfDays.Trim

                Exit Function
            End If
        Next
        Return getSicknessNoOfDays
    End Function


    Public Function getSicknessCheckDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement End")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(27), "</td")
                nme = empID.Length - 2
                getEmpID = empID(nme)
                'empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getSicknessCheckDate = empID(nme)
                getSicknessCheckDate = getSicknessCheckDate.Trim
                Exit Function
            End If
        Next
        Return getSicknessCheckDate
    End Function


    Public Function getSicknesAmountPaid(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase(">Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(15), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSicknesAmountPaid = empID(nme)
                getSicknesAmountPaid = getSicknesAmountPaid.Trim
                Exit Function
            End If
        Next
        Return getSicknesAmountPaid
    End Function

    Public Function getSicknessErr(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getSicknessErr = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                getSicknessErr = True
                Exit Function
            End If
        Next

    End Function

    Public Function getSicknessErr2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text


    End Function

    Public Function getSicknessErr3(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String

        Dim sHtlm
        Dim getEmpID As String

        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(17), "")

                'nme = empID.Length 
                getEmpID = empID(nme)


                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2

                empID = Split(empID(nme), " ")

                getSicknessErr3 = getEmpID
                Exit For
            End If

        Next

        If getSicknessErr3 = Nothing Or getSicknessErr3 = "" Then
            getSicknessErr3 = ""
            Return getSicknessErr3.Trim
        Else
            Return getSicknessErr3.Trim
        End If

    End Function

    Public Function getSicknessErr4(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String

        Dim sHtlm
        Dim getEmpID As String

        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(21), "")

                getEmpID = empID(nme)

                If getEmpID.Contains("<") Or getEmpID.Contains("<") Or getEmpID.Contains("/") Or getEmpID.Contains("table") Or getEmpID.Contains("td") Or getEmpID.Contains("tr") Or getEmpID = "" Or getEmpID = Nothing Then
                    getEmpID = ""
                Else
                    empID = Split(getEmpID, "</td")
                    nme = empID.Length - 2


                    getSicknessErr4 = empID(nme)
                    getSicknessErr4 = getSicknessErr4.Trim
                End If
            End If
        Next

        If getSicknessErr4 = Nothing Or getSicknessErr4 = "" Then
            getSicknessErr4 = ""
            Return getSicknessErr4
        Else
            Return getSicknessErr4
        End If

    End Function

#End Region

#Region "Sicnkess Benefit Information"

    Public Function GetSickBenInfoDateFiled(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Date Filed")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenInfoDateFiled = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GLAD = Split(GetSickBenInfoDateFiled, "&nbsp;")
                'GetSickBenInfoDateFiled = GLAD(3)
                'GetSickBenInfoDateFiled = GLAD(138).Replace("&nbsp;", "").Replace("</td", "")
                'GetSickBenInfoDateFiled = GLAD(253).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenInfoDateFiled = GLAD(137).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenInfoDateFiled = GetSickBenInfoDateFiled.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetSickBenRemarks(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Remarks")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenRemarks = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GLAD = Split(GetSickBenRemarks, "&nbsp;")
                'GetSickBenRemarks = GLAD(3)
                GetSickBenRemarks = GLAD(227).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenRemarks = GetSickBenRemarks.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetSickBenStartDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Start Date")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenStartDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GetSickBenStartDate = GLAD(248).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenStartDate = GLAD(253).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenStartDate = GetSickBenStartDate.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetSickBenEndDate(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("End Date")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenEndDate = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GetSickBenEndDate = GLAD(255).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenEndDate = GLAD(260).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenEndDate = GetSickBenEndDate.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetSickBenConPeriod(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement Period")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenConPeriod = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GetSickBenConPeriod = GetSickBenConPeriod.Replace("&nbsp;", " ")
                'GetSickBenConPeriod = GLAD(216).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenConPeriod = GLAD(221).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenConPeriod = GetSickBenConPeriod.Split("to")(0) & " to" & vbNewLine & GetSickBenConPeriod.Split("to")(1).Replace("o", "")
                GetSickBenConPeriod = GetSickBenConPeriod.Trim
                Exit Function
            End If
        Next
    End Function

    Public Function GetSickBenAmtPaid(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount Paid")) > 0 Then
                'GLAD = Split(Tmp(i + 1), ">")
                GLAD = Split(Tmp(i), ">")
                'GetSickBenAmtPaid = Left(GLAD(1), Len(GLAD(1)) - 4)
                'GLAD = Split(GetSickBenAmtPaid, "&nbsp;")
                'GetSickBenAmtPaid = GLAD(3)
                'GetSickBenAmtPaid = GLAD(198).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenAmtPaid = GLAD(209).Replace("&nbsp;", "").Replace("</td", "")
                GetSickBenAmtPaid = GetSickBenAmtPaid.Trim
                Exit Function
            End If
        Next
    End Function

#End Region

#Region "Sickness Eligibility"
    Public Function checkSicknessEligibility(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Available Benefits")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(1), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                checkSicknessEligibility = empID(nme)
                checkSicknessEligibility = checkSicknessEligibility.Trim
                Exit Function
            End If
        Next
        Return checkSicknessEligibility
    End Function

    Public Function getConfinementStart(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement Start")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(21), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getConfinementStart = empID(nme)
                getConfinementStart = getConfinementStart.Trim
                Exit Function
            End If
        Next
        Return getConfinementStart

    End Function

    Public Function getConfinementEnd(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Confinement End")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(27), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getConfinementEnd = empID(nme)
                getConfinementEnd = getConfinementEnd.Trim
                Exit Function
            End If
        Next
        Return getConfinementEnd

    End Function

    Public Function getDailySicknessAllowance(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Daily Sickness Allowance")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(39), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getDailySicknessAllowance = empID(nme)
                getDailySicknessAllowance = getDailySicknessAllowance.Trim
                Exit Function
            End If
        Next
        Return getDailySicknessAllowance

    End Function

    Public Function getSicknessAmountBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(15), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "<")
                nme = empID.Length - 2
                getSicknessAmountBenefit = empID(nme)
                getSicknessAmountBenefit = getSicknessAmountBenefit.Trim
                Exit Function
            End If
        Next
        Return getSicknessAmountBenefit

    End Function

#End Region

#Region "Maternity Eligibility"

    Public Function getMaternityErr(ByVal webbrowserpath) As Boolean


        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getMaternityErr = False

        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                getMaternityErr = True
                Exit Function
            End If
        Next


    End Function
    Public Function getMatEligibilityErr2(ByVal webbrowserpath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)

            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then

                GLAD = Split(Tmp(i), "")

                rchtextbox.Text = (Regex.Replace(GLAD(0), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.WordWrap = True
        Return rchbx.Text


    End Function


    Public Function getMAtTypeBenefit(ByVal webbrowserpath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(9), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMAtTypeBenefit = empID(nme)
                getMAtTypeBenefit = getMAtTypeBenefit.Trim
                Exit Function
            End If
        Next
        Return getMAtTypeBenefit
    End Function
    Public Function getMatAmtBen(ByVal webbrowserpath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(15), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMatAmtBen = empID(nme)
                getMatAmtBen = getMatAmtBen.Trim
                Exit Function
            End If
        Next
        Return getMatAmtBen
    End Function
    Public Function getMatDaysClaims(ByVal webbrowserpath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Number of Days Claimed")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(21), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMatDaysClaims = empID(nme)
                getMatDaysClaims = getMatDaysClaims.Trim
                Exit Function
            End If
        Next
        Return getMatDaysClaims
    End Function
    Public Function getMatAllowance(ByVal webbrowserpath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webbrowserpath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Daily Maternity Allowance")) > 0 Then
                empID = Split(Tmp(i), ">")
                empID = Split(empID(27), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getMatAllowance = empID(nme)
                getMatAllowance = getMatAllowance.Trim
                Exit Function
            End If
        Next
        Return getMatAllowance
    End Function




#End Region

#Region "Maternity"

    Public Function checKButtonMat(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        checKButtonMat = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Claim No.")) > 0 Then
                checKButtonMat = True
                Exit Function
            End If
        Next

    End Function

    'MATERNITY FOR MEDICAL EVALUATION,Illness Code :
    Public Function checkIfMaternityForMedicalEvalutionWithIllnessCode(ByVal webBrowserPath As WebBrowser) As Boolean
        If webBrowserPath.DocumentText.Contains("MATERNITY FOR MEDICAL EVALUATION") And webBrowserPath.DocumentText.Contains("Illness Code") Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getMsgMatBen(ByVal webBrowserPath) As Boolean
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer

        getMsgMatBen = False

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Member has no maternity claim filed as of date")) > 0 Then
                getMsgMatBen = True
                Exit Function
            End If
        Next

    End Function

    Public Function getClaimNo(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Claim No.")) > 0 Then

                empID = Split(Tmp(i), ">")

                empID = Split(empID(18), "Claim No.")

                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</A")
                nme = empID.Length - 2
                getClaimNo = empID(nme)
                getClaimNo = getClaimNo.Trim
                Exit Function
            End If
        Next

        Return getClaimNo
    End Function

    Public Function getClaimNo_v2(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            'If InStr(1, LCase(Tmp(i)), LCase("Claim No.")) > 0 Then
            If InStr(1, LCase(Tmp(i)), LCase("Batch No - Item No. :")) > 0 Then
                empID = Split(Tmp(i), ">")

                getClaimNo_v2 = empID(40).ToUpper.Replace("</td", "").Trim
                Exit Function
            End If
        Next

        Return getClaimNo_v2
    End Function

    'created by edel 08/23/2018 due to changes in sss page
    Public Function getMaternityBenefitVariables(ByVal webBrowserPath As WebBrowser, ByRef ClaimNo As String, ByRef DeliveryDate As String, ByRef Days As String, ByRef AmountPaid As String, ByRef StatusCHECKNo As String, ByRef StatusDate As String) As Boolean
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            'If InStr(1, LCase(Tmp(i)), LCase("Claim No.")) > 0 Then
            If InStr(1, LCase(Tmp(i)), LCase("Batch No - Item No.:")) > 0 Then
                empID = Split(Tmp(i), ">")

                ClaimNo = empID(41).ToUpper.Replace("</TD", "").Trim
                DeliveryDate = empID(77).ToUpper.Replace("</TD", "").Trim
                'Days = empID(59).Substring(empID(59).IndexOf("days") + 5).Replace("value=", "").Replace("""", "").Trim
                Days = empID(59).Split("=")(1).Replace("&nbsp", "").ToUpper.Replace("</TD", "").Replace("DAYS", "").Trim & " days"


                AmountPaid = empID(107).ToUpper.Replace("</TD", "").Trim

                Dim chkNo As String = empID(107).ToUpper.Replace("</TD", "").Trim
                'StatusCHECKNo = empID(95).ToUpper.Replace("</TD", "").Trim & IIf(chkNo = "", "", " / " & chkNo)
                StatusCHECKNo = empID(95).ToUpper.Replace("</TD", "").Trim

                'Dim settleDate As String = "" 'empID(95).ToUpper.Replace("</TD", "").Trim

                'StatusDate = IIf(settleDate = "NULL", "", settleDate)
                Return True
            End If
        Next

        Return False
    End Function

    Public Function getdelivDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Delivery Date")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(21), "Delivery Date")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getdelivDate = empID(nme)
                getdelivDate = getdelivDate.Trim
                Exit Function
            End If
        Next

        Return getdelivDate
    End Function

    Public Function getdays(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Days")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(23), "Days")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getdays = empID(nme)
                getdays = getdays.Trim
                Exit Function
            End If
        Next

        Return getdays
    End Function

    Public Function getAmountPaidMat(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount Paid")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(25), "Amount Paid")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getAmountPaidMat = empID(nme)
                getAmountPaidMat = getAmountPaidMat.Trim
                Exit Function
            End If
        Next

        Return getAmountPaidMat
    End Function

    Public Function getstatChkNo(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Status/CHK No.")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(27), "Status/CHK No.")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getstatChkNo = empID(nme)
                getstatChkNo = getstatChkNo.Trim
                Exit Function
            End If
        Next

        Return getstatChkNo
    End Function

    Public Function getstatDate(ByVal webBrowserPath) As String
        'On Error GoTo errHdlr
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Status Date")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(29), "Status Date")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getstatDate = empID(nme)
                Exit Function
            End If
        Next

        Return getstatDate
    End Function


#End Region

#Region "DDR ELIGIBILITY"

    Public Function getTechincalRetirement(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("List of Available Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(43), "List of Available Benefit")
                'nme = empID.Length 
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp;</td")
                If empID.Length > 2 Then nme = empID.Length - 2 Else nme = empID.Length - 1
                getTechincalRetirement = empID(nme)
                getTechincalRetirement = getTechincalRetirement.Trim
                Exit Function
            End If
        Next

        Return getTechincalRetirement
    End Function

    Public Function getTechincalRetirement2(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getTechincalRetirement2 = Left(GLAD(1), Len(GLAD(1)) - 4)
                getTechincalRetirement2 = getTechincalRetirement2.Trim
                Exit Function
            End If
        Next

    End Function

    Public Function getTechincalRetirement3(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getTechincalRetirement3 = Left(GLAD(1), Len(GLAD(1)) - 4)
                getTechincalRetirement3 = getTechincalRetirement3.Trim
                Exit Function
            End If
        Next

    End Function

    Public Function getContributionRetirement(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        Dim haha As Integer = 1
        sHtlm = webBrowserPath.Document.body.outerHtml
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Total Number of Posted Contributions")) > 0 Then
                GLAD = Split(Tmp(i + 1), ">")
                getContributionRetirement = Left(GLAD(1), Len(GLAD(1)) - 4)
                getContributionRetirement = getContributionRetirement.Trim
                getContributionRetirement = getContributionRetirement.Replace("&nbsp;", "")
                Exit Function
            End If
        Next

    End Function
#End Region

#Region "BENEFITS ELIGIBILITY"

    Public Function getBenefitChoicesDeath(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), UCase("Death Benefit Eligibility")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(108), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</b")
                nme = empID.Length - 2
                getBenefitChoicesDeath = empID(nme)
                getBenefitChoicesDeath = getBenefitChoicesDeath.Trim
                Exit Function
            End If
        Next
        Return getBenefitChoicesDeath
    End Function

    Public Function getSSFuneral(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(37), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSSFuneral = empID(nme)
                getSSFuneral = getSSFuneral.Trim
                Exit Function
            End If
        Next
        Return getSSFuneral
    End Function

    Public Function getDisability(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("tableDataMid")) > 0 Then
                empID = Split(Tmp(i), "</td>")

                empID = Split(empID(36), "tableDataMid")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, ">")
                nme = empID.Length - 1
                getDisability = empID(nme)
                getDisability = getDisability.Trim
                Exit Function
            End If
        Next
        Return getDisability
    End Function

#End Region

#Region "SS FUNERAL"

    Public Function getTypeOfBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(134), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTypeOfBenefit = empID(nme)
                getTypeOfBenefit = getTypeOfBenefit.Trim
                Exit Function
            End If
        Next
        Return getTypeOfBenefit
    End Function

    Public Function getAmtBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(43), "")
                nme = empID.Length - 1
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getAmtBenefit = empID(nme)
                getAmtBenefit = getAmtBenefit.Trim
                Exit Function
            End If
        Next
        Return getAmtBenefit
    End Function

    Public Function getSSFuneralErr(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSSFuneralErr = empID(nme)
                getSSFuneralErr = getSSFuneralErr.Trim

                Exit Function
            End If
        Next
        Return getSSFuneralErr
    End Function

    Public Function getSSFuneralErr2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(13), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSSFuneralErr2 = empID(nme)
                getSSFuneralErr2 = getSSFuneralErr2.Trim

                Exit Function
            End If
        Next
        Return getSSFuneralErr2
    End Function

    Public Function getSSFuneralErr3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(17), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getSSFuneralErr3 = empID(nme)
                getSSFuneralErr3 = getSSFuneralErr3.Trim

                Exit Function
            End If
        Next
        Return getSSFuneralErr3
    End Function

#End Region

#Region "PARTIAL OR DISABILITY ELIGIBILITY"

    Public Function getDisabilityAMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim str1(5) As String
        Dim textxx As String
        Dim rchbx As New RichTextBox
        For i = LBound(Tmp) To UBound(Tmp)
            Dim ctr As Integer
            str1(0) = "Average Monthly Salary Credit : "

            If InStr(1, LCase(Tmp(i)), LCase("Disability Eligibility")) > 0 Then

                GLAD = Split(Tmp(i), "Average Monthly Salary Credit")

                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})

                For p = 0 To _split.Length - 1
                    If _split(p).Trim = "" Then
                    Else
                        ctr = ctr + 1
                        If ctr >= str1.Length Then
                            ReDim Preserve str1(ctr)
                        End If
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p)
                        End If
                        'rchbx.Text = rchbx.Text & vbNewLine & _split(p)
                    End If
                Next

            End If

        Next
        rchbx.Text = str1(0) & str1(1) & vbNewLine & str1(2).Trim & " : " & str1(3) & vbNewLine & str1(4).Replace("(A) ", "").Replace("(A)", "").Trim & " : " & str1(5)


        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

    Public Function getMinAmtOfBenefit(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Minimum")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(142), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 3
                getMinAmtOfBenefit = empID(nme)
                getMinAmtOfBenefit = getMinAmtOfBenefit.Trim
                Exit Function
            End If
        Next
        Return getMinAmtOfBenefit
    End Function

    Public Function getTypeofBenefitAMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(134), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                getTypeofBenefitAMSC = empID(nme)
                getTypeofBenefitAMSC = getTypeofBenefitAMSC.Trim
                Exit Function
            End If
        Next
        Return getTypeofBenefitAMSC
    End Function

#End Region

#Region "PARTIAL DISABILITY - PENSION"

    Public Function disabilityEligiblErr(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                disabilityEligiblErr = empID(nme)
                disabilityEligiblErr = disabilityEligiblErr.Trim

                Exit Function
            End If
        Next
        Return disabilityEligiblErr
    End Function

    Public Function disabilityEligiblErr2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                disabilityEligiblErr2 = empID(nme)
                disabilityEligiblErr2 = disabilityEligiblErr2.Trim

                Exit Function
            End If
        Next
        Return disabilityEligiblErr2
    End Function

    Public Function disabilityEligiblErr3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                disabilityEligiblErr3 = empID(nme)
                disabilityEligiblErr3 = disabilityEligiblErr3.Trim

                Exit Function
            End If
        Next
        Return disabilityEligiblErr3
    End Function

    Public Function disabilityEligibleAMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Average Monthly Salary Credit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(116), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 3
                disabilityEligibleAMSC = empID(nme)
                disabilityEligibleAMSC = disabilityEligibleAMSC.Trim
                Exit Function
            End If
        Next
        Return disabilityEligibleAMSC
    End Function

    Public Function disabilityEligibleTOB(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(124), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                disabilityEligibleTOB = empID(nme)
                disabilityEligibleTOB = disabilityEligibleTOB.Trim
                Exit Function
            End If
        Next
        Return disabilityEligibleTOB
    End Function

    Public Function disabilityEligibleMAB(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Minimum&nbsp Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(132), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp")
                nme = empID.Length - 3
                disabilityEligibleMAB = empID(nme)
                disabilityEligibleMAB = disabilityEligibleMAB.Trim
                Exit Function
            End If
        Next
        Return disabilityEligibleMAB
    End Function

#End Region

#Region "TOTAL DISABILITY - PENSION"

    Public Function totalDisabilityErr(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                totalDisabilityErr = empID(nme)
                totalDisabilityErr = totalDisabilityErr.Trim

                Exit Function
            End If
        Next
        Return totalDisabilityErr
    End Function

    Public Function totalDisabilityErr2(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                totalDisabilityErr2 = empID(nme)
                totalDisabilityErr2 = totalDisabilityErr2.Trim

                Exit Function
            End If
        Next
        Return totalDisabilityErr2
    End Function

    Public Function totalDisabilityErr3(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("rejected")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(8), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                totalDisabilityErr3 = empID(nme)
                totalDisabilityErr3 = totalDisabilityErr3.Trim

                Exit Function
            End If
        Next
        Return totalDisabilityErr3
    End Function

    Public Function TotaldisabilityAMSC(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Average Monthly Salary Credit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(116), "tableDataLite")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "&nbsp &nbsp </td")
                nme = empID.Length - empID.Length
                TotaldisabilityAMSC = empID(nme)
                TotaldisabilityAMSC = TotaldisabilityAMSC.Trim
                Exit Function
            End If
        Next
        Return TotaldisabilityAMSC
    End Function

    Public Function TotaldisabilityTOB(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("Type of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(124), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                TotaldisabilityTOB = empID(nme)
                TotaldisabilityTOB = TotaldisabilityTOB.Trim
                Exit Function
            End If
        Next
        Return TotaldisabilityTOB
    End Function

    Public Function TotaldisabilityAOB(ByVal webBrowserPath) As String
        Dim Tmp() As String
        Dim empID() As String
        Dim sHtlm
        Dim getEmpID As String
        Dim i As Integer
        Dim nme As Integer

        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("&nbsp Amount of Benefit")) > 0 Then
                empID = Split(Tmp(i), ">")

                empID = Split(empID(132), "")
                getEmpID = empID(nme)
                empID = Split(getEmpID, "</td")
                nme = empID.Length - 2
                TotaldisabilityAOB = empID(nme)
                TotaldisabilityAOB = TotaldisabilityAOB.Trim
                Exit Function
            End If
        Next
        Return TotaldisabilityAOB
    End Function

#End Region

#Region "RETIREMENT ELIGIBILITY"

    Public Function retirementEligibiltyAMSC(ByVal webBrowserPath) As String

        Dim Tmp() As String
        Dim sHtlm
        Dim i As Integer
        Dim GLAD() As String
        sHtlm = webBrowserPath.Documenttext
        Tmp = Split(sHtlm, Chr(13))
        Dim textxx As String
        Dim str1(6) As String
        Dim rchbx As New RichTextBox
        Dim ctr As Integer
        ctr = Nothing

        For i = LBound(Tmp) To UBound(Tmp)
            '   Dim matchpattern As String = "<(?:[^>=]|='[^']*'|=""[^""]*""|=[^'""][^\s>]*)*>  <(?:[^>=]|='[^']*'|=""[^""]*""|=[^'""][^\s>]*)*>"
            str1(0) = "Average Monthly Salary Credit : "
            If InStr(1, LCase(Tmp(i)), LCase("Retirement Eligibility")) > 0 Then
                GLAD = Split(Tmp(i), "Average Monthly Salary Credit")

                rchtextbox.Text = (Regex.Replace(GLAD(1), matchpattern, replacementstring, RegexOptions.IgnoreCase Or RegexOptions.IgnorePatternWhitespace Or RegexOptions.Multiline Or RegexOptions.Singleline))
                rchtextbox.WordWrap = True
                Dim _split As String() = rchtextbox.Text.Split(New Char() {"|"c})
                For p = 0 To _split.Length - 1
                    '  rchbx.Text = Nothing
                    If _split(p).Trim = "" Then
                    Else
                        ctr = ctr + 1
                        If str1(ctr) = Nothing Then
                            str1(ctr) = _split(p)
                        End If
                    End If
                Next
            End If

        Next
        rchbx.Text = str1(0) & str1(1) & vbNewLine & str1(2).Trim & " : " & str1(3) & vbNewLine & str1(4).Trim & " : " & str1(5)

        rchbx.WordWrap = True
        Return rchbx.Text
    End Function

#End Region
End Class
