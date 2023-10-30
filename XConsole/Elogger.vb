Module ELogger

    Private Sub ChangeConsoleColor(Colour As ConsoleColor)
        Console.ForegroundColor = Colour ' Set console text color to Colour (param)
    End Sub
    Public Sub ThrowErrorLog(command As String, msg As String)
        ChangeConsoleColor(ConsoleColor.Red) ' See ChangeConsoleColor()
        Console.WriteLine(command & ": " & msg) ' Writes the log
        ChangeConsoleColor(ConsoleColor.White)
    End Sub
    Public Sub SendWarnLog(command As String, msg As String)
        ChangeConsoleColor(ConsoleColor.DarkYellow) ' See ChangeConsoleColor()
        Console.WriteLine(command & ": " & msg) ' Writes the log
        ChangeConsoleColor(ConsoleColor.White)
    End Sub
End Module
