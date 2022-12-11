using System.Text;
using System.Text.Json;
using RestSharp;

public class PokemonRepository
{
    private const string myMascotsPath = "MyMascots.json";
    private const string cachePath = "cache.json";

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
        // Check if it exists in cache
        string cache = File.ReadAllText(cachePath);
        List<Pokemon> cachePokemonList = JsonSerializer.Deserialize<List<Pokemon>>(cache).DistinctBy(p => p.name).ToList();
        foreach (Pokemon p in cachePokemonList)
        {
            if (p.name.Equals(pokemonName))
                return p;
        }

        // Get from PokeAPI
        var client = new RestClient();
        var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{pokemonName}", Method.Get);
        string item = client.Execute(request).Content;
        
        // Save new pokemon to cache
        Pokemon newPokemon = JsonSerializer.Deserialize<Pokemon>(item);
        cachePokemonList.Add(newPokemon);
        string newCache = JsonSerializer.Serialize(cachePokemonList);
        File.WriteAllText(cachePath, newCache);

        return newPokemon;

    }
}