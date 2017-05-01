$('.notification').on("mouseover", function () {
    var id = $(this).attr("data-notification-id");
    if ($(this).attr("data-read") != "True") {
        var data = {
            ID: id
        };
        var success = function () {
            $('#not-' + id).removeClass("active");
        };
        var fail = function () { };

        Ajax("/Notification/Read", data, success, fail);
    }
});