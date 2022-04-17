
$.ajax({
    type: "GET",
    dataType: "json",
    url: `/apiv3/FBI/StateCrimeStats`,
    data: {year: 2020, stateAbbrev: $("#stateAbbrev").val() },
    success: showStateStats,
    error: errorOnAjax
});


function fetchStateCrimeStats(year) {

    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/apiv3/FBI/StateCrimeStats`,
        data: {year: year, stateAbbrev: $("#stateAbbrev").val() },
        success: showStateStats,
        error: errorOnAjax

    });
}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function showStateStats(data) {

    internationalNumberFormat = new Intl.NumberFormat('en-US')
    var total_offenses = (data["violentCrimes"] + data["propertyCrimes"])
    var crime_per_capita = ((total_offenses / data["population"]) * 100000);

    $("#stateCrimeTable>tbody").empty();
    let repoTR = $(
        `<tr>
            <td>${internationalNumberFormat.format(data["population"])}</td>
            <td>${internationalNumberFormat.format(total_offenses)}</td>
            <td>${internationalNumberFormat.format(crime_per_capita.toFixed(2))}</td>
        <tr>`
    )
    $("#stateCrimeTable>tbody").append(repoTR);
    $("#stateCrimeTable").show();


}
