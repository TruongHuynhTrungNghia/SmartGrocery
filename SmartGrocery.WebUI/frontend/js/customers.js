function initilize() {
    registerCreateCustomer();
    registerEditCustomer();
    registerUploadImage();
}

function registerCreateCustomer() {
    $('#create-customer').on('click', function () {
        $.ajax({
            type: "GET",
            url: "/Customer/Create",
            success: function (result) {
                $modal = $(result).appendTo('body');

                $modal.modal({
                    backdrop: 'static',
                    keyboard: 'true'
                });

                removeModalAfterClosing($modal);
            }
        });
    });
}

function registerEditCustomer() {
    $('#edit-customer').on('click', function (e) {
        let data = {
            customerNumber: e.currentTarget.getAttribute('data-customer-number')
        };

        $.ajax({
            type: "GET",
            data: data,
            url: "/Customer/Edit",
            success: function (result) {
                $modal = $(result).appendTo('body');
                $modal.modal({
                    backdrop: "static",
                    keyboard: true
                });
            },
            error: function () {
                toastr.error("There are some error while loading data to edit customer.");
            }
        });
    });
}

function removeModalAfterClosing($modal) {
    $modal.on('hidden.bs.modal', function () {
        $modal.remove();
    })
}

function registerUploadImage() {
    $('#upload-image').on('click', function () {
        let emotionToolValue = $('#EmotionTool').find(':selected').text();
        let fd = new FormData();
        let files = $('#file')[0].files[0];
        fd.append('file', files);
        fd.append('emotionTool', emotionToolValue);

        $.ajax({
            type: 'POST',
            data: fd,
            contentType: false,
            processData: false,
            cache: false,
            url: '/Customer/EvaluateCustomerEmotion',
            success: function () {
                alert('Upload image successfully.');
            }
        });
    });
}