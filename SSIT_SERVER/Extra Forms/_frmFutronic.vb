
Imports ScanAPIHelper
Imports System.Threading
Imports System.IO
Imports Innovatrics.AnsiIso
Imports Innovatrics.Sdk.Commons

Public Class _frmFutronic
    Private appPath As String = New Uri(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase)).LocalPath
    Private tempFile As String = String.Format("{0}\Temp\sagem.bmp", appPath)

    Public isForceClosed As Integer = 0
    Public formResponse As Integer = 2
    Public fp As FingerPosition = FingerPosition.LeftPrimary
    Public cardTemplates As New List(Of Byte())
    Public qualityThreshold As Integer = 90 '200

    Public secondsBeforeExpired As Short = 10
    Public runningSecondsBeforeExpired As Short = 0
    Public isRunTimer As Boolean = False

    Delegate Sub SetTextCallback(text As String)

    Public Const FTR_ERROR_EMPTY_FRAME As Integer = 4306
    Public Const FTR_ERROR_MOVABLE_FINGER As Integer = &H20000001
    Public Const FTR_ERROR_NO_FRAME As Integer = &H20000002
    Public Const FTR_ERROR_USER_CANCELED As Integer = &H20000003
    Public Const FTR_ERROR_HARDWARE_INCOMPATIBLE As Integer = &H20000004
    Public Const FTR_ERROR_FIRMWARE_INCOMPATIBLE As Integer = &H20000005
    Public Const FTR_ERROR_INVALID_AUTHORIZATION_CODE As Integer = &H20000006

    Private m_hDevice As Device
    Public m_bCancelOperation As Boolean
    Private m_Frame() As Byte
    Private m_bScanning As Boolean
    Private m_ScanMode As Byte
    Private m_bIsLFDSupported As Boolean
    Private m_bExit As Boolean

    Private m_Compatibility As String = ""
    Private m_Interfaces As New List(Of Integer)
    Private m_InterfaceSelected As Integer
    Private m_InvertImage As Boolean
    Private m_DetectFakeFinger As Boolean
    Private m_Dose() As Integer = {1, 2, 3, 4, 5, 6, 7}
    Private m_DoseSelected As Integer
    Private m_chkFrame As Boolean

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        If runningSecondsBeforeExpired = 0 Then
            'Timer1.Stop()
            'MessageBox.Show("Time is up!" & vbNewLine & vbNewLine & "Start: " & startTime & vbNewLine & "End: " & endTime)
            Close()
        Else
            If isRunTimer Then runningSecondsBeforeExpired -= 1
        End If
        'Me.Text = "SCAN FINGER - " & runningSecondsBeforeExpired
    End Sub

    Enum FingerPosition
        RightPrimary = 0
        RightThumb
        LeftPrimary
        LeftThumb
    End Enum

    Public Class ComboBoxItem
        Dim m_String As String
        Dim m_InterfaceNumber As Integer

        Public Sub New(ByVal value As String, ByVal interfaceNumber As Integer)
            m_String = value
            m_InterfaceNumber = interfaceNumber
        End Sub

        Public Overrides Function ToString() As String
            Return m_String
        End Function

        Public ReadOnly Property interfaceNumber() As Integer
            Get
                Return m_InterfaceNumber
            End Get
        End Property
    End Class

    Private Sub _frmFutronic_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        DoubleBuffered = True
        'CheckForIllegalCrossThreadCalls = false
    End Sub

    Public Sub Init()
        'Return

        m_Interfaces.Clear()
        m_hDevice = Nothing
        m_ScanMode = 0
        m_bScanning = False
        m_bExit = False
        Dim defaultInterface As Integer
        Dim status() As FTRSCAN_INTERFACE_STATUS

        Try
            defaultInterface = ScanAPIHelper.Device.BaseInterface
            status = ScanAPIHelper.Device.GetInterfaces()
            For i As Integer = 0 To status.Length - 1
                If status(i) = FTRSCAN_INTERFACE_STATUS.FTRSCAN_INTERFACE_STATUS_CONNECTED Then
                    Dim index As Integer
                    'index = m_cmbInterfaces.Items.Add(New ComboBoxItem(i.ToString(), i))
                    m_Interfaces.Add(i)
                    index = m_Interfaces.Count - 1
                    If (defaultInterface = i) Then m_InterfaceSelected = index
                End If
            Next i

            OpenDevice()
            m_InvertImage = True

            If m_hDevice Is Nothing Then Return

            m_hDevice.InvertImage = m_InvertImage
            m_DoseSelected = 3
            m_ScanMode = 0

            ScanAPIHelper.Device.BaseInterface = m_InterfaceSelected
            runningSecondsBeforeExpired = secondsBeforeExpired
            isRunTimer = True
            Timer1.Enabled = True
        Catch ex As ScanAPIException
            ShowError(ex)
        End Try
    End Sub

    Private Sub OpenDevice()
        Try
            ' Return

            m_hDevice = New Device()
            m_hDevice.Open()
            m_hDevice.DetectFakeFinger = False
            m_DetectFakeFinger = m_hDevice.DetectFakeFinger

            m_bIsLFDSupported = False
            Dim dinfo As DeviceInfo = m_hDevice.Information
            Select Case (dinfo.DeviceCompatibility)
                Case 0
                    m_Compatibility = "FS80"
                    m_bIsLFDSupported = True
                Case 1
                    m_Compatibility = "FS80"
                    m_bIsLFDSupported = True
                Case 4
                    m_Compatibility = "FS80"
                    m_bIsLFDSupported = True
                Case 11
                    m_Compatibility = "FS80"
                    m_bIsLFDSupported = True
                Case 5
                    m_Compatibility = "FS88"
                    m_bIsLFDSupported = True
                Case 7
                    m_Compatibility = "FS50"
                Case 8
                    m_Compatibility = "FS60"
                Case 9
                    m_Compatibility = "FS25"
                    m_bIsLFDSupported = True
                Case 10
                    m_Compatibility = "FS10"
                Case 13
                    m_Compatibility = "FS80H"
                    m_bIsLFDSupported = True
                Case 14
                    m_Compatibility = "FS88H"
                    m_bIsLFDSupported = True
                Case 15
                    m_Compatibility = "FS64"
                Case 16
                    m_Compatibility = "FS26E"
                Case 17
                    m_Compatibility = "FS80HS"
                Case 18
                    m_Compatibility = "FS26"
                Case Else
                    m_Compatibility = "Unknown device"
            End Select

        Catch ex As ScanAPIException
            If m_hDevice IsNot Nothing Then
                m_hDevice.Dispose()
                m_hDevice = Nothing
                ShowError(ex)
            End If
        End Try
    End Sub

    Public Sub CloseDevice()
        Try
            m_bCancelOperation = True
            If Not m_hDevice Is Nothing Then m_hDevice.Dispose()
            m_hDevice = Nothing
            m_DetectFakeFinger = False
            m_picture.Image = Nothing
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ShowError(ByRef ex As ScanAPIException)
        Dim szMessage As String
        Select Case (ex.ErrorCode)
            Case FTR_ERROR_EMPTY_FRAME
                szMessage = "Empty Frame"
            Case FTR_ERROR_MOVABLE_FINGER
                szMessage = "Movable Finger"
            Case FTR_ERROR_NO_FRAME
                szMessage = "Fake Finger"
            Case FTR_ERROR_HARDWARE_INCOMPATIBLE
                szMessage = "Incompatible Hardware"
            Case FTR_ERROR_FIRMWARE_INCOMPATIBLE
                szMessage = "Incompatible Firmware"
            Case FTR_ERROR_INVALID_AUTHORIZATION_CODE
                szMessage = "Invalid Authorization Code"
            Case Else
                szMessage = String.Format("Error code: {0}", ex.ErrorCode)
        End Select
        SetMessageText(szMessage)
    End Sub

    Private Sub SetMessageText(ByVal text As String)
        If m_bExit Then
            Exit Sub
        End If
        If (m_textMessage.InvokeRequired) Then
            Dim d As SetTextCallback = New SetTextCallback(AddressOf Me.SetMessageText)
            Me.Invoke(d, New Object() {text})
        Else
            Me.m_textMessage.Text = text
            Me.Update()
        End If
    End Sub

    Private Sub SetLabelMessage(ByVal text As String)
        If (Label8.InvokeRequired) Then
            Dim d As SetTextCallback = New SetTextCallback(AddressOf Me.SetLabelMessage)
            Me.Invoke(d, New Object() {text})
        Else
            Me.Label8.Text = text
            Me.Update()
        End If
    End Sub

    'Private Sub m_btnGetFrame_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_btnGetFrame.Click
    Public Sub StartStopScan()
        If m_hDevice Is Nothing Then Init()

        If Not m_bScanning Then
            formResponse = 2
            BindGif()
            PlaceYourFingerMessage()
            m_bCancelOperation = False
            Dim WorkerThread As Thread = New Thread(New ThreadStart(AddressOf CaptureThread))
            WorkerThread.Start()
        Else
            m_bCancelOperation = True
        End If
    End Sub

    Private Sub CaptureThread()
        m_bScanning = True
        While (Not m_bCancelOperation)
            GetFrame()
            If m_Frame IsNot Nothing Then
                Dim myFile As MyBitmapFile = New MyBitmapFile(m_hDevice.ImageSize.Width, m_hDevice.ImageSize.Height, m_Frame)
                Dim BmpStream As MemoryStream = New MemoryStream(myFile.BitmatFileData)
                Dim Bmp As Bitmap = New Bitmap(BmpStream)
                m_picture.Image = Bmp
                Dim score As Integer = 0
                Try
                    If Not Directory.Exists("Temp") Then Directory.CreateDirectory("Temp")
                    Dim file As FileStream = New FileStream(tempFile, FileMode.Create)
                    file.Write(myFile.BitmatFileData, 0, myFile.BitmatFileData.Length)
                    file.Close()

                    score = GetImageQuality(tempFile)
                Catch ex As Exception

                End Try

                If score = 0 Then
                    lblImageQ.Text = 0
                    isRunTimer = True
                    PlaceYourFingerMessage()
                Else
                    runningSecondsBeforeExpired = secondsBeforeExpired
                    isRunTimer = False
                    SetLabelMessage("")
                End If

                If score >= CInt(txtQT.Text) Then
                    StartStopScan()
                    SaveToAnsi378()
                End If
            Else
                m_picture.Image = Nothing
                Try
                    Invoke(New Action(Sub()
                                          isRunTimer = True
                                          lblImageQ.Text = 0
                                          PlaceYourFingerMessage()
                                      End Sub))
                Catch ex As Exception
                End Try
            End If
            Thread.Sleep(10)
        End While
        m_bScanning = False
    End Sub

    Private Sub GetFrame()
        Try
            If m_hDevice Is Nothing Then Invoke(New Action(AddressOf Init))

            If m_ScanMode = 0 Then
                m_Frame = m_hDevice.GetFrame()
            Else
                m_Frame = m_hDevice.GetImage(m_ScanMode)
            End If
            SetMessageText("OK")
        Catch ex As ScanAPIException
            If m_Frame IsNot Nothing Then
                m_Frame = Nothing
                ShowError(ex)
            End If
        End Try
    End Sub

    'Private Sub m_chkFrame_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles m_chkFrame.CheckedChanged
    Private Sub CheckFrame()
        If (m_chkFrame) Then
            m_ScanMode = 0
        Else
            m_ScanMode = m_Dose(m_DoseSelected) + 1
        End If
    End Sub

    'Private Sub m_cmbDose_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
    '    m_ScanMode = m_cmbDose.SelectedIndex + 1
    'End Sub

    Private Sub _frmFutronic_FormClosed(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosedEventArgs) Handles MyBase.FormClosed
        'm_bExit = True
        'm_bCancelOperation = True
        'If m_hDevice IsNot Nothing Then
        '    m_hDevice.Dispose()
        '    m_hDevice = Nothing
        'End If

        'm_bCancelOperation = True
        'm_hDevice.Dispose()
        'm_hDevice = Nothing
        'm_DetectFakeFinger = False
        'm_picture.Image = Nothing
    End Sub

    Private Function GetImageQuality(ByVal bmpFile As String) As Integer
        Dim score As Integer = 0
        Try
            Dim iEngine As IEngine = IEngine.Instance
            iEngine.Init()
            Dim _ansi As Ansi = Ansi.Instance
            Dim rawImage As RawImage = RawImageExtension.LoadBmp(tempFile)
            'Dim ansiTemplate = _ansi.CreateTemplate(rawImage)
            'Dim result = _ansi.GetMinutiae(ansiTemplate).Count
            Invoke(New Action(Sub()
                                  score = rawImage.GetImageQuality
                                  lblImageQ.Text = score
                                  'lblMinutiaeCnt.Text = result
                              End Sub))
            iEngine.Terminate()
        Catch ex As Exception
            WriteToLog(String.Format("SaveToAnsi378(): {0}", ex.Message))
        End Try

        Return score
    End Function

    Private Sub SaveToAnsi378()
        Try
            Invoke(New Action(Sub()
                                  Dim iEngine As IEngine = IEngine.Instance
                                  iEngine.Init()
                                  Dim _ansi As Ansi = Ansi.Instance
                                  Dim rawImage As RawImage = RawImageExtension.LoadBmp(tempFile)
                                  Dim ansiTemplate() As Byte = _ansi.CreateTemplate(rawImage)
                                  'Dim ansiTemplate2() As Byte = Nothing
                                  '_ansi.SaveTemplate(tempFile.Replace(".bmp", ".ansi-fmr"), ansiTemplate)

                                  'Dim cardTemplatesSource As String = "D:\Futronic\Debug\Temp\sagem templates\1"
                                  Dim isMatch As Boolean = False

                                  For Each cardTemplate In cardTemplates
                                      Dim matchScore As Integer = _ansi.VerifyMatch(ansiTemplate, cardTemplate, 10)
                                      'MessageBox.Show("qualityThreshold: " & qualityThreshold & ", Score: " & matchScore)
                                      If matchScore >= qualityThreshold Then
                                          isMatch = True
                                      End If
                                  Next

                                  'For Each _file As String In Directory.GetFiles(cardTemplatesSource)
                                  '    Dim ansiTemplate2() As Byte = File.ReadAllBytes(_file)
                                  '    Dim matchScore As Integer = _ansi.VerifyMatch(ansiTemplate, ansiTemplate2, 10)
                                  '    If matchScore >= qualityThreshold Then
                                  '        isMatch = True
                                  '        'MsgBox(String.Format("File {0}: Verif score is {1}", Path.GetFileNameWithoutExtension(_file), _ansi.VerifyMatch(ansiTemplate, ansiTemplate2, 10)))
                                  '    End If
                                  'Next

                                  iEngine.Terminate()

                                  If isMatch Then
                                      formResponse = 0
                                      CloseDevice()
                                      Close()
                                  Else
                                      formResponse = 1
                                      Hide()
                                  End If
                              End Sub))
        Catch ex As Exception
            WriteToLog(String.Format("SaveToAnsi378(): {0}", ex.Message))
        End Try

    End Sub

    Public Shared Function VerifyTemplate(ByVal ansi1() As Byte, ByVal ansi2() As Byte, ByVal maxRotation As Integer) As Boolean
        System.Threading.Thread.Sleep(3000)
        Dim iEngine As IEngine = IEngine.Instance
        iEngine.Init()
        Dim _ansi As Ansi = Ansi.Instance
        Dim intResult As Integer = _ansi.VerifyMatch(ansi1, ansi2, maxRotation)
        'intResult>= 80
        iEngine.Terminate()
        Return intResult
    End Function

    Private Sub WriteToLog(ByVal desc As String)
        Dim file As String = "FutronicLog_" & Now.ToString("yyyyMMdd")
        Using sw As New StreamWriter(file, True)
            sw.WriteLine(Now & " " & desc)
            sw.Dispose()
            sw.Close()
        End Using
    End Sub

    Private Sub BindGif()
        Select Case fp
            Case FingerPosition.LeftPrimary
                pbGif.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes("Images\left hand - primary blinking.gif")))
            Case FingerPosition.RightPrimary
                pbGif.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes("Images\right hand - primary blinking.gif")))
            Case FingerPosition.LeftThumb
                pbGif.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes("Images\left hand - thumb blinking.gif")))
            Case FingerPosition.RightThumb
                pbGif.Image = Image.FromStream(New MemoryStream(System.IO.File.ReadAllBytes("Images\right hand - thumb blinking.gif")))
        End Select
    End Sub

    Private Sub PlaceYourFingerMessage()
        Dim finger As String = ""
        Select Case fp
            Case FingerPosition.LeftPrimary
                finger = "LEFT INDEX"
            Case FingerPosition.RightPrimary
                finger = "RIGHT INDEX"
            Case FingerPosition.LeftThumb
                finger = "LEFT THUMB"
            Case FingerPosition.RightThumb
                finger = "RIGHT THUMB"
        End Select

        SetLabelMessage(String.Format("PLACE {0} FINGER ON SCANNER", finger))
    End Sub

    Private Sub btnDecrease_Click(sender As Object, e As EventArgs) Handles btnDecrease.Click
        If txtQT.Text > 60 Then
            txtQT.Text = CInt(txtQT.Text) - 5
        End If
    End Sub

End Class
