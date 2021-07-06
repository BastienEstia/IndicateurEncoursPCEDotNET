Option Explicit On
Imports System.Data.OleDb

Class MainWindow
    Public cdBarre As String
    Public qtePlaque As Double
    Public lastOf As String
    Public nbOf As Double
    Public connexionString

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        Dim settings As SettingsWindow = New SettingsWindow()
        connexionString = settings.getBDDLocation()
        SeuilBas.Text = "20"
        SeuilHaut.Text = "50"
        While Not (My.Computer.FileSystem.FileExists(connexionString))
            settings.Show()

        End While
        Call MajTableau()
        Call MajIndicateur()
    End Sub

    Private Sub MajTableau()

        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table T_Encours_Press. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetT_Encours_PressTableAdapter As IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter = New IndicateurEncoursPCEDotNET.IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter()
        IndicateurPressageBDDDataSetT_Encours_PressTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Press, connexionStringInput.Text)
        Dim T_Encours_PressViewSource As CollectionViewSource = CType(Me.FindResource("T_Encours_PressViewSource"), CollectionViewSource)
        T_Encours_PressViewSource.View.MoveCurrentToFirst()

    End Sub

    Private Sub MajIndicateur()
        Dim nbPlaqueTot As Double
        Dim i As Integer = 0
        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = con.ConnexionBDD(connexionStringInput.Text)
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

    Private Sub SaisiMan_click(sender As Object, e As RoutedEventArgs) Handles saisieMan.Click
        cdBarre = cdBarreTB.Text
        qtePlaque = qtePlaqueTB.Text.ToString
        Dim con As Connexion = New Connexion()
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = con.ConnexionBDD(connexionStringInput.Text)
        con.InsertQuery(cdBarre, qtePlaque, cmd)
        Call MajTableau()
        Call MajIndicateur()
        lastOf = cdBarre
    End Sub

    Private Sub UndoLastOf_Click(sender As Object, e As RoutedEventArgs) Handles undoLastOf.Click
        Dim con As Connexion = New Connexion()
        Dim id As Integer
        Dim cmd As OleDbCommand = New OleDbCommand()
        cmd.Connection = con.ConnexionBDD(connexionStringInput.Text)
        id = con.SelectIdQuery(cmd)
        con.TruncateQuery(id, cmd)
        Call MajTableau()
        Call MajIndicateur()

    End Sub

    Private Sub Seuil_Click(sender As Object, e As RoutedEventArgs) Handles validSeuils.Click
        Call MajIndicateur()
    End Sub

    Private Sub Settings_Click(sender As Object, e As RoutedEventArgs) Handles ConfigurationButton.Click
        Dim settings As SettingsWindow = New SettingsWindow()
        settings.Show()
    End Sub
End Class
