Imports System.Data.OleDb

Public Class DeleteWindow
    Private Sub DeleteOk_Button_Click(sender As Object, e As RoutedEventArgs) Handles DeleteOk_Button.Click
        Dim con As New Connexion
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        Dim t_encours As New T_Encours(cmd, MySettings.Default.TableSelected)
        Try
            t_encours.TruncateQuery(DeleteOFTB.Text)
        Catch ex As Exception

        End Try

        Close()
    End Sub
End Class
