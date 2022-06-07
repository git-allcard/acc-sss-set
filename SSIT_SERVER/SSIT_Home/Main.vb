
#Region " Imports "

Imports System
Imports Sagem.MorphoKit
Imports System.Runtime.InteropServices

#End Region

Public Class Main
    'Dim xs As New MySettings

    Private Sub Main_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'DisableWindowsFunction(False)
    End Sub

    Private Sub Main_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Init()
    End Sub

    Public Sub Init()
        Try
            editSettings(xml_Filename, xml_path, "CRN", "")

            '********* UNCOMMENT FOR OLD SSCARD NEW CONFIGURATION
            'If SharedFunction.IsProgramRunning("old_sss_decrypt.exe") = 0 Then
            '    SharedFunction.OpenOldSSSDecrypt()
            'End If

            '*********END OF UNCOMMENT FOR OLD SSCARD NEW CONFIGURATION

            SharedFunction.ShowMainNewUserForm(New usrfrmIdle)
        Catch ex As Exception

        End Try

    End Sub


End Class
