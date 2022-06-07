Imports System.IO
Imports System.Drawing.Printing

Public Class AdjustMode


    Public Function Margins(ByVal B As Integer, ByVal T As Integer, ByVal L As Integer, ByVal R As Integer)

        Dim MF As New PrinterSettings

        MF.DefaultPageSettings.Margins.Bottom = B.ToString
        MF.DefaultPageSettings.Margins.Top = T.ToString
        MF.DefaultPageSettings.Margins.Left = L.ToString
        MF.DefaultPageSettings.Margins.Right = R.ToString



        Return MF
    End Function
    Public Function PrintPaper(ByVal Pwidth As String, ByVal Pheight As String)

        Dim papersizes As New Printing.PaperSize

        papersizes.Width = Pwidth.ToString
        papersizes.Height = Pheight.ToString


        Return papersizes


    End Function

End Class
