
//This is our public reCaptcha key.
//Note: due to complications in importing the Google dependency, this is repeated for every instance of a reCaptcha.
//I have no idea why importing from HTML doesn't trigger CORS, but importing from JS does.
let clientKey = "6LclWdYeAAAAAC9jApJxsbBpuHEUIODlc8yOICgn";

function validateCaptcha(reason) {
    grecaptcha.ready(function () {
        grecaptcha.execute(clientKey, { action: reason }).then(function (token) {
            $("#cappy").val(token);
        });

    });

}
