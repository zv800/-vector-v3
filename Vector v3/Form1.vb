Imports System.IO
Imports EasyExploits
Imports WeAreDevs_API
Imports System
Imports System.Windows.Forms
Imports System.Runtime.InteropServices
Imports System.Drawing
Imports System.Drawing.Drawing2D
Public Class Form1
    Private borderRadius As Integer = 30
    Private borderSize As Integer = 3
    Dim lastPoint As Point
    ' Dim hack As EasyExploits.Module = New EasyExploits.Module
    Dim hack As ExploitAPI = New ExploitAPI
    Dim p() As Process
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadbuttons()
        Dim fileReader As String
        fileReader = My.Computer.FileSystem.ReadAllText("settings.Vector",
               System.Text.Encoding.UTF32)

        FastColoredTextBox1.Text = fileReader

        Dim a As Integer
        For a = 10 To 40 Step +1
            Me.Opacity = a / 40
            Me.Refresh()
            Threading.Thread.Sleep(5)
        Next
        ListBox1.Items.Clear()
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        ListBox1.Items.Clear()
        Dim a As Integer
        For a = 30 To 10 Step -1
            Me.Opacity = a / 30
            Me.Refresh()
            Threading.Thread.Sleep(15)
        Next
        Me.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        ListBox1.Items.Clear()
        Dim a As Integer
        For a = 30 To 10 Step -1
            Me.Opacity = a / 30
            Me.Refresh()
            Threading.Thread.Sleep(15)
        Next
        Me.WindowState = FormWindowState.Minimized
        Me.Opacity = 100
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Panel1_MouseDown(sender As Object, e As MouseEventArgs) Handles Panel1.MouseDown
        lastPoint = New Point(e.X, e.Y)
    End Sub

    Private Sub Panel1_MouseMove(sender As Object, e As MouseEventArgs) Handles Panel1.MouseMove
        If (e.Button = MouseButtons.Left) Then
            Me.Left += e.X - lastPoint.X
            Me.Top += e.Y - lastPoint.Y
        End If

    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        OpenFileDialog1.Title = "Please select a lua file"
        OpenFileDialog1.InitialDirectory = "C:\"
        OpenFileDialog1.Filter = "lua Files|*.lua"
        time_ontop.Stop()
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim fileReader As String
            fileReader = My.Computer.FileSystem.ReadAllText(OpenFileDialog1.FileName,
               System.Text.Encoding.UTF32)
            FastColoredTextBox1.Text = fileReader
            time_ontop.Start()
        ElseIf OpenFileDialog1.ShowDialog() = DialogResult.Cancel Then
            time_ontop.Start()
        End If

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        SaveFileDialog1.Filter = "lua Files (*.lua*)|*.lua"
        time_ontop.Stop()
        If SaveFileDialog1.ShowDialog = Windows.Forms.DialogResult.OK _
         Then
            time_ontop.Start()
            My.Computer.FileSystem.WriteAllText _
            (SaveFileDialog1.FileName, FastColoredTextBox1.Text, True)

        ElseIf SaveFileDialog1.ShowDialog() = DialogResult.Cancel Then
            time_ontop.Start()
        End If
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        FastColoredTextBox1.Text = File.ReadAllText($"./Scripts/{ListBox1.SelectedItem}")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
        Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
    End Sub

    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs) Handles Panel1.Paint

    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        f_Attach()
    End Sub

    Private Sub FastColoredTextBox1_Load(sender As Object, e As EventArgs) Handles FastColoredTextBox1.Load
        FastColoredTextBox1.Language = FastColoredTextBox1.Language.Lua

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If hack.isAPIAttached = True Then
            hack.SendLuaScript(FastColoredTextBox1.Text)
        Else
            time_ontop.Stop()
            MsgBox("exploit is not attached", 0 + 16, "there was no attempt to execute")
            time_ontop.Start()
        End If

    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        Try
            Process.GetProcessesByName("RobloxPlayerBeta")(0).Kill()
        Catch ex As Exception

        End Try

    End Sub

    Private Sub InjTest_Tick(sender As Object, e As EventArgs) Handles InjTest.Tick
        If hack.isAPIAttached = True Then
            InjTest.Stop()
            Reset.Start()

            Dim fileReader As String
            Try
                Label3.Text = "injected"
                ListBox1.Items.Add("injected")

                ListBox1.Items.Clear()
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")

            Catch ex As Exception
                ListBox1.Items.Add("error")
                Label3.Text = "injected: 1 error"
                time_ontop.Stop()
                MsgBox("could not read 'Menu.Vector' try reinstalling the program")
                time_ontop.Start()
                Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
                ListBox1.Items.Clear()
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
                Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")

            End Try

        Else
        End If
    End Sub

    Private Sub Reset_Tick(sender As Object, e As EventArgs) Handles Reset.Tick
        If hack.isAPIAttached = False Then
            Reset.Stop()
            InjTest.Start()
            Label3.Text = "not injected"
            ListBox1.Items.Clear()
            ListBox1.Items.Add("error")
            ListBox1.Items.Add("Roblox closed")
            MsgBox("//", 0 + 16, "//")
            Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
            ListBox1.Items.Clear()
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")


        End If
    End Sub

    Private Sub FastColoredTextBox1_TextChanging(sender As Object, e As FastColoredTextBoxNS.TextChangingEventArgs) Handles FastColoredTextBox1.TextChanging
        If FastColoredTextBox1.Text = "./reset" Then
            time_ontop.Stop()
            MsgBox("unexpected error occurred", 0 + 16, "Vector v3")
            time_ontop.Start()
            Me.Close()
            MsgBox("experimental settings enabled")
        ElseIf FastColoredTextBox1.Text = "./vector>developer_settings = true" Then
            Button9.Enabled = True

        ElseIf FastColoredTextBox1.Text = "./vector>developer_settings = false" Then
            time_ontop.Stop()
            MsgBox("experimental settings disabled")
            time_ontop.Start()
            Button9.Enabled = False
        End If
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        ScriptHub.Show()
    End Sub

    Private Sub FastColoredTextBox1_TextChanged(sender As Object, e As FastColoredTextBoxNS.TextChangedEventArgs)

    End Sub
    <DllImport("Gdi32.dll", EntryPoint:="CreateRoundRectRgn")>
    Private Shared Function CreateRoundRectRgn(ByVal nLeftRect As Integer, ByVal nTopRect As Integer, ByVal nRightRect As Integer, ByVal nBottomRect As Integer, ByVal nWidthEllipse As Integer, ByVal nHeightEllipse As Integer) As IntPtr

    End Function
    Public Sub New()
        InitializeComponent()
        Me.FormBorderStyle = FormBorderStyle.None
        Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20))
    End Sub
    Private Sub f_Attach()
        p = Process.GetProcessesByName("RobloxPlayerBeta")
        If p.Count > 0 Then
            ListBox1.Items.Clear()
            ListBox1.Items.Add("attempting to inject")
            hack.LaunchExploit()
        Else

            ListBox1.Items.Clear()
            ListBox1.Items.Add("error")

            ListBox1.Items.Add("not in a game")
            time_ontop.Stop()
            MsgBox("You are currently not in a game so no attempt to inject was made", 0 + 16, "vector V3 -error")
            time_ontop.Start()
            Threading.Thread.Sleep(2000) ' 500 milliseconds = 0.5 seconds
            ListBox1.Items.Clear()
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.txt")
            Functions.PopulateListBox(ListBox1, "./Scripts", "*.lua")
        End If
    End Sub

    Private Sub time_ontop_Tick(sender As Object, e As EventArgs) Handles time_ontop.Tick
        Me.TopMost = True
    End Sub
    Private Sub loadbuttons()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button1.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button1.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button1.Width, 20, Button1.Width, Button1.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button1.Width - 20, Button1.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button1.Width - 20, Button1.Height, 20, Button1.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button1.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button1.Region = New Region(ellipseRadius)
        Button1.FlatStyle = FlatStyle.Flat
        Button1.FlatAppearance.BorderColor = BackColor
        load2()
    End Sub
    Sub load2()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button2.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button2.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button2.Width, 20, Button2.Width, Button2.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button2.Width - 20, Button2.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button2.Width - 20, Button2.Height, 20, Button2.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button2.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button2.Region = New Region(ellipseRadius)
        Button2.FlatStyle = FlatStyle.Flat
        Button2.FlatAppearance.BorderColor = BackColor
        load3()
    End Sub
    Sub load3()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button3.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button3.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button3.Width, 20, Button3.Width, Button3.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button3.Width - 20, Button3.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button3.Width - 20, Button3.Height, 20, Button3.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button3.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button3.Region = New Region(ellipseRadius)
        Button3.FlatStyle = FlatStyle.Flat
        Button3.FlatAppearance.BorderColor = BackColor
        load4()
    End Sub

    Sub load4()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button4.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button4.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button4.Width, 20, Button4.Width, Button4.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button4.Width - 20, Button4.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button4.Width - 20, Button4.Height, 20, Button4.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button4.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button4.Region = New Region(ellipseRadius)
        Button4.FlatStyle = FlatStyle.Flat
        Button4.FlatAppearance.BorderColor = BackColor
        load5()
    End Sub
    Sub load5()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button5.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button5.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button5.Width, 20, Button5.Width, Button5.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button5.Width - 20, Button5.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button5.Width - 20, Button5.Height, 20, Button5.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button5.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button5.Region = New Region(ellipseRadius)
        Button5.FlatStyle = FlatStyle.Flat
        Button5.FlatAppearance.BorderColor = BackColor
        load6()
    End Sub
    Sub load6()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        '     ellipseRadius.StartFigure()
        '  ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        '   ellipseRadius.AddLine(20, 0, Button6.Width - 20, 0)
        '    ellipseRadius.AddArc(New Rectangle(Button6.Width - 20, 0, 20, 20), -90, 90)
        '   ellipseRadius.AddLine(Button6.Width, 20, Button6.Width, Button6.Height - 20)
        '   ellipseRadius.AddArc(New Rectangle(Button6.Width - 20, Button6.Height - 20, 20, 20), 0, 90)
        '   ellipseRadius.AddLine(Button6.Width - 20, Button6.Height, 20, Button6.Height)
        '    ellipseRadius.AddArc(New Rectangle(0, Button6.Height - 20, 20, 20), 90, 90)
        '     ellipseRadius.CloseFigure()
        Button6.FlatStyle = FlatStyle.Flat
        Button6.FlatAppearance.BorderColor = BackColor
        Button6.FlatAppearance.MouseOverBackColor = BackColor
        Button6.FlatAppearance.MouseDownBackColor = BackColor
        load7()
    End Sub
    Sub load7()
        Dim ellipseRadius As New Drawing2D.GraphicsPath

        ' ellipseRadius.StartFigure()
        '   ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        '  ellipseRadius.AddLine(20, 0, Button7.Width - 20, 0)
        ' ellipseRadius.AddArc(New Rectangle(Button7.Width - 20, 0, 20, 20), -90, 90)
        ' ellipseRadius.AddLine(Button7.Width, 20, Button7.Width, Button7.Height - 20)
        'ellipseRadius.AddArc(New Rectangle(Button7.Width - 20, Button7.Height - 20, 20, 20), 0, 90)
        ' ellipseRadius.AddLine(Button7.Width - 20, Button7.Height, 20, Button7.Height)
        ' ellipseRadius.AddArc(New Rectangle(0, Button7.Height - 20, 20, 20), 90, 90)
        ' ellipseRadius.CloseFigure()
        Button7.FlatStyle = FlatStyle.Flat
        Button7.FlatAppearance.BorderColor = BackColor
        Button7.FlatAppearance.MouseOverBackColor = BackColor
        Button7.FlatAppearance.MouseDownBackColor = BackColor
        load8()
    End Sub
    Sub load8()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        '  ellipseRadius.StartFigure()
        ' ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        '  ellipseRadius.AddLine(20, 0, Button8.Width - 20, 0)
        '  ellipseRadius.AddArc(New Rectangle(Button8.Width - 20, 0, 20, 20), -90, 90)
        '  ellipseRadius.AddLine(Button8.Width, 20, Button8.Width, Button8.Height - 20)
        '  ellipseRadius.AddArc(New Rectangle(Button8.Width - 20, Button8.Height - 20, 20, 20), 0, 90)
        '  ellipseRadius.AddLine(Button8.Width - 20, Button8.Height, 20, Button8.Height)
        '   ellipseRadius.AddArc(New Rectangle(0, Button8.Height - 20, 20, 20), 90, 90)
        '    ellipseRadius.CloseFigure()
        Button8.FlatStyle = FlatStyle.Flat
        Button8.FlatAppearance.BorderColor = BackColor
        Button8.FlatAppearance.MouseOverBackColor = BackColor
        Button8.FlatAppearance.MouseDownBackColor = BackColor
        load9()
    End Sub
    Sub load9()
        Dim ellipseRadius As New Drawing2D.GraphicsPath
        ellipseRadius.StartFigure()
        ellipseRadius.AddArc(New Rectangle(0, 0, 20, 20), 180, 90)
        ellipseRadius.AddLine(20, 0, Button9.Width - 20, 0)
        ellipseRadius.AddArc(New Rectangle(Button9.Width - 20, 0, 20, 20), -90, 90)
        ellipseRadius.AddLine(Button9.Width, 20, Button9.Width, Button9.Height - 20)
        ellipseRadius.AddArc(New Rectangle(Button9.Width - 20, Button9.Height - 20, 20, 20), 0, 90)
        ellipseRadius.AddLine(Button9.Width - 20, Button9.Height, 20, Button9.Height)
        ellipseRadius.AddArc(New Rectangle(0, Button9.Height - 20, 20, 20), 90, 90)
        ellipseRadius.CloseFigure()
        Button9.Region = New Region(ellipseRadius)
        Button9.FlatStyle = FlatStyle.Flat
        Button9.FlatAppearance.BorderColor = BackColor

    End Sub

End Class