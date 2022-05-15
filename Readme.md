Copyright (c) 2022 Alparslan PARLAK
Requested by MBTR

Requirements
- .NET 6 SDK
- A MS SQL Server Instance
- Postsharp 6.10.10

You'll need to set the SQL server connection string at Api/appsettings.json & Helper/UrlValidator

Endpoints to use:

"/" -> A friendly welcome

"/{tag}" -> Once you hit this endpoint with a tag (aka shorteningTag), application
checks if the hash value points a URL in DB and redirects the browser to the URL if it's
avaible in DB.

"/shortenUrl?initialUrl={longurl}" -> Application generates a hash code and returns it to the
client after binding it to the long url in DB.

"/shortenUrl?initialUrl={longurl}&shorteningTag={tag}" -> Application generates a custom tag and returns it to the
client after binding it to the long url in DB.

*Some classes are nested together and not distributed to other layers as this is a demo app.
