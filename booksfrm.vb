Public Class booksfrm
    Dim cmd As New OleDb.OleDbCommand
    Dim sql As String
    Dim da As New OleDb.OleDbDataAdapter
    Dim dt As New DataTable
    Dim result As Integer
    Dim conn As OleDb.OleDbConnection = Myconnection()


    Public Function Myconnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\books.accdb")

    End Function
    Private Sub TextBox1_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TextBox1.TextChanged
        Try

            conn.Open()
            dt = New DataTable

            With cmd
                .Connection = conn
                .CommandText = "Select * from book_profile where TITLE like '" & TextBox1.Text & "%'"
            End With

            da.SelectCommand = cmd
            da.Fill(dt)

            DataGridView1.DataSource = dt

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        da.Dispose()

        conn.Close()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        mainfrm.Show()
        Me.Hide()
    End Sub

    Private Sub DataGridView1_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellClick
        Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        txtdewey.Text = DataGridView1.CurrentRow.Cells("Dewey_no").Value.ToString
        txttitle.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        txtsubject.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        txtauthor.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        txtcallno.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        txtcopyright.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        txtpagination.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        txtauthorsno.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString
        txtcopies.Text = DataGridView1.CurrentRow.Cells("no_of_copies").Value.ToString
        txtISBN.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString
        txtamount.Text = DataGridView1.CurrentRow.Cells("AMOUNT").Value.ToString
        txtcategory.Text = DataGridView1.CurrentRow.Cells("CATEGORY").Value.ToString
    End Sub

    Private Sub DataGridView1_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        'Me.Text = DataGridView1.CurrentRow.Cells(0).Value.ToString
        'txtdewey.Text = DataGridView1.CurrentRow.Cells("Dewey_no").Value.ToString
        'txttitle.Text = DataGridView1.CurrentRow.Cells(1).Value.ToString
        'txtsubject.Text = DataGridView1.CurrentRow.Cells(2).Value.ToString
        'txtauthor.Text = DataGridView1.CurrentRow.Cells(3).Value.ToString
        'txtcallno.Text = DataGridView1.CurrentRow.Cells(4).Value.ToString
        'txtcopyright.Text = DataGridView1.CurrentRow.Cells(5).Value.ToString
        'txtpagination.Text = DataGridView1.CurrentRow.Cells(6).Value.ToString
        'txtauthorsno.Text = DataGridView1.CurrentRow.Cells(7).Value.ToString
        'txtcopies.Text = DataGridView1.CurrentRow.Cells(8).Value.ToString
        'txtISBN.Text = DataGridView1.CurrentRow.Cells(9).Value.ToString
        'txtamount.Text = DataGridView1.CurrentRow.Cells("AMOUNT").Value.ToString
    End Sub



   
    Public Sub cleartextfields()
        For Each crt As Control In GroupBox1.Controls
            If crt.GetType Is GetType(TextBox) Then
                crt.Text = Nothing
            End If
        Next
    End Sub
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click

        Try
            sql = "INSERT INTO book_profile(Dewey_no,title,subject,author,call_no,copyright,pagination,authors_no,no_of_copies,ISBN,AMOUNT,CATEGORY)VALUES('" & txtdewey.Text & "','" & txttitle.Text & "','" & txtsubject.Text & "','" & txtauthor.Text & "','" & txtcallno.Text & "','" & txtcopyright.Text & "','" & txtpagination.Text & "','" & txtauthorsno.Text & "','" & txtcopies.Text & "','" & txtISBN.Text & "','" & txtamount.Text & "','" & txtcategory.Text & "')"
            conn.Open()
            With cmd
                .CommandText = sql
                .Connection = conn
            End With

            result = cmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("NEW RECORD HAS BEEN SAVED!")
                conn.Close()
                cleartextfields()


            Else
                MsgBox("NO RECORD HASS BEEN SAVED!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
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



    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            sql = "UPDATE book_profile SET Dewey_no='" & txtdewey.Text & "',title='" & txttitle.Text & "',subject='" & txtsubject.Text & "',call_no='" & txtcallno.Text & "',copyright='" & txtcopyright.Text & "',pagination='" & txtpagination.Text & "',authors_no='" & txtauthorsno.Text & "',no_of_copies='" & txtcopies.Text & "',ISBN='" & txtISBN.Text & "',AMOUNT='" & txtamount.Text & "',CATEGORY='" & txtcategory.Text & "' WHERE Dewey_no=" & Val(Me.Text)

            conn.Open()
            With cmd
                .CommandText = sql
                .Connection = conn
            End With

            result = cmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("NEW record has been UPDATED!")
                conn.Close()
                cleartextfields()

            Else
                MsgBox("NO record has been UPDATED!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try
    End Sub

    Private Sub Button6_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button6.Click
        Try
            sql = "DELETE FROM book_profile  WHERE Dewey_no=" & Me.Text
            conn.Open()
            With cmd
                .CommandText = sql
                .Connection = conn
            End With

            result = cmd.ExecuteNonQuery
            If result > 0 Then
                MsgBox("NEW RECORD HAS BEEN DELeTED!")
                conn.Close()
                cleartextfields()
            Else
                MsgBox("NO RECORD HASS BEEN DELeTED!")
            End If

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            conn.Close()


        End Try


    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        cleartextfields()

    End Sub


    Private Sub Form3_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

   

End Class

