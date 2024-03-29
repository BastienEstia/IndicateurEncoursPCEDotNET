﻿Option Explicit On
Imports System.Data.OleDb

Public Class SettingsWindow
    Private filename As String

    Private Sub SettingsW_Loaded(sender As Object, e As RoutedEventArgs) Handles SettingsW.Loaded
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDPath)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim indicateur As Indicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        TB_nbPlaqueMax.Text = indicateur.NbPlaqueMax.ToString
        Dim cbItem As New ComboBoxItem
        filename = MySettings.Default.BDDPath
        BDDLocation_TB.Text = MySettings.Default.BDDPath
        TB_TableFournisseur.Text = indicateur.PosteFourn
        TB_TableClient.Text = indicateur.PosteClient
        Table_ComboBox.SelectedValue = MySettings.Default.TableSelected
    End Sub

    Private Sub BDDLocation_Click(sender As Object, e As RoutedEventArgs)
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog()

        dlg.DefaultExt = "Access Databases|*.accdb"
        dlg.Filter = "Access Databases|*.accdb"

        Dim result As Boolean? = dlg.ShowDialog()

        If result = True Then
            filename = dlg.FileName
            BDDLocation_TB.Text = filename
        End If

    End Sub

    Public Function GetBDDLocation() As String
        Return filename
    End Function

    Private Sub SettingsOk_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsOk_Button.Click
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(GetBDDLocation)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim indicateur As Indicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        indicateur.NbPlaqueMax = CInt(TB_nbPlaqueMax.Text)
        t_indicateur.UpdateQuery(indicateur)
        MySettings.Default.BDDConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & GetBDDLocation() & ";Persist Security Info=True;Jet OLEDB:Database Password=password"
        MySettings.Default.BDDPath = GetBDDLocation()
        MySettings.Default.TableFournisseur = TB_TableFournisseur.Text
        MySettings.Default.TableClient = TB_TableClient.Text
        MySettings.Default.Save()
        BDDLocation_TB.Text = GetBDDLocation()
        Close()
    End Sub

    Private Sub Table_ComboBox_Loaded(sender As Object, e As RoutedEventArgs) Handles Table_ComboBox.Loaded
        Dim listeTable As New List(Of String)
        For Each table As String In MySettings.Default.TableList
            listeTable.Add(Split(table, "_")(2))
        Next table
        Table_ComboBox.ItemsSource = listeTable
    End Sub

    Private Sub Table_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles Table_ComboBox.SelectionChanged
        Dim selectedItem As String
        sender = CType(sender, ComboBox)
        selectedItem = sender.SelectedItem
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(GetBDDLocation)
        }
        Dim t_indicateur As New T_Indicateur(cmd)
        Dim indicateur As Indicateur = t_indicateur.SelectAllByTable(selectedItem)
        TB_nbPlaqueMax.Text = indicateur.NbPlaqueMax.ToString
        TB_TableClient.Text = indicateur.PosteClient
        TB_TableFournisseur.Text = indicateur.PosteFourn
        MySettings.Default.TableSelected = selectedItem
        MySettings.Default.Save()
    End Sub

    'Private Sub BDDLocation_TB_Copy_TextChanged(sender As Object, e As TextChangedEventArgs) Handles TB_nbPlaqueMax.TextChanged
    '    Dim nbPlaqueMax As Double
    '    sender = CType(sender, TextBox)
    '    nbPlaqueMax = sender.Text
    '    MySettings.Default.nbPlaqueMax = nbPlaqueMax
    '    MySettings.Default.Save()
    'End Sub

    Private Sub SettingsBack_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsBack_Button.Click
        DialogResult = False
    End Sub
End Class

