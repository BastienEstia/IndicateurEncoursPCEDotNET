Option Explicit On
Imports System.Data.OleDb

Public Class T_Encours
    Public Property Cmd As OleDbCommand
    Public Property Table As String

    Public Sub New(cmd As OleDbCommand, table As String)
        Me.Cmd = cmd
        Me.Table = table
    End Sub

    Public Sub New()

    End Sub

    Public Function InsertQuery(encours As Encours) As Boolean
        Dim query As String
        Dim er As OleDbDataReader = Nothing
        query = "INSERT INTO T_Encours_" & Table & " (Libelle, NbPlaque, NumOF) VALUES (Val_libelle, Val_nbPlaque, Val_numOF)"
        With Cmd.Parameters
            .AddWithValue("Val_libelle", encours.Libelle)
            .AddWithValue("Val_nbPlaque", encours.NbPlaque)
            .AddWithValue("Val_numOf", encours.NumOF)
        End With
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            er = Cmd.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            InsertQuery = False
        End Try
        With Cmd.Parameters
            .RemoveAt("Val_libelle")
            .RemoveAt("Val_nbPlaque")
            .RemoveAt("Val_numOf")
        End With
        er.Close()
        Cmd.Connection.Close()
        InsertQuery = True
    End Function

    Public Function TruncateQuery(numOf As String) As Boolean
        Dim query As String
        Dim er As OleDbDataReader = Nothing
        TruncateQuery = True
        query = "DELETE * FROM T_Encours_" & Table & " WHERE NumOF = Val_num"
        With Cmd.Parameters
            .AddWithValue("Val_num", numOf)
        End With
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            er = Cmd.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            TruncateQuery = False
        End Try
        er.Close()
        Cmd.Parameters.RemoveAt("Val_num")
        Cmd.Connection.Close()
    End Function

    Public Function SelectAll() As List(Of Encours)
        Dim query As String
        SelectAll = Nothing
        Dim encoursList As New List(Of Encours)
        Dim reader As OleDbDataReader = Nothing
        query = "SELECT * FROM T_Encours_" & Table
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            reader = Cmd.ExecuteReader()
            While reader.Read()
                Dim ofEncours As New Encours()
                ofEncours.Id = reader("Id")
                ofEncours.Libelle = reader("Libelle")
                ofEncours.NbPlaque = reader("NbPlaque")
                ofEncours.NumOF = reader("NumOF")
                ofEncours.Table = Table
                encoursList.Add(ofEncours)
            End While
            SelectAll = encoursList
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        reader.Close()
        Cmd.Connection.Close()
    End Function

    Public Function SelectAllByNumOF(numOF As String) As Encours
        Dim query As String
        SelectAllByNumOF = Nothing
        Dim encours As New Encours()
        Dim reader As OleDbDataReader = Nothing
        query = "SELECT * FROM T_Encours_" & Table & " WHERE NumOF = Val_numOF"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_numOF", numOF)
            End With
            Cmd.CommandText = query
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            If reader.Read() Then
                encours.Id = reader("Id")
                encours.NbPlaque = reader("NbPlaque")
                encours.NumOF = reader("NumOF")
                encours.Libelle = reader("Libelle")
                encours.Table = Table
                SelectAllByNumOF = encours
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        Cmd.Parameters.RemoveAt("Val_numOF")
        Try
            reader.Close()
        Catch ex As Exception
        End Try
        Cmd.Connection.Close()
    End Function


    Private Function SelectCountAll() As Integer
        Dim query As String
        Dim reader As OleDbDataReader = Nothing
        SelectCountAll = Nothing
        query = "SELECT COUNT(id) From T_Encours_" & MySettings.Default.TableSelected
        Cmd.CommandText = query
        Try
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            If reader.Read() Then
                SelectCountAll = reader.GetValue(0)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        End Try
        reader.Close()
        Cmd.Connection.Close()
    End Function

End Class
