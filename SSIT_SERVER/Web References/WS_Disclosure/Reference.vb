﻿'------------------------------------------------------------------------------
' <auto-generated>
'     This code was generated by a tool.
'     Runtime Version:4.0.30319.1
'
'     Changes to this file may cause incorrect behavior and will be lost if
'     the code is regenerated.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict Off
Option Explicit On

Imports System
Imports System.ComponentModel
Imports System.Diagnostics
Imports System.Web.Services
Imports System.Web.Services.Protocols
Imports System.Xml.Serialization

'
'This source code was auto-generated by Microsoft.VSDesigner, Version 4.0.30319.1.
'
Namespace WS_Disclosure
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Web.Services.WebServiceBindingAttribute(Name:="DisclosureWebserviceImplPortBinding", [Namespace]:="http://webservice/")>  _
    Partial Public Class DisclosureWebserviceImplService
        Inherits System.Web.Services.Protocols.SoapHttpClientProtocol
        
        Private calldisclosureOperationCompleted As System.Threading.SendOrPostCallback
        
        Private disAuthenticateOperationCompleted As System.Threading.SendOrPostCallback
        
        Private useDefaultCredentialsSetExplicitly As Boolean
        
        '''<remarks/>
        Public Sub New()
            MyBase.New
            Me.Url = Global.SSIT_SERVER.My.MySettings.Default.SSIT_SERVER_WS_Disclosure_DisclosureWebserviceImplService
            If (Me.IsLocalFileSystemWebService(Me.Url) = true) Then
                Me.UseDefaultCredentials = true
                Me.useDefaultCredentialsSetExplicitly = false
            Else
                Me.useDefaultCredentialsSetExplicitly = true
            End If
        End Sub
        
        Public Shadows Property Url() As String
            Get
                Return MyBase.Url
            End Get
            Set
                If (((Me.IsLocalFileSystemWebService(MyBase.Url) = true)  _
                            AndAlso (Me.useDefaultCredentialsSetExplicitly = false))  _
                            AndAlso (Me.IsLocalFileSystemWebService(value) = false)) Then
                    MyBase.UseDefaultCredentials = false
                End If
                MyBase.Url = value
            End Set
        End Property
        
        Public Shadows Property UseDefaultCredentials() As Boolean
            Get
                Return MyBase.UseDefaultCredentials
            End Get
            Set
                MyBase.UseDefaultCredentials = value
                Me.useDefaultCredentialsSetExplicitly = true
            End Set
        End Property
        
        '''<remarks/>
        Public Event calldisclosureCompleted As calldisclosureCompletedEventHandler
        
        '''<remarks/>
        Public Event disAuthenticateCompleted As disAuthenticateCompletedEventHandler
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://webservice/", ResponseNamespace:="http://webservice/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function calldisclosure(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal issnum As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iernum As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iloan_type As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iln_amt As Double, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iinstallment_term As Integer, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iurlds As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iprevln_amount As Double, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iservcharge As Double, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal itransac_token As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal itoken_id As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal ier_seq_no As String, <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal iaddress As String) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> disclosureWsResponse
            Dim results() As Object = Me.Invoke("calldisclosure", New Object() {issnum, iernum, iloan_type, iln_amt, iinstallment_term, iurlds, iprevln_amount, iservcharge, itransac_token, itoken_id, ier_seq_no, iaddress})
            Return CType(results(0),disclosureWsResponse)
        End Function
        
        '''<remarks/>
        Public Overloads Sub calldisclosureAsync(ByVal issnum As String, ByVal iernum As String, ByVal iloan_type As String, ByVal iln_amt As Double, ByVal iinstallment_term As Integer, ByVal iurlds As String, ByVal iprevln_amount As Double, ByVal iservcharge As Double, ByVal itransac_token As String, ByVal itoken_id As String, ByVal ier_seq_no As String, ByVal iaddress As String)
            Me.calldisclosureAsync(issnum, iernum, iloan_type, iln_amt, iinstallment_term, iurlds, iprevln_amount, iservcharge, itransac_token, itoken_id, ier_seq_no, iaddress, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub calldisclosureAsync(ByVal issnum As String, ByVal iernum As String, ByVal iloan_type As String, ByVal iln_amt As Double, ByVal iinstallment_term As Integer, ByVal iurlds As String, ByVal iprevln_amount As Double, ByVal iservcharge As Double, ByVal itransac_token As String, ByVal itoken_id As String, ByVal ier_seq_no As String, ByVal iaddress As String, ByVal userState As Object)
            If (Me.calldisclosureOperationCompleted Is Nothing) Then
                Me.calldisclosureOperationCompleted = AddressOf Me.OncalldisclosureOperationCompleted
            End If
            Me.InvokeAsync("calldisclosure", New Object() {issnum, iernum, iloan_type, iln_amt, iinstallment_term, iurlds, iprevln_amount, iservcharge, itransac_token, itoken_id, ier_seq_no, iaddress}, Me.calldisclosureOperationCompleted, userState)
        End Sub
        
        Private Sub OncalldisclosureOperationCompleted(ByVal arg As Object)
            If (Not (Me.calldisclosureCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent calldisclosureCompleted(Me, New calldisclosureCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        <System.Web.Services.Protocols.SoapDocumentMethodAttribute("", RequestNamespace:="http://webservice/", ResponseNamespace:="http://webservice/", Use:=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle:=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)>  _
        Public Function disAuthenticate(<System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> ByVal token_id As String) As <System.Xml.Serialization.XmlElementAttribute("return", Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)> disWsparams
            Dim results() As Object = Me.Invoke("disAuthenticate", New Object() {token_id})
            Return CType(results(0),disWsparams)
        End Function
        
        '''<remarks/>
        Public Overloads Sub disAuthenticateAsync(ByVal token_id As String)
            Me.disAuthenticateAsync(token_id, Nothing)
        End Sub
        
        '''<remarks/>
        Public Overloads Sub disAuthenticateAsync(ByVal token_id As String, ByVal userState As Object)
            If (Me.disAuthenticateOperationCompleted Is Nothing) Then
                Me.disAuthenticateOperationCompleted = AddressOf Me.OndisAuthenticateOperationCompleted
            End If
            Me.InvokeAsync("disAuthenticate", New Object() {token_id}, Me.disAuthenticateOperationCompleted, userState)
        End Sub
        
        Private Sub OndisAuthenticateOperationCompleted(ByVal arg As Object)
            If (Not (Me.disAuthenticateCompletedEvent) Is Nothing) Then
                Dim invokeArgs As System.Web.Services.Protocols.InvokeCompletedEventArgs = CType(arg,System.Web.Services.Protocols.InvokeCompletedEventArgs)
                RaiseEvent disAuthenticateCompleted(Me, New disAuthenticateCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState))
            End If
        End Sub
        
        '''<remarks/>
        Public Shadows Sub CancelAsync(ByVal userState As Object)
            MyBase.CancelAsync(userState)
        End Sub
        
        Private Function IsLocalFileSystemWebService(ByVal url As String) As Boolean
            If ((url Is Nothing)  _
                        OrElse (url Is String.Empty)) Then
                Return false
            End If
            Dim wsUri As System.Uri = New System.Uri(url)
            If ((wsUri.Port >= 1024)  _
                        AndAlso (String.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) = 0)) Then
                Return true
            End If
            Return false
        End Function
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://webservice/")>  _
    Partial Public Class disclosureWsResponse
        
        Private er_noField As String
        
        Private first_amortField As String
        
        Private installment_termField As Integer
        
        Private ln_amtField As Double
        
        Private loanbalField As String
        
        Private monthly_amortField As Double
        
        Private msgField As String
        
        Private net_proceedsField As String
        
        Private pathField As String
        
        Private ssnumField As String
        
        Private urlField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property er_no() As String
            Get
                Return Me.er_noField
            End Get
            Set
                Me.er_noField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property first_amort() As String
            Get
                Return Me.first_amortField
            End Get
            Set
                Me.first_amortField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property installment_term() As Integer
            Get
                Return Me.installment_termField
            End Get
            Set
                Me.installment_termField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property ln_amt() As Double
            Get
                Return Me.ln_amtField
            End Get
            Set
                Me.ln_amtField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property loanbal() As String
            Get
                Return Me.loanbalField
            End Get
            Set
                Me.loanbalField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property monthly_amort() As Double
            Get
                Return Me.monthly_amortField
            End Get
            Set
                Me.monthly_amortField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property msg() As String
            Get
                Return Me.msgField
            End Get
            Set
                Me.msgField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property net_proceeds() As String
            Get
                Return Me.net_proceedsField
            End Get
            Set
                Me.net_proceedsField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property path() As String
            Get
                Return Me.pathField
            End Get
            Set
                Me.pathField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property ssnum() As String
            Get
                Return Me.ssnumField
            End Get
            Set
                Me.ssnumField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property url() As String
            Get
                Return Me.urlField
            End Get
            Set
                Me.urlField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.1"),  _
     System.SerializableAttribute(),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code"),  _
     System.Xml.Serialization.XmlTypeAttribute([Namespace]:="http://webservice/")>  _
    Partial Public Class disWsparams
        
        Private msgField As String
        
        Private token_idField As String
        
        Private transac_tokenField As String
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property msg() As String
            Get
                Return Me.msgField
            End Get
            Set
                Me.msgField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property token_id() As String
            Get
                Return Me.token_idField
            End Get
            Set
                Me.token_idField = value
            End Set
        End Property
        
        '''<remarks/>
        <System.Xml.Serialization.XmlElementAttribute(Form:=System.Xml.Schema.XmlSchemaForm.Unqualified)>  _
        Public Property transac_token() As String
            Get
                Return Me.transac_tokenField
            End Get
            Set
                Me.transac_tokenField = value
            End Set
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub calldisclosureCompletedEventHandler(ByVal sender As Object, ByVal e As calldisclosureCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class calldisclosureCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As disclosureWsResponse
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),disclosureWsResponse)
            End Get
        End Property
    End Class
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1")>  _
    Public Delegate Sub disAuthenticateCompletedEventHandler(ByVal sender As Object, ByVal e As disAuthenticateCompletedEventArgs)
    
    '''<remarks/>
    <System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.0.30319.1"),  _
     System.Diagnostics.DebuggerStepThroughAttribute(),  _
     System.ComponentModel.DesignerCategoryAttribute("code")>  _
    Partial Public Class disAuthenticateCompletedEventArgs
        Inherits System.ComponentModel.AsyncCompletedEventArgs
        
        Private results() As Object
        
        Friend Sub New(ByVal results() As Object, ByVal exception As System.Exception, ByVal cancelled As Boolean, ByVal userState As Object)
            MyBase.New(exception, cancelled, userState)
            Me.results = results
        End Sub
        
        '''<remarks/>
        Public ReadOnly Property Result() As disWsparams
            Get
                Me.RaiseExceptionIfNecessary
                Return CType(Me.results(0),disWsparams)
            End Get
        End Property
    End Class
End Namespace
