

Public Class _frmExitSurveyMain

    Enum Screen
        Welcome = 1
        FindInfo
        EasyTouse
        OverallExp
        Lastpage
    End Enum

    Private findInfoResponse As Short = 0
    Private easyToUseRate As Short = 0
    Private overAllExpRate As Short = 0

    Private easyToUse As EasyToUse
    Private overallExp As OverallExp
    Private findInfo As FindInfo

    Private currentScreen As Screen = 1
    Private previousScreen As Screen = 1

    Private firstStep As Short = 1
    Private lastStep As Short = 5

    Public memberName As String = ""

    'Public Sub New()

    '    ' This call is required by the designer.
    '    InitializeComponent()
    '    NavigationControlVisibility()
    '    Reset()
    '    BindScreen()
    '    ' Add any initialization after the InitializeComponent() call.

    'End Sub

    Private Sub _frmExitSurveyMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        NavigationControlVisibility()
        BindScreen()
    End Sub

    Private Sub Reset()
        findInfoResponse = 0
        easyToUseRate = 0
        overAllExpRate = 0

        easyToUse = Nothing
        overallExp = Nothing
        findInfo = Nothing

        currentScreen = 1
        previousScreen = 1

        firstStep = 1
        lastStep = 5
    End Sub

    Private Function BindScreen() As Boolean
        Dim bln As Boolean = True

        Select Case previousScreen
            Case Screen.FindInfo
                If Not findInfo Is Nothing Then
                    bln = findInfo.IsValid
                    findInfoResponse = findInfo.SelectedValue
                End If
            Case Screen.EasyTouse
                If Not easyToUse Is Nothing Then
                    bln = easyToUse.IsValid
                    easyToUseRate = easyToUse.SelectedValue
                End If
            Case Screen.OverallExp
                If Not overallExp Is Nothing Then
                    bln = overallExp.IsValid
                    overAllExpRate = overallExp.SelectedValue
                End If
        End Select

        If bln Then
            If pnlBody.Controls.Count > 0 Then pnlBody.Controls.RemoveAt(0)
            Select Case currentScreen
                Case Screen.Welcome
                    Dim welcome As New Welcome
                    welcome.Dock = DockStyle.Fill
                    pnlBody.Controls.Add(welcome)
                Case Screen.FindInfo
                    findInfo = New FindInfo(findInfoResponse)
                    findInfo.Dock = DockStyle.Fill
                    pnlBody.Controls.Add(findInfo)
                Case Screen.EasyTouse
                    easyToUse = New EasyToUse(easyToUseRate)
                    easyToUse.Dock = DockStyle.Fill
                    pnlBody.Controls.Add(easyToUse)
                Case Screen.OverallExp
                    overallExp = New OverallExp(overAllExpRate)
                    overallExp.Dock = DockStyle.Fill
                    pnlBody.Controls.Add(overallExp)
                Case Screen.Lastpage
                    Dim ip As New insertProcedure
                    ip.insertSSEXITSURVEY(SSStempFile, IIf(findInfoResponse = 1, 1, 0), easyToUseRate, overAllExpRate, memberName)
                    ip = Nothing


                    Dim welcome As New Welcome(False)
                    welcome.Dock = DockStyle.Fill
                    pnlBody.Controls.Add(welcome)
            End Select
            If currentScreen = 1 Then lblStepCntr.Text = "" Else lblStepCntr.Text = String.Format("{0} / {1}", CShort(currentScreen) - 1, lastStep - 2)
        End If

        Return bln
    End Function

    Private Sub btnProceed_Click(sender As Object, e As EventArgs) Handles btnProceed.Click
        If currentScreen <> lastStep Then
            previousScreen = currentScreen
            currentScreen += 1
            If Not BindScreen() Then currentScreen -= 1
        End If

        NavigationControlVisibility()
    End Sub

    Private Sub btnBack_Click(sender As Object, e As EventArgs) Handles btnBack.Click
        If currentScreen <> firstStep Then
            previousScreen = currentScreen
            currentScreen -= 1
            If Not BindScreen() Then currentScreen += 1
        End If

        NavigationControlVisibility()
    End Sub

    Private Sub SplitContainer1_Panel2_Resize(sender As Object, e As EventArgs) Handles SplitContainer1.Panel2.Resize
        pnlBody.Left = (SplitContainer1.Panel2.ClientSize.Width - pnlBody.Width) / 2
        pnlBody.Top = (SplitContainer1.Panel2.ClientSize.Height - pnlBody.Height) / 2
    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs) Handles Label1.Click
        Reset()
        Close()
    End Sub

    Private Sub NavigationControlVisibility()
        Select Case currentScreen
            Case Screen.Lastpage
                btnProceed.Visible = False
                btnBack.Visible = False
                lblStepCntr.Visible = False
            Case Else
                If currentScreen = lastStep Then btnProceed.Visible = False Else btnProceed.Visible = True
                If currentScreen = firstStep Then btnBack.Visible = False Else btnBack.Visible = True
        End Select
    End Sub

End Class