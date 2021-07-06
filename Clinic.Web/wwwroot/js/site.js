//INICIA FUNCIONES SweetAlert
function swalAlert(title, message, success) {
    if (success) {
        Swal.fire(
            title,
            message,
            'success'
        )
    }
    else {
        Swal.fire(
            title,
            message,
            'error'
        )
    }
}

function openRedirectionModal(title, url) {
    let timerInterval
    Swal.fire({
        title: title,
        html: 'Redireccionando en <b></b> milisegundos.',
        timer: 5000,
        timerProgressBar: true,
        didOpen: () => {
            Swal.showLoading()
            timerInterval = setInterval(() => {
                const content = Swal.getHtmlContainer()
                if (content) {
                    const b = content.querySelector('b')
                    if (b) {
                        b.textContent = Swal.getTimerLeft()
                    }
                }
            }, 100)
        },
        willClose: () => {
            clearInterval(timerInterval)
        }
    }).then((result) => {
        window.location.href = url
    });
}
//FIN FUNCIONES SweetAlert
