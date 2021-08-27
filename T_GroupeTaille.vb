Option Explicit On
Imports System.Data.OleDb

Public Class T_GroupeTaille
    Private Property Cmd As OleDbCommand

    Public Sub New(cmd As OleDbCommand)
        Me.Cmd = cmd
    End Sub
    Public Sub New()

    End Sub

    Public Function SelectAllByTable(Table As String) As List(Of GroupeTaille)
        Dim query
        Dim reader As OleDbDataReader
        SelectAllByTable = New List(Of GroupeTaille)

        Dim taillelistTab As String()
        Dim i As Integer = 0
        query = "SELECT * FROM T_GroupeTaille Where Table = Val_table"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_Table", Table)
            End With
            Cmd.CommandText = query
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            While reader.Read()
                Dim groupeTailleItem As New GroupeTaille()
                If Not reader.IsDBNull(2) Then
                    taillelistTab = Split(reader("TailleList"), ",")
                    While i <= taillelistTab.Length - 1
                        groupeTailleItem.TailleList.Add(taillelistTab(i))
                        i += 1
                    End While
                End If
                groupeTailleItem.Coef = reader("Coef")
                groupeTailleItem.Groupe = reader("Groupe")
                groupeTailleItem.Table = reader("Table")
                groupeTailleItem.Id = reader("Id")
                SelectAllByTable.Add(groupeTailleItem)
            End While
            reader.Close()
            Cmd.Parameters.RemoveAt("Val_Table")
            Cmd.CommandText.Remove(0)
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        Cmd.Connection.Close()
    End Function

    Public Function InsertQuery(groupeTaille As GroupeTaille) As Boolean
        Dim Query As String
        Dim er As OleDbDataReader
        Dim i As Integer = 0
        Dim tailleString As String = Nothing
        Query = "INSERT INTO T_GroupeTaille (Table, TailleList, Coef, Groupe) VALUES (Val_table, Val_taillelist, Val_coef, Val_groupe)"
        While i <= groupeTaille.TailleList.Count - 1
            If i = groupeTaille.TailleList.Count - 1 Then
                tailleString += groupeTaille.TailleList(i)
            Else
                tailleString += groupeTaille.TailleList(i) & ","
            End If
            i += 1
        End While
        Try
            With Cmd.Parameters
                .AddWithValue("Val_table", groupeTaille.Table)
                .AddWithValue("Val_taillelist", tailleString)
                .AddWithValue("Val_coef", groupeTaille.Coef)
                .AddWithValue("Val_groupe", groupeTaille.Groupe)
            End With
            Cmd.CommandText = Query
            Cmd.Connection.Open()
            er = Cmd.ExecuteReader()
            With Cmd.Parameters
                .RemoveAt("Val_table")
                .RemoveAt("Val_taillelist")
                .RemoveAt("Val_coef")
                .RemoveAt("Val_groupe")
            End With
            er.Close()
            Cmd.Connection.Close()
        Catch e As Exception
            InsertQuery = False
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        InsertQuery = True
    End Function

    Public Function TruncateQuery(table As String, groupe As String) As Boolean
        Dim Query As String
        Dim er As OleDbDataReader
        Query = "DELETE * From T_GroupeTaille WHERE Table = Val_table AND Groupe = Val_groupe"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_table", table)
                .AddWithValue("Val_groupe", groupe)
            End With
            Cmd.CommandText = Query
            Cmd.Connection.Open()
            er = Cmd.ExecuteReader()
            With Cmd.Parameters
                .RemoveAt("Val_table")
                .RemoveAt("Val_groupe")
            End With
        Catch e As Exception
            TruncateQuery = False
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        TruncateQuery = True
        er.Close()
        Cmd.Connection.Close()
    End Function

    Public Function SelectAllById(id As Integer, cmd As OleDbCommand) As GroupeTaille
        Dim query
        Dim groupeTaille As New GroupeTaille()
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_GroupeTaille Where id = Val_id"
        Try
            With cmd.Parameters
                .AddWithValue("Val_id", id)
            End With
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()
            While reader.Read()
                groupeTaille.TailleList = reader("TailleList")
                groupeTaille.Groupe = reader("Groupe")
                groupeTaille.Coef = reader("Coef")
                groupeTaille.Table = reader("Table")
                groupeTaille.Id = reader("Id")
            End While
            With cmd.Parameters
                .RemoveAt("Val_id")
            End With
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAllById = groupeTaille
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAllById = groupeTaille
    End Function

    Public Function SelectAllIndic(cmd As OleDbCommand) As List(Of GroupeTaille)
        Dim query As String
        Dim groupeTailleList As New List(Of GroupeTaille)
        Dim reader As OleDbDataReader
        Dim i As Integer
        i = 0
        query = "SELECT * FROM T_GroupeTaille"
        Try
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()

            While reader.Read()
                groupeTailleList.Append(reader.GetValue(i))
                i += 1
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAllIndic = groupeTailleList
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAllIndic = groupeTailleList
    End Function

End Class
