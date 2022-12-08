using System.Text;
using System.Text.Json;
using RestSharp;

public class PokemonRepository
{
    private const string myMascotsPath = "MyMascots.json";

    public void save(Pokemon newPokemon)
    {
        List<Pokemon> original = viewAll();
        if (!original.Contains(newPokemon))
            original.Add(newPokemon);
        else
        {
            System.Console.WriteLine("Error! You already have this mascot!");
            return;
        }
        string newList = JsonSerializer.Serialize(original);
        File.WriteAllText(myMascotsPath, newList);
    }

    public List<Pokemon> viewAll()
    {
        string mascots = File.ReadAllText(myMascotsPath);
        return JsonSerializer.Deserialize<List<Pokemon>>(mascots).DistinctBy(p => p.name).ToList();
    }

    public Pokemon getPokemon(string pokemonName)
    {
        var client = new RestClient();
        var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{pokemonName}", Method.Get);
        string item = client.Execute(request).Content;
        return JsonSerializer.Deserialize<Pokemon>(item);

    }
}