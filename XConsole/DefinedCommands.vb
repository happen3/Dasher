Module DefinedCommands
    Sub CommandRunner(UserInputArgs As Array)
        Select Case UserInputArgs(0).ToLower()
            Case "exit"
                CommandExit(UserInputArgs)
            Case "print"
                CommandPrint(UserInputArgs)
            Case "calculate"
                CommandCalculate(UserInputArgs)
            Case "version"
                CommandVersion(UserInputArgs)
            Case "changedir"
                CommandChangeDirectory(UserInputArgs)
            Case "list"
                CommandList(UserInputArgs)
            Case "makedir"
                CommandMakeDir(UserInputArgs)
            Case "removedir"
                CommandRemoveDir(UserInputArgs)
            Case "datetime"
                CommandDateTime(UserInputArgs)
            Case "help"
                CommandHelp(UserInputArgs)
            Case "throwelog"
                If AllowDebugCommand = False Then
                    Console.WriteLine("dahser: " + UserInputArgs(0) + " is not a command")
                Else
                    DebugThrowTestELog(UserInputArgs)
                End If
            Case "sendwlog"
                If AllowDebugCommand = False Then
                    Console.WriteLine("dahser: " + UserInputArgs(0) + " is not a command")
                Else
                    DebugThrowTestWLog(UserInputArgs)
                End If
            Case "var"
                CommandGetVariables(UserInputArgs)
            Case "setvar"
                CommandMakeVariable(UserInputArgs)
            Case "runfile"
                CommandRun(UserInputArgs)
            Case "clear"
                CommandClearScreen(UserInputArgs)
            Case "cname"
                CommandSetConsoleTitle(UserInputArgs)
            Case "who"
                CommandWho(UserInputArgs)
            Case "ifequal"
                CommandCompare(UserInputArgs)
            Case Else
                Console.WriteLine("dahser: " + UserInputArgs(0) + " is not a command")
        End Select
    End Sub
End Module
