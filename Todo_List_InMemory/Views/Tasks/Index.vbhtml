@ModelType IEnumerable(Of Todo_List_InMemory.Task)
@Code
    ViewData("Title") = "Index"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<div>
    <h2>To Do</h2>
    <div class="form-inline">
        @Using (Html.BeginForm("Search", "Tasks"))
            @Html.AntiForgeryToken()
            @Html.TextBox("text")
            @<input type="submit" value="Search" Class="btn btn-default" />
            Html.EndForm()
        End Using


    </div>
</div>


<p>
    @Html.ActionLink("New Task", "Create", New With {.class = "btn btn-default"})
</p>
<table class="table">
    <tr>
        <th>
            @Html.DisplayNameFor(Function(model) model.Description)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.DueDate)
            @Html.ActionLink("▼", "SortByDueDate", "Tasks", New With {.area = ""}, New With {.style = "color:black;"})
            @Html.ActionLink("▲", "SortByDUeDateReversed", "Tasks", New With {.style = "color:black;"})
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Priority)
            @Html.ActionLink("▼", "SortByPriority", "Tasks", New With {.style = "color:black;"})
            @Html.ActionLink("▲", "SortByPriorityReversed", "Tasks", New With {.style = "color:black;"})
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Criticality)
        </th>
        <th>
            @Html.DisplayNameFor(Function(model) model.Category.CategoryName)
        </th>
        <th></th>
    </tr>

    @For Each item In Model
        @Code
            Dim cssClass = ""
            If item.DueDate < Today Then
                cssClass = "alert-danger"
            End If
        End Code
        @<tr class=@cssClass>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Description)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.DueDate)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Priority)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Criticality)
            </td>
            <td>
                @Html.DisplayFor(Function(modelItem) item.Category.CategoryName)
            </td>
            <td>
                @Html.ActionLink("Edit", "Edit", New With {.id = item.id}) |
                @Html.ActionLink("Finish ✔", "Delete", New With {.id = item.id})
            </td>
        </tr>
            Next

</table>