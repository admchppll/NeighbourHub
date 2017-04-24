CKEDITOR.replaceAll();

$(document).ready(function () {
    $('#Published').prop('checked', true);
});

var tags = [];

$('#tagAddBtn').on("click", function () {
    var tagName = $('#tagSelect option:selected').text();
    var tagID = $('#tagSelect').val();

    if ($.inArray(tagID, tags) < 0) {
        tags.push(tagID);
        var output = '<span class="tag" id="#tag-' + tagID + '">' + tagName + ' <a href="javascript:removeEventTag(' + tagID + ')" data-eventtag-id="' + tagID + '"><span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span></a></span>';
        $('#tagList').append(output);
        $('#Tags').val(tags.join());
    }
});

function remove(tagID) {
    var tags = $('#Tags').val().split(',');
    tags.splice($.inArray(tagID, tags), 1);
    $('#tag-' + tagID).remove();
    $('#Tags').val(tags.join());
}