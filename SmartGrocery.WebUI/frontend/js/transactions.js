function initilize() {
    registerEditTransaction();
    registerDeleteTransaction();
    ConfirmedDeleteTransaction();
    AddNewCellInTable();
    SearchNewProduct();
    calculatMoneyBaseOnAmount();
    calculateTotalPrice();
    recalculateTotalPrice();
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
    $(document).on('click', '#add-row', function (e) {
        $.ajax({
            type: 'GET',
            url: '/Transaction/GetNewProductSnapshot',
            success: function (result) {
                var newRow = $(result);
                var container = $('#products-table').find('tbody');
                container.append(newRow);
                updateNewRowIdandName(newRow, container);
            }
        });
    });

    $(document).on('click', '.deleteRow', function (event) {
        let container = $(this).closest('tbody');
        $(this).closest('tr').remove();

        let allRows = container.find('tr');
        for (let i = 0; i < allRows.length; i++) {
            let row = $(allRows[i]);

            row.find('input').each(function () {
                let currentId = $(this).attr('name').split('.')[1];

                $(this).attr('id', 'ProductSnapshots_' + i + '__' + currentId);
                $(this).attr('name', 'ProductSnapshots[' + i + '].' + currentId);
            });
        }
    });
}

function updateNewRowIdandName(newRow, container) {
    let indexOfNewProduct = container.find('tr').length - 1;

    newRow.find('input').each(function () {
        let currentId = $(this).attr('id');

        $(this).attr('id', 'ProductSnapshots_' + indexOfNewProduct + '__' + currentId);
        $(this).attr('name', 'ProductSnapshots[' + indexOfNewProduct + '].' + currentId);
    });
}

function SearchNewProduct() {
    $(document).on("click", ".search-by-product-number", function (event) {
        console.log(event.target);
        let $productNumberId = $(this).siblings('input');

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
    let prefix = $productNumberId.attr('name').split('.')[0];

    $.each(result.result, function (key, value) {
        console.log(key + ' ' + value);
        console.log($productNumberId.attr('id'));
        $('input[name="' + prefix + '.' + key + '"]').val(value);
    });
}

function calculatMoneyBaseOnAmount() {
    $(document).on('input', ".amount-input", function () {
        let currentAmount = parseFloat($(this).val());
        let totalAmount = parseFloat($(this).parent().closest('tr').find('input[name*=TotalProduct]').val());

        if (!isNaN(currentAmount)) {
            if (currentAmount <= totalAmount) {
                let currentPrice = parseFloat($(this).parent().closest('tr').find('input[name*=Price]').val());

                $(this).parent().closest('tr').find('.price-input').val(currentAmount * currentPrice);
                $(this).parent().closest('tr').find('.price-input').trigger('change');
            }
            else {
                alert("The remaining amount of product is " + totalAmount);
            }
        }
    });
}

function calculateTotalPrice() {
    $('#calculate-total-price').on('click', function () {
        let total = 0;

        $(document).find('.price-input').each(function (index, value) {
            total += parseFloat(value.value);
        });

        $('#Amount').val(total);
    })
}

function recalculateTotalPrice() {
    $(document).on('click', '#recalculate-total-price', function () {
        let total = 0;

        $(document).find('.price-input').each(function (index, value) {
            total += parseFloat(value.value);
        });

        $('#Amount').val(total);
    });
}