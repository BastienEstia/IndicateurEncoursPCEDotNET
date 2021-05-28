Imports System.Data
Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection
    Public Function ConnexionBDD() As OleDbConnection
        connString = "Provider=Microsoft.ACE.OLEDB.12.0;"
        connString += "Data Source=C:\Users\bastien.andres\Documents\IndicateurPressageBDD.accdb"
        oleConnection = New OleDbConnection(connString)
        Return oleConnection
    End Function

    Public Function SendOnlyQuery(chosenQuery As Integer) As Boolean
        Dim query As String
        Dim lastOf As String
        Select Case chosenQuery
            Case 1
                query = "INSERT INTO T_Encours_Press (Libelle, NbPlaque) VALUES (Val_libelle, Val_nbPlaque)"
            Case 2
                query = "TRUNCATE * From T_Encours Where Libelle = '" & lastOf & "'"
        End Select

        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand(query, con.ConnexionBDD())
        Select Case chosenQuery
            Case 1

        End Select
    End Function

End Class
