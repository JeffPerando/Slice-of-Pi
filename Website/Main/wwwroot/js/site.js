// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "apiv3/FBI/StateStats",
        success: displayStateInformation,
        error: errorOnAjax

    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/FBI/StateList",
        success: populateDropDown,
        error: errorOnAjax

    });

    $.ajax({
        type: "GET",
        dataType: "json",
        url: "apiv3/FBI/StateList",
        success: populateDropDown,
        error: errorOnAjax

    });

})

function errorOnAjax() {
    console.log("ERROR in ajax request");
}


function displayStateInformation(data)
{
    console.log(data);

    $("#safestStatesTable>tbody").empty();
    for (let i = 0; i < data.length; ++i) {
        let repoTR = $(
            `<tr>
                <td>${data[i]["state"]}</td>
                <td>${data[i]["crime_Per_Capita"]}</td>
            </tr>`
        )
        $("#safestStatesTable>tbody").append(repoTR);
        $("#safestStatesTable").show();
    }
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


//<form action="SingleStateStats" method="get">
//    <label for="aYear">Slide to the year you would like to look at</label>

//    <input id="slide" type="range" min="2" max="15" step="1" value="100">

//        <input type="submit" value="Submit">
//                </form>


//        <form action="SingleStateStats" method="get">
//            <div><label for="aYear">Slide to the year you would like to look at </label></div>
//            <br>
//                <br>
//                    <div class="main">
//                        <input type="range" min="1" max="15" step="1" value="1" id="slider">




//                            <input type="submit" value="Submit">
//                </form>





//<form action="SingleStateStats" method="get">
//    <div><label for="aYear">Slide to the year you would like to look at<span>@ViewBag.aYear</span></label></div>

//    <input id="slider" type="range" min="1" max="15" step="1" value="1">

//        <input type="submit" value="Submit">
//                </form>