// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(function() {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "apiv3/FBI/StateStats",
        success: displayStateInformation,
        error: errorOnAjax

    });
})

function errorOnAjax()
{
    console.log("ERROR in ajax request");
}

function displayStateInformation(data)
{
    console.log(data);

    $("#safestStatesTable>tbody").empty();
    for (let i = 0; i < data.length; ++i){
        let repoTR = $(
            `<tr>
                <td>${data[i]["state"]}</td>
                <td>${data[i]["actualConvictions"]}</td>
            </tr>`
        )
        $("#safestStatesTable>tbody").append(repoTR);
        $("#safestStatesTable").show();
    }
}