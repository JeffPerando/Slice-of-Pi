
$(function () {
    $.ajax({
        type: "GET",
        dataType: "text",
        url: "/apiv3/forms/address",
        success: addAddressForm,
        error: ajaxErr

    });

})

function ajaxErr(xhr, options, err) {
    console.log("AJAX is in his tent, complaining. Or maybe it's Achilles. Anyway there's an error");
    console.log(err);
}

function addAddressForm(data) {
    $("#addrForm").html(data);
}
