public class Mascot : Pokemon, IComparable<Mascot>
{
    public int hungry { get; set; }
    public int sleepy { get; set; }
    public int happiness { get; set; }
    public int tiredness { get; set; }
    public int health { get; set; }

    public Mascot()
    {
        this.hungry = 5;
        this.sleepy = 5;
        this.happiness = 5;
        this.tiredness = 5;
        this.health = 8;
    }

    public Mascot(Pokemon pokemon)
    {
        this.hungry = 5;
        this.sleepy = 5;
        this.happiness = 5;
        this.tiredness = 5;
        this.health = 8;
        this.name = pokemon.name;
        this.height = pokemon.height;
        this.weight = pokemon.weight;
        this.abilities = pokemon.abilities;
    }

    public void eat()
    {
        if (this.hungry > 0)
            this.hungry--;
        this.sleepy++;
        this.happiness++;
    }

    public void sleep()
    {
        this.sleepy = 0;
        this.hungry++;
        this.happiness++;
        if (this.tiredness > 0)
            this.tiredness--;
    }

    public void play()
    {
        this.hungry += 2;
        this.sleepy++;
        this.happiness += 2;
        this.tiredness ++;
    }

    public int CompareTo(Mascot other)
    {
        return this.name.CompareTo(other.name);
    }
}