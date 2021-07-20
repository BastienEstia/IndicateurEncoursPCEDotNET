Option Explicit On
Public Class CoefConfigWindow
    Private coef1 As Double

    Public Function GetCoef1()
        Return coef1
    End Function

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        'Dim sSelectedValues As List = MySettings.Default.CCBGr1.Cast(Of String).ToList
        'For i As Integer = 1 To MySettings.Default.CCBGr1.Count - 1
        '    sSelectedValues &= ";" & MySettings.Default.CCBGr1(i)
        'Next i
        '_comboGr1.Delimiter = ";"
        Dim i As Integer
        i = 0
        Dim list As IList
        _comboGr1.ItemsSource = MySettings.Default.TailleList
        _comboGr1.SelectedItem(i) = MySettings.Default.CCBGr1

        _comboGr2.ItemsSource = MySettings.Default.TailleList
        _comboGr3.ItemsSource = MySettings.Default.TailleList
        _comboGr4.ItemsSource = MySettings.Default.TailleList
        _comboGr5.ItemsSource = MySettings.Default.TailleList
        '_comboGr1.ValueMemberPath = sValueMember
    End Sub

    Private Sub SettingsOk_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsOk_Button.Click
        Dim i As Integer
        i = 0
        MySettings.Default.CCBGr1.Clear()

        While i <= _comboGr1.SelectedItems.Count - 1
            MySettings.Default.CCBGr1.Add(_comboGr1.SelectedItems(i).ToString())
            i += 1
        End While
        MySettings.Default.Save()
        'MySettings.Default.CCBGr2 = _comboGr2.SelectedItems
        'MySettings.Default.CCBGr3 = _comboGr3.SelectedItems
        'MySettings.Default.CCBGr4 = _comboGr4.SelectedItems
        'MySettings.Default.CCBGr5 = _comboGr5.SelectedItems
        Close()
    End Sub
End Class
