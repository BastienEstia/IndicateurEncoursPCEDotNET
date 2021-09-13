Option Explicit On
Imports System.Collections.Specialized
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

    Public Function GetTrueEncoursLvl() As Double
        Dim nbPlaqueTot As Double
        Dim i As Integer = 0
        Dim j As Integer = 0
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDPath)
        }
        Dim t_encoursTableSelected As New T_Encours(cmd, Table)
        Dim encoursList As List(Of Encours) = t_encoursTableSelected.SelectAll()
        Dim t_groupeTaille As New T_GroupeTaille(cmd)
        Dim groupeTailleList As List(Of GroupeTaille) = t_groupeTaille.SelectAllByTable(Table)
        Dim mat As String(,) = MatLibelleNbPlaque(encoursList)
        While i <= mat.GetLength(0) - 1
            While j <= groupeTailleList.Count - 1
                If groupeTailleList(j).TailleList.Contains(mat(i, 0)) Then
                    nbPlaqueTot += groupeTailleList(j).Coef * mat(i, 1)
                    Exit While
                End If
                j += 1
            End While
            i += 1
        End While
        EncoursLvl = nbPlaqueTot
        GetTrueEncoursLvl = EncoursLvl
    End Function
    Function LibelleToTaillePiece2(libelle As String, tailles As StringCollection) As String
        Dim n As Integer
        Dim i As Integer
        LibelleToTaillePiece2 = Nothing
        i = 0
        Try
            While n = 0
                n = InStr(3, libelle, tailles(i))
                LibelleToTaillePiece2 = tailles(i)
                i = i + 1
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function
    Public Function MatLibelleNbPlaque(encoursList As List(Of Encours)) As String(,)
        Dim i As Integer
        MatLibelleNbPlaque = Nothing
        Dim mat = New String(encoursList.Count - 1, 1) {}
        i = 0
        Try
            While i <= encoursList.Count - 1
                mat(i, 0) = LibelleToTaillePiece2(encoursList(i).Libelle, MySettings.Default.TailleList)
                mat(i, 1) = encoursList(i).NbPlaque.ToString()
                i += 1
            End While
            Return mat
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function
End Class
