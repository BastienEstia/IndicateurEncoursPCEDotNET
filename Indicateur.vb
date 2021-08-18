Option Explicit On
Imports System.Data.OleDb

Public Class Indicateur
    Private Property Id As Integer

    Public Property Table As String

    Public Property SeuilBas As Integer

    Public Property EncoursLvl As Integer

    Public Property SeuilHaut As Integer

    Public Property NbPlaqueMax As Integer

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, table As String, seuilBas As Integer, encoursLvl As Integer, seuilHaut As Integer, nbPlaqueMax As Integer, gridLenghtTop As GridLength, gridLenghtBot As GridLength, colorGLTop As GridLength, colorGLMid As GridLength, colorGLBot As GridLength)
        Me.Id = id
        Me.Table = table
        Me.SeuilBas = seuilBas
        Me.EncoursLvl = encoursLvl
        Me.SeuilHaut = seuilHaut
        Me.NbPlaqueMax = nbPlaqueMax
    End Sub

    Public Sub New(Indicateur As Indicateur)
        Me.Id = Indicateur.Id
        Me.Table = Indicateur.Table
        Me.SeuilBas = Indicateur.SeuilBas
        Me.EncoursLvl = Indicateur.EncoursLvl
        Me.SeuilHaut = Indicateur.SeuilHaut
        Me.NbPlaqueMax = Indicateur.NbPlaqueMax
    End Sub

    Public Function InsertQuery(table As String, seuilHaut As Integer, seuilBas As Integer, encoursLvl As Integer, nbPlaqueMax As Integer, cmd As OleDbCommand) As Boolean
        Dim Query As String
        Query = "INSERT INTO T_Indicateur & (Table, SeuilHaut, SeuilBas, EncoursLvl) VALUES (Val_table, Val_seuilHaut, Val_seuilBas, Val_encoursLvl)"
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

    Public Function SelectAllById(id As Integer, cmd As OleDbCommand) As Indicateur
        Dim query
        Dim indic As New Indicateur()
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
                indic.SeuilBas = reader("SeuilBas")
                indic.SeuilHaut = reader("SeuilHaut")
                indic.EncoursLvl = reader("EncoursLvl")
                indic.Table = reader("Table")
                indic.Id = reader("Id")
                indic.NbPlaqueMax = reader("NbPlaqueMax")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAllById = indic
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAllById = indic
    End Function

    Public Function SelectAllByTable(table As String, cmd As OleDbCommand)
        Dim query
        Dim indic As New Indicateur()
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_Indicateur Where Table = Val_table"
        Try
            With cmd.Parameters
                .AddWithValue("Val_Table", table)
            End With
            cmd.CommandText = query
            cmd.Connection.Open()
            reader = cmd.ExecuteReader()
            While reader.Read()
                indic.SeuilBas = reader("SeuilBas")
                indic.SeuilHaut = reader("SeuilHaut")
                indic.EncoursLvl = reader("EncoursLvl")
                indic.Table = reader("Table")
                indic.Id = reader("Id")
            End While
        Catch e As Exception
            MessageBox.Show(e.Message)
            SelectAllByTable = indic
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAllByTable = indic
    End Function

    Public Function SelectAllIndic(cmd As OleDbCommand) As List(Of Indicateur)
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
            SelectAllIndic = indicList
            Exit Function
        End Try
        cmd.Connection.Close()
        SelectAllIndic = indicList
    End Function

    Public Function ColorGLBot() As GridLength
        Try
            ColorGLBot = New GridLength(SeuilBas, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            ColorGLBot = New GridLength()
            Exit Function
        End Try
    End Function

    Public Function ColorGLMid() As GridLength
        Try
            ColorGLMid = New GridLength(SeuilHaut - SeuilBas, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            ColorGLMid = New GridLength()
            Exit Function
        End Try
    End Function

    Public Function ColorGLTop() As GridLength
        Try
            ColorGLTop = New GridLength(NbPlaqueMax - SeuilHaut, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            ColorGLTop = New GridLength()
            Exit Function
        End Try
    End Function

    Public Function GridLenghtBot() As GridLength
        Try
            GridLenghtBot = New GridLength(EncoursLvl, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            GridLenghtBot = New GridLength()
            Exit Function
        End Try
    End Function

    Public Function GridLenghtTop() As GridLength
        Try
            GridLenghtTop = New GridLength(NbPlaqueMax - EncoursLvl, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            GridLenghtTop = New GridLength()
            Exit Function
        End Try
    End Function
End Class
