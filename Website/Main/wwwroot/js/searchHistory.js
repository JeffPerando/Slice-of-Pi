
$.ajax({
    type: "GET",
    dataType: "json",
    url: "/api/SearchHistory/StateCrime",
    //data: {},
    success: showStateSearchHistory,
    error: errorOnAjax

});

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function showStateSearchHistory(data) {
    console.log(data);
    
    if (data.results.length == 0) {
        $("#scsr").html("<p><h5>We didn't find any search results</h5></p>");
        return;
    }

    let scsrTbl = '<table class="table table-dark table-condensed" align="center" id="safestStatesTable" border="1">';

    scsrTbl += `<thead><th>Date Searched</th>
                <th>State</th>
                <th>Year</th>
                <th>Population</th>
                <th>Violent Crimes</th>
                <th>Homicide</th>
                <th>Rape (Legacy)</th>
                <th>Rape (Revised)</th>
                <th>Robbery</th>
                <th>Aggravated Assault</th>
                <th>Property Crime</th>
                <th>Burglary</th>
                <th>Larceny</th>
                <th>Motor Vehicle Theft</th>
                <th>Arson</th></thead><tbody>`;

    for (const searchEntry of data.results) {
        scsrTbl += `<tr>
                        <td>${new Date(searchEntry["dateSearched"]).toLocaleString()}</td>
                        <td>${searchEntry["state"]}</td>
                        <td>${searchEntry["year"]}</td>
                        <td>${searchEntry["population"]}</td>
                        <td>${searchEntry["violentCrimes"]}</td>
                        <td>${searchEntry["homicide"]}</td>
                        <td>${searchEntry["rapeLegacy"]}</td>
                        <td>${searchEntry["rapeRevised"]}</td>
                        <td>${searchEntry["robbery"]}</td>
                        <td>${searchEntry["assault"]}</td>
                        <td>${searchEntry["propertyCrimes"]}</td>
                        <td>${searchEntry["burglary"]}</td>
                        <td>${searchEntry["larceny"]}</td>
                        <td>${searchEntry["motorVehicleTheft"]}</td>
                        <td>${searchEntry["arson"]}</td>
                    </tr>`;
    }

    scsrTbl += "</tbody></table>";

    $("#scsr").html(scsrTbl);

}