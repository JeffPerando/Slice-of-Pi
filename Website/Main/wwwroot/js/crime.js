$(function() {
    var ourObject = {stateAbbrev:$("#stateAbbrev").val()};
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/GetCityStats",
        data: {cityName:$("#cityName").val(),stateAbbrev:$("#stateAbbrev").val()},
        success: showCityStats,
        error: errorOnAjax

    });

    //$.ajax({
    //    type: "GET",
    //    dataType: "json",
    //    url: "apiv3/FBI/StateStats",
    //    success: displayStateInformation,
    //    error: errorOnAjax

    //});

    //$.ajax({
    //    type: "GET",
    //    dataType: "json",
    //    url: "apiv3/FBI/StateList",
    //    success: populateDropDown,
    //    error: errorOnAjax

    //});
})


function errorOnAjax()
{
    console.log("ERROR in ajax request");
}

function showCityStats(data)
{
    if(data.length == 0)
    {
        window.alert("Information was not found for this city. We either do not currently have information on this city, or it does not exist.\n\nReturning to homepage.");
        window.location.href = window.location.origin;
    }
    var noOffenses = [];
    var currentYearSelected = data[0]["year"];

    $("#cityCrimeStats>tbody").empty();
    for (let i = 0; i < data.length; ++i){

        if (data[i]["totalOffenses"] == 0)
        {
            noOffenses.push(data[i]);
            continue;
        }

        let repoTR = $(
            `<tr>
                <td style="color:white; font-weight:bold;">${data[i]["offenseType"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["totalOffenses"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["actualConvictions"]}</td>
            </tr>`
        )
        $("#cityCrimeStats>tbody").append(repoTR);
        $("#cityCrimeStats").show();
    }
    console.log("These are all the offenses that have not happened in this year: ", noOffenses);

    document.getElementById("year").textContent=" (" + currentYearSelected + ")";
}

function displayStateInformation(data) {
    $("#stateCrimeTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td style="color:white; font-weight:bold;">${data[i]["state"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["actualConvictions"]}</td>
            </tr>`
        )
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
    $("#stateCrimeTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td style="color:white; font-weight:bold;">${data[i]["offenseType"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["totalOffenses"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["actualConvictions"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["year"]}</td>
            </tr>`
        )
        $("#stateCrimeTable>tbody").append(repoTR);
        $("#stateCrimeTable").show();
    }
}
}