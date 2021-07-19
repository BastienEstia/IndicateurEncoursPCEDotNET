Option Explicit On
Imports System.Configuration
Imports System.Data.OleDb

Class MainWindow
    Public libelle As String
    Public qtePlaque As Double
    Public numOf As String
    Public lastOf As String
    Public nbOf As Double
    Public connexionString As String

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        connexionString = MySettings.Default.BDDPath
        connexionStringInput.Text = connexionString
        TB_Table.Text = MySettings.Default.TableSelected
        TB_PlMax.Text = MySettings.Default.nbPlaqueMax
        SeuilBas.Text = MySettings.Default.seuilBas
        SeuilHaut.Text = MySettings.Default.seuilHaut

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
        Dim IndicateurPressageBDDDataSetT_Encours_CoupeTableAdapter As IndicateurPressageBDDDataSetTableAdapters.T_Encours_CoupeTableAdapter = New IndicateurEncoursPCEDotNET.IndicateurPressageBDDDataSetTableAdapters.T_Encours_CoupeTableAdapter()

        Select Case MySettings.Default.TableSelected
            Case "Press"
                Try
                    mainGrid.DataContext = IndicateurPressageBDDDataSet.T_Encours_Press
                    IndicateurPressageBDDDataSetT_Encours_PressTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Press, connexionString)
                Catch ex As Exception
                    settingsW.ShowDialog()
                End Try
                CType(Me.FindResource("T_Encours_PressViewSource"), CollectionViewSource).View.MoveCurrentToFirst()
            Case "Coupe"
                Try
                    mainGrid.DataContext = IndicateurPressageBDDDataSet.T_Encours_Coupe
                    IndicateurPressageBDDDataSetT_Encours_CoupeTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Coupe, connexionString)
                Catch
                    settingsW.ShowDialog()
                End Try
                    CType(Me.FindResource("T_Encours_CoupeViewSource"), CollectionViewSource).View.MoveCurrentToFirst()
        End Select
    End Sub

    Public Sub MajIndicateur()
        Dim nbPlaqueTot As Double
        Dim nbPLaqueMax As Double
        nbPLaqueMax = MySettings.Default.nbPlaqueMax
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
        Dim seuilTopCalcul = nbPLaqueMax - SeuilHaut.Text
        colorGLTop = New GridLength(seuilTopCalcul, GridUnitType.Star)
        indicTop.Height = colorGLTop

        While i < listNbPlaque.Count
            nbPlaqueTot += listNbPlaque(i)
            i += 1
        End While

        gridLengthBot = New GridLength(nbPlaqueTot, GridUnitType.Star)
        curseurBot.Height = gridLengthBot
        gridLengthTop = New GridLength(nbPLaqueMax - nbPlaqueTot, GridUnitType.Star)
        curseurTop.Height = gridLengthTop

    End Sub

    Private Sub SaisieMan_click(sender As Object, e As RoutedEventArgs) Handles saisieMan.Click

        numOf = numOfTB.Text
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }

        Dim exsitResults As Integer = con.SelectCountForExistanceQuery(cmd, numOf)
        If exsitResults = 1 Then
            Dim res(3) As String
            res = con.SelectAllWithNumOFQuery(numOf, cmd)
            Dim retT As Boolean = con.TruncateQuery(numOf, MySettings.Default.TableFournisseur, cmd)
            Dim cmd2 As New OleDbCommand With {
                .Connection = con.ConnexionBDD(connexionString)
            }
            Dim retI As Boolean = con.InsertQuery(res(0), res(1), res(2), MySettings.Default.TableSelected, cmd2)
        Else
            qtePlaque = qtePlaqueTB.Text.ToString
            libelle = libelleTB.Text
            Dim retI As Boolean = con.InsertQuery(libelle, qtePlaque, numOf, MySettings.Default.TableSelected, cmd)
        End If

        Call MajTableau()
        Call MajIndicateur()
        lastOf = numOf

    End Sub

    Public Sub MajTableFournisseur()
        Dim tableFournisseur As String
        tableFournisseur = MySettings.Default.TableFournisseur


    End Sub

    Private Sub UndoLastOf_Click(sender As Object, e As RoutedEventArgs) Handles undoLastOf.Click
        Dim con As New Connexion()
        Dim id As Integer
        Dim lastOf As String
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        id = con.SelectLastIdQuery(cmd)
        lastOf = con.SelectLastNumOf(id, cmd)
        Dim ret As Boolean = con.TruncateQuery(lastOf, MySettings.Default.TableSelected, cmd)
        Call MajTableau()
        Call MajIndicateur()

    End Sub

    Private Sub Seuil_Click(sender As Object, e As RoutedEventArgs) Handles validSeuils.Click
        Dim messageTextBox As String
        messageTextBox = "Attention seuil vide, trop haut ou trop bas !"
        Dim caption As String
        caption = "Error"
        Dim button As MessageBoxButton
        button = MessageBoxButton.OK
        Dim icon As MessageBoxImage
        icon = MessageBoxImage.Warning
        Dim result As MessageBoxResult
        Try
            Call MajIndicateur()
        Catch ex As Exception
            result = MessageBox.Show(messageTextBox, caption, button, icon, MessageBoxResult.Yes)
        End Try


    End Sub

    Private Sub Settings_Click(sender As Object, e As RoutedEventArgs)
        Dim settingsW As New SettingsWindow()
        settingsW.ShowDialog()
        TB_Table.Text = MySettings.Default.TableSelected

        connexionString = MySettings.Default.BDDPath
        connexionStringInput.Text = connexionString

        Call MajIndicateur()
        Call MajTableau()
    End Sub

    Private Sub Window_Closing(sender As Object, e As ComponentModel.CancelEventArgs)
        System.Windows.Application.Current.Shutdown()
    End Sub

    'Private Sub dropTableBtn_Click(sender As Object, e As RoutedEventArgs) Handles dropTableBtn.Click
    '    Dim con As New Connexion
    '    Dim cmd As New OleDbCommand With {
    '        .Connection = con.ConnexionBDD(connexionString)
    '    }
    '    con.DropTableQuery(cmd)

    'End Sub
End Class
