Option Explicit On
Imports System.Configuration
Imports System.Data.OleDb

Class MainWindow
    Public cdBarre As String
    Public qtePlaque As Double
    Public lastOf As String
    Public nbOf As Double
    Public connexionString As String

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        connexionString = My.Settings.BDDPath
        connexionStringInput.Text = connexionString
        Dim settings As New SettingsWindow
        Call MajTableau()
        SeuilBas.Text = "20"
        SeuilHaut.Text = "50"
        Call MajIndicateur()
    End Sub

    Public Sub MajTableau()
        Dim settingsW As New SettingsWindow
        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table T_Encours_Press. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetT_Encours_PressTableAdapter As IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter = New IndicateurEncoursPCEDotNET.IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter()
        On Error GoTo Handler
        IndicateurPressageBDDDataSetT_Encours_PressTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Press, connexionString)
Handler:
        If (TypeOf Err.GetException() Is System.Data.OleDb.OleDbException) Then
#Disable Warning BC42104
            settingsW.ShowDialog()
#Enable Warning BC42104
        End If

        Dim T_Encours_PressViewSource As CollectionViewSource = CType(Me.FindResource("T_Encours_PressViewSource"), CollectionViewSource)
        T_Encours_PressViewSource.View.MoveCurrentToFirst()

    End Sub

    Public Sub MajIndicateur()
        Dim nbPlaqueTot As Double
        Dim i As Integer = 0
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim listNbPlaque As List(Of Double) = con.SelectNbPlaqueQuery(cmd)
        Dim gridLengthTop As GridLength
        Dim gridLengthBot As GridLength
        Dim colorGLTop As GridLength
        Dim colorGLMid As GridLength
        Dim colorGLBot As GridLength

        colorGLBot = New GridLength(SeuilBas.Text, GridUnitType.Star)
        indicBot.Height = colorGLBot
        colorGLMid = New GridLength(SeuilHaut.Text - SeuilBas.Text, GridUnitType.Star)
        indicMid.Height = colorGLMid
        Dim seuilTopCalcul = 75 - SeuilHaut.Text
        colorGLTop = New GridLength(seuilTopCalcul, GridUnitType.Star)
        indicTop.Height = colorGLTop

        While i < listNbPlaque.Count
            nbPlaqueTot += listNbPlaque(i)
            i += 1
        End While

        gridLengthBot = New GridLength(nbPlaqueTot, GridUnitType.Star)
        curseurBot.Height = gridLengthBot
        gridLengthTop = New GridLength(75 - nbPlaqueTot, GridUnitType.Star)
        curseurTop.Height = gridLengthTop

    End Sub

    Private Sub SaisieMan_click(sender As Object, e As RoutedEventArgs) Handles saisieMan.Click
        cdBarre = cdBarreTB.Text
        qtePlaque = qtePlaqueTB.Text.ToString
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim ret As Boolean = con.InsertQuery(cdBarre, qtePlaque, cmd)
        Call MajTableau()
        Call MajIndicateur()
        lastOf = cdBarre

    End Sub

    Private Sub UndoLastOf_Click(sender As Object, e As RoutedEventArgs) Handles undoLastOf.Click
        Dim id As Integer
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        id = con.SelectIdQuery(cmd)
        Dim ret As Boolean = con.TruncateQuery(id, cmd)
        Call MajTableau()
        Call MajIndicateur()

    End Sub

    Private Sub Seuil_Click(sender As Object, e As RoutedEventArgs) Handles validSeuils.Click
        Call MajIndicateur()
    End Sub

    Private Sub Settings_Click(sender As Object, e As RoutedEventArgs)
        Dim settingsW As New SettingsWindow()
        settingsW.ShowDialog()

    End Sub
End Class
