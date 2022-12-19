public class Mascot : Pokemon, IComparable<Mascot>
{
    public RangeAttribute hungry { get; set; }
    public RangeAttribute sleepy { get; set; }
    public RangeAttribute happiness { get; set; }
    public RangeAttribute tiredness { get; set; }
    public RangeAttribute health { get; set; }

    private void Initialize()
    {
        this.hungry = new RangeAttribute(0, 10, 5);
        this.sleepy = new RangeAttribute(0, 10, 5);
        this.happiness = new RangeAttribute(0, 10, 5);
        this.tiredness = new RangeAttribute(0, 10, 5);
        this.health = new RangeAttribute(0, 10, 10);
    }
    public Mascot()
    {
        Initialize();
    }

    public Mascot(Pokemon pokemon)
    {
        Initialize();

        this.name = pokemon.name;
        this.height = pokemon.height;
        this.weight = pokemon.weight;
        this.abilities = pokemon.abilities;
    }

    public void Eat()
    {
        if (hungry.value == 0)
        {
            health.ModifyBy(-1);
        }
        else
        {
            this.hungry.ModifyBy(-1);
            this.sleepy.ModifyBy(1);
            this.happiness.ModifyBy(1);
            this.health.ModifyBy(1);
        }
    }

    public void Sleep()
    {
        this.sleepy.ModifyBy(-8);
        this.hungry.ModifyBy(1);
        this.happiness.ModifyBy(1);
        this.tiredness.ModifyBy(-1);
        this.health.ModifyBy(1);
    }

    public void Play()
    {
        if (tiredness.value == 10)
        {
            health.ModifyBy(-1);
        }
        else
        {
            this.hungry.ModifyBy(1);
            this.sleepy.ModifyBy(1);
            this.happiness.ModifyBy(1);
            this.tiredness.ModifyBy(1);
        }
    }

    public void TakeMedicine(){
        health.ModifyBy(5);
    }

    public int CompareTo(Mascot other)
    {
        return this.name.CompareTo(other.name);
    }

    public string GetHealthMessage()
    {
        if (this.health.value > 7)
            return "Healthy";
        if (this.health.value > 5)
            return "Slightly sick";
        if (this.health.value > 2)
            return "Sick";

        return "Very Sick!!!";
    }

    public string GetHungryMessage()
    {
        if (this.hungry.value > 7)
            return "Very Hungry!!!";
        if (this.hungry.value > 5)
            return "Hungry";

        return "No Hungry";
    }

    public string GetSleepyMessage()
    {
        if (this.sleepy.value > 8)
            return "Very Sleepy!!!";
        if (this.sleepy.value > 6)
            return "Sleepy";

        return "No Sleepy";
    }

    public string GetHappinessMessage()
    {
        if (this.happiness.value > 8)
            return "Very Happy!!!";
        if (this.happiness.value > 6)
            return "Happy!";
        if (this.happiness.value > 2)
            return "Sad!";

        return "Very Sad!!!";
    }

    public string GetTirednessMessage()
    {
        if (this.tiredness.value > 8)
            return "Very Tired!!!";
        if (this.tiredness.value > 6)
            return "Tired!";

        return "No Tired";
    }
}

public class RangeAttribute
{
    public int min { get; private set; }
    public int max { get; private set; }
    public int value { get; private set; }

    public RangeAttribute(int min, int max, int value)
    {
        this.min = min;
        this.max = max;
        this.value = value;
    }

    public void ModifyBy(int n)
    {
        this.value += n;
        if (this.value > max) value = max;
        else if (this.value < min) value = min;
    }

}