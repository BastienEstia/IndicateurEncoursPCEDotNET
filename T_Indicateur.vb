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
        Dim reader As OleDbDataReader = Nothing
        SelectAllByTable = New Indicateur
        query = "SELECT * FROM T_Indicateur Where CurrentPoste = Val_poste"
        Try
            With cmd.Parameters
                .AddWithValue("Val_poste", table)
            End With
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()
            While reader.Read()
                SelectAllByTable.SeuilBas = reader("SeuilBas")
                SelectAllByTable.SeuilHaut = reader("SeuilHaut")
                SelectAllByTable.EncoursLvl = reader("EncoursLvl")
                SelectAllByTable.Table = reader("CurrentPoste")
                SelectAllByTable.Id = reader("Id")
                SelectAllByTable.NbPlaqueMax = reader("NbPlaqueMax")
                SelectAllByTable.PosteFourn = reader("PosteFourn")
                SelectAllByTable.PosteClient = reader("PosteClient")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAllByTable = Nothing
        End Try
        cmd.Parameters.RemoveAt("Val_poste")
        reader.Close()
        cmd.Connection.Close()
    End Function

    Public Function InsertQuery(table As String, seuilHaut As Integer, seuilBas As Integer, encoursLvl As Integer, nbPlaqueMax As Integer, PosteClient As String, PosteFourn As String, cmd As OleDbCommand) As Boolean
        Dim Query As String
        InsertQuery = True
        Dim er As OleDbDataReader = Nothing
        Query = "INSERT INTO T_Indicateur (Table, SeuilHaut, SeuilBas, EncoursLvl, PosteClient, PosteFourn) VALUES (Val_table, Val_seuilHaut, Val_seuilBas, Val_encoursLvl, Val_posteclient, Val_postefourn)"
        Try
            With cmd.Parameters
                .AddWithValue("Val_table", table)
                .AddWithValue("Val_seuilHaut", seuilHaut)
                .AddWithValue("Val_seuilBas", seuilBas)
                .AddWithValue("Val_encoursLvl", encoursLvl)
                .AddWithValue("Val_nbPlaqueMax", nbPlaqueMax)
                .AddWithValue("Val_posteclient", PosteClient)
                .AddWithValue("Val_postefourn", PosteFourn)
            End With
            cmd.Connection.Open()
            er = cmd.ExecuteReader()
        Catch e As Exception
            InsertQuery = False
            MessageBox.Show(e.Message)
        End Try
        With cmd.Parameters
            .RemoveAt("Val_table")
            .RemoveAt("Val_seuilHaut")
            .RemoveAt("Val_seuilBas")
            .RemoveAt("Val_encoursLvl")
            .RemoveAt("Val_nbPlaqueMax")
            .RemoveAt("Val_posteclient")
            .RemoveAt("Val_postefourn")
        End With
        er.Close()
        cmd.Connection.Close()
    End Function

    Public Function TruncateQuery(table As Integer, cmd As OleDbCommand) As Boolean
        Dim Query As String
        TruncateQuery = True
        Dim er As OleDbDataReader = Nothing
        Query = "DELETE * From T_Indicateur WHERE Poste = Val_poste"
        Try
            With cmd.Parameters
                .AddWithValue("Val_poste", table)
            End With
            cmd.CommandText = Query
            cmd.Connection.Open()
            er = cmd.ExecuteReader()
        Catch e As Exception
            TruncateQuery = False
            MessageBox.Show(e.Message)
        End Try
        cmd.Parameters.RemoveAt("Val_poste")
        er.Close()
        cmd.Connection.Close()
    End Function

    Public Function SelectAllById(id As Integer) As Indicateur
        Dim query
        SelectAllById = Nothing
        Dim reader As OleDbDataReader = Nothing
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
                SelectAllById.PosteClient = reader("PosteClient")
                SelectAllById.PosteFourn = reader("PosteFourn")
            End While

        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        cmd.Parameters.RemoveAt("Val_id")
        reader.Close()
        cmd.Connection.Close()
    End Function

    Public Function SelectAll() As List(Of Indicateur)
        Dim query As String
        SelectAll = Nothing
        Dim indicList As New List(Of Indicateur)
        Dim reader As OleDbDataReader = Nothing
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
            SelectAll = indicList
        Catch e As Exception
            MessageBox.Show(e.Message)
        End Try
        reader.Close()
        cmd.Connection.Close()
    End Function

    Public Function UpdateQuery(indicateur As Indicateur) As Boolean
        Dim Query As String
        UpdateQuery = True
        'Dim er As OleDbDataReader = Nothing
        Query = "UPDATE T_Indicateur SET SeuilHaut = Val_seuilhaut, SeuilBas = Val_seuilBas, Encourslvl = Val_encourslvl, nbPlaqueMax = Val_nbplaquemax, PosteClient = Val_posteclient, PosteFourn = Val_postefourn WHERE CurrentPoste = Val_poste;"
        Try
            With cmd.Parameters
                .AddWithValue("Val_seuilhaut", indicateur.SeuilHaut)
                .AddWithValue("Val_seuilBas", indicateur.SeuilBas)
                .AddWithValue("Val_encourslvl", indicateur.EncoursLvl)
                .AddWithValue("Val_nbplaquemax", indicateur.NbPlaqueMax)
                .AddWithValue("Val_poste", indicateur.Table)
                .AddWithValue("Val_posteclient", indicateur.PosteClient)
                .AddWithValue("Val_postefourn", indicateur.PosteFourn)
            End With
            cmd.CommandText = Query
            cmd.Connection.Open()
            Dim res As Integer
            cmd.ExecuteNonQuery()
            Console.WriteLine("Ligne affectée : " & res)
        Catch e As Exception
            UpdateQuery = False
            MessageBox.Show(e.Message)
        End Try
        With cmd.Parameters
            .RemoveAt("Val_seuilhaut")
            .RemoveAt("Val_seuilBas")
            .RemoveAt("Val_encourslvl")
            .RemoveAt("Val_nbplaquemax")
            .RemoveAt("Val_poste")
            .RemoveAt("Val_posteclient")
            .RemoveAt("Val_postefourn")
        End With
        'er.Close()
        cmd.Connection.Close()
    End Function
End Class
