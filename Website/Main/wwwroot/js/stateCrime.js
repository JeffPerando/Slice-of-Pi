
$(function () {
    var ourObject = { stateAbbrev: $("#stateAbbrev").val() };
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateCrimeStats",
        //data: { stateAbbrev: $("#stateAbbrev").val() },
        data: { stateAbbrev: $("#stateAbbrev").val(), year: $("#year").val() },
        success: showStateStats,
        error: errorOnAjax

    });

    //$.ajax({
    //    type: "GET",
    //    dataType: "json",
    //    url: "/apiv3/FBI/StateStats",
    //    data: { stateAbbrev: $("#stateAbbrev").val() },
    //    success: showStateStats,
    //    error: errorOnAjax

    //});


    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateList",
        success: populateDropDown,
        error: errorOnAjax

    });
})


function errorOnAjax() {
    console.log("ERROR in ajax request");
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

function callAjaxForState() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateCrimeStats",
        data: { stateAbbrev: $("#stateAbbrev").val(), year: $("#year") },
        success: showStateStats,
        error: errorOnAjax

    });

}

function showStateStats(data) {

    console.log(data);
    $("#stateCrimeTable>tbody").empty();

        let repoTR = $(
            `<tr>
                <td style="color:white; font-weight:bold;">${data["state_abbr"]}</td>
                <td style="color:white; font-weight:bold;">${data["year"]}</td>
                <td style="color:white; font-weight:bold;">${data["population"]}</td>
                <td style="color:white; font-weight:bold;">${data["violent_crime"]}</td>
                <td style="color:white; font-weight:bold;">${data["homicide"]}</td>
                <td style="color:white; font-weight:bold;">${data["rape_legacy"]}</td>
                <td style="color:white; font-weight:bold;">${data["rape_revised"]}</td>
                <td style="color:white; font-weight:bold;">${data["robbery"]}</td>
                <td style="color:white; font-weight:bold;">${data["aggravated_assault"]}</td>
                <td style="color:white; font-weight:bold;">${data["property_crime"]}</td>
                <td style="color:white; font-weight:bold;">${data["burglary"]}</td>
                <td style="color:white; font-weight:bold;">${data["larceny"]}</td>
                <td style="color:white; font-weight:bold;">${data["motor_vehicle_theft"]}</td>
                <td style="color:white; font-weight:bold;">${data["arson"]}</td>
            </tr>`
        )
        $("#stateCrimeTable>tbody").append(repoTR);
        $("#stateCrimeTable").show();
  
}
//function callAjaxForState() {
//    $.ajax({
//        type: "GET",
//        dataType: "json",
//        url: "/apiv3/FBI/StateCrimeStats?" + $("form").serialize(),
//        data: { stateAbbrev: $("#stateAbbrev").val() },
//        success: showStateStats,
//        error: errorOnAjax

//    });

//}