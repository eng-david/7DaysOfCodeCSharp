using System.Text;
using System.Text.Json;
using RestSharp;

public class MascotRepository
{
    private const string myMascotsPath = "MyMascots.json";
    private const string cachePath = "cache.json";

    public bool save(Mascot newMascot)
    {
        List<Mascot> myMascots = viewAll();
        if (!myMascots.Contains(newMascot))
            myMascots.Add(newMascot);
        else
        {
            System.Console.WriteLine("Error! You already have this mascot!");
            return false;
        }
        string newList = JsonSerializer.Serialize(myMascots);
        File.WriteAllText(myMascotsPath, newList);
        return true;
    }

    public List<Mascot> viewAll()
    {
        string mascots = File.ReadAllText(myMascotsPath);
        return JsonSerializer.Deserialize<List<Mascot>>(mascots).DistinctBy(p => p.name).ToList();
    }

    public Pokemon getPokemon(string pokemonName)
    {
        if (pokemonName == string.Empty)
            throw new ArgumentException();

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