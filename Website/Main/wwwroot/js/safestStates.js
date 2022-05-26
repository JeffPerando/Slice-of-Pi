
$(document).ready(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/api/GetSafestStates",
        success: displayStateInformation,
        error: errorOnAjax

    });
});

function errorOnAjax(xhr, status, error) {
    console.log(xhr);
    console.log(`ERROR in ajax request: Status code ${xhr.status}, error: ${error}`);
}

function toTD(data) {
    let td = document.createElement("td");
    td.style = "color:white; font-weight:bold;";
    td.textContent = data;
    return td;
}

function displayStateInformation(data) {
    $("#safestStatesTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let state = data[i];
        let repoTR = document.createElement("tr");

        repoTR.append(toTD(state["state"]["name"]));
        repoTR.append(toTD(state["population"].toLocaleString("en-US")));
        repoTR.append(toTD(state["crimePerCapita"]));

        $("#safestStatesTable>tbody").append(repoTR);

    }

    $("#safestStatesTable").show();

    document.getElementById("loadingIcon").textContent = "";
}
