
$.ajax({
    type: "GET",
    dataType: "json",
    url: `/api/StateCrimeStats`,
    data: {year: 2020, stateAbbrev: $("#stateAbbrev").val() },
    success: showStateStats,
    error: errorOnAjax
});


function fetchStateCrimeStats(year) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/api/StateCrimeStats`,
        data: {year: year, stateAbbrev: $("#stateAbbrev").val() },
        success: showStateStats,
        error: errorOnAjax

    });
}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function toTD(content) {
    let td = document.createElement("td");
    td.textContent = content;
    return td;
}

function showStateStats(data) {
    let numFmt = new Intl.NumberFormat('en-US')
    let total_offenses = (data["violentCrimes"] + data["propertyCrimes"])
    let crime_per_capita = ((total_offenses / data["population"]) * 100000);

    $("#stateCrimeTable>tbody").empty();
    let repoTR = document.createElement("tr");

    repoTR.appendChild(toTD(numFmt.format(data["population"])));
    repoTR.appendChild(toTD(numFmt.format(total_offenses)));
    repoTR.appendChild(toTD(numFmt.format(crime_per_capita.toFixed(2))));

    $("#stateCrimeTable>tbody").append(repoTR);
    $("#stateCrimeTable").show();

}
