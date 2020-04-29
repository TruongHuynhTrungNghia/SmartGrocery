function initilize() {
    registerCreateProduct();
    registerEditProduct();
    registerDeleteProduct();
    ConfirmedDeleteProduct();
}

function registerCreateProduct() {
    $("#create-product").on("click", function () {
        $.ajax({
            type: "GET",
            url: "/Product/Create",
            success: function (result) {
                $modal = $(result).appendTo('body');

                $modal.modal({
                    backdrop: "static",
                    keyboard: "true"
                });

                removeModalAfterClosing($modal);
            },
            error: function () {
                toastr.error("There are some error while loading data to create new product.");
            }
        });
    });
}

function registerEditProduct() {
    $('.btn.edit-product').on("click", function (e) {
        let data = {
            id: e.currentTarget.getAttribute('data-product-id')
        };

        $.ajax({
            type: "GET",
            data: data,
            url: "/Product/Edit",
            success: function (result) {
                $modal = $(result).appendTo('body');
                $modal.modal({
                    backdrop: "static",
                    keyboard: true
                });
            },
            error: function () {
                toastr.error("There are some error while loading data to edit product.");
            }
        });
    });
}

function registerDeleteProduct() {
    $('.btn.delete-product').on('click', function (e) {
        let id = e.currentTarget.getAttribute('data-product-id');

        const $modal = $('#confirm-delete-product-modal').appendTo('body');

        $modal.modal({
            backdrop: 'static',
            keyboard: true
        });

        $('#current-product-id').val(id);

        removeModalAfterClosing($modal);
    });
}

function ConfirmedDeleteProduct() {
    $('#delete-product-button').on('click', function () {
        var data = {
            id: $('#current-product-id').val()
        };

        $.ajax({
            type: "POST",
            data: data,
            url: "/Product/Delete",
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