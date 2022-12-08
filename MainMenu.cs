public class MainMenu
{

    PokemonRepository repository;

    public MainMenu(PokemonRepository repository)
    {
        this.repository = repository;

        System.Console.Write("Hello, what is your name: ");
        //string name = Console.ReadLine();

        while (true)
        {
            System.Console.WriteLine(" --- MENU --- ");
            System.Console.WriteLine("1 - Adopt a mascot");
            System.Console.WriteLine("2 - See your mascots");
            System.Console.WriteLine("3 - Exit");
            string mainMenuOpt = Console.ReadLine();

            switch (mainMenuOpt)
            {
                case "1":
                    adoptMascot(repository);
                    break;
                case "2":
                    seeMascots(repository);
                    break;
                default:
                    return;

            }
        }
    }

    private static void adoptMascot(PokemonRepository repository)
    {
        System.Console.WriteLine(" --- ADOPT A MASCOT --- ");
        System.Console.Write("Type the name of mascot for adoption: ");
        string mascotName = Console.ReadLine();

        if (mascotName == String.Empty)
            return;

        Pokemon pokemon = repository.getPokemon(mascotName);
        System.Console.WriteLine(pokemon);
        System.Console.WriteLine($"Are you sure you want to adopt {pokemon.name} ?");
        System.Console.WriteLine("1 - Yes");
        System.Console.WriteLine("2 - No");
        string adoptOpt = Console.ReadLine();

        switch (adoptOpt)
        {
            case "1":
                repository.save(pokemon);
                break;
            default:
                adoptMascot(repository);
                break;
        }
    }

    private static void seeMascots(PokemonRepository repository)
    {
        System.Console.WriteLine(" --- YOUR MASCOTS --- ");
        foreach (Pokemon p in repository.viewAll())
        {
            Console.WriteLine(p);
        }
    }

}