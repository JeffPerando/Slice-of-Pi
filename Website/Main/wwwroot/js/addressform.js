
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

function addStatesToForm(states) {
    for (const state of states) {
        var element = document.createElement("option");
        element.textContent = state["abbrev"];
        element.value = state["abbrev"];
        $("#addrStates").appendChild(element);
    }
}
