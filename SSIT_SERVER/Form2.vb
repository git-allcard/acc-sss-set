Public Class Form2

    Public td As TestData2

    Public crn As String = ""
    Public sssNo As String = ""
    Public ccdt As String = ""

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'gridModule.gen = False
        gridModule.DataSource = td.ModuleList
    End Sub

    Private Sub gridModule_CellClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridModule.CellClick
        Try
            gridData.DataSource = td.ModuleData(gridModule.CurrentCell.Value)
            gridData.Columns(3).Visible = False
        Catch ex As Exception
        End Try
    End Sub

    Private Sub gridData_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles gridData.CellDoubleClick
        Try
            crn = gridData.Rows(e.RowIndex).Cells(0).Value
            sssNo = gridData.Rows(e.RowIndex).Cells(1).Value
            ccdt = gridData.Rows(e.RowIndex).Cells(2).Value
            Close()
        Catch ex As Exception

        End Try
    End Sub
End Class