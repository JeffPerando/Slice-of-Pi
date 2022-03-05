
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
    console.log("These are all the offenses that have not happened in this year: ", noOffenses);

    document.getElementById("year").textContent=" (" + currentYearSelected + ")";
}
