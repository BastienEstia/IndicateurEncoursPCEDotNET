﻿Option Explicit On
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

    Public Function InsertQuery(libelle As String, qtePlaque As Integer, numOf As String) As Boolean
        Dim query As String
        query = "INSERT INTO T_Encours_" & Table & " (Libelle, NbPlaque, NumOF) VALUES (Val_libelle, Val_nbPlaque, Val_numOf)"
        With Cmd.Parameters
            .AddWithValue("Val_libelle", libelle)
            .AddWithValue("Val_nbPlaque", qtePlaque)
            .AddWithValue("Val_numOf", numOf)
        End With
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            Cmd.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            InsertQuery = False
            Exit Function
        End Try
        With Cmd.Parameters
            .RemoveAt("Val_libelle")
            .RemoveAt("Val_nbPlaque")
            .RemoveAt("Val_numOf")
        End With
        Cmd.Connection.Close()
        InsertQuery = True
    End Function

    Public Function TruncateQuery(numOf As String) As Boolean
        Dim query As String
        query = "DELETE * FROM T_Encours_" & Table & " WHERE NumOF = Val_num"
        With Cmd.Parameters
            .AddWithValue("Val_num", numOf)
        End With
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            Cmd.ExecuteReader()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            TruncateQuery = False
            Exit Function
        End Try
        Cmd.Parameters.RemoveAt("Val_num")
        Cmd.Connection.Close()
        Return True
    End Function

    Public Function SelectAll() As List(Of Encours)
        Dim query As String
        SelectAll = Nothing
        Dim ofEncours As New Encours()
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_Encours" & Table
        Cmd.CommandText = query
        Cmd.Connection.Open()
        Try
            reader = Cmd.ExecuteReader()
            While reader.Read()
                ofEncours.Id = reader("Id")
                ofEncours.Libelle = reader("Libelle")
                ofEncours.NbPlaque = reader("NbPlaque")
                ofEncours.NumOF = reader("NumOF")
                ofEncours.Table = Table
                SelectAll.Add(ofEncours)
            End While
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function

    Public Function SelectAllByNumOF(numOF As String) As Encours
        Dim query As String
        SelectAllByNumOF = Nothing
        Dim reader As OleDbDataReader
        query = "SELECT * FROM T_Encours" & Table & " WHERE NumOF = Val_numOF"
        Try
            With Cmd.Parameters
                .AddWithValue("Val_numOF", numOF)
            End With
            Cmd.Connection.Open()
            reader = Cmd.ExecuteReader()
            If reader.Read() Then
                SelectAllByNumOF.Id = reader("Id")
                SelectAllByNumOF.Libelle = reader("NbPlaque")
                SelectAllByNumOF.NbPlaque = reader("NumOF")
                SelectAllByNumOF.Table = Table
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function


    Private Function SelectCountAll() As Integer
        Dim query As String
        SelectCountAll = Nothing
        query = "SELECT COUNT(id) From T_Encours_" & MySettings.Default.TableSelected
        Cmd.CommandText = query
        Try
            Cmd.Connection.Open()
            Dim reader As OleDbDataReader
            reader = Cmd.ExecuteReader()
            If reader.Read() Then
                SelectCountAll = reader.GetValue(0)
            End If
            reader.Close()
            Cmd.Connection.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
            Exit Function
        End Try
    End Function

End Class
