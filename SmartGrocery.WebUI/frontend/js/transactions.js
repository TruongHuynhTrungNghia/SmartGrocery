function initilize() {
    registerEditTransaction();
    registerDeleteTransaction();
    ConfirmedDeleteTransaction();
    AddNewCellInTable();
    SearchNewProduct();
}

function registerEditTransaction() {
    $('#edit-transaction').on("click", function (e) {
        let data = {
            id: e.currentTarget.getAttribute('data-transaction-id')
        };

        $.ajax({
            type: "GET",
            data: data,
            url: "/Transaction/Edit",
            success: function (result) {
                $modal = $(result).appendTo('body');
                $modal.modal({
                    backdrop: "static",
                    keyboard: true
                });
            },
            error: function () {
                toastr.error("There are some error while loading data to edit new product.");
            }
        });
    });
}

function registerDeleteTransaction() {
    $('.btn.delete-transaction').on('click', function (e) {
        let id = e.currentTarget.getAttribute('data-transaction-id');

        const $modal = $('#confirm-delete-transaction-modal').appendTo('body');

        $modal.modal({
            backdrop: 'static',
            keyboard: true
        });

        $('#current-transaction-id').val(id);

        removeModalAfterClosing($modal);
    });
}

function ConfirmedDeleteTransaction() {
    $('#delete-transaction-button').on('click', function () {
        var data = {
            id: $('#current-transaction-id').val()
        };

        $.ajax({
            type: "POST",
            data: data,
            url: "/Transaction/Delete",
            error: function () {
                toastr.error("There are some error while deleting data.");
            }
        })
    });
}

function removeModalAfterClosing($modal) {
    $modal.on('hidden.bs.modal', function () {
        $modal.remove();
    })
}

function AddNewCellInTable() {
    var counter = 1;

    $('#add-row').on('click', function () {
        var newRow = $("<tr>");
        var cols = "";

        cols += '<td class="input-group"><input class="form-control" id="ProductSnapshots_' + counter + '__ProductNumer" name="ProductSnapshots[' + counter + '].ProductNumer" type="text" value=""><span class="input-group-addon clickable search-by-product-number"><i class="glyphicon glyphicon-search"></i></span></td>';
        cols += '<td><input class="form-control" id = "ProductSnapshots_' + counter + '__ProductName" name = "ProductSnapshots[' + counter + '].ProductName" type = "text" value = "" ></td >';
        cols += '<td><input class="form-control" data-val="true" data-val-number="The field NumberOfSoldProduct must be a number." id = "ProductSnapshots_' + counter + '__NumberOfSoldProduct" name = "ProductSnapshots[' + counter + '].NumberOfSoldProduct" type = "text" value = "" ></td >';
        cols += '<td><input class="form-control" data-val="true" data-val-number="The field Price must be a number." id = "ProductSnapshots_' + counter + '__Price" name = "ProductSnapshots[' + counter + '].Price" type = "text" value = "" ></td >'
        cols += '<td><button class="deleteRow btn btn-danger"><i class="glyphicon glyphicon-trash center-block"></i></button></td >';
        newRow.append(cols);
        $("table.order-list").append(newRow);
        counter++;
    });

    $(document).on('click', '.deleteRow',function (event) {
        console.log(event);
        $(this).closest('tr').remove();
        counter -= 1
    });
}

function SearchNewProduct() {
    $(document).on("click", ".search-by-product-number", function (event) {
        console.log(event.target);
        let $productNumberId = $('.search-by-product-number').siblings('input');

        if ($productNumberId.val().length < 3) {
            toastr.warning("Please input at least 3 characters.");
        } else {
            SearchProductByItNumber($productNumberId)
        }
    })
}

function SearchProductByItNumber($productNumberId) {
    let data = {
        productNumber: $productNumberId.val()
    };

    $.ajax({
        type: "GET",
        data: data,
        url: "/Product/GetProductByNumber",
        success: function (result) {
            populateProductInfo($productNumberId, result);
        },
        error: function () {
            toastr.error("There are some error while loading data.");
        }
    });
}

function populateProductInfo($productNumberId, result) {
    $.each(result.result, function (key, value) {
        console.log(key + ' ' + value);
        console.log($productNumberId.attr('id'));
    });
}

//function EnableAutoComplete($productNumberId, data) {
//    $($productNumberId).autocomplete({
//        source: function (request, response) {
//            response($.map(data.result, function (val, item) {
//                return {
//                    label: val.ProductName,
//                    value: val.ProductNumber,
//                    ProductInfo: val
//                };
//            }));
//        },
//        select: function (event, ui) {

//        }
//    }).focus(function () {
//        $(this).autocomplete('search', $productNumberId.val());
//    });

//    $productNumberId.focus();
//}