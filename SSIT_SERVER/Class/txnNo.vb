Public Class txnNo
    Dim db As New ConnectionString
    Public Function GETTXN(ByVal BRANCHCDE As String, ByVal CTR As String, ByVal TRANSTYP As String _
                           , ByVal TRANSDATE As String, ByVal SEQNUM As String)

        db.sql = "Insert into SSTRANSCTRREFNO (CTR) values('" & CTR & "')"
        db.ExecuteSQLQuery(db.sql)

        db.sql = "Insert into SSTRANSREFNO (BRANCH_CD,BRANCH_NAME,CTR,ENCODE_DT,ENCODE_TME,SEQUENCE_NUM,TRANSTYP) values('" _
             & BRANCHCDE & "','" & kioskName & "', '" & CTR & "', '" & TRANSDATE & "', '" & TimeOfDay & "','" & SEQNUM & "','" & TRANSTYP & "')"
        db.ExecuteSQLQuery(db.sql)
        'End If
        'CTR = CTR.Substring(1, 3)
        GETTXN = BRANCHCDE + kioskID + TRANSTYP + TRANSDATE + SEQNUM

        Return GETTXN
    End Function


    Public Function getnum(ByVal dToday As String, ByVal transType As String)
        Try
            Dim getTransNo As String
            Dim txt1 As String
            Dim txt2
            Dim getTxtDate As String
            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Using SW As New IO.StreamReader(Application.StartupPath & "\" & "REF_NUM\" & "\" & "REF_NUM.txt", True)
                txt1 = SW.ReadToEnd
            End Using
            txt1 = txt1.Trim
            If txt1 = "" Or txt1 = Nothing Then
                ' getTxtDate = Nothing
            Else

                txt2 = txt1.Split(New Char() {"|"c})
                getTransNo = txt2(0)
                getTxtDate = txt2(1)
            End If
         
            If getTransNo = "" Or getTransNo = Nothing And getTxtDate = Nothing Then
                getTransNo = "0001"
                Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "Ref_Num\" & "\" & "REF_NUM.txt", False)
                    SW.WriteLine(getTransNo & "|" & Date.Today)
                End Using
            Else

                getTransNo = getTransNo.PadLeft(4, "0") + 1
                getTransNo = getTransNo.PadLeft(4, "0")

                Using SW As New IO.StreamWriter(Application.StartupPath & "\" & "Ref_Num\" & "\" & "REF_NUM.txt", False)
                    SW.WriteLine(getTransNo & "|" & Date.Today)
                End Using
            End If


            getnum = getbranchCoDE & kioskID & transType & dToday & getTransNo
            Return getnum

        Catch ex As Exception

        End Try
    
    End Function

End Class
