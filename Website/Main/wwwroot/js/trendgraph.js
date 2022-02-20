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
    console.log(data);

    for(let i = 0; i < data.length; i++)
    {
        years.push(data[i]["year"]);
        crimes.push(data[i]["totalOffenses"]);
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
            borderColor: 'red',
            borderWidth: 4
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



