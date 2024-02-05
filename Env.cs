using System.Reflection;

public class Env
{

    Dictionary<string, string> EnviromentValues;
    static Env instance;

    public static void Init()
    {
        instance = new Env();

        instance.EnviromentValues = new Dictionary<string, string>();
        
        string path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        Console.WriteLine(path);

        string[] fileContent = File.ReadAllLines(Path.Combine(path, ".env"));

        Array.ForEach(fileContent, content =>
        {
            string key = content.Split("=")[0];
            string value = content.Split("=")[1];

            instance.EnviromentValues.Add(key, value);
        });
    }

    public static string ValueOf(string key)
    {
        return instance.EnviromentValues[key];
    }
}