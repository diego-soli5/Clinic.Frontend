var currentPage = 1;

$(function () {

    //INICIA MISC
    function bindEvts(pgNum) {

        currentPage = pgNum;
    }
    //FIN MISC

    //INICIA PAGINACION
    $("#frmSearch").submit(function (e) {
        e.preventDefault();

        let ide = $("#identification").val();
        let ms = $("#medicalSpecialty").val();

        $("#tblEmployees").load(`/client/medic/GetTable?identification=${ide}&medicalSpecialty=${ms}`, function () {
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

        $("#tblMedics").load(`/client/medic/GetTable?pageNumber=${pgnum}&isPagination=true`, function () {
            bindEvts(pgnum);
        });
    }

    bindPaginationEvts();
    //FIN PAGINACION
});