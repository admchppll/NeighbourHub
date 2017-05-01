function volunteer(eventID) {
    var data = {
        EventId: eventID
    };
    AjaxVolunteer('/Volunteer/Volunteer', data);
}

function withdraw(eventID) {
    var data = {
        EventId: eventID
    };
    AjaxVolunteer('/Volunteer/Withdraw', data);
}

function volunteerAdminOperation(volunteerID, eventID, operation) {
    var url = "/Volunteer/";
    var data = {
        ID: volunteerID,
        EventId: eventID
    };

    var success = function (data) {
        ResultMessage(data.title, data.message, data.success, "volunteerAdmin");
    }

    switch (operation) {
        case "accept":
            url += "Accept";
            break;
        case "confirm":
            url += "Confirm";
            break;
        case "reject":
            url += "Reject";
            break;
        default: break;
    }

    Ajax(url, data, success, success);
}