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

function showChartTrend(data){
    
    const years = [];
    const crimes = [];

    for(let i = 0; i < data.length; i++)
    {
        years.push(data[i]["year"]);
        crimes.push(data[i]["totalOffenses"]);
    }

    var select = document.getElementById("yearSelector");
    for (let i = 0; i < years.length; i++)
    {
        
        var option = years[i];
        var element = document.createElement("option");
        element.textContent = option;
        element.value = option;
        select.appendChild(element);
    }
    
    const ctx = document.getElementById('crimeTrendChart').getContext('2d');
    const myChart = new Chart(ctx, {
    type: 'line',
    data: {
        labels: years,
        datasets: [{
            label: 'Amount of Crime',
            data: crimes,
            backgroundColor: 'transparent',
            borderColor: 'rgb(75, 192, 192)',
            borderWidth: 4,
            tension: 0.2,
            fill: true,
            pointStyle: 'rectRounded',
            backgroundColor: 'rgb(100, 192, 192, 0.2)',
  
        }]
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



