# driver_uploader
Simple service that implement Google Drive API

## How to run

Just hit the dotnet command in the shell:

```vb.net
dotnet run -file <filePath> -mime <file_type>
```

The desired folder where the file will be uploaded is specified inside the **.env** file with variable name `APK_FOLDER`. The folder value needs to be the url that appears after the `https://drive.google.com/drive/u/0/folders/` path. 

## Prerequisites

This code assumes that you already have the ACCESS TOKEN, REFRESH TOKEN, CLIENT ID, and CLIENT SECRET in hand to be loaded up by the Environment class. If you don't have these strings already, you can follow [Alejandro Dominguez](https://medium.com/geekculture/upload-files-to-google-drive-with-c-c32d5c8a7abc) on how you can grab them.

## Environment file

Every environment variable needs to be specified within the **.env** file. The variables are:

```text
ACCESS_TOKEN= <your_access_token>
REFRESH_TOKEN= <your_refresh_token>
EMAIL= <your_email>
CLIENT_ID= <your_client_id>
CLIENT_SECRET= <your_client_secret>
APK_FOLDER= <driver_folder_link>
```
