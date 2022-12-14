public class AppController
{

    MascotRepository repository;

    public AppController(MascotRepository repository)
    {
        this.repository = repository;
        AppView.WriteTitle("Tamagotchi");
        MainMenuController();
    }

    private void MainMenuController()
    {
        var options = new Dictionary<string, string>()
        {
            ["1"] = "Adopt a mascot",
            ["2"] = "See your mascots",
            ["q"] = "Exit"
        };

        while (true)
        {
            AppView.WriteTitle("Main Menu");
            switch (AppView.MenuView(options))
            {
                case "1":
                    adoptMascotController();
                    break;
                case "2":
                    seeMascotsController();
                    break;
                case "q":
                    return;
            }
        }
    }

    public void adoptMascotController()
    {
        AppView.WriteTitle("ADOPT A MASCOT");

        string mascotName = AppView.ReadLine("Type the name of mascot for adoption: ");
        try
        {
            Pokemon pokemon = repository.getPokemon(mascotName);
            AppView.WriteLine(pokemon);
            AppView.WriteLine($"Are you sure you want to adopt {pokemon.name} ?");
            var options = new Dictionary<string, string>()
            {
                ["1"] = "Yes",
                ["2"] = "No"
            };
            switch (AppView.MenuView(options))
            {
                case "1":
                    Mascot mascot = new Mascot(pokemon);
                    if (repository.save(mascot))
                        AppView.WriteLine("Mascot successfully adopted, the egg is hatching.");
                    return;
                case "2":
                    return;
            }

        }
        catch (Exception)
        {
            AppView.WriteError("Mascot not found");
        }

    }

    public void seeMascotsController()
    {
        List<Mascot> mascots = repository.viewAll();
        if (mascots.Count > 0)
        {
            AppView.WriteTitle($"YOUR MASCOTS ({mascots.Count})");
            mascots.ForEach(m => AppView.WriteLine(m));
        }
        else
        {
            AppView.WriteError("YOU DONT HAVE ANY MASCOTS");
        }

    }



}