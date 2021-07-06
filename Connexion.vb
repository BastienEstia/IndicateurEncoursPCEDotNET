Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection
    Public Function ConnexionBDD(connexionStringInput As String) As OleDbConnection
        connString = "Provider=Microsoft.ACE.OLEDB.12.0;"
        connString += "Data Source=" & connexionStringInput
        oleConnection = New OleDbConnection(connString)
        Return oleConnection
    End Function

    Public Function GetConnString()
        Return connString
    End Function

    Public Function SetConnString(connString As String)
        Me.connString = connString
    End Function

    Public Function GetRightConnString(connString As String)
        Dim rightConnString As String
        rightConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & connString & ";Persist Security Info=True;Jet OLEDB:Database Password=password"
        Return rightConnString
    End Function

    Public Function InsertQuery(cdBarre As String, qtePlaque As Double, cmd As OleDbCommand) As Boolean
        Dim query As String
        query = "INSERT INTO T_Encours_Press (Libelle, NbPlaque) VALUES (Val_libelle, Val_nbPlaque)"
        cmd.CommandText = query
        With cmd.Parameters
            .AddWithValue("Val_libelle", cdBarre)
            .AddWithValue("Val_nbPlaque", qtePlaque)
        End With
        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Connection.Close()

        Return True
    End Function

    Public Function TruncateQuery(id As Integer, cmd As OleDbCommand) As Boolean
        Dim query As String
        query = "DELETE * From T_Encours_Press Where id = Val_id"
        cmd.CommandText = query


        With cmd.Parameters
            .AddWithValue("Val_id", id)
        End With

        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Connection.Close()

        Return True
    End Function

    Private Function CountQuery(cmd As OleDbCommand) As Integer
        Dim query As String
        Dim nbof As Integer
        query = "SELECT COUNT(id) From T_Encours_Press"
        cmd.CommandText = query
        cmd.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nbof = reader.GetValue(0)
        End If
        cmd.Connection.Close()

        Return nbof
    End Function

    Public Function SelectIdQuery(cmd As OleDbCommand) As Integer
        Dim query As String
        Dim nOf As Integer
        Dim nbof As Integer
        Dim i As Integer
        i = 0

        Dim con As Connexion = New Connexion()
        nbof = con.CountQuery(cmd)
        Dim nbOfTab(nbof) As Integer

        query = "SELECT Id FROM T_Encours_Press"
        Dim cmd1 = cmd
        cmd1.CommandText = query
        cmd.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        While reader.Read()
            nbOfTab(i) = (reader("id"))
            i += 1
        End While

        reader.Close()
        cmd.Connection.Close()
        nOf = nbOfTab.Max

        Return nOf
    End Function

    Public Function SelectNbPlaqueQuery(cmd As OleDbCommand) As List(Of Double)
        Dim listNbPlaque = New List(Of Double)
        Dim query As String
        Dim nbof As Integer
        Dim i As Integer

        i = 0

        Dim con As Connexion = New Connexion()

        nbof = con.CountQuery(cmd)
        Dim nbPlaqueTab(nbof) As Integer

        query = "SELECT NbPlaque FROM T_Encours_Press"
        Dim cmd1 = cmd
        cmd.CommandText = query
        cmd1.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        While reader.Read()
            'nbPlaqueTab(i) = (reader("NbPlaque"))
            listNbPlaque.Add(reader("NbPlaque"))
            i += 1
        End While

        reader.Close()

        Return listNbPlaque
    End Function

    Public Function SelectCountTableQuery(cmd As OleDbCommand) As Array
        Dim query As String
        Dim nbof As Array
        query = "SELECT COUNT(id) From * WHERE id = 0"
        cmd.CommandText = query
        cmd.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nbof = reader.GetValue(0)
            cmd.Connection.Close()
        End If

        Return nbof
    End Function

End Class
