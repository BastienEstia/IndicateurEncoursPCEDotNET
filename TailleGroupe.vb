Option Explicit On
Imports System.Data.OleDb

Public Class TailleGroupe

    Private Property Id As Integer
    Private Property GroupeId As Integer
    Private Property TailleList As List(Of String)
    Public Property Coef As Double
    Public Property Table As String

    Public Sub New(id As Integer, groupeId As Integer tailleList As List(Of String), coef As Double)
        Me.Id = id
        Me.GroupeId = groupeId
        Me.TailleList = TailleList
        Me.Coef = Coef
        Me.Table = Table
    End Sub

    Public Sub New()

    End Sub

End Class
