
$(function () {
    var ourObject = { stateAbbrev: $("#stateAbbrev").val() };
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateCrimeStats",
        data: { stateAbbrev: $("#stateAbbrev").val() },
        success: showStateStats,
        error: errorOnAjax

    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateList",
        success: populateDropDown,
        error: errorOnAjax

    });
})


function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function updateSlider(slideAmount)
{

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

    console.log(data);
    $("#stateCrimeTable>tbody").empty();

        let repoTR = $(
            `<tr>
                <td>${data["state_abbr"]}</td>
                <td>${data["year"]}</td>
                <td>${data["population"]}</td>
                <td>${data["violent_crime"]}</td>
                <td>${data["homicide"]}</td>
                <td>${data["rape_legacy"]}</td>
                <td>${data["rape_revised"]}</td>
                <td>${data["robbery"]}</td>
                <td>${data["aggravated_assault"]}</td>
                <td>${data["property_crime"]}</td>
                <td>${data["burglary"]}</td>
                <td>${data["larceny"]}</td>
                <td>${data["motor_vehicle_theft"]}</td>
                <td>${data["arson"]}</td>
            </tr>`
        )
        $("#stateCrimeTable>tbody").append(repoTR);
        $("#stateCrimeTable").show();
    
}