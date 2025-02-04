﻿$("form#data").submit(function (e) {
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

$("#editUserForm").submit(function (e) {
    e.preventDefault();

    var phone = $("input[name='PhoneNumber']").val();
    var address = $("input[name='Address']").val();


    $.ajax({
        type: "POST",
        url: '/SelfService/Home/EditUser',
        data: { PhoneNumber: phone, Address: address },
        success: function (data) {
            location.reload();
        },
        error: function (data) {
            alert("Došlo je do greške pri čuvanju podataka.");
        }
    });
});

function enableEdit() {
    document.getElementById("phoneDisplay").style.display = "none";
    document.getElementById("addressDisplay").style.display = "none";
    document.getElementById("phoneEdit").style.display = "block";
    document.getElementById("addressEdit").style.display = "block";
    document.getElementById("editButton").style.display = "none";
    document.getElementById("saveButton").style.display = "inline-block";
}