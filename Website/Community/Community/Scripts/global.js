function getRequestVerificationToken(data) {
    return $('input[name=__RequestVerificationToken]').val();
}

//Parameters: JSON object
//successFunction/failureFunction: must be a javascript function thats passed
function ajax(url, parameters, successFunction, failFunction) {
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parameters),
        dataType: "json",
        method: "POST",
        headers: {
            "__RequestVerificationToken": getRequestVerificationToken()
        },
        success: successFunction,
        error: failFunction
    });
}

function ajaxVolunteer(url, parameters) {
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parameters),
        dataType: "json",
        method: "POST",
        headers: {
            "__RequestVerificationToken": getRequestVerificationToken()
        },
        success: function (data) {
            resultMessage(data.title, data.message, data.success, "volunteer");
        },
        error: function (data) {
            resultMessage(data.title, data.message, data.success, "volunteer");
        }
    });
}

function ajaxSimple(url, parameters, resultArea) {
    $.ajax({
        url: url,
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(parameters),
        dataType: "json",
        method: "POST",
        headers: {
            "__RequestVerificationToken": getRequestVerificationToken()
        },
        success: function (data) {
            resultMessage(data.title, data.message, data.success, resultArea);
        },
        error: function (data) {
            resultMessage(data.title, data.message, data.success, resultArea);
        }
    });
}

function createCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + ";"; // domain=theneighbourhub.online;";
}

function resultMessage(title, message, success, messageLocation) {

    $("#" + messageLocation + "Message").removeClass("hidden");
    if (success) {
        $("#" + messageLocation + "Message").removeClass("alert-danger");
        $("#" + messageLocation + "Message").addClass("alert-success");
    }
    else {
        $("#" + messageLocation + "Message").removeClass("alert-success");
        $("#" + messageLocation + "Message").addClass("alert-danger");
    }
    $("#" + messageLocation + "MsgTitle").html(title);
    $("#" + messageLocation + "MsgContent").html(message);
}

Array.prototype.remove = function () {
    var what, a = arguments, L = a.length, ax;
    while (L && this.length) {
        what = a[--L];
        while ((ax = this.indexOf(what)) !== -1) {
            this.splice(ax, 1);
        }
    }
    return this;
};

$(document).ready(function () {
    $("#postcode").autocomplete({
        source: function (request, response) {
            $.ajax({
                url: "http://api.postcodes.io/postcodes/" + request.term + "/autocomplete",
                type: "GET",
                dataType: "json",
                success: function (data) {
                    response($.map(data.result, function (item) {
                        return { label: item, value: item };
                    }));
                }
            });
        },
        messages: {
            noResults: "", results: ""
        }
    });
});