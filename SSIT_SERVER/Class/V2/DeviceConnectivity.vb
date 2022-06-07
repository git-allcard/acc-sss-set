Public Class DeviceConnectivity

    Public Shared Function CheckDevice(ByVal deviceName As String, ByVal deviceManufacturer As String) As Boolean
        Dim pc As String = "." 'local
        Dim wmi As Object = GetObject("winmgmts:\\" & pc & "\root\cimv2")
        Dim devices As Object = wmi.ExecQuery(String.Format("Select * from Win32_PnPEntity WHERE Name='{0}' And Manufacturer='{1}'", deviceName, deviceManufacturer))
        For Each d As Object In devices
            Return True
        Next

        Return False
    End Function

    Public Shared Function IsPrinterOnline() As Boolean
        Dim cntr As Short = 0

        If CheckDevice("No Printer Attached", "Microsoft") Then cntr += 1

        If cntr > 0 Then Return True Else Return False
    End Function

    Public Shared Function IsFingerprintScannerPresent() As Boolean
        Dim cntr As Short = 0
        If CheckDevice("Futronic Fingerprint Scanner 2.0", "Futronic Technology Company Ltd.") Then cntr += 1
        If cntr > 0 Then Return True Else Return False
    End Function

    Public Shared Function IsCardReaderPresent() As Boolean
        Dim cntr As Short = 0

        If CheckDevice("Microsoft Usbccid Smartcard Reader (WUDF)", "Microsoft") Then cntr += 1
        If CheckDevice("HID Global OMNIKEY 5422CL Smartcard Reader 0", "Microsoft") Then cntr += 1
        If CheckDevice("OMNIKEY 5x21", "HID Global") Then cntr += 1

        If cntr > 0 Then Return True Else Return False
    End Function

End Class
