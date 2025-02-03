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

function enableEdit() {
    document.getElementById("phoneDisplay").style.display = "none";
    document.getElementById("addressDisplay").style.display = "none";
    document.getElementById("phoneEdit").style.display = "block";
    document.getElementById("addressEdit").style.display = "block";
    document.getElementById("editButton").style.display = "none";
    document.getElementById("saveButton").style.display = "inline-block";
}