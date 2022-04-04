
$(function () {
    $.ajax({
        type: "GET",
        dataType: "json",
        url: "/apiv3/SearchHistory/StateCrime",
        //data: {},
        success: showStateSearchHistory,
        error: errorOnAjax

    });

})

function errorOnAjax() {
    console.log("ERROR in ajax request");
}

function showStateSearchHistory(data) {

}