Public Class visual
    Dim ballX As Decimal
    Dim ballY As Decimal

    Dim Xpercentage As Decimal
    Dim Ypercentage As Decimal


    Private Sub visual_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub ball_move_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ball_move.Tick
        'ballX = Cursor.Position.X - Me.Location.X
        'ballY = Cursor.Position.Y - Me.Location.Y
        getJoystickPerc()

        ballX = Xpercentage * 5
        ballY = Ypercentage * 5

        ballX = ballX - 25
        ballY = ballY - 25
        Dim ballLoc As New Point(ballX, ballY)
        PictureBox1.Location = ballLoc
    End Sub

    Private Sub getJoystickPerc()

        Xpercentage = 100 - ((65535 - Form1.Label2.Text) / 655.35)
        Ypercentage = 100 - ((65535 - Form1.Label3.Text) / 655.35)
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class