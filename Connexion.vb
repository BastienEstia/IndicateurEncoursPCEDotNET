Imports System.Data
Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection
    Private oleCommande As OleDbCommand
    Private oleAdapteur As OleDbDataAdapter
    Private dataSet As DataSet

    Public Function OuvertureBDD() As OleDbConnection
        connString = "Provider=Microsoft.ACE.OLDB.12.0;"
        'connString += "Jet OLEDB:Database Password = password"
        'connString += "Data Source=C:\Users\bastien.andres\Documents\BDDTestIndic.mdb"
        connString += "Data Source=C:\Users\bastien.andres\Documents\IndicateurPressageBDD.accdb"
        oleConnection = New OleDbConnection(connString)
        'oleCommande = New OleDbCommand(requete, oleConnection)
        'oleAdapteur = New OleDbDataAdapter(oleCommande)
        'dataSet = New DataSet()
        'oleAdapteur.Fill(dataSet, "Resultat")
        Return oleConnection
    End Function

End Class
