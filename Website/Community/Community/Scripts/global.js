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