var ide = "";
var sta = "";

$(function () {

    //INICIA MISC
    function bindEvts() {
        bindPaginationEvts();
        bindCRUDevts();
    }
    //FIN MISC

    //INICIA PAGINACION
    $("#frmSearch").submit(function (e) {
        e.preventDefault();

        ide = $("#identification").val();
        sta = $("#EmployeeStatus").val();

        $("#tblEmployees").load(`/admin/employee/GetTable?identification=${ide}&employeeStatus=${sta}`, function () {
            bindEvts();
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
            bindEvts();
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

    }

    bindCRUDevts();
    //FIN BOTONES FIRE & ACTIVATE
});


