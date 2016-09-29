Public Class Form1
    Public i As Integer = 0
    Dim authenticated As Boolean = False
    Public timerenabled As Boolean = False
    Public seconds As Integer = 0

    ' This declares what Type the variable joystick1 will be for. The Type is Joystick.
    ' WithEvents allows you to easily add events using the IDE. 
    Private WithEvents joystick1 As Joystick

    ' This is an event that belongs to the Form. It is raised when the form loads.
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' Here we create the Joystick object. You must pass Me - which refers to this Form, 
        ' and 0 - which is the Joystick id.
        joystick1 = New Joystick(Me, 0)

        'start serial

        Try
            With SerialPort1
                '.PortName = "COM4"
                .BaudRate = 57600
                .Parity = IO.Ports.Parity.None
                .DataBits = 8
                .StopBits = IO.Ports.StopBits.One
                .ReadTimeout = 10000
            End With
            'SerialPort1.Open()

            'i = 1

            'chckSerial.Start()

        Catch
            MsgBox("Failed to connect with serial.")
        End Try
    End Sub

    ' And now we have the four events that belong to joystick1.

    Private Sub joystick1_Down() Handles joystick1.Down
        ' TODO: Replace this so that it plays a sound instead.
        Label1.Text = "Down"
    End Sub

    Private Sub joystick1_Left() Handles joystick1.Left
        ' TODO: Replace this so that it plays a sound instead.
        Label1.Text = "Left"
    End Sub

    Private Sub joystick1_Right() Handles joystick1.Right
        ' TODO: Replace this so that it plays a sound instead.
        Label1.Text = "Right"
    End Sub

    Private Sub joystick1_Up() Handles joystick1.Up
        ' TODO: Replace this so that it plays a sound instead.
        Label1.Text = "Up"
    End Sub
    ' Private Sub joystick1_buttonPressed() Handles joystick1.buttonPressed
    ' TODO: Replace this so that it plays a sound instead.
    '    Me.Text = joystick1.b1
    'End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Label2.Text = joystick1.xpos
        Label3.Text = joystick1.ypos

        Label5.Text = Format(100 - ((65535 - Label2.Text) / 655.35), "0.00") 'Gets percentage and round off to two decimals
        Label6.Text = Format(100 - ((65535 - Label3.Text) / 655.35), "0.00") 'Gets percentage and round off to two decimals

        If Label5.Text > 50 Then 'Joystick to the right, which is +5v
            Label7.Text = Format((((Label5.Text - 0) * 5) / 100), "0.00")
        Else                    ' Joystick to the left, which is -5v
            Label7.Text = Format((((Label5.Text - 0) * 5) / 100), "0.00")
        End If

        If Label6.Text > 50 Then 'Joystick to the bottom, which is +5v
            Label8.Text = Format((((Label6.Text - 0) * 5) / 100), "0.00")
        Else                    ' Joystick to the top, which is -5v
            Label8.Text = Format((((Label6.Text - 0) * 5) / 100), "0.00")
        End If

        Label11.Text = joystick1.btnValue
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        visual.Show()
    End Sub

    Sub SendSerialData(ByVal data As String)
        Try
            ' Send strings to a serial port.
            SerialPort1.WriteLine(data)
        Catch
            MsgBox("Failed. Cannot send to serial port.")
        End Try

    End Sub

    Private Sub Label7_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label7.TextChanged

    End Sub

    Private Sub Label8_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label8.TextChanged

    End Sub

    Private Sub Timer2_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chckSerial.Tick
        Dim inputTXT As String = SerialPort1.ReadExisting.Replace(" ", vbCr)
        If inputTXT = Nothing Then
        Else
            ListBox1.Items.Add(inputTXT)
            ListBox1.SelectedIndex = ListBox1.Items.Count - 1
        End If
    End Sub

    Private Sub TextBox1_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles TextBox1.KeyDown
        If e.KeyData = Keys.Enter And authenticated = True Then
            e.SuppressKeyPress = True
            If IsNumeric(TextBox1.Text) Then
                If timerenabled = True Then
                    seconds = TextBox1.Text
                    ListBox2.Items.Add("Timer is set to " & TextBox1.Text & " seconds.")
                    stat_timer.Text = "Timer: " & TextBox1.Text & " seconds (enabled)"
                Else
                    ListBox2.Items.Add("Unrecognized command. Make sure timer is enabled.")
                End If
            End If

            If TextBox1.Text.StartsWith("COM") Then
                Try
                    SerialPort1.PortName = TextBox1.Text
                    SerialPort1.Open()
                    chckSerial.Start()
                    ListBox2.Items.Add(TextBox1.Text & " is connected.")
                    stat_serial.Text = "Serial: Connected"
                    stat_com.Text = "COM Port: " & TextBox1.Text
                Catch
                    chckSerial.Stop()
                    i = 0
                    stat_serial.Text = "Serial: Disconnected"
                    ListBox2.Items.Add("Failed to connect to port " & TextBox1.Text)
                    stat_com.Text = "COM Port: Not set"
                End Try
            End If

            If TextBox1.Text = "Disconnect" Then
                chckSerial.Stop()
                i = 0
                SerialPort1.Close()
                ListBox2.Items.Add("Disconnected from port.")
                stat_serial.Text = "Serial: Disconnected"
            End If

            If TextBox1.Text = "cls" Then
                ListBox1.Items.Clear()
                ListBox2.Items.Clear()
            End If

            If TextBox1.Text = "Set Timer" Then
                timerenabled = True
                ListBox2.Items.Add("Specify time in seconds >>>")
            End If

            If TextBox1.Text = "e" Then
                timerenabled = False
                stat_timer.Text = "Timer: 0 seconds (disabled)"
                i = 1
                ListBox2.Items.Add("Controls are manually enabled.")
            End If

            If TextBox1.Text = "d" Then
                timerenabled = False
                stat_timer.Text = "Timer: 0 seconds (disabled)"
                i = 0
                ListBox2.Items.Add("Controls are manually disabled.")
            End If

            If TextBox1.Text = "s" Then
                If seconds > 0 Then
                    i = 1
                    user_timer.Start()
                    ListBox2.Items.Add("Timer has started. Controls are enabled.")
                Else
                    timerenabled = True
                    ListBox2.Items.Add("Specify time in seconds >>>")
                End If
            End If

            If TextBox1.Text = "p" Then
                If seconds > 0 And timerenabled Then
                    user_timer.Stop()
                    ListBox2.Items.Add("Timer has been paused. Controls are still active.")
                End If
            End If

            If TextBox1.Text = "r" Then
                user_timer.Stop()
                i = 0
                ListBox2.Items.Add("Timer is reset.")
                timerenabled = True
                ListBox2.Items.Add("Specify time in seconds >>>")
                stat_timer.Text = "Timer: 0 seconds (enabled)"
            End If

            If TextBox1.Text = "," Then
                i = 0
                ListBox2.Items.Add("Seat is normalized.")
                SendSerialData("r2.5")
            End If

            If TextBox1.Text = "." Then
                i = 0
                ListBox2.Items.Add("Seat is lowered.")
                SendSerialData("r5")
            End If

            If TextBox1.Text = "l" Then
                i = 0
                SendSerialData("t")
                authenticated = False
                user_timer.Stop()
                ListBox2.Items.Add("Control Station is locked.")
                ListBox2.Items.Add("Please enter administration passcode >>>")
            End If

            ListBox2.SelectedIndex = ListBox2.Items.Count - 1
            TextBox1.Clear() 'last line
            'All commands available
        ElseIf e.KeyData = Keys.Enter Then
            'Prompt for password
            e.SuppressKeyPress = True

            If TextBox1.Text = "bazinga" Then
                authenticated = True
                ListBox2.Items.Add("Access Granted.")
                ListBox2.Items.Add("Select COM Port to start >>")
                For Each sp As String In My.Computer.Ports.SerialPortNames 'lists the available serial ports
                    ListBox1.Items.Add(sp)
                Next
            Else
                ListBox2.Items.Add("Access Denied. Invalid administration passcode.")
            End If
            ListBox2.SelectedIndex = ListBox2.Items.Count - 1
            TextBox1.Clear() 'last line
        End If

    End Sub

    Private Sub user_timer_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles user_timer.Tick
        If seconds < 1 Then
            user_timer.Stop()
            i = 0
            stat_timer.Text = "Timer: 0 seconds (enabled)"
            seconds = 0
            ListBox2.Items.Add("Timer has ended. Controls are disabled.")
            ListBox2.SelectedIndex = ListBox2.Items.Count - 1
        Else
            seconds = seconds - 1
            stat_timer.Text = "Timer: " & seconds & " seconds (enabled)"
        End If
    End Sub

    Private Sub send_serial_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles send_serialP.Tick
        If i = 1 Then
            'SendSerialData("p" & Label8.Text)
            SerialPort1.Write("p" & Label8.Text)
            send_serialP.Stop()
            send_serialR.Start()
        End If
    End Sub

    Private Sub send_serialR_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles send_serialR.Tick
        If i = 1 Then
            'SendSerialData("r" & Label7.Text)
            SerialPort1.Write("r" & Label7.Text)
            send_serialR.Stop()
            send_serialP.Start()
        End If
    End Sub

    Private Sub Label11_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Label11.TextChanged
        If Label11.Text = "1024" And i = 1 Then 'disable controls
            timerenabled = False
            stat_timer.Text = "Timer: 0 seconds (disabled)"
            i = 0
            ListBox2.Items.Add("Controls are manually disabled.")
        ElseIf Label11.Text = "2048" And i = 0 Then 'enable controls
            timerenabled = False
            stat_timer.Text = "Timer: 0 seconds (disabled)"
            i = 1
            ListBox2.Items.Add("Controls are manually enabled.")
        ElseIf Label11.Text = "512" Then
            i = 0
            ListBox2.Items.Add("Seat is lowered.")
            SendSerialData("r5")
        ElseIf Label11.Text = "256" Then
            i = 0
            ListBox2.Items.Add("Seat is normalized.")
            SendSerialData("r2.5")
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ticketing.Show()
    End Sub
End Class