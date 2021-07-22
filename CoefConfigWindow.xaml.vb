Option Explicit On
Public Class CoefConfigWindow
    Private coef1 As Double

    Public Function GetCoef1()
        Return coef1
    End Function

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        'MySettings.Default.TailleList.Remove("R3740")
        'MySettings.Default.Save()

        Dim sSelectedValuesGr1 As String
        Dim sSelectedValuesGr2 As String
        Dim sSelectedValuesGr3 As String
        Dim sSelectedValuesGr4 As String
        Dim sSelectedValuesGr5 As String

        Dim i As Integer
        i = 1

        Dim listGr1 As New List(Of Object)
        Dim listGr2 As New List(Of Object)
        Dim listGr3 As New List(Of Object)
        Dim listGr4 As New List(Of Object)
        Dim listGr5 As New List(Of Object)

        Dim objGr1 As Object
        Dim objGr2 As Object
        Dim objGr3 As Object
        Dim objGr4 As Object
        Dim objGr5 As Object



        _comboGr1.ItemsSource = MySettings.Default.TailleList
        _comboGr2.ItemsSource = MySettings.Default.TailleList
        _comboGr3.ItemsSource = MySettings.Default.TailleList
        _comboGr4.ItemsSource = MySettings.Default.TailleList
        _comboGr5.ItemsSource = MySettings.Default.TailleList

        If Not (MySettings.Default.CCBGr1(0) = "?") Then
            sSelectedValuesGr1 = MySettings.Default.CCBGr1(0) & ","
            objGr1 = MySettings.Default.CCBGr1(0)
            listGr1.Add(objGr1)
        End If

        If Not (MySettings.Default.CCBGr2(0) = "?") Then
            sSelectedValuesGr2 = MySettings.Default.CCBGr2(0) & ","
            objGr2 = MySettings.Default.CCBGr2(0)
            listGr2.Add(objGr2)
        End If

        If Not (MySettings.Default.CCBGr3(0) = "?") Then
            sSelectedValuesGr3 = MySettings.Default.CCBGr3(0) & ","
            objGr3 = MySettings.Default.CCBGr3(0)
            listGr3.Add(objGr3)
        End If

        If Not (MySettings.Default.CCBGr4(0) = "?") Then
            sSelectedValuesGr4 = MySettings.Default.CCBGr4(0) & ","
            objGr4 = MySettings.Default.CCBGr4(0)
            listGr4.Add(objGr4)
        End If

        If Not (MySettings.Default.CCBGr5(0) = "?") Then
            sSelectedValuesGr5 = MySettings.Default.CCBGr5(0) & ","
            objGr5 = MySettings.Default.CCBGr5(0)
            listGr5.Add(objGr5)
        End If

        While i <= MySettings.Default.CCBGr1.Count - 1
            sSelectedValuesGr1 = sSelectedValuesGr1 & MySettings.Default.CCBGr1(i) & ","
            objGr1 = MySettings.Default.CCBGr1(i)
            listGr1.Add(objGr1)
            i += 1
        End While

        i = 1
        While i <= MySettings.Default.CCBGr2.Count - 1
            sSelectedValuesGr2 = sSelectedValuesGr2 & MySettings.Default.CCBGr2(i) & ","
            objGr2 = MySettings.Default.CCBGr2(i)
            listGr2.Add(objGr2)
            i += 1
        End While

        i = 1
        While i <= MySettings.Default.CCBGr3.Count - 1
            sSelectedValuesGr3 = sSelectedValuesGr3 & MySettings.Default.CCBGr3(i) & ","
            objGr3 = MySettings.Default.CCBGr3(i)
            listGr3.Add(objGr3)
            i += 1
        End While

        i = 1
        While i <= MySettings.Default.CCBGr4.Count - 1
            sSelectedValuesGr4 = sSelectedValuesGr4 & MySettings.Default.CCBGr4(i) & ","
            objGr4 = MySettings.Default.CCBGr4(i)
            listGr4.Add(objGr4)
            i += 1
        End While

        i = 1
        While i <= MySettings.Default.CCBGr5.Count - 1
            sSelectedValuesGr5 = sSelectedValuesGr5 & MySettings.Default.CCBGr5(i) & ","
            objGr5 = MySettings.Default.CCBGr5(i)
            listGr5.Add(objGr5)
            i += 1
        End While

        _comboGr1.SelectedValue = sSelectedValuesGr1
        _comboGr1.SelectedItemsOverride = listGr1
        _comboGr1.Text = sSelectedValuesGr1

        _comboGr2.SelectedValue = sSelectedValuesGr2
        _comboGr2.SelectedItemsOverride = listGr2
        _comboGr2.Text = sSelectedValuesGr2

        _comboGr3.SelectedValue = sSelectedValuesGr3
        _comboGr3.SelectedItemsOverride = listGr3
        _comboGr3.Text = sSelectedValuesGr3

        _comboGr4.SelectedValue = sSelectedValuesGr4
        _comboGr4.SelectedItemsOverride = listGr4
        _comboGr4.Text = sSelectedValuesGr4

        _comboGr5.SelectedValue = sSelectedValuesGr5
        _comboGr5.SelectedItemsOverride = listGr5
        _comboGr5.Text = sSelectedValuesGr5

        SliderGr1.Value = MySettings.Default.coef1
        SliderGr2.Value = MySettings.Default.coef2
        SliderGr3.Value = MySettings.Default.coef3
        SliderGr4.Value = MySettings.Default.coef4
        SliderGr5.Value = MySettings.Default.coef5

        For i = 0 To _comboGr1.SelectedItems.Count - 1
            labelGr1.Content &= _comboGr1.SelectedItems(i) & vbCrLf
        Next

        For i = 0 To _comboGr2.SelectedItems.Count - 1
            labelGr2.Content &= _comboGr2.SelectedItems(i) & vbCrLf
        Next

        For i = 0 To _comboGr3.SelectedItems.Count - 1
            labelGr3.Content &= _comboGr3.SelectedItems(i) & vbCrLf
        Next

        For i = 0 To _comboGr4.SelectedItems.Count - 1
            labelGr4.Content &= _comboGr4.SelectedItems(i) & vbCrLf
        Next

        For i = 0 To _comboGr5.SelectedItems.Count - 1
            labelGr5.Content &= _comboGr5.SelectedItems(i) & vbCrLf
        Next

    End Sub

    Private Sub SettingsOk_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsOk_Button.Click
        Dim i As Integer
        Dim verifUniciteList As New List(Of String)
        i = 0
        MySettings.Default.CCBGr1.Clear()
        If _comboGr1.SelectedItems.Count = 0 Then
            MySettings.Default.CCBGr1.Add("?")
            MySettings.Default.Save()
        Else
            While i <= _comboGr1.SelectedItems.Count - 1
                MySettings.Default.CCBGr1.Add(_comboGr1.SelectedItems(i).ToString())
                verifUniciteList.Add(_comboGr1.SelectedItems(i).ToString())
                MySettings.Default.Save()
                i += 1
            End While
        End If

        i = 0
        MySettings.Default.CCBGr2.Clear()
        If _comboGr2.SelectedItems.Count = 0 Then
            MySettings.Default.CCBGr2.Add("?")
            MySettings.Default.Save()
        Else
            While i <= _comboGr2.SelectedItems.Count - 1
                MySettings.Default.CCBGr2.Add(_comboGr2.SelectedItems(i).ToString())
                verifUniciteList.Add(_comboGr2.SelectedItems(i).ToString())
                MySettings.Default.Save()
                i += 1
            End While
        End If

        i = 0
        MySettings.Default.CCBGr3.Clear()
        If _comboGr3.SelectedItems.Count = 0 Then
            MySettings.Default.CCBGr3.Add("?")
            MySettings.Default.Save()
        Else
            While i <= _comboGr3.SelectedItems.Count - 1
                MySettings.Default.CCBGr3.Add(_comboGr3.SelectedItems(i).ToString())
                verifUniciteList.Add(_comboGr3.SelectedItems(i).ToString())
                MySettings.Default.Save()
                i += 1
            End While
        End If

        i = 0
        MySettings.Default.CCBGr4.Clear()
        If _comboGr4.SelectedItems.Count = 0 Then
            MySettings.Default.CCBGr4.Add("?")
            verifUniciteList.Add(_comboGr4.SelectedItems(i).ToString())
            MySettings.Default.Save()
        Else
            While i <= _comboGr4.SelectedItems.Count - 1
                MySettings.Default.CCBGr4.Add(_comboGr4.SelectedItems(i).ToString())
                verifUniciteList.Add(_comboGr4.SelectedItems(i).ToString())
                MySettings.Default.Save()
                i += 1
            End While
        End If

        i = 0
        MySettings.Default.CCBGr5.Clear()
        If _comboGr5.SelectedItems.Count = 0 Then
            MySettings.Default.CCBGr5.Add("?")
            MySettings.Default.Save()
        Else
            While i <= _comboGr5.SelectedItems.Count - 1
                MySettings.Default.CCBGr5.Add(_comboGr5.SelectedItems(i).ToString())
                verifUniciteList.Add(_comboGr5.SelectedItems(i).ToString())
                MySettings.Default.Save()
                i += 1
            End While
        End If
        MySettings.Default.Save()

        'For i = 0 To verifUniciteList.Count - 1
        '    If Not MySettings.Default.TailleList(i) Then

        '    End If
        'Next

        If verifUniciteList.Distinct.Count < MySettings.Default.TailleList.Count Then
            'message box attention manque des tailles assignées
        ElseIf verifUniciteList.Distinct.Count > MySettings.Default.TailleList.Count Then
            'message bos attention taille dans groupe différents
        End If

        Dim MW As New MainWindow()
        For Each wnd As Window In Windows.Application.Current.Windows
            If wnd.GetType Is GetType(MainWindow) Then
                MW = wnd
            End If
        Next

        MW.coef1 = SliderGr1.Value
        MySettings.Default.coef1 = SliderGr1.Value
        MW.coef1 = SliderGr2.Value
        MySettings.Default.coef2 = SliderGr2.Value
        MW.coef3 = SliderGr3.Value
        MySettings.Default.coef3 = SliderGr3.Value
        MW.coef4 = SliderGr4.Value
        MySettings.Default.coef4 = SliderGr4.Value
        MW.coef5 = SliderGr4.Value
        MySettings.Default.coef5 = SliderGr5.Value
        MySettings.Default.Save()

        MW.tailleGr1 = _comboGr1.SelectedItems
        MW.tailleGr2 = _comboGr2.SelectedItems
        MW.tailleGr3 = _comboGr3.SelectedItems
        MW.tailleGr4 = _comboGr4.SelectedItems
        MW.tailleGr5 = _comboGr5.SelectedItems

        Close()
    End Sub

    Private Sub ComboGr1_Closed(sender As Object, e As RoutedEventArgs) Handles _comboGr1.Closed
        labelGr1.Content = ""
        For i As Integer = 0 To _comboGr1.SelectedItems.Count - 1
            labelGr1.Content &= _comboGr1.SelectedItems(i) & vbCrLf
        Next
    End Sub

    Private Sub ComboGr2_Closed(sender As Object, e As RoutedEventArgs) Handles _comboGr2.Closed
        labelGr2.Content = ""
        For i As Integer = 0 To _comboGr2.SelectedItems.Count - 1
            labelGr2.Content &= _comboGr2.SelectedItems(i) & vbCrLf
        Next
    End Sub

    Private Sub ComboGr3_Closed(sender As Object, e As RoutedEventArgs) Handles _comboGr3.Closed
        labelGr3.Content = ""
        For i As Integer = 0 To _comboGr3.SelectedItems.Count - 1
            labelGr3.Content &= _comboGr3.SelectedItems(i) & vbCrLf
        Next
    End Sub

    Private Sub ComboGr4_Closed(sender As Object, e As RoutedEventArgs) Handles _comboGr4.Closed
        labelGr4.Content = ""
        For i As Integer = 0 To _comboGr4.SelectedItems.Count - 1
            labelGr4.Content &= _comboGr4.SelectedItems(i) & vbCrLf
        Next
    End Sub

    Private Sub ComboGr5_Closed(sender As Object, e As RoutedEventArgs) Handles _comboGr5.Closed
        labelGr5.Content = ""
        For i As Integer = 0 To _comboGr5.SelectedItems.Count - 1
            labelGr5.Content &= _comboGr5.SelectedItems(i) & vbCrLf
        Next
    End Sub

    Private Sub SliderGr1_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SliderGr1.ValueChanged
        SliderGr1TB.Text = SliderGr1.Value
    End Sub

    Private Sub SliderGr2_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SliderGr2.ValueChanged
        SliderGr2TB.Text = SliderGr2.Value
    End Sub

    Private Sub SliderGr3_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SliderGr3.ValueChanged
        SliderGr3TB.Text = SliderGr3.Value
    End Sub

    Private Sub SliderGr4_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SliderGr4.ValueChanged
        SliderGr4TB.Text = SliderGr4.Value
    End Sub

    Private Sub SliderGr5_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Double)) Handles SliderGr5.ValueChanged
        SliderGr5TB.Text = SliderGr5.Value
    End Sub

    Private Sub SliderGr1TB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SliderGr1TB.TextChanged
        If SliderGr1TB.Text <> "" Then
            SliderGr1.Value = SliderGr1TB.Text
        End If
    End Sub

    Private Sub SliderGr2TB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SliderGr2TB.TextChanged
        If SliderGr2TB.Text <> "" Then
            SliderGr2.Value = SliderGr2TB.Text
        End If
    End Sub

    Private Sub SliderGr3TB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SliderGr3TB.TextChanged
        If SliderGr3TB.Text <> "" Then
            SliderGr3.Value = SliderGr3TB.Text
        End If
    End Sub

    Private Sub SliderGr4TB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SliderGr4TB.TextChanged
        If SliderGr4TB.Text <> "" Then
            SliderGr4.Value = SliderGr4TB.Text
        End If
    End Sub

    Private Sub SliderGr5TB_TextChanged(sender As Object, e As TextChangedEventArgs) Handles SliderGr5TB.TextChanged
        If SliderGr5TB.Text <> "" Then
            SliderGr5.Value = SliderGr5TB.Text
        End If
    End Sub
End Class
