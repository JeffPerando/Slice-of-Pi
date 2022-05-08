
window.addEventListener("load", () => {
    const uri = $("#qrCodeData").attr('data-url');
    new QRCode($("#qrCode")[0],
        {
            text: uri,
            width: 128,
            height: 128,
            correctLevel: QRCode.CorrectLevel.H
        });
});