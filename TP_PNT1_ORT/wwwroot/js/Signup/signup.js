var signupElements = document.querySelectorAll("#signup input[name][id]");
//console.log(signupElements);

function getFormValue() {

    var form = {};

    signupElements.forEach(e => {

        form[e.name] = e.value;

    });

    

    return form;

}

function signup(event) {

    event.preventDefault();

    var form = getFormValue();

    console.log(form);

    var req = new XMLHttpRequest();
    req.open("POST", window.location + "/Signup", true );
    req.setRequestHeader("Content-Type", "application/json;charset=UTF-8");
    req.send(
        JSON.stringify(form)
    );



}

