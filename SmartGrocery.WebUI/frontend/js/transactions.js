function initilize() {
    registerEditTransaction();
    registerDeleteTransaction();
    ConfirmedDeleteTransaction();
    AddNewCellInTable();
    SearchNewProduct();
    calculatMoneyBaseOnAmount();
    calculateTotalPrice();
    recalculateTotalPrice();
    startup();
    searchCustomer();

    $(document).ready(function () {
        $(window).keydown(function (event) {
            if (event.keyCode == 13) {
                event.preventDefault();
                return false;
            }
        });
    });

    handleScanProductNumber();
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
                toastr.error("There are some error while loading data to edit transaction.");
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
    $(document).on('click', '#add-row', function () {
        //$.ajax({
        //    type: 'GET',
        //    url: '/Transaction/GetNewProductSnapshot',
        //    success: function (result) {
        //        var newRow = $(result);
        //        var container = $('#products-table').find('tbody');
        //        container.append(newRow);
        //        updateNewRowIdandName(newRow, container);
        //    }
        //});
        AddNewProductRow();
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

    $('#ProductSnapshots_' + indexOfNewProduct + '__ProductNumber').focus();
}

function SearchNewProduct() {
    $(document).on("click", ".search-by-product-number", function (event) {
        let $productNumberId = $(this).siblings('input');

        if ($productNumberId.val().length < 3) {
            alert("Please input at least 3 characters.");
        } else {
            searchProductByItNumber($productNumberId)
        }
    })
}

function searchProductByItNumber($productNumberId) {
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
        $('#create-transacion-form').submit();
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

var logElement = document.getElementById("log");

function capture(recordedBlob) {
    var canvas = document.getElementById('canvas');
    var video = document.getElementById('video');
    canvas.width = video.videoWidth;
    canvas.height = video.videoHeight;
    canvas.getContext('2d').drawImage(video, 0, 0, video.videoWidth, video.videoHeight);

    /** Code to merge image **/
    /** For instance, if I want to merge a play image on center of existing image **/
    const playImage = new Image();
    playImage.src = 'path to image asset';
    playImage.onload = () => {
        const startX = (video.videoWidth / 2) - (playImage.width / 2);
        const startY = (video.videoHeight / 2) - (playImage.height / 2);
        canvas.getContext('2d').drawImage(playImage, startX, startY, playImage.width, playImage.height);
        canvas.toBlob() = (blob) => {
            const img = new Image();
            img.src = window.URL.createObjectUrl(blob);
        };
    };
    /** End **/
}

var streaming = false;
var width = 320;
var height = 0; 
var canvas = document.getElementById("canvas");
var video = document.getElementById("video");
let $stopButton = document.getElementById("stopButton");

function startup() {
    let startButton = document.getElementById("start");

    if (video != null) {
        video.addEventListener('canplay', function (ev) {
            if (!streaming) {
                height = video.videoHeight / (video.videoWidth / width);

                // Firefox currently has a bug where the height can't be read from
                // the video, so we will make assumptions if this happens.

                if (isNaN(height)) {
                    height = width / (4 / 3);
                }

                video.setAttribute('width', width);
                video.setAttribute('height', height);
                canvas.setAttribute('width', width);
                canvas.setAttribute('height', height);
                streaming = true;
            }
        }, false);
    }

    if (startButton != null) {
        startButton.addEventListener('click', function (event) {
            navigator.mediaDevices.getUserMedia({ video: true, audio: false })
                .then(function (stream) {
                    video.srcObject = stream;
                    video.play();
                })
                .catch(function (err) {
                    console.log("An error occurred: " + err);
                });
            captureImages();
            event.preventDefault();
        }, false);
    }
}

function stop(stream) {
    stream.getTracks().forEach(track => track.stop());
}

function log(msg) {
    logElement.innerHTML += msg + "\n";
}

function captureImages() {
    let captureTimems = 2000;

    const timer = setInterval(function () {
        captureImage();
        
        onstopvideo(timer);
    }, captureTimems);
}

//function captureImages() {
//    let captureTimeMS = 2000;
//    let totalImage = 1;

//    const timer = setInterval(function () {
//        captureImage();
//        totalImage--;

//        if (totalImage === 0) {
//            clearInterval(timer);
//        }
//    }, captureTimeMS);

//    //stop(video.srcObject);
//}

function onStopVideo(timer) {
    $('#stopButton').on("click", function () {
        clearInterval(timer);
    });
}

let currentEmotion;
let currentEmotionProbability = 0;
function captureImage() {
    let context = canvas.getContext('2d');

    if (width && height) {
        canvas.width = width;
        canvas.height = height;
        context.drawImage(video, 0, 0, width, height);

        let image = canvas.toDataURL('image/png');
        let emotionToolValue = $('#emotionTool').find(':selected').text();

        //var formData = new FormData();
        //formData.append("base64image", data);
        //formData.append("EmotionTool", emotionToolValue);
        let data = {
            base64image: image,
            emotionTool: emotionToolValue
        };

        $.ajax({
            url: '/Transaction/StoreVideo',
            data: data,
            type: 'POST',
            success: function (data) {
                log(data.result.Probability + '  ' + data.result.Emotion);
                if (currentEmotionProbability < parseFloat(data.result.Probability)) {
                    currentEmotion = data.result.Emotion;
                    currentEmotionProbability = data.result.Probability;
                }

                updateCustomerEmotionData(currentEmotion, currentEmotionProbability);
                updateEmotionalProgressBar(data.result.Emotion, data.result.Probability);
            }
        });
    }
}

function updateCustomerEmotionData(currentEmotion, currentEmotionProbability) {
    $('#CustomerEmotion').val(currentEmotion);
    $('#CustomerEmotionProbability').val(currentEmotionProbability);
}

function updateEmotionalProgressBar(currentEmotion, currentEmotionProbability) {
    refreshProccessingBar();

    switch (currentEmotion) {
        case 'NEUTRAL':
            setupProcessBar(currentEmotionProbability, 'neutral');
            break;
        case 'CALM':
            setupProcessBar(currentEmotionProbability, 'neutral');
            break;
        case 'UNKNOWN':
            setupProcessBar(currentEmotionProbability, 'neutral');
            break;
        case 'HAPPY':
            setupProcessBar(currentEmotionProbability, 'positive');
            break;
        case 'SURPRISED':
            setupProcessBar(currentEmotionProbability, 'positive');
            break;
        case 'SCARED':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
        case 'ANGRY':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
        case 'DISGUST':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
        case 'CONFUSED':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
        case 'DISGUSTED':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
        case 'FEAR':
            setupProcessBar(currentEmotionProbability, 'negative');
            break;
    }
}

function setupProcessBar(currentEmotionProbability, customerEmotionStatus) {
    const $positiveEmotionProgressBarId = $('#positive-emotion-progress-bar');
    const $negativeEmotionProgressBarId = $('#negative-emotion-progress-bar');
    const $neuralEmotionProgressBarId = $('#neural-emotion-progress-bar');

    switch (customerEmotionStatus) {
        case 'negative':
            $negativeEmotionProgressBarId.addClass('progress-bar progress-bar-warning active');
            $negativeEmotionProgressBarId.css({ "width": currentEmotionProbability + "%" });
            break;
        case 'positive':
            $positiveEmotionProgressBarId.addClass('progress-bar progress-bar-success active');
            $positiveEmotionProgressBarId.css({ "width": currentEmotionProbability + "%" });
            break;
        default:
            $neuralEmotionProgressBarId.addClass('progress-bar progress-bar-info active');
            $neuralEmotionProgressBarId.css({ "width": currentEmotionProbability + "%" });
            break;
    }
}

function searchCustomer() {
    $('#search-customer-id').on('click', function () {
        let data = {
            customerNumber: $('#CustomerId').val()
        };

        $.ajax({
            data: data,
            type: 'GET',
            url: '/Customer/SearchCustomerById',
            success: function(result) {
                $('#CustomerName').val(result.result.FirstName);
                $('#CustomerId').val(result.result.CustomerId);
            }
        });
    });
}

function refreshProccessingBar() {
    $('#positive-emotion-progress-bar').removeClass();
    $('#neural-emotion-progress-bar').removeClass();
    $('#negative-emotion-progress-bar').removeClass();
}

function handleScanProductNumber() {
    $(document).on('keydown', '.scan-product-number', function (e) {
        if (e.keyCode == 13) {

            let $productNumberId = $('#' + $(this).attr('id'));

            if ($productNumberId.val().length <= 3) {
                alert("Please input at least 3 characters.");
            } else {
                searchProductByItNumber($productNumberId)
            }

            AddNewProductRow();
        }
    });
}

function AddNewProductRow() {
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
}