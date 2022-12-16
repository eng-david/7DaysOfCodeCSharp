using System.Text;

internal static class AppView
{
    internal static void WriteTitle(string title)
    {
        System.Console.WriteLine($"\n\t --- {title.ToUpper()}  --- \n");
    }

    internal static void WriteError(string text)
    {
        System.Console.WriteLine($"\n ERROR: {text.ToLower()}! \n");
    }

    internal static void WriteLine(Object text)
    {
        System.Console.WriteLine(text);
    }

    internal static string ReadLine()
    {
        return System.Console.ReadLine();
    }

    internal static string ReadLine(string text)
    {
        System.Console.Write(text + " ");
        return System.Console.ReadLine();
    }



    internal static string MenuView(Dictionary<string, string> options)
    {

        Console.WriteLine("\nchoose an option:");

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

    internal static bool YesNoMenu(string message)
    {

        Console.WriteLine($"\n{message}");

        var options = new Dictionary<string, string>()
        {
            ["Y"] = "Yes",
            ["N"] = "No"
        };

        foreach (var option in options)
        {
            System.Console.WriteLine(option.Key + " - " + option.Value);
        }

        while (true)
        {
            string opt = Console.ReadLine().ToUpper();
            switch (opt)
            {
                case "Y":
                    return true;
                case "N":
                    return false;
                default:
                    WriteError("invalid option, try again");
                    break;
            }
        }
    }


    internal static string ProgressBar(int value, int total = 10)
    {
        if (value > total)
            value = total;
        var sb = new StringBuilder(32);
        sb.Append('|');
        Enumerable.Range(0, value).ToList().ForEach(i => sb.Append('='));
        Enumerable.Range(0, total - value).ToList().ForEach(i => sb.Append('-'));
        sb.Append('|');

        return sb.ToString();
    }
}