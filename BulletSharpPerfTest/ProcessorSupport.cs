using System;
using System.Runtime.InteropServices;
using System.Security;

namespace BulletSharpPerfTest
{
    class ProcessorSupport
    {
        public enum InfoType : uint
        {
            HighestFunction = 0,
            VersionInformation = 1,
            HighestExtendedFunction = 0x80000000,
            ExtendedFeatureInformation = 0x80000001
        }

        public static bool SupportsRdtsc()
        {
            var cpuInfo = new uint[4];
            CpuID(InfoType.VersionInformation, cpuInfo);
            return (cpuInfo[3] & 0x10) != 0;
        }

        public static bool SupportsRdtscp()
        {
            var cpuInfo = new uint[4];
            CpuID(InfoType.ExtendedFeatureInformation, cpuInfo);
            return (cpuInfo[3] & 0x10) != 0x8000000;
        }

        [SuppressUnmanagedCodeSecurity]
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void CpuIDNative(InfoType infoType, uint[] cpuInfo);

        public static void CpuID(InfoType infoType, uint[] cpuInfo)
        {
            Bytecode bytecode;
            if (Environment.Is64BitProcess)
            {
                bytecode = new Bytecode(new byte[]
                {
                    0x53, // push rbx
                    0x57, // push rdi
                    0x48, 0x89, 0xD7, // mov rdi, rdx

                    0x89, 0xC8, // mov eax, ecx
                    0x0F, 0xA2, // cpuid

                    0x89, 0x07, // mov [rdi], eax
                    0x89, 0x5F, 0x04, // mov [rdi+4], ebx
                    0x89, 0x4F, 0x08, // mov [rdi+8], ecx
                    0x89, 0x57, 0x0C, // mov [rdi+C], edx

                    0x5F, // pop rdi
                    0x5B, // pop rbx
                    0xC3 // ret
                }, typeof(CpuIDNative));
            }
            else
            {
                bytecode = new Bytecode(new byte[]
                {
                    0x53, // push ebx
                    0x57, // push edi

                    0x8B, 0x44, 0x24, 0x0C, // mov eax, [esp+12]
                    0x0F, 0xA2, // cpuid

                    0x8B, 0x7C, 0x24, 0x10, // mov edi, [esp+16]
                    0x89, 0x07, // mov [edi], eax
                    0x89, 0x5F, 0x04, // mov [edi+4], ebx
                    0x89, 0x4F, 0x08, // mov [edi+8], ecx
                    0x89, 0x57, 0X0C, // mov [edi+C], edx

                    0x5F, // pop edi
                    0x5B, // pop ebx
                    0xC3 // ret
                }, typeof(CpuIDNative));
            }
            (bytecode.Delegate as CpuIDNative).Invoke(infoType, cpuInfo);
            bytecode.Dispose();
        }
    }
}
