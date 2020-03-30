function initilize() {
    registerCreateProduct();
    registerEditProduct();
    registerDeleteProduct();
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

        console.log(data);
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
                toastr.error("There are some error while loading data to edit new product.");
            }
        });
    });
}

function registerDeleteProduct() {
}

function removeModalAfterClosing($modal) {
    $modal.on('hidden.bs.modal', function () {
        $modal.remove();
    })
}