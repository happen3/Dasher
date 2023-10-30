Imports System.Security.Cryptography
Imports System.Text

Module MD5Hasher

    ''' <summary>
    ''' Built in module that allows to make MD5 hashes.
    ''' </summary>
    ''' <param name="input"></param>
    ''' <returns>hash As String</returns>

    Function GenerateMD5Hash(ByVal input As String) As String
        Using md5 As MD5 = MD5.Create()
            Dim inputBytes As Byte() = Encoding.UTF8.GetBytes(input)
            Dim hashBytes As Byte() = md5.ComputeHash(inputBytes)
            Dim sb As New StringBuilder()

            For i As Integer = 0 To hashBytes.Length - 1
                sb.Append(hashBytes(i).ToString("x2"))
            Next

            Return sb.ToString()
        End Using
    End Function

    Sub CommandGenerateMD5(args As Array)
        If args.Length = 2 Then
            Dim hash = GenerateMD5Hash(Replace(args(1), ChrW(34), ""))
            Console.WriteLine($"md5gen: MD5 result is {hash}")
        Else
            ThrowErrorLog("md5gen", "Excepted one argument inputString")
        End If
    End Sub
End Module