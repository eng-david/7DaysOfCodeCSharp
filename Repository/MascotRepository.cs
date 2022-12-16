using System.Text;
using System.Text.Json;
using RestSharp;

public class MascotRepository
{
    private string myMascotsPath;
    private const string cachePath = "cache.json";

    public MascotRepository(string fileName)
    {
        this.myMascotsPath = fileName;
    }

    public void save(Mascot newMascot)
    {
        List<Mascot> myMascots = viewAll();
        if (!myMascots.Contains(newMascot))
            myMascots.Add(newMascot);
        else
        {
            throw new Exception("You already have this mascot");
        }

        saveList(myMascots);
    }

    public void update(Mascot mascot)
    {
        List<Mascot> myMascots = viewAll();

        myMascots.Remove(mascot);
        myMascots.Add(mascot);

        saveList(myMascots);
    }

    public void remove(Mascot mascot)
    {
        List<Mascot> myMascots = viewAll();
        myMascots.Remove(mascot);
        saveList(myMascots);
    }

    private void saveList(List<Mascot> updatedList)
    {
        string myMascotsUpdated = JsonSerializer.Serialize(updatedList.Distinct(), new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(myMascotsPath, myMascotsUpdated);
    }

    public List<Mascot> viewAll()
    {
        string mascots = File.ReadAllText(myMascotsPath);
        List<Mascot> list = JsonSerializer.Deserialize<List<Mascot>>(mascots);
        list.Sort();

        return list;
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

        try
        {
            // Get from PokeAPI
            var client = new RestClient();
            var request = new RestRequest($"https://pokeapi.co/api/v2/pokemon/{pokemonName}", Method.Get);
            RestResponse response = client.Execute(request);
            if (!response.IsSuccessful)
                throw new Exception("Mascot not found");
            string json = response.Content;
            
            // Save new pokemon to cache
            Pokemon newPokemon = JsonSerializer.Deserialize<Pokemon>(json);
            cachePokemonList.Add(newPokemon);
            string newCache = JsonSerializer.Serialize(cachePokemonList.Distinct(), new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(cachePath, newCache);

            return newPokemon;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }

    }
}