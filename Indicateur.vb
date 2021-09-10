Option Explicit On
Imports System.Data.OleDb

Public Class Indicateur
    Public Property Id As Integer

    Public Property Table As String

    Public Property SeuilBas As Integer

    Public Property EncoursLvl As Integer

    Public Property SeuilHaut As Integer

    Public Property NbPlaqueMax As Integer

    Public Property PosteClient As String

    Public Property PosteFourn As String

    Public Sub New()

    End Sub

    Public Sub New(id As Integer, table As String, seuilBas As Integer, encoursLvl As Integer, seuilHaut As Integer, nbPlaqueMax As Integer, PosteClient As String, PosteFourn As String)
        Me.Id = id
        Me.Table = table
        Me.SeuilBas = seuilBas
        Me.EncoursLvl = encoursLvl
        Me.SeuilHaut = seuilHaut
        Me.NbPlaqueMax = nbPlaqueMax
        Me.PosteClient = PosteClient
        Me.PosteFourn = PosteFourn
    End Sub

    Public Sub New(table As String, cmd As OleDbCommand)

    End Sub

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

    Public Function GridLengthBot() As GridLength
        Try
            GridLengthBot = New GridLength(EncoursLvl, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            GridLengthBot = New GridLength()
            Exit Function
        End Try
    End Function

    Public Function GridLengthTop() As GridLength
        Try
            GridLengthTop = New GridLength(NbPlaqueMax - EncoursLvl, GridUnitType.Star)
        Catch e As Exception
            MessageBox.Show(e.Message)
            GridLengthTop = New GridLength()
            Exit Function
        End Try
    End Function

End Class
