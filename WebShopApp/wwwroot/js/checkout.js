function placeOrder() {

    var address = $("input[name='Address']").val();
    var city = $("input[name='City']").val();
    var postalCode = $("input[name='PostalCode']").val();
    var country = $("input[name='Country']").val();

    $.ajax({
        type: "POST",
        url: '/Orders/Home/PlaceOrder',
        data: { Address: address, City: city, PostalCode: postalCode, Country: country },
        success: function (data, status) {
            if (data.redirectToUrl) {
                window.location.href = data.redirectToUrl;
            }
        },
        error: function (data, status) {
        }
    });
}
