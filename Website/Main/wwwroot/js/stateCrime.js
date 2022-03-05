
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
        url: "/apiv3/FBI/StateStats",
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
                <td style="color:white;">${data["state_abbr"]}</td>
                <td style="color:white;">${data["year"]}</td>
                <td style="color:white;">${data["population"]}</td>
                <td style="color:white;">${data["violent_crime"]}</td>
                <td style="color:white;">${data["homicide"]}</td>
                <td style="color:white;">${data["rape_legacy"]}</td>
                <td style="color:white;">${data["rape_revised"]}</td>
                <td style="color:white;">${data["robbery"]}</td>
                <td style="color:white;">${data["aggravated_assault"]}</td>
                <td style="color:white;">${data["property_crime"]}</td>
                <td style="color:white;">${data["burglary"]}</td>
                <td style="color:white;">${data["larceny"]}</td>
                <td style="color:white;">${data["motor_vehicle_theft"]}</td>
                <td style="color:white;">${data["arson"]}</td>
            </tr>`
        )
        $("#stateCrimeTable>tbody").append(repoTR);
        $("#stateCrimeTable").show();
    
}
