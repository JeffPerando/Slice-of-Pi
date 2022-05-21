
var latestYear = 2015;
var oldestYear = 1985;

$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/CityTrends",
        data: { cityName: $("#cityName").val(), stateAbbrev: $("#stateAbbrev").val() },
        success: function (data) {
            showChartTrend(data);
            populateTrendChartYearAmount();
        },
        error: errorOnAjax

    });

})

function toOption(content) {
    var option = document.createElement("option");
    option.textContent = content;
    option.value = content;
    return option;
}

function populateTrendChartYearAmount() {
    var select = $("#trendGraphYearSelector");

    for (let year = latestYear; year >= oldestYear; --year) {
        select.appendChild(toOption(year));
    }

}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

let graphChart = null;
function showChartTrend(data, trendSelectorYear) {

    document.querySelector('#yearSelector').innerHTML = '';
    if (isNaN(trendSelectorYear)) {
        trendSelectorYear = latestYear;
    }
    
    const years = [];
    const crimes = [];
    const propertyCrimes = [];
    const violentCrimes = [];
    const years_list = [];
    const year_removed = (trendSelectorYear - oldestYear);

    for (let i = 0; i < data.cityInfo.length; i++) {
        years_list.push(data.cityInfo[i].year);
    }

    data.cityInfo.splice(0, year_removed);

    for (let i = 0; i < data.cityInfo.length; i++) {
        years.push(data.cityInfo[i].year);
        crimes.push(data.cityInfo[i].totalOffenses);
        propertyCrimes.push(data.cityInfo[i].propertyCrimes);
        violentCrimes.push(data.cityInfo[i].violentCrimes);

    }


    years_list.reverse();
    var select = document.getElementById("yearSelector");
    for (let i = 0; i < years_list.length; i++) {
        select.appendChild(toOption(years_list[i]));
    }
    
    let stateInfo = data.stateInfo
    stateInfo.sort((a, b) => parseInt(a.year) - parseInt(b.year));
    
    var displayStateInfo = [];
    for (let i = 0; i < stateInfo.length; i++)
    {
        displayStateInfo.push(stateInfo[i].totalOffenses);
    }

    var rating_percent = ((crimes[crimes.length - 1] / displayStateInfo[displayStateInfo.length - 1]) * 100).toFixed(2);

    if (rating_percent < 1)
    {
        document.getElementById("ratingLetter").textContent = "A ("  + rating_percent + "%)";
    }
    else if (0 > rating_percent < 5)
    {
        document.getElementById("ratingLetter").textContent = "B ("  + rating_percent + "%)";
    }
    else if (5 > rating_percent < 10)
    {
        document.getElementById("ratingLetter").textContent = "C ("  + rating_percent + "%)";
    }
    else if (10 > rating_percent < 15)
    {
        document.getElementById("ratingLetter").textContent = "D ("  + rating_percent + "%)";
    }
    else if (rating_percent >= 15)
    {
        document.getElementById("ratingLetter").textContent = "F ("  + rating_percent + "%)";
    }

    const config = {
        type: 'line',
        data: {
            labels: years,
            datasets: [{
                label: 'Total Crimes Reported',
                data: crimes,
                backgroundColor: 'transparent',
                borderColor: 'rgb(75, 192, 192)',
                borderWidth: 4,
                tension: 0.2,
                fill: true,
                pointStyle: 'rectRounded',

                backgroundColor: 'rgb(100, 192, 192, 0.2)'

            },
            {
                label: 'Numbers of Property Crime Reported',
                data: propertyCrimes,
                backgroundColor: 'transparent',
                borderColor: 'rgb(0, 212, 49)',
                borderWidth: 4,
                tension: 0.2,
                fill: true,
                pointStyle: 'rectRounded',
                backgroundColor: 'rgb(0, 230, 54, 0.2)'

            },
            {
                label: 'Numbers of Violent Crime Reported',
                data: violentCrimes,
                backgroundColor: 'transparent',
                borderColor: 'rgb(188, 0, 0)',
                borderWidth: 4,
                tension: 0.2,
                fill: true,
                pointStyle: 'rectRounded',
                backgroundColor: 'rgb(236, 0, 0, 0.2)'

            },
            {
                label: 'State Crime reported',
                data: displayStateInfo,
                backgroundColor: 'transparent',
                borderColor: 'rgb(55, 102, 44)',
                borderWidth: 4,
                tension: 0.2,
                fill: true,
                pointStyle: 'rectRounded',
                backgroundColor: 'rgb(55, 102, 44, 0.2)',
                hidden: true

            }
            ]

        },
        options: {
            scales: {
                y: {
                    beginAtZero: true
                }
            }
        }
    }

    //Creats the Line Graph
    const ctx = document.getElementById('crimeTrendChart').getContext('2d');

    if (graphChart != null) {
        graphChart.destroy();
    }
    graphChart = new Chart(ctx, config);

    document.getElementById("loadingIconGraph").textContent = "";

}