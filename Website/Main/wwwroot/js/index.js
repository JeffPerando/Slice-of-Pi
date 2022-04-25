
$(function () {
    console.log("Loading index JS");

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/GetSafestStates",
        success: displayStateInformation,
        error: errorOnAjax

    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/States",
        success: populateDropDown,
        error: errorOnAjax

    });

})

function errorOnAjax(xhr, status, error) {
    console.log(xhr);
    console.log(`ERROR in ajax request: Status code ${xhr.status}, error: ${error}`);
}

function displayStateInformation(data) {

    $("#safestStatesTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td style="color:white; font-weight:bold;">${data[i]["state"]}</td>
                <td style="color:white; font-weight:bold;">${data[i]["population"].toLocaleString("en-US")}</td>
                <td style="color:white; font-weight:bold;">${data[i]["crimePerCapita"]}</td>
            </tr>`
        )
        $("#safestStatesTable>tbody").append(repoTR);
        $("#safestStatesTable").show();
    }
    document.getElementById("loadingIcon").textContent = "";
}

function populateDropDown(data) {
    var select = document.getElementById("stateAbbrev");
    for (var i = 0; i < data.length; i++) {
        var option = data[i];
        var element = document.createElement("option");
        element.textContent = option;
        element.value = option;
        select.appendChild(element);
    }
}
