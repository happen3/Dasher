Imports System.Data
Imports System.Security
Imports System.Linq.Expressions
Imports System.Runtime.InteropServices
Imports System.Reflection

Module Program

    Public Axtensions As New Dictionary(Of String, String)()

    Public xversion = "1.1.0-win64"
    Public iVariables As New Dictionary(Of String, Object)()

#If DEBUG Then
    Public AllowDebugCommand As Boolean = True
#Else
    Public AllowDebugCommand As Boolean = False
#End If

#If NETCOREAPP Then
    Public CurrentDirectory As String
    Sub New()
        If RuntimeInformation.IsOSPlatform(OSPlatform.Windows) Then
            CurrentDirectory = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
        ElseIf RuntimeInformation.IsOSPlatform(OSPlatform.Linux) OrElse RuntimeInformation.IsOSPlatform(OSPlatform.OSX) Then
            CurrentDirectory = "/home/"
            xversion = "1.1.0-linux"
        Else
            ' Unsupported platform, handle accordingly
            Throw New PlatformNotSupportedException("Unsupported platform")
        End If
    End Sub
#Else
    Public CurrentDirectory As String = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)
#End If

    Public YKeyPressed As Boolean = False
    Public UserInputArgs As String()

    Sub Main(args As String())

        iVariables("ARunMsg") = True

        Console.ForegroundColor = ConsoleColor.White

        IO.Directory.SetCurrentDirectory(CurrentDirectory)

        Console.Title = "dasher " + xversion
        Dim UserInput As String


        Console.WriteLine("Welcome to dasher!")
        Console.WriteLine("Type " + ChrW(42) + "help" + ChrW(42) + " for a list of commands.")
        Console.WriteLine("")
        Do
            Console.WriteLine(CurrentDirectory)
            Console.Write("dahser " + xversion + " $~ ")

            UserInput = Console.ReadLine()

            ' Use a custom function to split arguments while preserving quoted parts
            UserInputArgs = SplitArguments(UserInput)

            If UserInputArgs.Length > 0 Then
                ExtensionLoader.LoadExt(UserInputArgs)
            End If
        Loop
    End Sub


End Module
