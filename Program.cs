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
        System.Console.Write("Hello, what is your name: ");
        //string name = Console.ReadLine();
        
        mainMenu(repository);

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
    private static void mainMenu(PokemonRepository repository)
    {
        System.Console.WriteLine(" --- MENU --- ");
        System.Console.WriteLine("1 - Adopt a mascot");
        System.Console.WriteLine("2 - See your mascots");
        System.Console.WriteLine("3 - Exit");
        string mainMenuOpt = Console.ReadLine();

        switch (mainMenuOpt)
        {
            case "1":
                adoptMascot(repository);
                break;
            case "2":
                seeMascots(repository);
                break;
            default:
                return;

        }
    }

    private static void adoptMascot(PokemonRepository repository)
    {
        System.Console.WriteLine(" --- ADOPT A MASCOT --- ");
        System.Console.Write("Type the name of mascot for adoption: ");
        string? mascotName = Console.ReadLine();

        if (mascotName == null)
            mainMenu(repository);

        Pokemon pokemon = getPokemon(mascotName);
        System.Console.WriteLine(pokemon);
        System.Console.WriteLine($"Are you sure you want to adopt {pokemon.name} ?");
        System.Console.WriteLine("1 - Yes");
        System.Console.WriteLine("2 - No");
        string adoptOpt = Console.ReadLine();

        switch (adoptOpt)
        {
            case "1":
                repository.Pokemons.Add(pokemon);
                mainMenu(repository);
                break;
            case "2":
                adoptMascot(repository);
                break;
            default:
                return;
        }
    }

    private static Pokemon getPokemon(string pokemonName)
    {

        // HttpClient client = new HttpClient();
        // string json = client.GetStringAsync($"https://pokeapi.co/api/v2/pokemon/{pokemonName}");
        // return JsonSerializer.Deserialize<Pokemon>(json);

        var client = new RestClient();
        var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{pokemonName}", Method.Get);
        string item = client.Execute(request).Content;
        return JsonSerializer.Deserialize<Pokemon>(item);

    }

    private static void seeMascots(PokemonRepository repository)
    {
        System.Console.WriteLine(" --- YOUR MASCOTS --- ");
        foreach (Pokemon p in repository.Pokemons)
        {
            Console.WriteLine(p);
        }
        mainMenu(repository);
    }
}