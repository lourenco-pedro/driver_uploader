public class Program
{
    public static int Main(string[] args)
    {
        if(args.Length == 0)
        {
            throw new NoArgumentsGivenExeption();
        }

        string filePath = GetArgValue(args, "-file");
        string mime = GetArgValue(args, "-mime");

        const string accessTokenEnvironment = "ACCESS_TOKEN";
        const string refreshTokenEnvironment = "REFRESH_TOKEN";
        const string clientIdEnvironment = "CLIENT_ID";
        const string clientSecretEnvironment = "CLIENT_SECRET";
        const string emailEnvironment = "EMAIL";
        const string apkRootFoldersEnvironment = "APK_FOLDER";

        Env.Init();

        Drive.Credentials credentials = new Drive.Credentials
        {
            AccessToken = Env.ValueOf(accessTokenEnvironment),
            RefreshToken = Env.ValueOf(refreshTokenEnvironment),
            AppName = "driver_uploader",
            ClientId = Env.ValueOf(clientIdEnvironment),
            ClientSecret = Env.ValueOf(clientSecretEnvironment),
            Email = Env.ValueOf(emailEnvironment)
        };

        Drive driveService = new Drive(credentials);

        Console.WriteLine("Drive service Initialized!");
        Console.WriteLine("Sending file to drive...");

        try
        {
            using(FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                string fileName = Path.GetFileName(fileStream.Name);
                try
                {
                    driveService.UploadFile(fileStream, fileName, mime, string.Empty, Env.ValueOf(apkRootFoldersEnvironment));
                    Console.WriteLine("Operation complete!");
                    return 0;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Operation fail with exception");
                    Console.WriteLine(e);
                    return 1;
                }
            }
        }
        catch
        {
            throw new InvalidOperationException();
        }
    }

    static string GetArgValue(string[] args, string arg)
    {
        int indexOf = Array.IndexOf(args, arg);
        string value = args[indexOf+1];
        return value;
    } 

    class NoArgumentsGivenExeption : Exception
    {}
}