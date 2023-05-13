Public Class log

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        mainfrm.Button1.Enabled = True
        mainfrm.Button2.Enabled = False
        mainfrm.Button3.Enabled = False
        mainfrm.Button4.Enabled = False
        mainfrm.btnlogout.Enabled = False
        mainfrm.btnsettings.Enabled = False
        booksfrm.Close()
        frmcashier.Close()
        booklist.Close()
        accounts.Close()
        history.Close()
        Me.Close()

    End Sub
End Class