//INICIA FUNCIONES SweetAlert
function swalAlert(message, success) {
    if (success) {
        Swal.fire(
            'Exito!',
            message,
            'success'
        )
    }
    else {
        Swal.fire(
            'Hubo un problema...',
            message,
            'error'
        )
    }
}
//FIN FUNCIONES SweetAlert
