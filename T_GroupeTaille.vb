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
        SelectAllByTable = Nothing
        Dim groupeTaille As New GroupeTaille()
        query = "SELECT * FROM T_TailleGroupe Where Table = Val_table"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_Table", Table)
            End With
            Cmd.CommandText = query
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            While reader.Read()
                groupeTaille.TailleList = reader("TailleList")
                groupeTaille.Coef = reader("Coef")
                groupeTaille.Groupe = reader("Groupe")
                groupeTaille.Table = reader("Table")
                groupeTaille.Id = reader("Id")
                SelectAllByTable.Add(groupeTaille)
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        Cmd.Connection.Close()
    End Function

    Public Function InsertQuery(groupeTaille As GroupeTaille) As Boolean
        Dim Query As String
        Query = "INSERT INTO T_TailleGroupe & (Table, TailleList, Coef, Groupe) VALUES (Val_table, Val_tailleList, Val_coef, Val_Groupe)"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_table", groupeTaille.Table)
                .AddWithValue("Val_tailleList", groupeTaille.TailleList)
                .AddWithValue("Val_coef", groupeTaille.Coef)
                .AddWithValue("Val_tailleList", groupeTaille.TailleList)
                .AddWithValue("Val_groupe", groupeTaille.Groupe)
            End With
            Cmd.ExecuteReader()
            With Cmd.Parameters
                .RemoveAt("Val_table")
                .RemoveAt("Val_tailleList")
                .RemoveAt("Val_coef")
                .RemoveAt("Val_tailleList")
                .RemoveAt("Val_groupe")
            End With
            Cmd.Connection.Close()
        Catch e As Exception
            InsertQuery = False
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        InsertQuery = True
    End Function

    Public Function TruncateQuery(id As Integer, cmd As OleDbCommand) As Boolean
        Dim Query As String
        Query = "DELETE * From T_TailleGroupe WHERE Id = Val_id"
        Try
            cmd.CommandText = Query
            cmd.Connection.Open()
            cmd.ExecuteReader()
        Catch e As Exception
            TruncateQuery = False
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        TruncateQuery = True
        cmd.Connection.Close()
    End Function

    Public Function SelectAllById(id As Integer, cmd As OleDbCommand) As GroupeTaille
        Dim query
        Dim groupeTaille As New GroupeTaille()
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_TailleGroupe Where id = Val_id"
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
