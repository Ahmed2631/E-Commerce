var dtle;
document.addEventListener('DOMContentLoaded', function () {
    loaddataTable();
});
function loaddataTable() {
    dtle = $("#productTable").DataTable({
        "ajax": {
            "url": "/Admin/OrderDetails/AllOrders",
        },
        "columns": [
            { "data": "id" },
            { "data": "userName" },
            { "data": "phoneNumber" },
            { "data": "applicationUser.email" },
            { "data": "orderStatus" },
            { "data": "totalPrice" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/Admin/OrderDetails/Details?orderId=${data}" class='btn btn-success'>Details</a>`
                }
            }
        ]   
    });
}