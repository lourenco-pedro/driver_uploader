using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using static Google.Apis.Drive.v3.DriveService;

public class Drive
{
    DriveService service;

    public Drive(Credentials credentials)
    {
        TokenResponse tokenResponse = new TokenResponse
        {
            AccessToken = credentials.AccessToken,
            RefreshToken = credentials.RefreshToken
        };

        string applicationName = credentials.AppName;
        string username = credentials.Email;

        var apiCodeFlow = new GoogleAuthorizationCodeFlow
        (
            new GoogleAuthorizationCodeFlow.Initializer
            {
                ClientSecrets = new Google.Apis.Auth.OAuth2.ClientSecrets
                {
                    ClientId = credentials.ClientId,
                    ClientSecret = credentials.ClientSecret
                },
                Scopes = new[] { Scope.Drive },
                DataStore = new FileDataStore(applicationName)
            }
        );

        var credential = new UserCredential(apiCodeFlow, username, tokenResponse);
        
        service = new DriveService(new BaseClientService.Initializer
        {
            HttpClientInitializer = credential,
            ApplicationName = applicationName
        });
    }

    public void UploadFile(Stream file, string fileName, string fileMime, string fileDescription, string filePath)
    {
        if(null == service)
        {
            throw new ServiceNotInitializedException();
        }

        Google.Apis.Drive.v3.Data.File driveFile = new Google.Apis.Drive.v3.Data.File();
        driveFile.Name = fileName;
        driveFile.Description = fileDescription;
        driveFile.MimeType = fileMime;

        driveFile.Parents = new[] { filePath };

        var request = service.Files.Create(driveFile, file, fileMime);
        request.Fields = "id";

        var response =  request.Upload();
        if(response.Status != Google.Apis.Upload.UploadStatus.Completed)
            throw response.Exception;
    }

    public struct Credentials
    {
        public string AccessToken;
        public string RefreshToken;
        public string AppName;
        public string Email;
        public string ClientId;
        public string ClientSecret;
    }

    public class ServiceNotInitializedException : Exception
    {
    }
}