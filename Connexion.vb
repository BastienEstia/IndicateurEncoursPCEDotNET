Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection

    Public Property ConnString1 As String
        Get
            Return connString
        End Get
        Set(value As String)
            connString = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(connString As String, oleConnection As OleDbConnection)
        Me.connString = connString
        Me.oleConnection = oleConnection
    End Sub

    Public Function ConnexionBDD(connexionStringInput As String) As OleDbConnection
        ConnString1 = "Provider=Microsoft.ACE.OLEDB.12.0;"
        ConnString1 += "Data Source=" & connexionStringInput
        oleConnection = New OleDbConnection(ConnString1)
        Return oleConnection
    End Function



    Public Function GetRightConnString(connString As String)
        Dim rightConnString As String
        rightConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & connString & ";Persist Security Info=True;Jet OLEDB:Database Password=password"
        Return rightConnString
    End Function
End Class