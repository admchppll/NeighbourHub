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

function createCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + ";"; // domain=neighbourhub.online;";
}