
$(function () {
    $.ajax({
        type: "GET",
        url: "/api/States",
        success: addStatesToForm,
        error: ajaxErr

    });

})

function ajaxErr(xhr, options, err) {
    console.log("AJAX is in his tent, complaining. Or maybe it's Achilles. Anyway there's an error");
    console.log(err);
}

function addStatesToForm(data) {
    data.forEach(state => {
        $("#addrStates").append(`<option>${state["abbrev"]}</option>`);
    })
}
