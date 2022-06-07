
Public Class AllcardUMID

    'tested
    Public Shared Function ReadData(ByRef data As String,
                                    Optional ByVal ErrorMessage As String = "") As Boolean
        Dim sb As New System.Text.StringBuilder

        Try
            Dim sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing
            Init(sc)

            Dim _CRN As String = GetCRN(ErrorMessage, sc)

            Try
                If _CRN = "" Then _CRN = GetCRN_QC(ErrorMessage)

                sb.Append(_CRN & "|") '0
                sb.Append(GetCSN(ErrorMessage, sc) & "|") '1
                sb.Append(GetCCDT(ErrorMessage, sc) & "|") '2
                sb.Append(GetCardStatus(ErrorMessage, sc) & "|") '3              

                data = sb.ToString

                Return True
            Catch ex As Exception
                ErrorMessage = String.Format("ReadData(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("ReadData(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function GetCRNandCCDTandStatus(ByRef CRN As String, ByRef CCDT As String, ByRef CardStatus As String,
                                                  Optional ByVal ErrorMessage As String = "") As Boolean
        Dim sb As New System.Text.StringBuilder

        Try
            Dim sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing
            Init(sc)

            Dim _CRN As String = GetCRN(ErrorMessage, sc)

            Try
                If _CRN = "" Then _CRN = GetCRN_QC(ErrorMessage)

                CRN = _CRN
                CCDT = GetCCDT(ErrorMessage, sc)
                CardStatus = GetCardStatus(ErrorMessage, sc)

                Return True
            Catch ex As Exception
                ErrorMessage = String.Format("GetCRNandCCDTandStatus(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("GetCRNandCCDTandStatus(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function GetCRNandCCDTandStatus2(ByRef CRN As String, ByRef CCDT As String, ByRef CardStatus As String,
                                                   ByRef BlockSSNumber As String, Optional ByVal ErrorMessage As String = "") As Boolean
        Dim sb As New System.Text.StringBuilder

        Try
            Dim sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing
            Init(sc)

            AppletVersion = CheckAppVersion(ErrorMessage, sc)

            Dim _CRN As String = GetCRN(ErrorMessage, sc)

            Try
                If _CRN = "" Then _CRN = GetCRN_QC(ErrorMessage)

                CRN = _CRN
                CCDT = GetCCDT(ErrorMessage, sc)
                BlockSSNumber = ReadSector36(ErrorMessage, sc)

                'If AppletVersion = "1" Then
                If Not GetCRNAndFingerprints2("", ErrorMessage) Then
                    CardStatus = "Error"
                End If
                'End If

                CardStatus = GetCardStatus(ErrorMessage, sc)

                Return True
            Catch ex As Exception
                ErrorMessage = String.Format("GetCRNandCCDTandStatus(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("GetCRNandCCDTandStatus(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    'tested
    Public Shared Function GetFName(Optional ByRef ErrorMessage As String = "", _
                                     Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            If sc.AuthenticateSL1() Then
                Return Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FIRST_NAME))
            Else
                ErrorMessage = "GetFName(): SL1 failed"
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("GetFName(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetCRN(Optional ByVal ErrorMessage As String = "", Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Dim _CRN As String = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CRN))

            Try
                If _CRN = "" Then _CRN = GetCRN_QC(ErrorMessage)
            Catch ex As Exception
                ErrorMessage = String.Format("GetCRN(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End Try

            Return _CRN
        Catch ex As Exception
            ErrorMessage = String.Format("GetCRN(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetCSN(Optional ByVal ErrorMessage As String = "", Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Try
                Return Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CSN))
            Catch ex As Exception
                ErrorMessage = String.Format("GetCSN(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("GetCSN(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetCardStatus(ByRef ErrorMessage As String, Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Dim status As String = ""
            If sc.GetCardStatus(status) Then
                Return status
            Else
                'If status = "" Then
                '    Return "CARD_BLOCKED"
                'Else
                Return "Error"
                'End If
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("GetCardStatus(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage.Replace(vbNewLine, " ") & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetCCDT(Optional ByRef ErrorMessage As String = "", Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Dim CCDT As String

            Try
                Dim tByte(0) As Byte
                Dim CCDT_ASCII As String = System.Text.ASCIIEncoding.ASCII.GetString(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CARD_CREATION_DATE)).Replace(System.Text.ASCIIEncoding.ASCII.GetString(tByte), "").Trim
                Dim CCDT_BCD As String = ByteArrayToHexString(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CARD_CREATION_DATE)).Replace(System.Text.ASCIIEncoding.ASCII.GetString(tByte), "").Trim
                Try
                    IsDate(String.Format("{0}/{1}/{2}", CCDT_ASCII.Substring(4, 2), CCDT_ASCII.Substring(6, 2), CCDT_ASCII.Substring(0, 4)))
                    CCDT = CCDT_ASCII
                Catch ex As Exception
                    CCDT = CCDT_BCD.Substring(0, 8)
                End Try

                If CCDT = "00000000" Then CCDT = Get_CardCreationDate_QC(ErrorMessage)

                Return CCDT
            Catch ex As Exception
                ErrorMessage = String.Format("GetCCDT(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("GetCCDT(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function Get_CardCreationDate_QC(Optional ByRef ErrorMessage As String = "") As String
        Try
            Dim _ErrorMessage As Byte() = New Byte(1023) {}
            Dim Data(1023) As Byte
            Dim Result As Boolean = False

            If UMIDSAM_QC.UMIDCard_Autn_QC(_ErrorMessage) Then
                Result = UMIDSAM_QC.UMIDCard_Read_CardCreationDate_QC(Data, _ErrorMessage)
                If Result Then
                    Return DetermineRespose(Result, Data, _ErrorMessage)
                Else
                    ErrorMessage = String.Format("Get_CardCreationDate_QC(): Error catched {0}", DetermineRespose(Result, Data, _ErrorMessage))
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    Return "Error"
                End If
            Else
                ErrorMessage = String.Format("Get_CardCreationDate_QC(): QC authentication failed")
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("Get_CardCreationDate_QC(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetCRN_QC(ByRef ErrorMessage As String) As String
        Try
            Dim _ErrorMessage As Byte() = New Byte(1023) {}
            Dim Data(1023) As Byte
            Dim Result As Boolean

            If UMIDSAM_QC.UMIDCard_Autn_QC(_ErrorMessage) Then
                Result = UMIDSAM_QC.UMIDCard_Read_CRN_QC(Data, _ErrorMessage)
                If Result Then
                    Return DetermineRespose(Result, Data, _ErrorMessage)
                Else
                    ErrorMessage = String.Format("GetCRN_QC(): Error catched {0}", DetermineRespose(Result, Data, _ErrorMessage))
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    Return "Error"
                End If
            Else
                ErrorMessage = String.Format("GetCRN_QC(): QC authentication failed")
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("GetCRN_QC(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function ActivateCard(Optional ByRef ErrorMessage As String = "",
                                        Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
        Try
            Init(sc)
            'pblcSC.SelectApplet(My.Settings.UMID, My.Settings.SAM)

            Dim result As Boolean

            AppletVersion = CheckAppVersion(ErrorMessage, sc)

            Select Case AppletVersion 'CheckAppVersion(ErrorMessage, sc)
                Case AllcardUMID.UMIDType.UMID_OLD
                    Try
                        If ConnectSmartCard() Then
                            Application.DoEvents()
                            If ConnectSAM() Then
                                Application.DoEvents()
                                ConnectUMID()
                            End If
                        End If
                    Catch ex As Exception
                        ErrorMessage = String.Format("ActivateCard(2): Runtime error catched {0}", ex.Message)
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End Try

                    Dim Data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(GetUMIDCardPIN(ErrorMessage, sc))

                    result = sc.UMIDCard_Activate(Data, Data.Length)

                    If result Then
                        result = True
                    Else
                        ErrorMessage = "Old card - Activation failed"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(3):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End If
                Case AllcardUMID.UMIDType.UMID_NEW
                    If sc.AuthenticateSL1() Then
                        Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"

                        result = sc.getUmidFile(Path_Fingerprint_LP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_PRIMARY_FINGER)

                        If result Then
                            Dim Data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(GetUMIDCardPIN)

                            result = sc.UMIDCard_Activate(Data, Data.Length)

                            If result Then
                                Return True
                            Else
                                ErrorMessage = "New card - Activation failed"
                                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(4):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                                result = False
                            End If
                        Else
                            ErrorMessage = "Activation failed. New card - get fingerprint failed"
                            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(5):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                            result = False
                        End If
                    Else
                        ErrorMessage = "Activation failed. New card - SL1 failed"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(6):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End If
                Case "Error"
                    ErrorMessage = "Checking of applet version failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(7):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    result = False
                Case Else
                    ErrorMessage = "Checking of applet version failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(8):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    result = False
            End Select

            Return result
        Catch ex As Exception
            ErrorMessage = String.Format("ActivateCard(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function ActivateCardv2(Optional ByRef ErrorMessage As String = "",
                                          Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing,
                                          Optional pin As String = "") As Boolean
        Try
            Init(sc)
            'pblcSC.SelectApplet(My.Settings.UMID, My.Settings.SAM)

            Dim result As Boolean

            AppletVersion = CheckAppVersion(ErrorMessage, sc)

            Select Case AppletVersion 'CheckAppVersion(ErrorMessage, sc)
                Case AllcardUMID.UMIDType.UMID_OLD
                    Try
                        If ConnectSmartCard() Then
                            Application.DoEvents()
                            If ConnectSAM() Then
                                Application.DoEvents()
                                ConnectUMID()
                            End If
                        End If
                    Catch ex As Exception
                        ErrorMessage = String.Format("ActivateCard(2): Runtime error catched {0}", ex.Message)
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End Try

                    Dim Data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes("123456")

                    result = sc.UMIDCard_Activate(Data, Data.Length)

                    If result Then
                        result = True
                    Else
                        ErrorMessage = "Old card - Activation failed"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(3):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End If
                Case AllcardUMID.UMIDType.UMID_NEW
                    If sc.AuthenticateSL1() Then
                        Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"

                        result = sc.getUmidFile(Path_Fingerprint_LP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_PRIMARY_FINGER)

                        If result Then
                            Dim Data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(pin)

                            result = sc.UMIDCard_Activate(Data, Data.Length)

                            If result Then
                                Return True
                            Else
                                ErrorMessage = "New card - Activation failed"
                                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(4):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                                result = False
                            End If
                        Else
                            ErrorMessage = "Activation failed. New card - get fingerprint failed"
                            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(5):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                            result = False
                        End If
                    Else
                        ErrorMessage = "Activation failed. New card - SL1 failed"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(6):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End If
                Case "Error"
                    ErrorMessage = "Checking of applet version failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(7):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    result = False
                Case Else
                    ErrorMessage = "Checking of applet version failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|ActivateCard(8):" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    result = False
            End Select

            Return result
        Catch ex As Exception
            ErrorMessage = String.Format("ActivateCard(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    'tested

    Public Shared Function BlockUMIDCard(ByVal PIN As String, Optional ByRef ErrorMessage As String = "",
                                         Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
        Try
            Return True

            Init(sc)

            Dim result As Boolean

            If sc.AuthenticateSL1() Then
                Dim bytePin() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(PIN)

                If sc.AuthenticateSL2(bytePin) Then
                    result = sc.ApplicationBlock()

                    If result Then
                        Return True
                    Else
                        ErrorMessage = "BlockUMIDCard(): Failed to block card"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        result = False
                    End If
                Else
                    ErrorMessage = "BlockUMIDCard(): SL2 failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    result = False
                End If
            Else
                ErrorMessage = "BlockUMIDCard(): SL1 failed"
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                result = False
            End If

            Return result
        Catch ex As Exception
            ErrorMessage = String.Format("BlockUMIDCard(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function ChangePINv2(ByVal oldPin As String, ByVal newPin As String, ByRef ErrorMessage As String, Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
        Init(sc)

        Try
            If AppletVersion = AllcardUMID.UMIDType.UMID_OLD Then
                If sc.AuthenticateSL1() Then
                    If Not sc.AuthenticateSL2(Util.AsciiToByteArray(oldPin)) Then
                        Return False
                    End If
                Else
                    Return False
                End If
            End If

            Return sc.UMIDCard_Change_PIN(oldPin, newPin)
        Catch ex As Exception
            ErrorMessage = String.Format("ChangePIN(): Runtime error {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)
        End Try
    End Function

    Public Shared Function GetCCDTAndSector36(ByRef _CCDT As String, ByRef Sector36 As String, ByRef ErrorMessage As String) As Boolean
        Try
            Dim sc As New UMIDLibrary.AllCardTech_Smart_Card()
            sc.InitializeReaders()
            sc.SelectApplet(My.Settings.UMID, My.Settings.SAM)
            Dim tByte(0) As Byte

            If sc.AuthenticateSL1 Then
                Try
                    Dim CCDT_ASCII As String = System.Text.ASCIIEncoding.ASCII.GetString(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CARD_CREATION_DATE)).Replace(System.Text.ASCIIEncoding.ASCII.GetString(tByte), "").Trim
                    Dim CCDT_BCD As String = ByteArrayToHexString(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CARD_CREATION_DATE)).Replace(System.Text.ASCIIEncoding.ASCII.GetString(tByte), "").Trim
                    Try
                        IsDate(String.Format("{0}/{1}/{2}", CCDT_ASCII.Substring(4, 2), CCDT_ASCII.Substring(6, 2), CCDT_ASCII.Substring(0, 4)))
                        _CCDT = CCDT_ASCII
                    Catch ex As Exception
                        _CCDT = CCDT_BCD.Substring(0, 8)
                    End Try

                    If _CCDT = "00000000" Then _CCDT = Get_CardCreationDate_QC(ErrorMessage)
                Catch ex As Exception
                    _CCDT = ""
                End Try

                Dim byteSector36() As Byte = sc.ReadSector(36, 0, 32)

                If CInt(byteSector36.GetValue(0)) > 0 Then
                    Sector36 = System.Text.ASCIIEncoding.ASCII.GetString(byteSector36)
                Else
                    Sector36 = "NO_DATA"
                End If
            Else
                ErrorMessage = String.Format("GetCCDTAndSector36(): SL1 failed")
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End If

            Return True
        Catch ex As Exception
            ErrorMessage = String.Format("GetCCDTAndSector36(): Runtime error {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function ReadSector36(Optional ByRef ErrorMessage As String = "", _
                                        Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Dim byteSector36() As Byte = sc.ReadSector(36, 0, 32)

            If CInt(byteSector36.GetValue(0)) > 0 Then
                Return System.Text.ASCIIEncoding.ASCII.GetString(byteSector36)
            Else
                Return "NO_DATA"
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("ReadSector36(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'check with jp
    Public Shared Function WriteSectorData(ByVal intSectorID As Integer, ByVal strData As String, _
                                           Optional ByRef ErrorMessage As String = "", _
                                           Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
        Try
            Init(sc)

            Dim intSectorLength As Integer = GetSectorLengthv2(intSectorID)

            If strData.Length <= intSectorLength Then
                strData = strData.PadRight(intSectorLength, " ")
            Else
                ErrorMessage = String.Format("WriteSectorData(): Invalid data length. Data length {0}, Sector length {1}", strData.Length, intSectorLength)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End If

            Dim Data() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(strData)

            Return sc.WriteSector(intSectorID, 0, Data.Length, Data)
        Catch ex As Exception
            ErrorMessage = String.Format("WriteSectorData(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Private Shared Function GetSectorLength(ByVal intSectorID As Integer, Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Integer
        Init(sc)

        Select Case AppletVersion ' CheckAppVersion(, sc)
            Case AllcardUMID.UMIDType.UMID_OLD
                Return 32
            Case AllcardUMID.UMIDType.UMID_NEW
                If intSectorID = 1 Then
                    Return 1
                ElseIf intSectorID = 37 Then
                    Return 1
                ElseIf intSectorID = 73 Then
                    Return 1
                ElseIf intSectorID = 109 Then
                    Return 1
                ElseIf intSectorID = 2 Then
                    Return 512
                ElseIf intSectorID = 38 Then
                    Return 512
                ElseIf intSectorID = 74 Then
                    Return 512
                ElseIf intSectorID = 110 Then
                    Return 512
                ElseIf intSectorID >= 7 And intSectorID <= 16 Then
                    Return 128
                ElseIf intSectorID >= 43 And intSectorID <= 52 Then
                    Return 128
                ElseIf intSectorID >= 79 And intSectorID <= 124 Then
                    Return 128
                ElseIf intSectorID >= 3 And intSectorID <= 6 Then
                    Return 256
                ElseIf intSectorID >= 39 And intSectorID <= 42 Then
                    Return 256
                ElseIf intSectorID >= 75 And intSectorID <= 78 Then
                    Return 256
                ElseIf intSectorID >= 111 And intSectorID <= 114 Then
                    Return 256
                Else
                    Return 64
                End If
        End Select
    End Function

    Public Shared Function GetSectorLengthv2(ByVal intSectorID As Integer) As Integer
        'Init(sc)

        Select Case AppletVersion ' CheckAppVersion(, sc)
            Case AllcardUMID.UMIDType.UMID_OLD
                Return 32
            Case AllcardUMID.UMIDType.UMID_NEW
                If intSectorID = 1 Then
                    Return 1
                ElseIf intSectorID = 37 Then
                    Return 1
                ElseIf intSectorID = 73 Then
                    Return 1
                ElseIf intSectorID = 109 Then
                    Return 1
                ElseIf intSectorID = 2 Then
                    Return 512
                ElseIf intSectorID = 38 Then
                    Return 512
                ElseIf intSectorID = 74 Then
                    Return 512
                ElseIf intSectorID = 110 Then
                    Return 512
                ElseIf intSectorID >= 7 And intSectorID <= 16 Then
                    Return 128
                ElseIf intSectorID >= 43 And intSectorID <= 52 Then
                    Return 128
                ElseIf intSectorID >= 79 And intSectorID <= 124 Then
                    Return 128
                ElseIf intSectorID >= 3 And intSectorID <= 6 Then
                    Return 256
                ElseIf intSectorID >= 39 And intSectorID <= 42 Then
                    Return 256
                ElseIf intSectorID >= 75 And intSectorID <= 78 Then
                    Return 256
                ElseIf intSectorID >= 111 And intSectorID <= 114 Then
                    Return 256
                Else
                    Return 64
                End If
        End Select
    End Function

    'tested
    Public Shared Function CheckAppVersion(Optional ByRef ErrorMessage As String = "", _
                                           Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            Try
                Return sc.CheckVersion()
            Catch ex As Exception
                ErrorMessage = String.Format("CheckAppVersion(2): Runtime error catched {0}", ex.Message)
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return "Error"
            End Try
        Catch ex As Exception
            ErrorMessage = String.Format("CheckAppVersion(1): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    'tested
    Public Shared Function GetUMIDCardPIN(Optional ByRef ErrorMessage As String = "", _
                                          Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As String
        Try
            Init(sc)

            AppletVersion = CheckAppVersion(ErrorMessage, sc)

            Select Case AppletVersion 'CheckAppVersion(, sc)
                Case AllcardUMID.UMIDType.UMID_OLD
                    Return "123456"
                Case AllcardUMID.UMIDType.UMID_NEW
                    Return "12345678"
                    'Return "123456"
                Case "Error"
                    Return "Error"
                Case Else
                    Return "Error"
            End Select
        Catch ex As Exception
            ErrorMessage = String.Format("GetUMIDCardPIN(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return "Error"
        End Try
    End Function

    Public Shared Function DetermineRespose(ByVal Result As Boolean, ByRef Data() As Byte, ByRef Err() As Byte) As String
        Try

            Dim NullByte(0) As Byte

            If Result Then
                Return System.Text.ASCIIEncoding.ASCII.GetString(Data).Replace(System.Text.ASCIIEncoding.ASCII.GetString(NullByte), "").Trim
            Else
                Return System.Text.ASCIIEncoding.ASCII.GetString(Err).Replace(System.Text.ASCIIEncoding.ASCII.GetString(NullByte), "").Trim
            End If

        Catch ex As Exception
            Return "Unable to read data."
        End Try

    End Function

    Public Shared Function ConnectSmartCard() As Boolean
        Dim ErrorMessage As Byte() = New Byte(1023) {}
        Dim Result As Boolean = False

        Result = UMIDSAM_QC.SmartReader_Connect_Debug(My.Settings.UMID, My.Settings.SAM, ErrorMessage)

        Return Result
    End Function

    Public Shared Function ConnectSAM() As Boolean

        Dim ErrorMessage As Byte() = New Byte(1023) {}
        Dim Result As Boolean = False

        Result = UMIDSAM_QC.UMIDSAM_Connect(ErrorMessage)

        Return Result

    End Function

    Public Shared Function ConnectUMID() As Boolean

        Dim ErrorMessage As Byte() = New Byte(1023) {}
        Dim Result As Boolean = False

        Result = UMIDSAM_QC.UMIDCard_Connect(ErrorMessage)

        Return Result

    End Function

    Public Shared Function DisConnectUMID() As Boolean
        UMIDSAM_QC.UMIDCard_DisConnect()
    End Function

    'Public Shared Function GetCRNAndFingerprints(ByRef CRN As String,
    '                                             Optional ByVal ErrorMessage As String = "", _
    '                                             Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
    '    Init(sc)

    '    Try
    '        Dim sb As New System.Text.StringBuilder

    '        Dim result1 As Boolean = True
    '        Dim result2 As Boolean = True
    '        Dim result3 As Boolean = True
    '        Dim result4 As Boolean = True

    '        If sc.AuthenticateSL1() Then
    '            'CRN = GetCRN(ErrorMessage, sc2)

    '            'Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"
    '            Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"
    '            Dim Path_Fingerprint_RP As String = "Temp\RP.ansi-fmr"
    '            Dim Path_Fingerprint_LB As String = "Temp\LB.ansi-fmr"
    '            Dim Path_Fingerprint_RB As String = "Temp\RB.ansi-fmr"

    '            If Not sc.getUmidFile(Path_Fingerprint_LP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_PRIMARY_FINGER) Then
    '                sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get left primary finger") & ".")
    '                result1 = False
    '            End If

    '            If Not sc.getUmidFile(Path_Fingerprint_RP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_PRIMARY_FINGER) Then
    '                sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get right primary finger") & ".")
    '                result2 = False
    '            End If

    '            If Not sc.getUmidFile(Path_Fingerprint_LB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_SECONDARY_FINGER) Then
    '                sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get left secondary finger") & ".")
    '                result3 = False
    '            End If

    '            If Not sc.getUmidFile(Path_Fingerprint_RB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_SECONDARY_FINGER) Then
    '                sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get right secondary finger") & ".")
    '                result4 = False
    '            End If

    '            ErrorMessage = sb.ToString

    '            If Not result1 And Not result2 And Not result3 And Not result4 Then
    '                Return False
    '            Else
    '                Return True
    '            End If
    '        Else
    '            'SharedFunction.ShowMessage("SL1 FAILED")

    '            ErrorMessage = String.Format("GetCRNAndFingerprints(): SL1 failed")
    '            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

    '            Return False
    '        End If
    '    Catch ex As Exception
    '        ErrorMessage = String.Format("GetCRNAndFingerprints(): Runtime error catched {0}", ex.Message)
    '        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

    '        Return False
    '    End Try
    'End Function

    Public Shared Function GetCRNAndFingerprints2(ByRef CRN As String,
                                                 Optional ByVal ErrorMessage As String = "", _
                                                 Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
       Init(sc)

        Try
            Dim sb As New System.Text.StringBuilder

            Dim result1 As Boolean = True
            Dim result2 As Boolean = True
            Dim result3 As Boolean = True
            Dim result4 As Boolean = True

            If sc.AuthenticateSL1() Then
                'CRN = GetCRN(ErrorMessage, sc2)

                'Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"
                Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"
                Dim Path_Fingerprint_RP As String = "Temp\RP.ansi-fmr"
                Dim Path_Fingerprint_LB As String = "Temp\LB.ansi-fmr"
                Dim Path_Fingerprint_RB As String = "Temp\RB.ansi-fmr"

                If Not sc.getUmidFile(Path_Fingerprint_LP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_PRIMARY_FINGER) Then
                    sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get left primary finger") & ".")
                    result1 = False
                End If

                If Not sc.getUmidFile(Path_Fingerprint_RP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_PRIMARY_FINGER) Then
                    sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get right primary finger") & ".")
                    result2 = False
                End If

                If Not sc.getUmidFile(Path_Fingerprint_LB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_SECONDARY_FINGER) Then
                    sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get left secondary finger") & ".")
                    result3 = False
                End If

                If Not sc.getUmidFile(Path_Fingerprint_RB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_SECONDARY_FINGER) Then
                    sb.Append(String.Format("GetCRNAndFingerprints(): Failed to get right secondary finger") & ".")
                    result4 = False
                End If

                ErrorMessage = sb.ToString

                If Not result1 And Not result2 And Not result3 And Not result4 Then
                    Return False
                Else
                    Return True
                End If
            Else
                'SharedFunction.ShowMessage("SL1 FAILED")

                ErrorMessage = String.Format("GetCRNAndFingerprints(): SL1 failed")
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("GetCRNAndFingerprints(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

    Public Shared Function ChangePIN(ByVal oldPIN As String, ByVal newPIN As String, _
                                     Optional ByVal ErrorMessage As String = "", _
                                     Optional ByVal sc As UMIDLibrary.AllCardTech_Smart_Card = Nothing) As Boolean
        Try
            Init(sc)

            Dim result As Boolean

            If sc.AuthenticateSL1() Then
                If Not sc.AuthenticateSL2(System.Text.ASCIIEncoding.ASCII.GetBytes(oldPIN)) Then
                    ErrorMessage = "ChangePIN(): SL2 failed"
                    SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                    Return False
                Else
                    result = sc.UMIDCard_Change_PIN(oldPIN, newPIN)

                    If result Then
                        Return True
                    Else
                        ErrorMessage = "ChangePIN(): Failed to change pin"
                        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                        Return False
                    End If
                End If
            Else
                ErrorMessage = "ChangePIN(): SL1 failed"
                SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

                Return False
            End If
        Catch ex As Exception
            ErrorMessage = String.Format("ChangePIN(): Runtime error catched {0}", ex.Message)
            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

            Return False
        End Try
    End Function

#Region " Others "

    Public Enum UMIDType
        UMID_OLD = 1
        UMID_NEW
    End Enum

    'tested
    Private Shared Sub Init(ByRef sc As UMIDLibrary.AllCardTech_Smart_Card)
        If sc Is Nothing Then
            sc = New UMIDLibrary.AllCardTech_Smart_Card()
            sc.InitializeReaders()

            Dim pUmid As Integer = readSettings(xml_Filename, xml_path, "UMID")
            Dim pSam As Integer = readSettings(xml_Filename, xml_path, "SAM")
            sc.SelectApplet(pUmid, pSam)

            AppletVersion = sc.CheckVersion
        End If
    End Sub

    'tested
    Public Shared Function accSC() As UMIDLibrary.AllCardTech_Smart_Card
        Return New UMIDLibrary.AllCardTech_Smart_Card
    End Function

    'tested
    Private Shared Function Util() As UMIDLibrary.AllCardTech_Util
        Return New UMIDLibrary.AllCardTech_Util()
    End Function

#End Region

    Public Shared Sub ConnectToCard()
        Dim ReaderName As String = "HID Global OMNIKEY 5422 Smartcard Reader 0"

        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
            'displayOut(1, retCode, "", lstBoxLog)
            Exit Sub
        End If

        ' Shared Connection
        retCode = ModWinsCard.SCardConnect(hContext, ReaderName, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

        If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            '' check if ACR128 SAM is used and use Direct Mode if SAM is not detected
            'If InStr(cmbReader.Text, "ACR128U SAM") > 0 Then

            '    '1. Direct Connection
            '    retCode = ModWinsCard.SCardConnect(hContext, cmbReader.Text, ModWinsCard.SCARD_SHARE_DIRECT, 0, hCard, Protocol)

            '    If retCode <> ModWinsCard.SCARD_S_SUCCESS Then

            '        displayOut(1, retCode, "", lstBoxLog)
            '        connActive = False

            '        Exit Sub

            '    Else

            '        displayOut(0, 0, "Successful connection to " & cmbReader.Text, lstBoxLog)

            '    End If

            'Else

            '    displayOut(1, retCode, "", lstBoxLog)
            '    connActive = False
            '    Exit Sub

            'End If

        Else

            'displayOut(0, 0, "Successful connection to " & cmbReader.Text, lstBoxLog)

        End If

        connActive = True

    End Sub

    Public Shared Function IsCardPresent() As Boolean
        Dim bln As Boolean = False

        Dim ReaderName As String = readSettings(xml_Filename, xml_path, "SmartCardReader")
        'MessageBox.Show(ReaderName)

        retCode = ModWinsCard.SCardEstablishContext(ModWinsCard.SCARD_SCOPE_USER, 0, 0, hContext)

        Try
            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Console.WriteLine("ModWinsCard.SCardEstablishContext failed")
                'MessageBox.Show("ModWinsCard.SCardEstablishContext failed")
                Return bln
            End If

            ' Shared Connection
            retCode = ModWinsCard.SCardConnect(hContext, ReaderName, ModWinsCard.SCARD_SHARE_SHARED, ModWinsCard.SCARD_PROTOCOL_T0 Or ModWinsCard.SCARD_PROTOCOL_T1, hCard, Protocol)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then
                Console.WriteLine("ModWinsCard.SCardConnect failed")
                'MessageBox.Show("ModWinsCard.SCardConnect failed")
                Return bln
            End If

            Dim tmpStr As String = ""
            Dim indx As Integer


            validATS = False

            Call ClearBuffers()

            SendBuff(0) = &HFF                              ' CLA
            SendBuff(1) = &HCA                              ' INS
            SendBuff(2) = &H0                               ' P1 : Other cards
            SendBuff(3) = &H0                               ' P2
            SendBuff(4) = &H0                               ' Le : Full Length

            SendLen = SendBuff(4) + 5
            RecvLen = &HFF

            Dim lstBoxLog As New ListBox
            retCode = SendAPDUandDisplay(3, lstBoxLog)

            If retCode <> ModWinsCard.SCARD_S_SUCCESS Then Return False

            If validATS Then

                For indx = 0 To (RecvLen - 3)
                    tmpStr = tmpStr + Microsoft.VisualBasic.Right("00" & Hex(RecvBuff(indx)), 2) + " "
                Next indx

                'displayOut(3, 0, "UID:" + tmpStr.Trim, lstBoxLog)

                If tmpStr <> "" Then bln = True

            End If

            Return bln
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            retCode = ModWinsCard.SCardDisconnect(hCard, ModWinsCard.SCARD_UNPOWER_CARD)
        End Try
    End Function

End Class
