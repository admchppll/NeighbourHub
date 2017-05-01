function resolve(reportID) {
    var data = {
        ID: reportID
    };

    AjaxSimple("/Report/Resolve", data, "report")
}

function suspendEvent(eventID) {
    var data = {
        ID: eventID
    };

    AjaxSimple("/Event/Suspend", data, "report")
}

function suspendUser(userID) {
    var data = {
        ID: userID
    };

    AjaxSimple("/Profile/Suspend", data, "report")
}

$('#resolveBtn').on('click', function () {
    resolve($('#reportID').val());
});

$('#suspendEventBtn').on('click', function () {
    suspendEvent($('#eventID').val());
});

$('#suspendUserBtn').on('click', function () {
    suspendUser($('#userID').val());
});