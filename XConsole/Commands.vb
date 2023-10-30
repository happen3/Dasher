Imports System.Data

Module Commands
    Sub CommandMakeVariable(args As Array)
        If args.Length = 3 Then
            iVariables(args(1)) = args(2)
        Else
            Console.WriteLine(args(0) & ": Excepted two arguments varName, varValue")
        End If
    End Sub

    Sub CommandCompare(args As Array)
        If args.Length = 3 Then
            If args(1) = args(2) Then
                Console.WriteLine("ifequal: Left and right are equal.")
            Else
                Console.WriteLine("ifequal: Left and right are not equal.")
            End If
        Else
            ThrowErrorLog(args(0), "Excepted two arguments Left, Right")
        End If
    End Sub
    Sub CommandSetConsoleTitle(args As Array)
        If args.Length = 2 Then
            Console.WriteLine($"cname: Setting name to {args(1)}")
            Console.Title = args(1)
        Else
            Console.WriteLine(args(0) & ": Excepted one argument cTitle")
        End If
    End Sub
    Sub CommandWho(args As Array)
        If args.Length = 1 Then
            Console.WriteLine($"who: You are {Environment.UserName}.")
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub CommandClearScreen(args As Array)
        If args.Length = 1 Then
            Console.Clear()
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub CommandRun(args As Array)
        Try
            YKeyPressed = False
            If args.Length > 1 Then
                Dim filePath As String = args(1)
                If iVariables.ContainsKey("ARunMsg") AndAlso CBool(iVariables("ARunMsg")) Then
                    Console.WriteLine("Running unknown apps might put your computer at risk.")
                    Console.WriteLine("Be sure to trust the app and the app provider before starting it.")
                    Console.WriteLine("Do you want to continue? (y/n)")

                    While Not YKeyPressed
                        Dim key As ConsoleKeyInfo = Console.ReadKey(True)
                        If key.Key = ConsoleKey.Y Then
                            YKeyPressed = True
                            If IO.File.Exists(filePath) Then
                                Shell(filePath)
                            Else
                                Console.WriteLine("File not found: " & filePath)
                            End If
                        ElseIf key.Key = ConsoleKey.N Then
                            YKeyPressed = True
                            Console.WriteLine("Operation canceled.")
                        End If
                    End While
                Else
                    If IO.File.Exists(filePath) Then
                        Shell(filePath)
                    Else
                        Console.WriteLine("File not found: " & filePath)
                    End If
                End If
            Else
                Console.WriteLine(args(0) & ": Expected a file path as an argument")
            End If
        Catch ex As Exception
            ThrowErrorLog("runfile", "Handled exception: " & ex.ToString)
        End Try
    End Sub

    Sub CommandGetVariables(args As Array)
        If args.Length = 1 Then
            Console.WriteLine("List of the active variables:")
            For Each variableName As String In iVariables.Keys
                Dim variableValue As Object = iVariables(variableName)
                Console.WriteLine($"{variableName} = {variableValue}")
            Next
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub DebugThrowTestELog(args)
        If args.Length = 2 Then
            Console.WriteLine("elogger: Throwing test exception... ")
            ThrowErrorLog("debug/testing", args(1))
        Else
            Console.WriteLine(args(0) & ": Excepted one argument testException")
        End If
    End Sub
    Sub DebugThrowTestWLog(args)
        If args.Length = 2 Then
            Console.WriteLine("elogger: Throwing test warning... ")
            SendWarnLog("debug/testing", args(1))
        Else
            Console.WriteLine(args(0) & ": Excepted one argument testWarnMsg")
        End If
    End Sub
    Sub CommandVersion(args)
        If args.Length = 1 Then
            Console.WriteLine("dasher: Using " & Environment.OSVersion.ToString)
            Console.WriteLine("dasher: ARR\Dasher version " + xversion)
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If

    End Sub
    Sub CommandExit(args As String())
        If args.Length = 1 Then
            Console.WriteLine(" . . .")
            Environment.Exit(0)
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub CommandDateTime(args As String())
        If args.Length = 1 Then
            Console.WriteLine("datetime: The current date&time is " & DateAndTime.Now)
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub CommandMakeDir(args As String())
        If args.Length = 2 Then ' Check the number of arguments (excluding the input)
            Dim dirName As String = args(1).Replace(ChrW(34), "") ' Remove quotes
            Try
                IO.Directory.CreateDirectory(dirName)
                Console.WriteLine("makedir: Created folder " & dirName)
            Catch ex As Exception
                ThrowErrorLog("makedir", " Error creating folder " & dirName & ": " & ex.Message)
            End Try
        Else
            Console.WriteLine(args(0) & ": Expected one argument dirName") ' Throw an argument
        End If
    End Sub

    Sub CommandRemoveDir(args As String())
        If args.Length = 2 Then ' Check the number of arguments (excluding the input)
            Dim dirName As String = args(1).Replace(ChrW(34), "") ' Remove quotes
            Try
                If IO.Directory.Exists(dirName) Then
                    IO.Directory.Delete(dirName)
                    Console.WriteLine("removedir: Removed folder " & dirName)
                Else
                    Console.WriteLine("removedir: Folder " & dirName & " does not exist.")
                End If
            Catch ex As Exception
                ThrowErrorLog("removedir", "Error removing folder " & dirName & ": " & ex.Message)
            End Try
        Else
            Console.WriteLine(args(0) & ": Expected one argument dirName") ' Throw an argument
        End If
    End Sub

    Sub CommandList(args As String())
        If args.Length = 1 Then
            Console.WriteLine("Directory listing for " + CurrentDirectory)
            For Each dir As String In IO.Directory.GetDirectories(CurrentDirectory)
                Console.WriteLine(dir)
            Next

            Console.WriteLine("File listing for " + CurrentDirectory)
            For Each file As String In IO.Directory.GetFiles(CurrentDirectory)
                Console.WriteLine(file)
            Next

        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub
    Sub CommandPrint(args As String())
        If args.Length = 2 Then ' Check the number of arguments (excluding the input)
            Dim text As String = args(1).Replace(ChrW(34), "") ' Remove quotes
            Console.WriteLine(text) ' Print the text
        Else
            Console.WriteLine(args(0) & ": Expected one argument text") ' Throw an argument
        End If
    End Sub
    Sub CommandCalculate(args As String())
        If args.Length <> 2 Then
            Console.WriteLine(args(0) & ": Expected one argument expression")
        End If
        Using table As New DataTable()
            ' Calculate the expression and store the result in a variable
            Dim result As Object = table.Compute(Replace(args(1), ChrW(34), ""), "")

            ' Convert the result to the appropriate data type (in this case, Integer)
            Dim finalResult As Integer = Convert.ToInt32(result)

            ' Print the result
            Console.WriteLine(args(0) + ": Expression " + args(1))
            Console.WriteLine(args(0) + ": Result is " & finalResult)
        End Using ' Dispose of the DataTable after use
    End Sub
    Sub CommandHelp(args As String())
        If args.Length = 1 Then
            Console.WriteLine("Available Commands:")
            Console.WriteLine("  exit - Exit the dasher application.")
            Console.WriteLine("  print <text> - Print the specified text.")
            Console.WriteLine("  calculate <expression> - Evaluates a mathematical expression.")
            Console.WriteLine("  version - Display version information.")
            Console.WriteLine("  changedir <directory> - Change the current directory.")
            Console.WriteLine("  list - List directories and files in the current directory.")
            Console.WriteLine("  makedir <directory> - Create a new directory.")
            Console.WriteLine("  removedir <directory> - Remove an empty directory.")
            Console.WriteLine("  datetime - Display the current date and time.")
            Console.WriteLine("  help - Display this help message.")
            Console.WriteLine("  var - Display a list of active variables.")
            Console.WriteLine("  setvar - Creates/sets a variable.")
            Console.WriteLine("  clear - Clear screen.")
            Console.WriteLine("  run <file> - Runs the specified file.")
            Console.WriteLine("  who - Shows your username.")
            Console.WriteLine("  cname <name> - Sets the console title.")
            Console.WriteLine("  ifequal <left> <right> - Compares if left and right are equal or not.")
            Console.WriteLine("  == EXTENSIONS == ")
            For Each Axt As String In Axtensions.Keys
                Dim Desc As Object = Axtensions(Axt)
                Console.WriteLine($"  {Axt} - {Desc}")
            Next
        Else
            Console.WriteLine(args(0) & ": Argument use disallowed")
        End If
    End Sub

    Sub CommandChangeDirectory(args As String())
        If args.Length = 2 Then
            Dim newDirectory As String = args(1).Replace(ChrW(34), "") ' Remove quotes from the directory path
            Try
                ' Check if the directory exists before changing to it
                If IO.Directory.Exists(newDirectory) Then
                    IO.Directory.SetCurrentDirectory(newDirectory)
                    CurrentDirectory = IO.Path.GetFullPath(".")
                Else
                    Console.WriteLine(args(0) + ": The specified directory does not exist.")
                End If
            Catch ex As Exception
                ThrowErrorLog(args(0), "An error occurred: " & ex.Message)
            End Try
        Else
            Console.WriteLine(args(0) & ": Expected one argument: directory path")
        End If

    End Sub
End Module
