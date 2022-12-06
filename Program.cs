using System.Diagnostics;
using System.Text;
using System.Text.Json;

const string cachePath = "cache.json";

// choose web or cache.
string extrator = "cache";

StringBuilder json = new StringBuilder();
if (extrator == "web")
{
    HttpClient client = new HttpClient();
    json.Append("[");
    for (int i = 1; i <= 15; i++)
    {
        string item = await client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{i}");
        json.Append(item);
        json.Append(",");
    }
    // remove last comma
    json.Remove(json.Length - 1, 1);
    json.Append("]");
    File.WriteAllText(cachePath, json.ToString());
}
else if (extrator == "cache")
{
    json.Append(File.ReadAllText(cachePath));
}

var pockemonList = JsonSerializer.Deserialize<List<Pockemon>>(json.ToString());
foreach (Pockemon p in pockemonList){
    Console.WriteLine(p);
}