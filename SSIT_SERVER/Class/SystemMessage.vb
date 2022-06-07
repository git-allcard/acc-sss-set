
Public Class SystemMessage

    Enum MsgType
        SystemMsg = 1
        ErrorMsg = 2
        QuestionMsg = 3
        Warning = 4
    End Enum

    Public Sub New(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As MsgType)
        Me._msg = _msg
        Me._form = _form
        Me._msgType = _msgType
    End Sub

    Public Sub New(ByVal _form As UserControl, ByVal _msg As String, ByVal _msgType As MsgType, ByVal _msgButtons As MessageBoxButtons)
        Me._msg = _msg
        Me._form = _form
        Me._msgType = _msgType
        Me._msgButtons = _msgButtons
    End Sub

    Private Delegate Sub Action()
    Private _msg As String
    Private _form As UserControl
    Private _msgType As MsgType
    Private _msgButtons As MessageBoxButtons

    Public Sub ShowMsg()
        _form.Invoke(New Action(AddressOf Display))
    End Sub

    Private Sub Display()
        Select Case _msgType
            Case MsgType.SystemMsg
                SharedFunction.ShowInfoMessage(_msg)
            Case MsgType.ErrorMsg
                SharedFunction.ShowErrorMessage(_msg)
            Case MsgType.QuestionMsg
                'SharedFunction.ShowErrorMessage(_msg)
                btnResult = SharedFunction.ShowMessage(_msg, _msgButtons, MessageBoxIcon.Question)
            Case MsgType.Warning
                'SharedFunction.ShowErrorMessage(_msg)
                btnResult = SharedFunction.ShowMessage(_msg, _msgButtons, MessageBoxIcon.Warning)
        End Select
    End Sub

    Public btnResult As DialogResult

    'Private Function DisplayQuestion() As DialogResult
    '    Select Case _msgType
    '        Case MsgType.SystemMsg
    '            SharedFunction.ShowInfoMessage(_msg)
    '        Case MsgType.ErrorMsg
    '            SharedFunction.ShowErrorMessage(_msg)
    '        Case MsgType.QuestionMsg
    '            btnResult = SharedFunction.ShowMessage(_msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question)
    '    End Select
    'End Function

End Class
