Option Explicit On
Imports System.Collections.Specialized
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
        TB_Table_Fournisseur.Text = MySettings.Default.TableFournisseur
        TB_Table_Client.Text = MySettings.Default.TableClient

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
        Dim j As Integer = 0
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim cmd1 As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim t_encoursTableSelected As New T_Encours(cmd, MySettings.Default.TableSelected)
        Dim encoursList As List(Of Encours) = t_encoursTableSelected.SelectAll()
        Dim t_groupeTaille As New T_GroupeTaille(cmd)
        Dim groupeTailleList As List(Of GroupeTaille) = t_groupeTaille.SelectAllByTable(MySettings.Default.TableSelected)
        Dim mat As String(,) = MatLibelleNbPlaque(encoursList)
        Dim t_indicateur As New T_Indicateur(cmd1)
        Dim TableSelectedIndicateur = t_indicateur.SelectAllByTable(MySettings.Default.TableSelected)
        indicBot.Height = TableSelectedIndicateur.ColorGLBot
        indicMid.Height = TableSelectedIndicateur.ColorGLMid
        indicTop.Height = TableSelectedIndicateur.ColorGLTop
        While i < mat.GetLength(0) - 1
            While j < groupeTailleList.Count
                If groupeTailleList(j).TailleList.Contains(mat(i, 0)) Then
                    nbPlaqueTot += groupeTailleList(j).Coef * mat(i, 1)
                End If
                j += 1
            End While
            i += 1
        End While
        TableSelectedIndicateur.EncoursLvl = nbPlaqueTot
        curseurBot.Height = TableSelectedIndicateur.GridLengthBot
        curseurTop.Height = TableSelectedIndicateur.GridLengthTop
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
        Dim encoursFournisseur As Encours = t_encoursTableFournisseur.SelectAllByNumOF(numOf)

        Dim encours As New Encours
        If encoursFournisseur Is Nothing Then
            Dim encoursTemp As Encours = t_encoursTableTemp.SelectAllByNumOF(numOf)
            If encoursTemp Is Nothing Then
                encours.NbPlaque = qtePlaqueTB.Text.ToString
                encours.Libelle = libelleTB.Text
                t_encoursTableSelected.InsertQuery(encours)
            Else
                encours.Libelle = encoursTemp.Libelle
                encours.NbPlaque = encoursTemp.NbPlaque
                t_encoursTableSelected.InsertQuery(encours)
            End If
        Else
            t_encoursTableFournisseur.TruncateQuery(numOf)
            t_encoursTableSelected.InsertQuery(encours)
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
        Dim t_encoursTableSelected As New T_Encours
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        listEncours = t_encoursTableSelected.SelectAll
        lastOf = listEncours(-1)
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
        Try
            Call MajIndicateur()
        Catch ex As Exception
            result = MessageBox.Show(messageTextBox, caption, button, icon, MessageBoxResult.Yes)
        End Try

        Call MajTableau()
        Call MajIndicateur()
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
