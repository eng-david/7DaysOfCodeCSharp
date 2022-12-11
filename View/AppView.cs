public static class AppView
{
    public static string MainMenuView(List<string> options)
    {

        System.Console.WriteLine("\n --- MENU --- \n");
        
        for (int i = 1; i < options.Count + 1; i++)
        {
            System.Console.WriteLine($"{i} - {options[i-1]}");
        }

        return Console.ReadLine();

    }

}