using System.Diagnostics;
using System.Text;
using System.Text.Json;
using RestSharp;

internal class Program
{
    private static void Main(string[] args)
    {
        var repository = new PokemonRepository();

        System.Console.WriteLine(" --- TAMAGOTCHI --- ");
        
        new App(repository);
        
    }    
   
}