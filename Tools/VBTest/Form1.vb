Imports Tools

Public Class Form1
    Dim serialPortBase As SerialPortBase

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        serialPortBase.Open(ComboBox1.SelectedItem.ToString(), 9600, IO.Ports.Parity.None, 8, IO.Ports.StopBits.One)
        If serialPortBase.IsOpen Then
                MsgBox("串口打开")
            Else
                MsgBox("串口未打开")
        End If


    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        serialPortBase = New SerialPortBase
        ComboBox1.DataSource = serialPortBase.GetPortNames()

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If serialPortBase.IsOpen Then
            Dim bytes = serialPortBase.SendAndReceived(TextBox1.Text.ToHex(), TimeSpan.FromMilliseconds(1000))
            TextBox2.Text = Tools.StringHelper.ByteToHexString(bytes, " ")
        Else
            MsgBox("串口未打开")
        End If
    End Sub

    Private Sub SerialPort1_DataReceived(sender As Object, e As IO.Ports.SerialDataReceivedEventArgs)

    End Sub
End Class
