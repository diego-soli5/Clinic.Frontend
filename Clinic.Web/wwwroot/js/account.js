const imageInput = document.querySelector("#image");
const imagePreview = document.querySelector("#previewImage");
const frmImage = document.querySelector("#frmImage");
const userIconImages = document.querySelectorAll("#userImage");
const btn = document.querySelector("#btnImg");

imageInput.addEventListener("change", () => {
    const filesOfImageInput = imageInput.files;

    if (!filesOfImageInput || !filesOfImageInput.length) {
        imagePreview.src = "";
        return;
    }

    const firstFileOfImageInput = filesOfImageInput[0];
    const objectURL = URL.createObjectURL(firstFileOfImageInput);

    imagePreview.src = objectURL;
});

frmImage.addEventListener("submit", function (e) {
    e.preventDefault();

    const url = "/api/Account/Profile/ChangeImage";

    const body = new FormData(frmImage);

    fetch(url, { body: body, method: 'POST' })
        .then(response => response.json())
        .then(json => {
            if (json.success) {
                $("#modalImg").modal("hide");
                btn.innerText = "Cambiar Imagen";
                userIconImages.forEach(img => img.src = URL.createObjectURL(imageInput.files[0]));
                imagePreview.src = "";
                imageInput.value = null;

            } else {
                swalAlert('', json.message, false);
            }
        });
});