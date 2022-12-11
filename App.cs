public class App
{

    PokemonRepository repository;

    public App(PokemonRepository repository)
    {
        this.repository = repository;
        MainMenu();
    }

    private void MainMenu()
    {

        System.Console.WriteLine("\n --- MENU --- \n");
        System.Console.WriteLine("1 - Adopt a mascot");
        System.Console.WriteLine("2 - See your mascots");
        System.Console.WriteLine("3 - Exit");
        string mainMenuOpt = Console.ReadLine();

        switch (mainMenuOpt)
        {
            case "1":
                adoptMascot();
                break;
            case "2":
                seeMascots();
                break;
            case "3":
                return;
            default:
                System.Console.WriteLine("Invalid Option!\n");
                MainMenu();
                break;
        }
    }


    private void adoptMascot()
    {
        System.Console.WriteLine("\n --- ADOPT A MASCOT --- \n");
        System.Console.Write("Type the name of mascot for adoption: ");
        string mascotName = Console.ReadLine();

        if (mascotName == String.Empty)
            return;

        Pokemon pokemon = null;
        try
        {
            pokemon = repository.getPokemon(mascotName);
        }
        catch (Exception)
        {
            System.Console.WriteLine("Mascot not found!");
            adoptMascot();
        }

        System.Console.WriteLine(pokemon);
        System.Console.WriteLine($"Are you sure you want to adopt {pokemon.name} ?");
        System.Console.WriteLine("1 - Yes");
        System.Console.WriteLine("2 - No");
        string opt = Console.ReadLine();

        switch (opt)
        {
            case "1":
                repository.save(pokemon);
                System.Console.WriteLine("Mascot successfully adopted, the egg is hatching.");
                MainMenu();
                break;
            case "2":
                adoptMascot();
                break;
            default:
                System.Console.WriteLine("Invalid Option!!");
                adoptMascot();
                break;
        }
    }

    private void seeMascots()
    {
        List<Pokemon> mascots = repository.viewAll();
        System.Console.WriteLine($"\n --- YOUR MASCOTS --- ({mascots.Count})\n");
        mascots.ForEach(p => System.Console.WriteLine(p));
        MainMenu();
    }

}