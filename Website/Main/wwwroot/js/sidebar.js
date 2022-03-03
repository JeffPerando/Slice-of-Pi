
var isSidebarOpen = false;

$("#sidebarToggle").click(
    function () {
        console.log("Sidebar toggled")
        let width = "200px";
        if (isSidebarOpen) {
            width = "0px";
        }
        $("#sidebar").css("width", width);
        isSidebarOpen = !isSidebarOpen
    });

console.log("Sidebar registered")
