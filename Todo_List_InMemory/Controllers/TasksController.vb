Imports System
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.Entity
Imports System.Linq
Imports System.Net
Imports System.Web
Imports System.Web.Mvc
Imports Todo_List_InMemory
Imports Todo_List_InMemory.Data

Namespace Controllers
    Public Class TasksController
        Inherits System.Web.Mvc.Controller

        Private db As New Todo_List_InMemoryContext

        ' GET: Tasks
        Function Index() As ActionResult
            Dim tasks = db.Tasks.Include(Function(t) t.Category)
            Return View(tasks.ToList())
        End Function

        ' GET: Tasks/Details/5
        Function Details(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim task As Task = db.Tasks.Find(id)
            If IsNothing(task) Then
                Return HttpNotFound()
            End If
            Return View(task)
        End Function

        ' GET: Tasks/Create
        Function Create() As ActionResult
            ViewBag.CategoryId = New SelectList(db.Categories, "id", "CategoryName")
            Return View()
        End Function

        ' POST: Tasks/Create
        'Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        'Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Create(<Bind(Include:="id,Description,DueDate,Priority,Criticality,CategoryId")> ByVal task As Task) As ActionResult
            If ModelState.IsValid Then
                db.Tasks.Add(task)
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CategoryId = New SelectList(db.Categories, "id", "CategoryName", task.CategoryId)
            Return View(task)
        End Function

        ' GET: Tasks/Edit/5
        Function Edit(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim task As Task = db.Tasks.Find(id)
            If IsNothing(task) Then
                Return HttpNotFound()
            End If
            ViewBag.CategoryId = New SelectList(db.Categories, "id", "CategoryName", task.CategoryId)
            Return View(task)
        End Function

        ' POST: Tasks/Edit/5
        'Para proteger-se contra ataques de excesso de postagem, ative as propriedades específicas às quais deseja se associar. 
        'Para obter mais detalhes, confira https://go.microsoft.com/fwlink/?LinkId=317598.
        <HttpPost()>
        <ValidateAntiForgeryToken()>
        Function Edit(<Bind(Include:="id,Description,DueDate,Priority,Criticality,CategoryId")> ByVal task As Task) As ActionResult
            If ModelState.IsValid Then
                db.Entry(task).State = EntityState.Modified
                db.SaveChanges()
                Return RedirectToAction("Index")
            End If
            ViewBag.CategoryId = New SelectList(db.Categories, "id", "CategoryName", task.CategoryId)
            Return View(task)
        End Function

        ' GET: Tasks/Delete/5
        Function Delete(ByVal id As Integer?) As ActionResult
            If IsNothing(id) Then
                Return New HttpStatusCodeResult(HttpStatusCode.BadRequest)
            End If
            Dim task As Task = db.Tasks.Find(id)
            If IsNothing(task) Then
                Return HttpNotFound()
            End If
            Return View(task)
        End Function

        ' POST: Tasks/Delete/5
        <HttpPost()>
        <ActionName("Delete")>
        <ValidateAntiForgeryToken()>
        Function DeleteConfirmed(ByVal id As Integer) As ActionResult
            Dim task As Task = db.Tasks.Find(id)
            db.Tasks.Remove(task)
            db.SaveChanges()
            Return RedirectToAction("Index")
        End Function

        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If (disposing) Then
                db.Dispose()
            End If
            MyBase.Dispose(disposing)
        End Sub

        Function SortByPriority(ByVal text As String) As ActionResult
            Dim tasks = db.Tasks.OrderBy(Function(t) t.Priority).ToList()
            Return View("Index", tasks)
        End Function

        Function SortByPriorityReversed(ByVal text As String) As ActionResult
            Dim tasks = db.Tasks.OrderByDescending(Function(t) t.Priority).ToList()
            Return View("Index", tasks)
        End Function


        Function SortByDueDate(ByVal text As String) As ActionResult
            Dim tasks = db.Tasks.OrderBy(Function(t) t.DueDate).ToList()
            Return View("Index", tasks)
        End Function

        Function SortByDueDateReversed(ByVal text As String) As ActionResult
            Dim tasks = db.Tasks.OrderByDescending(Function(t) t.DueDate).ToList()
            Return View("Index", tasks)
        End Function


        <HttpPost()>
        <ActionName("Search")>
        <ValidateAntiForgeryToken()>
        Function Search(ByVal text As String) As ActionResult
            Dim tasks = db.Tasks.Where(Function(t) t.Description.Contains(text) Or t.Category.CategoryName.Contains(text) Or t.Criticality.Contains(text)).ToList()

            'search by due date and append it to tasks
            For Each t In db.Tasks
                'this is separated cuz ToString wasn't running inside Where
                Dim dateString = t.DueDate.ToString()
                If dateString.Contains(text) And Not tasks.Contains(t) Then
                    tasks.Add(t)
                End If
            Next

            Return View("Index", tasks)

        End Function

    End Class
End Namespace
