function placeOrder() {
    $.ajax({
        type: "POST",
        url: '/Orders/Home/PlaceOrder',
        success: function (data, status) {
            //location.reload();
        },
        error: function (data, status) {
        }
    });
}