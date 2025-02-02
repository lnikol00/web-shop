$("form#data").submit(function (e) {
    e.preventDefault();
    var formData = new FormData(this);

    $.ajax({
        url: $(this).attr("action"),
        type: 'POST',
        data: formData,
        success: function (data) {
            location.reload();
        },
        cache: false,
        contentType: false,
        processData: false
    });
});

function deleteImage() {
    $.ajax({
        type: "POST",
        url: '/SelfService/Home/DeleteProfleImage',
        success: function (data, status) {
            location.reload();
        },
        error: function (data, status) {
        }
    });
}