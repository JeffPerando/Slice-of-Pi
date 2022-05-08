
window.addEventListener("load", () => {
    const uri = $("#qrCodeData").attr('data-url');
    new QRCode($("#qrCode")[0],
        {
            text: uri,
            width: 150,
            height: 150,
            correctLevel: QRCode.CorrectLevel.H
        });
});