Public Class Encours
    Public Property Id As List(Of Integer)
    Public Property Libelle As List(Of String)
    Public Property NbPlaque As List(Of Integer)
    Public Property NumOF As List(Of String)
    Public Property Table As String

    Public Sub New(id As List(Of Integer), libelle As List(Of String), nbPlaque As List(Of Integer), numOF As List(Of String), table As String)
        Me.Id = id
        Me.Libelle = libelle
        Me.NbPlaque = nbPlaque
        Me.NumOF = numOF
        Me.Table = table
    End Sub

    Public Sub New()

    End Sub

    Public Function MatLibelleNbPlaque() As String(,)
        Dim i As Integer
        MatLibelleNbPlaque = Nothing
        i = 0
        Try
            While i <= Libelle.Count
                MatLibelleNbPlaque(i, 0) = Libelle(i)
                MatLibelleNbPlaque(i, 1) = NbPlaque(i)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function

End Class
