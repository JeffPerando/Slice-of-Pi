
$(document).ready(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/States",
        success: populateStateDropdown,
        error: errorOnAjax

    });
});

function disableForms(off) {
    $(":input").prop('disabled', off ? 'disabled' : '');
}

function fetchCities() {
    let state = $("#states")[0].value;

    if (state == "") {
        return;
    }

    disableForms(true);
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/GetCitiesIn",
        data: {
            stateID: state
        },
        success: populateCityDropdown,
        error: errorOnAjax

    });

}

function populateStateDropdown(data) {
    let select = $("#states");
    for (var i = 0; i < data.length; i++) {
        let state = data[i]["name"];
        select.append(`<option value=${i}>${state}</option>`);
    }
    select.change(fetchCities);
}

function populateCityDropdown(data) {
    //$("#spinnyBoi").hide();
    let select = $("#cities");

    //clear the HTML (most other ways don't seem to work)
    select.html(`<option value="">Choose a city</option>`);
    for (var i = 0; i < data.length; i++) {
        let city = data[i];
        select.append(`<option value="${city}">${city}</option>`);
    }

    disableForms(false);

}
