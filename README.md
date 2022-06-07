# Slice-of-Pi

Capstone project for CS461 and CS462 at Western Oregon University

To use this project, you'll need the following API keys:

* `apiFBIKey`: The key to the [FBI Crime Statistics API](https://crime-data-explorer.fr.cloud.gov/pages/docApi)
* `ATTOMKey`: The key to the [ATTOM Property API](https://api.developer.attomdata.com/docs)
* `captchaServerKey`: The server-side key for [reCAPTCHA V3](https://developers.google.com/recaptcha/docs/v3)
  * client-side key is currently hardcoded with no easy fix
* `EmailPW`: The app password to the associated [GMail](https://developers.google.com/gmail/api) account
  * username currently hardcoded to `sliceofpi.cs46x`
* `GoogleKey`: The key to the [Google Maps API](https://developers.google.com/maps/documentation/streetview/overview)
* `PrivateGoogleAuthKey`: The private authorization key to the same

And the following connection strings are needed:
* `ApplicationDbContextConnection`: The SQL database for application-side data; Mostly user metadata like addresses.
* `MainIdentityDbContextConnection`: The SQL database containing all ASP.NET Identity data, which includes user logins
* `APICacheDbContextConnection`: The MongoDB database for API caching. Doesn't need UP or DOWN scripts due to the fluid nature of MongoDB.
  * This must be a `mongodb+srv://` connection string.

The SQL databases need to be populated using `UP.sql` and `ID_UP.sql` respectively. MongoDB needs no scripts.

All database scripts can be found under `Website/Main/Scripts`.
