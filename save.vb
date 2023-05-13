Module save
    Public result As Integer
    Public cmd As New OleDb.OleDbCommand
    Public con As OleDb.OleDbConnection = Myconnection()
    Public dr As OleDb.OleDbDataReader
    Public DtgCanEdit As Boolean = True
    Public Function Myconnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\books.accdb")
    End Function
    Public username As String = ""
    Public password As String = ""
    Public FullName As String = ""

    Public Sub mysqlsave(ByVal sql As String)
        Try
            If con.State = ConnectionState.Open Then con.Close()
            con.Open()

            With cmd
                .Connection = con
                .CommandText = sql
                result = cmd.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("No Customer has been Inserted!")

                    'Else
                    '    MsgBox("New Customer has been inserted succesfully!")
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
        con.Close()
        'clearfields()


    End Sub

    Public Sub mysqlupdate(ByVal sql As String)
        Try
            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
                result = cmd.ExecuteNonQuery
                If result = 0 Then
                    MsgBox("No Customer has been Updated!")



                Else
                    MsgBox("New Customer is succesfully Updated!")
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
        con.Close()

    End Sub

    Public Sub delete(ByVal sql As String)
        Try
            con.Open()
            With cmd
                .Connection = con
                .CommandText = sql
                result = cmd.ExecuteNonQuery

                If result = 0 Then
                    MsgBox("No Data has been Deleted!")

                Else
                    MsgBox("New record is  succesfully Deleted!")
                End If
            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
        con.Close()

    End Sub
    Public Sub AutoFill(ByVal Qry As String, ByVal TxtResult As TextBox)
        Dim QryRes As New AutoCompleteStringCollection
        TxtResult.AutoCompleteCustomSource.Clear()

        con.Open()
        With cmd
            .Connection = con
            .CommandText = Qry
        End With

        dr = cmd.ExecuteReader
        While dr.Read
            QryRes.Add(dr.GetValue(0))
        End While

        TxtResult.AutoCompleteMode = AutoCompleteMode.Suggest
        TxtResult.AutoCompleteSource = AutoCompleteSource.CustomSource
        TxtResult.AutoCompleteCustomSource = QryRes
        con.Close()
    End Sub
    'Public Sub clearfields(ByVal group As Object)
    '    For Each ctrl As Control In group.Controls
    '        If ctrl.GetType Is GetType(TextBox) Then
    '            ctrl.Text = Nothing

    '        End If
    '    Next
    'End Sub

End Module

