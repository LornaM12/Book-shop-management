Public Class frmcashier
    Dim cmd As New OleDb.OleDbCommand
    Dim sql As String
    Dim da As New OleDb.OleDbDataAdapter
    Dim dt As New DataTable
    Dim result As Integer
    Dim conn As OleDb.OleDbConnection = Myconnection()
    Dim FinalTotalPayments As Integer



    Public Function Myconnection() As OleDb.OleDbConnection
        Return New OleDb.OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & Application.StartupPath & "\books.accdb")

    End Function

    Private Sub txtquantity_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles txtquantity.KeyDown
        Dim amount As Integer
        Dim Dewey_no As Integer
        Dim no_of_copies As Integer
        Dim FishTotalPayments As Integer = 0
        Dim title As String = ""
     

        If txtquantity.Text = Nothing Then
            ''''''''''''''''''''''''''''''''
        Else
            If e.KeyCode = Keys.Enter Then

                con.Open()
                Try

                    sql = "SELECT Dewey_no, title, no_of_copies, AMOUNT, (no_of_copies * AMOUNT) AS TotalPayments FROM book_profile WHERE title = '" & Combotitle.Text & "'"
                    cmd = New OleDb.OleDbCommand(sql, con)

                    dr = cmd.ExecuteReader
                    While dr.Read
                        Dewey_no = dr("Dewey_no")
                        title = dr("title")
                        no_of_copies = dr("no_of_copies")
                        amount = dr("AMOUNT")
                    End While

                Catch ex As Exception
                    MsgBox(ex.Message)
                End Try

                If no_of_copies < Val(txtquantity.Text) Then
                    MessageBox.Show("Insufficient stocks of Books!" & vbNewLine & "There are only " & no_of_copies & " pcs of " & title & " left!", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtquantity.Clear()
                Else
                   
                    FishTotalPayments = Val(txtquantity.Text) * amount
                    FinalTotalPayments += FishTotalPayments


                   
                    txttotal.Text = FinalTotalPayments

                    Try

                        dt = New DataTable
                        sql = "UPDATE book_profile SET no_of_copies = no_of_copies - " & Val(txtquantity.Text) & " WHERE Dewey_no = " & Dewey_no
                        '                   
                        cmd = New OleDb.OleDbCommand(sql, con)
                        cmd.ExecuteNonQuery()

                    Catch ex As Exception
                        MessageBox.Show(ex.Message)
                    End Try
                End If


                con.Close()

                txtquantity.Focus()
                Combotitle.Focus()

                mysqlsave("INSERT INTO tblcash (CUSTOMERS_NAME,DATE_PURCHASED,BOOKS_TITLE,AUTHORS_NAME,ISBN_NO,AMOUNT,QUANTITY,TOTAL,CASHIER_NAME) " & _
" VALUES('" & txtcustname.Text & "','" & dtpdate.Text & "','" & Combotitle.Text & "','" & txtauthor.Text & "','" & txtisbn.Text & "','" & txtamt.Text & "','" & txtquantity.Text & "','" & txttotal.Text & "','" & txtincharge.Text & "')")

            End If
                        con.Close()
            Try
                sql = "SELECT * FROM tblcash"
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
        End If
     
    End Sub

   
    Private Sub txtquantity_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtquantity.TextChanged
       
        Try
            sql = "SELECT * FROM tblcash"
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
        'Dim no_of_copies = Nothing

        'con.Open()
        'sql = "select * from book_profile where title = '" & Combotitle.Text & "'"
        'cmd = New OleDb.OleDbCommand(sql, con)
        'dr = cmd.ExecuteReader
        'While dr.Read
        '    If IsDBNull((dr("no_of_copies"))) = False Then
        '        no_of_copies = dr("no_of_copies")
        '    End If
        'End While
        'con.Close()

        'If Val(txtquantity.Text) > no_of_copies Then
        '    MessageBox.Show("Out of stocks!")
        '    'txtquantity.Text = 0
        'ElseIf Val(txtquantity.Text) < 0 Then
        '    txtquantity.Text = 0
        'Else
        '    txttotal.Text = Val(txtamt.Text) * Val(txtquantity.Text) + Val(txttotal.Text)
        'End If

       
    End Sub

    Private Sub txtantreceived_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtantreceived.TextChanged
        txtchange.Text = Val(txtantreceived.Text) - Val(txttotal.Text)

        If txtantreceived.Text = Nothing Then
            txtchange.Text = "0.00"
        End If
       
    End Sub

    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.Checked = True Then
            TextBox1.Text = Val(txttotal.Text) * 0.8
        Else
            CheckBox1.Checked = False
            txttotal.Text = Val(txtamt.Text) * Val(txtquantity.Text)
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        booklist.Show()

    End Sub

    Private Sub Button1_Click_1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        booklist.Show()
    End Sub


    



    Private Sub txtchange_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtchange.TextChanged
        If txtchange.Text < 0 Then
            txtchange.Text = 0
        End If
    End Sub

    Private Sub frmcashier_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        txtincharge.Text = FullName
        Try
            sql = "SELECT * FROM tblcash"
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

        con.Open()
        sql = "select distinct category from book_profile"
        cmd = New OleDb.OleDbCommand(sql, con)
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull(dr("category")) = False Then
                Combocategory.Items.Add(dr("category"))
            End If
        End While
        con.Close()



    End Sub

  

   

    Public Sub addtolist()
        Dim sql As String
        Dim cmd As New OleDb.OleDbCommand
        Dim dr As OleDb.OleDbDataReader
        Try
            sql = "SELECT * FROM book_profile where TITLE = '" & Combotitle.Text & "'"

            con.Open()
            With cmd
                .CommandText = sql
                .Connection = con
            End With
            dr = cmd.ExecuteReader
            While dr.Read()
                
            End While
        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

  

   


    Private Sub Combotitle_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Combotitle.TextChanged
        con.Open()
        sql = "select * from book_profile where title = '" & Combotitle.Text & "'"
        cmd = New OleDb.OleDbCommand(sql, con)
        dr = cmd.ExecuteReader
        While dr.Read
            If IsDBNull((dr("author"))) = False Or _
                  IsDBNull((dr("ISBN"))) = False Or _
                IsDBNull((dr("amount"))) = False Then


                txtauthor.Text = dr("author")
                txtisbn.Text = dr("ISBN")
                txtamt.Text = dr("amount")
            End If
        End While
        con.Close()

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combocategory.SelectedIndexChanged
        con.Open()
        sql = "select * from book_profile where category = '" & Combocategory.Text & "'"
        cmd = New OleDb.OleDbCommand(sql, con)
        dr = cmd.ExecuteReader
        While dr.Read
            Combotitle.Items.Add(dr("title"))
        End While
        con.Close()

    End Sub

   

    Private Sub Button1_Click_3(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        GroupBox2.Visible = True
    End Sub


    Private Sub txtamt_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtamt.TextChanged

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        txtcustname.Clear()
        Combotitle.Items.Clear()
        txtauthor.Clear()
        txtisbn.Clear()
        txtamt.Clear()
        txtquantity.Clear()
        txttotal.Clear()


    End Sub
    Private Sub UpdateDecreaseQuantity()
        Try
            sql = "Update Item SET no_of_copies = no_of_copies - " & Val(txtquantity.Text) & " WHERE title = '" & Combotitle.Text & "'"
            con.Open()
            cmd = New OleDb.OleDbCommand(sql, con)
            cmd.ExecuteNonQuery()

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            cmd.Dispose()
            con.Close()
        End Try

    End Sub

    Private Sub Combotitle_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Combotitle.SelectedIndexChanged
        If Combotitle.Text < Nothing Then
            txtauthor.Text = Nothing

        ElseIf txtisbn.Text = Nothing Then
        ElseIf txtamt.Text = Nothing Then

        End If
    End Sub

    Private Sub txttotal_TextChanged(sender As Object, e As EventArgs) Handles txttotal.TextChanged

    End Sub
End Class
