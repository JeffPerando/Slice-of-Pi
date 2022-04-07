
$.ajax({
    type: "GET",
    dataType: "json",
    url: "/apiv3/FBI/StateList",
    success: populateDropDown,
    error: errorOnAjax

});

function fetchCrimeStats(btn) {
    $("#spinnyBoi").show();
    $("#stateCrimeDiv").removeAttr("hidden");
    let form = $(btn).parents('form');

    let formData = form.serialize();

    console.log(`Form data: ${formData}`);

    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/apiv3/FBI/StateCrimeStats`,
        data: form.serialize(),
        success: showStateStats,
        error: errorOnAjax

    });
}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function populateDropDown(data) {
    var select = document.getElementById("stateAbbrev");
    for (var i = 0; i < data.length; i++) {
        var option = data[i];
        var element = document.createElement("option");
        element.textContent = option;
        element.value = option;
        select.appendChild(element);
    }
}

function showStateStats(data) {
    $("#spinnyBoi").removeAttr("hidden");
    console.log(data);

    $("#stateCrimeHeader").html(`Here Are the Crime Statistics For ${data["state"]} in ${data["year"]}`);

    $("#stateCrimeTable>tbody").html(`<tr>
            <td style="color:white; font-weight:bold;">${data["population"]}</td>
            <td style="color:white; font-weight:bold;">${data["violentCrimes"]}</td>
            <td style="color:white; font-weight:bold;">${data["homicide"]}</td>
            <td style="color:white; font-weight:bold;">${data["rapeLegacy"]}</td>
            <td style="color:white; font-weight:bold;">${data["rapeRevised"]}</td>
            <td style="color:white; font-weight:bold;">${data["robbery"]}</td>
            <td style="color:white; font-weight:bold;">${data["assault"]}</td>
            <td style="color:white; font-weight:bold;">${data["propertyCrimes"]}</td>
            <td style="color:white; font-weight:bold;">${data["burglary"]}</td>
            <td style="color:white; font-weight:bold;">${data["larceny"]}</td>
            <td style="color:white; font-weight:bold;">${data["motorVehicleTheft"]}</td>
            <td style="color:white; font-weight:bold;">${data["arson"]}</td>
        </tr>`);
    $("#spinnyBoi").hide();
    window.scrollTo(0, document.body.scrollHeight);
}
