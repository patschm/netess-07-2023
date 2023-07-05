//using SomeLib;

using System.Reflection;

namespace SomeClient;
class Program
{
    static void Main(string[] args)
    {
        //Person p = new Person {Name="Klara", Age=56};
        //p.Introduce();

        Assembly asm = Assembly.LoadFrom(@"D:\.NET Essentials\netess-07-2023\day3\dist\SomeLib.dll");
        Assembly exe = Assembly.GetExecutingAssembly();
        System.Console.WriteLine(exe.FullName);

        CheckAssembly(asm);
    }

    private static void CheckAssembly(Assembly asm)
    {
        System.Console.WriteLine(asm.FullName);
        foreach(Type t in asm.GetTypes())
        {
            System.Console.WriteLine(t.FullName);
        }
        System.Console.WriteLine("====================");
        Type? tp = asm.GetType("SomeLib.Person");
        
        foreach(var info in tp.GetMembers(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
        {
            System.Console.WriteLine(info.Name);
        }

        System.Console.WriteLine("===============================");
        object? p1 = Activator.CreateInstance(tp);
        PropertyInfo? pName = tp.GetProperty("Name");
        pName.SetValue(p1, "Geraldine");
        var pAge = tp.GetProperty("Age");
        pAge.SetValue(p1, 42);

        MethodInfo mIntro = tp.GetMethod("Introduce");
        mIntro.Invoke(p1, new object[]{});

        FieldInfo fAge = tp.GetField("_age", BindingFlags.Instance | BindingFlags.NonPublic);
        fAge.SetValue(p1, -42);

        mIntro.Invoke(p1, new object[]{});

        dynamic p2 = Activator.CreateInstance(tp);
        p2.Name = "Nadine";
        p2.Age = 18;
        p2.Introduce();

    }
}
