Option Explicit On
Imports System.Data.OleDb
Imports Xceed.Wpf.Toolkit

Public Class CoefConfigWindow
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        'MySettings.Default.TailleList.Remove("R3740")
        'MySettings.Default.Save()

        Dim i As Integer
        Dim j As Integer
        i = 1
        Dim labelList As New List(Of Label)
        Dim comboBoxList As New List(Of CheckComboBox)
        Dim sliderList As New List(Of Slider)
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        Dim t_groupeTaille As New T_GroupeTaille(cmd)

        Dim groupeTailleList As List(Of GroupeTaille) = t_groupeTaille.SelectAllByTable(MySettings.Default.TableSelected)

        comboBoxList.Add(_comboGr1)
        comboBoxList.Add(_comboGr2)
        comboBoxList.Add(_comboGr3)
        comboBoxList.Add(_comboGr4)
        comboBoxList.Add(_comboGr5)

        sliderList.Add(SliderGr1)
        sliderList.Add(SliderGr2)
        sliderList.Add(SliderGr3)
        sliderList.Add(SliderGr4)
        sliderList.Add(SliderGr5)

        labelList.Add(labelGr1)
        labelList.Add(labelGr2)
        labelList.Add(labelGr3)
        labelList.Add(labelGr4)
        labelList.Add(labelGr5)

        For i = 0 To 4
            comboBoxList(i).ItemsSource = MySettings.Default.TailleList
            comboBoxList(i).SelectedItemsOverride = groupeTailleList(i).TailleList
            sliderList(i).Value = groupeTailleList(i).Coef
            For j = 0 To comboBoxList(i).SelectedItems.Count - 1
                labelList(i).Content &= comboBoxList(i).SelectedItems(j) & vbCrLf
            Next j
        Next i
    End Sub

    Private Sub SettingsOk_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsOk_Button.Click
        Dim i As Integer
        Dim verifUniciteList As New List(Of String)
        Dim comboBoxList As New List(Of CheckComboBox)
        Dim groupeTaille As New GroupeTaille()
        Dim sliderList As New List(Of Slider)

        comboBoxList.Add(_comboGr1)
        comboBoxList.Add(_comboGr2)
        comboBoxList.Add(_comboGr3)
        comboBoxList.Add(_comboGr4)
        comboBoxList.Add(_comboGr5)

        sliderList.Add(SliderGr1)
        sliderList.Add(SliderGr2)
        sliderList.Add(SliderGr3)
        sliderList.Add(SliderGr4)
        sliderList.Add(SliderGr5)

        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        Dim t_groupeTaille = New T_GroupeTaille(cmd)
        Dim groupeTailleList As List(Of GroupeTaille) = t_groupeTaille.SelectAllByTable(MySettings.Default.TableSelected)
        i = 0
        While i <= groupeTailleList.Count - 1
            Try
                groupeTailleList(i).TailleList.Clear()
                groupeTailleList(i).TailleList = comboBoxList(i).SelectedItems
                groupeTailleList(i).Coef = sliderList(i).Value
                verifUniciteList.AddRange(comboBoxList(i).SelectedItems)
            Catch ex As Exception
                groupeTailleList(i).TailleList = comboBoxList(i).SelectedItems
            End Try
            i += 1
        End While
        'For i = 0 To verifUniciteList.Count - 1
        '    If Not MySettings.Default.TailleList(i) Then

        '    End If
        'Next
        Dim resultlist As New List(Of String)
        Dim defaultlist As New List(Of String)
        Dim resultlistString As String = Nothing
        i = 0
        While i <= MySettings.Default.TailleList.Count - 1
            defaultlist.Add(MySettings.Default.TailleList(i))
            i += 1
        End While
        resultlist.AddRange(defaultlist.Except(verifUniciteList))
        i = 0
        If resultlist.Count > 0 Then
            While i <= resultlist.Count - 1
                resultlistString += resultlist(i) + " "
                i += 1
            End While
            MsgBox("Les tailles suivantes ne sont pas assignées : " & resultlistString)
            Exit Sub
        End If
        i = 0
        While i <= groupeTailleList.Count - 1
            t_groupeTaille.UpdateQuery(groupeTailleList(i))
            i += 1
        End While
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

    Private Sub SettingsBack_Button_Click(sender As Object, e As RoutedEventArgs) Handles SettingsBack_Button.Click
        DialogResult = False
    End Sub
End Class
