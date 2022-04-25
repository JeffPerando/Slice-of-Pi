﻿
$(document).ready(function ()
{
    $("#yearSelect").change(fetchCrimeStats);
    $("#perCapita").change(toggleCrimePerCapita);
    $("#natCrimeDiv").hide();
    
    $("#natCrimeTable").tablesorter({
        sortList: [[0, 0]], textExtraction: function (node) {
            return node.innerHTML.replaceAll(',', '');
        }
    });
    
});

function disableForms(off) {
    $(":input").prop('disabled', off ? 'disabled' : '');
}

function fetchCrimeStats() {
    let year = $("#year")[0].value;

    if (year == 0) {
        return;
    }

    disableForms(true);
    $("#spinnyBoi").show();
    $("#natCrimeDiv").hide();
    $("#natCrimeTable>tbody").html("");

    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/api/NationalCrime`,
        data: {
            year: year
        },
        success: function (data) {
            showCrimeStats(data, year);
        },
        error: errorOnAjax

    });

}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function toTD(data) {
    return `<td style="color:white; font-weight:bold;">${data}</td>`;
}

function showCrimeStats(data, year) {
    //awful, awful code to reset the per-capita check.
    //this is in case someone leaves it checked, to prevent weird crime stat displays
    $("#perCapita").change(null)[0].checked = false;
    $("#perCapita").change(toggleCrimePerCapita);

    $("#spinnyBoi").hide();//.removeAttr("hidden");
    $("#natCrimeDiv").show();
    
    $("#natCrimeHeader").html(`National Crime Statistics For ${year}`);

    let tbl = $("#natCrimeTable>tbody");
    tbl.html("");

    for (let state of data.stateCrimes) {
        tbl.append(`<tr>
            ${toTD(state["state"])}
            ${toTD(state["population"])}
            ${toTD(state["violentCrimes"])}
            ${toTD(state["homicide"])}
            ${toTD(state["rapeLegacy"])}
            ${toTD(state["rapeRevised"])}
            ${toTD(state["robbery"])}
            ${toTD(state["assault"])}
            ${toTD(state["propertyCrimes"])}
            ${toTD(state["burglary"])}
            ${toTD(state["larceny"])}
            ${toTD(state["motorVehicleTheft"])}
            ${toTD(state["arson"])}
        </tr>`);
    }
    /*
    $("#natCrimeTable").tablesorter({
        sortList: [[0, 0]], textExtraction: function (node) {
            return node.innerHTML.replaceAll(',', '');
        }
    });
    */
    prettifyTable();
    
    disableForms(false);
    window.scrollTo(0, 1020);

    $("#natCrimeTable").trigger("update");

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

var debounce = true;

function toggleCrimePerCapita(e) {
    e.stopImmediatePropagation();
    let tbl = $("#natCrimeTable")[0];
    let perCapIn = $("#perCapita")[0].checked;
    
    //console.log(`Per capita: ${perCapIn}`)
    
    for (let x = 1; x < tbl.rows.length; x++) {
        for (let y = 2; y < 13; y++) {
            let pop = tblGet(tbl, x, 1);
            let cell = tblGet(tbl, x, y);
            let result = "";

            if (perCapIn) {
                //This ensures the resulting fraction is to 2 decimal places
                result = Math.round((cell / pop) * 10000000) / 100;
            } else {
                result = Math.round((cell * pop) / 100000);
            }

            tblSet(tbl, x, y, result);

        }

    }

    if (!perCapIn) {
        prettifyTable();

    }

    //$("#natCrimeTable").tablesorter({ sortList: [[0, 0]] });

    $("#natCrimeTable").trigger("update");
    //$("#natCrimeTable").trigger("sorton", [[0, 0]]);

}
