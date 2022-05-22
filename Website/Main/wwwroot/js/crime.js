
$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/GetCityStats",
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
        window.alert("Information was not found for this location. We either do not currently have information on this location, or it does not exist.\n\nReturning to Search.");
        window.history.go(-1);
    }
    internationalNumberFormat = new Intl.NumberFormat('en-US')
    var noOffenses = [];
    var currentYearSelected = data[0]["year"];
    data = data.reverse();


    $("#cityCrimeStats>tbody").empty();
    for (let i = 0; i < data.length; ++i) {

        if (data[i]["totalOffenses"] == 0) {
            noOffenses.push(data[i]);
            continue;
        }

        let repoTR = document.createElement("tr");

        let offenseType = document.createElement("td");
        let offenses = document.createElement("td");
        let convictions = document.createElement("td");

        offenseType.textContent = capitalize(data[i]["offenseType"].replaceAll("-", " "));
        offenses.textContent = internationalNumberFormat.format(data[i]["totalOffenses"]);
        convictions.textContent = internationalNumberFormat.format(data[i]["actualConvictions"]);

        repoTR.appendChild(offenseType);
        repoTR.appendChild(offenses);
        repoTR.appendChild(convictions);

        $("#cityCrimeStats>tbody").append(repoTR);

    }

    $("#cityCrimeStats").show();

    if (noOffenses.length > 0) {
        $("#cityCrimeStatsNoCrime").empty();
        for (let i = 0; i < noOffenses.length; ++i) {
            document.getElementById("cityCrimeNoCrimeheader").textContent = "Crimes not committed: ";
            if (noOffenses[i]["offenseType"] == "human-trafficing")
            {
                noOffenses[i]["offenseType"] = "Human-Trafficking";
            }
            var offense = noOffenses[i]["offenseType"].replaceAll("-", " ");
            var ul = document.getElementById("cityCrimeStatsNoCrime");
            if (offense == "human trafficing") {
                offense = "Human Trafficking"
            }
            var li = document.createElement("li")

            li.append(document.createTextNode("> " + (capitalize(offense))));
            ul.append(li);
        }

    }


    document.getElementById("year").textContent = ` (${currentYearSelected})`;

    // Creates the Pi Graph
    showChartPercentage(data);

    document.getElementById("loadingIcon").textContent = "";
}

function showHomeCityStats(data) {
    console.log(data);
    internationalNumberFormat = new Intl.NumberFormat('en-US')
    var noOffenses = [];
    var currentYearSelected = data[0]["year"];
    data = data.reverse();


    $("#cityCrimeStatsDefaultHome>tbody").empty();
    for (let i = 0; i < data.length; ++i) {

        if (data[i]["totalOffenses"] == 0) {
            noOffenses.push(data[i]);
            continue;
        }

        let repoTR = document.createElement("tr");

        let offenseType = document.createElement("td");
        let offenses = document.createElement("td");
        let convictions = document.createElement("td");

        offenseType.textContent = capitalize(data[i]["offenseType"].replaceAll("-", " "));
        offenses.textContent = internationalNumberFormat.format(data[i]["totalOffenses"]);
        convictions.textContent = internationalNumberFormat.format(data[i]["actualConvictions"]);

        repoTR.appendChild(offenseType);
        repoTR.appendChild(offenses);
        repoTR.appendChild(convictions);

        $("#cityCrimeStatsDefaultHome>tbody").append(repoTR);

    }

    $("#cityCrimeStatsDefaultHome").show();

    if (noOffenses.length > 0) {
        $("#cityCrimeStatsNoCrimeDefaultHome").empty();
        for (let i = 0; i < noOffenses.length; ++i) {
            document.getElementById("cityCrimeNoCrimeheaderDefaultHome").textContent = "Crimes not committed: ";
            if (noOffenses[i]["offenseType"] == "human-trafficing")
            {
                noOffenses[i]["offenseType"] = "Human-Trafficking";
            }
            var offense = noOffenses[i]["offenseType"].replaceAll("-", " ");
            var ul = document.getElementById("cityCrimeStatsNoCrimeDefaultHome");
            if (offense == "human trafficing") {
                offense = "Human Trafficking"
            }
            var li = document.createElement("li")

            li.append(document.createTextNode("> " + (capitalize(offense))));
            ul.append(li);
        }

    }


    document.getElementById("year").textContent = ` (${currentYearSelected})`;


    document.getElementById("loadingIcon").textContent = "";
}

let myChart = null;
function showChartPercentage(data) {
    let crimes = data;
    let crimeTypes = [];
    let amountCrimes = [];
    let percentagesCrimes = []
    //Sorts the list by offense type kinda like a Linq 
    crimes.sort((a, b) => a.offenseType.localeCompare(b.offenseType));

    for (let i = 0; i < crimes.length; i++) {
        if (crimes[i]["totalOffenses"] <= 0) {
            continue;
        }
        crimeTypes.push(capitalize(crimes[i]["offenseType"]));
        amountCrimes.push(crimes[i]["totalOffenses"]);
    }

    //Gets sum of amountCrimes
    var sum = amountCrimes.reduce(function (a, b) {
        return a + b;
    }, 0);

    for (let i = 0; i < crimes.length; i++) {
        if (((crimes[i]["totalOffenses"] / sum) * 100).toFixed(1) <= 0) {
            continue
        }
        percentagesCrimes.push(((crimes[i]["totalOffenses"] / sum) * 100).toFixed(1));
    }
    const config = {
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
    }

    const ctx = document.getElementById('crimeTrendPercentage').getContext('2d');

    if (myChart != null) {
        myChart.destroy();
    }
    myChart = new Chart(ctx, config);

}

function displayStateInformation(data) {
    $("#stateCrimeTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = document.createElement("tr");

        let state = document.createElement("td");
        let convictions = document.createElement("td");

        state.textContent = data[i]["state"];
        convictions.textContent = data[i]["actualConvictions"];

        repoTR.appendChild(state);
        repoTR.appendChild(convictions);

        $("#safestStatesTable>tbody").append(repoTR);

    }

    $("#safestStatesTable").show();

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
    const lower = offense.toLowerCase();
    return offense.charAt(0).toUpperCase() + lower.slice(1);
}
