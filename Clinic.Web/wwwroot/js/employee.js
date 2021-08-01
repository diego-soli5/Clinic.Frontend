class EmployeeManager {
    identification = "";

    constructor() {
        this.bindSearchEvts();
        this.bindEvts();
    }

    getPaginatedPage(pgnum) {
        $("#tblEmployees").load(`/admin/employee/GetTable?pageNumber=${pgnum}&isPagination=true&identification=${this.identification}`, () => {
            this.bindEvts();
        });
    }

    bindSearchEvts() {
        document.querySelector("#frmSearch").addEventListener('submit', ((e) => {
            e.preventDefault();

            this.identification = $("#identification").val();

            $("#tblEmployees").load(`/admin/employee/GetTable?identification=${this.identification}`, () => {
                this.bindEvts();
            });

        }));
    }

    bindEvts() {
        this.bindCRUDevts();
        this.bindPaginationEvts();
    }

    bindCRUDevts() {

        document.querySelectorAll('tbody tr.pointer').forEach(x => {
            x.addEventListener('click', function (e) {
                let id = e.currentTarget.firstElementChild.value;

                window.location.href = `/Admin/Employee/Details?id=${id}`;
            });
        });

    }

    bindPaginationEvts() {

        document.querySelectorAll('#btnPage').forEach(x => {
            x.addEventListener('click', (e) => {
                e.preventDefault();

                let pgnum = e.target.getAttribute('data-page');

                this.getPaginatedPage(pgnum);
            });
        });

        document.getElementById("btnNextPage").addEventListener("click", (e) => {
            e.preventDefault();

            let pgnum = e.target.getAttribute('data-page');

            this.getPaginatedPage(pgnum);
        });

        document.getElementById("btnPrevPage").addEventListener("click", (e) => {
            e.preventDefault();

            let pgnum = e.target.getAttribute('data-page');

            this.getPaginatedPage(pgnum);
        });
    }
}

var employeeManager = new EmployeeManager();






