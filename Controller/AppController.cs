public class AppController
{

    private MascotRepository repository;

    public AppController(MascotRepository repository)
    {
        this.repository = repository;
        AppView.WriteTitle("Tamagotchi");
        MainMenu();
    }

    private void MainMenu()
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
                    adoptMascot();
                    break;
                case "2":
                    seeMascots();
                    break;
                case "q":
                    return;
            }
        }
    }

    private void adoptMascot()
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

    private void seeMascots()
    {
        List<Mascot> mascots = repository.viewAll();
        if (mascots.Count > 0)
        {
            AppView.WriteTitle($"YOUR MASCOTS ({mascots.Count})");

            var options = new Dictionary<string, string>();
            for (int i = 0; i < mascots.Count; i++)
            {
                options.Add((i + 1).ToString(), $"Name: {mascots[i].name}\tHealth: {AppView.ProgressBar(mascots[i].health.value)}");
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
        AppView.WriteTitle($"Mascot: {mascot.name}");

        if (mascot.health.value == 0)
        {
            AppView.WriteLine(@"
  ##   
######
  ##
  ##
  ##
  ##
   ");
   return;
        }

        AppView.WriteLine("\t(=^.^=)\n");
        var options = new Dictionary<string, string>()
        {
            ["1"] = $"Feed {mascot.name}",
            ["2"] = $"Play whit {mascot.name}",
            ["3"] = $"Put {mascot.name} to sleep",
            ["4"] = "Take Medicine",
            ["5"] = $"Donate {mascot.name}",
            ["q"] = "Exit"
        };

        while (true)
        {
            AppView.WriteLine($"Health:\t\t{AppView.ProgressBar(mascot.health.value)}\t{mascot.GetHealthMessage()}");
            AppView.WriteLine($"Hungry:\t\t{AppView.ProgressBar(mascot.hungry.value)}\t{mascot.GetHungryMessage()}");
            AppView.WriteLine($"Sleepy:\t\t{AppView.ProgressBar(mascot.sleepy.value)}\t{mascot.GetSleepyMessage()}");
            AppView.WriteLine($"Happiness:\t{AppView.ProgressBar(mascot.happiness.value)}\t{mascot.GetHappinessMessage()}");
            AppView.WriteLine($"Tiredness:\t{AppView.ProgressBar(mascot.tiredness.value)}\t{mascot.GetTirednessMessage()}");

            switch (AppView.MenuView(options))
            {
                case "1":
                    mascot.Eat();
                    AppView.WriteLine($"{mascot.name} have eat some food.\n");
                    break;
                case "2":
                    mascot.Play();
                    AppView.WriteLine($"{mascot.name} have fun!\n");
                    break;
                case "3":
                    mascot.Sleep();
                    AppView.WriteLine($"{mascot.name} is sleeping. (=^.^=) zzzz\n");
                    break;
                case "4":
                    mascot.TakeMedicine();
                    AppView.WriteLine($"{mascot.name} have take some medicine.\n");
                    break;
                case "5":
                    if (AppView.YesNoMenu($"Are you sure to donate {mascot.name}?"))
                        AppView.WriteLine("Bye Bye!!! (=^.^=)");
                    repository.remove(mascot);
                    return;
                case "q":
                    repository.update(mascot);
                    return;
            }
        }

    }

}