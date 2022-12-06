using System.Diagnostics;
using Newtonsoft.Json;

const string cachePath = "cache.json";

// choose web or cache.
string extrator = "cache";

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

var pockemomList = JsonConvert.DeserializeObject<List<Pockemon>>(json);

foreach (Pockemon p in pockemomList){
    System.Console.WriteLine(p);
}