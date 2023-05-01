
document.getElementById('imgdiv').addEventListener("click", e => {
    document.getElementById('imginput').click();
});

document.getElementById('imginput').addEventListener("change", e => {
    const reader = new FileReader(); // Create a new FileReader object
    reader.onload = function () {
        document.getElementById('user-profile-img').src = reader.result; // Set the source of the image tag to the selected image
    }
    reader.readAsDataURL(e.target.files[0]); // Read the selected file as a data URL
});

function ved1() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    var b = a.options[a.selectedIndex];
    for (var i = 0; i < a.length; i++) {

        if (a.options[i].selected == true) {

            a.options[i].selected = false
            c.add(a.options[i])

            ved1()
        }

    }
}
function ved2() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    var b = c.options[c.selectedIndex];
    for (var i = 0; i < c.length; i++) {
        if (c.options[i].selected == true) {
            c.options[i].selected = false
            a.add(c.options[i])
            ved2()
        }
    }
}
function ved3() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < a.length;) {
        c.add(a.options[c, i])
    }
}
function ved4() {
    var a = document.getElementById("s1");
    var c = document.getElementById("s2");
    for (var i = 0; i < c.length;) {
        a.add(c.options[a, i])
    }
}
document.getElementById('skillSave').addEventListener("click", e => {
    var selectedSkills = [];
    const skillsSelected = $('#s2 option');

    for (var i = 0; i < skillsSelected.length; i++) {
        selectedSkills.push(skillsSelected[i].value);
    }

    //skillsSelected.forEach(fillskill);

    //function fillskill(skill) {
    //	selectedSkills.push(skill.value);
    //}
    console.log(selectedSkills);
    $.ajax({
        url: '/Employee/Home/SaveUserSkills',
        type: 'POST',
        data: { selectedSkills: selectedSkills },

        success: function (response) {

            $('#userskilldiv').html($(response).find('#userskilldiv').html());
            document.getElementById('close').click();

        },
        error: function () {
            alert("could not comment");
        }
    });
})

function firstname(){
    debugger;

    var firstname = documnent.getElementById('firstname').value;
    var firstnamespan = document.getElementById('firstname1');
    if (firstname.length == 0) {
        document.getElementById('firstname1').style.display = "block";
    }
    else {
        fdocument.getElementById('firstname1').style.display = "none";
    }


}
function lastname(){
    var lastname = documnent.getElementById('lastname').value;
    var lastnamespan = document.getElementById('lastname2');
    if (lastname.length == 0) {
        document.getElementById('lastname2').style.display = "block";
    }

    else {
        document.getElementById('lastname2').style.display = "none";
    }
}

function Messageemp() {
    
    var message = document.getElementById('massage1').value;
    var messagespann = document.getElementById('massage');
    if (message.length == 0) {
        messagespann.style.display = "block";
    }
    else {
        messagespann.style.display = "none";
    }
}
function Subjectemp() {
  
    var message = document.getElementById('subject1').value;
    var messagespann = document.getElementById('subject');
    if (message.length == 0) {
        messagespann.style.display = "block";
    }
    else {
        messagespann.style.display = "none";
    }

}
function changepass() {
    var pass1 = document.getElementById('inputPassword1').value;
    var errpass1 = document.getElementById('oldpass');


    var pass2 = document.getElementById('inputPassword2').value;
    var pass21 = document.getElementById('newpass2');
    var errpass2 = document.getElementById('newpass');

    var pass3 = document.getElementById('inputPassword3').value;
    var errpass3 = document.getElementById('confirmpass');


    if (pass1.length == 0 || pass2.length == 0 || pass3.length == 0) {
        if (pass1.length == 0) {
            errpass1.style.display = "block";


        } if (pass2.length == 0) {
            errpass2.style.display = "block";

        } if (pass3.length == 0) {
            errpass3.style.display = "block";

        }

    }
    else {
        if (pass2.length < 8 || !/[A-Z]/.test(pass2) || !/[a-z]/.test(pass2) || !/[\W_]/.test(pass2)) {
            pass21.style.display = "block";

        } else {
            pass21.style.display = "none";



            if (pass2 != pass3) {
                Swal.fire({
                    position: 'top-center',
                    icon: 'warning',
                    title: 'Password and Confirmpassword must be same',
                    showConfirmButton: false,
                    timer: 2500
                })
            }
            else {
                $.ajax({
                    url: '/Employee/Home/changepass',
                    type: 'POST',
                    data: { pass1: pass1, pass2: pass2, pass3: pass3 },

                    success: function (response) {
                        if (response.success == false) {
                            Swal.fire({
                                position: 'top-center',
                                icon: 'warning',
                                title: 'Old password doesnot match',
                                showConfirmButton: false,
                                timer: 2500
                            })
                        }
                        else {
                            Swal.fire({
                                position: 'top-center',
                                icon: 'success',
                                title: 'Password changed successfully',
                                showConfirmButton: false,
                                timer: 2500
                            })
                            $('#close1').click();
                        }



                    },
                    error: function () {
                        alert("could not comment");
                    }
                });
            }
        }
    }

}

function errorfunction() {
    var pass1 = document.getElementById('inputPassword1').value;
    var errpass1 = document.getElementById('oldpass');


  



    if (pass1.length == 0) {
        errpass1.style.display = "block";


    }
    else
    {



        document.getElementById('oldpass').style.display = "none";
    }
   
   

}
function errorfunction2() {
    var pass2 = document.getElementById('inputPassword2').value;
    var pass21 = document.getElementById('newpass2');
    var errpass2 = document.getElementById('newpass');

    if (pass2.length == 0) {
        errpass2.style.display = "block";

    }
    else {

        document.getElementById('newpass').style.display = "none";
        if (pass2.length < 8 || !/[A-Z]/.test(pass2) || !/[a-z]/.test(pass2) || !/[\W_]/.test(pass2)) {
            pass21.style.display = "block";

        } else {
            pass21.style.display = "none";
        }






    }

}
function errorfunction3() {
    var pass3 = document.getElementById('inputPassword3').value;
    var errpass3 = document.getElementById('confirmpass');

    if (pass3.length == 0) {
        errpass3.style.display = "block";

    }


    else {


        document.getElementById('confirmpass').style.display = "none";

    }

}
