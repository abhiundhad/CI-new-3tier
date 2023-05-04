
function deletetimesheet(timesheetid) {

    $.ajax({
        url: '/Employee/Volunteering/deletetimesheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {

            $('.timesheetdiv').html($(response).find('.timesheetdiv').html());


        },
        error: function () {
            alert("could not comment");
        }
    });

}
function deletegoalsheet(timesheetid) {

    $.ajax({
        url: '/Employee/Volunteering/deletetimesheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {

            $('.goalsheetdiv').html($(response).find('.goalsheetdiv').html());


        },
        error: function () {
            alert("could not comment");
        }
    });

}
function editgoalsheet(timesheetid) {

    $.ajax({
        url: '/Employee/Volunteering/editsheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {
            console.log(response);
            console.log(response.timesheet.action);
            var ele = document.getElementById('action1');
            ele.value = response.timesheet.action;
            ele.max = response.finalremaininggoalvalue;
            var mission = document.getElementById('selectgoal');
            mission.value = response.timesheet.missionId;
            var date = document.getElementById('date2');
            date.value = response.timesheet.dateVolunteered;
            var notes = document.getElementById('notes1');
            notes.value = response.timesheet.notes;
            var timesheetid1 = document.getElementById('timesheetid2');
            timesheetid1.value = response.timesheet.timesheetId;
            /*ele.innerText = response.timesheet.action;*/




        },
        error: function () {
            alert("could not comment");
        }
    });

}
function edittimesheet(timesheetid) {

    $.ajax({
        url: '/Employee/Volunteering/editsheet',
        type: 'POST',
        data: { timesheetid: timesheetid },

        success: function (response) {
            console.log(response.timesheet.action);
            var hour = document.getElementById('hour1');
            hour.value = String(response.timesheet.timesheetTime).split(":")[0];
            var minute = document.getElementById('minute1');
            minute.value = String(response.timesheet.timesheetTime).split(":")[1];
            var mission = document.getElementById('mission');
            mission.value = response.timesheet.missionId;
            var date = document.getElementById('date1');
            date.value = response.timesheet.dateVolunteered;
            var notes = document.getElementById('notes');
            notes.value = response.timesheet.notes;
            var timesheetid1 = document.getElementById('timesheetid1');
            timesheetid1.value = response.timesheet.timesheetId;
            /*ele.innerText = response.timesheet.action;*/



        },
        error: function () {
            alert("could not comment");
        }
    });

}

function reload() {
    location.reload();
}
function setmaxvalue() {
    var id = document.getElementById("selectgoal").value;
   

    $.ajax({
        type: "GET",
        url: "/Employee/Volunteering/GetGoalValue",
        data: { id: id },
        success: function (res) {
            console.log(res);
            document.getElementById("GoalValue").max = res.goalValue;
    
        },
        error: function () {
            console.log("check Goal Value Error")
        }
    })

}
function setmaxvalue2() {
    var id = document.getElementById("selectgoal2").value;

 
    $.ajax({
        type: "GET",
        url: "/Employee/Volunteering/GetGoalValue",
        data: { id: id },
        success: function (res) {
            console.log(res);
       
            document.getElementById("action1").max = res.goalValue;
        },
        error: function () {
            console.log("check Goal Value Error")
        }
    })

}