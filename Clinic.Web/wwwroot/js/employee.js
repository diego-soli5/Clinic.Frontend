$(function () {

    $("#frmSearch").submit(function (e) {
         e.preventDefault();
 
         let ide = $("#identification").val();
         let status = $("#EmployeeStatus").val();
 
         $("#tblEmployees").load(`/admin/employee/GetTable?identification=${ide}&employeeStatus=${status}`, function () {
             bindPaginationEvts();
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

        $("#tblEmployees").load(`/admin/employee/GetTable?pageNumber=${pgnum}&isPagination=true`, function () {
            bindPaginationEvts();
        });
    }

    bindPaginationEvts();

});
