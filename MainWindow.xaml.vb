Option Explicit On
Imports System.Data
Imports System.Data.Linq
Imports System.Data.OleDb
Imports LinqToDB
Imports LinqToDB.Reflection.Methods.LinqToDB

Class MainWindow
    Public cdBarre As String
    Public qtePlaque As Double
    Public lastOf As String
    Public nbOf As Double

    Private Sub majIndicateur(choixQuery As Integer)

        Dim countOfQuery As String
        countOfQuery = "SELECT COUNT(*) FROM T_Encours_Press"
        Dim con As Connexion = New Connexion()

        Dim cmd = New OleDbCommand(countOfQuery, con.ConnexionBDD())
        cmd.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nbOf = reader.GetValue(0)
        End If
        cmd.Connection.Close()

    End Sub

    Private Sub countOfQuery()

    End Sub

    Private Sub saisiMan_click()
        cdBarre = cdBarreTB.Text
        qtePlaque = qtePlaqueTB.Text
        Call majIndicateur(1)

        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table T_Encours_Press. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetT_Encours_PressTableAdapter As IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter = New IndicateurEncoursPCEDotNET.IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter()
        IndicateurPressageBDDDataSetT_Encours_PressTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Press)
        Dim T_Encours_PressViewSource As CollectionViewSource = CType(Me.FindResource("T_Encours_PressViewSource"), CollectionViewSource)
        T_Encours_PressViewSource.View.MoveCurrentToFirst()

        lastOf = cdBarre
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded

        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table T_Encours_Press. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetT_Encours_PressTableAdapter As IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter = New IndicateurEncoursPCEDotNET.IndicateurPressageBDDDataSetTableAdapters.T_Encours_PressTableAdapter()
        IndicateurPressageBDDDataSetT_Encours_PressTableAdapter.Fill(IndicateurPressageBDDDataSet.T_Encours_Press)
        Dim T_Encours_PressViewSource As CollectionViewSource = CType(Me.FindResource("T_Encours_PressViewSource"), CollectionViewSource)
        T_Encours_PressViewSource.View.MoveCurrentToFirst()
    End Sub

    Private Sub undoLastOf_Click(sender As Object, e As RoutedEventArgs) Handles undoLastOf.Click



    End Sub
End Class
