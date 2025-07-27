var dtle;
document.addEventListener('DOMContentLoaded', function () {
    loaddataTable();
});
function loaddataTable() {
    dtle = $("#productTable").DataTable({
        "ajax": {
            "url": "/Admin/Product/GetAllProducts",
        },
        "columns": [
            { "data": "name" },
            { "data": "category.name" },
            { "data": "purchaseprices" },
            { "data": "salesprices" },
            { "data": "profite" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/Product/Edit/${data}" class='btn btn-success'>Edit</a>
                            <a href="/Admin/Product/DeleteProduct/${data}" class='btn btn-danger' >Delete</a>`;
                }
            }
        ]   
    });
}