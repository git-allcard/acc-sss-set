
Imports DevComponents.DotNetBar
Imports Oracle.DataAccess.Client
Public Class _frmUserAuthentication

    Public certainDate, outPut As String
    Public finalDateAcop As String
    Dim getYear As String = Date.Today.Year + 1
    Dim getDay As String = Date.Today.Day
    Public getSalDay As String
    Dim getMonth As String = Date.Today.Month
    Dim xtd As New ExtractedDetails
    Dim db As New ConnectionString
    Dim txn As New txnNo
    Dim printF As New printModule
    Dim tempSSSHeader As String
    Public tempSSNum As String
    Public techRetDate As String
    Dim getLastNum As String
    Public ctrTrans As String
    Dim xs As New MySettings

    Private Sub _frmUserAuthentication_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            tagPage = "17"
            If _frmMainMenu.prtRes = 0 Then
                lblGetReceipt.Visible = False
            ElseIf _frmMainMenu.prtRes = 1 Then
                lblGetReceipt.Visible = True
            End If
            'xtd.getRawFile()
            Dim bdate As Date = xtd.getbDate ' printF.GetDateBith(_frmWebBrowser.WebBrowser1)
            Dim getYearBday As String = db.putSingleValue("select DATEPART(YEAR, GETDATE())") + 1
            Dim getDayBday As String = bdate.Day
            Dim getMonthBday As String = (bdate.Month)
            certainDate = getMonthBday & "-" & getDayBday & "-" & getYearBday
            lblDate.Text = Date.Today.ToString("MM/dd/yyyy")
            lblBranch.Text = kioskBranch
            lblTerminalNo.Text = kioskID

            Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
            Dim getkiosk_cluster As String = db.putSingleValue("select CLSTR_CD from SSINFOTERMCLSTR where CLSTR_NM = '" & kioskCluster & "'")
            Dim getkiosk_group As String = db.putSingleValue("select GROUP_CD from SSINFOTERMGROUP where GROUP_NM = '" & kioskGroup & "'")
            Dim getKioskName As String = kioskName
            'getKioskName = getKioskName.Substring(getKioskName.Length - 3)
            tempSSNum = UsrfrmPageHeader1.lblSSSNo.Text.Replace("-", "")
            If getKioskName <> "" Then getKioskName = getKioskName.Substring(0, 1)

            'getTransacNum()
            getText()

            _frmMainMenu.PrintControls(True)
            '_frmMainMenu.btnPrint.Image = Image.FromFile(Application.StartupPath & "\images\PRINT.png")


        Catch ex As Exception
            Console.WriteLine(ex.Message)
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Public Sub getTransacNum()

        Dim getbranchCoDE As String = db.putSingleValue("select BRANCH_CD from SSINFOTERMBR where BRANCH_NM = '" & kioskBranch & "'")
        'Dim getTransNo As String = db.putSingleValue("select max(SEQUENCE_NUM) from SSTRANSREFNO")
        'If getTransNo = "" Then
        '    getTransNo = "0001"
        'Else
        '    ' getTransNo = +1
        '    getTransNo = getTransNo.PadLeft(4, "0") + 1
        '    getTransNo = getTransNo.PadLeft(4, "0")
        'End If

        'Dim CTRno As String = db.putSingleValue("select max(CTR) from SSTRANSCTRREFNO")
        'If CTRno = "" Then
        '    CTRno = "0001"
        'Else
        '    ' getTransNo = +1
        '    CTRno = CTRno.PadLeft(4, "0") + 1
        '    CTRno = CTRno.PadLeft(4, "0")
        'End If

        'If _frmMaternityNotification.matnotifTag = 1 Then

        '    lblTransactionNo.Text = _frmMaternitySummary.transMatnotif

        'ElseIf _frmLoanSummaryEmployed.salaryTagLoan = 1 Then

        '    lblTransactionNo.Text = _frmLoanSummaryEmployed.employedRefNo

        'ElseIf _frmLoanSummaryMember_v2.salaryMembTagLoan = 1 Then

        '    lblTransactionNo.Text = _frmLoanSummaryMember_v2.txnNum


        'ElseIf _frmTechnicalRetirementWillAvailLumpSum.techLumpTag = 1 Then

        '    lblTransactionNo.Text = _frmMainMenu.techRetTransNum

        'ElseIf _frmTechnicalRetirementWillAvail.techPenTag = 1 Then

        '    lblTransactionNo.Text = _frmMainMenu.techRetTransNum

        'Else

        lblTransactionNo.Text = txn.getnum(Date.Today.ToString("yyMMdd"), transTag) 'txn.GETTXN(getbranchCoDE, CTRno, transTag, Date.Today.ToString("yyMMdd"), getTransNo)

        'End If

        If lblTransactionNo.Text = "" Then

        Else

            editSettings(xml_Filename, xml_path, "lastTransNo", lblTransactionNo.Text)
            lastTransNo = readSettings(xml_Filename, xml_path, "lastTransNo") 'lblTransactionNo.Text

        End If

    End Sub
    Private Sub getText()
        Dim newTxtBox, newTxtBox2 As New Label
        Dim textNote As String
        Dim fnt As Font
        newTxtBox.Parent = Panel6
        newTxtBox.Visible = True
        'newTxtBox.AutoSize = True
        newTxtBox.AutoSize = False
        newTxtBox.Width = Panel6.Width - 100
        newTxtBox.Height = Panel6.Height - 100
        If SharedFunction.GetMonitorInch = SharedFunction.monitorInch.nineteenInch Then
            fnt = New Drawing.Font("Segoe UI", 12, FontStyle.Bold)
        Else
            fnt = New Drawing.Font("Segoe UI", 11, FontStyle.Bold)
        End If
        newTxtBox.Font = fnt
        newTxtBox.Dock = DockStyle.Top

        'newTxtBox2.Parent = Panel7
        newTxtBox2.Visible = True
        newTxtBox2.AutoSize = True
        fnt = New Drawing.Font("Segoe UI", 8, FontStyle.Bold)
        newTxtBox2.Font = fnt
        newTxtBox2.Dock = DockStyle.Top



        newTxtBox2.Text = "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT" & vbNewLine &
                          "FRONTLINE SERVICE COUNTER OR GO TO THE NEAREST SSS BRANCH (IF THIS" & vbNewLine &
                          "TERMINAL IS NOT LOCATED AT AN SSS BRANCH/SERVICE OFFICE)."


        Dim txnNoLabel As String = "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
        If lblTransactionNo.Text = "Transaction Number :" Then txnNoLabel = ""

        Select Case authentication

            'User Authentication
            Case "UA01"
                newTxtBox.Text = "SORRY, WE FAILED TO VERIFY YOUR IDENTITY. "

            Case "UA02"
                'newTxtBox.Text = "THE SYSTEM HAS FAILED TO AUTHENTICATE YOUR FINGERPRINT." & vbNewLine &
                '                "PLEASE TRY AGAIN." & vbNewLine & vbNewLine &
                '                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                '                "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                '                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                If Not isGSISCard Then
                    newTxtBox.Text = "The system has failed to authenticate your card. You cannot proceed. You may access your account on the following day.".ToUpper & vbNewLine & vbNewLine & txnNoLabel
                Else
                    newTxtBox.Text = "FAILED AUTHENTICATION, PLEASE SEEK ASSISTANCE FROM OUR FRONTLINE SERVICE COUNTER OF THE NEAREST SSS BRANCH." & vbNewLine & vbNewLine & txnNoLabel
                End If
            Case "WR00"

                newTxtBox.Text = "THIS SERVICE IS TEMPORARILY UNAVAILABLE." & vbNewLine & vbNewLine &
                            "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                             "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                             "AT THE SSS BRANCH." & vbNewLine & vbNewLine
                ' "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'Techincal Retirement Print

            Case "WR01"

                newTxtBox.Text = "YOU ARE ALREADY REGISTERED. YOU MAY LOGIN IN THE SSS WEBSITE USING" & vbNewLine &
                                 "YOUR ACCOUNT." & vbNewLine & vbNewLine &
                                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "WR02"
                'newTxtBox.Text = "Member must not have a final claim you are not allowed to register. "

                newTxtBox.Text = "MEMBER HAS ALREADY AVAILED OF A FINAL CLAIM.  WEB REGISTRATION NOT" & vbNewLine &
                    "ALLOWED. " & vbNewLine & vbNewLine &
                                         "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


            Case "WR03"

                newTxtBox.Text = "YOUR SS CARD NUMBER IS INVALID." & vbNewLine & vbNewLine &
                           "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL01"

                newTxtBox.Text = "APPLICATION INELIGIBLE FOR THE FOLLOWING REASON/S:" & vbNewLine &
                                 "ACCOUNT NUMBER BELONGS TO ANOTHER PERSON. " & vbNewLine & vbNewLine &
                                         "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL02"
                newTxtBox.Text = "TRANSACTION DENIED. YOUR CITIBANK PUN IS INVALID." & vbNewLine & vbNewLine &
                                       "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL03"

                newTxtBox.Text = "SYSTEM ERROR: BRANCH NAME CANNOT BE FOUND." & vbNewLine & vbNewLine &
                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL04"
                newTxtBox.Text = "YOUR APPLICATION IS INELIGIBLE FOR THE FOLLOWING REASON/S:" & vbNewLine &
                                  "YOU STILL HAVE A PENDING SALARY LOAN APPLICATION." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                 "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL05"

                newTxtBox.Text = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE PROCESSING YOUR APPLICATION." & vbNewLine &
                                 "PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                          "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan
            Case "SL06"

                newTxtBox.Text = "YOUR APPLICATION IS INELIGIBLE FOR THE FOLLOWING REASON/S:" & vbNewLine &
                                 "YOU STILL HAVE A PENDING SALARY LOAN APPLICATION. " & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Salary Loan Print
            Case "SLP01"

                newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN APPLICATION. KINDLY" & vbNewLine &
                                 "CHECK YOUR EMAIL FOR THE COPY OF YOUR LOAN DISCLOSURE STATEMENT." & vbNewLine & vbNewLine &
                                "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine &
                               "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'Salary Loan Print

            Case "SLP02"
                'argie102

                Dim MyConnection As String = "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" & db_Host & ")(PORT=" & db_Port & "))(CONNECT_DATA=(SERVICE_NAME=" & db_ServiceName & ")));User Id=" & db_UserID & ";Password=" & db_Password & ";"
                Dim dbConn As OracleConnection = New OracleConnection(MyConnection)
                Dim dbComm As OracleCommand
                dbConn.Open()
                dbComm = dbConn.CreateCommand
                dbComm.Parameters.Add("MSG", OracleDbType.Varchar2, 32767).Direction = ParameterDirection.Output
                dbComm.CommandText = "PR_LOANVALIDITY"
                dbComm.CommandType = CommandType.StoredProcedure
                dbComm.ExecuteNonQuery()
                dbConn.Close()
                getSalDay = dbComm.Parameters("MSG").Value.ToString
                Dim getdateSL As Date = getSalDay
                Dim getMonthBday As String = MonthName(getdateSL.Month)
                Dim getDay As String = getdateSL.Day
                Dim getYear As String = getdateSL.Year

                Dim finalSLdate As String = getMonthBday & " " & getDay & ", " & getYear

                'newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN APPLICATION. KINDLY" & vbNewLine & _
                '                 "CHECK YOUR EMAIL FOR THE COPY OF YOUR LOAN DISCLOSURE STATEMENT. YOUR " & vbNewLine & _
                '                 "APPLICATION MUST BE CERTIFIED BY YOUR EMPLOYER ON OR BEFORE " & vbNewLine & UCase(finalSLdate) & "." & _
                '   vbNewLine & vbNewLine & _
                '                "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine & _
                '                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR SALARY LOAN. YOUR APPLICATION" & vbNewLine &
                             "SHOULD BE CERTIFIED BY YOUR EMPLOYER ON OR BEFORE " & UCase(finalSLdate) & vbNewLine &
                             "THROUGH ITS SSS WEBSITE ACCOUNT. OTHERWISE, IT WILL EXPIRE, THUS, YOU" & vbNewLine &
                             "NEED TO RE-SUBMIT YOUR APPLICATION." & vbNewLine & vbNewLine &
                            "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine &
                            "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


                'Maternity Notification
            Case "MN01"

                newTxtBox.Text = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE SUBMITTING YOUR REQUEST." & vbNewLine &
                                "PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN02"


                newTxtBox.Text = "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED TO SUBMIT THEIR" & vbNewLine &
                                 "MATERNITY NOTIFICATION. " & vbNewLine & vbNewLine &
                                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN03"

                newTxtBox.Text = "SORRY, MEMBER HAS ALREADY AVAILED OF A FINAL CLAIM. " & vbNewLine & vbNewLine &
               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN04"

                newTxtBox.Text = "SORRY, WE DID NOT FIND ANY SUPPORTING DOCUMENT SUBMITTED TO SUPPORT " & vbNewLine &
                                 "YOUR SS FORM E-1." & vbNewLine & vbNewLine &
                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN05"
                newTxtBox.Text = "SORRY, YOUR SS NUMBER IS INACTIVE. " & vbNewLine & vbNewLine &
                               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN06"

                newTxtBox.Text = "SORRY, ONLY FEMALE MEMBERS BETWEEN AGES 14 TO 60 YEARS OLD ARE ALLOWED" & vbNewLine &
                                 "TO SUBMIT THEIR MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                ' Maternity Notification
            Case "MN07"

                newTxtBox.Text = "SORRY, THIS FACILITY IS FOR SELF-EMPLOYED/VOLUNTARY MEMBERS ONLY." & vbNewLine &
                                 "PLEASE SUBMIT YOUR MATERNITY NOTIFICATION THROUGH YOUR EMPLOYER." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MN08"

                newTxtBox.Text = "SORRY, THIS FACILITY IS FOR SELF-EMPLOYED/VOLUNTARY MEMBERS ONLY." & vbNewLine &
                                 "PLEASE SUBMIT YOUR MATERNITY NOTIFICATION THROUGH YOUR EMPLOYER." & vbNewLine & vbNewLine &
                               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Maternity Notification
            Case "MNP01"

                newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR MATERNITY NOTIFICATION.  " & vbNewLine & vbNewLine &
                                 "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine &
                                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                ' nikki003
            Case "MNP02"
                newTxtBox.Text = "WE HAVE ACCEPTED YOUR MATERNITY NOTIFICATION, HOWEVER, A" & vbNewLine &
                                 "DISCREPANCY WAS NOTED, HENCE, PLEASE UPDATE/CORRECT YOUR" & vbNewLine &
                                 "RECORDS IN OUR FILE PRIOR TO FILING OF MATERNITY REIMBURSEMENT." & vbNewLine & vbNewLine &
                                 "-MEMBER LACKS THE REQUIRED QUALIFYING CONTRIBUTIONS." & vbNewLine & vbNewLine &
                                "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW." & vbNewLine & vbNewLine &
                                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
            Case "MNS01"

                newTxtBox.Text = "SORRY, THE DELIVERY NUMBER YOU INDICATED WAS ALREADY SETTLED. YOU" & vbNewLine &
                                 "CANNOT PROCEED WITH THE SUBMISSION OF YOUR MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                               "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text



            Case "MNS02"

                newTxtBox.Text = "SORRY, YOUR DATE OF LAST DELIVERY IS INVALID (AFTER THE CURRENT DATE). " & vbNewLine & vbNewLine &
                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS03"

                newTxtBox.Text = "SORRY, YOU HAVE ALREADY REACHED THE MAXIMUM LIMIT OF FOUR (4)" & vbNewLine &
                   "PREGNANCIES." & vbNewLine & vbNewLine &
                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS04"
                '                "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED TO SUBMIT THEIR"
                newTxtBox.Text = "SORRY, EXPECTED DATE OF DELIVERY SHOULD BE ANY DATE WITHIN 9 MONTHS " & vbNewLine &
                   "FROM THE CURRENT DATE." & vbNewLine & vbNewLine &
                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS05"
                newTxtBox.Text = "SORRY, EXPECTED DATE OF DELIVERY SHOULD BE ANY DATE WITHIN 9 MONTHS " & vbNewLine &
                   "FROM THE CURRENT DATE." & vbNewLine & vbNewLine &
                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS06"
                '                "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED TO SUBMIT THEIR"
                newTxtBox.Text = "SORRY, YOUR EXPECTED DATE OF DELIVERY SHOULD HAVE AN INTERVAL OF AT" & vbNewLine &
                   "LEAST 6 MONTHS FROM YOUR LAST DELIVERY DATE." & vbNewLine & vbNewLine &
                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS07"

                newTxtBox.Text = "SORRY, YOUR DATE FORMAT IS INVALID." & vbNewLine & vbNewLine &
                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS08"

                newTxtBox.Text = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR MATERNITY NOTIFICATION." & vbNewLine & vbNewLine &
                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "MNS09"

                newTxtBox.Text = "SORRY, YOU HAVE ENTERED AN INVALID NUMBER OF DELIVERIES." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Techincal Retirement

            'Case "TR00"
            '    '                "SORRY, YOU HAVE ENTERED AN INVALID NUMBER OF DELIVERIES."
            '    newTxtBox.Text = "SORRY, YOU CANNOT PROCEED WITH YOUR APPLICATION FOR" & vbNewLine &
            '                     "TECHNICAL RETIREMENT DUE TO THE FOLLOWING REASON/S:" & vbNewLine & vbNewLine &
            '                    _frmTechnicalRetirementQA.QAanswer & vbNewLine & vbNewLine &
            '                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE " & vbNewLine &
            '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
            '      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR01"

                newTxtBox.Text = "SORRY, BUT YOU NEED TO APPLY FOR UMID CARD AND ENROLL IT TO ANY OF THE " & vbNewLine &
                                 "PARTICIPATING BANKS TO FILE YOUR RETIREMENT CLAIM USING THIS FACILITY " & vbNewLine & vbNewLine &
                                 "PLEASE GO TO ANY OF THE ACCREDITED BANKS BELOW:" & vbNewLine & vbNewLine &
                                 "1) PNB               www.pnb.com" & vbNewLine &
                                 "2) Union Bank of the Phil.      www.unionbankph.com" & vbNewLine &
                                 "3) Security Bank                                      www.securitybank.com" & vbNewLine & vbNewLine &
                        "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR02"


                newTxtBox.Text = "SORRY, BUT YOU NEED TO ENROLL YOUR UMID CARD TO ANY OF THE FOLLOWING" & vbNewLine &
                                  "PARTICIPATING BANKS TO FILE YOUR RETIREMENT CLAIM USING THIS FACILITY." & vbNewLine & vbNewLine &
                                  "PHILIPPINE NATIONAL BANK               www.pnb.com" & vbNewLine &
                                  "UNION BANK OF THE PHILIPPINES      www.unionbankph.com" & vbNewLine &
                                  "SECURITY BANK                                      www.securitybank.com" & vbNewLine & vbNewLine &
                                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR05"

                newTxtBox.Text = "SORRY, ONLY MEMBERS WHO ARE 65 YEARS OLD AND ABOVE ARE ALLOWED TO" & vbNewLine &
                                 "FILE TECHNICAL RETIREMENT BENEFIT CLAIM." & vbNewLine & vbNewLine &
                              "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text



            Case "TR06"

                newTxtBox.Text = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR APPLICATION FOR" & vbNewLine &
                    "RETIREMENT." & vbNewLine & vbNewLine &
                   "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR07"

                newTxtBox.Text = "YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS BEEN CANCELLED." & vbNewLine & vbNewLine &
                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR08"


                newTxtBox.Text = "PLEASE BE INFORMED THAT THE EFFECTIVITY OF YOUR RETIREMENT " & vbNewLine &
                                 "IS ON " & techRetDate & "." & vbNewLine & vbNewLine &
                                  "THIS DOES NOT FALL UNDER TECHNICAL RETIREMENT." & vbNewLine & vbNewLine &
                                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR09"
                newTxtBox.Text = "THIS SERVICE IS NOT YET AVAILABLE." & vbNewLine & vbNewLine &
                                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine &
                                   "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine &
                                   "AT THE SSS BRANCH." & vbNewLine & vbNewLine
                ' "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            'Case "TR10"

            '    Dim qualMonths As Integer = _frmTechnicalRetirementQA.lessCont
            '    newTxtBox.Text = "YOU HAVE CHOSEN TO CONTINUE PAYING YOUR CONTRIBUTION AS " & vbNewLine &
            '                     "VOLUNTARY PAYING MEMBER TO QUALIFY FOR PENSION." & vbNewLine & vbNewLine &
            '                      "THE NUMBER OF LACKING MONTHLY CONTRIBUTIONS TO QUALIFY" & vbNewLine &
            '                     "FOR PENSION: " & qualMonths & vbNewLine & vbNewLine &
            '                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
            '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
            '                      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


            'Case "TR11"


            '    newTxtBox.Text = "SORRY, BUT YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS  " & vbNewLine &
            '                     "BEEN REJECTED DUE TO THE FOLLOWING REASON/S. " & vbNewLine & vbNewLine &
            '                    UCase(_frmTechnicalRetirementQA.getTechRetVal) & vbNewLine & vbNewLine &
            '                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
            '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
            '                      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


            'Case "TR11_1"

            '    newTxtBox.AutoSize = False
            '    newTxtBox.Size = Panel6.Size
            '    newTxtBox.Text = "SORRY, BUT YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS  " & vbNewLine &
            '                     "BEEN REJECTED DUE TO THE FOLLOWING REASON/S. " & vbNewLine & vbNewLine &
            '                    UCase(_frmTechnicalRetirementQA.getTechRetVal) & vbNewLine & vbNewLine &
            '                    "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
            '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
            '                      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "TR12"


                newTxtBox.Text = "SORRY, BUT YOU HAVE PENDING SSC CASE." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


            Case "TR13"


                newTxtBox.Text = "SORRY, BUT YOU HAVE EXISTING MEMBER LOAN BALANCE, HENCE YOU CANNOT." & vbNewLine & vbNewLine &
                                 "USE THIS FACILITY TO FILE YOUR TECHNICAL RETIREMENT CLAIM." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            'Case "TR14"

            '    '                "SORRY, BUT YOU HAVE EXISTING MEMBER LOAN BALANCE, HENCE YOU CANNOT."
            '    newTxtBox.Text = "PLEASE BE INFORMED THAT THE EFFECTIVITY OF YOUR TECHNICAL RETIREMENT" & vbNewLine &
            '                   "IS ON " & _frmTechnicalRetirementLessCont.tempssDate & "." & vbNewLine & vbNewLine &
            '                     _frmTechnicalRetirementLessCont.lessProceeds & vbNewLine &
            '                     "YOUR OUTSTANDING LOAN BALANCE IS GREATER THAN THE AMOUNT OF YOUR" & vbNewLine &
            '                     "RETIREMENT LUMP SUM BENEFIT. PLEASE SEEK ASSISTANCE FROM OUR MEMBER" & vbNewLine &
            '                     "SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
            '                     "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text



            Case "TR15"

                newTxtBox.Text = "SORRY, YOU WERE ALREADY GRANTED RETIREMENT BENEFIT." & vbNewLine & vbNewLine &
                                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text



            Case "TRP01"

                newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR APPLICATION FOR" & vbNewLine &
                                 "TECHNICAL RETIREMENT. " & vbNewLine & vbNewLine &
                                 "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine &
                                 "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text



                'Case "TR01"

                '    newTxtBox.Text = "SORRY, BUT YOU NEED TO APPLY FOR UMID CARD IF YOU INTEND TO FILE " & vbNewLine & _
                '                     "A TECHNICAL RETIREMENT CLAIM USING THIS FACILITY. " & vbNewLine & vbNewLine & _
                '                     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                '    'Techincal Retirement
                'Case "TR02"


                '    newTxtBox.Text = "SORRY, BUT YOU NEED TO ENROLL YOUR UMID CARD TO ANY PARTICIPATING" & vbNewLine & _
                '                      "BANKS TO FILE YOUR RETIREMENT CLAIM USING THIS FACILITY." & vbNewLine & vbNewLine & _
                '                       "PLEASE GO TO ANY OF THE ACCREDITED BANKS BELOW: " & vbNewLine & vbNewLine & _
                '                      "PHILIPPINE NATIONAL BANK               www.pnb.com" & vbNewLine & _
                '                      "UNION BANK OF THE PHILIPPINES      www.unionbankph.com" & vbNewLine & _
                '                      "SECURITY BANK                                      www.securitybank.com" & vbNewLine & vbNewLine & _
                '                        "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '        "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


                'Case "TR03"

                '    newTxtBox.Text = "SORRY, YOUR UMID CARD ACCOUNT IS INVALID." & vbNewLine & vbNewLine & _
                '          "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '       "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Case "TR04"
                '    newTxtBox.Text = "SORRY, BUT YOU NEED TO ENROLL YOUR UMID CARD TO ANY PARTICIPATING" & vbNewLine & _
                '                      "BANKS TO FILE YOUR RETIREMENT CLAIM USING THIS FACILITY." & vbNewLine & vbNewLine & _
                '                       "PLEASE GO TO ANY OF THE ACCREDITED BANKS BELOW: " & vbNewLine & vbNewLine & _
                '                      "PHILIPPINE NATIONAL BANK                www.pnb.com" & vbNewLine & _
                '                      "UNION BANK OF THE PHILIPPINES      www.unionbankph.com" & vbNewLine & _
                '                      "SECURITY BANK                           www.securitybank.com" & vbNewLine & vbNewLine & _
                '                       "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '        "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Case "TR05"

                '    newTxtBox.Text = "SORRY, ONLY MEMBERS WHO ARE 65 YEARS OLD AND ABOVE ARE ALLOWED TO" & vbNewLine & _
                '                     "APPLY FOR TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                '                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '       "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Case "TR06"

                '    newTxtBox.Text = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                '       "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Case "TR07"

                '    newTxtBox.Text = "YOUR APPLICATION FOR TECHNICAL RETIREMENT HAS BEEN CANCELLED." & vbNewLine & vbNewLine & _
                '     "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Case "TR08"


                '    newTxtBox.Text = "PLEASE BE INFORMED THAT THE EFFECTIVITY OF YOUR RETIREMENT " & vbNewLine & _
                '                     "IS ON " & techRetDate & "." & vbNewLine & vbNewLine & _
                '                      "THIS DOES NOT FALL UNDER TECHNICAL RETIREMENT." & vbNewLine & vbNewLine & _
                '                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine & _
                '                      "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine & _
                '      "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'Case "TR09"
                '    newTxtBox.Text = "THIS SERVICE IS NOT YET AVAILABLE." & vbNewLine & vbNewLine & _
                '                      "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR" & vbNewLine & _
                '                       "MEMBER SERVICE REPRESENTATIVE AT OUR SERVICE COUNTER" & vbNewLine & _
                '                       "AT THE SSS BRANCH." & vbNewLine & vbNewLine
                '    ' "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                '    'Techincal Retirement Print
                'Case "TRP01"

                '    newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR APPLICATION FOR" & vbNewLine & _
                '                     "TECHNICAL RETIREMENT. " & vbNewLine & vbNewLine & _
                '                     "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW: " & vbNewLine & vbNewLine & _
                '                     "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'ACOP
            Case "ACOP01"

                newTxtBox.Text = "WE REGRET THAT YOU CANNOT PROCEED WITH YOUR ANNUAL CONFIRMATION OF " & vbNewLine &
                                 "PENSIONER DUE TO YOUR POSTED CONTRIBUTIONS AFTER YOUR RETIREMENT" & vbNewLine &
                                 "DATE. " & vbNewLine &
                                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


                'ACOP
            Case "ACOP02"

                Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)
                'Dim getYearBday As String = Date.Today.Year + 1
                Dim getYearBday As String = db.putSingleValue("select DATEPART(YEAR, GETDATE())") + 1
                Dim getDayBday As String = bdate.Day
                Dim getMonthBday As String = MonthName(bdate.Month)
                getMonthBday = UCase(getMonthBday)
                finalDateAcop = getMonthBday & " " & getDayBday & ", " & getYearBday
                Dim finalDateAcop1 As String = getMonthBday & " " & getYearBday
                Dim mon1 As String = bdate.Month
                Dim numDate As Date = bdate.Month & "/" & bdate.Day & "/" & getYearBday
                Dim bMonth As Date = DateAdd(DateInterval.Month, -6, numDate)
                Dim bmnth1 As String = UCase(MonthName(bMonth.Month))
                Dim bMonth1 As Date = DateAdd(DateInterval.Month, -1, numDate)
                Dim bmnth2 As String = UCase(MonthName(bMonth1.Month))


                'newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR ANNUAL CONFIRMATION OF PENSIONER " & vbNewLine &
                '                 "(ACOP) COMPLIANCE. YOUR NEXT SCHEDULE WILL BE IN " & finalDateAcop1 & "." & vbNewLine &
                '                 "YOU MAY ALSO REPORT FROM " & bmnth1 & " TO " & bmnth2 & " " & getYearBday & vbNewLine &
                '                 "FOR EARLY COMPLIANCE." & vbNewLine & vbNewLine &
                '                 "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine &
                '   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


                newTxtBox.Text = "YOU HAVE SUCCESSFULLY SUBMITTED YOUR ANNUAL CONFIRMATION OF PENSIONER " & vbNewLine &
                                "(ACOP) COMPLIANCE. YOUR NEXT SCHEDULE WILL BE IN " & finalDateAcop1 & "." & vbNewLine &
                                "YOU MAY ALSO REPORT FROM " & bmnth1 & " TO " & bmnth2 & " " & getYearBday & " FOR EARLY COMPLIANCE." & vbNewLine & vbNewLine &
                                "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW:" & vbNewLine & vbNewLine &
                  "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'ACOP
            Case "ACOP03"
                ' INSERT DATE HERE AFTER DATE ON *********
                Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)

                Dim getYearBday As String = db.putSingleValue("select DATEPART(year,max( NXTSUBM)) from SSTRANSACOP  where ssnum ='" & tempSSNum & "' GROUP BY SSNUM ")
                Dim getDayBday As String = bdate.Day
                Dim getMonthBday As String = MonthName(bdate.Month)
                getMonthBday = UCase(getMonthBday)
                finalDateAcop = getMonthBday & " " & getDayBday & ", " & getYearBday

                newTxtBox.Text = "SORRY, YOU ARE ONLY ALLOWED TO SUBMIT YOUR ANNUAL CONFIRMATION OF " & vbNewLine &
                                 "PENSIONER COMPLIANCE NOT EARLIER THAN 6 MONTHS PRIOR TO YOUR  " & vbNewLine &
                                 "SCHEDULED DATE ON " & finalDateAcop & "." & vbNewLine & vbNewLine &
                  "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'ACOP
            Case "ACOP04"

                Dim bdate As Date = printF.GetDateBith(_frmWebBrowser.WebBrowser1)

                Dim ssnum As String = UsrfrmPageHeader1.lblSSSNo.Text.Replace("-", "")
                Dim getYearBday As String = db.putSingleValue("select DATEPART(year,max(NXTSUBM)) from SSTRANSACOP  where ssnum ='" & tempSSNum & "' GROUP BY SSNUM ")
                Dim getDayBday As String = bdate.Day
                Dim getMonthBday As String = MonthName(bdate.Month)
                getMonthBday = UCase(getMonthBday)
                Dim finalDate As String = getMonthBday & " " & getDayBday & ", " & getYearBday
                Dim finalDateAcop1 As String = getMonthBday & " " & getYearBday
                Dim mon1 As String = bdate.Month
                Dim numDate As Date = bdate.Month & "/" & bdate.Day & "/" & getYearBday
                Dim bMonth As Date = DateAdd(DateInterval.Month, -6, numDate)
                Dim bmnth1 As String = UCase(MonthName(bMonth.Month))
                Dim bMonth1 As Date = DateAdd(DateInterval.Month, -1, numDate)
                Dim bmnth2 As String = UCase(MonthName(bMonth1.Month))
                newTxtBox.Text = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR ANNUAL CONFIRMATION OF" & vbNewLine &
                                 "PENSIONER (ACOP) COMPLIANCE  FOR THE CURRENT YEAR. YOUR NEXT" & vbNewLine &
                                 "SCHEDULE WILL BE IN " & finalDateAcop1 & "." & " YOU MAY ALSO REPORT FROM" & vbNewLine &
                                 bmnth1 & " TO " & bmnth2 & " " & getYearBday & " FOR EARLY COMPLIANCE." & vbNewLine &
                                 vbNewLine & vbNewLine &
                "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "ACOP06"

                newTxtBox.Text = "SORRY, BUT YOU HAVE ALREADY SUBMITTED YOUR ANNUAL CONFIRMATION OF " & vbNewLine & vbNewLine &
                                 "PENSIONER (ACOP) COMPLIANCE  FOR THE CURRENT YEAR. FOR ANY CONCERNS," & vbNewLine &
                                 "PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE AT OUR" & vbNewLine &
                                  "SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

            Case "ACOP07"

                newTxtBox.Text = "WE REGRET THAT YOU CANNOT PROCEED WITH YOUR ANNUAL CONFIRMATION OF " & vbNewLine &
                                 "PENSIONER DUE TO YOUR POSTED CONTRIBUTIONS AFTER YOUR RETIREMENT" & vbNewLine &
                                 "DATE. PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE REPRESENTATIVE" & vbNewLine &
                                 "AT OUR SERVICE COUNTER AT THE SSS BRANCH, OTHERWISE YOUR PENSION" & vbNewLine &
                                 "WILL BE SUSPENDED. " & vbNewLine & vbNewLine &
                                 "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'Pension Maintenance
            Case "PM01"

                newTxtBox.Text = "YOUR MAILING ADDRESS/CONTACT INFORMATION WAS SUCCESSFULLY UPDATED. " & vbNewLine & vbNewLine &
                                 "PLEASE TAKE NOTE OF YOUR TRANSACTION REFERENCE NUMBER BELOW." & vbNewLine & vbNewLine &
                                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'NO UMID CARD
            Case "NUC"

                newTxtBox.Text = "SORRY, BUT YOU NEED TO APPLY FOR UMID CARD IF YOU INTEND TO FILE" & vbNewLine &
                                 "A TECHNICAL RETIREMENT CLAIM USING THIS FACILITY. " & vbNewLine & vbNewLine &
                             "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text

                'SIMPLIFIED WEB REGISTRATION
            Case "MRG01"

                newTxtBox.Text = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE SUBMITING YOUR REQUEST." & vbNewLine &
                                 "PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'SIMPLIFIED WEB REGISTRATION
            Case "MRG02"
                '                "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED TO SUBMIT THEIR"
                newTxtBox.Text = "YOU HAVE SUCCESSFULLY CREATED YOUR WEB ACCOUNT. PLEASE CHECK YOUR" & vbNewLine &
                                 "EMAIL FOR THE VERIFICATION LINK TO ACTIVATE YOUR ACCOUNT." & vbNewLine & vbNewLine &
                                 "IF YOU ARE UNABLE TO RECEIVE YOUR EMAIL FROM YOUR INBOX, PLEASE" & vbNewLine &
                                 "CHECK YOUR SPAM OR JUNK FOLDER. " & vbNewLine & vbNewLine &
                   "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
                'SIMPLIFIED WEB REGISTRATION
            Case "MRG03"
                newTxtBox.Text = outPut
            Case "EMN01"


                newTxtBox.Text = authenticationMsg.ToUpper & vbNewLine & vbNewLine &
                                "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
            Case "SWR01"


                newTxtBox.Text = authenticationMsg.ToUpper & vbNewLine & vbNewLine & txnNoLabel
                                '"TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
            Case "SET001"

                newTxtBox.Text = authenticationMsg.ToUpper & vbNewLine & vbNewLine & txnNoLabel
                                '"TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text
            Case "SET002"

                newTxtBox.Text = authenticationMsg.ToUpper

            Case Else
                '                "SORRY, ONLY QUALIFIED FEMALE MEMBERS ARE ALLOWED TO SUBMIT THEIR"

                newTxtBox.Text = "SORRY, THERE WAS AN ERROR ENCOUNTERED WHILE SUBMITTING YOUR REQUEST. " & vbNewLine &
                                "PLEASE TRY AGAIN LATER." & vbNewLine & vbNewLine &
                 "FOR ANY CONCERNS, PLEASE SEEK ASSISTANCE FROM OUR MEMBER SERVICE" & vbNewLine &
                                  "REPRESENTATIVE AT OUR SERVICE COUNTER AT THE SSS BRANCH." & vbNewLine & vbNewLine &
                    "TRANSACTION REFERENCE NUMBER: " & lblTransactionNo.Text


        End Select

        'If lblTransactionNo.Text = "Transaction Number :" Then
        '    Label9.Visible = False
        '    lblTransactionNo.Visible = False
        'Else
        '    Label9.Visible = True
        '    lblTransactionNo.Visible = True
        'End If

        '  _frmPleaseWait.Hide()


    End Sub

    Private Sub _frmUserAuthentication_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub
End Class