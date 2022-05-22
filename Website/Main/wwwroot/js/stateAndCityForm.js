
function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function disableForms(off) {
    $(":input").prop('disabled', off ? 'disabled' : '');
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
        select.append(option);
    }
    select.change(fetchCities);
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
            stateAbbrev: state
        },
        success: populateCityDropdown,
        error: errorOnAjax

    });

}

function populateCityDropdown(data) {
    //$("#spinnyBoi").hide();
    let select = $("#cities");
    //clear the HTML (most other ways don't seem to work)
    select.html(`<option value="">Select a city</option>`);
    for (var i = 0; i < data.length; i++) {
        let city = data[i];
        let option = document.createElement("option");
        option.textContent = city;
        option.value = city;
        select.append(option);
    }

    disableForms(false);

}
