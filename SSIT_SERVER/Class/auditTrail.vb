Public Class auditTrail
    Dim db As New ConnectionString
    Dim autoGen As String
    Public Function getModuleLogs(ByVal ssNumber As String, ByVal affectedTable As String, ByVal tagModule As String, ByVal transDate As String _
                                  , ByVal transTime As String, ByVal BRANCH_IP As String, ByVal BRANCH_CODE As String, ByVal CLUSTER As String _
                                  , ByVal DIVISION As String, ByVal MEMBER_STATUS As String, ByVal countPrint As Integer, ByVal TRANS_NUM As String _
                                  , ByVal DESC As String)

        Try
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & BRANCH_CODE & "'")
            Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & CLUSTER & "'")
            Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & DIVISION & "'")

            'If db.checkExistence("select autogenID, tagModule from SSTRANSAT where autogenID ='" & autoGen & "' and tagModule = '" & tagModule & "' ") = True Then
            '    db.ExecuteSQLQuery("update SSTRANSAT set Task = '" & task & "', transDate = '" & transDate & "', transTime = '" & transTime & "' where autogenID = '" & autoGen & "' ")
            'Else
            db.sql = "Insert into SSTRANSAT (SSNUM,PROC_CD,TAG_MOD,ENCODE_DT,ENCODE_TME,COUNT,BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,MEMSTAT,CNT_PRNT,TRANSNUM,TRANS_DESC)values('" _
                & ssNumber & "','" & affectedTable & "','" & tagModule & "','" & transDate & "','" & transTime & "', '" & "1" & "','" & BRANCH_IP & _
                "', '" & getbranchCoDE & "','" & getkiosk_cluster & "','" & getkiosk_group & "','" & MEMBER_STATUS & "','" & countPrint & "', '" & TRANS_NUM & "', '" & DESC & "')"
            db.ExecuteSQLQuery(db.sql)
            'End If

        Catch ex As Exception
            Dim errorLogs As String = ex.ToString
            errorLogs = errorLogs.Trim

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getdate As String = Date.Today.ToString("ddMMyyyy")
            'Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "logs\" & getdate & " " & getbranchCoDE & "\" & "Error Logs.txt", True)
            '    SW.WriteLine("AT CLASS" & "," & "Error: " & errorLogs & "," & kioskIP & "," & kioskID & "," & kioskName & "," & kioskBranch & "," & "DATESTAMP : " & Date.Now.ToString("MM/dd/yyyy hh:mm:ss tt") & vbNewLine)
            'End Using
            'db.sql = "insert into SSINFOTERMERRLOGS values('" & kioskIP & "', '" & kioskID & "', '" & kioskName & "', '" & kioskBranch & "', '" & errorLogs _
            '    & "','" & "Class: auditTrail" & "', '" & "Inserting logs in audit trail table error" & "', '" & Date.Today.ToShortDateString & "', '" & TimeOfDay & "') "
            'db.ExecuteSQLQuery(db.sql)
            Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(kioskIP & "|" & kioskID & "|" & kioskName & "|" & kioskBranch & "|" & errorLogs _
                   & "|" & "Class: auditTrail" & "|" & "Inserting logs in audit trail table error" & "|" & Date.Today.ToShortDateString & "|" & TimeOfDay & vbNewLine)
            End Using
        End Try

    End Function

    Public Function getHREF(ByVal webBrowserPath) As String
        Dim sHtlm

        Dim Tmp() As String
        Dim LoadLStatus() As String
        Dim getLStatus As String
        Dim nme As Integer
        Dim APremiumPath As String


        sHtlm = webBrowserPath.DocumentText
        Tmp = Split(sHtlm, Chr(13))
        For i = LBound(Tmp) To UBound(Tmp)
            If InStr(1, LCase(Tmp(i)), LCase("LoanDetails")) > 0 Then
                LoadLStatus = Split(Tmp(i), ">")

                LoadLStatus = Split(LoadLStatus(25), "Loan Type")
                nme = LoadLStatus.Length - 1
                getLStatus = LoadLStatus(nme)
                LoadLStatus = Split(getLStatus, "HREF=")
                LoadLStatus = Split(getLStatus, """")
                nme = LoadLStatus.Length - 2
                APremiumPath = LoadLStatus(nme)
                webBrowserPath.Navigate(getPermanentURL & APremiumPath)

            End If
        Next
        Return getHREF
    End Function

    'Public Function getAutoID()
    '    Dim getGen As String = db.putSingleNumber("select autogenID from SSTRANSAT")
    '    If getGen = 0 Or getGen = "" Then
    '        autoGen = My.Settings.firstGen
    '        My.Settings.autoGenID = autoGen
    '    Else
    '        autoGen = db.GenRecID(getGen)
    '        My.Settings.autoGenID = autoGen
    '    End If

    '    Return autoGen
    'End Function

End Class
