
Imports System.Runtime.InteropServices

Public Class UMIDSAM_QC

    Private Const _dll As String = "UMIDSAM.dll"

    <DllImport(_dll)> _
    Public Shared Function SmartReader_Connect_Debug(ByVal iUMID As Integer, ByVal iSAM As Integer, ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_Connect(ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDSAM_Connect(ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Sub UMIDCard_DisConnect()
    End Sub

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_Autn_QC(ByVal ErrorMessage() As Byte) As Boolean
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_Read_CardCreationDate_QC(ByVal DataRead As Byte(), ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_Read_CRN_QC(ByVal Data() As Byte, ByVal ErrorMessage() As Byte) As Boolean
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_Change_PIN(ByVal UserPin As Byte(), ByVal iUserPin As Integer, ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_SectorKeyAuth(ByVal KeyType As Integer, ByVal KeyID As Integer, ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

    <DllImport(_dll)> _
    Public Shared Function UMIDCard_WriteSectorData(ByVal SectorID As Integer, ByVal Offset As Integer, ByVal DateLen As Integer, ByVal SectorData As Byte(), ByVal ErrorMessage As Byte()) As [Boolean]
    End Function

End Class
