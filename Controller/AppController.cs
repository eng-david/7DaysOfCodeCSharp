public class AppController
{

    private MascotRepository repository;

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

    private void adoptMascotController()
    {
        AppView.WriteTitle("ADOPT A MASCOT");

        string mascotName = AppView.ReadLine("Type the name of mascot for adoption: ");
        try
        {
            Pokemon pokemon = repository.getPokemon(mascotName);
            AppView.WriteLine(pokemon);

            if (AppView.YesNoMenu($"Are you sure you want to adopt {pokemon.name} ?"))
            {
                repository.save(new Mascot(pokemon));
                AppView.WriteLine("Mascot successfully adopted, the egg is hatching.");
            }
        }
        catch (Exception e)
        {
            AppView.WriteError(e.Message);
        }

    }

    private void seeMascotsController()
    {
        List<Mascot> mascots = repository.viewAll();
        if (mascots.Count > 0)
        {
            AppView.WriteTitle($"YOUR MASCOTS ({mascots.Count})");

            var options = new Dictionary<string, string>();
            for (int i = 0; i < mascots.Count; i++)
            {
                options.Add((i + 1).ToString(), $"Name: {mascots[i].name}\tHealth: {AppView.ProgressBar(mascots[i].health)}");
            }
            options.Add("q", "Exit");
            string opt = AppView.MenuView(options);
            if (opt == "q")
                return;
            ViewMascotDetails(mascots[int.Parse(opt) - 1]);
        }
        else
            AppView.WriteError("YOU DONT HAVE ANY MASCOTS");
    }

    private void ViewMascotDetails(Mascot mascot)
    {

        while (true)
        {
            AppView.WriteTitle($"Mascot: {mascot.name}");
            AppView.WriteTitle(@"");

            string healthMessage = "";
            string hungryMessage = "";
            string sleepyMessage = "";
            string happinessMessage = "";
            string tirednessMessage = "";

            AppView.WriteLine($"Health:\t\t{AppView.ProgressBar(mascot.health)}\t{healthMessage}");
            AppView.WriteLine($"Hungry:\t\t{AppView.ProgressBar(mascot.hungry)}\t{hungryMessage}");
            AppView.WriteLine($"Sleepy:\t\t{AppView.ProgressBar(mascot.sleepy)}\t{sleepyMessage}");
            AppView.WriteLine($"Happiness:\t{AppView.ProgressBar(mascot.happiness)}\t{happinessMessage}");
            AppView.WriteLine($"Tiredness:\t{AppView.ProgressBar(mascot.tiredness)}\t{tirednessMessage}");

            var options = new Dictionary<string, string>()
            {
                ["1"] = $"Feed {mascot.name}",
                ["2"] = $"Play whit {mascot.name}",
                ["3"] = $"Put {mascot.name} to sleep",
                ["4"] = $"Donate {mascot.name}",
                ["q"] = "Exit"
            };

            switch (AppView.MenuView(options))
            {
                case "1":
                    mascot.eat();
                    AppView.WriteLine($"{mascot.name} have eat some food.\n(=^ш^=)");
                    break;
                case "2":
                    mascot.play();
                    AppView.WriteLine($"{mascot.name} have fun!\n(=^ш^=)");
                    break;
                case "3":
                    mascot.sleep();
                    AppView.WriteLine($"{mascot.name} is sleeping.\n(=^ш^=)");
                    break;
                case "4":
                    if (AppView.YesNoMenu($"Are you sure to donate {mascot.name}?"))
                        repository.remove(mascot);
                    return;
                case "q":
                    repository.update(mascot);
                    return;
            }
        }

    }

}