
$(function() {
<<<<<<< HEAD
    var ourObject = {stateAbbrev:$("#stateAbbrev").val()};
=======
>>>>>>> dev/dev
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/GetCityStats",
        data: {cityName:$("#cityName").val(),stateAbbrev:$("#stateAbbrev").val()},
        success: showCityStats,
        error: errorOnAjax

    });

<<<<<<< HEAD
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
=======
>>>>>>> dev/dev
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

    $("#cityCrimeStats>tbody").empty();
    for (let i = 0; i < data.length; ++i){
        let repoTR = $(
            `<tr>
                <td>${data[i]["offenseType"]}</td>
                <td>${data[i]["totalOffenses"]}</td>
                <td>${data[i]["actualConvictions"]}</td>
                <td>${data[i]["year"]}</td>
            </tr>`
        )
        $("#cityCrimeStats>tbody").append(repoTR);
        $("#cityCrimeStats").show();
    }
}
<<<<<<< HEAD

function displayStateInformation(data) {
    $("#stateCrimeTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td>${data[i]["state"]}</td>
                <td>${data[i]["actualConvictions"]}</td>
            </tr>`
        )
        $("#safestStatesTable>tbody").append(repoTR);
        $("#safestStatesTable").show();
    }
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
    $("#stateCrimeTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td>${data[i]["offenseType"]}</td>
                <td>${data[i]["totalOffenses"]}</td>
                <td>${data[i]["actualConvictions"]}</td>
                <td>${data[i]["year"]}</td>
            </tr>`
        )
        $("#stateCrimeTable>tbody").append(repoTR);
        $("#stateCrimeTable").show();
    }
}
=======
>>>>>>> dev/dev
