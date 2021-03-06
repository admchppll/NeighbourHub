﻿//add new tag to event
$('#tagAddBtn').on("click", function () {
    var tagName = $('#tagSelect option:selected').text();

    var success = function (data) {
        if (data.success) {
            var output = '<span class="tag">' + tagName + ' <a href="javascript:RemoveTag('+ data.ID + ')" data-eventtag-id="' + data.ID + '"><span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span></a></span>';
            $('#tagList').append(output);
        }
        else {
            ResultMessage("", "We couldn't add the tag to event!", false, 'eventTag')
        }
    };
    var fail = function (data) {
        ResultMessage("", "We couldn't add the tag to your event!", false, 'eventTag')
    };

    var tagID = $('#tagSelect').val();
    var eventID = $('#eventId').val();
    var parameters = {
        EventID: eventID,
        TagID: tagID
    };

    Ajax("/EventTag/Create", parameters, success, fail);
});

//Remove tag from event
function RemoveTag(tagID) {
    console.log(tagID);
    var success = function (data) {
        $('#tag-' + tagID).remove();
        ResultMessage("Success! ", "Tag Removed!", true, 'eventTag');
    };
    var fail = function (data) {
        ResultMessage("", "We couldn't add the tag to your event!", false, 'eventTag');
    };

    var parameters = {
        id: tagID
    };

    Ajax("/EventTag/Delete", parameters, success, fail);
};