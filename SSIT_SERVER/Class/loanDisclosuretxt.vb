
Imports System
Imports System.IO
Imports System.Text
Imports System.Collections.Generic
Imports System.Text.RegularExpressions
Imports System.Net
Imports System.Web
Public Class loanDisclosuretxt
    Dim webText
    Dim richtextbox2 As New RichTextBox
    Dim richtextbox1 As New RichTextBox
    Dim richtextbox3 As New RichTextBox
    Dim webtext1
    Dim str1, str1_1, str22, str2_2, str3, str3_3, str4, str4_4, str5, str5_5, str6, str6_6, str7, str7_7, str01, allstrings, allstrings1 As String
    Dim cnt As Integer
    Dim str2 As String

    Public Function getLoanDisclosure()
        Try
            Dim htmlTag

            htmlTag = _frmSalaryLoanDisclosurev2.WebBrowser1.DocumentText

            getLoanDisclosure = htmlTag
        Catch ex As Exception

        End Try



    End Function
End Class
