using System;

namespace Vullis;

class Program
{
    static void Main(string[] args)
    {
        var resa = new Unmanaged();
        try
        {
            resa.Open();
            //resa.Close();
        }
        finally
        {
            resa.Dispose();
            resa = null;
        }

        // Om de Garbage Collector in de luren te leggen.
        for (int i = 0; i < 2; i++)
        {
            var x = new Unmanaged();
            using (x)
            {
                //x.Dispose();

            }
            x = null;
        }

        GC.Collect();
        GC.WaitForPendingFinalizers();

        using var resb = new Unmanaged();
        {
            resb.Open();
        }
        Console.ReadLine();
    }
}

