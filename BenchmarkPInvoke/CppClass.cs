using System;
using System.Runtime.InteropServices;

namespace BenchmarkPInvoke
{
    public class CppClass : IDisposable
    {
        private IntPtr _native;

        public CppClass()
        {
            _native = CppClass_new();
        }

        public void Empty()
        {
            CppClass_empty(_native);
        }

        public int Zero()
        {
            return CppClass_zero(_native);
        }

        public int Identity(int n)
        {
            return CppClass_identity(_native, n);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            CppClass_delete(_native);
            _native = IntPtr.Zero;
        }

        ~CppClass()
        {
            Dispose(false);
        }

        [DllImport("benchmark.dll")]
        private static extern IntPtr CppClass_new();
        [DllImport("benchmark.dll")]
        private static extern void CppClass_empty(IntPtr obj);
        [DllImport("benchmark.dll")]
        private static extern int CppClass_zero(IntPtr obj);
        [DllImport("benchmark.dll")]
        private static extern int CppClass_identity(IntPtr obj, int n);
        [DllImport("benchmark.dll")]
        private static extern void CppClass_delete(IntPtr obj);
    }
}
