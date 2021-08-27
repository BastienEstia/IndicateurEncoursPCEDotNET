Option Explicit On
Imports System.Data.OleDb

Public Class T_Indicateur
    Public Property cmd As OleDbCommand

    Public Sub New(cmd As OleDbCommand)
        Me.cmd = cmd
    End Sub

    Public Sub New()

    End Sub

    Public Function SelectAllByTable(table As String) As Indicateur
        Dim query
        Dim reader As OleDbDataReader
        SelectAllByTable = New Indicateur
        query = "SELECT * FROM T_Indicateur Where Table = Val_table"
        Try
            With cmd.Parameters
                .AddWithValue("Val_Table", table)
            End With
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()
            While reader.Read()
                SelectAllByTable.SeuilBas = reader("SeuilBas")
                SelectAllByTable.SeuilHaut = reader("SeuilHaut")
                SelectAllByTable.EncoursLvl = reader("EncoursLvl")
                SelectAllByTable.Table = reader("Table")
                SelectAllByTable.Id = reader("Id")
                SelectAllByTable.NbPlaqueMax = reader("NbPlaqueMax")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            cmd.Connection.Close()
            Exit Function
        End Try
        cmd.Connection.Close()
    End Function

    Public Function InsertQuery(table As String, seuilHaut As Integer, seuilBas As Integer, encoursLvl As Integer, nbPlaqueMax As Integer, cmd As OleDbCommand) As Boolean
        Dim Query As String
        Query = "INSERT INTO T_Indicateur (Table, SeuilHaut, SeuilBas, EncoursLvl) VALUES (Val_table, Val_seuilHaut, Val_seuilBas, Val_encoursLvl)"
        Try
            With cmd.Parameters
                .AddWithValue("Val_table", table)
                .AddWithValue("Val_seuilHaut", seuilHaut)
                .AddWithValue("Val_seuilBas", seuilBas)
                .AddWithValue("Val_encoursLvl", encoursLvl)
                .AddWithValue("Val_nbPlaqueMax", nbPlaqueMax)
            End With
            cmd.ExecuteReader()
            With cmd.Parameters
                .RemoveAt("Val_table")
                .RemoveAt("Val_seuilHaut")
                .RemoveAt("Val_seuilBas")
                .RemoveAt("Val_encoursLvl")
                .RemoveAt("Val_nbPlaqueMax")
            End With
            cmd.Connection.Close()
        Catch e As Exception
            InsertQuery = False
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        InsertQuery = True
    End Function

    Public Function TruncateQuery(id As Integer, cmd As OleDbCommand) As Boolean
        Dim Query As String
        Query = "DELETE * From T_Indicateur WHERE Table = Val_table"
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
    End Function

    Public Function SelectAllById(id As Integer) As Indicateur
        Dim query
        SelectAllById = Nothing
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_Indicateur Where id = Val_id"
        Try
            With cmd.Parameters
                .AddWithValue("Val_id", id)
            End With
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()
            While reader.Read()
                SelectAllById.SeuilBas = reader("SeuilBas")
                SelectAllById.SeuilHaut = reader("SeuilHaut")
                SelectAllById.EncoursLvl = reader("EncoursLvl")
                SelectAllById.Table = reader("Table")
                SelectAllById.Id = reader("Id")
                SelectAllById.NbPlaqueMax = reader("NbPlaqueMax")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            Exit Function
        End Try
        cmd.Connection.Close()
    End Function

    Public Function SelectAll() As List(Of Indicateur)
        Dim query As String
        Dim indicList As New List(Of Indicateur)
        Dim reader As OleDbDataReader
        Dim i As Integer
        i = 0
        query = "SELECT * FROM T_Inidicateur"
        Try
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()

            While reader.Read()
                indicList.Append(reader.GetValue(i))
                i += 1
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAll = indicList
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAll = indicList
    End Function

End Class
