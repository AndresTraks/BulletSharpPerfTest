using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using BenchmarkCppCli;

namespace BulletSharpPerfTest
{
    class Program
    {
        private static void Main()
        {
            if (!ProcessorSupport.SupportsRdtscp())
            {
                Console.WriteLine("RDTSCP instruction not supported!");
                return;
            }

            using (var benchmark = new BenchmarkTimer())
            {
                for (int j = 0; j < 100; j++)
                {
                    int v = 1;
                    const int n = 1000000;
                    var results = new List<ulong>(n);
                    for (int i = 0; i < n; i++)
                    {
                        ulong r = benchmark.Get();

                        //Benchmark.Empty();
                        //v = Benchmark.Zero();
                        v = Benchmark.Identity(3);

                        //empty();
                        //v = zero();
                        //v = identity(3);

                        ulong r2 = benchmark.Get();
                        ulong d = r2 - r;
                        results.Add(d);
                    }
                    Console.WriteLine(v);

                    ulong dd = results.Aggregate((a, s) => a + s);
                    Console.WriteLine("Avg: {0}", dd / n);

                    int maxCount = 0;
                    ulong maxValue = 0;
                    var freq = results.GroupBy(value => value);
                    foreach (var grouping in freq)
                    {
                        if (grouping.Count() > maxCount)
                        {
                            maxCount = grouping.Count();
                            maxValue = grouping.Key;
                        }
                    }
                    Console.WriteLine("Mode: {0}", maxValue);
                }
            }
        }

        [DllImport("benchmark.dll")]
        private static extern void empty();
        [DllImport("benchmark.dll")]
        private static extern int zero();
        [DllImport("benchmark.dll")]
        private static extern int identity(int n);
    }
}