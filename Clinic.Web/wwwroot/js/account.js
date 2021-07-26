class AccountManager {
    newImageInput = null;
    newImagePreview = null;
    frmChangeImage = null;
    userIconImages = null;
    btnChangeImage = null;

    constructor() {
        this.newImageInput = document.querySelector("#newImage");
        this.newImagePreview = document.querySelector("#newImagePreview");
        this.frmChangeImage = document.querySelector("#frmChangeImage");
        this.userIconImages = document.querySelectorAll("#profileUserImg");
        this.btnChangeImage = document.querySelector("#btnChangeImage");
    }

    addEvts() {
        this.newImageInput.addEventListener("change", () => { this.previewNewImage(); });
        this.frmChangeImage.addEventListener("submit", (e) => { this.changeImage(e); });
    }

    previewNewImage() {
        const filesOfImageInput = this.newImageInput.files;

        if (!filesOfImageInput || !filesOfImageInput.length) {
            newImagePreview.src = "";
            return;
        }

        const firstFileOfImageInput = filesOfImageInput[0];
        const objectURL = URL.createObjectURL(firstFileOfImageInput);

        this.newImagePreview.src = objectURL;
    }

    changeImage(e) {
        e.preventDefault();

        const url = "/api/Account/Profile/ChangeImage";

        const body = new FormData(frmChangeImage);

        fetch(url, { body: body, method: 'POST' })
            .then(response => response.json())
            .then(json => this.manageResponse(json));
    }

    manageResponse(json) {
        if (json.success) {
            $("#modalNewImage").modal("hide");

            this.userIconImages.forEach(img => img.src = URL.createObjectURL(this.newImageInput.files[0]));
            this.newImagePreview.src = "";
            this.newImageInput.value = null;

            let newImgName = json.newResourceName;
            sessionStorage.setItem("profileImgName", newImgName);
        } else {
            swalAlert('', json.message, false);
        }
    }
}


let accountManager = new AccountManager();
accountManager.addEvts();
