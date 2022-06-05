var tabla = null;
var jugadoresModal = $('#jugadoresModal');

$(document).ready(function () {

    debugger;
    //datatable
    tabla = $('#usuarios').DataTable({});
    
    //$('#usuarios').on('click', function () {
    //    tabla.row.add(['TEST1', 'TEST2', 'TEST3', 'TEST4']).draw(false);
    //    counter++;
    //});

});


let list = [];
let listaYaAgregados = [];
let usuariosAAgregar = document.querySelector("#UsuariosAAgregar");
let errorMessage = document.querySelector("#ErrorMessage");
let emailUsuario = document.querySelector("input[id=EmailUsuario]");

function openModal() {

    list = [];
    usuariosAAgregar.innerHTML = "";
    errorMessage.innerHTML = "";
    emailUsuario.value = "";
    jugadoresModal.modal('show');

}

function agregarJugador(idGrupo) {

    
    var email = emailUsuario.value;
    var req = new XMLHttpRequest();
    req.open("POST", "/Grupos/agregarUsuario");
    req.setRequestHeader("Content-Type", 'application/x-www-form-urlencoded');
    var params = "email=" + email + "&" + "idGrupo=" + idGrupo;

    req.onreadystatechange = function () {

       
        if (req.readyState === 4 && req.status === 200) {
           
            //console.log(req.responseText);

            var usuario = JSON.parse(req.responseText);
            //console.log(tabla.rows().data());


            //var existe = list.find(e => { return e.email == email })
            //|| tabla.data().find(e => { return e.email == email });
            debugger;
            
            var existe = listaYaAgregados.find(e => { return e.email == email })

            if (!existe) {

                debugger;
                emailUsuario.value = "";
                list.push(usuario);
                listaYaAgregados.push(usuario);
                usuariosAAgregar.innerHTML += `<span class="badge badge-warning"> ${usuario.nombre} ${usuario.apellido} </span>&nbsp`;

            } else {
                errorMessage.innerHTML = '<div class="alert alert-danger" role="alert">' + "Ya fue ingresado el usuario seleccionado" + '</div>';
            }

            

        } else if (req.readyState === 4 && req.status !== 200) {

            console.log(req.readyState, req.status, req.responseText);
            errorMessage.innerHTML = '<div class="alert alert-danger" role="alert">' + req.responseText + '</div>';

        }
    }

    req.send(params);
}


function agregarJugadoresTabla( idGrupo ) {


    
    //var req = new XMLHttpRequest();
    //req.open("POST", "/Grupos/recargarJugadores");
    //req.setRequestHeader("Content-Type", 'application/json');

    debugger;

    list.forEach(e => {

        var idx = tabla.data().length;

        var usuarioGrupoHtml = `
            <input id="UsuariosGrupos_${idx}__email" name="UsuariosGrupos[${idx}].email" type="hidden" value="${e.email}">
            <input data-val="true" data-val-required="The idGrupo field is required." id="UsuariosGrupos_${idx}__idGrupo" name="UsuariosGrupos[${idx}].idGrupo" type="hidden" value="${idGrupo}">
            ${e.email}
        `;

        tabla.row.add([e.nombre, e.apellido, usuarioGrupoHtml]).draw(false);

    })
    

    

    jugadoresModal.modal('hide');

}
