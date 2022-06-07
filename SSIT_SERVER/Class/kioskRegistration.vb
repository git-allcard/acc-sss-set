Public Class kioskRegistration
    Dim ip As String
    Dim pcname As String
    Public Function runFirstTime()
        ip = GetIP()
        ip = ip.Trim
        pcname = GetPcName()
        pcname = pcname.Trim
        runFirstTime = ip & " " & pcname

        Return runFirstTime

    End Function

    Public Function GetIP() As String
        Dim strIPAddress As String = System.Net.Dns.GetHostByName(System.Net.Dns.GetHostName).AddressList(0).ToString()
        Return strIPAddress
    End Function

    Public Function GetPcName() As String
        Dim strHostName As String = System.Net.Dns.GetHostName()
        Return strHostName
    End Function

    Public Function GetSerial() As String

        Dim oMotherB As Object = Nothing
        Dim sMBsernumb As String = Nothing
        Dim oWMG As Object = GetObject("WinMgmts:")

        oMotherB = oWMG.InstancesOf("Win32_BaseBoard")

        For Each obj As Object In oMotherB

            sMBsernumb += obj.SerialNumber

        Next
        Return sMBsernumb
    End Function
   
End Class
