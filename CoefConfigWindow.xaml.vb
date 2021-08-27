Option Explicit On
Imports System.Data.OleDb

Public Class CoefConfigWindow
    Private coef1 As Double

    Public Function GetCoef1()
        Return coef1
    End Function

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

        'MySettings.Default.TailleList.Remove("R3740")
        'MySettings.Default.Save()

        Dim i As Integer
        i = 1

        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        Dim t_groupeTaille As New T_GroupeTaille(cmd)

        Dim groupeTailleList As List(Of GroupeTaille) = t_groupeTaille.SelectAllByTable(MySettings.Default.TableSelected)

        _comboGr1.ItemsSource = MySettings.Default.TailleList
        _comboGr2.ItemsSource = MySettings.Default.TailleList
        _comboGr3.ItemsSource = MySettings.Default.TailleList
        _comboGr4.ItemsSource = MySettings.Default.TailleList
        _comboGr5.ItemsSource = MySettings.Default.TailleList

        _comboGr1.SelectedValue = groupeTailleList(0).TailleList.ToString()
        _comboGr1.SelectedItemsOverride = groupeTailleList(0).TailleList
        _comboGr1.Text = groupeTailleList(0).TailleList.ToString()

        _comboGr2.SelectedValue = groupeTailleList(1).TailleList.ToString()
        _comboGr2.SelectedItemsOverride = groupeTailleList(1).TailleList
        _comboGr2.Text = groupeTailleList(1).TailleList.ToString()

        _comboGr3.SelectedValue = groupeTailleList(2).TailleList.ToString()
        _comboGr3.SelectedItemsOverride = groupeTailleList(2).TailleList
        _comboGr3.Text = groupeTailleList(2).TailleList.ToString()

        _comboGr4.SelectedValue = groupeTailleList(3).TailleList.ToString()
        _comboGr4.SelectedItemsOverride = groupeTailleList(3).TailleList
        _comboGr4.Text = groupeTailleList(3).TailleList.ToString()

        _comboGr5.SelectedValue = groupeTailleList(4).TailleList.ToString()
        _comboGr5.SelectedItemsOverride = groupeTailleList(4).TailleList
        _comboGr5.Text = groupeTailleList(4).TailleList.ToString()

        SliderGr1.Value = groupeTailleList(0).Coef
        SliderGr2.Value = groupeTailleList(1).Coef
        SliderGr3.Value = groupeTailleList(2).Coef
        SliderGr4.Value = groupeTailleList(3).Coef
        SliderGr5.Value = groupeTailleList(4).Coef

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
        Dim list As New List(Of Integer)
        Dim groupeTaille As New GroupeTaille()
        Dim groupeTailleList As New List(Of GroupeTaille)
        i = 0
        Dim con As New Connexion()
        Dim cmd As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        Dim cmd2 As New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }

        Dim t_groupeTaille As New T_GroupeTaille(cmd)

        groupeTailleList = t_groupeTaille.SelectAllByTable(MySettings.Default.TableSelected)

        While i <= groupeTailleList.Count - 1
            groupeTailleList(i).TailleList.Clear()
            i += 1
        End While

        list.Add(_comboGr1.SelectedItems.Count)
        list.Add(_comboGr2.SelectedItems.Count)
        list.Add(_comboGr3.SelectedItems.Count)
        list.Add(_comboGr4.SelectedItems.Count)
        list.Add(_comboGr5.SelectedItems.Count)

        i = 0
        While i <= _comboGr1.SelectedItems().Count - 1
            groupeTailleList(0).TailleList.Add(_comboGr1.SelectedItems(i).ToString())
            verifUniciteList.Add(_comboGr1.SelectedItems(i).ToString())
            i += 1
        End While

        i = 0
        While i <= _comboGr2.SelectedItems().Count - 1
            groupeTailleList(1).TailleList.Add(_comboGr2.SelectedItems(i).ToString())
            verifUniciteList.Add(_comboGr2.SelectedItems(i).ToString())
            i += 1
        End While

        i = 0
        While i <= _comboGr3.SelectedItems().Count - 1
            groupeTailleList(2).TailleList.Add(_comboGr3.SelectedItems(i).ToString())
            verifUniciteList.Add(_comboGr3.SelectedItems(i).ToString())
            i += 1
        End While

        i = 0
        While i <= _comboGr4.SelectedItems().Count - 1
            groupeTailleList(3).TailleList.Add(_comboGr4.SelectedItems(i).ToString())
            verifUniciteList.Add(_comboGr4.SelectedItems(i).ToString())
            i += 1
        End While

        i = 0
        While i <= _comboGr5.SelectedItems().Count - 1
            groupeTailleList(4).TailleList.Add(_comboGr5.SelectedItems(i).ToString())
            verifUniciteList.Add(_comboGr5.SelectedItems(i).ToString())
            i += 1
        End While

        'For i = 0 To verifUniciteList.Count - 1
        '    If Not MySettings.Default.TailleList(i) Then

        '    End If
        'Next

        If verifUniciteList.Distinct.Count < MySettings.Default.TailleList.Count Then
            'message box attention manque des tailles assignées
        ElseIf verifUniciteList.Distinct.Count > MySettings.Default.TailleList.Count Then
            'message bos attention taille dans groupe différents
        End If

        cmd = New OleDbCommand With {
            .Connection = con.ConnexionBDD(MySettings.Default.BDDConnString)
        }
        t_groupeTaille = New T_GroupeTaille(cmd)

        i = 0
        While i < groupeTailleList.Count - 1

            t_groupeTaille.TruncateQuery(MySettings.Default.TableSelected, groupeTailleList(i).Groupe)
            t_groupeTaille.InsertQuery(groupeTailleList(i))
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
End Class
