CKEDITOR.replaceAll();

$('#skillAddBtn').on("click", function () {
    var skillName = $('#skillSelect option:selected').text();
    var skillID = $('#skillSelect').val();

    var tags = $('#Skills').val().split(',');

    if ($.inArray(skillID, tags) < 0) {
        tags.push(skillID);
        var output = '<span class="skill" id="sk-' + skillID + '">' + skillName + ' <a href="javascript:removeSkill(' + skillID + ')" data-skill-id="' + skillID + '"><span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span></a></span>';
        $('#skillList').append(output);
        $('#Skills').val(tags.join());
    }
});

$('#interestAddBtn').on("click", function () {
    var interestName = $('#interestSelect option:selected').text();
    var interestID = $('#interestSelect').val();

    var tags = $('#Interests').val().split(',');

    if ($.inArray(interestID, tags) < 0) {
        tags.push(interestID);
        var output = '<span class="interest" id="int-' + interestID + '">' + interestName + ' <a href="javascript:removeInterest(' + interestID + ')" data-interest-id="' + interestID + '"><span class="glyphicon glyphicon-remove-circle" aria-hidden="true"></span></a></span>';
        $('#interestList').append(output);
        $('#Interests').val(tags.join());
    }
});

function removeSkill(tagID) {
    var skills = $('#Skills').val().split(',');
    skills.splice($.inArray(tagID, skills), 1);
    $('#sk-' + tagID).remove();
    $('#Skills').val(skills.join());
}

function deleteSkill(tagID) {
    var skills = $('#Skills').val().split(',');
    skills.splice($.inArray(tagID, skills), 1);
    $('#sk-' + tagID).remove();
    $('#Skills').val(skills.join());

    var deletedSkills = $('#DeletedSkills').val().split(',');
    deletedSkills.push(tagID);
    $('#DeletedSkills').val(deletedSkills.join());
}

function removeInterest(tagID) {
    var interests = $('#Interests').val().split(',');
    interests.splice($.inArray(tagID, interests), 1);
    $('#int-' + tagID).remove();
    $('#Interests').val(skills.join());
}

function deleteInterest(tagID) {
    var interests = $('#Interests').val().split(',');
    interests.splice($.inArray(tagID, interests), 1);
    $('#int-' + tagID).remove();
    $('#Interests').val(interests.join());

    var deletedInterests = $('#DeletedInterests').val().split(',');
    deletedInterests.push(tagID);
    $('#DeletedInterests').val(deletedInterests.join());
}