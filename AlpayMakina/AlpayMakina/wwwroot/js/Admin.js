function LoadUrlToData(url, data, callback) {
    url = encodeURI(url);

    $.ajax({
        url: url,
        dataType: 'json',
        type: 'post',
        data: data,
        success: function (result) {
            UnBlockUI();
            //DocListInit();
            result.state = "success";
            callback(result);
        },
        error: function (result) {
            UnBlockUI();
            //DocListInit();
            result.state = "error";
            callback(result);
        }

    });
}
function LoadUrlToDivFromHistory(url, div) {
    url = encodeURI(url);
    //BlockUI("Please wait...");
    $("#" + div).html("");
    $("#" + div).load(url, function () {
        UnBlockUI();
        DocListInit();
        if (div == "divCountryDeliveryList") {
            CalculateTotalCountryVolume();
        }
    });
}
function LoadUrlToDiv(url, div) {
    //Metronic.blockUI({
    //    message: "Lütfen bekleyiniz...",
    //    boxed: true
    //});
    $("#" + div).html("");
    $("#" + div).load(url, function () {
        Metronic.unblockUI();
    });
}
function ReloadF() {
    window.location.reload();
}
$('.btnConfirm').on('click', function (event) {
    var confirmation = confirm("İşleme Devam Etmek İstiyor Musunuz?");
    if (!confirmation) {
        event.preventDefault(); // Form submit işlemini durdur
    }
});
