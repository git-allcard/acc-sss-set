Public Class SharedFunction

    'Public Shared SMTP_USER As String = "noreply@sss.gov.ph"
    Public Shared SMTP_USER As String = "notifications@sss.gov.ph"
    'Public Shared SMTP_USER As String = "noreply"

    'Public Shared SMTP_PASS As String = "sss_sdd"
    Public Shared SMTP_PASS As String = ""

    'Public Shared SMTP_HOST As String = "sssemail"
    Public Shared SMTP_HOST As String = "smtp-svr3"

    Public Shared SMTP_MAILADD As String = "notifications@sss.gov.ph" '"noreply@sss.gov.ph"


    Private Shared SSS_WS_TOKENID As String = "WESHJZ1Q103017102439"

    Public Shared PRN_MemberType_Employed As String = "Employed"
    Public Shared PRN_MemberType_SelfEmployed As String = "Self-Employed"
    Public Shared PRN_MemberType_VoluntaryPaying As String = "Voluntary"
    Public Shared PRN_MemberType_HouseHoldHelp As String = "Household help"
    Public Shared PRN_MemberType_NonWorkingSpouse As String = "Non-working Spouse"
    Public Shared PRN_MemberType_OverseasContractWorker As String = "OFW"

    'added by edel Nov 20, 2020
    Public Shared SET_PROJECT_NAME As String = "Self-Service Express Terminal"
    Public Shared FAILED_MATCHING_LIMIT As Integer = 4


    Private Shared MsgHeader As String = SET_PROJECT_NAME.ToUpper()

    Public Shared Sub replaceCardData(ByVal crn As String, ByRef refCRN As String, ByRef refSSSNum As String, ByRef refCCDT As String)
        For Each line As String In System.IO.File.ReadLines(Application.StartupPath & "\1001.txt")
            If line.Trim = "" Then
            ElseIf line.Trim.Contains("---") Then
            Else
                Dim arr() As String = line.Split("|")
                If arr(0) = crn Then
                    refCRN = arr(1).Replace("-", "")
                    refSSSNum = arr(2)
                    refCCDT = arr(3)
                    Exit For
                End If
            End If
        Next
    End Sub

    Public Shared Function ShowMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.YesNo, Optional ByVal msgBoxIcn As MessageBoxIcon = MessageBoxIcon.Question) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, msgBoxIcn)
    End Function

    Public Shared Function ShowInfoMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Information)
    End Function

    Public Shared Function ShowErrorMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Error)
    End Function

    Public Shared Function ShowWarningMessage(ByVal strMsg As String, Optional ByVal msgBoxBtn As MessageBoxButtons = MessageBoxButtons.OK) As DialogResult
        Return MessageBox.Show(strMsg, MsgHeader, msgBoxBtn, MessageBoxIcon.Warning)
    End Function

    Public Shared Sub ShowUnableToConnectToRemoteServerMessage()
        MessageBox.Show("UNABLE TO CONNECT TO REMOTE SERVER. PLEASE TRY AGAIN.", MsgHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub

    Public Shared Sub ShowAPIResponseMessage(ByVal msg As String)
        MessageBox.Show(msg.ToUpper & "." & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", MsgHeader, MessageBoxButtons.OK, MessageBoxIcon.Warning)
    End Sub


    Public Shared Sub CreateCardData(ByVal Type As String, ByVal Data As String)
        'If Not IO.Directory.Exists("Temp") Then IO.Directory.CreateDirectory("Temp")

        'Dim sw As New System.IO.StreamWriter("Temp\CARD_DATA")
        'sw.WriteLine(Type & "|" & Data)
        'sw.Dispose()
        'sw.Close()
        Data = Data.Replace("|", "")

        editSettings(xml_Filename, xml_path, "CARD_DATA", Type & "|" & Data & "|")

        'My.Settings.CARD_DATA = Type & "|" & Data & "|"
        'My.Settings.Save()
    End Sub

    Public Shared Sub ShowMainNewUserForm(ByVal userform As UserControl)
        SharedFunction.RemoveMainActiveUserForm()
        GC.Collect()
        userform.Dock = DockStyle.Fill
        Main.Controls.Add(userform)
        Main.Refresh()
        'Main.ResizeMain()
    End Sub

    Public Shared Sub RemoveMainActiveUserForm()
        For Each ctrl As Control In Main.Controls
            If TypeOf ctrl Is Button Then
            Else
                Main.Controls.Remove(ctrl)
            End If
        Next
    End Sub

    Public Shared Sub ShowMainDefaultUserForm()
        RemoveMainActiveUserForm()
        GC.Collect()
        Dim _userform As New usrfrmSelectv2
        _userform.Dock = DockStyle.Fill
        Main.Controls.Add(_userform)
        Main.Refresh()
    End Sub


    Public Shared Sub OpenSSIT_Member(ByVal REF_NUM As String)
        'Dim strFile As String = "D:\for sss\may 20\New folder\SSIT_SERVER\SSIT_SERVER\bin\Debug\SSIT.exe"
        'Dim strFile As String = "C:\Program Files\SSIT\SSIT_SERVER.exe"

        Select Case REF_NUM.Trim.Replace("-", "").Length
            Case 10 'SSS#
                SaveActivityToDbase("OLDSSSCARD", REF_NUM)
            Case 12 'CRN
                If Not isGSISCard Then
                    SaveActivityToDbase("UMIDCARD", REF_NUM)
                Else
                    SaveActivityToDbase("UMIDCARDGSIS", REF_NUM)
                End If
        End Select

        Main.Hide()
        HomeScreen()
    End Sub

    'Public Shared Sub OpenOldSSSDecrypt()
    '    Dim strFile As String = "sss card\old_sss_decrypt.exe"

    '    If IO.File.Exists(strFile) Then
    '        Dim startInfo As New ProcessStartInfo(strFile)
    '        startInfo.WindowStyle = ProcessWindowStyle.Hidden
    '        startInfo.Arguments = "iL91*z(YoO@a"
    '        Process.Start(startInfo)
    '    Else
    '        ShowInfoMessage(strFile & " does not exist")
    '    End If
    'End Sub

    Public Shared Sub HouseKeeping()
        Try
            For Each strFile In IO.Directory.GetFiles("Temp")
                IO.File.Delete(strFile)
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Shared FingerScannerDevice As String = Application.StartupPath & "\FingerScannerDevice.txt"

    'check device manager for fingerscanner
    Public Shared Sub GetFingerscannerDevice()
        If IO.File.Exists(FingerScannerDevice) Then IO.File.Delete(FingerScannerDevice)

        Dim pc As String = "." 'local
        Dim wmi As Object = GetObject("winmgmts:\\" & pc & "\root\cimv2")
        Dim devices As Object = wmi.ExecQuery("Select * from Win32_PnPEntity")
        For Each d As Object In devices
            Dim DeviceName As String = IIf(IsDBNull(d.Name), 0, d.Name).ToString.ToUpper
            Dim DeviceManufacturer As String = IIf(IsDBNull(d.Manufacturer), 0, d.Manufacturer).ToString.ToUpper
            If DeviceName.Contains("FUTRONIC") And DeviceManufacturer.Contains("FUTRONIC") Then
                IO.File.WriteAllText(FingerScannerDevice, "FUTRONIC")
            ElseIf DeviceName.Contains("SAGEM") Then ' And DeviceManufacturer.Contains("HID") Then
                IO.File.WriteAllText(FingerScannerDevice, "SAGEM")
            End If
        Next
    End Sub

    Public Shared Sub KillProgram(ByVal Program As String)
        Try
            Dim programs() As Process = Process.GetProcessesByName(Program.Replace(".exe", "").Replace(".EXE", ""))
            For Each _program As Process In programs
                _program.Kill()
            Next
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveActivityToDbase(ByVal TRANS_TYPE As String, ByVal REF_NUM As String)
        Dim DAL As New DAL_Mssql
        If DAL.InsertSSINFOTERMACCESS(kioskID, TRANS_TYPE, REF_NUM) Then
        End If
    End Sub

    Private Delegate Sub Action()

    'Public Shared Sub InvokeSystemMessage(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As SystemMessage.MsgType)
    '    Dim sm As New SystemMessage(_form, _msg, _msgType)
    '    sm.ShowMsg()
    'End Sub

    Public Shared Sub InvokeSystemMessage(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As SystemMessage.MsgType, Optional ByRef btnResult As DialogResult = DialogResult.OK)
        Dim sm As New SystemMessage(_form, _msg, _msgType, MessageBoxButtons.OK)
        sm.ShowMsg()
        btnResult = sm.btnResult
    End Sub

    Public Shared Sub InvokeSystemMessage2(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As SystemMessage.MsgType, Optional ByRef btnResult As DialogResult = DialogResult.OK)
        Dim sm As New SystemMessage(_form, _msg, _msgType, MessageBoxButtons.YesNo)
        sm.ShowMsg()
        btnResult = sm.btnResult
    End Sub

    Public Shared Sub InvokeSystemMessage3(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As SystemMessage.MsgType, Optional ByRef btnResult As DialogResult = DialogResult.OK)
        Dim sm As New SystemMessage(_form, _msg, _msgType, MessageBoxButtons.OKCancel)
        sm.ShowMsg()
        btnResult = sm.btnResult
    End Sub

    Public Shared Function CHECK_MEMSTATUS(ByVal form As UserControl, ByVal REF_NUM As String, Optional ByVal intAttemptCntr As Short = 1) As String


        REF_NUM = REF_NUM.Trim.Replace("-", "")

        Dim DAL As New DAL_Oracle
        If DAL.CHECK_MEMSTATUS(REF_NUM) Then
            Dim checkMemStatus As Object = DAL.ObjectResult

            If checkMemStatus = "INVALID_SSS_OR_CR_NUMBER" Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & IIf(REF_NUM.Length = 12, "CRN ", "SSS# ") & REF_NUM & ". Failed to get member status - " & checkMemStatus & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                SharedFunction.InvokeSystemMessage(form, "UNABLE TO GET MEMBER STATUS. PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                Return ""
            ElseIf checkMemStatus = "" Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & IIf(REF_NUM.Length = 12, "CRN ", "SSS# ") & REF_NUM & ". Failed to get member status - " & checkMemStatus & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                SharedFunction.InvokeSystemMessage(form, "UNABLE TO GET MEMBER STATUS. PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                Return ""
            ElseIf checkMemStatus.ToString.Contains("no data found") Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & IIf(REF_NUM.Length = 12, "CRN ", "SSS# ") & REF_NUM & ". Failed to get member status - " & checkMemStatus & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                SharedFunction.InvokeSystemMessage(form, "UNABLE TO GET MEMBER STATUS. PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                Return ""
            Else
                'My.Settings.CHECK_MEMSTATUS = checkMemStatus
                'My.Settings.Save()
                CHECK_MEMSTATUS_Settings = checkMemStatus
                editSettings(xml_Filename, xml_path, "CHECK_MEMSTATUS", checkMemStatus)

                Return "OK"
            End If
        Else
            If intAttemptCntr <= 3 Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & IIf(REF_NUM.Length = 12, "CRN ", "SSS# ") & REF_NUM & ". CHECK_MEMSTATUS(" & intAttemptCntr.ToString & "): " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                CHECK_MEMSTATUS(form, REF_NUM, intAttemptCntr + 1)
            Else
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & IIf(REF_NUM.Length = 12, "CRN ", "SSS# ") & REF_NUM & ". CHECK_MEMSTATUS(" & intAttemptCntr.ToString & "): " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                'SharedFunction.InvokeSystemMessage(form, DAL.ErrorMessage & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                'SharedFunction.InvokeSystemMessage(form, "SYSTEM ERROR" & vbNewLine & vbNewLine & "PLEASE TRY AGAIN.", SystemMessage.MsgType.ErrorMsg)
                SharedFunction.InvokeSystemMessage(form, "YOU PLACED A NON-SSS CARD OR YOUR SSS CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "NO CARD DETECTED" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                'If REF_NUM.Length = 12 Then
                '    SharedFunction.InvokeSystemMessage(form, DAL.ErrorMessage & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                'Else
                '    SharedFunction.InvokeSystemMessage(form, "YOU PLACED A NON-SSS CARD OR YOUR SSS CARD IS DEFECTIVE." & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                'End If

                Return "ERROR"
            End If
        End If
    End Function

    Public Shared Function checkIfWithUMID(ByVal form As UserControl, ByVal SSNUM As String, Optional ByVal intAttemptCntr As Short = 1) As String
        Dim DAL As New DAL_Oracle
        If DAL.checkIfWithUMID(SSNUM) Then
            Dim result As String = DAL.ObjectResult.ToString
            If result = "MEMBER_WITH_UMID_CARD" Then
                ' SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "SSS# " & SSNUM & ". MEMBER_WITH_UMID_CARD" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                'SharedFunction.InvokeSystemMessage(form, "YOUR SS CARD IS NOT VALID FOR SSS TRANSACTION. PLEASE USE YOUR LATEST ISSUED UMID CARD.", SystemMessage.MsgType.ErrorMsg)
                SharedFunction.InvokeSystemMessage(form, "YOUR SS CARD IS NOT VALID FOR SSS TRANSACTION. PLEASE USE YOUR LATEST ISSUED UMID CARD." & vbNewLine & vbNewLine & "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                'SharedFunction.ShowWarningMessage("The UMID card presented is not the latest card")
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFWITHUMID(): SSNUM " & SSNUM & "- NOT THE LATEST CARD." & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                Return ""
            Else
                Return "OK"
            End If
        Else
            If intAttemptCntr <= 3 Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "SSS# " & SSNUM & ". checkIfWithUMID(" & intAttemptCntr.ToString & "): " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                checkIfWithUMID(form, SSNUM, intAttemptCntr + 1)
            Else
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "SSS# " & SSNUM & ". checkIfWithUMID(" & intAttemptCntr.ToString & "): " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                SharedFunction.InvokeSystemMessage(form, DAL.ErrorMessage & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                Return "ERROR"
            End If
        End If
    End Function

#Region " Logs "

    Private Shared SystemLog As String = "D:\SSIT\logs\" & Now.ToString("ddMMyyyy")
    Private Shared ErrorLog As String = "D:\SSIT\logs\" & Now.ToString("ddMMyyyy")

    Private Shared Sub InitLogFolder()
        If Not IO.Directory.Exists("D:\SSIT\logs\" & Now.ToString("ddMMyyyy") & " " & getbranchCoDE_1) Then IO.Directory.CreateDirectory("D:\SSIT\logs\" & Now.ToString("ddMMyyyy") & " " & getbranchCoDE_1)
    End Sub

    Public Shared Sub SaveToLog(ByVal strData As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(SystemLog & " " & getbranchCoDE_1 & "\System.log", True)
            sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToLogv2(ByVal desc As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(SystemLog & " " & getbranchCoDE_1 & "\System.log", True)
            sw.WriteLine(String.Format("{0}|{1}|{2}|{3}", TimeStamp, desc, kioskIP, getbranchCoDE_1))
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToErrorLog(ByVal strData As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(ErrorLog & " " & getbranchCoDE_1 & "\Error.log", True)
            sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToErrorLogv2(ByVal desc As String)
        Try
            InitLogFolder()
            Dim sw As New IO.StreamWriter(ErrorLog & " " & getbranchCoDE_1 & "\Error.log", True)
            sw.WriteLine(String.Format("{0}|{1}|{2}|{3}", TimeStamp, desc, kioskIP, getbranchCoDE_1))
            'sw.WriteLine(strData)
            sw.Dispose()
            sw.Close()
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToInfoTerminalLog(ByVal logPath As String, ByVal btnDesc As String, ByVal err As String)
        Try
            Using SW As New IO.StreamWriter(logPath & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|Form: Main Form|Print out of {5} Failed|{6}|{7}**", kioskIP, kioskID, kioskName, kioskBranch, err.Trim, btnDesc, Date.Today.ToShortDateString, TimeOfDay))
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToInfoTerminalLogPrint(ByVal logPath As String, ByVal btnDesc As String, ByVal err As String)
        Try
            Using SW As New IO.StreamWriter(logPath & "\" & "InfoTerminal_logs.txt", True)
                SW.WriteLine(String.Format("{0}|{1}|{2}|{3}|{4}|Form: Print Form|Print out of {5} Failed|{6}|{7}**", kioskIP, kioskID, kioskName, kioskBranch, err.Trim, btnDesc, Date.Today.ToShortDateString, TimeOfDay))
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Sub SaveToTransactionLog(ByVal logPath As String, ByVal crn As String, ByVal code As String, ByVal transNum As String, ByVal transDesc As String)
        Try
            Using SW As New IO.StreamWriter(logPath & "\" & "transaction_logs.txt", True)
                SW.WriteLine(crn & "|" & code & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
            End Using
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function TimeStamp() As String
        Return Now.ToString("MM/dd/yy hh:mm:ss tt").PadRight(25, " ")
    End Function

    Public Enum UtilityTool
        Taskbar = 1
        TaskManager = 2
    End Enum

    Public Shared Sub SSITUtilityTool(ByVal UtilityTool As UtilityTool, ByVal Status As Short)
        'Dim s As String = "D:\EDEL\ACC\Projects\SSIT Utility\SSIT Utility\bin\Debug\SSIT Utility.exe"
        'System.Diagnostics.Process.Start("SSIT Utility.exe", UtilityTool & "," & Status)
        Dim startInfo As New ProcessStartInfo("SSIT Utility.exe")
        startInfo.WindowStyle = ProcessWindowStyle.Hidden
        startInfo.Arguments = UtilityTool & "," & Status
        Process.Start(startInfo)
    End Sub

    Public Shared Function WriteSSSNumber_InUMIDCardSector(ByVal CRN As String, ByVal SSSNumber As String) As Boolean
        Dim intSector As Integer = 36
        Dim ErrorMessage As String = ""
        Dim result As Boolean = AllcardUMID.WriteSectorData(intSector, SSSNumber, ErrorMessage)

        If result Then
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & CRN & " - Writing SSS# " & SSSNumber & " in sector is success" & "|" & kioskIP & "|" & getbranchCoDE_1)
            Return True
        Else
            SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & String.Format("CRN {0}.{1}", CRN, ErrorMessage) & "|" & kioskIP & "|" & getbranchCoDE_1)
            Return False
        End If

    End Function

    Public Shared Function ChangePIN(ByVal form As UserControl, ByVal oldPIN As String, ByVal PIN As String, ByVal CRN As String) As Boolean
        Dim ErrorMessage As String = ""
        'Dim result As Boolean = AllcardUMID.ChangePIN(AllcardUMID.GetUMIDCardPIN(ErrorMessage), PIN, ErrorMessage)
        Dim result As Boolean = AllcardUMID.ChangePIN(oldPIN, PIN, ErrorMessage)

        If result Then
            Return True
        Else
            SaveToErrorLog(TimeStamp() & "|" & String.Format("CRN {0}.{1}", CRN, ErrorMessage) & "|" & kioskID & "|" & getbranchCoDE_1 & cardType)
            SharedFunction.InvokeSystemMessage(form, "UNABLE TO CHANGE PIN", SystemMessage.MsgType.ErrorMsg)
            Return False
        End If
    End Function

#End Region

    Private Shared PIN_FILE As String = "PIN_FILE.txt"

    'Public Shared Sub UpdatetempPINTable(ByVal CRN As String, ByVal PIN As String)
    '    'Dim dt As DataTable = My.Settings.tempPINTable
    '    If My.Settings.tempPINTable.Select("CRN='" & CRN & "'").Length = 0 Then
    '        Dim rw As DataRow = My.Settings.tempPINTable.NewRow
    '        rw("CRN") = CRN.Replace("-", "")
    '        rw("PIN") = PIN
    '        My.Settings.tempPINTable.Rows.Add(rw)
    '        'dt.AcceptChanges()
    '    Else
    '        My.Settings.tempPINTable.Select("CRN='" & CRN & "'")(0)("PIN") = PIN
    '        'dt.AcceptChanges()
    '    End If
    '    'My.Settings.tempPINTable = dt
    '    My.Settings.Save()
    'End Sub


    Public Shared Function GetPIN_byCRN(ByVal CRN As String) As String
        Dim PIN As String = "123456"
        Dim sr As New IO.StreamReader(PIN_FILE)
        Do While Not sr.EndOfStream
            Dim strLine As String = sr.ReadLine
            If strLine <> "" Then
                If strLine.Split("|")(0) = CRN Then
                    PIN = strLine.Split("|")(1)
                    Exit Do
                End If
            End If
        Loop
        sr.Dispose()
        sr.Close()

        Return PIN

        'Dim dt As DataTable = My.Settings.tempPINTable
        'If My.Settings.tempPINTable.Select("CRN='" & CRN & "'").Length = 0 Then
        'Return "123456"
        'Else
        'Return My.Settings.tempPINTable.Select("CRN='" & CRN & "'")(0)("PIN")
        'End If
    End Function

    Public Shared Function CheckCRNIfValid(ByVal CRN As String, ByVal CCDT As String) As Short
        Dim DAL As New DAL_Oracle
        'ShowErrorMessage("CRN " & CRN.Replace("-", "") & ",CCDT " & CCDT)
        If DAL.checkIfValid(CRN.Replace("-", ""), CCDT) Then
            If DAL.ObjectResult Is Nothing Then
                Return 3
            Else
                If DAL.ObjectResult.ToString = "" Then
                    Return 4
                Else
                    If DAL.ObjectResult.ToString = "INVALID" Then
                        Return 5
                    Else
                        'My.Settings.CSN = DAL.ObjectResult.ToString
                        editSettings(xml_Filename, xml_path, "CSN", DAL.ObjectResult.ToString.Split("||")(0))
                        ' My.Settings.CSN = DAL.ObjectResult.ToString.Split("||")(0)
                        If DAL.ObjectResult Is Nothing Then
                            'My.Settings.CardPIN = ""
                            editSettings(xml_Filename, xml_path, "CardPIN", "")
                        ElseIf IsDBNull(DAL.ObjectResult) Then
                            '  My.Settings.CardPIN = ""
                            editSettings(xml_Filename, xml_path, "CardPIN", "")
                        Else
                            If DAL.ObjectResult.ToString.Split("||")(2) = "NO_PIN" Then
                                ' My.Settings.CardPIN = ""
                                editSettings(xml_Filename, xml_path, "CardPIN", "")
                            Else
                                ' My.Settings.CardPIN = DAL.ObjectResult.ToString.Split("||")(2)
                                editSettings(xml_Filename, xml_path, "CardPIN", DAL.ObjectResult.ToString.Split("||")(2))
                            End If
                        End If

                        ' My.Settings.Save()

                        Return 0
                    End If
                End If
            End If
        Else
            'ShowErrorMessage(DAL.ErrorMessage)
            If DAL.ErrorMessage.Contains("no data found") Then
                Return 1
            ElseIf DAL.ErrorMessage.Contains("no data passed to obfuscation toolkit") Then
                Return 6
            Else
                Return 2
            End If
        End If
    End Function

    Public Shared Function CHECKIFRECENT(ByVal form As UserControl, ByVal CRN As String, ByVal CCDT As String, ByVal STEPS As String, _
                                         ByRef SSNum As String, ByRef PIN As String, Optional ByVal intAttemptCntr As Short = 1) As Short
        Dim DAL As New DAL_Oracle
        Dim resultMsg As String = ""
        'MessageBox.Show("CRN:" & CRN.Replace("-", "") & "   CCDT:" & CCDT)
        If DAL.CHECKIFRECENT(CRN.Replace("-", ""), CCDT, STEPS, SSNum, PIN, resultMsg) Then
            Select Case resultMsg
                Case "SUCCESS"
                    Return 0
                Case "NOT THE LATEST CARD"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(): CRN " & CRN & " AND CCDT " & CCDT & " - NOT THE LATEST CARD" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                    'SharedFunction.InvokeSystemMessage(form, "YOUR UMID CARD IS NOT VALID FOR SSS TRANSACTION. PLEASE MAKE SURE THAT YOU USE YOUR LATEST ISSUED UMID CARD." & vbNewLine & vbNewLine & "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)
                    SharedFunction.ShowWarningMessage("The UMID card presented is not the latest card.".ToUpper)

                    Return 1
                Case "NOT ISSUED BY SSS"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(): CRN " & CRN & " AND CCDT " & CCDT & " - NOT ISSUED BY SSS" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                    'SharedFunction.InvokeSystemMessage(form, "YOUR UMID CARD IS NOT VALID FOR SSS TRANSACTION." & vbNewLine & vbNewLine & "TO ENABLE ITS USE WITH SSS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.Warning)
                    SharedFunction.InvokeSystemMessage(form, "Your UMID card is not valid for SSS transaction. To enable its use with SSS, please seek assistance from our Member Service Representative (MSR) at our frontline service counter or go to the nearest SSS branch.".ToUpper, SystemMessage.MsgType.Warning)

                    Return 2
                Case "NO_PIN"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(): CRN " & CRN & " AND CCDT " & CCDT & " - NO PIN" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                    Return 3
                Case Else
                    If intAttemptCntr <= 3 Then
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(" & intAttemptCntr.ToString & "): CRN " & CRN & " AND CCDT " & CCDT & ". SP result " & resultMsg & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                        CHECKIFRECENT(form, CRN, CCDT, STEPS, SSNum, PIN, intAttemptCntr + 1)
                    Else
                        SharedFunction.InvokeSystemMessage(form, DAL.ErrorMessage & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

                        Return 4
                    End If
            End Select
        Else
            If intAttemptCntr <= 3 Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(" & intAttemptCntr.ToString & "): CRN " & CRN & " AND CCDT " & CCDT & ". Error: " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                CHECKIFRECENT(form, CRN, CCDT, STEPS, SSNum, PIN, intAttemptCntr + 1)
            Else
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "CHECKIFRECENT(" & intAttemptCntr.ToString & "): CRN " & CRN & " AND CCDT " & CCDT & ". Error: " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                SharedFunction.InvokeSystemMessage(form, DAL.ErrorMessage & vbNewLine & vbNewLine & "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH.", SystemMessage.MsgType.ErrorMsg)

                Return 4
            End If
        End If
    End Function

    Public Shared Function GetMemberPIN(ByVal CRN As String, ByVal CSN As String) As String
        Dim DAL As New DAL_Oracle
        If DAL.decryptPIN(CRN.Replace("-", ""), CSN) Then
            'If DAL.decryptPIN2(CRN.Replace("-", "")) Then
            If DAL.ObjectResult Is Nothing Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "GetMemberPIN(): CRN " & CRN & ", CSN " & CSN & ". Failed to get pin" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                Return "FAILED2"
            Else
                If DAL.ObjectResult.ToString = "" Then

                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "GetMemberPIN(): CRN " & CRN & ", CSN " & CSN & ". Failed to get pin" & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
                    Return "FAILED3"
                Else
                    Return DAL.ObjectResult.ToString
                End If
            End If
        Else
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "GetMemberPIN(): CRN " & CRN & ", CSN " & CSN & ". Error: " & DAL.ErrorMessage & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)
            Return "FAILED1"
        End If
    End Function

    Public Shared Function BlockUMIDCard(ByVal _CRN As String, ByVal PIN As String) As Boolean
        Try
            Dim ErrorMessage As String = ""
            Dim result As Boolean = AllcardUMID.BlockUMIDCard(PIN, ErrorMessage)

            If Not result Then
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & String.Format("BlockUMIDCardCRN {0}.{1}", _CRN, ErrorMessage) & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            Else
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "BlockUMIDCard(): CRN " & "" & _CRN & " - Success card blocking" & "|" & kioskIP & "|" & getbranchCoDE_1)

                Return True
            End If
        Catch ex As Exception
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & "BlockUMIDCard(): CRN " & "" & _CRN & " - Error " & ex.Message & "|" & kioskID & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Sub HomeScreen()
        Try
            Dim xtd As New ExtractedDetails
            Dim tagDead As Integer

            createFile()
            Dim firstRun As String = readSettings(xml_Filename, xml_path, "firstRun")

            'If firstRun <> "3" Then
            '    'lblStatus.Visible = True
            '    'lblStatus.Text = "* Please Contact Administrator to set kiosk settings! "
            '    _frmHomeScreen.Show()
            'Else

            Dim result As Integer = xtd.checkFileType

            'ShowInfoMessage("CARD_DATA: " & My.Settings.CARD_DATA & "   result: " & result)

            Select Case result
                Case 1
                    GC.Collect()
                    xtd.getRawFile()
                    tagDead = IIf(CHECK_MEMSTATUS_Settings = "WITH_FINAL_CLAIM", 1, 0) ' xtd.getMemStat(xtd.getCRN)
                    If tagDead = 0 Then
                        xtd.getRawFile()
                        Dim getID As String = xtd.getCRN

                        getURL = getPermanentURL & "controller?action=sss&id=" & getID
                        _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & getID)

                        ShowMainDefaultUserForm()

                        _frmMainMenu.IsMainMenuActive = True
                        _frmMainMenu.MainLoad()
                        _frmMainMenu.Show()

                    Else
                        MsgBox("Member already avail a final claim", MsgBoxStyle.Information, "Information")
                        Application.Exit()
                    End If
                Case 2
                    GC.Collect()
                    'xtd.getRawFile()
                    'My.Settings.SSStempFile = My.Settings.SS_Number 'xtd.getSSSNumber
                    ' editSettings(xml_Filename, xml_path, "SSStempFile", xtd.getSSSNumber)

                    tagDead = IIf(CHECK_MEMSTATUS_Settings = "WITH_FINAL_CLAIM", 1, 0) 'xtd.getMemStat(My.Settings.SSStempFile)
                    If tagDead = 0 Then


                        'Dim getID As String = xtd.getCRN
                        'xtd.getRawFile()
                        'If SSStempFile Is Nothing Then
                        SSStempFile = readSettings(xml_Filename, xml_path, "SS_Number")
                        'SSStempFile = "0226879523"
                        'End If

                        If getPermanentURL Is Nothing Then getPermanentURL = readSettings(xml_Filename, xml_path, "getPermanentURL")

                        getURL = getPermanentURL & "controller?action=sss&id=" & SSStempFile
                        _frmWebBrowser.WebBrowser1.Navigate(getPermanentURL & "controller?action=sss&id=" & SSStempFile)

                        ShowMainDefaultUserForm()

                        _frmMainMenu.IsMainMenuActive = True
                        _frmMainMenu.MainLoad()
                        _frmMainMenu.Show()
                    Else
                        MsgBox("Member already avail a final claim", MsgBoxStyle.Information, "Information")
                        Application.Exit()
                    End If
                Case 3
                    _frmSSSwebsite.Show()
                    ShowMainDefaultUserForm()
                Case 4
                    _frmFeedbackKioskMain.Show()
                    ShowMainDefaultUserForm()
                Case 6
                    '_frmCitizenCharter3.wb.Navigate(Application.StartupPath & "\charter\CITIZENS_CHARTER.pdf")
                    '_frmCitizenCharter3.wb.Navigate("D:\d.pdf")
                    _frmCitizenCharter3.DOC_TYPE = 1
                    _frmCitizenCharter3.Show()
                    ShowMainDefaultUserForm()
                Case Else
                    'lblStatus.Visible = True
                    'lblStatus.Text = "* No file generated from card(s)! "

                    'Application.Exit()
                    'Me.Hide()
                    'System.Diagnostics.Process.Start(Application.StartupPath & "\" & "SSIT_Home" & "\" & "SSIT_HOME.exe")
                    'Main.Show()

                    'ShowMainDefaultUserForm()
                    'ShowAppMainForm()

                    _frmMainMenu.IsMainMenuActive = False
                    _frmMainMenu.Hide()
                    SharedFunction.ShowMainDefaultUserForm()
                    Main.Show()
            End Select
            'End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Shared Sub createFile()
        Dim filepath As String = Application.StartupPath & "\REF_NUM" ' & "\" & "Ref_Num\" & "REF_NUM.txt"  '  "C:\Users\Nikki Cassandra\Desktop\sample.txt"
        If System.IO.Directory.Exists(filepath) = False Then
            System.IO.Directory.CreateDirectory(filepath)
            System.IO.File.Create(filepath & "\REF_NUM.txt").Dispose()
        End If


    End Sub

    Public Shared Sub ShowAppMainForm(Optional ByVal _form As Form = Nothing)
        If Not _form Is Nothing Then
            '_form.Dispose()
            _form.Close()
            '_form = Nothing
        End If

        'ShowMainDefaultUserForm()
        If Main.Visible = False Then Main.Show()
    End Sub

    'Public Shared Sub BackToPreviousTxn()
    '    Try


    '        Select Case CurrentTxnType
    '            Case TxnType.SimplifiedWebRegistration
    '                If TxnAuthenticationResult Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    'show your simplifiedwebregis form and proceed to submit txn
    '                    _frmMemberRegistration2.submitRegistration()
    '                Else
    '                    transTag = "WR"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "SIMPLIFIED WEB REGISTRATION"
    '                    txnTypeRcpt = "SWR"
    '                    msgFailedAuth("17")
    '                End If
    '            Case TxnType.ApplicationForSalaryLoanMember
    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmLoanSummaryMember_v2.submitSalaryLoanMemberV2()
    '                Else
    '                    transTag = "SL"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "SALARY LOAN"
    '                    txnTypeRcpt = "SL"
    '                    msgFailedAuth("17")

    '                End If

    '            Case TxnType.ApplicationForSalaryLoanEmployed

    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmLoanSummaryEmployed.submitSalaryLoan()
    '                Else
    '                    transTag = "SL"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "SALARY LOAN"
    '                    txnTypeRcpt = "SL"
    '                    msgFailedAuth("17")
    '                End If

    '            Case TxnType.MaternityNotification
    '                'ShowInfoMessage("Authentication is " & IIf(TxnAuthenticationResult = True, "success", "failed") & ". Your previous txn is " & CurrentTxnType.ToString)
    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    '_frmMaternitySummary.submitMaternity() 
    '                    _frmEnhanceMaternityNotifSummary.submitMaternity()
    '                Else
    '                    transTag = "MT"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "MATERNITY NOTIFICATION"
    '                    txnTypeRcpt = "MT"
    '                    msgFailedAuth("17")
    '                End If
    '            Case TxnType.TechnicalRetirementPen

    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmTechnicalRetirementWillAvail.SubmitPension()
    '                Else
    '                    transTag = "TR"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "TECHNICAL RETIREMENT"
    '                    txnTypeRcpt = "TR"
    '                    msgFailedAuth("17")
    '                End If
    '            Case TxnType.TechnicalRetirementLumpSum

    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmTechnicalRetirementWillAvailLumpSum.submitLumpSum()
    '                Else
    '                    transTag = "TR"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "TECHNICAL RETIREMENT"
    '                    txnTypeRcpt = "TR"
    '                    msgFailedAuth("17")
    '                End If
    '            Case TxnType.AcopNoDep

    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmACOPconfirmationDependent.submitACOP()
    '                Else
    '                    transTag = "AC"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "ANNUAL CONFIRMATION OF PENSIONER"
    '                    txnTypeRcpt = "AC"
    '                    msgFailedAuth("17")
    '                End If
    '            Case TxnType.AcopDep
    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmAcopSummary.submitAcopDep()
    '                Else
    '                    transTag = "AC"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "ANNUAL CONFIRMATION OF PENSIONER"
    '                    txnTypeRcpt = "AC"
    '                    msgFailedAuth("17")
    '                End If

    '            Case TxnType.PensionMaintenance
    '                If TxnAuthenticationResult = True Then
    '                    _frm2.lblWaitSSS.Visible = True
    '                    _frmPensionSummary.submitPensionMaintenance()
    '                Else
    '                    transTag = "PM"
    '                    _frmUserAuthentication.getTransacNum()
    '                    crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
    '                    txnName = "CHANGE OF ADDRESS/CONTACT INFORMATION"
    '                    txnTypeRcpt = "PM"
    '                    msgFailedAuth("17")
    '                End If


    '        End Select
    '    Catch ex As Exception
    '    End Try
    'End Sub

    Public Shared Sub BackToPreviousTxnv2()
        Try
            Select Case CurrentTxnType
                Case TxnType.SimplifiedWebRegistration
                    If TxnAuthenticationResult Then
                        _frm2.lblWaitSSS.Visible = True
                        '_frmSWR2.SubmitSWR()
                        _frmSWR2v2.SubmitSWR()
                    Else
                        transTag = "WR"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "SIMPLIFIED WEB REGISTRATION"
                        txnTypeRcpt = "SWR"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If
                Case TxnType.ApplicationForSalaryLoanMember
                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmSalaryLoanEmployerv2.submitSalaryLoanMemberV3()
                    Else
                        transTag = "SL"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "SALARY LOAN"
                        txnTypeRcpt = "SL"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If

                Case TxnType.MaternityNotification
                    'ShowInfoMessage("Authentication is " & IIf(TxnAuthenticationResult = True, "success", "failed") & ". Your previous txn is " & CurrentTxnType.ToString)
                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmEnhanceMaternityNotifSummary.submitMaternity()
                    Else
                        transTag = "MT"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "MATERNITY NOTIFICATION"
                        txnTypeRcpt = "MT"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If

                Case TxnType.AcopNoDep

                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmACOPconfirmationDependent.submitACOP()
                    Else
                        transTag = "AC"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "ANNUAL CONFIRMATION OF PENSIONER"
                        txnTypeRcpt = "AC"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If
                Case TxnType.AcopDep
                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmAcopSummary.submitAcopDep()
                    Else
                        transTag = "AC"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "ANNUAL CONFIRMATION OF PENSIONER"
                        txnTypeRcpt = "AC"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If

                Case TxnType.PensionMaintenance
                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmPensionSummary.submitPensionMaintenance()
                    Else
                        transTag = "PM"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "CHANGE OF ADDRESS/CONTACT INFORMATION"
                        txnTypeRcpt = "PM"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If
                Case TxnType.PinChange
                    If TxnAuthenticationResult Then
                        _frmMainMenu.ShowPinChangeForm()
                    Else
                        transTag = "PC"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "PIN CHANGE"
                        txnTypeRcpt = "PC"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If
                Case TxnType.UpdateContactInformation
                    If TxnAuthenticationResult Then
                        _frmMainMenu.UpdateContactInformationv3()
                    Else
                        transTag = "UCI"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "UPDATE CONTACT INFORMATION"
                        txnTypeRcpt = "UCI"
                        msgFailedAuth("17")
                        Application.DoEvents()
                        If Not isGSISCard Then
                            System.Threading.Thread.Sleep(3000)
                            _frmMainMenu.LogoutUser()
                        End If
                    End If
                Case TxnType.OnlineRetirement
                    If TxnAuthenticationResult = True Then
                        _frm2.lblWaitSSS.Visible = True
                        _frmTechRetirementConfirm.submitTechnicalRetirementv2()
                    Else
                        transTag = "TR"
                        _frmUserAuthentication.getTransacNum()
                        crn_1 = readSettings(xml_Filename, xml_path, "SSStempFile")
                        txnName = "ONLINE RETIREMENT"
                        txnTypeRcpt = "TR"
                        msgFailedAuth("17")
                    End If
            End Select
        Catch ex As Exception
        End Try
    End Sub

    Public Shared Function IsProgramRunning(ByVal Program As String) As Short
        Dim p() As Process
        p = Process.GetProcessesByName(Program.Replace(".exe", "").Replace(".EXE", ""))

        Return p.Length
    End Function

    Public Shared Function GetYearDifference(ByVal dte1 As Date, ByVal dte2 As Date) As Long
        Return DateDiff(DateInterval.Year, dte1, dte2)
    End Function

    Private Shared wb As New WebBrowser
    Private Shared wbState As Short = 0
    Public Shared WebserviceToken As String = ""
    Public Shared WebserviceTxnToken As String = ""

    Public Shared Sub GetTxnToken(ByVal url As String, ByVal wsToken As String)
        WebserviceToken = wsToken
        wbState = 0
        AddHandler wb.DocumentCompleted, AddressOf wb_DocumentCompleted
        wb.Navigate(url)
        wbState = 1
    End Sub

    Private Shared Sub wb_DocumentCompleted(ByVal sender As System.Object, ByVal e As System.Windows.Forms.WebBrowserDocumentCompletedEventArgs)
        If wbState = 1 Then
            Dim doc As HtmlDocument
            Dim elem As HtmlElement

            doc = wb.Document

            For Each elem In doc.All
                If elem.GetAttribute("type") = "text" Then
                    elem.SetAttribute("value", WebserviceToken)
                End If
            Next
            For Each elem In doc.All
                If elem.GetAttribute("type") = "submit" Then
                    elem.InvokeMember("click")
                    wbState = 2
                End If
            Next
        ElseIf wbState = 2 Then
            Dim webpageContent As String = wb.DocumentText
            Dim startIndexTag As String = "Transac_token: <b>"
            Dim endIndexTag As String = "</b>"
            Dim varPosition1 As Integer = webpageContent.IndexOf(startIndexTag)
            Dim varPosition2 As Integer = webpageContent.IndexOf(endIndexTag, (varPosition1 + 1))
            Dim result1 As String = webpageContent.Substring(varPosition1 + startIndexTag.Length, varPosition2 - varPosition1)
            Dim txnToken As String = result1.Trim.Substring(0, (result1.Trim.Length - endIndexTag.Length))
            WebserviceTxnToken = String.Format("Token: {0}, Length: {1}", txnToken, txnToken.Length)
            MessageBox.Show(WebserviceTxnToken)
            RemoveHandler wb.DocumentCompleted, AddressOf wb_DocumentCompleted
        End If
    End Sub

    'Public Shared Function tokenDetailsResponse(ByVal sssNo As String) As SSSTokenGenerator.tokenDetailsResponse
    '    Dim tds As New SSSTokenGenerator.TokenDetailsService()
    '    'tds.Url = "http://10.0.4.252:3014/TokenGeneration/TokenDetailsPort?WSDL"
    '    'tds.Url = "http://10.141.249.22:8017/TokenServiceBean/TokenServiceBeanService?WSDL"
    '    Try
    '        Dim tdr As New SSSTokenGenerator.tokenDetailsRequest
    '        tdr.sssid = sssNo
    '        tdr.erbrn = ""
    '        tdr.appcd = "RC"
    '        tdr.trancd = "M"
    '        Return tds.TokenGeneration(tdr)
    '    Catch ex As Exception
    '        Dim errMsg As String = ex.Message
    '        Dim tdr As New SSSTokenGenerator.tokenDetailsResponse
    '        tdr.msg = 0
    '        Return tdr
    '    Finally
    '        tds.Dispose()
    '    End Try
    'End Function

    Public Shared Function tokenDetailsResponse(ByVal sssNo As String) As SSSTokenGenerator.tokenDetailsResponse
        Dim tds As New SSSTokenGenerator.TokenServiceBeanService
        tds.Url = UpdateCntctInfoTokenGenerator_URL
        Try
            Return tds.generateToken(sssNo, "", "RC", "M")
        Catch ex As Exception
            Dim errMsg As String = ex.Message
            Dim tdr As New SSSTokenGenerator.tokenDetailsResponse
            tdr.msg = 0
            Return tdr
        Finally
            tds.Dispose()
        End Try
    End Function

    'Public Shared Function GetSalaryLoanEligibilityWS(ByVal SSSNo As String, ByVal InType As String, ByVal LoanType As String,
    '                                                  ByVal SeqNo As String, ByRef response As String) As Boolean
    '    Dim ws As New WS_SalaryLoanEligib.EligibilityWebserviceImplService
    '    Dim WsResponse As WS_SalaryLoanEligib.eligibWsResponse
    '    Try
    '        Dim TxnToken As String = ws.AuthenticateToken(SSS_WS_TOKENID).transac_token.ToString()
    '        'WsResponse = ws.calleligibility("0226879523", "0000000000", "S", 1000, TxnToken, SSS_WS_TOKENID)
    '        WsResponse = ws.calleligibility(SSSNo, InType, LoanType, SeqNo, TxnToken, SSS_WS_TOKENID)
    '        response = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", _
    '                                "",
    '                                WsResponse.serviceFee,
    '                                WsResponse.totalbalance,
    '                                WsResponse.netloan,
    '                                WsResponse.appl_st,
    '                                WsResponse.monthly_amort,
    '                                WsResponse.rejlist,
    '                                WsResponse.loanableAmount,
    '                                WsResponse.loan_month,
    '                                WsResponse.maxLoanableAmount)

    '        Return True
    '    Catch ex As Exception
    '        response = "Error|GetSalaryLoanEligibilityWS(): Runtime error catched " & ex.Message
    '        Return False
    '    Finally
    '        WsResponse = Nothing
    '        ws.Dispose()
    '        ws = Nothing
    '    End Try
    'End Function

    'Public Shared Function GetSalaryLoanEligibilityWS2() As Boolean
    '    Dim ws As New WS_SalaryLoanEligib.EligibilityWebserviceImplService
    '    Dim WsResponse As WS_SalaryLoanEligib.eligibWsResponse
    '    Try
    '        Dim TxnToken As String = ws.AuthenticateToken(SSS_WS_TOKENID).transac_token.ToString()
    '        WsResponse = ws.calleligibility("0226879523", "0000000000", "S", 1000, TxnToken, SSS_WS_TOKENID)
    '        'WsResponse = ws.calleligibility(SSSNo, InType, LoanType, SeqNo, TxnToken, SSS_WS_TOKENID)
    '        response = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}|{7}|{8}|{9}", _
    '                                "",
    '                                WsResponse.serviceFee,
    '                                WsResponse.totalbalance,
    '                                WsResponse.netloan,
    '                                WsResponse.appl_st,
    '                                WsResponse.monthly_amort,
    '                                WsResponse.rejlist,
    '                                WsResponse.loanableAmount,
    '                                WsResponse.loan_month,
    '                                WsResponse.maxLoanableAmount)

    '        Return True
    '    Catch ex As Exception
    '        response = "Error|GetSalaryLoanEligibilityWS(): Runtime error catched " & ex.Message
    '        Return False
    '    Finally
    '        WsResponse = Nothing
    '        ws.Dispose()
    '        ws = Nothing
    '    End Try
    'End Function

    Public Shared Function GetMemberDisclosureWS(ByVal SSSNo As String, ByVal ErNum As String, ByVal LoanType As String,
                                                 ByVal LoanAmount As Double, ByVal InstallmentTerm As Integer,
                                                 ByVal UrIds As String, ByVal PrevLoanAmount As Double,
                                                 ByVal ServiceCharge As Double, ByVal ErSeqNo As String,
                                                 ByVal Address As String, ByVal TransID_TokenID As String, ByRef response As String) As Boolean

        Dim ws As New WS_Disclosure.DisclosureWebserviceImplService
        Dim WsResponse As WS_Disclosure.disclosureWsResponse
        Try
            'Dim TxnToken As String = ws.disAuthenticate(SSS_WS_TOKENID).transac_token.ToString()
            'WsResponse = ws.calldisclosure("0226879523", "0000000000", "S", 1000, 2, "", 0, 10, "21P3GFLXQW4UNAXUI0Y7DNSUMBGY8C5Q9YD61F061918150521", SSS_WS_TOKENID, "000", "NCR")
            WsResponse = ws.calldisclosure(SSSNo, ErNum, LoanType, LoanAmount, InstallmentTerm, UrIds, PrevLoanAmount, ServiceCharge, TransID_TokenID, SSS_WS_TOKENID, ErSeqNo, Address)
            'response = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", "", WsResponse.msg, WsResponse.ln_amt, WsResponse.monthly_amort, WsResponse.net_proceeds, WsResponse.loanbal, WsResponse.path)
            response = String.Format("{0}", WsResponse.msg)

            Try
                WS_CD_LoanAmount = LoanAmount 'WsResponse.ln_amt
                WS_CD_LoanBalance = WsResponse.loanbal
                WS_CD_NetProceeds = WsResponse.net_proceeds
                WS_CD_MonthlyAmort = WsResponse.monthly_amort
                WS_CD_PDF_Path = WsResponse.path
            Catch ex As Exception
            End Try

            Return True
        Catch ex As Exception
            response = "Error|GetMemberDisclosureWS(): Runtime error catched " & ex.Message
            Return False
        Finally
            WsResponse = Nothing
            ws.Dispose()
            ws = Nothing
        End Try
    End Function

    Public Shared Function Get_getAllowedMemberTypeListPRN(ByRef cbo As ComboBox, ByVal memberType As String, ByRef ErrMsg As String) As Boolean
        Try
            'MobileWS2BeanService_SessionToken = "SSIT5DX8FFS9LRR7ZTV3BSSITA6Q3N0BFLMP12081C08095T"
            'MobileWS2BeanService_URL = "https://wws.sss.gov.ph/MobileWSBean/MobileWSBeanService?WSDL"

            'MessageBox.Show(db_server)

            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            If IsRunningInTestEnv(db_server) Then
                SoapStr.Append("<target:getAllowedMemberTypeListPRN xmlns:target=""http://sevice.webservice.ejbsss/"">")
            Else
                SoapStr.Append("<target:getAllowedMemberTypeListPRN xmlns:target=""http://service.webservice.sss/"">")
            End If

            SoapStr.Append("<authToken>" & MobileWS2BeanService_SessionToken & "</authToken>")
            SoapStr.Append("<memberType>" & memberType.Trim.Replace(vbNewLine, " ") & "</memberType>")
            SoapStr.Append("</target:getAllowedMemberTypeListPRN>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""

            'Dim _file As String = "getAllowedMemberTypeListPRN.txt"
            'System.IO.File.WriteAllText(_file, SoapStr.ToString.Replace("<e", vbNewLine & "<e").Replace("<t", vbNewLine & "<t").Replace("<a", vbNewLine & "<a").Replace("<m", vbNewLine & "<m"))
            'Process.Start(_file)

            If Execute_PRN_WSDL(MobileWS2BeanService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                cbo.Items.Add("-Select Membership Type-")

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        cbo.Items.Add(cnNode2.InnerText)
                    Next
                Next

                Return True
            Else
                ErrMsg = "Get_getAllowedMemberTypeListPRN(): Execute_PRN_WSDL error " & ErrMsg
                Return False
            End If
        Catch ex As Exception
            ErrMsg = "Get_getAllowedMemberTypeListPRN(): Runtime error catched " & ex.Message
            Return False
        End Try
    End Function

    Private Shared localDBHost As String = "LAPTOP-NIBVC02K" '10.0.202.95

    Public Shared Function IsRunningInTestEnv(ByVal db_server As String) As Boolean
        If getPermanentURL.Contains("10.0.4.252:8888/sss-itservepp/") Then
            Return False
        ElseIf getPermanentURL.Contains("http://prs:7777/sss-ssitserve/") Then
            Return False
        Else
            Return True
        End If

        'Select Case db_server
        '    Case "10.0.6.191", "LAPTOP-NIBVC02K"
        '        Return True
        '    Case Else
        '        Return False
        'End Select
    End Function

    Public Shared Function Get_getContributionListPRN(ByRef cbo As ComboBox, ByRef ErrMsg As String) As Boolean
        Try
            If Not cboContributionListPRN Is Nothing Then
                'cbo = cboContributionListPRN
                'cbo.Items.Add("-Select Premium-")

                Dim decMaxValue As Decimal = 0

                For Each item As String In cboContributionListPRN.Items
                    cbo.Items.Add(item.Trim)
                Next

                Return True
            End If

            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            If IsRunningInTestEnv(db_server) Then
                SoapStr.Append("<target:getContributionListPRN xmlns:target=""http://sevice.webservice.ejbsss/"">")
            Else
                SoapStr.Append("<target:getContributionListPRN xmlns:target=""http://service.webservice.sss/"">")
            End If
            SoapStr.Append("<authToken>" & MobileWS2BeanService_SessionToken & "</authToken>")
            SoapStr.Append("</target:getContributionListPRN>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""

            If Execute_PRN_WSDL(MobileWS2BeanService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                cbo.Items.Add("-Select Premium-")

                Dim decMaxValue As Decimal = 0

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        cbo.Items.Add(cnNode2.InnerText)
                        If CDec(cnNode2.InnerText) > decMaxValue Then decMaxValue = CDec(cnNode2.InnerText)
                    Next
                Next

                cboContributionListPRN = cbo
                ContributionListPRN_MaxValue = decMaxValue

                Return True
            Else
                ErrMsg = "Get_getContributionListPRN(): Execute_PRN_WSDL error " & ErrMsg
                Return False
            End If
        Catch ex As Exception
            ErrMsg = "Get_getContributionListPRN(): Runtime error catched " & ex.Message
            Return False
        End Try

    End Function

    Public Shared Function Get_insertTransactionPRN(ByVal paymentRefNo As String, ByVal applicableDate As String,
                                                    ByVal ssnum As String, ByVal monthlyContribution As String,
                                                    ByVal flexiFundContribution As String, ByVal totalAmount As String,
                                                    ByVal memberType As String, ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            If IsRunningInTestEnv(db_server) Then
                SoapStr.Append("<target:insertTransactionPRN xmlns:target=""http://sevice.webservice.ejbsss/"">")
            Else
                SoapStr.Append("<target:insertTransactionPRN xmlns:target=""http://service.webservice.sss/"">")
            End If
            SoapStr.Append("<authToken>" & MobileWS2BeanService_SessionToken & "</authToken>")
            SoapStr.Append("<paymentRefNo>" & paymentRefNo & "</paymentRefNo>")
            SoapStr.Append("<applicableDate>" & applicableDate & "</applicableDate>")
            SoapStr.Append("<ssnum>" & ssnum & "</ssnum>")
            SoapStr.Append("<monthlyContribution>" & monthlyContribution & "</monthlyContribution>")
            SoapStr.Append("<flexiFundContribution>" & flexiFundContribution & "</flexiFundContribution>")
            SoapStr.Append("<totalAmount>" & totalAmount & "</totalAmount>")
            SoapStr.Append("<memberType>" & memberType & "</memberType>")
            SoapStr.Append("</target:insertTransactionPRN>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(MobileWS2BeanService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim processFlag As String = ""
                Dim returnMessage As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "processFlag"
                                processFlag = cnNode2.InnerText
                            Case "returnMessage"
                                returnMessage = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(processFlag)
                arrResponse.Add(returnMessage)

                Return True
            Else
                arrResponse.Add("0")
                arrResponse.Add("Get_insertTransactionPRN(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("Get_insertTransactionPRN(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function Get_inquireIndividualSSnum(ByVal SSNum As String, ByVal DOB As String, ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:inquireIndividualSSnum xmlns:target=""http://controller.iprn.sss.com/"">")
            SoapStr.Append("<arg0>" & IPRNImplService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>" & SSNum & "</arg1>")
            SoapStr.Append("<arg2>" & CDate(DOB).ToString("MM/dd/yyyy") & "</arg2>")
            SoapStr.Append("</target:inquireIndividualSSnum>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(IPRNImplService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim repCd As String = ""
                Dim errorMessage As String = ""
                Dim eename As String = ""
                Dim fapId As String = ""
                Dim tapId As String = ""
                Dim iprnum As String = ""
                Dim pdfPath As String = ""
                Dim tsamt As String = ""
                Dim dueDate As String = ""
                Dim repDT As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "repcd"
                                repCd = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                                'If cnNode2.InnerText = "0" Then arrResponse.Add("Success")
                            Case "errorMessage"
                                errorMessage = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "eename"
                                eename = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "fapld"
                                fapId = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "tapld"
                                tapId = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "iprnum"
                                iprnum = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "pdfPath"
                                pdfPath = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "tsamt"
                                tsamt = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "dueDate"
                                dueDate = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                            Case "repdt"
                                repDT = cnNode2.InnerText
                                'arrResponse.Add(cnNode2.InnerText)
                        End Select
                    Next
                Next

                arrResponse.Add(repCd) '0
                arrResponse.Add(errorMessage) '1
                arrResponse.Add(eename) '2
                arrResponse.Add(fapId) '3
                arrResponse.Add(tapId) '4
                arrResponse.Add(iprnum) '5
                arrResponse.Add(pdfPath) '6
                arrResponse.Add(tsamt) '7
                arrResponse.Add(dueDate) '8

                Return True
            Else
                arrResponse.Add("2")
                arrResponse.Add("Get_inquireIndividualSSnum(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("Get_inquireIndividualSSnum(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function Get_inquireIndividualSSnum2(ByVal SSNum As String, ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:inquireIndividualSSnum xmlns:target=""http://controller.iprn.sss.com/"">")
            'SoapStr.Append("<arg0>FbPPDV9yLesn9Dhb0H9Lm2izVTENMASgs1sJTCwiG6Cblda9iP</arg0>")
            SoapStr.Append("<arg0>" & IPRNImplService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>" & SSNum & "</arg1>")
            SoapStr.Append("</target:inquireIndividualSSnum>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            'If Execute_PRN_WSDL("https://wws.sss.gov.ph/testsevm/IPRNImplService?wsdl", SoapStr.ToString, SoapResponse, ErrMsg) Then
            If Execute_PRN_WSDL(IPRNImplService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")
                Dim intSeq As Short = 1

                For Each cnNode1 As System.Xml.XmlNode In list

                    Dim repCd As String = ""
                    Dim errorMessage As String = ""
                    Dim eename As String = ""
                    Dim eenum As String = ""
                    Dim fapId As String = ""
                    Dim tapId As String = ""
                    Dim iprnum As String = ""
                    Dim pdfPath As String = ""
                    Dim provamt As String = ""
                    Dim ssamt As String = ""
                    Dim ecamt As String = ""
                    Dim tsamt As String = ""
                    Dim dueDate As String = ""
                    Dim repDT As String = ""
                    Dim flexamt As String = ""

                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "repcd"
                                repCd = cnNode2.InnerText
                            Case "errorMessage"
                                errorMessage = cnNode2.InnerText
                            Case "eename"
                                eename = cnNode2.InnerText
                            Case "eenum"
                                eenum = cnNode2.InnerText
                            Case "fapld"
                                fapId = cnNode2.InnerText
                            Case "tapld"
                                tapId = cnNode2.InnerText
                            Case "iprnum"
                                iprnum = cnNode2.InnerText
                            Case "pdfPath"
                                pdfPath = cnNode2.InnerText
                            Case "provamt"
                                provamt = cnNode2.InnerText
                            Case "ssamt"
                                ssamt = cnNode2.InnerText
                            Case "ecamt"
                                ecamt = cnNode2.InnerText
                            Case "tsamt"
                                tsamt = cnNode2.InnerText
                            Case "dueDate"
                                dueDate = cnNode2.InnerText
                            Case "repdt"
                                repDT = cnNode2.InnerText
                            Case "flexamt"
                                flexamt = cnNode2.InnerText
                        End Select
                    Next

                    If repCd = "0" Then
                        Dim rw As DataRow = TableMemberPRNApplication.NewRow
                        'rw("Seq") = intSeq.ToString("N0")
                        rw("eename") = eename
                        rw("eenum") = eenum
                        rw("fapld") = fapId
                        rw("fapld_Date") = CDate(String.Format("{0}/01/{1}", fapId.Substring(0, 2), fapId.Substring(2, 4)))
                        rw("iprnum") = iprnum
                        rw("repcd") = repCd
                        If repDT <> "" Then rw("repdt") = CDate(repDT).ToString("dd-MMM-yy")
                        rw("tapld") = tapId
                        If provamt <> "" Then rw("provamt") = CDec(provamt).ToString("N2") Else rw("provamt") = "-"
                        If ssamt <> "" Then rw("ssamt") = CDec(ssamt).ToString("N2") Else rw("ssamt") = "-"
                        If ecamt <> "" Then rw("ecamt") = CDec(ecamt).ToString("N2") Else rw("ecamt") = "-"
                        rw("tsamt") = CDec(tsamt).ToString("N2")
                        rw("dueDate") = CDate(dueDate).ToString("dd-MMM-yy")
                        rw("dueDate_Date") = CDate(dueDate)
                        rw("pdfPath") = pdfPath
                        rw("ApplicablePeriod") = String.Format("{0} {1} - {2} {3}", Get_MonthDesc(CInt(fapId.Substring(0, 2))).Substring(0, 3), fapId.Substring(2, 4), Get_MonthDesc(CInt(tapId.Substring(0, 2))).Substring(0, 3), tapId.Substring(2, 4))
                        rw("MemberType") = SharedFunction.Get_PRN_MembershipTypeDesc_ByPRN(iprnum)

                        Dim intNoOfMonths As Integer = ((DateDiff(DateInterval.Month, CDate(String.Format("{0}/01/{1}", fapId.Substring(0, 2), fapId.Substring(2, 4))), CDate(String.Format("{0}/01/{1}", tapId.Substring(0, 2), tapId.Substring(2, 4))))) + 1)
                        Dim monthlyPayment As Decimal = CDec(tsamt) / intNoOfMonths
                        Dim flexiFund As Decimal = 0
                        If monthlyPayment >= ContributionListPRN_MaxValue Then
                            rw("MonthlyPayment") = CDec(ContributionListPRN_MaxValue).ToString("N2")
                            'flexiFund = CDec(tsamt) - (ContributionListPRN_MaxValue * intNoOfMonths)
                        Else
                            rw("MonthlyPayment") = CDec(monthlyPayment).ToString("N2")
                        End If
                        If flexamt <> "" Then flexiFund = CDec(flexamt).ToString("N2") Else flexiFund = "0.00"

                        'rw("MonthlyPayment") = CDec(tsamt).ToString("N2")
                        rw("FlexiFund") = CDec(flexiFund).ToString("N2")
                        TableMemberPRNApplication.Rows.Add(rw)
                        intSeq += 1
                    End If
                Next

                If intSeq > 1 Then
                    arrResponse.Add("0") '0
                    arrResponse.Add("SUCCESS") '0
                Else
                    arrResponse.Add("1") '0
                    arrResponse.Add("NO RECORD FOUND") '0
                End If

                Return True
            Else
                arrResponse.Add("1")
                arrResponse.Add("Get_inquireIndividualSSnum2(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("Get_inquireIndividualSSnum2(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function Get_loadIndividualPRNChange(ByVal memberType As String, ByVal SSNum As String, ByVal totalAmount As String, ByVal flexiAmount As String,
                                                  ByVal fromApplicableDate As String, ByVal toApplicableDate As String,
                                                  ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try


            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:loadIndividualPRNChange xmlns:target=""http://controller.iprn.sss.com/"">")
            SoapStr.Append("<arg0>" & IPRNImplService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>" & Get_PRN_MembershipTypeCode(memberType) & "</arg1>")
            SoapStr.Append("<arg2>" & SSNum & "</arg2>")
            SoapStr.Append("<arg3>" & totalAmount & "</arg3>")
            SoapStr.Append("<arg4>" & flexiAmount & "</arg4>")
            SoapStr.Append("<arg5>" & fromApplicableDate & "</arg5>")
            SoapStr.Append("<arg6>" & toApplicableDate & "</arg6>")
            SoapStr.Append("<arg7></arg7>")
            'SoapStr.Append("<arg6></arg6>")
            SoapStr.Append("</target:loadIndividualPRNChange>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(IPRNImplService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim repCd As String = ""
                Dim errorMessage As String = ""
                Dim eename As String = ""
                Dim fapId As String = ""
                Dim tapId As String = ""
                Dim iprnum As String = ""
                Dim pdfPath As String = ""
                Dim tsamt As String = ""
                Dim dueDate As String = ""
                Dim repDT As String = ""

                Dim eenum As String = ""
                Dim ecamt As String = ""
                Dim ssamt As String = ""
                Dim provamt As String = ""
                Dim flexamt As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "repcd"
                                repCd = cnNode2.InnerText
                            Case "errorMessage"
                                errorMessage = cnNode2.InnerText
                            Case "eename"
                                eename = cnNode2.InnerText
                            Case "fapld"
                                fapId = cnNode2.InnerText
                            Case "tapld"
                                tapId = cnNode2.InnerText
                            Case "iprnum"
                                iprnum = cnNode2.InnerText
                            Case "pdfPath"
                                pdfPath = cnNode2.InnerText
                            Case "tsamt"
                                tsamt = cnNode2.InnerText
                            Case "dueDate"
                                dueDate = cnNode2.InnerText
                            Case "repdt"
                                repDT = cnNode2.InnerText
                            Case "eenum"
                                eenum = cnNode2.InnerText
                            Case "ecamt"
                                ecamt = cnNode2.InnerText
                            Case "ssamt"
                                ssamt = cnNode2.InnerText
                            Case "provamt"
                                provamt = cnNode2.InnerText
                            Case "flexamt"
                                flexamt = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(repCd)
                arrResponse.Add(errorMessage)
                arrResponse.Add(eename)
                arrResponse.Add(fapId)
                arrResponse.Add(tapId)
                arrResponse.Add(iprnum)
                arrResponse.Add(pdfPath)
                arrResponse.Add(tsamt)
                arrResponse.Add(dueDate)

                arrResponse.Add(eenum)
                arrResponse.Add(ecamt)
                arrResponse.Add(ssamt)
                arrResponse.Add(provamt)
                arrResponse.Add(flexamt)

                Return True
            Else
                arrResponse.Add("2")
                arrResponse.Add("Get_loadIndividualPRNChange(): Execute_PRN_WSDL error " & ErrMsg)

                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("Get_loadIndividualPRNChange(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function Execute_PRN_WSDL(ByVal EndPoint As String, ByVal SoapMessage As String,
                                            ByRef SoapResponse As String, ByRef ErrMsg As String) As Boolean

        Dim Request As System.Net.WebRequest
        Dim Response As System.Net.WebResponse
        Dim DataStream As System.IO.Stream
        Dim Reader As System.IO.StreamReader
        Dim SoapByte() As Byte
        Dim SoapStr As String = SoapMessage
        Dim pSuccess As Boolean = True

        Try
            SoapByte = System.Text.Encoding.UTF8.GetBytes(SoapStr)

            Request = System.Net.WebRequest.Create(EndPoint)

            System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            Request.UseDefaultCredentials = True

            Request.ContentType = "text/xml; charset=utf-8"
            Request.ContentLength = SoapByte.Length
            Request.Method = "POST"


            DataStream = Request.GetRequestStream()
            DataStream.Write(SoapByte, 0, SoapByte.Length)
            DataStream.Close()

            Response = Request.GetResponse()
            DataStream = Response.GetResponseStream()
            Reader = New System.IO.StreamReader(DataStream)
            Dim SD2Request As String = Reader.ReadToEnd()

            DataStream.Close()
            Reader.Close()
            Response.Close()

            SoapResponse = SD2Request

            'SaveUpdateContactInfoSoapMessage("**SOAP MESSAGE REQUEST**" & vbNewLine & SoapMessage)
            'SaveUpdateContactInfoSoapMessage("**SOAP MESSAGE RESPONSE**" & vbNewLine & SoapResponse)

            Return True
        Catch ex As Exception
            ErrMsg = ex.Message
            Return False
        End Try
    End Function

    Public Shared Function updateContactMailAddr(ByVal ssnum As String,
                                                 ByVal email As String, ByVal faxno As String,
                                                 ByVal homeTel As String, ByVal mobile As String,
                                                 ByVal website As String, ByVal workTel As String,
                                                 ByVal brgyCd As String, ByVal house_lot As String,
                                                 ByVal street As String, ByVal subd As String, ByVal unit_bldg As String,
                                                 ByVal cntryCd As String, ByVal frgnAddr As String,
                                                 ByVal frgnCity As String, ByVal frgnZip As String,
                                                 ByVal IsMailingAddressHaveValue As Boolean,
                                                 ByVal IsForeignAddressHaveValue As Boolean,
                                                 ByVal IsContactInfoHaveValue As Boolean,
                                                 ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:updateContactMailAddr xmlns:target=""http://service.rcs.com/"">")
            SoapStr.Append("<arg0>" & UpdateCntctInfoService_SessionToken & "</arg0>")

            SoapStr.Append("<arg1>")
            SoapStr.Append("<email>" & email & "</email>")
            SoapStr.Append("<faxno>" & faxno & "</faxno>")
            SoapStr.Append("<homeTel>" & homeTel & "</homeTel>")
            SoapStr.Append("<mobile>" & mobile & "</mobile>")
            SoapStr.Append("<ssnum>" & IIf(IsContactInfoHaveValue, ssnum, "") & "</ssnum>")
            SoapStr.Append("<website>" & website & "</website>")
            SoapStr.Append("<workTel>" & workTel & "</workTel>")
            SoapStr.Append("</arg1>")

            SoapStr.Append("<arg2>")
            SoapStr.Append("<brgyCd>" & brgyCd & "</brgyCd>")
            SoapStr.Append("<house_lot>" & house_lot & "</house_lot>")
            SoapStr.Append("<ssnum>" & IIf(IsMailingAddressHaveValue, ssnum, "") & "</ssnum>")
            SoapStr.Append("<street>" & street & "</street>")
            SoapStr.Append("<subd>" & subd & "</subd>")
            SoapStr.Append("<unit_bldg>" & unit_bldg & "</unit_bldg>")
            SoapStr.Append("</arg2>")

            SoapStr.Append("<arg3>")
            SoapStr.Append("<cntryCd>" & cntryCd & "</cntryCd>")
            SoapStr.Append("<frgnAddr>" & frgnAddr & "</frgnAddr>")
            SoapStr.Append("<frgnCity>" & frgnCity & "</frgnCity>")
            SoapStr.Append("<frgnZip>" & frgnZip & "</frgnZip>")
            SoapStr.Append("<ssnum>" & IIf(IsForeignAddressHaveValue, ssnum, "") & "</ssnum>")
            SoapStr.Append("</arg3>")


            SoapStr.Append("</target:updateContactMailAddr>")
            SoapStr.Append("</env:Body></env:Envelope>")


            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(UpdateCntctInfoService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim replyCode As String = ""
                Dim remarks As String = ""
                Dim replyDate As String = ""
                Dim transNo As String = ""
                Dim pdf As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "replyCode"
                                replyCode = cnNode2.InnerText
                            Case "remarks"
                                remarks = cnNode2.InnerText
                            Case "replyDate"
                                replyDate = cnNode2.InnerText
                            Case "transNo"
                                transNo = cnNode2.InnerText
                            Case "url"
                                pdf = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(replyCode)
                arrResponse.Add(remarks)
                arrResponse.Add(transNo)
                arrResponse.Add(replyDate)
                arrResponse.Add(pdf)

                Return True
            Else
                arrResponse.Add("0")
                arrResponse.Add("updateContactMailAddr(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("updateContactMailAddr(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function webUpdateLocalAddr(ByVal brgyCd As String, ByVal house_lot As String,
                                                    ByVal ssnum As String, ByVal street As String,
                                                    ByVal subd As String, ByVal unit_bldg As String,
                                                    ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:webUpdateLocalAddr xmlns:target=""http://service.rcs.com/"">")
            SoapStr.Append("<arg0>" & UpdateCntctInfoService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>")
            SoapStr.Append("<brgyCd>" & brgyCd & "</brgyCd>")
            SoapStr.Append("<house_lot>" & house_lot & "</house_lot>")
            SoapStr.Append("<ssnum>" & ssnum & "</ssnum>")
            SoapStr.Append("<street>" & street & "</street>")
            SoapStr.Append("<subd>" & subd & "</subd>")
            SoapStr.Append("<unit_bldg>" & unit_bldg & "</unit_bldg>")
            SoapStr.Append("</arg1>")
            SoapStr.Append("</target:webUpdateLocalAddr>")
            SoapStr.Append("</env:Body></env:Envelope>")


            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(UpdateCntctInfoService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim replyCode As String = ""
                Dim remarks As String = ""
                Dim replyDate As String = ""
                Dim transNo As String = ""
                Dim pdf As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "replyCode"
                                replyCode = cnNode2.InnerText
                            Case "remarks"
                                remarks = cnNode2.InnerText
                            Case "replyDate"
                                replyDate = cnNode2.InnerText
                            Case "transNo"
                                transNo = cnNode2.InnerText
                            Case "url"
                                pdf = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(replyCode)
                arrResponse.Add(remarks)
                arrResponse.Add(transNo)
                arrResponse.Add(replyDate)
                arrResponse.Add(pdf)

                Return True
            Else
                arrResponse.Add("0")
                arrResponse.Add("webUpdateLocalAddr(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("webUpdateLocalAddr(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function webUpdateForeignAddr(ByVal cntryCd As String, ByVal frgnAddr As String,
                                                   ByVal frgnCity As String, ByVal frgnZip As String,
                                                   ByVal ssnum As String,
                                                   ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:webUpdateForeignAddr xmlns:target=""http://service.rcs.com/"">")
            SoapStr.Append("<arg0>" & UpdateCntctInfoService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>")
            SoapStr.Append("<cntryCd>" & cntryCd & "</cntryCd>")
            SoapStr.Append("<frgnAddr>" & frgnAddr & "</frgnAddr>")
            SoapStr.Append("<frgnCity>" & frgnCity & "</frgnCity>")
            SoapStr.Append("<frgnZip>" & frgnZip & "</frgnZip>")
            SoapStr.Append("<ssnum>" & ssnum & "</ssnum>")
            SoapStr.Append("</arg1>")
            SoapStr.Append("</target:webUpdateForeignAddr>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(UpdateCntctInfoService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim replyCode As String = ""
                Dim remarks As String = ""
                Dim replyDate As String = ""
                Dim transNo As String = ""
                Dim pdf As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "replyCode"
                                replyCode = cnNode2.InnerText
                            Case "remarks"
                                remarks = cnNode2.InnerText
                            Case "replyDate"
                                replyDate = cnNode2.InnerText
                            Case "transNo"
                                transNo = cnNode2.InnerText
                            Case "url"
                                pdf = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(replyCode)
                arrResponse.Add(remarks)
                arrResponse.Add(transNo)
                arrResponse.Add(replyDate)
                arrResponse.Add(pdf)

                Return True
            Else
                arrResponse.Add("0")
                arrResponse.Add("webUpdateForeignAddr(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("webUpdateForeignAddr(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    Public Shared Function webUpdateContact(ByVal email As String, ByVal faxno As String,
                                            ByVal homeTel As String, ByVal mobile As String,
                                            ByVal ssnum As String, ByVal website As String, ByVal workTel As String,
                                            ByRef response() As String) As Boolean
        Dim arrResponse As New List(Of String)

        Try
            Dim SoapStr As New System.Text.StringBuilder
            SoapStr.Append("<env:Envelope xmlns:env=""http://schemas.xmlsoap.org/soap/envelope/"">")
            SoapStr.Append("<env:Header /><env:Body>")
            SoapStr.Append("<target:webUpdateContact xmlns:target=""http://service.rcs.com/"">")
            SoapStr.Append("<arg0>" & UpdateCntctInfoService_SessionToken & "</arg0>")
            SoapStr.Append("<arg1>")
            SoapStr.Append("<email>" & email & "</email>")
            SoapStr.Append("<faxno>" & faxno & "</faxno>")
            SoapStr.Append("<homeTel>" & homeTel & "</homeTel>")
            SoapStr.Append("<mobile>" & mobile & "</mobile>")
            SoapStr.Append("<ssnum>" & ssnum & "</ssnum>")
            SoapStr.Append("<website>" & website & "</website>")
            SoapStr.Append("<workTel>" & workTel & "</workTel>")
            SoapStr.Append("</arg1>")
            SoapStr.Append("</target:webUpdateContact>")
            SoapStr.Append("</env:Body></env:Envelope>")

            Dim SoapResponse As String = ""
            Dim ErrMsg As String = ""

            If Execute_PRN_WSDL(UpdateCntctInfoService_URL, SoapStr.ToString, SoapResponse, ErrMsg) Then
                Dim x As New System.Xml.XmlDocument
                x.LoadXml(SoapResponse)

                Dim list As System.Xml.XmlNodeList = x.GetElementsByTagName("return")

                Dim replyCode As String = ""
                Dim remarks As String = ""
                Dim replyDate As String = ""
                Dim transNo As String = ""
                Dim pdf As String = ""

                For Each cnNode1 As System.Xml.XmlNode In list
                    For Each cnNode2 As System.Xml.XmlNode In cnNode1.ChildNodes
                        Select Case cnNode2.Name
                            Case "replyCode"
                                replyCode = cnNode2.InnerText
                            Case "remarks"
                                remarks = cnNode2.InnerText
                            Case "replyDate"
                                replyDate = cnNode2.InnerText
                            Case "transNo"
                                transNo = cnNode2.InnerText
                            Case "url"
                                pdf = cnNode2.InnerText
                        End Select
                    Next
                Next

                arrResponse.Add(replyCode)
                arrResponse.Add(remarks)
                arrResponse.Add(transNo)
                arrResponse.Add(replyDate)
                arrResponse.Add(pdf)

                Return True
            Else
                arrResponse.Add("0")
                arrResponse.Add("webUpdateContact(): Execute_PRN_WSDL error " & ErrMsg)
                Return False
            End If
        Catch ex As Exception
            arrResponse.Add("2")
            arrResponse.Add("webUpdateContact(): Runtime error catched " & ex.Message)
            Return False
        Finally
            response = arrResponse.ToArray
        End Try
    End Function

    'Private Shared Sub SaveUpdateContactInfoSoapMessage(ByVal data As String)
    '    Dim sw As New System.IO.StreamWriter(Application.StartupPath & "\UpdateContactInfoSoapRequest.txt", True)
    '    sw.Write(vbNewLine)
    '    sw.Write(data & vbNewLine)
    '    sw.Close()
    '    sw.Dispose()
    'End Sub

    Public Shared Function GetMemberDisclosureWS3() As Boolean

        'Dim ws As New WS_Disclosure.DisclosureWebserviceImplService
        'Dim WsResponse As WS_Disclosure.disclosureWsResponse

        Dim ws As New WS_Disclosure.DisclosureWebserviceImplService
        Dim WsResponse As WS_Disclosure.disclosureWsResponse
        Try
            'System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            'ws.UseDefaultCredentials = True
            'Dim TxnToken As String = ws.disAuthenticate(SSS_WS_TOKENID, 434343).transac_token.ToString()
            WsResponse = ws.calldisclosure("0226879523", "0000000000", "S", 1000, 2, "", 0, 10, "21P3GFLXQW4UNAXUI0Y7DNSUMBGY8C5Q9YD61F061918150521", SSS_WS_TOKENID, "000", "NCR")
            'WsResponse = ws.calldisclosure(SSSNo, ErNum, LoanType, LoanAmount, InstallmentTerm, UrIds, PrevLoanAmount, ServiceCharge, TxnToken, SSS_WS_TOKENID, ErSeqNo, Address)
            Response = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", "", WsResponse.msg, WsResponse.ln_amt, WsResponse.monthly_amort, WsResponse.net_proceeds, WsResponse.loanbal, WsResponse.path)


            Return True
        Catch ex As Exception
            Response = "Error|GetMemberDisclosureWS(): Runtime error catched " & ex.Message
            Return False
        Finally
            WsResponse = Nothing
            ws.Dispose()
            ws = Nothing
        End Try
    End Function

    Public Shared Function GetMemberDisclosureWS2() As Boolean

        Dim ws As New WS_Disclosure.DisclosureWebserviceImplService
        Dim WsResponse As WS_Disclosure.disclosureWsResponse
        Try
            'System.Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf AcceptAllCertifications
            'ws.UseDefaultCredentials = True
            Dim TxnToken As String = ws.disAuthenticate(SSS_WS_TOKENID).transac_token.ToString()
            WsResponse = ws.calldisclosure("0226879523", "0000000000", "S", 1000, 2, "", 0, 10, TxnToken, SSS_WS_TOKENID, "000", "NCR")
            ' WsResponse = ws.calldisclosure(SSSNo, ErNum, LoanType, LoanAmount, InstallmentTerm, UrIds, PrevLoanAmount, ServiceCharge, TxnToken, SSS_WS_TOKENID, ErSeqNo, Address)

            Response = String.Format("{0}|{1}|{2}|{3}|{4}|{5}|{6}", "", WsResponse.msg, WsResponse.ln_amt, WsResponse.monthly_amort, WsResponse.net_proceeds, WsResponse.loanbal, WsResponse.path)

            Return True
        Catch ex As Exception
            Response = "Error|GetMemberDisclosureWS(): Runtime error catched " & ex.Message
            Return False
        Finally
            WsResponse = Nothing
            ws.Dispose()
            ws = Nothing
        End Try
    End Function

    Public Shared Function GetDocumentValueByTag(ByVal tag As String) As String
        Dim webpageContent As String = IO.File.ReadAllText("D:\sl.txt")
        Dim startIndexTag As String = String.Format("name=""{0}""", tag)
        Dim startIndexTag2 As String = "value="
        Dim endIndexTag As String = """>"

        Dim varPosition1 As Integer = webpageContent.IndexOf(startIndexTag)
        Dim varPosition2 As Integer = webpageContent.IndexOf(startIndexTag2, (varPosition1 + 1))
        Dim varPosition3 As Integer = webpageContent.IndexOf(endIndexTag, (varPosition2 + 1))
        Dim result1 As String = webpageContent.Substring(varPosition2 + startIndexTag2.Length, varPosition3 - varPosition2)
        Dim result2 As String = result1.Substring(0, result1.IndexOf(endIndexTag)).Trim.Replace("""", "")

        Return result2
    End Function

    Public Shared Function GetDocumentValueByTag2(ByVal wbContent As String, ByVal tag As String) As String
        Dim startIndexTag As String = String.Format("name=""{0}""", tag)
        Dim startIndexTag2 As String = "value="
        Dim endIndexTag As String = """>"

        Dim varPosition1 As Integer = wbContent.IndexOf(startIndexTag)
        Dim varPosition2 As Integer = wbContent.IndexOf(startIndexTag2, (varPosition1 + 1))
        Dim varPosition3 As Integer = wbContent.IndexOf(endIndexTag, (varPosition2 + 1))
        Dim result1 As String = wbContent.Substring(varPosition2 + startIndexTag2.Length, varPosition3 - varPosition2)
        Dim result2 As String = result1.Substring(0, result1.IndexOf(endIndexTag)).Trim.Replace("""", "")

        Return result2
    End Function

    Public Shared Function ComputeServiceFee(ByVal amount As Decimal) As Decimal
        If amount > 0 Then
            Return amount * 0.01
        Else
            Return 0
        End If
    End Function

    Public Shared Function AcceptAllCertifications(ByVal sender As Object, ByVal certification As System.Security.Cryptography.X509Certificates.X509Certificate,
                                            ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain,
                                            ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function

    Public Shared Sub Clear_PRN_Sessions()
        PRN_MemberWithPRN = False
        PRN = ""
        PRN_MembershipType = ""
        PRN_Period_From = ""
        PRN_Period_To = ""
        PRN_MonthlyContribution = "0.00"
        PRN_TotalAmount = "0.00"
        PRN_DueDate = ""
        PRN_PDF = ""
        PRN_FlexiFund = "0.00"
    End Sub

    Public Shared Function Get_PRN_MembershipTypeCode(ByVal memberType As String) As String
        Select Case memberType.Trim.ToUpper.Replace("&nbsp", "").Replace(";", "")
            Case "Employed".ToUpper, "Covered Employee".ToUpper, "Employed".ToUpper 'PRN_MemberType_Employed.ToUpper
                Return "0"
            Case "Self-Employed".ToUpper 'PRN_MemberType_SelfEmployed.ToUpper
                Return "1"
            Case "Voluntary".ToUpper, "Voluntary Paying".ToUpper, "Voluntary Member".ToUpper 'PRN_MemberType_VoluntaryPaying.ToUpper
                Return "2"
            Case "Household Help".ToUpper 'PRN_MemberType_HouseHoldHelp.ToUpper
                Return "3"
            Case "Non-working Spouse".ToUpper 'PRN_MemberType_NonWorkingSpouse.ToUpper
                Return "4"
            Case "OFW".ToUpper, "Overseas Contract Worker".ToUpper, "OCW - Flexi Fund Member".ToUpper, "OCW-Flexi Fund Member".ToUpper 'PRN_MemberType_OverseasContractWorker.ToUpper"
                Return "5"
        End Select

        Return ""
    End Function

    Public Shared Function AppliedFrom() As String
        Return String.Format("{0}{1}{2}", "SSET", kioskID, kioskName)
    End Function

    'Public Shared Function IsDisplayDateOfSeparation(ByVal typeOfRetirement As String) As Boolean
    '    Select Case CHECK_MEMSTATUS_Settings.ToUpper
    '        Case "VOLUNTARY", "OFW" ', "NON-WORKING SPOUSE","NON WORKING SPOUSE", "covered employee"
    '            Return False
    '        Case Else
    '            If typeOfRetirement = "Optional Retirement" Then
    '                Return True
    '            Else
    '                Return False
    '            End If
    '    End Select
    'End Function

    Public Shared Function IsDisplayERCertification(ByVal No_of_months_flg As Integer, ByVal typeOfRetirement As String) As Boolean
        If No_of_months_flg = "0" Then
            Return False
        ElseIf No_of_months_flg = "1" And typeOfRetirement = "Optional Retirement" Then
            Return True
        End If
    End Function

    Public Shared Function Get_PRN_MembershipTypeDesc_ByPRN(ByVal PRN As String) As String
        Select Case PRN.Replace("&nbsp", "").Replace(";", "").Trim.Substring(0, 1).ToUpper
            Case "E"
                Return PRN_MemberType_Employed
            Case "S"
                Return PRN_MemberType_SelfEmployed
            Case "V"
                Return PRN_MemberType_VoluntaryPaying
            Case "H"
                Return PRN_MemberType_HouseHoldHelp
            Case "N"
                Return PRN_MemberType_NonWorkingSpouse
            Case "O"
                Return PRN_MemberType_OverseasContractWorker
            Case Else
                Return ""
        End Select
    End Function

    Public Enum monitorInch
        nineteenInch = 19
        twelveInch = 12
    End Enum

    Public Shared Function GetMonitorInch() As monitorInch
        '1280 x 1024 is 19inch
        '1024 x 768 (during UAT) (small screen)
        '1920 x 1080 (during PROD) (small screen)
        Dim width As Integer = SystemInformation.PrimaryMonitorSize.Width
        Dim height As Integer = SystemInformation.PrimaryMonitorSize.Height
        If width = 1280 And height = 1024 Then
            Return monitorInch.nineteenInch
        Else
            Return monitorInch.twelveInch
        End If
    End Function

    Public Shared Function GetMonitorInch_bak() As monitorInch
        '1280 x 1024 is 19inch
        '1024 x 768 (during UAT) (small screen)
        '1920 x 1080 (during PROD) (small screen)
        Dim width As Integer = SystemInformation.PrimaryMonitorSize.Width
        Dim height As Integer = SystemInformation.PrimaryMonitorSize.Height
        If width = 1920 And height = 1080 Then
            Return monitorInch.twelveInch
        Else
            Return monitorInch.nineteenInch
        End If
    End Function

    Public Shared Sub FormMode(ByRef parentControl As Control, ByVal intFormMode As Short, ByRef intSelected As Short)
        If parentControl Is Nothing Then Return
        If parentControl.Visible = False Then Return

        intSelected += 1
        For Each ctrl In parentControl.Controls("Panel15").Controls
            If TypeOf ctrl Is TextBox Then
                CType(ctrl, TextBox).BorderStyle = IIf(intFormMode = 0, Windows.Forms.BorderStyle.FixedSingle, Windows.Forms.BorderStyle.None)
                CType(ctrl, TextBox).ReadOnly = IIf(intFormMode = 0, False, True)
                CType(ctrl, TextBox).BackColor = Color.White
            ElseIf TypeOf ctrl Is LinkLabel Then
                If CType(ctrl, LinkLabel).Text = "CLEAR" Then CType(ctrl, LinkLabel).Visible = IIf(intFormMode = 0, True, False)
            ElseIf TypeOf ctrl Is Label Then
                If CType(ctrl, Label).Text = "*" Then CType(ctrl, Label).Visible = IIf(intFormMode = 0, True, False)
                If CType(ctrl, Label).Text.Contains("BLANK") Then CType(ctrl, Label).Visible = IIf(intFormMode = 0, True, False)
            End If
        Next
    End Sub

    Public Shared Function CheckTerminalConnections(ByVal xs As MySettings, ByVal db As ConnectionString) As Short
        Dim kiosk_Ip As String = xs.getIPAddress()

        Dim response As Short = 0

        Dim connstring1 As String = "DSN=" & db_DSN & ";SERVER=" & db_server & ";DATABASE=" & db_Name & ";UID=" & db_UName & ";PWD=" & db_Pass & ""

        If System.Net.NetworkInformation.NetworkInterface.GetIsNetworkAvailable() = False Then
            'ShowWarningMessage("THE SYSTEM IS NOT CONNECTED TO THE REMOTE SERVER. PLEASE MAKE SURE THE NETWORK CABLE IS CONNECTED.")
            response = 1
        ElseIf db.webisconnected(connstring1) = False Then
            'ShowWarningMessage("THE SYSTEM IS NOT CONNECTED TO THE REMOTE SERVER. PLEASE MAKE SURE THE NETWORK CABLE IS CONNECTED.")
            response = 1
        End If

        Return response
    End Function

    Private Shared Function Get_MonthDesc(ByVal monthNo As String) As String
        Select Case CShort(monthNo)
            Case 1
                Return "January"
            Case 2
                Return "February"
            Case 3
                Return "March"
            Case 4
                Return "April"
            Case 5
                Return "May"
            Case 6
                Return "June"
            Case 7
                Return "July"
            Case 8
                Return "August"
            Case 9
                Return "September"
            Case 10
                Return "October"
            Case 11
                Return "November"
            Case 12
                Return "December"
        End Select

        Return ""
    End Function

    Public Shared Sub ShowErrorForm(ByRef pnl As Panel)
        GC.Collect()
        '_frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
        pnl.Controls.Clear()
        _frmErrorForm.TopLevel = False
        _frmErrorForm.Parent = pnl
        _frmErrorForm.Dock = DockStyle.Fill
        _frmErrorForm.Show()
    End Sub


    Class MemberOtherInfo

        Public IsSuccess As Boolean
        Public ReadOnly ErrorMsg As String = ""

        Public ReadOnly vMAILING_ADDR As String = ""
        Public ReadOnly vFOREIGN_ADDR As String = ""

        Public ReadOnly vHSE_LBLK_NO As String = ""
        Public ReadOnly vUNIT_BLDG_NM As String = ""
        Public ReadOnly vSTREET As String = ""
        Public ReadOnly vSUBDIVISION As String = ""
        Public ReadOnly vBARANGAY_CD As String = ""
        Public ReadOnly vCITY_MUN_CD As String = ""
        Public ReadOnly vPROVINCE_CD As String = ""
        Public ReadOnly vPOST_CD As String = ""
        Public ReadOnly vCPNUMBER As String = ""
        Public ReadOnly vTELNO As String = ""
        Public ReadOnly vEMAIL As String = ""
        'Public ReadOnly vFOREIGN_ADDR As String = ""
        'Public ReadOnly vFOREIGN_CITY As String = ""
        'Public ReadOnly vFOREIGN_ZIP As String = ""
        'Public ReadOnly vCOUNTRY_CD As String = ""
        Public ReadOnly vMSG As String = ""

        Public Sub New()
            Dim DAL_Oracle As New DAL_Oracle
            Try
                Dim arrParam(5) As String
                If DAL_Oracle.PR_MEM_OTHERINFO_v2(arrParam) Then
                    vMAILING_ADDR = arrParam(0).Trim
                    vFOREIGN_ADDR = arrParam(1).Trim
                    vCPNUMBER = arrParam(2).Trim
                    vTELNO = arrParam(3).Trim
                    vEMAIL = arrParam(4).Trim
                    vMSG = arrParam(5).Trim

                    'vHSE_LBLK_NO = arrParam(0).Trim
                    'vUNIT_BLDG_NM = arrParam(1).Trim
                    'vSTREET = arrParam(2).Trim
                    'vSUBDIVISION = arrParam(3).Trim
                    'vBARANGAY_CD = arrParam(4).Trim
                    'vCITY_MUN_CD = arrParam(5).Trim
                    'vPROVINCE_CD = arrParam(6).Trim
                    'vPOST_CD = arrParam(7).Trim
                    'vCPNUMBER = arrParam(8).Trim
                    'vTELNO = arrParam(9).Trim
                    'vEMAIL = arrParam(10).Trim
                    'vFOREIGN_ADDR = arrParam(11).Trim
                    'vFOREIGN_CITY = arrParam(12).Trim
                    'vFOREIGN_ZIP = arrParam(13).Trim
                    'vCOUNTRY_CD = arrParam(14).Trim
                    'vMSG = arrParam(15).Trim
                Else
                    ErrorMsg = DAL_Oracle.ErrorMessage
                    IsSuccess = False
                    Return
                End If

                IsSuccess = True
            Catch ex As Exception
                ErrorMsg = ex.Message
                IsSuccess = False
            Finally
                DAL_Oracle = Nothing
            End Try
        End Sub

    End Class

    Public Shared Function ViewPDF(ByVal url As String, Optional ByVal pageNumber As String = "1", Optional zoomValue As String = "") As String
        Dim tempFile As String = Application.StartupPath & "\viewPDF.html"
        Dim zoomDocument As String = ""
        If zoomValue <> "" Then zoomDocument = "&zoom=" & zoomValue & "%"

        Dim sb As New System.Text.StringBuilder
        sb.Append("<!DOCTYPE html>")
        sb.Append("<html>")
        sb.Append("<body>")
        Dim f As String = String.Format("{0}#page={1}", url, pageNumber)
        sb.Append(String.Format("<iframe src=""{0}&toolbar=0&navpanes=0&scrollbar=1"" width=""100%"" height=""820px""></iframe>", String.Format("{0}#page={1}{2}", url, pageNumber, zoomDocument)))
        sb.Append("</body>")
        sb.Append("</html>")
        System.IO.File.WriteAllText(tempFile, sb.ToString)
        Return tempFile
    End Function

    Public Shared Function ViewPDF2(ByVal url As String, Optional ByVal pageNumber As String = "1", Optional zoomValue As String = "") As String
        Dim tempFile As String = Application.StartupPath & "\viewPDF.html"
        Dim zoomDocument As String = ""
        If zoomValue <> "" Then zoomDocument = "&zoom=" & zoomValue & "%"

        Dim sb As New System.Text.StringBuilder
        sb.Append("<!DOCTYPE html>")
        sb.Append("<html>")
        sb.Append("<body>")
        Dim f As String = String.Format("{0}#page={1}", url, pageNumber)
        sb.Append(String.Format("<iframe src=""{0}&toolbar=0&navpanes=0&scrollbar=1"" width=""120%"" height=""1020px""></iframe>", String.Format("{0}#page={1}{2}", url, pageNumber, zoomDocument)))
        sb.Append("</body>")
        sb.Append("</html>")
        System.IO.File.WriteAllText(tempFile, sb.ToString)
        Return tempFile
    End Function

    Public Shared Function DisablePinOrFingerprint() As Boolean
        Return IO.File.Exists(Application.StartupPath & "\nopin.txt")
    End Function

    Public Shared Function GetCurrentAge(ByVal dob As Date) As Integer
        Dim age As Integer
        age = Today.Year - dob.Year
        If (dob > Today.AddYears(-age)) Then age -= 1
        Return age
    End Function

    Public Shared Function FormatDataWithCharLength(ByVal value As String, ByVal intCharLength As Short, ByRef IsErrorFlag As Boolean) As String
        Dim space As String = " "

        Dim sbOld As New System.Text.StringBuilder
        sbOld.Append(value)

        Dim sbNew As New System.Text.StringBuilder

        'check loop to see if it exceeds normal number
        Dim intLoop As Short = 0
        Dim intNormalLoop As Short = 15

        Do While sbOld.ToString <> ""
            Dim intSpaceLastIndex As Short
            If sbOld.ToString.Length > intCharLength Then
                If sbOld.ToString.Substring(intCharLength - 1, 1) = space Then
                    sbNew.AppendLine(Trim(sbOld.ToString.Substring(0, intCharLength)))
                    sbOld.Remove(0, intCharLength)
                ElseIf sbOld.ToString.Substring(intCharLength - 1, 1) = vbLf Then
                    sbNew.AppendLine(Trim(sbOld.ToString.Substring(0, intCharLength)))
                    sbOld.Remove(0, intCharLength)
                ElseIf sbOld.ToString.Substring(intCharLength - 1, 1) = vbCrLf Then
                    sbNew.AppendLine(Trim(sbOld.ToString.Substring(0, intCharLength)))
                    sbOld.Remove(0, intCharLength)
                ElseIf sbOld.ToString.Substring(intCharLength - 1, 1) <> space And sbOld.ToString.Substring(intCharLength, 1) <> space Then
                    intSpaceLastIndex = sbOld.ToString.Substring(0, intCharLength).LastIndexOf(space)
                    sbNew.AppendLine(Trim(sbOld.ToString.Substring(0, intSpaceLastIndex)))
                    sbOld.Remove(0, intSpaceLastIndex)
                Else
                    sbNew.AppendLine(Trim(sbOld.ToString.Substring(0, intCharLength)))
                    sbOld.Remove(0, intCharLength)
                End If

                intLoop += 1

                If intLoop > intNormalLoop Then
                    IsErrorFlag = True
                    Return ""
                End If
            Else
                sbNew.Append(Trim(sbOld.ToString.Substring(0)))
                sbOld.Clear()
            End If
        Loop

        IsErrorFlag = False
        Return sbNew.ToString
    End Function

    Public Shared Function GetPostalCodeFromAddress(ByVal address As String) As String
        Dim postalCode As String = ""

        Try
            Dim spaceLastIndex As Integer = address.LastIndexOf(" ")
            If spaceLastIndex <> -1 Then postalCode = address.Substring(spaceLastIndex + 1)
        Catch ex As Exception
        End Try

        Return postalCode
    End Function

    Public Shared Function callLoanEligibRejectReason() As String
        Dim reason As String = ""

        Dim eligib As New SalaryLoan.eligibwebservice
        If eligib.calleligibility(SSStempFile, "", "") Then
            If eligib.calleligibilityResponse.appl_st <> "Qualified" Then
                Dim sbRejectReason As New System.Text.StringBuilder
                For Each reject As EligibilityWebserviceImplService.rejection In eligib.calleligibilityResponse.rej_dtls
                    sbRejectReason.Append(reject.reasons.Trim & vbNewLine)
                Next

                reason = sbRejectReason.ToString
            End If
        End If

        Return reason
    End Function

    Public Shared Sub DropdownSelectedValue(ByRef cbo As ComboBox)
        If cbo.Items.Count = 2 Then
            cbo.SelectedIndex = 1
            cbo.Enabled = False
        Else
            cbo.SelectedIndex = 0
            cbo.Enabled = True
        End If
    End Sub

    Public Shared Function HandleUnableToConnectRemoteServerError(ByVal exception As String, ByVal response As Short) As Short
        If exception.Contains("The remote name could not be resolved") Then
            Return 1
        ElseIf exception.Contains("Unable to connect to remote server") Then
            Return 1
        Else
            Return response
        End If
    End Function

    Public Shared Function FormatERBRN(ByVal erBrn As String) As String
        If erBrn.Trim.Length = 3 Then
            Return erBrn.Trim
        Else
            Return erBrn.Trim.PadLeft(3, "0")
        End If
    End Function

    Public Shared Sub ZoomFunction(ByVal isZoomEnable As Boolean)
        Dim regPath As String = "HKEY_CURRENT_USER\SOFTWARE\Microsoft\Internet Explorer\Zoom"
        Dim value As Integer = 1
        Dim regZoomDisabled As String = "ZoomDisabled"
        If isZoomEnable Then value = 0
        If My.Computer.Registry.CurrentUser.OpenSubKey(String.Format("{0}\{1}", regPath, regZoomDisabled)) Is Nothing Then
            My.Computer.Registry.CurrentUser.CreateSubKey(String.Format("{0}\{1}", regPath, regZoomDisabled))
            My.Computer.Registry.SetValue(regPath, "ZoomDisabled", value, Microsoft.Win32.RegistryValueKind.DWord)
        Else
            My.Computer.Registry.SetValue(regPath, "ZoomDisabled", value, Microsoft.Win32.RegistryValueKind.DWord)
        End If
    End Sub

    Public Shared Sub DownloadTechnicalRetirementRequirementDoc()
        Dim wc As New System.Net.WebClient
        Try
            System.Net.ServicePointManager.Expect100Continue = True
            System.Net.ServicePointManager.SecurityProtocol = DirectCast(3072, System.Net.SecurityProtocolType)
            wc.DownloadFile(RetirementBenefitDocReq_URL, technicalRetirementReqDoc)
        Catch ex As Exception
        Finally
            wc.Dispose()
        End Try
    End Sub

End Class
