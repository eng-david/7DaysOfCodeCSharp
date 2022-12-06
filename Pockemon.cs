public class Pockemon{
    public string? Name { get; set; }
    public string? Url { get; set; }

    public override string ToString()
    {
        return $"name: {Name}, url: {Url}\n";
    }
}