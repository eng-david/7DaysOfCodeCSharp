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
            AppView.WriteError("You already have this mascot");
            return false;
        }
        string newList = JsonSerializer.Serialize(myMascots.Distinct());
        File.WriteAllText(myMascotsPath, newList);
        return true;
    }

    public List<Mascot> viewAll()
    {
        string mascots = File.ReadAllText(myMascotsPath);
        return JsonSerializer.Deserialize<List<Mascot>>(mascots);
    }

    public Pokemon getPokemon(string pokemonName)
    {
        if (pokemonName == string.Empty)
            throw new ArgumentException();

        // Check if it exists in cache
        string cache = File.ReadAllText(cachePath);
        List<Pokemon> cachePokemonList = JsonSerializer.Deserialize<List<Pokemon>>(cache).ToList();
        foreach (Pokemon p in cachePokemonList)
        {
            if (p.Equals(pokemonName))
                return p;
        }

        // Get from PokeAPI
        var client = new RestClient();
        var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{pokemonName}", Method.Get);
        string json = client.Execute(request).Content;

        // Save new pokemon to cache
        Pokemon newPokemon = JsonSerializer.Deserialize<Pokemon>(json);
        cachePokemonList.Add(newPokemon);
        string newCache = JsonSerializer.Serialize(cachePokemonList.Distinct());
        File.WriteAllText(cachePath, newCache);

        return newPokemon;

    }
}