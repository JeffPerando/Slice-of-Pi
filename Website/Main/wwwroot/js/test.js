$.ajax({
    type: "GET",
    dataType: "json",
    url: "/apiv3/FBI/ATTOM",
    success: populateDropDown,
    error: errorOnAjax

});

function populateDropDown(data) {
    var x = data;
    console.log(x);
    $("#spinnyBoi").removeAttr("hidden");
    console.log(data);


    $("#stateCrimeTable>tbody").html(`<tr>
            <td style="color:white; font-weight:bold;">${data["name"]}</td>

        </tr>`);
    $("#spinnyBoi").hide();
    window.scrollTo(0, document.body.scrollHeight);
}
