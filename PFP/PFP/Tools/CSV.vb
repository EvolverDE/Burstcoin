﻿
Namespace CSVTool

    Public Class CSVReader
        Private _Path As String
        Private _Splitter As String
        Private _ListsParameter As List(Of String()) = New List(Of String())


        Sub New(Path As String, Optional ByVal Splitter As String = ";")
            Me.Path = Path
            Me.Splitter = Splitter
            ReadCSV()
        End Sub

        Public Property Path As String
            Get
                Return _Path
            End Get
            Set(value As String)
                _Path = value
            End Set
        End Property

        Public Property Splitter As String
            Get
                Return _Splitter
            End Get
            Set(value As String)
                _Splitter = value
            End Set
        End Property

        Public ReadOnly Property Lists As List(Of String())
            Get
                Return _ListsParameter
            End Get
        End Property


        Private Sub ReadCSV()
            For Each Line As String In System.IO.File.ReadAllLines(Path, Text.Encoding.Default)
                Lists.Add(Line.Split(Splitter))
            Next

            Lists.RemoveAt(0)

        End Sub

    End Class




    Public Class CSVWriter
        Private _Path As String
        Private _Splitter As String
        Private _ListsParameter As List(Of String()) = New List(Of String())


        Sub New(Path As String, ByVal List As List(Of String()), Optional ByVal Splitter As String = ";", Optional ByVal Mode As String = "append")
            Me.Path = Path
            Me.Splitter = Splitter
            Me.Lists = List
            WriteCSV(Mode)
        End Sub


        Public Property Path As String
            Get
                Return _Path
            End Get
            Set(value As String)
                _Path = value
            End Set
        End Property

        Public Property Splitter As String
            Get
                Return _Splitter
            End Get
            Set(value As String)
                _Splitter = value
            End Set
        End Property

        Public WriteOnly Property Lists As List(Of String())
            Set(ByVal value As List(Of String()))
                _ListsParameter = value
            End Set
        End Property

        Private Sub WriteCSV(ByVal Mode As String)

            Dim MaxLen As Integer = 0
            For Each LineAry As String() In _ListsParameter
                If LineAry.Length > MaxLen Then
                    MaxLen = LineAry.Length - 1
                End If
            Next

            Dim Lines As List(Of String) = New List(Of String)
            For Each LineAry As String() In _ListsParameter

                Dim Line As String = ""
                Dim LineLen As Integer = 0
                For i As Integer = 0 To LineAry.Length - 1
                    Dim LineItem As String = LineAry(i)

                    Line += LineItem + Splitter
                    LineLen = i
                Next

                If LineLen < MaxLen Then
                    Line += Splitter
                End If

                Lines.Add(Line)

            Next

            If Mode = "append" Then
                Dim x As IO.StreamWriter = System.IO.File.AppendText(Path)

                For Each Line As String In Lines
                    x.WriteLine(Line)
                Next

                x.Close()
            Else
                System.IO.File.WriteAllLines(Path, Lines.ToArray, Text.Encoding.Default)
            End If

        End Sub


    End Class

End Namespace