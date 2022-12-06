using Newtonsoft.Json;

const string cachePath = "cache.json";
string extrator = "web";

string json = String.Empty;
if (extrator == "web")
{
    HttpClient client = new HttpClient();
    json = await client.GetStringAsync("https://pokeapi.co/api/v2/pokemon/");
    File.WriteAllText(cachePath, json);
}
else if (extrator == "cache")
{
    json = File.ReadAllText(cachePath);
}
Console.WriteLine(json);
