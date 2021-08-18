Option Explicit On
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
        TB_Table.Text = MySettings.Default.TableSelected
        TB_PlMax.Text = MySettings.Default.nbPlaqueMax
        SeuilBas.Text = MySettings.Default.seuilBas
        SeuilHaut.Text = MySettings.Default.seuilHaut
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
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(connexionString)
        }
        Dim nbPlaqueLibelleMat(,) As String = con.SelectNbPlaqueQuery(cmd)
        Dim gridLengthTop As GridLength
        Dim gridLengthBot As GridLength
        Dim colorGLTop As GridLength
        Dim colorGLMid As GridLength
        Dim colorGLBot As GridLength
        Dim typePieceTab As String()
        Dim typePiece As String

        Dim TableSelectedIndicateur As New Indicateur(TableSelectedIndicateur.SelectAllByTable(MySettings.Default.TableSelected, cmd))


        coef1 = MySettings.Default.coef1
        coef2 = MySettings.Default.coef2
        coef3 = MySettings.Default.coef3
        coef4 = MySettings.Default.coef4
        coef5 = MySettings.Default.coef5

        While i <= MySettings.Default.CCBGr1.Count - 1
            Dim objGr = MySettings.Default.CCBGr1(i)
            tailleGr1.Add(objGr)
            i += 1
        End While
        i = 0
        While i <= MySettings.Default.CCBGr2.Count - 1
            Dim objGr = MySettings.Default.CCBGr2(i)
            tailleGr2.Add(objGr)
            i += 1
        End While
        i = 0
        While i <= MySettings.Default.CCBGr3.Count - 1
            Dim objGr = MySettings.Default.CCBGr3(i)
            tailleGr3.Add(objGr)
            i += 1
        End While
        i = 0
        While i <= MySettings.Default.CCBGr4.Count - 1
            Dim objGr = MySettings.Default.CCBGr4(i)
            tailleGr4.Add(objGr)
            i += 1
        End While
        i = 0
        While i <= MySettings.Default.CCBGr5.Count - 1
            Dim objGr = MySettings.Default.CCBGr5(i)
            tailleGr5.Add(objGr)
            i += 1
        End While

        colorGLBot = New GridLength(SeuilBas.Text, GridUnitType.Star)
        indicBot.Height = colorGLBot
        colorGLMid = New GridLength(SeuilHaut.Text - SeuilBas.Text, GridUnitType.Star)
        indicMid.Height = colorGLMid
        Dim seuilTopCalcul = nbPLaqueMax - SeuilHaut.Text
        colorGLTop = New GridLength(seuilTopCalcul, GridUnitType.Star)
        indicTop.Height = colorGLTop

        i = 0
        'If typePiece <> "" Then
        While i < nbPlaqueLibelleMat.GetLength(0) - 1

            typePiece = LibelleToTaillePiece(nbPlaqueLibelleMat(i, 1))
            'Select Case typePiece
            '    Case Contains()
            '        nbPlaqueTot += coef1 * nbPlaqueLibelleMat(i, 0)
            'End Select
            If tailleGr1.Contains(typePiece) Then
                nbPlaqueTot += coef1 * nbPlaqueLibelleMat(i, 0)
            ElseIf tailleGr2.Contains(typePiece) Then
                nbPlaqueTot += coef2 * nbPlaqueLibelleMat(i, 0)
            ElseIf tailleGr3.Contains(typePiece) Then
                nbPlaqueTot += coef3 * nbPlaqueLibelleMat(i, 0)
            ElseIf tailleGr4.Contains(typePiece) Then
                nbPlaqueTot += coef4 * nbPlaqueLibelleMat(i, 0)
            ElseIf tailleGr5.Contains(typePiece) Then
                nbPlaqueTot += coef5 * nbPlaqueLibelleMat(i, 0)
            End If
            i += 1
        End While
        'End If

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
            Dim res As String() = con.SelectAllWithNumOFQuery(numOf, cmd)
            Dim retT As Boolean = con.TruncateQuery(numOf, MySettings.Default.TableFournisseur, cmd)
            Dim cmd2 As New OleDbCommand With {
                .Connection = con.ConnexionBDD(connexionString)
            }
            Dim retI As Boolean = con.InsertQuery(res(0), res(1), res(2), MySettings.Default.TableSelected, cmd2)
        ElseIf qtePlaqueTB.Text = "" Then
            Dim res As String()
            res = con.SelectAllWithNumOFInTempTableQuery(numOf, cmd)
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
End Class
