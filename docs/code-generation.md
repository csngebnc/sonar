## Frontend code generation

It is possible to generate typescript clients using NSwag.

First download and install NSwag Studio on your computer.
`https://github.com/RicoSuter/NSwag/wiki/NSwagStudio`

If you have NSwag Studio already installed, you just have to start the following batch file.

`{root}/tools/generate-frontend-service-clients.bat`

This will run the generation process for each service, then you can check the generated code in the `{root}/src/frontend/main/src/generated` folder.
