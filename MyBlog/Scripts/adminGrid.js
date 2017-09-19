$(document).ready(AdminGrid());

function AdminGrid()
{
    console.log("start kendo");
    $("#admingrid").kendoGrid(
    {
        sortable: true,
        filterable: true
        });

    var grid = $("#admingrid").data("kendoGrid");
    console.log(grid);
}