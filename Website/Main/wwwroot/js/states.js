
function errorOnAjax() {
    console.log("ERROR in ajax request");
}

$(document).ready(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/States",
        success: populateStateDropdown,
        error: errorOnAjax

    });

});

function populateStateDropdown(data) {
    let select = $("#states");
    for (var i = 0; i < data.length; i++) {
        let state = data[i];
        let option = document.createElement("option");
        option.textContent = state.name;
        option.value = state.abbrev;
        option.id = state.name;
        select.append(option);
    }
}
