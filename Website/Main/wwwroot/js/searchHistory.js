
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

function toElem(type, content) {
    let th = document.createElement(type);
    th.textContent = content;
    return th;
}

function showStateSearchHistory(data) {
    //oh boy

    if (data.results.length == 0) {
        //I... I'm not rewriting this. Just... no. It's simple as can be.
        $("#scsr").html("<p><h5>We didn't find any search results</h5></p>");
        return;
    }

    let scsrTbl = document.createElement("table"); //'<table class="table table-dark table-condensed" align="center" id="safestStatesTable" border="1">';

    scsrTbl.classList = ["table", "table-dark", "table-condensed"];
//    scsrTbl.align = "center";//WHY???
    scsrTbl.id = "stateCrimeSearchResultTable";

    let headers = ["Date Searched", "State", "Year", "Population",
        "Population", "Violent Crimes", "Homicide",
        "Rape (Legacy)", "Rape (Revised)", "Robbery",
        "Aggravated Assault", "Property Crime", "Burglary",
        "Larceny", "Motor Vehicle Theft", "Arson"];

    let head = document.createElement("thead");

    for (const h in headers) {
        head.appendChild(toElem("th", h));
    }

    let body = document.createElement("tbody");

    let crimes = ["state", "year",
        "population", "violentCrimes", "homicide",
        "rapeLegacy", "rapeRevised", "robbery",
        "assault", "propertyCrimes", "burglary",
        "larceny", "motorVehicleTheft", "arson"
    ];

    for (const searchEntry of data.results) {
        let row = document.createElement("tr");

        row.appendChild(toElem("td", new Date(searchEntry["dateSearched"]).toLocaleString()));

        for (const crime of crimes) {
            row.appendChild(toElem("td", searchEntry[crime]));
        }

        body.appendChild(row);

    }

    scsrTbl.appendChild(head);
    scsrTbl.appendChild(body);

    let link = document.createElement("a");
    link.href = "/Download/StateCrimeSearchHistory";
    link.classList = ["btn", "btn-danger"];
    link.textContent = "Download";

    $("#scsr").append(scsrTbl).append(link);

}