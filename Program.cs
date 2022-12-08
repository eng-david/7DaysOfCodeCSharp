using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RestSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        var repository = new PokemonRepository();

        System.Console.WriteLine(" --- TAMAGOTCHI --- ");
        
        new MainMenu(repository);
        
        //mainMenu(repository);

        // const string cachePath = "cache.json";

        // // choose web or cache.
        // string extrator = "web";

        // StringBuilder json = new StringBuilder();
        // if (extrator == "web")
        // {
        //     var client = new RestClient();
        //     // HttpClient client = new HttpClient();
        //     json.Append("[");
        //     for (int i = 1; i <= 2; i++)
        //     {
        //         var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{i}", Method.Get);
        //         string item = client.Execute(request).Content;
        //         json.Append(item);
        //         json.Append(",");
        //     }
        //     // remove last comma
        //     json.Remove(json.Length - 1, 1);
        //     json.Append("]");
        //     File.WriteAllText(cachePath, json.ToString());
        // }
        // else if (extrator == "cache")
        // {
        //     json.Append(File.ReadAllText(cachePath));
        // }

        // var pockemonList = JsonSerializer.Deserialize<List<Pokemon>>(json.ToString());
        // foreach (Pokemon p in pockemonList)
        // {
        //     Console.WriteLine(p);
        // }

    }    
   
}