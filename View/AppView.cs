public static class AppView
{
    public static void WriteTitle(string title)
    {
        System.Console.WriteLine($"\n\t --- {title.ToUpper()}  --- \n");
    }

    public static void WriteError(string text)
    {
        System.Console.WriteLine($"\n ERROR: {text.ToLower()}! \n");
    }

    public static void WriteLine(Object text)
    {
        System.Console.WriteLine(text);
    }

    public static string ReadLine(){
        return System.Console.ReadLine();
    }

    public static string ReadLine(string text){
        System.Console.Write(text + " ");
        return System.Console.ReadLine();
    }



    public static string MenuView(Dictionary<string, string> options)
    {

        Console.WriteLine("choose an option:");

        foreach (var option in options)
        {
            System.Console.WriteLine(option.Key + " - " + option.Value);
        }

        while (true)
        {
            string opt = Console.ReadLine();
            if (options.Keys.Contains(opt))
                return opt;
            WriteError("invalid option, try again");
        }
    }

}