
$(document).ready(function ()
{
    $("#yearSelect").change(fetchCrimeStats);
    $("#perCapita").change(toggleCrimePerCapita);
    $("#natCrimeDiv").hide();
});

function disableYearDropdown(off) {
    $("#yearSelect :input").prop('disabled', off ? 'disabled' : '');
}

function fetchCrimeStats() {
    disableYearDropdown(true);
    $("#spinnyBoi").show();
    let form = $("#yearSelect");
    let formData = form.serialize();
    
    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/api/NationalCrime`,
        data: formData,
        success: function (data) {
            showCrimeStats(data, $("#year option:selected").text());
        },
        error: errorOnAjax

    });

}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function showCrimeStats(data, year) {
    $("#spinnyBoi").hide();//.removeAttr("hidden");

    $("#natCrimeHeader").html(`National Crime Statistics For ${year}`);

    $("#natCrimeTable>tbody").html("");
    for (let state of data.stateCrimes) {
        $("#natCrimeTable>tbody").append(`<tr>
            <td style="color:white; font-weight:bold;">${state["state"]}</td>
            <td style="color:white; font-weight:bold;">${state["population"]}</td>
            <td style="color:white; font-weight:bold;">${state["violentCrimes"]}</td>
            <td style="color:white; font-weight:bold;">${state["homicide"]}</td>
            <td style="color:white; font-weight:bold;">${state["rapeLegacy"]}</td>
            <td style="color:white; font-weight:bold;">${state["rapeRevised"]}</td>
            <td style="color:white; font-weight:bold;">${state["robbery"]}</td>
            <td style="color:white; font-weight:bold;">${state["assault"]}</td>
            <td style="color:white; font-weight:bold;">${state["propertyCrimes"]}</td>
            <td style="color:white; font-weight:bold;">${state["burglary"]}</td>
            <td style="color:white; font-weight:bold;">${state["larceny"]}</td>
            <td style="color:white; font-weight:bold;">${state["motorVehicleTheft"]}</td>
            <td style="color:white; font-weight:bold;">${state["arson"]}</td>
        </tr>`);
    }

    prettifyTable();

    $("#natCrimeTable").tablesorter({
        sortList: [[0, 0]], textExtraction: function (node) {
            return node.innerHTML.replaceAll(',', '');
        }
    });
    $("#natCrimeDiv").show();
    disableYearDropdown(false);

    window.scrollTo(0, 1020);

}

function prettifyTable() {
    let tbl = $("#natCrimeTable")[0];

    for (let x = 1; x < tbl.rows.length; x++) {
        for (let y = 1; y < 13; y++) {
            tblSet(tbl, x, y, parseInt(tblGet(tbl, x, y)).toLocaleString("en-US"));
            
        }

    }

}

function tblGet(table, x, y) {
    return table.rows[x].cells[y].innerText.replaceAll(',', '');
}

function tblSet(table, x, y, text) {
    table.rows[x].cells[y].innerText = text;
}

function toggleCrimePerCapita() {
    let tbl = $("#natCrimeTable")[0];
    let perCapita = $("#perCapita")[0].checked;

    for (let x = 1; x < tbl.rows.length; x++) {
        for (let y = 2; y < 13; y++) {
            let pop = tblGet(tbl, x, 1);
            let cell = tblGet(tbl, x, y);
            let result = "";

            if (perCapita) {
                //This ensures the resulting fraction is to 2 decimal places
                result = Math.round((cell / pop) * 10000000) / 100;
            } else {
                result = Math.round((cell * pop) / 100000);
            }

            tblSet(tbl, x, y, result);

        }

    }

    if (!perCapita) {
        prettifyTable();

    }

    //$("#natCrimeTable").tablesorter({ sortList: [[0, 0]] });

    $("#natCrimeTable").trigger("update");
    //$("#natCrimeTable").trigger("sorton", [[0, 0]]);

}
