Imports System.Runtime.InteropServices
Public Class ClearCache
    <StructLayout(LayoutKind.Explicit, Size:=80)> _
    Public Structure INTERNET_CACHE_ENTRY_INFOA
        <FieldOffset(0)> Public dwStructSize As UInt32
        <FieldOffset(4)> Public lpszSourceUrlName As IntPtr
        <FieldOffset(8)> Public lpszLocalFileName As IntPtr
        <FieldOffset(12)> Public CacheEntryType As UInt32
        <FieldOffset(16)> Public dwUseCount As UInt32
        <FieldOffset(20)> Public dwHitRate As UInt32
        <FieldOffset(24)> Public dwSizeLow As UInt32
        <FieldOffset(28)> Public dwSizeHigh As UInt32
        <FieldOffset(32)> Public LastModifiedTime As FILETIME
        <FieldOffset(40)> Public ExpireTime As FILETIME
        <FieldOffset(48)> Public LastAccessTime As FILETIME
        <FieldOffset(56)> Public LastSyncTime As FILETIME
        <FieldOffset(64)> Public lpHeaderInfo As IntPtr
        <FieldOffset(68)> Public dwHeaderInfoSize As UInt32
        <FieldOffset(72)> Public lpszFileExtension As IntPtr
        <FieldOffset(76)> Public dwReserved As UInt32
        <FieldOffset(76)> Public dwExemptDelta As UInt32
    End Structure

    'For PInvoke: Initiates the enumeration of the cache groups in the Internet cache
    <DllImport("wininet.dll", SetLastError:=True, _
       CharSet:=CharSet.Auto, _
       EntryPoint:="FindFirstUrlCacheGroup", _
       CallingConvention:=CallingConvention.StdCall)> _
    Shared Function FindFirstUrlCacheGroup( _
        ByVal dwFlags As Int32, _
        ByVal dwFilter As Integer, _
        ByVal lpSearchCondition As IntPtr, _
        ByVal dwSearchCondition As Int32, _
        ByRef lpGroupId As Long, _
        ByVal lpReserved As IntPtr) As IntPtr
    End Function

    'For PInvoke: Retrieves the next cache group in a cache group enumeration
    <DllImport("wininet.dll", _
       SetLastError:=True, _
       CharSet:=CharSet.Auto, _
       EntryPoint:="FindNextUrlCacheGroup", _
       CallingConvention:=CallingConvention.StdCall)> _
    Shared Function FindNextUrlCacheGroup( _
        ByVal hFind As IntPtr, _
        ByRef lpGroupId As Long, _
        ByVal lpReserved As IntPtr) As Boolean
    End Function

    'For PInvoke: Releases the specified GROUPID and any associated state in the cache index file
    <DllImport("wininet.dll", _
       SetLastError:=True, _
       CharSet:=CharSet.Auto, _
       EntryPoint:="DeleteUrlCacheGroup", _
       CallingConvention:=CallingConvention.StdCall)> _
    Shared Function DeleteUrlCacheGroup( _
        ByVal GroupId As Long, _
        ByVal dwFlags As Int32, _
        ByVal lpReserved As IntPtr) As Boolean
    End Function

    'For PInvoke: Begins the enumeration of the Internet cache
    <DllImport("wininet.dll", _
        SetLastError:=True, _
        CharSet:=CharSet.Auto, _
        EntryPoint:="FindFirstUrlCacheEntryA", _
        CallingConvention:=CallingConvention.StdCall)> _
    Shared Function FindFirstUrlCacheEntry( _
    <MarshalAs(UnmanagedType.LPStr)> ByVal lpszUrlSearchPattern As String, _
         ByVal lpFirstCacheEntryInfo As IntPtr, _
         ByRef lpdwFirstCacheEntryInfoBufferSize As Int32) As IntPtr
    End Function

    'For PInvoke: Retrieves the next entry in the Internet cache
    <DllImport("wininet.dll", _
       SetLastError:=True, _
       CharSet:=CharSet.Auto, _
       EntryPoint:="FindNextUrlCacheEntryA", _
       CallingConvention:=CallingConvention.StdCall)> _
    Shared Function FindNextUrlCacheEntry( _
          ByVal hFind As IntPtr, _
          ByVal lpNextCacheEntryInfo As IntPtr, _
          ByRef lpdwNextCacheEntryInfoBufferSize As Integer) As Boolean
    End Function

    'For PInvoke: Removes the file that is associated with the source name from the cache, if the file exists
    <DllImport("wininet.dll", _
      SetLastError:=True, _
      CharSet:=CharSet.Auto, _
      EntryPoint:="DeleteUrlCacheEntryA", _
      CallingConvention:=CallingConvention.StdCall)> _
    Shared Function DeleteUrlCacheEntry( _
        ByVal lpszUrlName As IntPtr) As Boolean
    End Function

    Public Const INTERNET_OPTION_END_BROWSER_SESSION As Integer = 42

    'added by edel 03/03/2017
    <DllImport("wininet.dll", _
         SetLastError:=True, _
         CharSet:=CharSet.Auto, _
         EntryPoint:="InternetSetOption", _
         CallingConvention:=CallingConvention.StdCall)> _
    Shared Function InternetSetOption( _
          ByVal hInternet As IntPtr, _
          ByVal dwOption As Integer, _
          ByVal lpBuffer As Integer, _
          ByVal lpdwBufferLength As Integer) As Boolean
    End Function


End Class
