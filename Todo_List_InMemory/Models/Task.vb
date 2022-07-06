Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Partial Public Class Task
    <Key()>
    Public Property id As Integer
    Public Property Description As String
    Public Property DueDate As Nullable(Of Date)
    Public Property Priority As Nullable(Of Byte)
    Public Property Criticality As String
    Public Property CategoryId As Nullable(Of Integer)

    Public Overridable Property Category As Category

End Class