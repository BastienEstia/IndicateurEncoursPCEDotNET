﻿Option Explicit On
Imports System.Collections.Specialized
Imports System.Data
Imports System.Data.OleDb

Public Class MainWindow
    Public libelle As String
    Public qtePlaque As Double
    Public numOf As String
    Public lastOf As String
    Public nbOf As Double
    Public connexionString As String

    Public coef1 As Double
    Public coef2 As Double
    Public coef3 As Double
    Public coef4 As Double
    Public coef5 As Double

    Public tailleGr1 As New List(Of Object)
    Public tailleGr2 As New List(Of Object)
    Public tailleGr3 As New List(Of Object)
    Public tailleGr4 As New List(Of Object)
    Public tailleGr5 As New List(Of Object)

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        connexionString = MySettings.Default.BDDPath
        connexionStringInput.Text = connexionString
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim indicateur As Indicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        TB_Table.Text = MySettings.Default.TableSelected
        TB_PlMax.Text = MySettings.Default.nbPlaqueMax
        SeuilBas.Text = indicateur.SeuilBas
        SeuilHaut.Text = indicateur.SeuilHaut
        TB_Table_Fournisseur.Text = indicateur.PosteFourn
        TB_Table_Client.Text = indicateur.PosteClient

        Dim settings As New SettingsWindow
        Call MajTableau()
        Call MajIndicateur()
    End Sub

    Public Sub MajTableau()
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_encours As New T_Encours(cmd, MySettings.Default.TableSelected)
        Dim encoursList As List(Of Encours) = t_encours.SelectAll()
        Dim dt As New DataTable
        Dim ds As New DataSet
        dt.Load(t_encours.SelectAllForDGV)
        ds.Tables.Add(dt)
        mainDataGrid.ItemsSource = dt.DefaultView

    End Sub

    Public Sub MajTableau1()
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
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim TableSelectedIndicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        TableSelectedIndicateur.GetTrueEncoursLvl()
        indicBot.Height = TableSelectedIndicateur.ColorGLBot
        indicMid.Height = TableSelectedIndicateur.ColorGLMid
        indicTop.Height = TableSelectedIndicateur.ColorGLTop
        TB_PlMax.Text = TableSelectedIndicateur.NbPlaqueMax.ToString()

        curseurBot.Height = TableSelectedIndicateur.GridLengthBot
        curseurTop.Height = TableSelectedIndicateur.GridLengthTop
        indicLabel.Content = MySettings.Default.TableSelected

        Dim rowDefinitionIndicTopList As New List(Of RowDefinition) From {
            indicTop1,
            indicTop2,
            indicTop3
        }
        Dim rowDefinitionIndicMidList As New List(Of RowDefinition) From {
            indicMid1,
            indicMid2,
            indicMid3
        }
        Dim rowDefinitionIndicBotList As New List(Of RowDefinition) From {
            indicBot1,
            indicBot2,
            indicBot3
        }
        Dim rowDefinitionCursorTopList As New List(Of RowDefinition) From {
            curseurTop1,
            curseurTop2,
            curseurTop3
        }
        Dim rowDefinitionCursorBotList As New List(Of RowDefinition) From {
            curseurBot1,
            curseurBot2,
            curseurBot3
        }
        Dim label1 As New Label
        Dim label2 As New Label
        Dim label3 As New Label
        label1 = indic1Label
        label2 = indic2Label
        label3 = indic3Label

        Dim indicLabelList As New List(Of Label) From {
            label1,
            label2,
            label3
        }
        Dim indicateursList As New List(Of Indicateur)
        Dim j As Integer
        j = 0
        For i = 1 To t_indicateur.SelectCountQuery()
            Dim indicateurExterne As Indicateur = t_indicateur.SelectAllById(i)
            If Not indicateurExterne.Table = MySettings.Default.TableSelected Then
                With indicLabelList(j)
                    .HorizontalContentAlignment = HorizontalAlignment.Center
                    .Content = indicateurExterne.Table
                End With
                indicateurExterne.GetTrueEncoursLvl()
                rowDefinitionCursorTopList(j).Height = indicateurExterne.GridLengthTop
                rowDefinitionCursorBotList(j).Height = indicateurExterne.GridLengthBot
                rowDefinitionIndicTopList(j).Height = indicateurExterne.ColorGLTop
                rowDefinitionIndicMidList(j).Height = indicateurExterne.ColorGLMid
                rowDefinitionIndicBotList(j).Height = indicateurExterne.ColorGLBot
                j += 1
            End If
        Next i

    End Sub

    Private Sub SaisieMan_click(sender As Object, e As RoutedEventArgs) Handles saisieMan.Click

        numOf = numOfTB.Text
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_encoursTableSelected As New T_Encours(cmd, MySettings.Default.TableSelected)
        Dim t_encoursTableFournisseur As New T_Encours(cmd, MySettings.Default.TableFournisseur)
        Dim t_encoursTableTemp As New T_Encours(cmd, MySettings.Default.TableTemp)
        Dim encoursFournisseur As Encours
        Try
            encoursFournisseur = t_encoursTableFournisseur.SelectAllByNumOF(numOf)
        Catch ex As Exception
            encoursFournisseur = Nothing
        End Try
        Dim encours As New Encours
        If encoursFournisseur Is Nothing Then
            Dim encoursTemp As Encours = t_encoursTableTemp.SelectAllByNumOF(numOf)
            If encoursTemp Is Nothing Then
                encours.NbPlaque = CInt(qtePlaqueTB.Text)
                encours.Libelle = libelleTB.Text
                encours.NumOF = numOfTB.Text
                encours.Table = MySettings.Default.TableSelected
                t_encoursTableSelected.InsertQuery(encours)
            Else
                encours.Libelle = encoursTemp.Libelle
                encours.NbPlaque = encoursTemp.NbPlaque
                encours.NumOF = numOf
                encours.Table = MySettings.Default.TableSelected
                t_encoursTableSelected.InsertQuery(encours)
            End If
        Else
            t_encoursTableFournisseur.TruncateQuery(numOf)
            t_encoursTableSelected.InsertQuery(encoursFournisseur)
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
        Dim lastOf As Encours
        Dim listEncours As List(Of Encours)

        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_encoursTableSelected As New T_Encours(cmd, MySettings.Default.TableSelected)
        listEncours = t_encoursTableSelected.SelectAll
        lastOf = listEncours(listEncours.Count - 1)
        t_encoursTableSelected.TruncateQuery(lastOf.NumOF)
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
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim currentIndicateur As Indicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        Dim newIndicateur As New Indicateur()
        Try
            newIndicateur = currentIndicateur
            newIndicateur.SeuilHaut = SeuilHaut.Text
            newIndicateur.SeuilBas = SeuilBas.Text
            t_indicateur.UpdateQuery(newIndicateur)
            Call MajIndicateur()
        Catch ex As Exception
            result = MessageBox.Show(messageTextBox, caption, button, icon, MessageBoxResult.Yes)
            Exit Sub
        End Try
        Call MajTableau()
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

    Private Sub Coef_Click(sender As Object, e As RoutedEventArgs)
        Dim coefConfigW As New CoefConfigWindow()
        coefConfigW.ShowDialog()
        Call MajIndicateur()
    End Sub

    'Private Sub dropTableBtn_Click(sender As Object, e As RoutedEventArgs) Handles dropTableBtn.Click
    '    Dim con As New Connexion
    '    Dim cmd As New OleDbCommand With {
    '        .Connection = con.ConnexionBDD(connexionString)
    '    }
    '    con.DropTableQuery(cmd)

    'End Sub

    Public Function LibelleToTaillePiece(libelle As String) As String
        Dim libelleTab() As Char
        Dim taillePiece As String
        libelleTab = libelle.ToArray
        Dim caracSpe As Char
        caracSpe = libelleTab(6)
        Try
            Dim dbl As Double
            Dim str As Double
            If libelleTab(3) = "H" Then
                taillePiece = libelleTab(3) & libelleTab(4) & libelleTab(5) & libelleTab(6) & libelleTab(7)
            ElseIf libelleTab(3) = "R" Then
                str = libelleTab(6).ToString
                dbl = CDbl(str)
                taillePiece = libelleTab(3) & libelleTab(4) & libelleTab(5) & libelleTab(6) & libelleTab(7)
            Else
                taillePiece = libelleTab(3) & libelleTab(4) & libelleTab(5)
            End If

        Catch ex As Exception
            taillePiece = libelleTab(3) & libelleTab(4) & libelleTab(5)
        End Try

        Return taillePiece
    End Function



    Private Sub deleteOF_Click(sender As Object, e As RoutedEventArgs) Handles deleteOF.Click
        Dim deleteW As New DeleteWindow()
        deleteW.ShowDialog()
        Call MajTableau()
        Call MajIndicateur()
    End Sub
End Class
