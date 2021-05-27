Imports System.Data
Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection
    Public Function OuvertureBDD() As OleDbConnection
        connString = "Provider=Microsoft.ACE.OLEDB.12.0;"
        connString += "Data Source=C:\Users\bastien.andres\Documents\IndicateurPressageBDD.accdb"
        oleConnection = New OleDbConnection(connString)
        Return oleConnection
    End Function
End Class
