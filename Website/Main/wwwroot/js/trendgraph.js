
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
        select.append(toOption(year));
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

    for (let i = 0; i < data.length; i++) {
        years_list.push(data[i].year);
    }

    data.splice(0, year_removed);

    for (let i = 0; i < data.length; i++) {
        years.push(data[i].year);
        crimes.push(data[i].totalOffenses);
        propertyCrimes.push(data[i].propertyCrimes);
        violentCrimes.push(data[i].violentCrimes);

    }


    years_list.reverse();
    var select = document.getElementById("yearSelector");
    for (let i = 0; i < years_list.length; i++) {
        select.appendChild(toOption(years_list[i]));
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