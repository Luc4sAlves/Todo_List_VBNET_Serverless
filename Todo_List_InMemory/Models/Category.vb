Imports System
Imports System.Collections.Generic
Imports System.ComponentModel.DataAnnotations

Partial Public Class Category
    <Key()>
    Public Property id As Integer
    Public Property CategoryName As String
    Public Overridable Property Tasks As ICollection(Of Task) = New HashSet(Of Task)

End Class