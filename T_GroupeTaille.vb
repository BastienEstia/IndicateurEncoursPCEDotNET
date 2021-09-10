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
        Dim reader As OleDbDataReader = Nothing
        SelectAllByTable = New List(Of GroupeTaille)
        Dim taillelistTab As String()
        Dim i As Integer = 0
        query = "SELECT * FROM T_GroupeTaille Where Poste = Val_poste"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_poste", Table)
            End With
            Cmd.CommandText = query
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            While reader.Read()
                Dim groupeTailleItem As New GroupeTaille()
                Dim groupeString = reader("Taillelist")
                If Not IsDBNull(groupeString) Then
                    taillelistTab = Split(groupeString, "-")
                    i = 0
                    While i <= taillelistTab.Length - 1
                        groupeTailleItem.TailleList.Add(taillelistTab(i))
                        i += 1
                    End While
                Else
                    'groupeTailleItem.TailleList.Add("")
                End If
                groupeTailleItem.Coef = reader("Coef")
                groupeTailleItem.Groupe = reader("Grp")
                groupeTailleItem.Table = reader("Poste")
                groupeTailleItem.Id = reader("Id")
                SelectAllByTable.Add(groupeTailleItem)
            End While

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        Cmd.Parameters.RemoveAt("Val_poste")
        reader.Close()
        Cmd.Connection.Close()
    End Function

    Public Function InsertQuery(groupeTaille As GroupeTaille) As Boolean
        Dim Query As String
        InsertQuery = True
        Dim er As OleDbDataReader = Nothing
        Dim i As Integer = 0
        Dim tailleString As String = Nothing
        Query = "INSERT INTO T_GroupeTaille (Poste, Taillelist, Coef, Grp) VALUES (Val_poste, Val_taillelist, Val_coef, Val_groupe)"
        While i <= groupeTaille.TailleList.Count - 1
            If i = groupeTaille.TailleList.Count - 1 Then
                tailleString += groupeTaille.TailleList(i)
            Else
                tailleString += groupeTaille.TailleList(i) & "-"
            End If
            i += 1
        End While
        Try
            With Cmd.Parameters
                .AddWithValue("Val_poste", groupeTaille.Table)
                .AddWithValue("Val_taillelist", tailleString)
                .AddWithValue("Val_coef", groupeTaille.Coef)
                .AddWithValue("Val_groupe", groupeTaille.Groupe)
            End With
            Cmd.CommandText = Query
            Cmd.Connection.Open()
            er = Cmd.ExecuteReader()
        Catch e As Exception
            InsertQuery = False
            MessageBox.Show(e.Message)
        End Try
        With Cmd.Parameters
            .RemoveAt("Val_table")
            .RemoveAt("Val_taillelist")
            .RemoveAt("Val_coef")
            .RemoveAt("Val_groupe")
        End With
        er.Close()
        Cmd.Connection.Close()
    End Function

    Public Function TruncateQuery(table As String, groupe As String) As Boolean
        Dim Query As String
        TruncateQuery = True
        Dim er As OleDbDataReader = Nothing
        Query = "DELETE * From T_GroupeTaille WHERE Poste = Val_poste AND Grp = Val_groupe"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_poste", table)
                .AddWithValue("Val_groupe", groupe)
            End With
            Cmd.CommandText = Query
            Cmd.Connection.Open()
            er = Cmd.ExecuteReader()
        Catch e As Exception
            TruncateQuery = False
            MessageBox.Show(e.Message)
        End Try
        With Cmd.Parameters
            .RemoveAt("Val_table")
            .RemoveAt("Val_groupe")
        End With
        er.Close()
        Cmd.Connection.Close()
    End Function

    Public Function SelectAllById(id As Integer, cmd As OleDbCommand) As GroupeTaille
        Dim query
        Dim groupeTaille As New GroupeTaille()
        Dim reader As OleDbDataReader = Nothing
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
                groupeTaille.Groupe = reader("Grp")
                groupeTaille.Coef = reader("Coef")
                groupeTaille.Table = reader("Poste")
                groupeTaille.Id = reader("Id")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        With cmd.Parameters
            .RemoveAt("Val_id")
        End With
        reader.Close()
        cmd.Connection.Close()
        SelectAllById = groupeTaille
    End Function

    Public Function SelectAllIndic(cmd As OleDbCommand) As List(Of GroupeTaille)
        Dim query As String
        Dim groupeTailleList As New List(Of GroupeTaille)
        Dim reader As OleDbDataReader = Nothing
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
            SelectAllIndic = Nothing
            reader.Close()
            cmd.Connection.Close()
            Exit Function
        End Try
        reader.Close()
        cmd.Connection.Close()
        SelectAllIndic = groupeTailleList
    End Function

    Public Function UpdateQuery(groupeTaille As GroupeTaille) As Boolean
        Dim Query As String
        Dim er As OleDbDataReader = Nothing
        Dim i As Integer = 0
        Dim tailleString As String = ""
        Query = "UPDATE T_GroupeTaille SET Taillelist = Val_taillelist, Coef = Val_coef WHERE Poste = Val_poste AND Grp = Val_groupe;"
        While i <= groupeTaille.TailleList.Count - 1
            If i = groupeTaille.TailleList.Count - 1 Then
                tailleString += groupeTaille.TailleList(i)
            Else
                tailleString += groupeTaille.TailleList(i) & "-"
            End If
            i += 1
        End While
        Try
            With Cmd.Parameters
                .AddWithValue("Val_taillelist", tailleString)
                .AddWithValue("Val_coef", groupeTaille.Coef)
                .AddWithValue("Val_poste", groupeTaille.Table)
                .AddWithValue("Val_groupe", groupeTaille.Groupe)
            End With
            Cmd.CommandText = Query
            Cmd.Connection.Open()
            er = Cmd.ExecuteReader()
        Catch e As Exception
            UpdateQuery = False
            MessageBox.Show(e.Message & vbCrLf & e.Source)
        End Try
        With Cmd.Parameters
            .RemoveAt("Val_poste")
            .RemoveAt("Val_taillelist")
            .RemoveAt("Val_coef")
            .RemoveAt("Val_groupe")
        End With
        er.Close()
        Cmd.Connection.Close()
        UpdateQuery = True
    End Function
End Class
