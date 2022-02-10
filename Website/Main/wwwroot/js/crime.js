
$(function() {
    var ourObject = {cityName:$("#cityName").val(),stateAbbrev:$("#stateAbbrev").val()};
    console.log(ourObject);

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