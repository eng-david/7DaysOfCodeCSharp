public class AppController
{

    PokemonRepository repository;

    public AppController(PokemonRepository repository)
    {
        this.repository = repository;
        MainMenuController();

    }

    private void MainMenuController()
    {
        var options = new List<string> { "Adopt a mascot", "See your mascots", "Exit" };
        while (true)
        {
            string opt = AppView.MainMenuView(options);

            switch (opt)
            {
                case "1":
                    adoptMascotController();
                    break;
                case "2":
                    seeMascotsController();
                    break;
                case "3":
                    return;
                default:
                    System.Console.WriteLine("Invalid Option!\n");
                    break;
            }
        }
    }

    public void adoptMascotController()
    {
        System.Console.WriteLine("\n --- ADOPT A MASCOT --- \n");

        System.Console.Write("Type the name of mascot for adoption: ");
        string mascotName = Console.ReadLine();
        try
        {
            Pokemon pokemon = repository.getPokemon(mascotName);
            System.Console.WriteLine(pokemon);
            while (true)
            {
                System.Console.WriteLine($"Are you sure you want to adopt {pokemon.name} ?");
                System.Console.WriteLine("1 - Yes");
                System.Console.WriteLine("2 - No");
                string opt = Console.ReadLine();
                switch (opt)
                {
                    case "1":
                        if (repository.save(pokemon))
                            System.Console.WriteLine("Mascot successfully adopted, the egg is hatching.");
                        return;
                    case "2":
                        return;
                    default:
                        System.Console.WriteLine("Invalid Option!!");
                        break;
                }
            }
        }
        catch (Exception)
        {
            System.Console.WriteLine("Mascot not found!");
        }

    }

    public void seeMascotsController()
    {
        List<Pokemon> mascots = repository.viewAll();
        if (mascots.Count > 0)
        {
            System.Console.WriteLine($"\n --- YOUR MASCOTS --- ({mascots.Count})\n");
            mascots.ForEach(p => System.Console.WriteLine(p));
        } 
        else {
            System.Console.WriteLine("\n --- YOU DONT HAVE ANY MASCOTS ---\n");
        }

    }



}