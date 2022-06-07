
Imports System.Drawing
Imports System.Drawing.Imaging
Imports System.Drawing.Bitmap
Imports System.Drawing.Graphics
Imports System.IO

Public Class _frmPRNApplication_ConfirmFinal

    Dim printF As New printModule
    Dim o, l, r, q As Integer
    Dim cnt, cnt1, cnt3 As Integer
    Dim xtd As New ExtractedDetails
    Dim lview3 As New ListView
    Dim rtn As Integer
    Public ac As String
    ReadOnly ValidChars As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,.- "
    ReadOnly ValidCharsCity As String = _
"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789,- "
    ReadOnly ValidCharsNum As String = _
"0123456789 "
    Dim tempSSSHeader As String
    Dim salaryLoanTag As String
    Dim xs As New MySettings

    Public Sub New()
        ' This call is required by the designer.
        InitializeComponent()
        ' Add any initialization after the InitializeComponent() call.
    End Sub

    Private Sub _frmPRNApplication_ConfirmFinal_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            BarcodeCode39()
            PopulateSessionValues()

            If System.IO.File.Exists(PRN_BarcodeImageFile) Then
                pbBarcode.Image = Image.FromStream(New System.IO.MemoryStream(System.IO.File.ReadAllBytes(PRN_BarcodeImageFile)))
            Else
                pbBarcode.Image = Nothing
            End If

            _frmMainMenu.Button5.Enabled = True
            _frmMainMenu.Button6.Enabled = True
            _frmMainMenu.PrintControls(False)
            '_frmMainMenu.Button5.Text = "BACK"
            '_frmMainMenu.Button6.Text = "NEXT"
            '_frmMainMenu.Button5.Image = Image.FromFile(Application.StartupPath & "\images\BACK_DISABLED.png")
            '_frmMainMenu.Button6.Image = Image.FromFile(Application.StartupPath & "\images\NEXT_DISABLED.png")
        Catch ex As Exception
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try

    End Sub

    Public Sub PopulateSessionValues()
        lblPRN.Text = PRN
        lblMembershipType.Text = PRN_MembershipType
        lblApplicationPeriod.Text = String.Format("{0} {1} to {2} {3}", Get_MonthDesc(PRN_Period_From.Substring(0, 2)), PRN_Period_From.Substring(2, 4), Get_MonthDesc(PRN_Period_To.Substring(0, 2)), PRN_Period_To.Substring(2, 4))
        PRN_MonthlyContribution = GetMonthlyContribution()
        lblMonthlyContri.Text = CDec(PRN_MonthlyContribution).ToString("N2")
        lblTotalAmount.Text = CDec(PRN_TotalAmount).ToString("N2")
        lblDueDate.Text = PRN_DueDate 'CDate(PRN_DueDate).ToString("MM/dd/yyyy")

        GenerateBarcode(PRN)
    End Sub

    Private Sub _frmPRNApplication_KeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyData = Keys.Alt + Keys.F4 Then
            e.Handled = True
        End If
    End Sub

    Private Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            If _frmMainMenu.IsAllowedToPrint Then
                Dim class1 As New PrintHelper
                class1.BarcodeImage = PRN_BarcodeImageFile
                class1.prt(class1.prtPRN_Receipt(UsrfrmPageHeader1.lblMemberName.Text, SSStempFile, PRN, PRN_MembershipType, lblMonthlyContri.Text, "0.00", lblTotalAmount.Text, lblApplicationPeriod.Text, lblDueDate.Text, "STATEMENT OF ACCOUNT (PRN)", "Print"), DefaultPrinterName)
                class1 = Nothing
                _frmMainMenu.print_cnt(xtd.getCRN, Date.Today.ToShortDateString)
            End If
        Catch ex As Exception
        End Try
    End Sub

    Private Function DefaultPrinterName() As String
        Dim oPS As New System.Drawing.Printing.PrinterSettings

        Try
            DefaultPrinterName = oPS.PrinterName
        Catch ex As System.Exception
            DefaultPrinterName = ""
        Finally
            oPS = Nothing
        End Try
    End Function

    Private Sub RedirectTo_withPRN(ByVal frm As Form)
        Try
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            frm.TopLevel = False
            frm.Parent = _frmMainMenu.splitContainerControl.Panel2
            frm.Dock = DockStyle.Fill
            frm.Show()
        Catch ex As Exception
            GC.Collect()
            _frmMainMenu.splitContainerControl.Panel2.Controls.Clear()
            _frmErrorForm.TopLevel = False
            _frmErrorForm.Parent = _frmMainMenu.splitContainerControl.Panel2
            _frmErrorForm.Dock = DockStyle.Fill
            _frmErrorForm.Show()
        End Try
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        RedirectTo_withPRN(_frmPRNApplication)
    End Sub

    Private Function Get_MonthDesc(ByVal monthNo As String) As String
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
    End Function

    Private _encoding As Hashtable = New Hashtable
    Private Const _wideBarWidth As Short = 8
    Private Const _narrowBarWidth As Short = 2
    Private Const _barHeight As Short = 100

    Sub BarcodeCode39()
        _encoding.Add("*", "bWbwBwBwb")
        _encoding.Add("-", "bWbwbwBwB")
        _encoding.Add("$", "bWbWbWbwb")
        _encoding.Add("%", "bwbWbWbWb")
        _encoding.Add(" ", "bWBwbwBwb")
        _encoding.Add(".", "BWbwbwBwb")
        _encoding.Add("/", "bWbWbwbWb")
        _encoding.Add("+", "bWbwbWbWb")
        _encoding.Add("0", "bwbWBwBwb")
        _encoding.Add("1", "BwbWbwbwB")
        _encoding.Add("2", "bwBWbwbwB")
        _encoding.Add("3", "BwBWbwbwb")
        _encoding.Add("4", "bwbWBwbwB")
        _encoding.Add("5", "BwbWBwbwb")
        _encoding.Add("6", "bwBWBwbwb")
        _encoding.Add("7", "bwbWbwBwB")
        _encoding.Add("8", "BwbWbwBwb")
        _encoding.Add("9", "bwBWbwBwb")
        _encoding.Add("A", "BwbwbWbwB")
        _encoding.Add("B", "bwBwbWbwB")
        _encoding.Add("C", "BwBwbWbwb")
        _encoding.Add("D", "bwbwBWbwB")
        _encoding.Add("E", "BwbwBWbwb")
        _encoding.Add("F", "bwBwBWbwb")
        _encoding.Add("G", "bwbwbWBwB")
        _encoding.Add("H", "BwbwbWBwb")
        _encoding.Add("I", "bwBwbWBwb")
        _encoding.Add("J", "bwbwBWBwb")
        _encoding.Add("K", "BwbwbwbWB")
        _encoding.Add("L", "bwBwbwbWB")
        _encoding.Add("M", "BwBwbwbWb")
        _encoding.Add("N", "bwbwBwbWB")
        _encoding.Add("O", "BwbwBwbWb")
        _encoding.Add("P", "bwBwBwbWb")
        _encoding.Add("Q", "bwbwbwBWB")
        _encoding.Add("R", "BwbwbwBWb")
        _encoding.Add("S", "bwBwbwBWb")
        _encoding.Add("T", "bwbwBwBWb")
        _encoding.Add("U", "BWbwbwbwB")
        _encoding.Add("V", "bWBwbwbwB")
        _encoding.Add("W", "BWBwbwbwb")
        _encoding.Add("X", "bWbwBwbwB")
        _encoding.Add("Y", "BWbwBwbwb")
        _encoding.Add("Z", "bWBwBwbwb")
    End Sub

    Private PRN_BarcodeImageFile As String = Application.StartupPath & "\PRN_Barcode.jpg"

    Private Sub GenerateBarcode(ByVal barcode As String)
        Dim bmp As New Bitmap(GenerateBarcodeImage(630, 140, barcode))
        bmp.Save(PRN_BarcodeImageFile)
        bmp.Dispose()
    End Sub

    Protected Function getBCSymbolColor(ByVal symbol As String) As System.Drawing.Brush
        getBCSymbolColor = Brushes.Black
        If symbol = "W" Or symbol = "w" Then
            getBCSymbolColor = Brushes.White
        End If
    End Function

    Protected Function getBCSymbolWidth(ByVal symbol As String) As Short
        getBCSymbolWidth = _narrowBarWidth
        If symbol = "B" Or symbol = "W" Then
            getBCSymbolWidth = _wideBarWidth
        End If
    End Function

    Protected Overridable Function GenerateBarcodeImage(ByVal imageWidth As Short, ByVal imageHeight As Short, ByVal Code As String) As MemoryStream
        'create a new bitmap
        Dim b As New Bitmap(imageWidth, imageHeight, Imaging.PixelFormat.Format32bppArgb)

        'create a canvas to paint on
        Dim canvas As New Rectangle(0, 0, imageWidth, imageHeight)

        'draw a white background
        Dim g As Graphics = Graphics.FromImage(b)
        g.FillRectangle(Brushes.White, 0, 0, imageWidth, imageHeight)

        'write the unaltered code at the bottom
        'TODO: truely center this text
        Dim textBrush As New SolidBrush(Color.Black)
        g.DrawString(Code, New Font("Courier New", 12), textBrush, 100, 110)

        'Code has to be surrounded by asterisks to make it a valid Code39 barcode
        Dim UseCode As String = String.Format("{0}{1}{0}", "*", Code)

        'Start drawing at 10, 10
        Dim XPosition As Short = 10
        Dim YPosition As Short = 10

        Dim invalidCharacter As Boolean = False
        Dim CurrentSymbol As String = String.Empty

        For j As Short = 0 To CShort(UseCode.Length - 1)
            CurrentSymbol = UseCode.Substring(j, 1)
            'check if symbol can be used
            If Not IsNothing(_encoding(CurrentSymbol)) Then
                Dim EncodedSymbol As String = _encoding(CurrentSymbol).ToString

                For i As Short = 0 To CShort(EncodedSymbol.Length - 1)
                    Dim CurrentCode As String = EncodedSymbol.Substring(i, 1)
                    g.FillRectangle(getBCSymbolColor(CurrentCode), XPosition, YPosition, getBCSymbolWidth(CurrentCode), _barHeight)
                    XPosition = XPosition + getBCSymbolWidth(CurrentCode)
                Next

                'After each written full symbol we need a whitespace (narrow width)
                g.FillRectangle(getBCSymbolColor("w"), XPosition, YPosition, getBCSymbolWidth("w"), _barHeight)
                XPosition = XPosition + getBCSymbolWidth("w")
            Else
                invalidCharacter = True
            End If
        Next

        'errorhandling when an invalidcharacter is found
        If invalidCharacter Then
            g.FillRectangle(Brushes.White, 0, 0, imageWidth, imageHeight)
            g.DrawString("Invalid characters found,", New Font("Courier New", 8), textBrush, 0, 0)
            g.DrawString("no barcode generated", New Font("Courier New", 8), textBrush, 0, 10)
            g.DrawString("Input was: ", New Font("Courier New", 8), textBrush, 0, 30)
            g.DrawString(Code, New Font("Courier New", 8), textBrush, 0, 40)
        End If

        'write the image into a memorystream
        Dim ms As New MemoryStream

        Dim encodingParams As New EncoderParameters
        encodingParams.Param(0) = New EncoderParameter(Encoder.Quality, 100)

        Dim encodingInfo As ImageCodecInfo = FindCodecInfo("PNG")

        b.Save(ms, encodingInfo, encodingParams)

        'dispose of the object we won't need any more
        g.Dispose()
        b.Dispose()

        Return ms
    End Function

    Protected Overridable Function FindCodecInfo(ByVal codec As String) As ImageCodecInfo
        Dim encoders As ImageCodecInfo() = ImageCodecInfo.GetImageEncoders
        For Each e As ImageCodecInfo In encoders
            If e.FormatDescription.Equals(codec) Then Return e
        Next
        Return Nothing
    End Function

    Private Function GetMonthlyContribution() As Decimal
        Try
            Dim dtFrom As Date = String.Format("{0}/01/{1}", PRN_Period_From.Substring(0, 2), PRN_Period_From.Substring(2, 4))
            Dim dtTo As Date = String.Format("{0}/01/{1}", PRN_Period_To.Substring(0, 2), PRN_Period_To.Substring(2, 4))
            Dim intMonthDifference As Integer = DateDiff(DateInterval.Month, dtFrom, dtTo) + 1

            Dim ErrMsg As String = ""
            Dim cboContribution As New ComboBox
            If Not SharedFunction.Get_getContributionListPRN(cboContribution, ErrMsg) Then
                ErrorHandler(ErrMsg)
                Return 0
            Else
                For i As Short = 1 To cboContribution.Items.Count - 1
                    If CDec(CDec(cboContribution.Items(i).ToString) * intMonthDifference) = CDec(PRN_TotalAmount) Then
                        Return CDec(cboContribution.Items(i).ToString)
                    End If
                Next

                'if for loop failed, total amount is with flexi fund
                Return ContributionListPRN_MaxValue '1760
            End If
        Catch ex As Exception
            ErrorHandler("GetMonthlyContribution(): Runtime error catched " & ex.Message)
            Return 0
        End Try
    End Function

    Private Sub ErrorHandler(ByVal response As String)
        GC.Collect()
        authentication = "PRN01"
        tagPage = "8"
        Dim transNum As String = ""
        Dim transDesc As String = response
        Using SW As New IO.StreamWriter(_frmMainMenu.Path1 & "\" & "transaction_logs.txt", True)
            SW.WriteLine(xtd.getCRN & "|" & "10046" & "|" & tagPage & "|" & DateTime.Today.ToShortDateString & "|" & TimeOfDay & "|" & kioskIP & "|" & kioskBranch & "|" & kioskCluster & "|" & kioskGroup & "|" & userType & "|" & printTag & "|" & transNum & "|" & transDesc & vbNewLine)
        End Using
    End Sub

End Class