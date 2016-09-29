Public Class ticketing

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Button1.Enabled = False
        Form1.i = 1
        Form1.timerenabled = True
        Form1.seconds = ComboBox2.Text
        Form1.user_timer.Start()
        Form1.ListBox2.Items.Add("Timer has started. Controls are enabled.")
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Timer1.Stop()
        WebBrowser1.Hide()
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyCode = Keys.Return Then
            WebBrowser1.Document.DomDocument.GetElementById("ticketid").Value = TextBox1.Text
            WebBrowser1.Document.DomDocument.GetElementById("rewrite").Value = ComboBox1.Text
            WebBrowser1.Show()
            WebBrowser1.Document.DomDocument.GetElementById("rewrite").Focus()
            SendKeys.Send("{ENTER}")
            Timer1.Start()
            Timer2.Start()
            TextBox1.Clear()
        End If
    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer2.Tick
        Timer2.Stop()
        If WebBrowser1.Document.Body.InnerHtml.Contains("Ticket Validity: Active") Then
            Button1.Enabled = True
        End If
    End Sub
End Class