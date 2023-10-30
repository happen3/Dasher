Module ExtensionLoader
    Sub LoadExt(UserInputArgs As Array)
        ' Reference here extensions '

        Axtensions("hashverif") = "Verifies if 2 hashes are identical."
        Axtensions("md5gen <input>") = "Generates a md5 hash from one argument"

        Select Case UserInputArgs(0).ToLower()
            Case "hashverif"
                CommandMD5Verif(UserInputArgs)
            Case "md5gen"
                CommandGenerateMD5(UserInputArgs)
            Case Else
                DefinedCommands.CommandRunner(UserInputArgs)
        End Select
    End Sub
End Module
