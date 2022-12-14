using System.Collections;
using System.Text;

public class Pokemon : IComparable
{
    public string name { get; set; }
    public int height { get; set; }
    public int weight { get; set; }

    public List<Ability> abilities { get; set; }

    public int CompareTo(object obj)
    {
        if (obj is string)
        {
            return this.name.CompareTo(obj);
        }
        
        if (obj is Pokemon)
        {
            return this.name.CompareTo((obj as Pokemon).name);
        }

        return -1;
    }

    public override bool Equals(Object obj)
    {
        if (this.CompareTo(obj) == 0)
            return true;

        return false;
    }

    public override int GetHashCode()
    {
        return this.name.GetHashCode();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();

        sb.Append($"Name:\t{name}\n");
        sb.Append($"Height:\t{height}\n");
        sb.Append($"Weight:\t{weight}\n");
        sb.Append($"Abilities: ({abilities.Count})\n");
        abilities.ForEach(a => sb.Append($"{a.ability.name.ToUpper()}\n"));

        return sb.ToString();
    }


}