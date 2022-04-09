
$(function() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/GetCityStats",
        data: { cityName: $("#cityName").val(), stateAbbrev: $("#stateAbbrev").val() },
        success: showCityStats,
        error: errorOnAjax

    });

})

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function showCityStats(data) {
    if (data.length == 0) {
        window.alert("Information was not found for this city. We either do not currently have information on this city, or it does not exist.\n\nReturning to homepage.");
        window.location.href = window.location.origin;
    }
    var noOffenses = [];
    var currentYearSelected = data[0]["year"];

    $("#cityCrimeStats>tbody").empty();
    for (let i = 0; i < data.length; ++i) {

        if (data[i]["totalOffenses"] == 0) {
            noOffenses.push(data[i]);
            continue;
        }
        if (data[i]["offenseType"] == "rape-legacy")
        {
            data[i]["offenseType"] = "rape (Legacy)";
        }

        let repoTR = $(
            `<tr>
                <td>${capitalize(data[i]["offenseType"].replaceAll("-", " "))}</td>
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
            
            li.appendChild(document.createTextNode("> " + (capitalize(offense))));
            ul.appendChild(li);
        }
        
    }


    document.getElementById("year").textContent = " (" + currentYearSelected + ")";

    // Creates the Pi Graph
    showChartPercentage(data);
}

function showChartPercentage(data){

    let crimes = data;
    const crimeTypes = [];
    const amountCrimes = [];
    const percentagesCrimes = []
    //Sorts the list by offense type kinda like a Linq 
    crimes.sort((a,b) => a.offenseType.localeCompare(b.offenseType));
    console.log(crimes);

    for (let i = 0; i < crimes.length; i++)
    {
        if (crimes[i]["totalOffenses"] <= 0)
        {
            continue;
        }
        crimeTypes.push(capitalize(crimes[i]["offenseType"]));
        amountCrimes.push(crimes[i]["totalOffenses"]);
    }
    
    //Gets sum of amountCrimes
    var sum = amountCrimes.reduce(function(a, b){
        return a + b;
    }, 0);

    for (let i = 0; i < crimes.length; i++)
    {
        percentagesCrimes.push(((crimes[i]["totalOffenses"] / sum) * 100).toFixed(1));
    }

    console.log(amountCrimes);
    console.log(crimeTypes);
    console.log(percentagesCrimes);

    const ctx = document.getElementById('crimeTrendPercentage').getContext('2d');
    const myChart = new Chart(ctx, {
        type: 'pie',
        options: {
        },
        data: {
            labels: crimeTypes,
            datasets: [{
              label: 'Crimes percentages in this area.',
              data: percentagesCrimes,
              backgroundColor: [
                'rgb(255, 99, 132)',
                'rgb(54, 162, 235)',
                'rgb(56, 32, 86)',
                'rgb(29, 205, 125)',
                'rgb(32, 194, 39)',
                'rgb(220, 205, 100)',
                'rgb(212, 20, 136)',
                'rgb(239, 130, 93)',
                'rgb(93, 130, 86)',
                'rgb(159, 205, 86)',
                'rgb(0, 205, 143)',
              ],
              hoverOffset: 4
            }]
          },
    });
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
