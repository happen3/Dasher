Imports System.Reflection

Module Utils
    Public Function GetVariables() As List(Of String)
        ' Get the current assembly
        Dim assembly As Assembly = Assembly.GetExecutingAssembly()

        ' Get the type of the current module
        Dim moduleType As Type = assembly.GetModules()(0).GetType()

        ' Get the list of fields (variables) in the module
        Dim fields As FieldInfo() = moduleType.GetFields(BindingFlags.Static Or BindingFlags.Instance Or BindingFlags.NonPublic Or BindingFlags.Public)

        ' Create a list to store variable names
        Dim variableNames As New List(Of String)()

        ' Iterate through the fields and add variable names to the list
        For Each field As FieldInfo In fields
            variableNames.Add(field.Name)
        Next

        ' Return the list of variable names
        Return variableNames
    End Function
    Public Function SplitArguments(input As String) As String()
        Dim parts As New List(Of String)
        Dim insideQuotes As Boolean = False
        Dim startIndex As Integer = 0

        For i As Integer = 0 To input.Length - 1
            If input(i) = """"c Then
                insideQuotes = Not insideQuotes
            ElseIf input(i) = " "c AndAlso Not insideQuotes Then
                Dim part As String = input.Substring(startIndex, i - startIndex)
                parts.Add(part)
                startIndex = i + 1
            End If
        Next

        ' Add the last part after the loop
        Dim lastPart As String = input.Substring(startIndex)
        parts.Add(lastPart)

        Return parts.ToArray()
    End Function
End Module
