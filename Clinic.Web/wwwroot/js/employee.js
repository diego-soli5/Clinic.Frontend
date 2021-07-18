var currentPage = 1;
var ide = "";
var sta = "";

$(function () {

    //INICIA MISC
    function bindEvts(pgnum) {
        bindPaginationEvts();
        bindCRUDevts();

        currentPage = pgnum;
    }
    //FIN MISC

    //INICIA PAGINACION
    $("#frmSearch").submit(function (e) {
        e.preventDefault();

        ide = $("#identification").val();
        sta = $("#EmployeeStatus").val();

        $("#tblEmployees").load(`/admin/employee/GetTable?identification=${ide}&employeeStatus=${sta}`, function () {
            bindEvts(1);
        });

    });

    function bindPaginationEvts() {

        document.querySelectorAll('#btnPage').forEach(x => {
            x.addEventListener('click', function (e) {
                e.preventDefault();

                let pgnum = e.target.getAttribute('data-page');

                getPaginatedPage(pgnum);
            });
        });

        document.getElementById("btnNextPage").addEventListener("click", function (e) {
            e.preventDefault();

            let pgnum = e.target.getAttribute('data-page');

            getPaginatedPage(pgnum);
        });

        document.getElementById("btnPrevPage").addEventListener("click", function (e) {
            e.preventDefault();

            let pgnum = e.target.getAttribute('data-page');

            getPaginatedPage(pgnum);
        });
    }

    function getPaginatedPage(pgnum) {

        $("#tblEmployees").load(`/admin/employee/GetTable?pageNumber=${pgnum}&isPagination=true&identification=${ide}&employeeStatus=${sta}`, function () {
            bindEvts(pgnum);
        });
    }

    bindPaginationEvts();
    //FIN PAGINACION

    //INICIA BOTONES CRUD & FIRE & ACTIVATE
    function bindCRUDevts() {

        document.querySelectorAll('tbody tr.pointer').forEach(x => {
            x.addEventListener('click', function (e) {
                let id = e.currentTarget.firstElementChild.value;

                window.location.href = `/Admin/Employee/Details?id=${id}`;
            });
        });

        document.querySelectorAll("#btnFire").forEach(x => {
            x.addEventListener("click", function (e) {
                e.stopPropagation();

                let id = e.target.getAttribute("data-id");

                Swal.fire({
                    title: 'Despedir Empleado',
                    text: "Estás seguro de realizar la acción?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, estoy seguro.',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {

                        fetch(`/api/admin/employee/fire/${id}`, { method: 'POST' })
                            .then(res => res.json())
                            .then(data => {
                               
                                swalAlert('Despedir', data.message, data.success);

                                if (data.success) {
                                    getPaginatedPage(currentPage);
                                }

                            }).catch(err => {
                                swalAlert("Hubo un error desconocido, asegurate de tener conexión a internet.", false);
                            });
                    }
                })
            });
        });

        document.querySelectorAll("#btnActivate").forEach(x => {
            x.addEventListener("click", function (e) {
                e.stopPropagation();
                
                let id = e.target.getAttribute("data-id");

                Swal.fire({
                    title: 'Activar empleado?',
                    text: "Estás seguro de realizar la acción?",
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonColor: '#3085d6',
                    cancelButtonColor: '#d33',
                    confirmButtonText: 'Si, estoy seguro.',
                    cancelButtonText: 'Cancelar'
                }).then((result) => {
                    if (result.isConfirmed) {

                        fetch(`/api/admin/employee/activate/${id}`, { method: 'POST' })
                            .then(res => res.json())
                            .then(data => {

                                swalAlert('Activar', data.message, data.success);

                                if (data.success) {
                                    getPaginatedPage(currentPage);
                                }
               
                            }).catch(err => {
                                swalAlert("Hubo un error desconocido, asegurate de tener conexión a internet.", false);
                            });
                    }
                })
            });
        });
    }

    bindCRUDevts();
    //FIN BOTONES FIRE & ACTIVATE
});


