Option Explicit On
Imports System.Data
Imports System.Data.Linq
Imports System.Data.OleDb
Imports LinqToDB
Imports LinqToDB.Reflection.Methods.LinqToDB

Class MainWindow
    Public cdBarre As String
    Public qtePlaque As Double

    Private Sub TextBox_TextChanged(sender As Object, e As TextChangedEventArgs)

        'cdBarre = cdBarreTB.Text
        'qtePlaque = qtePlaqueTB.Text
        'Call majIndicateur()

    End Sub

    Private Sub majIndicateur()

        Dim con As Connexion = New Connexion()
        ' Dim Table As Table(Of Table1) = db.GetTable(Of Table1)
        Dim insertQuery = "INSERT INTO Table1 (Libelle, NbPlaque) VALUES (Val_libelle, Val_nbPlaque)"
        Dim cmd = New OleDbCommand(insertQuery, con.OuvertureBDD())
        With cmd.Parameters
            .AddWithValue("Val_libelle", cdBarre)
            .AddWithValue("Val_nbPlaque", qtePlaque)
        End With
        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Connection.Close()

    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded

        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table Table1. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetTable1TableAdapter As IndicateurPressageBDDDataSetTableAdapters.Table1TableAdapter = New IndicateurPressageBDDDataSetTableAdapters.Table1TableAdapter()
        IndicateurPressageBDDDataSetTable1TableAdapter.Fill(IndicateurPressageBDDDataSet.Table1)
        Dim Table1ViewSource As CollectionViewSource = CType(Me.FindResource("Table1ViewSource"), CollectionViewSource)
        Table1ViewSource.View.MoveCurrentToFirst()
    End Sub

    Private Sub saisieMan_Click(sender As Object, e As RoutedEventArgs) Handles saisieMan.Click
        cdBarre = cdBarreTB.Text
        qtePlaque = qtePlaqueTB.Text
        Call majIndicateur()
        Dim IndicateurPressageBDDDataSet As IndicateurPressageBDDDataSet = CType(Me.FindResource("IndicateurPressageBDDDataSet"), IndicateurPressageBDDDataSet)
        'Chargez les données dans la table Table1. Vous pouvez modifier ce code selon les besoins.
        Dim IndicateurPressageBDDDataSetTable1TableAdapter As IndicateurPressageBDDDataSetTableAdapters.Table1TableAdapter = New IndicateurPressageBDDDataSetTableAdapters.Table1TableAdapter()
        IndicateurPressageBDDDataSetTable1TableAdapter.Fill(IndicateurPressageBDDDataSet.Table1)
        Dim Table1ViewSource As CollectionViewSource = CType(Me.FindResource("Table1ViewSource"), CollectionViewSource)
        Table1ViewSource.View.MoveCurrentToFirst()

    End Sub
End Class
