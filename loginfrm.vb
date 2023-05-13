Public Class loginfrm

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        con.Open()
        cmd = New OleDb.OleDbCommand("SELECT * FROM tblacount WHERE userusername='" & txtuser.Text & "' and userpassword='" & Txtpassword.Text & "'", con)
        dr = cmd.ExecuteReader
        While dr.Read
            username = dr("userusername")
            password = dr("userpassword")
            FullName = dr("userfullname")

        End While
        con.Close()

        If username = ("admin") And password = ("admin") Then

            Me.Hide()
            mainfrm.Button1.Enabled = False
            mainfrm.Button2.Enabled = True
            mainfrm.Button3.Enabled = True
            mainfrm.Button4.Enabled = True
            mainfrm.btnlogout.Enabled = True
            mainfrm.btnsettings.Enabled = True
            Me.Close()
        Else
            MsgBox("error to login!")
            txtuser.Clear()
            Txtpassword.Clear()
        End If


    End Sub

    Private Sub Txtpassword_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Txtpassword.TextChanged
        Txtpassword.UseSystemPasswordChar = True
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Hide()
        mainfrm.Show()
    End Sub





    Private Sub CheckBox1_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CheckBox1.CheckedChanged
        If CheckBox1.CheckState = CheckState.Checked Then

            Txtpassword.UseSystemPasswordChar = False
        Else

            Txtpassword.UseSystemPasswordChar = True
        End If
    End Sub

    Private Sub txtuser_TextChanged(sender As Object, e As EventArgs) Handles txtuser.TextChanged

    End Sub
End Class