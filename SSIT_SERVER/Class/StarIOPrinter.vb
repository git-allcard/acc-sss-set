
Public Class StarIOPrinter

    Public Shared Function CheckPrinterAvailabilityv2() As Boolean
        'new printer have no sdk
        If Not DeviceConnectivity.IsPrinterOnline Then
            Dim resultRegis As Integer = MessageBox.Show("THIS TERMINAL IS UNABLE TO PRINT THE RECEIPT DUE TO THE FOLLOWING REASON/S. " & vbNewLine & "PRINTER IS OFFLINE OR NOT CONNECTED." & vbNewLine & vbNewLine & "DO YOU WANT TO PROCEED?" & vbNewLine, "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
            If resultRegis = DialogResult.No Then
                Return False
            ElseIf resultRegis = DialogResult.Yes Then
                Return True
            End If
        Else
            Return True
        End If

    End Function


End Class
