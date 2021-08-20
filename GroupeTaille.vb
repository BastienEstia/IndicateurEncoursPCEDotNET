Option Explicit On
Imports System.Data.OleDb

Public Class GroupeTaille

    Public Property Id As Integer
    Public Property Table As String
    Public Property TailleList As List(Of String)
    Public Property Coef As Double
    Public Property Groupe As Integer

    Public Sub New(id As Integer, table As String, tailleList As List(Of String), coef As Double, groupe As Integer)
        Me.Id = id
        Me.Table = table
        Me.TailleList = tailleList
        Me.Coef = coef
        Me.Groupe = groupe
    End Sub

    Public Sub New()

    End Sub

End Class
