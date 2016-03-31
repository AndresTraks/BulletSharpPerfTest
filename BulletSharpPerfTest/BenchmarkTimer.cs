using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;
using BenchmarkCppCli;

namespace BulletSharpPerfTest
{
    class BenchmarkTimer : IDisposable
    {
        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ulong RctscpNative();

        private readonly Bytecode _rdtscp;
        private readonly RctscpNative _rdtscpDelegate;

        public BenchmarkTimer()
        {
            var process = Process.GetCurrentProcess();
            //process.ProcessorAffinity = new IntPtr(4);
            process.PriorityClass = ProcessPriorityClass.High;
            Thread.CurrentThread.Priority = ThreadPriority.Highest;

            if (ProcessorSupport.SupportsRdtscp())
            {
                if (Environment.Is64BitProcess)
                {
                    _rdtscp = new Bytecode(new byte[]
                    {
                        0x0F, 0x01, 0xF9, // rdtscp
                        // high-order bits are cleared
                        0x48, 0xC1, 0xE2, 0x20, // shl rdx, 32
                        0x48, 0x09, 0xD0, // or rax, rdx
                        0xC3 // ret
                    }, typeof (RctscpNative));
                }
                else
                {
                    _rdtscp = new Bytecode(new byte[]
                    {
                        0x0F, 0x01, 0xF9, // rdtscp
                        0xC3 // ret
                    }, typeof (RctscpNative));
                }
                _rdtscpDelegate = _rdtscp.Delegate as RctscpNative;

                // warm up cache
                Get();
            }
        }

        public ulong Get()
        {
            return Rdtscp.Get();
            //return _rdtscpDelegate.Invoke();
            //return rdtscp();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _rdtscp.Dispose();
            }
        }

        [DllImport("benchmark.dll")]
        private static extern ulong rdtscp();
    }
}
