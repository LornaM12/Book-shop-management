Public Class accounts
    Dim cmd As New OleDb.OleDbCommand
    Dim sql As String
    Dim da As New OleDb.OleDbDataAdapter
    Dim dt As New DataTable
    Dim result As Integer
    Dim conn As OleDb.OleDbConnection = Myconnection()


    Public Function Myconnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\books.accdb")

    End Function
    
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtpass.Text <> txtconfirm.Text Then
            MsgBox("Password Confirmation did not match!", MsgBoxStyle.Information)
        Else
            mysqlsave("INSERT INTO tblacount ( userusername , userpassword ,userfullname ) " & _
                  " VALUES('" & txtusername.Text & "','" & txtpass.Text & "','" & TextBox1.Text & "')")

        End If

    End Sub

    Private Sub itemdatagrid_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick

        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        txtusername.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        txtpass.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        txtconfirm.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        TextBox1.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString

    End Sub

    Private Sub accounts_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            Sql = "SELECT * FROM tblacount"
            conn.Open()
            With cmd
                .CommandText = Sql
                .Connection = conn
                dt = New DataTable
            End With

            da.SelectCommand = cmd
            da.Fill(dt)
            DataGridView1.DataSource = dt


        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub btndel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btndel.Click
        delete("Delete * from tblacount where ID=" & Me.Text)

    End Sub

   
End Class