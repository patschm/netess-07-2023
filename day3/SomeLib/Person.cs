namespace SomeLib;

public class Person
{
    public string? Name { get; set; }
    private int _age;
    public int Age
    {
        get { return _age; }
        set
        {
            if (value >= 0 && value < 123)
            {
                _age = value;
            }
        }
    }

    public void Introduce()
    {
        System.Console.WriteLine($"Hello, I'm {Name} and I'm {Age} years old");
    }
}
