Option Explicit On
Public Class SettingsWindow
    Public filename As String

    Private Sub BDDLocation_Click(sender As Object, e As RoutedEventArgs)
        Dim dlg As Microsoft.Win32.OpenFileDialog = New Microsoft.Win32.OpenFileDialog()

        dlg.DefaultExt = "Access Databases|*.accdb"
        dlg.Filter = "Access Databases|*.accdb"

        Dim result As Nullable(Of Boolean) = dlg.ShowDialog()

        If result = True Then
            filename = dlg.FileName
            BDDLocation_TB.Text = filename
        End If

    End Sub

    Public Function getBDDLocation()
        Return filename
    End Function

    Private Sub Settings_Unload(sender As Object, e As RoutedEventArgs)
        Me.Close()
    End Sub

    Private Sub Settings_Continue(sender As Object, e As RoutedEventArgs)
        Me.Close()
        Me.getBDDLocation()
    End Sub
End Class
