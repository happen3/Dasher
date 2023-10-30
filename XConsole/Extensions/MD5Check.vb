Module MD5Check

    ' BUILT-IN MODULE TO CHECK HASHES '
    Private Sub VerifyMD5Hash(hash1 As String, hash2 As String)

        '''<summary>Compare a hash to see if it's exactly same.</summary>

        If hash1 = hash2 Then
            Console.WriteLine("hashverif: Hash values are exactly equal.")
        Else
            Console.WriteLine("hashverif: Hash values are not equal!")
        End If
    End Sub
    Sub CommandMD5Verif(args As Array)
        If args.Length = 3 Then
            VerifyMD5Hash(args(1), args(2))
        Else
            ELogger.ThrowErrorLog("hashverif", "Excepted two arguments hashString")
        End If
    End Sub
End Module
