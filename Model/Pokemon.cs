using System.Collections;
using System.Text;

public class Pokemon: IEquatable<Pokemon>, IComparable
{
    public string? name { get; set; }
    public int height { get; set; }
    public int weight { get; set; }

    public List<Ability>? abilities { get; set; }

    public int CompareTo(object? obj)
    {
        Pokemon otherPokemon = obj as Pokemon;
        return this.name.CompareTo(otherPokemon.name);
    }

    public bool Equals(Pokemon? other)
    {
        if (this.CompareTo(other) == 0) 
        return true;
        return false;
    }


    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"Name:\t{name}\n");
        sb.Append($"Height:\t{height}\n");
        sb.Append($"Weight:\t{weight}\n");
        sb.Append("Abilities:\n");
        
        foreach (var ability in abilities)
        {
            sb.Append($"{ability.ability.name.ToUpper()}\n");
        }

        return sb.ToString();
    }


}