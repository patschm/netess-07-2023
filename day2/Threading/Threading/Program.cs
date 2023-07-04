using System.Collections.Concurrent;
using System.Security.Cryptography;

namespace Threading
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Synchroon();
            //AsynchronousAPM();
            //AsynchronousTask();
            //AsynchronousHip();
            //AsynchronousErrors();
            //YieldIntermezzo();
            //var nikko = new CancellationTokenSource();
            //OokHeelGaaf(nikko.Token);
            //Console.WriteLine("Press enter to explode");
            //Console.ReadLine();
            //nikko.Cancel();

            AsyncCollections();
            Console.WriteLine("Einde Programma");
            Console.ReadLine();
        }

        private static void AsyncCollections()
        {
            //int[] nrs = {}
            //<int> list = new List<int>();   
            ConcurrentBag<int> list = new ConcurrentBag<int>();
            for(int x = 0; x < 10; x++) { }
            Parallel.For(0, 10, x=>list.Add(x));
            
        }

        private static void OokHeelGaaf(CancellationToken bommetje)
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (bommetje.IsCancellationRequested)
                    {
                        Console.WriteLine("Kaboooom!!!");
                        return;
                    }
                    Console.WriteLine(i);
                    Task.Delay(1000).Wait();
                }
            });
        }

        private static void YieldIntermezzo()
        {
            foreach(var i in GetNumbers())
            {
                Console.WriteLine(i);
            }
        }

        static IEnumerable<int> GetNumbers()
        {
            //var list = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8 };
            //return list;
            yield return 1;
            Console.WriteLine("Een");
            yield return 2;
            Console.WriteLine("Twee");
            yield return 3;
            Console.WriteLine("Drie");
            yield return 4;
            Console.WriteLine("Vier");
            yield return 5;
            Console.WriteLine("Vijf");
            yield return 6;
            Console.WriteLine("Zes");
            yield return 7;
        }

        private static async void AsynchronousErrors()
        {
            Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                {
                    if (i % 10 == 0)
                    {
                        throw new Exception("Ooops!");
                    }
                }
            }).ContinueWith(t =>
            {
                if (t.Exception != null)
                {
                    Console.WriteLine(t.Exception.InnerException?.Message);
                }
            });


            try
            {
                await Task.Run(() =>
                {
                    for (int i = 0; i < 1000; i++)
                    {
                        if (i % 10 == 0)
                        {
                            throw new Exception("Ooops!");
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static async void AsynchronousHip()
        {
            Task<int> t2 = Task.Run(() => LongAdd(4, 5));
            var result = await t2;
            Console.WriteLine($"Hippe {result}");
        }

        private static void AsynchronousTask()
        {
            Task<int> t2 = Task.Run(() => LongAdd(4,5));
            
            Task<int> t1 = new Task<int>(() => {
                int result = LongAdd(2, 3);
                return result;
            });

            t1.ContinueWith(prevTask => {
                int result = t1.Result;
                Console.WriteLine($"Het antwoord is {result}");
                return 42;
            })
                // Serie geschakeld
                .ContinueWith(pt =>
            {
                Console.WriteLine(pt.Status);
                Console.WriteLine($"Serie: {pt.Result}");
            });
            // Parallel geschakeld.
            t1.ContinueWith(pt =>
            {
                Console.WriteLine($"Parallel: {pt.Result}");
            });
            t1.Start();

            // result = t1.Result;
            //Console.WriteLine($"Het antwoord is {result}");
        }

        private static void AsynchronousAPM()
        {
            // Werkt niet in .NET (core)
            Func<int, int, int> fn = LongAdd;

            IAsyncResult ar = fn.BeginInvoke(3, 4, arr => {
                int result = fn.EndInvoke(arr);
                Console.WriteLine(result);
            }, null);

            
        }

        private static void Synchroon()
        {
            int result = LongAdd(2, 3);
            Console.WriteLine($"Het antwoord is {result}");
        }

        static int LongAdd(int a, int b)
        {
            Task.Delay(5000).Wait();
            return a + b;
        }
    }
}