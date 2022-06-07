Public Class insertProcedure
    Dim db As New ConnectionString

    Public Function insertMatnotif(ByVal ssNumber As String, ByVal BRANCH_CODE As String, ByVal YEAR As String, ByVal WTRANS_NO As String, ByVal DELIV_DATE As String, ByVal LDELIV_NO As String, ByVal LDELIV_DATE As String,
                                   ByVal CREATE_DATE As String, ByVal CREATE_TIME As String, ByVal kiosk_ID As String)

        'db.sql = "insert into SSTRANSINFORTERMMN values ('" & xtd.getTempSSS & "','" & xtd.getFullname & "','" & _frmMaternityNotification.txtNumber.Text & _
        '                       "','" & _frmMaternityNotification.DateTimePicker1.Value & "','" & _frmMaternityNotification.DateTimePicker2.Value & "','" & kioskID & "','" & kioskBranch & _
        '                       "'. '" & kioskIP & "', '" & kioskCluster & "', '" & kioskGroup & "')"
        'db.ExecuteSQLQuery(db.sql)
        db.sql = "insert into SSTRANSINFORTERMMN (SSNUM,BRANCH_CD,YEAR,WTRANS_NO,DELIV_DATE,LDELIV_NO,LDELIV_DATE,ENCODE_DT,ENCODE_TME, KIOSK_ID) values ('" & ssNumber & "','" & BRANCH_CODE & "','" & YEAR &
                               "','" & WTRANS_NO & "','" & DELIV_DATE & "', '" & LDELIV_NO & "', '" & LDELIV_DATE &
                               "','" & CREATE_DATE & "','" & CREATE_TIME & "', '" & kiosk_ID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertEnhancedMatnotif(ByVal ssNumber As String, ByVal S_TRANID As String, ByVal DELIV_DATE As String, ByVal NAME As String, ByVal RELATIONSHIP As String,
                                           ByVal NO_OF_DAYS As Short, ByVal SSTRANS_NO As String)


        db.sql = String.Format("insert into SSTRANSINFORTERMEMN (SSNUM, S_TRANID, DELIV_DATE, NAME, RELATIONSHIP, NO_OF_ALLOCATED_DAYS, SSTRANS_NO, ENCODE_DT, ENCODE_TME, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN, FULL_NAME) values ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}', GETDATE(), GETDATE(), '{7}', '{8}', '{9}', '{10}', '{11}')", ssNumber, S_TRANID, DELIV_DATE, NAME, RELATIONSHIP, NO_OF_DAYS, SSTRANS_NO, kioskID, kioskBranchCD, kioskCluster, kioskGroup, HTMLDataExtractor.MemberFullName)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertPinChange(ByVal ssNumber As String, ByVal S_TRANID As String)
        db.sql = String.Format("insert into SSTRANSPINCHANGE (SSNUM, S_TRANID, ENCODE_DT, ENCODE_TME, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN) values ('{0}', '{1}', GETDATE(), GETDATE(), '{2}', '{3}', '{4}', '{5}')", ssNumber, S_TRANID, kioskID, kioskBranchCD, kioskCluster, kioskGroup)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertOnlineRetirement(ByVal certificationTag As String, ByVal ssNumber As String,
                                            ByVal dateOfContingency As String, ByVal address1 As String, ByVal email As String, ByVal landLine As String, ByVal mobileNo As String,
                                            ByVal advance18Months As String, ByVal bankBRSTN As String, ByVal acctNo As String, ByVal dateOfSeparation As String,
                                            ByVal retirementFlag As String, ByVal retirementAmount As String, ByVal appliedFrom As String, ByVal membershipStatus As String,
                                            ByVal employerSSNumber As String, ByVal employerERBR As String, ByVal setTransNo As String, ByVal transId As String)
        db.sql = String.Format("insert into SSTRANSINFOTERMOR (SSNUM, TRAN_ID, TRANS_NO, SET_TRANS_NO, CERTIFICATION_TAG, CONTINGENCY_DATE, ADDRESS, ADVANCE_18_MONTHS, BANK_BRSTN, ACCTNO, EMAIL, LANDLINE, MOBILE_NO, SEPARATION_DATE, RETIREMENT_FLAG, RETIREMENT_AMOUNT, APPLIED_FROM, MEMBERSHIP_STATUS, EMPLOYER_SSNUM, EMPLOYER_ERBR, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN, ENCODE_DT, ENCODE_TME, FULL_NAME) values ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', '{15}', '{16}', '{17}', '{18}', '{19}', '{20}', '{21}', '{22}', '{23}', GETDATE(), GETDATE(), '{24}')", ssNumber, transId, "", setTransNo, certificationTag, dateOfContingency, address1, advance18Months, bankBRSTN, acctNo, email, landLine, mobileNo, dateOfSeparation, retirementFlag, retirementAmount, appliedFrom, membershipStatus, employerSSNumber, employerERBR, kioskID, kioskBranchCD, kioskCluster, kioskGroup, HTMLDataExtractor.MemberFullName)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertSimplifiedWebRegistration(ByVal ssNumber As String, ByVal emailAddress As String, ByVal userId As String, ByVal sTranId As String, ByVal fullName As String)
        db.sql = String.Format("insert into SSTRANSINFOTERMSWR (SSNUM, EMAIL, USER_ID, S_TRANID, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN, FULL_NAME, ENCODE_DT, ENCODE_TME) values ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}', '{7}', '{8}', GETDATE(), GETDATE())", ssNumber, emailAddress, userId, sTranId, kioskID, kioskBranchCD, kioskCluster, kioskGroup, fullName)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertSalaryLoan(ByVal ssNumber As String, ByVal fullName As String, ByVal memberType As String, ByVal loanAmount As Decimal, ByVal netLoan As Decimal, ByVal disbursementAcct As String, ByVal employerId As String, ByVal employerName As String, ByVal employerBranchOffice As String, ByVal sTranId As String, ByVal setTranId As String)
        db.sql = String.Format("insert into SSTRANSSL (SSNUM, FULL_NAME, MEMBER_TYPE, LOAN_AMOUNT, NET_LOAN, DISBURSEMENT_ACCT, EMPLOYER_ID, EMPLOYER_NAME, EMPLOYER_BRANCH_OFFICE, SET_TRANS_NO, SSTRANS_NO, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN, ENCODE_DT, ENCODE_TME) values ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}', '{7}', '{8}', '{9}', '{10}', '{11}', '{12}', '{13}', '{14}', GETDATE(), GETDATE())", ssNumber, fullName, memberType, loanAmount, netLoan, disbursementAcct, employerId, employerName.Replace("'", "''"), employerBranchOffice.Replace("'", "''"), setTranId, sTranId, kioskID, kioskBranchCD, kioskCluster, kioskGroup)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertGSISLINK(ByVal ssNumber As String, ByVal csn As String, ByVal crn As String, ByVal firstName As String, ByVal middleName As String,
                                           ByVal lastName As String, ByVal suffix As String, ByVal gender As String, ByVal dob As String, ByVal ccdt As String, ByVal fullName As String)


        db.sql = String.Format("insert into SSTRANSGSISLINK (SSNUM, CSN, CRN, FIRST_NAME, MIDDLE_NAME, LAST_NAME, SUFFIX, GENDER, BIRTH_DATE, CCDT, ENCODE_DT, ENCODE_TME, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN) values ('{0}', '{1}', '{2}', '{3}', '{4}','{5}','{6}', '{7}', '{8}', '{9}', GETDATE(), GETDATE(), '{10}', '{11}', '{12}', '{13}')", ssNumber, csn, crn, firstName, middleName, lastName, suffix, gender, dob, ccdt, kioskID, kioskBranchCD, kioskCluster, kioskGroup)
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertCardActivate(ByVal ssNumber As String, ByVal crn As String)
        Try
            db.sql = String.Format("insert into SSTRANSCARDACTIVATE (SSNUM, CRN, ENCODE_DT, ENCODE_TME, KIOSK_ID, BRANCH_CD, CLSTR, DIVSN) values ('{0}', '{1}', GETDATE(), GETDATE(), '{2}', '{3}', '{4}', '{5}')", ssNumber, crn, kioskID, kioskBranchCD, kioskCluster, kioskGroup)
            db.ExecuteSQLQuery(db.sql)
        Catch ex As Exception
        End Try
    End Function

    Public Function insertSSEXITSURVEY(ByVal ssNumber As String, ByVal infoFound As Short, ByVal easyToUse As Short, ByVal overAllExp As Short, ByVal memberName As String)
        Try
            db.sql = String.Format("insert into SSEXITSURVEY (SSNUM, ENCODE_DT, ENCODE_TME, BRANCH_IP, BRANCH_CD, KIOSK_ID, INFO_FOUND, EASY_TO_USE_RATE, OVERALL_EXP_RATE, FULL_NAME, CLSTR, DIVSN) values ('{0}', GETDATE(), GETDATE(), '{1}', '{2}', '{3}', {4}, {5}, {6}, '{7}', '{8}', '{9}')", ssNumber, kioskIP, kioskBranchCD, kioskID, infoFound, easyToUse, overAllExp, memberName, kioskCluster, kioskGroup)
            db.ExecuteSQLQuery(db.sql)
        Catch ex As Exception
        End Try
    End Function

    Public Function insertTechRet(ByVal SSNUMBER As String, ByVal DOB As Date, ByVal DOC As Date, ByVal UMID_BANK As String, ByVal BANK_BRANCH As String, ByVal SAVACCTNO As String, ByVal ADDRESS1 As String, _
                                  ByVal ADDRESS2 As String, ByVal POSTALCODE As String, ByVal EADD As String, ByVal LANDLINE As String, ByVal MOBILE As String, ByVal USERTYPE As String, ByVal BRANCH_IP As String, ByVal BRANCH_CD As String, ByVal CLUSTER As String, ByVal DIVISION As String, ByVal transID As String, ByVal kioskID As String)
        db.sql = "insert into SSTRANSINFOTERMTR (SSNUM,DOBTH,DOCVRG,UMID_BANK,BANK_BR,TRANS_ACCTNO,STREET,BRGAY,POST_CD,EMAIL,PHONE,CELNO,USRTYP,ENCODE_DT,ENCODE_TME,BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,TAG, TRANID,KIOSK_ID) values ('" _
            & SSNUMBER & "','" & DOB & "','" & DOC & "','" & UMID_BANK & "','" & BANK_BRANCH & "','" & SAVACCTNO & _
                         "', '" & ADDRESS1 & "', '" & ADDRESS2 & "', '" & POSTALCODE & "','" & EADD & _
                         "', '" & LANDLINE & "', '" & MOBILE & "', '" & USERTYPE & "', '" & Date.Today.ToShortDateString & _
                         "', '" & TimeOfDay & "', '" & BRANCH_IP & "', '" & BRANCH_CD & "', '" & CLUSTER & "', '" & DIVISION & "', '" & "2" & "', '" & transID & "', '" & kioskID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertTechRetLumpSum(ByVal SSNUMBER As String, ByVal DOB As Date, ByVal LUMPSUMAMT As String, ByVal TAG As String, ByVal BRANCH_IP As String, ByVal BRANCH_CD As String, ByVal CLUSTER As String, ByVal DIVISION As String, ByVal TRANSID As String, ByVal kioskID As String)
        db.sql = "insert into SSTRANSINFOTERMTRLS (SSNUM,DOBTH,LUMPSUMAMT,TAG,DATECREATED,TIMESTAMP,BRANCH_IP,BRANCH_CD,CLUSTER,DIVISION, TRANID, KIOSK_ID) values ('" _
            & SSNUMBER & "','" & DOB & "','" & LUMPSUMAMT & "', '" & TAG & "', '" & Date.Today.ToString("MM/dd/yyyy") & "','" & TimeOfDay & "', '" & BRANCH_IP & "', '" & BRANCH_CD & "', '" & CLUSTER & "', '" & DIVISION & "', '" & TRANSID & "', '" & kioskID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertAcop(ByVal SSNUM As String, ByVal ENCODE_DT As Date, ByVal ENCODE_TME As Date, ByVal BRANCH_IP As String, ByVal BRANCH_CD As String, ByVal CLSTR As String, ByVal DIVSN As String, ByVal nextSubmission As String, ByVal TranID As String, ByVal kioskID As String)
        db.sql = "insert into SSTRANSACOP (SSNUM,ENCODE_DT,ENCODE_TME,BRANCH_IP,BRANCH_CD,CLSTR,DIVSN,NXTSUBM, TRANID,KIOSK_ID) values ('" _
            & SSNUM & "','" & ENCODE_DT & "','" & ENCODE_TME & "','" & BRANCH_IP & "','" & BRANCH_CD & "','" & CLSTR & _
                         "', '" & DIVSN & "','" & nextSubmission & "', '" & TranID & "','" & kioskID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

    Public Function insertDep(ByVal SSNUM As String, ByVal DPDNAME As String, ByVal DPDSTAT As String, ByVal branch_cd As String, ByVal ENCODE_DT As String, ByVal ENCODE_TME As String, ByVal TYPROFRPT As String, ByVal DPDDATE As String, ByVal tranID As String, ByVal kiosk_ID As String)
        db.sql = "INSERT INTO SSTRANSACOPAD (SSNUM,DPDNAME,DPDSTAT,BRANCH_CD,ENCODE_DT,ENCODE_TME,TYPOFRPT,DPDDATE, TRANID) values ('" & SSNUM & "', '" & DPDNAME & "', '" &
                              DPDSTAT & "', '" & branch_cd & "', '" & ENCODE_DT & "', '" & ENCODE_TME & "', '" & TYPROFRPT & "', '" & DPDDATE & "', '" & tranID & "', '" & kiosk_ID & "')"
        db.ExecuteSQLQuery(db.sql)
    End Function
    Public Function insertDep1(ByVal SSNUM As String, ByVal DPDNAME As String, ByVal DPDSTAT As String, ByVal branch_cd As String, ByVal ENCODE_DT As String, ByVal ENCODE_TME As String, ByVal TYPROFRPT As String, ByVal DPDDATE As String, ByVal tranID As String, ByVal kiosk_ID As String)
        db.sql = "INSERT INTO SSTRANSACOPAD (SSNUM,DPDNAME,DPDSTAT,BRANCH_CD,ENCODE_DT,ENCODE_TME,TYPOFRPT,TRANID) values ('" & SSNUM & "', '" & DPDNAME & "', '" &
                              DPDSTAT & "', '" & branch_cd & "', '" & ENCODE_DT & "', '" & ENCODE_TME & "', '" & TYPROFRPT & "', '" & tranID & "')"
        db.ExecuteSQLQuery(db.sql)
    End Function
    Public Function updateDep(ByVal SSNUM As String, ByVal DPDNAME As String, ByVal DPDSTAT As String, ByVal branch_cd As String, ByVal ENCODE_DT As String, ByVal ENCODE_TME As String, ByVal TYPROFRPT As String, ByVal DPDDATE As String)
        db.sql = db.sql = "UPDATE SSTRANSACOPAD set DPDSTAT = '" & DPDSTAT & "', BRANCH_CD = '" & branch_cd & "', ENCODE_DT= '" & ENCODE_DT & "', ENCODE_TME = '" & _
                               ENCODE_TME & "' , TYPOFRPT = '" & TYPROFRPT & "', DPDDATE = '" & DPDDATE & "' where DPDNAME = '" & DPDNAME & "' and SSNUM = '" & SSNUM & "' "


        db.ExecuteSQLQuery(db.sql)
    End Function

    Public Function insertPension(ByVal SSNUM As String, ByVal HOUSENO As String, ByVal SUBDIVISION As String, ByVal CITY As String, ByVal ZIPCODE As String _
                                  , ByVal LANDLINE As String, ByVal MOBILE As String, ByVal EMAIL As String, ByVal ENCODE_DT As Date, ByVal ENCODE_TME As String, ByVal BRANCH_CD As String _
                                  , ByVal CLSTR As String, ByVal DIVSN As String, ByVal kiosk_ID As String)
        db.sql = "insert into SSTRANSPM (SSNUM,HOUSENO,SUBDIVISION,CITY,ZIPCODE,LANDLINE,MOBILE,EMAIL,ENCODE_DT,ENCODE_TME,BRANCH_CD,CLSTR,DIVSN, KIOSK_ID) values ('" _
           & SSNUM & "','" & HOUSENO & "','" & SUBDIVISION & "','" & CITY & "','" & ZIPCODE & _
                        "', '" & LANDLINE & "', '" & MOBILE & "', '" & EMAIL & "', '" & ENCODE_DT & _
                        "', '" & ENCODE_TME & "', '" & BRANCH_CD & "', '" & CLSTR & "', '" & DIVSN & "', '" & kiosk_ID & "')"
        db.ExecuteSQLQuery(db.sql)


    End Function

    'Public Function insertAcopSSS(ByVal TRANID As String, ByVal SSNUM As String, ByVal REFNO As String, ByVal BRANCH As String)
    '    db.sql = "insert into IKAUDITREPMF (TRANID,SSNUM,REFNO,REFDT,BRANCH) values ('" _
    '        & TRANID & "','" & SSNUM & "','" & REFNO & "','" & Today & "','" & BRANCH & "')"
    '    db.ExecuteSQLQuery(db.sql)

    'End Function
    ' ********** WITH WORKSTATION
    'Public Function insertDependentSSS(ByVal TRANID As String, ByVal SSNUM As String, ByVal REFNO As String, ByVal BRANCH As String _
    '                                   , ByVal NAME_DEP As String, ByVal RELCD As String, ByVal RELTYP As String, ByVal STATDT As String, ByVal KIOSK_ID As String, ByVal WORKSTATION As String)
    '    db.sql = "insert into IKBENEFREPTF (TRANID,SSNUM,REFNO,REFDT,BRANCH,NAME$,RELCD,RELTYP,STATDT,TAG,KIOSK_ID,WORKSTATION) values ('" _
    '        & TRANID & "','" & SSNUM & "','" & REFNO & "','" & Today & "','" & BRANCH & "','" & NAME_DEP & "','" & RELCD & _
    '        "','" & RELTYP & "','" & STATDT & "', '0', '" & kioskID & "', '" & WORKSTATION & "')"
    '    db.ExecuteSQLQuery(db.sql)

    'End Function

    Public Function insertDependentSSS(ByVal TRANID As String, ByVal SSNUM As String, ByVal REFNO As String, ByVal BRANCH As String _
                                       , ByVal NAME_DEP As String, ByVal RELCD As String, ByVal RELTYP As String, ByVal STATDT As String, ByVal KIOSK_ID As String)
        db.sql = "insert into IKBENEFREPTF (TRANID,SSNUM,REFNO,REFDT,BRANCH,NAME$,RELCD,RELTYP,STATDT,TAG,KIOSK_ID) values ('" _
            & TRANID & "','" & SSNUM & "','" & REFNO & "','" & Today & "','" & BRANCH & "','" & NAME_DEP & "','" & RELCD &
            "','" & RELTYP & "','" & STATDT & "', '0', '" & kioskID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

    ' *********** OLD IKMAINHISTTF CODE.

    'Public Function insertPenSSS(ByVal TRANID As String, ByVal SSNUM As String, ByVal REFNO As String, ByVal BRANCH As String _
    '                           , ByVal ADD1 As String, ByVal ADD2 As String, ByVal PCODE As String, ByVal EMAIL As String _
    '                           , ByVal LANDLINE As String, ByVal MOBILE As String, ByVal kioskID As String, ByVal WORKSTATION As String)
    '    db.sql = "insert into IKMAINHISTTF  (TRANID,SSNUM,REFNO,REPDT,BRANCH,ADD1,ADD2,PCODE,EMAIL,LANDLINE,MOBILE,TAG,KIOSK_ID,WORKSTATION) values ('" _
    '        & TRANID & "','" & SSNUM & "','" & REFNO & "','" & Date.Today & "','" & BRANCH & "','" & ADD1 & "','" & ADD2 & "','" & PCODE & _
    '        "','" & EMAIL & "','" & LANDLINE & "','" & MOBILE & "', '0','" & kioskID & "', '" & WORKSTATION & "')"
    '    db.ExecuteSQLQuery(db.sql)

    'End Function

    Public Function insertPenSSS(ByVal TRANID As String, ByVal SSNUM As String, ByVal REFNO As String, ByVal BRANCH As String _
                                   , ByVal ADD1 As String, ByVal ADD2 As String, ByVal PCODE As String, ByVal EMAIL As String _
                                   , ByVal LANDLINE As String, ByVal MOBILE As String, ByVal kioskID As String)
        db.sql = "insert into IKMAINHISTTF  (TRANID,SSNUM,REFNO,REPDT,BRANCH,ADD1,ADD2,PCODE,EMAIL,LANDLINE,MOBILE,TAG,KIOSK_ID) values ('" _
            & TRANID & "','" & SSNUM & "','" & REFNO & "','" & Date.Today & "','" & BRANCH & "','" & ADD1 & "','" & ADD2 & "','" & PCODE & _
            "','" & EMAIL & "','" & LANDLINE & "','" & MOBILE & "', '0','" & kioskID & "')"
        db.ExecuteSQLQuery(db.sql)

    End Function

End Class
