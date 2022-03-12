
$(function() {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/GetCityStats",
        data: {cityName:$("#cityName").val(),stateAbbrev:$("#stateAbbrev").val()},
        success: showCityStats,
        error: errorOnAjax

    });



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
                <td>${data[i]["offenseType"]}</td>
                <td>${data[i]["totalOffenses"]}</td>
                <td>${data[i]["actualConvictions"]}</td>
            </tr>`
        )
        
        $("#cityCrimeStats>tbody").append(repoTR);
        $("#cityCrimeStats").show();
    }

    if (noOffenses.length > 0)
    {
        $("#cityCrimeStatsNoCrime").empty();
        for (let i = 0; i < noOffenses.length; ++i)
        {
            document.getElementById("cityCrimeNoCrimeheader").textContent="Crimes not committed: ";
            var offense = noOffenses[i]["offenseType"]
            var ul = document.getElementById("cityCrimeStatsNoCrime");
            var li = document.createElement("li")
            
            li.appendChild(document.createTextNode(capitalize(offense)));
            ul.appendChild(li);
        }
        
    }


    document.getElementById("year").textContent=" (" + currentYearSelected + ")";
}

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

function populateYear(data) {
    var select = document.getElementById("yearSelector");
    for (var i = 0; i < data.length; i++) {
        var option = data[i];
        var element = document.createElement("option");
        element.textContent = option;
        element.value = option;
        select.appendChild(element);
    }
}

function capitalize(offense) {
    const lower = offense.toLowerCase()
    return offense.charAt(0).toUpperCase() + lower.slice(1)
}
