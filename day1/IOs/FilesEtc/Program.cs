namespace FilesEtc;
class Program
{
    static void Main(string[] args)
    {
        //StaticGroup();
        InstanceGroup();
    }

    private static void InstanceGroup()
    {
        var file = new FileInfo("bla.txt");
        file.Create().Close();
        System.Console.WriteLine(file.Exists);
        System.Console.WriteLine(file.Attributes);
        Console.ReadLine();
        file.Delete();
    }

    private static void StaticGroup()
    {
        File.Create("bla.txt").Close();
        System.Console.WriteLine(File.Exists("bla.txt"));
        System.Console.WriteLine(File.GetAttributes("bla.txt"));
        Console.ReadLine();
        File.Delete("bla.txt");
    }

}
