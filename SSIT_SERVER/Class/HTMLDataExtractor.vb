Public Class HTMLDataExtractor

    Public Shared Function getCRN(ByVal webBrowserPath) As String
        Dim sHtlm = webBrowserPath.Documenttext

        Try
            Dim varPosition As Integer = sHtlm.IndexOf("crn_val")
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(1).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Replace("-", "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function getSSSNum(ByVal webBrowserPath) As String
        Dim sHtlm = webBrowserPath.Documenttext

        Try
            Dim varPosition As Integer = sHtlm.IndexOf("ssnum_val")
            Dim arr1() As String = sHtlm.Substring(varPosition, 200).Split(">")

            Return arr1(1).Replace("</td", "").Replace("&nbsp", "").Replace(";", "").Replace("-", "")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function GetDateBith() As String
        Try
            Dim printF As New printModule
            Dim dob As String = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
            printF = Nothing
            Return dob
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Shared Function MemberFullName() As String
        Try
            Dim printF As New printModule
            Dim fname As String = printF.GetFirstName(_frmWebBrowser.WebBrowser1)
            Dim mname As String = printF.GetMiddleName(_frmWebBrowser.WebBrowser1)
            Dim lname As String = printF.GetLastName(_frmWebBrowser.WebBrowser1).Replace(",", "")
            'Dim fullnameprint As String = lname & " " & fname & " " & mname
            printF = Nothing
            Return String.Format("{0}{1} {2}", fname, IIf(mname = "", "", " " & mname), lname)
        Catch ex As Exception
            Return ""
        End Try

    End Function

    Public Shared Function IsRegisteredInSSSWebsite() As Boolean
        Dim printF As New printModule
        Dim bln As Boolean = printF.IsRegisteredInSSSWebsite(_frmWebBrowser.WebBrowser1)
        printF = Nothing
        Return bln
    End Function

End Class
