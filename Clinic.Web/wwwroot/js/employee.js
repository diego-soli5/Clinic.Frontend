$(function () {
    function bindEvents() {
        $("#frmSearch").submit(function (e) {
            e.preventDefault();

            let ide = $("#identification").val();
            let status = $("#EmployeeStatus").val();

            $("#tblEmployees").load(`/admin/employee/GetTable?identification=${ide}&employeeStatus=${status}`, function () {
                bindEvents();
            });
        });

        document.querySelectorAll('#btnPage').forEach(x => {
            x.addEventListener('click', function (e) {
                e.preventDefault();

                let pgnum = e.target.getAttribute('data-page');

                $("#tblEmployees").load(`/admin/employee/GetTable?pageNumber=${pgnum}`, function () {
                    bindEvents();
                });
            });
        });
    }

    bindEvents();
    
});
