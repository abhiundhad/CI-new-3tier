function SaveSkills() {
    var selectedSkills = [];
    var selected = "";
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
        console.log(skillsSelected[i].value)
    }

    selected = selectedSkills.toString();
    console.log(selected)

    document.getElementById('selectedSkills').value = selected;

};

function reload() {
    location.reload();
}
function editmission(missionId) {
    $.ajax({
        url: '/Admin/Admin/GetMission',
        type: 'POST',
        data: { missionId: missionId },

        success: function (response) {

            //document.getElementById("themeName").value = response.title;
            $('#AdminMissionmodal').html($(response).find('#AdminMissionmodal').html());

        },
        error: function () {
            alert("could not comment");
        }
    });
}

function goalchange() {
    if (document.getElementById("missionType").value == "goal") {
        document.getElementById("goal").style.display = "";
        document.getElementById("time").style.display = "none";
    } else {
        document.getElementById("goal").style.display = "none";
        document.getElementById("time").style.display = "block";

    }

}
/*Dropzone.autoDiscover = false;*/

$(document).ready(function () {
    //var myDropzone = new Dropzone("#my-awesome-dropzone", {
    //    url: "AddMission",
    //    paramName: "file", // The name that will be used to transfer the file
    //    maxFilesize: 10, // MB
    //    acceptedFiles: '.jpeg,.png,.jpg,.pdf,.docx,.xlxs',
    //    addRemoveLinks: true,
    //    parallelUploads: 5,
    //    autoProcessQueue: false,
    //    uploadMultiple: true,
    //    dictRemoveFile: '<i class="bi bi-trash"></i>',
    //    previewsContainer: '.dropzone-previews',
    //    clickable: '.dz-message',

    //});
    //$('#missionSave').on('click', function (e) {
    //    e.preventDefault();
    //    myDropzone.processQueue();
    //});

    myDropzone.on('sendingmultiple', function () {
        // Add any additional data you want to send with the request
        var data = $('#my-awesome-dropzone').serializeArray();
        $.each(data, function (key, el) {
            myDropzone.options.params[el.name] = el.value;
        });
    });

    myDropzone.on('successmultiple', function (files, response) {
        // Do something when the upload is successful
    });
});

function removedoc(docId) {
    $.ajax({
        url: '/Admin/Admin/delDoc',
        type: 'POST',
        data: { docId: docId },

        success: function (response) {

            //document.getElementById("themeName").value = response.title;
            /*            $('#AdminMissionmodal').html($(response).find('#AdminMissionmodal').html());*/
            document.getElementById(docId).style.display = "none";

        },
        error: function () {
            alert("could not comment");
        }
    });
}

function removeimg(imgId) {
    $.ajax({
        url: '/Admin/Admin/delImg',
        type: 'POST',
        data: { imgId: imgId },

        success: function (response) {

            //document.getElementById("themeName").value = response.title;
            /*            $('#AdminMissionmodal').html($(response).find('#AdminMissionmodal').html());*/
            document.getElementById(imgId).style.display = "none";

        },
        error: function () {
            alert("could not comment");
        }
    });
}
function showSave() {
    document.getElementById("missionSave").style.display = "";
    document.getElementById("missionSave1").style.display = "none";
}