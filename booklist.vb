Public Class booklist
    Dim cmd As New OleDb.OleDbCommand
    Dim sql As String
    Dim da As New OleDb.OleDbDataAdapter
    Dim dt As New DataTable
    Dim result As Integer
    Dim conn As OleDb.OleDbConnection = Myconnection()


    Public Function Myconnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\books.accdb")

    End Function
    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

    End Sub

    Private Sub booklist_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            sql = "SELECT * FROM book_profile"
            conn.Open()
            With cmd
                .CommandText = sql
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

    Private Sub DataGridView1_DoubleClick(ByVal sender As Object, ByVal e As System.EventArgs) Handles DataGridView1.DoubleClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
      

        frmcashier.Combotitle.Text = DataGridView1.CurrentRow.Cells("title").Value.ToString
        frmcashier.txtauthor.Text = DataGridView1.CurrentRow.Cells("author").Value.ToString
        frmcashier.txtisbn.Text = DataGridView1.CurrentRow.Cells("ISBN").Value.ToString
        frmcashier.txtamt.Text = DataGridView1.CurrentRow.Cells("AMOUNT").Value.ToString

        Me.Close()
       
    End Sub

    Private Sub GroupBox1_Enter(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles GroupBox1.Enter

    End Sub
End Class