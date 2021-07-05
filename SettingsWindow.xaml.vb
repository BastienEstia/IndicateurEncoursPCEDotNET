Option Explicit On
Public Class SettingsWindow
    Private filename As String

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
        Dim MW As New MainWindow()
        For Each wnd As Window In Windows.Application.Current.Windows
            If wnd.GetType Is GetType(MainWindow) Then
                MW = wnd
            End If
        Next
        MW.connexionString = GetBDDLocation()
        MW.MajTableau()
        MW.MajIndicateur()
        Close()
    End Sub
End Class
