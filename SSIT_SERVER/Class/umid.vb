Public Class umid

    Private sbException As New System.Text.StringBuilder
    Private sc As UMIDLibrary.AllCardTech_Smart_Card

    Private ExceptionValue As String = ""
    Public Property Exception() As String
        Get
            Return ExceptionValue
        End Get
        Set(ByVal value As String)
            ExceptionValue = value
        End Set
    End Property

    Private sssNumberValue As String = ""
    Public Property sssNumber() As String
        Get
            Return sssNumberValue
        End Get
        Set(ByVal value As String)
            sssNumberValue = value
        End Set
    End Property

    Private crnValue As String = ""
    Public Property crn() As String
        Get
            Return crnValue
        End Get
        Set(ByVal value As String)
            crnValue = value
        End Set
    End Property

    Private firstNameValue As String = ""
    Public Property firstName() As String
        Get
            Return firstNameValue
        End Get
        Set(ByVal value As String)
            firstNameValue = value
        End Set
    End Property

    Private middleNameValue As String = ""
    Public Property middleName() As String
        Get
            Return middleNameValue
        End Get
        Set(ByVal value As String)
            middleNameValue = value
        End Set
    End Property

    Private lastNameValue As String = ""
    Public Property lastName() As String
        Get
            Return lastNameValue
        End Get
        Set(ByVal value As String)
            lastNameValue = value
        End Set
    End Property

    Private suffixValue As String = ""
    Public Property suffix() As String
        Get
            Return suffixValue
        End Get
        Set(ByVal value As String)
            suffixValue = value
        End Set
    End Property

    Private postalCodeValue As String = ""
    Public Property postalCode() As String
        Get
            Return postalCodeValue
        End Get
        Set(ByVal value As String)
            postalCodeValue = value
        End Set
    End Property

    Private countryCodeValue As String = ""
    Public Property countryCode() As String
        Get
            Return countryCodeValue
        End Get
        Set(ByVal value As String)
            countryCodeValue = value
        End Set
    End Property

    Private provinceValue As String = ""
    Public Property province() As String
        Get
            Return provinceValue
        End Get
        Set(ByVal value As String)
            provinceValue = value
        End Set
    End Property

    Private cityValue As String = ""
    Public Property city() As String
        Get
            Return cityValue
        End Get
        Set(ByVal value As String)
            cityValue = value
        End Set
    End Property

    Private barangayValue As String = ""
    Public Property barangay() As String
        Get
            Return barangayValue
        End Get
        Set(ByVal value As String)
            barangayValue = value
        End Set
    End Property

    Private subdivisionValue As String = ""
    Public Property subdivision() As String
        Get
            Return subdivisionValue
        End Get
        Set(ByVal value As String)
            subdivisionValue = value
        End Set
    End Property

    Private streetNameValue As String = ""
    Public Property streetName() As String
        Get
            Return streetNameValue
        End Get
        Set(ByVal value As String)
            streetNameValue = value
        End Set
    End Property

    Private houseLotBlockValue As String = ""
    Public Property houseLotBlock() As String
        Get
            Return houseLotBlockValue
        End Get
        Set(ByVal value As String)
            houseLotBlockValue = value
        End Set
    End Property

    Private roomFloorUnitBldgValue As String = ""
    Public Property roomFloorUnitBldg() As String
        Get
            Return roomFloorUnitBldgValue
        End Get
        Set(ByVal value As String)
            roomFloorUnitBldgValue = value
        End Set
    End Property

    Private genderValue As String = ""
    Public Property gender() As String
        Get
            Return genderValue
        End Get
        Set(ByVal value As String)
            genderValue = value
        End Set
    End Property

    Private dateOfBirthValue As String = ""
    Public Property dateOfBirth() As String
        Get
            Return dateOfBirthValue
        End Get
        Set(ByVal value As String)
            dateOfBirthValue = value
        End Set
    End Property

    Private cityBirthValue As String = ""
    Public Property cityBirth() As String
        Get
            Return cityBirthValue
        End Get
        Set(ByVal value As String)
            cityBirthValue = value
        End Set
    End Property

    Private provinceBirthValue As String = ""
    Public Property provinceBirth() As String
        Get
            Return provinceBirthValue
        End Get
        Set(ByVal value As String)
            provinceBirthValue = value
        End Set
    End Property

    Private countryCodeBirthValue As String = ""
    Public Property countryCodeBirth() As String
        Get
            Return countryCodeBirthValue
        End Get
        Set(ByVal value As String)
            countryCodeBirthValue = value
        End Set
    End Property

    Private maritalStatusValue As String = ""
    Public Property maritalStatus() As String
        Get
            Return maritalStatusValue
        End Get
        Set(ByVal value As String)
            maritalStatusValue = value
        End Set
    End Property

    Private firstNameFatherValue As String = ""
    Public Property firstNameFather() As String
        Get
            Return firstNameFatherValue
        End Get
        Set(ByVal value As String)
            firstNameFatherValue = value
        End Set
    End Property

    Private middleNameFatherValue As String = ""
    Public Property middleNameFather() As String
        Get
            Return middleNameFatherValue
        End Get
        Set(ByVal value As String)
            middleNameFatherValue = value
        End Set
    End Property

    Private lastNameFatherValue As String = ""
    Public Property lastNameFather() As String
        Get
            Return lastNameFatherValue
        End Get
        Set(ByVal value As String)
            lastNameFatherValue = value
        End Set
    End Property

    Private suffixFatherValue As String = ""
    Public Property suffixFather() As String
        Get
            Return suffixFatherValue
        End Get
        Set(ByVal value As String)
            suffixFatherValue = value
        End Set
    End Property

    Private firstNamemotherValue As String = ""
    Public Property firstNamemother() As String
        Get
            Return firstNamemotherValue
        End Get
        Set(ByVal value As String)
            firstNamemotherValue = value
        End Set
    End Property

    Private middleNamemotherValue As String = ""
    Public Property middleNamemother() As String
        Get
            Return middleNamemotherValue
        End Get
        Set(ByVal value As String)
            middleNamemotherValue = value
        End Set
    End Property

    Private lastNamemotherValue As String = ""
    Public Property lastNamemother() As String
        Get
            Return lastNamemotherValue
        End Get
        Set(ByVal value As String)
            lastNamemotherValue = value
        End Set
    End Property

    Private suffixmotherValue As String = ""
    Public Property suffixmother() As String
        Get
            Return suffixmotherValue
        End Get
        Set(ByVal value As String)
            suffixmotherValue = value
        End Set
    End Property

    Private heightValue As String = ""
    Public Property height() As String
        Get
            Return heightValue
        End Get
        Set(ByVal value As String)
            heightValue = value
        End Set
    End Property

    Private weightValue As String = ""
    Public Property weight() As String
        Get
            Return weightValue
        End Get
        Set(ByVal value As String)
            weightValue = value
        End Set
    End Property

    Private distinguishFeatureValue As String = ""
    Public Property distinguishFeature() As String
        Get
            Return distinguishFeatureValue
        End Get
        Set(ByVal value As String)
            distinguishFeatureValue = value
        End Set
    End Property

    Private tinValue As String = ""
    Public Property tin() As String
        Get
            Return tinValue
        End Get
        Set(ByVal value As String)
            tinValue = value
        End Set
    End Property

    Private leftPrimaryCodeValue As String = ""
    Public Property leftPrimaryCode() As String
        Get
            Return leftPrimaryCodeValue
        End Get
        Set(ByVal value As String)
            leftPrimaryCodeValue = value
        End Set
    End Property

    Private rightPrimaryCodeValue As String = ""
    Public Property rightPrimaryCode() As String
        Get
            Return rightPrimaryCodeValue
        End Get
        Set(ByVal value As String)
            rightPrimaryCodeValue = value
        End Set
    End Property

    Private leftBackupCodeValue As String = ""
    Public Property leftBackupCode() As String
        Get
            Return leftBackupCodeValue
        End Get
        Set(ByVal value As String)
            leftBackupCodeValue = value
        End Set
    End Property

    Private rightBackupCodeValue As String = ""
    Public Property rightBackupCode() As String
        Get
            Return rightBackupCodeValue
        End Get
        Set(ByVal value As String)
            rightBackupCodeValue = value
        End Set
    End Property

    Private pinValue As String = ""
    Public Property pin() As String
        Get
            Return pinValue
        End Get
        Set(ByVal value As String)
            pinValue = value
        End Set
    End Property

    Private ccdtValue As String = ""
    Public Property ccdt() As String
        Get
            Return ccdtValue
        End Get
        Set(ByVal value As String)
            ccdtValue = value
        End Set
    End Property

    Private cardStatusValue As String = ""
    Public Property cardStatus() As String
        Get
            Return cardStatusValue
        End Get
        Set(ByVal value As String)
            cardStatusValue = value
        End Set
    End Property

    Private cardStatusCodeValue As String = ""
    Public Property cardStatusCode() As String
        Get
            Return cardStatusCodeValue
        End Get
        Set(ByVal value As String)
            cardStatusCodeValue = value
        End Set
    End Property

    Private csnValue As String = ""
    Public Property csn() As String
        Get
            Return csnValue
        End Get
        Set(ByVal value As String)
            csnValue = value
        End Set
    End Property

    Private sssSector36Value As String
    Public Property sssSector36() As String
        Get
            Return sssSector36Value
        End Get
        Set(ByVal value As String)
            sssSector36Value = value
        End Set
    End Property

    Private isGsisCardValue As Boolean = False
    Public Property isGsisCard() As Boolean
        Get
            Return isGsisCardValue
        End Get
        Set(ByVal value As Boolean)
            isGsisCardValue = value
        End Set
    End Property

    Private isFingerprintsExtractedValue As Boolean = True
    Public Property isFingerprintsExtracted() As Boolean
        Get
            Return isFingerprintsExtractedValue
        End Get
        Set(ByVal value As Boolean)
            isFingerprintsExtractedValue = value
        End Set
    End Property

    Private appletVersionValue As appVersion
    Public Property appletVersion() As appVersion
        Get
            Return appletVersionValue
        End Get
        Set(ByVal value As appVersion)
            appletVersionValue = value
        End Set
    End Property

    Private isAppletSelectedValue As Boolean
    Public Property isAppletSelected() As Boolean
        Get
            Return isAppletSelectedValue
        End Get
        Set(ByVal value As Boolean)
            isAppletSelectedValue = value
        End Set
    End Property

    Private Function accSC() As UMIDLibrary.AllCardTech_Smart_Card
        Return New UMIDLibrary.AllCardTech_Smart_Card
    End Function

    'tested
    Private Function Util() As UMIDLibrary.AllCardTech_Util
        Return New UMIDLibrary.AllCardTech_Util()
    End Function

    Public Enum appVersion
        oldApplet = 1
        newApplet = 2
    End Enum

    Public Sub New()
        Try
            Initialize()
        Catch ex As Exception
            MessageBox.Show("umid new error " & vbNewLine & vbNewLine & ex.Message)
        End Try

    End Sub

    Public Sub Initialize()
        sc = New UMIDLibrary.AllCardTech_Smart_Card()

        sc.InitializeReaders()
        SelectApplet()
        appletVersionValue = sc.CheckVersion
    End Sub

    Public Sub SelectApplet()
        Dim pUmid As Integer = readSettings(xml_Filename, xml_path, "UMID")
        Dim pSam As Integer = readSettings(xml_Filename, xml_path, "SAM")
        Try
            isAppletSelected = sc.SelectApplet(pUmid, pSam)

            If Not isAppletSelected Then
                SharedFunction.SaveToLog(SharedFunction.TimeStamp & "|" & "SelectApplet is failed. Version " & sc.CheckVersion & "|" & kioskIP & "|" & getbranchCoDE_1)
            End If
        Catch ex As Exception
            MessageBox.Show("error in select applet" & vbNewLine & vbNewLine & ex.Message)
        End Try

    End Sub

    Public Function Read_CRN_CCDT_Status() As Boolean
        getCRN()
        ccdt = GetCCDT()
        getCardStatus()
    End Function

    Private Sub getCRN()
        If appletVersion = appVersion.newApplet Then
            crnValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CRN))
        Else
            crnValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CRN))
            If crnValue = "Error" Then
                crnValue = AllcardUMID.GetCRN_QC("")
            ElseIf crnValue = "" Then
                crnValue = AllcardUMID.GetCRN_QC("")
            End If
        End If
    End Sub

    Private Function getPIN() As String
        If appletVersion = appVersion.newApplet Then
            Return "12345678"
        Else
            Return "123456"
        End If
    End Function

    Public Function ReadData(ByVal isReadInitialData As Boolean, Optional ByVal pin As String = "") As Boolean
        Try
            If Not isAppletSelected Then Return False

            'If appletVersion = appVersion.newApplet Then
            '    If Not sc.AuthenticateSL1() Then
            '        getCardStatus()

            '        If cardStatusCode = "9" Then Return True

            '        sbException.Append("AuthenticateSL1 failed...")
            '        Return False
            '    End If
            'Else
            '    getCardStatus()
            '    Return True
            'End If


            getCRN()

            If Not isReadInitialData Then
                If pin = "" Then pin = getPIN()
                pinValue = pin
                Dim bytePin() As Byte = System.Text.ASCIIEncoding.ASCII.GetBytes(pin)
                If Not sc.AuthenticateSL2(bytePin) Then
                    sbException.Append("AuthenticateSL2 failed...")
                    Return False
                End If
                firstNameValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FIRST_NAME))
                middleNameValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MIDDLE_NAME))
                lastNameValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.LAST_NAME))
                suffixValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.SUFFIX))
                postalCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_POSTAL_CODE))
                countryCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_COUNTRY))
                provinceValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_PROVINCIAL_OR_STATE))
                cityValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_CITY_OR_MUNICIPALITY))
                barangayValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_BARANGAY_OR_DISTRIC_OR_LOCALITY))
                subdivisionValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_SUBDIVISION))
                streetNameValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_STREET_NAME))
                houseLotBlockValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_HOUSE_OR_LOT_AND_BLOCK_NUMBER))
                roomFloorUnitBldgValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.ADDRESS_ROOM_OR_FLOOR_OR_UNIT_NO_AND_BUILDING_NAME))
                genderValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.GENDER))
                dateOfBirthValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.DATE_OF_BIRTH))
                cityBirthValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.PLACE_OF_BIRTH_CITY))
                provinceBirthValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.PLACE_OF_BIRTH_PROVINCE))
                countryCodeBirthValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.PLACE_OF_BIRTH_COUNTRY))
                maritalStatusValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MARITAL_STATUS))
                firstNameFatherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FATHER_FIRST_NAME))
                middleNameFatherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FATHER_MIDDLE_NAME))
                lastNameFatherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FATHER_LAST_NAME))
                suffixFatherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.FATHER_SUFFIX))
                firstNamemotherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MOTHER_FIRST_NAME))
                middleNamemotherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MOTHER_MIDDLE_NAME))
                lastNamemotherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MOTHER_LAST_NAME))
                suffixmotherValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.MOTHER_SUFFIX))
                heightValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.HEIGHT))
                weightValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.WEIGHT))
                distinguishFeatureValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.DISTINGUISHING_FEATURES))
                tinValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.TIN))
                leftPrimaryCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.LEFT_PRIMARY_FINGER_CODE))
                rightPrimaryCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.RIGHT_PRIMARY_FINGER_CODE))
                leftBackupCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.LEFT_SECONDARY_FINGER_CODE))
                rightBackupCodeValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.RIGHT_SECONDARY_FINGER_CODE))

                'ExtractFingerprints()
            End If

            csnValue = Util.ByteArrayToAscii(sc.getUmidData(UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.CSN))

            'Select Case crn
            '    Case "003373638407"
            '        Temp_ReplaceFieldsValue003373638407()
            'End Select

            If csnValue <> "" Then If csnValue.Substring(0, 2) = "02" Then isGsisCardValue = True

            ccdt = GetCCDT()

            Dim byteSector36() As Byte = sc.ReadSector(36, 0, 10)

            If CInt(byteSector36.GetValue(0)) > 0 Then
                Dim sectorData As String = System.Text.ASCIIEncoding.ASCII.GetString(byteSector36).Trim
                If sectorData.Length >= 10 Then
                    sssSector36Value = sectorData.Substring(0, 10)
                Else
                    sssSector36Value = sectorData
                End If
            Else
                sssSector36Value = "NO_DATA"
            End If

            getCardStatus()

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show("Failed to read in ReadData(): " & ex.Message)
            Return False
        End Try
    End Function

    Public Sub Temp_ReplaceFieldsValue003373638407()
        crn = "011161569010"
        firstName = "RICHELYN"
        middleName = "REYES"
        lastName = "JARDELIZA"
        suffix = ""
        dateOfBirth = "19800514"
        csnValue = "0220210118AB00000001"
    End Sub

    Public Function ReadFingerprints() As Boolean
        Try
            If Not isAppletSelected Then Return False

            SelectApplet()
            If Not sc.AuthenticateSL1() Then
                sbException.Append("AuthenticateSL1 failed...")
                Return False
            Else
                ExtractFingerprints()
                If Not isFingerprintsExtractedValue Then Return False
            End If

            Return True
        Catch ex As Exception
            Console.WriteLine(ex.Message)
            MessageBox.Show("ReadFingerprints(): " & ex.Message)
            Return False
        End Try
    End Function

    Private Sub ExtractFingerprints()
        Dim Path_Fingerprint_LP As String = "Temp\LP.ansi-fmr"
        Dim Path_Fingerprint_RP As String = "Temp\RP.ansi-fmr"
        Dim Path_Fingerprint_LB As String = "Temp\LB.ansi-fmr"
        Dim Path_Fingerprint_RB As String = "Temp\RB.ansi-fmr"

        Dim blnLP As Boolean = False
        Dim blnRP As Boolean = False
        Dim blnLB As Boolean = False
        Dim blnRB As Boolean = False

        blnLP = sc.getUmidFile(Path_Fingerprint_LP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_PRIMARY_FINGER)
        blnRP = sc.getUmidFile(Path_Fingerprint_RP, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_PRIMARY_FINGER)
        blnLB = sc.getUmidFile(Path_Fingerprint_LB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_LEFT_SECONDARY_FINGER)
        blnRB = sc.getUmidFile(Path_Fingerprint_RB, UMIDLibrary.AllCardTech_Smart_Card.UMID_Fields.BIOMETRIC_RIGHT_SECONDARY_FINGER)

        If Not blnLP And Not blnLB And Not blnRP And Not blnRB Then isFingerprintsExtracted = False
    End Sub

    Private Sub getCardStatus()
        Dim status As String = ""
        'sc.InitializeReaders()
        SelectApplet()
        sc.GetCardStatus(status)
        cardStatusValue = status
        If cardStatusValue = "CARD_ACTIVE" Then
            cardStatusCode = "0"
        ElseIf cardStatusValue = "CARD_INACTIVE" Then
            cardStatusCode = "1"
        ElseIf cardStatusValue = "CARD_BLOCKED" Then
            cardStatusCode = "9"
        End If
    End Sub

    Public Function GetCCDT() As String
        Dim ErrorMessage As String = ""
        Try
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

                If CCDT = "00000000" Then CCDT = AllcardUMID.Get_CardCreationDate_QC(ErrorMessage)

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

    'Public Function ReadData(ByRef data As String,
    '                               Optional ByVal ErrorMessage As String = "") As Boolean
    '    Dim sb As New System.Text.StringBuilder

    '    Try
    '        Dim sc As UMIDLibrary.AllCardTech_Smart_Card
    '        Init(sc)

    '        Dim _CRN As String = GetCRN(ErrorMessage, sc)

    '        Try
    '            If _CRN = "" Then _CRN = GetCRN_QC(ErrorMessage)

    '            sb.Append(_CRN & "|") '0
    '            sb.Append(GetCSN(ErrorMessage, sc) & "|") '1
    '            sb.Append(GetCCDT(ErrorMessage, sc) & "|") '2
    '            sb.Append(GetCardStatus(ErrorMessage, sc) & "|") '3              

    '            data = sb.ToString

    '            Return True
    '        Catch ex As Exception
    '            ErrorMessage = String.Format("ReadData(2): Runtime error catched {0}", ex.Message)
    '            SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

    '            Return False
    '        End Try
    '    Catch ex As Exception
    '        ErrorMessage = String.Format("ReadData(1): Runtime error catched {0}", ex.Message)
    '        SharedFunction.SaveToErrorLog(SharedFunction.TimeStamp & "|" & ErrorMessage & "|" & kioskIP & "|" & getbranchCoDE_1 & "|" & cardType)

    '        Return False
    '    End Try
    'End Function

End Class
