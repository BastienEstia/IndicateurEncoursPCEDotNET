Imports System.Data.OleDb

Public Class Connexion
    Private connString As String
    Private oleConnection As OleDbConnection

    Public Property ConnString1 As String
        Get
            Return connString
        End Get
        Set(value As String)
            connString = value
        End Set
    End Property

    Public Sub New()

    End Sub

    Public Sub New(connString As String, oleConnection As OleDbConnection)
        Me.connString = connString
        Me.oleConnection = oleConnection
    End Sub

    Public Function ConnexionBDD(connexionStringInput As String) As OleDbConnection
        ConnString1 = "Provider=Microsoft.ACE.OLEDB.12.0;"
        ConnString1 += "Data Source=" & connexionStringInput
        oleConnection = New OleDbConnection(ConnString1)
        Return oleConnection
    End Function



    Public Function GetRightConnString(connString As String)
        Dim rightConnString As String
        rightConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & connString & ";Persist Security Info=True;Jet OLEDB:Database Password=password"
        Return rightConnString
    End Function

    Public Function InsertQuery(libelle As String, qtePlaque As Integer, numOf As String, table As String, cmd As OleDbCommand) As Boolean
        Dim query As String
        query = "INSERT INTO T_Encours_" & table & " (Libelle, NbPlaque, NumOF) VALUES (Val_libelle, Val_nbPlaque, Val_numOf)"
        With cmd.Parameters
            .AddWithValue("Val_libelle", libelle)
            .AddWithValue("Val_nbPlaque", qtePlaque)
            .AddWithValue("Val_numOf", numOf)
        End With
        cmd.CommandText = query
        cmd.Connection.Open()
        cmd.ExecuteReader()
        With cmd.Parameters
            .RemoveAt("Val_libelle")
            .RemoveAt("Val_nbPlaque")
            .RemoveAt("Val_numOf")
        End With
        cmd.Connection.Close()

        Return True
    End Function

    Public Function TruncateQuery(numOf As String, Table As String, cmd As OleDbCommand) As Boolean
        Dim query As String
        query = "DELETE * From T_Encours_" & Table & " WHERE NumOF = Val_num"

        With cmd.Parameters
            .AddWithValue("Val_num", numOf)
        End With

        cmd.CommandText = query

        cmd.Connection.Open()
        cmd.ExecuteReader()
        cmd.Parameters.RemoveAt("Val_num")
        cmd.Connection.Close()

        Return True
    End Function

    Public Function SelectAllWithNumOFQuery(numOf As String, cmd As OleDbCommand) As String()
        Dim query As String
        Dim libelle As String
        Dim nbPlaque As Integer
        Dim resNumOF As String
        Dim res(3) As String

        Dim con As Connexion = New Connexion()
        query = "SELECT libelle, nbPlaque, numOF FROM T_Encours_" & MySettings.Default.TableFournisseur & " WHERE NumOF = Val_NumOF"
        With cmd.Parameters
            .AddWithValue("Val_NumOF", numOf)
        End With
        cmd.CommandText = query
        cmd.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            libelle = reader("Libelle")
            nbPlaque = reader("NbPlaque")
            resNumOF = reader("NumOF")
        Else
            Return res
        End If

        reader.Close()
        cmd.Connection.Close()
        res(0) = libelle
        res(1) = nbPlaque.ToString
        res(2) = numOf

        Return res
    End Function

    Public Function SelectAllWithNumOFInTempTableQuery(numOf As String, cmd As OleDbCommand) As String()
        Dim query As String
        Dim libelle As String
        Dim nbPlaque As Integer
        Dim resNumOF As String
        Dim res(3) As String

        Dim con As Connexion = New Connexion()
        query = "SELECT libelle, nbPlaque, numOF FROM T_Tempon WHERE NumOF = Val_NumOF"
        With cmd.Parameters
            .AddWithValue("Val_NumOF", numOf)
        End With
        cmd.CommandText = query
        cmd.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            libelle = reader("Libelle")
            nbPlaque = reader("NbPlaque")
            resNumOF = reader("NumOF")
        Else
            Return res
        End If

        reader.Close()
        cmd.Connection.Close()
        res(0) = libelle
        res(1) = nbPlaque.ToString
        res(2) = numOf

        Return res
    End Function

    Private Function CountQuery(cmd As OleDbCommand) As Integer
        Dim query As String
        Dim nbof As Integer
        query = "SELECT COUNT(id) From T_Encours_" & MySettings.Default.TableSelected
        cmd.CommandText = query
        cmd.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()
        If reader.Read() Then
            nbof = reader.GetValue(0)
        End If
        reader.Close()
        cmd.Connection.Close()

        Return nbof
    End Function

    'Public Function DropTableQuery(cmd As OleDbCommand)

    '    Dim query As String
    '    'query = "Drop Table T_Encours_Coupe"
    '    'cmd.CommandText = query
    '    'cmd.Connection.Open()
    '    'cmd.ExecuteReader()
    '    'cmd.Connection.Close()

    '    query = "Create Table T_Encours_Coupe (id INT NOT NULL IDENTITY(1,1), Libelle varchar(30), NbPlaque int, PRIMARY KEY(id))"
    '    cmd.CommandText = query
    '    cmd.Connection.Open()
    '    cmd.ExecuteReader()
    '    cmd.Connection.Close()

    'End Function

    Public Function SelectLastIdQuery(cmd As OleDbCommand) As Integer
        Dim query As String
        Dim lastId As Integer
        Dim nbof As Integer
        Dim i As Integer
        i = 0

        Dim con As Connexion = New Connexion()
        nbof = con.CountQuery(cmd)
        Dim IdOfTab(nbof) As Integer

        query = "SELECT Id FROM T_Encours_" & MySettings.Default.TableSelected
        Dim cmd1 = cmd
        cmd1.CommandText = query
        cmd1.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd1.ExecuteReader()

        While reader.Read()
            IdOfTab(i) = reader("id")
            i += 1
        End While

        reader.Close()
        cmd1.Connection.Close()

        lastId = IdOfTab.Max
        Return lastId
    End Function

    Public Function SelectLastNumOf(Id As Integer, cmd As OleDbCommand) As String
        Dim lastNumOf As String
        Dim query As String
        Dim NumOf As String

        Dim con As Connexion = New Connexion()

        query = "SELECT NumOF FROM T_Encours_" & MySettings.Default.TableSelected & " Where Id = Val_id"
        cmd.CommandText = query
        With cmd.Parameters
            .AddWithValue("Val_id", Id)
        End With

        cmd.Connection.Open()

        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        If reader.Read() Then
            NumOf = reader("NumOF")
        End If

        reader.Close()
        cmd.Parameters.RemoveAt("Val_id")
        cmd.Connection.Close()

        lastNumOf = NumOf
        Return lastNumOf

    End Function

    Public Function SelectCountForExistanceQuery(cmd As OleDbCommand, numOf As String)
        Dim query As String
        Dim existResults As Integer

        Dim con As Connexion = New Connexion()
        query = "SELECT Count(*) FROM T_Encours_" & MySettings.Default.TableFournisseur & " WHERE NumOf = Val_numOf"

        cmd.CommandText = query
        cmd.Connection.Open()
        With cmd.Parameters
            .AddWithValue("Val_numOf", numOf)
        End With
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        While reader.Read()
            existResults = reader.GetValue(0)
        End While
        reader.Close()
        cmd.Parameters.RemoveAt("Val_numOf")
        cmd.Connection.Close()

        Return existResults
    End Function

    Public Function SelectNbPlaqueQuery(cmd As OleDbCommand) As String(,)
        Dim query As String
        Dim nbof As Integer
        Dim i As Integer
        i = 0

        Dim con As New Connexion()
        nbof = con.CountQuery(cmd)
        Dim nbPlaqueLibelleMat(,) As String = New String(nbof.ToString, 2) {}
        query = "SELECT libelle, NbPlaque FROM T_Encours_" & MySettings.Default.TableSelected
        Dim cmd1 = cmd
        cmd.CommandText = query
        cmd1.Connection.Open()
        Dim reader As OleDbDataReader
        reader = cmd.ExecuteReader()

        While reader.Read()
            nbPlaqueLibelleMat(i, 0) = reader("NbPlaque")
            nbPlaqueLibelleMat(i, 1) = reader("Libelle")
            i += 1
        End While

        reader.Close()

        Return nbPlaqueLibelleMat
    End Function
End Class