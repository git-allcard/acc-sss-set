Imports System.Runtime.InteropServices
Module modClear_Cache
    Sub Main()
        Try
            'Indicates that all of the cache groups in the user's system should be enumerated
            Const CACHEGROUP_SEARCH_ALL = &H0
            'Indicates that all of the cache entries that are associated with the cache group should be deleted,
            'unless the entry belongs to another cache group.
            Const CACHEGROUP_FLAG_FLUSHURL_ONDELETE = &H2
            'File not found.
            Const ERROR_FILE_NOT_FOUND = &H2
            'No more items have been found.
            Const ERROR_NO_MORE_ITEMS = 259
            'Pointer to a GROUPID variable
            Dim groupId As Long = 0

            'Local variables
            Dim cacheEntryInfoBufferSizeInitial As Integer = 0
            Dim cacheEntryInfoBufferSize As Integer = 0
            Dim cacheEntryInfoBuffer As IntPtr = IntPtr.Zero
            Dim internetCacheEntry As ClearCache.INTERNET_CACHE_ENTRY_INFOA
            Dim enumHandle As IntPtr = IntPtr.Zero
            Dim returnValue As Boolean = False

            'Delete the groups first.
            'Groups may not always exist on the system.
            'For more information, visit the following Microsoft Web site: 
            'http://msdn.microsoft.com/library/?url=/workshop/networking/wininet/overview/cache.asp
            'By default, a URL does not belong to any group. Therefore, that cache may become
            'empty even when CacheGroup APIs are not used because the existing URL does not belong to any group.     

            enumHandle = ClearCache.FindFirstUrlCacheGroup(0, CACHEGROUP_SEARCH_ALL, IntPtr.Zero, 0, groupId, IntPtr.Zero)
            'If there are no items in the Cache, you are finished.
            If (Not enumHandle.Equals(IntPtr.Zero) And ERROR_NO_MORE_ITEMS.Equals(Marshal.GetLastWin32Error)) Then
                Exit Sub
            End If

            'Loop through Cache Group, and then delete entries.
            While (True)
                'Delete a particular Cache Group.
                returnValue = ClearCache.DeleteUrlCacheGroup(groupId, CACHEGROUP_FLAG_FLUSHURL_ONDELETE, IntPtr.Zero)

                If (Not returnValue And ERROR_FILE_NOT_FOUND.Equals(Marshal.GetLastWin32Error())) Then
                    returnValue = ClearCache.FindNextUrlCacheGroup(enumHandle, groupId, IntPtr.Zero)
                End If

                If (Not returnValue And (ERROR_NO_MORE_ITEMS.Equals(Marshal.GetLastWin32Error()) Or ERROR_FILE_NOT_FOUND.Equals(Marshal.GetLastWin32Error()))) Then
                    Exit While
                End If
            End While
            'Start to delete URLs that do not belong to any group.
            enumHandle = ClearCache.FindFirstUrlCacheEntry(vbNull, IntPtr.Zero, cacheEntryInfoBufferSizeInitial)

            If (enumHandle.Equals(IntPtr.Zero) And ERROR_NO_MORE_ITEMS.Equals(Marshal.GetLastWin32Error())) Then
                Exit Sub
            End If

            cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial
            cacheEntryInfoBuffer = Marshal.AllocHGlobal(cacheEntryInfoBufferSize)
            enumHandle = ClearCache.FindFirstUrlCacheEntry(vbNull, cacheEntryInfoBuffer, cacheEntryInfoBufferSizeInitial)

            While (True)
                internetCacheEntry = CType(Marshal.PtrToStructure(cacheEntryInfoBuffer, GetType(ClearCache.INTERNET_CACHE_ENTRY_INFOA)), ClearCache.INTERNET_CACHE_ENTRY_INFOA)
                cacheEntryInfoBufferSizeInitial = cacheEntryInfoBufferSize
                returnValue = ClearCache.DeleteUrlCacheEntry(internetCacheEntry.lpszSourceUrlName)

                If (Not returnValue) Then
                    'Console.WriteLine("Error Deleting: {0}", Marshal.GetLastWin32Error())
                End If

                returnValue = ClearCache.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, cacheEntryInfoBufferSizeInitial)
                If (Not returnValue And ERROR_NO_MORE_ITEMS.Equals(Marshal.GetLastWin32Error())) Then
                    Exit While
                End If

                If (Not returnValue And cacheEntryInfoBufferSizeInitial > cacheEntryInfoBufferSize) Then

                    cacheEntryInfoBufferSize = cacheEntryInfoBufferSizeInitial
                    Dim tempIntPtr As New IntPtr(cacheEntryInfoBufferSize)
                    cacheEntryInfoBuffer = Marshal.ReAllocHGlobal(cacheEntryInfoBuffer, tempIntPtr)
                    returnValue = ClearCache.FindNextUrlCacheEntry(enumHandle, cacheEntryInfoBuffer, cacheEntryInfoBufferSizeInitial)
                End If
            End While
            Marshal.FreeHGlobal(cacheEntryInfoBuffer)

        Catch ex As Exception

        End Try
    
    End Sub


End Module
