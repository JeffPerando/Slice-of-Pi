

//change to fetchListings
function fetchCrimeStats(btn) {
    $("#spinnyBoi").show();
    $("#stateCrimeDiv").removeAttr("hidden");
    let form = $(btn).parents('form');

    let formData = form.serialize();

    $.ajax({
        type: "GET",
        dataType: "json",
        url: `/api/FBI/Listings`,
        data: form.serialize(),
        success: showStateStats, //change to populateTable
        error: errorOnAjax

    });
}

function errorOnAjax() {
    console.log("ERROR in ajax request");
}
