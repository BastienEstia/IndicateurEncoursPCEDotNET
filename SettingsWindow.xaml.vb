Option Explicit On
Public Class SettingsWindow
    Private filename As String

    Private Sub SettingsW_Loaded(sender As Object, e As RoutedEventArgs) Handles SettingsW.Loaded
        Dim cbItem As New ComboBoxItem
        filename = MySettings.Default.BDDPath
        'ComboBoxItem.ContentProperty = MySettings.Default.TableSelected
        BDDLocation_TB.Text = MySettings.Default.BDDPath
        Table_ComboBox.SelectedValue = MySettings.Default.TableSelected
    End Sub

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

        MySettings.Default.BDDConnString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" & MW.connexionString & ";Persist Security Info=True;Jet OLEDB:Database Password=password"
        MySettings.Default.BDDPath = MW.connexionString
        MySettings.Default.Save()
        BDDLocation_TB.Text = MW.connexionString
        Close()
    End Sub

    Private Sub Table_ComboBox_Loaded(sender As Object, e As RoutedEventArgs) Handles Table_ComboBox.Loaded
        Dim listeTable As New List(Of String)
        For Each table As String In MySettings.Default.TableList
            listeTable.Add(Split(table, "_")(2))
        Next table
        Table_ComboBox.ItemsSource = listeTable
    End Sub

    Private Sub Table_ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles Table_ComboBox.SelectionChanged
        Dim selectedItem As String
        sender = CType(sender, ComboBox)
        selectedItem = sender.SelectedItem
        MySettings.Default.TableSelected = selectedItem
        MySettings.Default.Save()
    End Sub


End Class

