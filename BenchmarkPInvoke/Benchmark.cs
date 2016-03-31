using System.Runtime.InteropServices;

namespace BenchmarkPInvoke
{
    public class Benchmark
    {
        public static void Empty()
        {
            empty();
        }

        public static int Zero()
        {
            return zero();
        }

        public static int Identity(int n)
        {
            return identity(n);
        }

        [DllImport("benchmark.dll")]
        private static extern void empty();
        [DllImport("benchmark.dll")]
        private static extern int zero();
        [DllImport("benchmark.dll")]
        private static extern int identity(int n);
    }
}
