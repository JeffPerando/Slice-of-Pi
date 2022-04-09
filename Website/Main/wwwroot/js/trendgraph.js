$(function() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/GetCityTrends",
        data: {cityName:$("#cityName").val(),stateAbbrev:$("#stateAbbrev").val()},
        success: showChartTrend,
        error: errorOnAjax

    });

})

function errorOnAjax() {
    console.log("ERROR in ajax request");
}


function showChartTrend(data){
    
    
    const years = [];
    const crimes = [];
    const propertyCrimes = [];
    const violentCrimes = [];
    const years_list = [];


    for(let i = 0; i < data.totalTrends.length; i++)
    {
        years.push(data.totalTrends[i]["year"]);
        crimes.push(data.totalTrends[i]["totalOffenses"]);
        propertyCrimes.push(data.propertyTrends[i]["totalOffenses"]);
        violentCrimes.push(data.violentTrends[i]["totalOffenses"]);
        years_list.push(data.totalTrends[i]["year"]);
    }
    years_list.reverse();
    var select = document.getElementById("yearSelector");
    for (let i = 0; i < years_list.length; i++)
    {
        
        var option = years_list[i];
        var element = document.createElement("option");
        element.textContent = option;
        element.value = option;
        select.appendChild(element);
    }

    //Creats the Line Graph
    const ctx = document.getElementById('crimeTrendChart').getContext('2d');
    const myChart = new Chart(ctx, {
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
    });

    
}