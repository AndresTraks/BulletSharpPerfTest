using System;
using System.Runtime.InteropServices;

namespace BulletSharpPerfTest
{
    class Bytecode : IDisposable
    {
        private IntPtr _codePtr;
        public Delegate Delegate { get; }

        public Bytecode(byte[] code, Type delegateType)
        {
            _codePtr = VirtualAlloc(IntPtr.Zero, (IntPtr)code.Length, AllocationType.Commit, Protect.ExecuteReadWrite);
            Marshal.Copy(code, 0, _codePtr, code.Length);

            Protect oldProtection;
            VirtualProtect(_codePtr, (IntPtr)code.Length, Protect.Execute, out oldProtection);

            Delegate = Marshal.GetDelegateForFunctionPointer(_codePtr, delegateType);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (_codePtr != IntPtr.Zero)
            {
                VirtualFree(_codePtr, IntPtr.Zero, FreeType.Release);
                _codePtr = IntPtr.Zero;
            }
        }

        ~Bytecode()
        {
            Dispose(false);
        }

        [Flags]
        private enum AllocationType : uint
        {
            Commit = 0x1000,
        }

        [Flags]
        private enum FreeType : uint
        {
            Release = 0x8000
        }

        [Flags]
        private enum Protect : uint
        {
            Execute = 0x10,
            ExecuteReadWrite = 0x40,
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr VirtualAlloc(IntPtr lpAddress, IntPtr dwSize, AllocationType flAllocationType, Protect flProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualProtect(IntPtr lpAddress, IntPtr dwSize, Protect flNewProtect, out Protect lpflOldProtect);

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool VirtualFree(IntPtr lpAddress, IntPtr dwSize, FreeType dwFreeType);
    }
}
