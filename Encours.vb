Imports System.Data.OleDb

Public Class Encours
    Public Property Id As Integer
    Public Property Libelle As String
    Public Property NbPlaque As Integer
    Public Property NumOF As String
    Public Property Table As String

    Public Sub New(id As Integer, libelle As String, nbPlaque As Integer, numOF As String, table As String)
        Me.Id = id
        Me.Libelle = libelle
        Me.NbPlaque = nbPlaque
        Me.NumOF = numOF
        Me.Table = table
    End Sub

    Public Sub New()

    End Sub

End Class
