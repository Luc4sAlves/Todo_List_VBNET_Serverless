@ModelType Todo_List_InMemory.Task
@Code
    ViewData("Title") = "Delete"
    Layout = "~/Views/Shared/_Layout.vbhtml"
End Code

<h2>Finish Task</h2>

<h3>Is this task finished?</h3>
<div>
    <hr />
    <dl class="dl-horizontal">
        <dt>
            @Html.DisplayNameFor(Function(model) model.Description)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Description)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.DueDate)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.DueDate)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Priority)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Priority)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Criticality)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Criticality)
        </dd>

        <dt>
            @Html.DisplayNameFor(Function(model) model.Category.CategoryName)
        </dt>

        <dd>
            @Html.DisplayFor(Function(model) model.Category.CategoryName)
        </dd>

    </dl>
    @Using (Html.BeginForm())
        @Html.AntiForgeryToken()

        @<div class="form-actions no-color">
            <input type="submit" value="Yes, finish" class="btn btn-default" /> |
            @Html.ActionLink("Back to List", "Index")
        </div>
    End Using
</div>