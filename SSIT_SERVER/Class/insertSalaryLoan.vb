Public Class insertSalaryLoan
    Dim con As New ConnectionString
    Dim printf As New printModule
    Public Function insertAppSal(ByVal SSNBR As String, ByVal BRSNM As String, ByVal BRFNM As String, ByVal BRMID As String, ByVal HOME1 _
                                 As String, ByVal HOME2 As String, ByVal POSCD As String, ByVal ERID As String, ByVal LNTYP As String, ByVal ENCDR As String, ByVal ERBRN _
                                 As String, ByVal TUITN As String, ByVal TCONT As String, ByVal HIMSC As String, ByVal LNBAL As String, ByVal SRFEE _
                                 As String, ByVal SCFEE As String, ByVal AMONT As String, ByVal CITNM As String, ByVal LADTE As String, ByVal STYPE _
                                 As String, ByVal IPADD As String, ByVal BRANCH_CD As String, ByVal CLSTR As String, ByVal DIVSN As String, ByVal TRANSID As String, ByVal MEMSTATUS As String, ByVal kioskID As String)


        con.sql = "INSERT INTO SSTRANSAPPSL (IN_SSNBR,IN_BRSNM,IN_BRFNM,IN_BRMID,IN_HOME1,IN_HOME2,IN_POSCD,IN_ERID,IN_LNTYP,IN_ENCDR,IN_ERBRN,IN_TUITN,IN_TCONT,IN_HIMSC,IN_LNBAL,IN_SRFEE,IN_SCFEE,IN_AMONT,IN_CITNM,IN_LADTE,IN_STYPE,IN_IPADD,BRANCH_CD,CLSTR,DIVSN,ENCODE_DT,ENCODE_TME,S_TRANID,STRMEMBERSTATUS,KIOSK_ID) values ('" _
                            & SSNBR & "', '" & BRSNM & "', '" & BRFNM & "', '" & BRMID & "', '" & HOME1 & "', '" & HOME2 & "', '" & POSCD & "', '" & ERID & "', '" & LNTYP & "', '" & ENCDR & _
                            "', '" & ERBRN & "', '" & TUITN & "', '" & TCONT & "', '" & HIMSC & "', '" & LNBAL & "', '" & SRFEE & "', '" & SCFEE & "', '" & AMONT & "', '" & CITNM & _
                            "', '" & LADTE & "', '" & STYPE & "', '" & IPADD & "','" & BRANCH_CD & "','" & CLSTR & "' , '" & DIVSN & "','" & Date.Today.ToString("MM/dd/yyyy") & "','" & TimeOfDay & "', '" & TRANSID & "', '" & MEMSTATUS & "', '" & kioskID & "') "
        con.ExecuteSQLQuery(con.sql)
        'MsgBox("Application for Salary Loan was successfully submitted!", MsgBoxStyle.Information, "Salary Loan Application")




    End Function

    Public Function insertAppSaleMP(ByVal STRSSSID As String, ByVal STRTRANTYPE As String, ByVal STREMPMEMTAG As String, ByVal STRMEMBERSTATUS As String, ByVal strBankCode _
                             As String, ByVal strBankName As String, ByVal strERBR As String, ByVal strEmpId As String, ByVal STRPAYMENTOPT As String, ByVal LOANAMOUNT As String, ByVal STRADDRESS1 _
                             As String, ByVal STRADDRESS2 As String, ByVal strPostalcode As String, ByVal memfname As String, ByVal memmidInit As String, ByVal memlname _
                             As String, ByVal IN_TCONT As String, ByVal IN_HIMSC As String, ByVal IN_LNBAL As String, ByVal IN_SRFEE As String, ByVal IN_SCFEE _
                             As String, ByVal S_TRANID As String, ByVal IN_IPADD As String, ByVal BRANCH_CD As String, ByVal CLSTR As String, ByVal DIVSN As String, ByVal TRANSNO As String, ByVal kioskID As String)


        con.sql = "INSERT INTO SSTRANSAPPSLEMP (STRSSSID,STRTRANTYPE,STREMPMEMTAG,STRMEMBERSTATUS,strBankCode,strBankName,strERBR,strEmpId,STRPAYMENTOPT,LOANAMOUNT,STRADDRESS1,STRADDRESS2,strPostalcode,memfname,memmidInit,memlname,IN_TCONT,IN_HIMSC,IN_LNBAL,IN_SRFEE,IN_SCFEE,S_TRANID,IN_IPADD,BRANCH_CD,CLSTR,DIVSN,TRANSREFNO,ENCODE_DT,ENCODE_TME,KIOSK_ID) values ('" _
                            & STRSSSID & "', '" & STRTRANTYPE & "', '" & STREMPMEMTAG & "', '" & STRMEMBERSTATUS & "', '" & strBankCode & "', '" & strBankName & "', '" & strERBR & "', '" & strEmpId & "', '" & STRPAYMENTOPT & "', '" & LOANAMOUNT & _
                            "', '" & STRADDRESS1 & "', '" & STRADDRESS2 & "', '" & strPostalcode & "', '" & memfname & "', '" & memmidInit & "', '" & memlname & "', '" & IN_TCONT & "', '" & IN_HIMSC & "', '" & IN_LNBAL & _
                            "', '" & IN_SRFEE & "', '" & IN_SCFEE & "', '" & S_TRANID & "','" & IN_IPADD & "','" & BRANCH_CD & "','" & CLSTR & "' , '" & DIVSN & "', '" & TRANSNO & "','" & Date.Today.ToString("MM/dd/yyyy") & "','" & TimeOfDay & "','" & kioskID & "') "
        con.ExecuteSQLQuery(con.sql)
        'MsgBox("Application for Salary Loan was successfully submitted!", MsgBoxStyle.Information, "Salary Loan Application")




    End Function

    Public Function getDetailsForSalLoan(ByVal loanType As String, ByVal ssNum As String, ByVal memSurname As String, ByVal memGivenName As String, ByVal memMidName As String, ByVal memAdd As String, ByVal memzipCode As String, ByVal empID As String, ByVal empName As String, ByVal empAdd As String, ByVal empZipCode As String)
        Dim memAdd1, memAdd2, empAdd1, empAdd2 As String
        ' NOTE : memAdd1 is the members no. and street, barangay and town or district, while the memAdd2 is the members city or province. memAddress should be substring in order to get the memAdd1 and memAdd2. Same with empAdd.
        If memAdd = "" Then

        End If
        If empAdd = "" Then

        End If
        '_frmSalaryLoan.txtLoanType.Text = loanType
        Dim fname As String = printf.GetFirstName(_frmWebBrowser.WebBrowser1)
        Dim mname As String = printf.GetMiddleName(_frmWebBrowser.WebBrowser1)
        Dim lname As String = printf.GetLastName(_frmWebBrowser.WebBrowser1)
        _frmPRNApplication.UsrfrmPageHeader1.lblSSSNo.Text = ssNum
        lname = memSurname
        fname = memGivenName
        mname = memMidName
        '_frmSalaryLoanMember_v2.txtMemAdd1.Text = memAdd1
        ' _frmSalaryLoanMember_v2.txtMemAdd2.Text = memAdd2
        '_frmSalaryLoanMember_v2.txtMemZipCode.Text = memzipCode
        '_frmSalaryLoan.lblEmpID.Text = empID
        '_frmSalaryLoan.lblEmpName.Text = empName
        '_frmSalaryLoan.txtEmpAdd1.Text = empAdd1
        '_frmSalaryLoan.txtEmpAdd2.Text = empAdd2
        '_frmSalaryLoan.txtEmpZipCode.Text = empZipCode
    End Function


End Class
