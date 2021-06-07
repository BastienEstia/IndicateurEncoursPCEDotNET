Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection
    Public Function ConnexionBDD() As OleDbConnection
        connString = "Provider=Microsoft.ACE.OLEDB.12.0;"
        connString += "Data Source=S:\Public\Lean Exxelia\18 - Mise en flux Pressage Coupe Etalement\MFE-Mise en flux PCE\Indicateur\IndicateurPressageBDD.accdb"
        oleConnection = New OleDbConnection(connString)
        Return oleConnection
    End Function

    Public Function InsertQuery(cdBarre As String, qtePlaque As Double) As Boolean
        Dim query As String
        query = "INSERT INTO T_Encours_Press (Libelle, NbPlaque) VALUES (Val_libelle, Val_nbPlaque)"

        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand(query, con.ConnexionBDD())
        With cmd.Parameters
            .AddWithValue("Val_libelle", cdBarre)
            .AddWithValue("Val_nbPlaque", qtePlaque)
        End With
        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Connection.Close()

        Return True
    End Function

    Public Function TruncateQuery(id As Integer) As Boolean
        Dim query As String
        query = "DELETE * From T_Encours_Press Where id = Val_id"

        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand(query, con.ConnexionBDD())
        With cmd.Parameters
            .AddWithValue("Val_id", id)
        End With

        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Connection.Close()

        Return True
    End Function

    Private Function CountQuery() As Integer
        Dim query As String
        Dim nbof As Integer
        query = "SELECT COUNT(id) From T_Encours_Press"

        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand(query, con.ConnexionBDD())

        cmd.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nbof = reader.GetValue(0)
        End If
        cmd.Connection.Close()

        Return nbof
    End Function

    Public Function SelectIdQuery() As Integer
        Dim query As String
        Dim nOf As Integer
        Dim nbof As Integer
        Dim i As Integer
        i = 0

        Dim con As Connexion = New Connexion()

        nbof = con.CountQuery
        Dim nbOfTab(nbof) As Integer

        query = "SELECT Id FROM T_Encours_Press"
        Dim cmd = New OleDbCommand(query, con.ConnexionBDD())
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

    Public Function SelectNbPlaqueQuery() As List(Of Double)
        Dim listNbPlaque = New List(Of Double)
        Dim query As String
        Dim nbof As Integer
        Dim i As Integer

        i = 0

        Dim con As Connexion = New Connexion()

        nbof = con.CountQuery
        Dim nbPlaqueTab(nbof) As Integer

        query = "SELECT NbPlaque FROM T_Encours_Press"
        Dim cmd = New OleDbCommand(query, con.ConnexionBDD())
        cmd.Connection.Open()

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

End Class
