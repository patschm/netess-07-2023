using CalcLib;

namespace CalcClient;
class Program
{
    static void Main(string[] args)
    {
       var calc = new Calculator();
       int result = calc.Add(4,5);
       Console.WriteLine(result);
       Console.ReadLine();
    }
}
