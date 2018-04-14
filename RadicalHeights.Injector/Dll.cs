namespace RadicalHeights.Injector
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Text;

    public static class Dll
    {
        [DllImport("kernel32.dll")]
        public static extern IntPtr OpenProcess(int DesiredAccess, bool InheritHandle, int ProcessId);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr GetModuleHandle(string ModuleName);

        [DllImport("kernel32", CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        static extern IntPtr GetProcAddress(IntPtr Module, string ProcName);

        [DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        static extern IntPtr VirtualAllocEx(IntPtr Process, IntPtr Address, uint Size, uint AllocationType, uint Protect);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool WriteProcessMemory(IntPtr Process, IntPtr BaseAddress, byte[] Buffer, uint Size, out UIntPtr NumberOfBytesWritten);

        [DllImport("kernel32.dll")]
        static extern IntPtr CreateRemoteThread(IntPtr Process, IntPtr ThreadAttributes, uint StackSize, IntPtr StartAddress, IntPtr Parameter, uint CreationFlags, IntPtr ThreadId);
        
        // -----------------------------------------

        const int PROCESS_CREATE_THREAD     = 0x0002;
        const int PROCESS_QUERY_INFORMATION = 0x0400;
        const int PROCESS_VM_OPERATION      = 0x0008;
        const int PROCESS_VM_WRITE          = 0x0020;
        const int PROCESS_VM_READ           = 0x0010;

        const uint MEM_COMMIT               = 0x00001000;
        const uint MEM_RESERVE              = 0x00002000;
        const uint PAGE_READWRITE           = 4;

        /// <summary>
        /// Tries to inject the specified dll.
        /// </summary>
        /// <param name="Path">The path.</param>
        public static bool TryInject(Process Process, string Path)
        {
            IntPtr Handle               = OpenProcess(PROCESS_CREATE_THREAD | PROCESS_QUERY_INFORMATION | PROCESS_VM_OPERATION | PROCESS_VM_WRITE | PROCESS_VM_READ, false, Process.Id);
            IntPtr LoadLibraryAddr      = GetProcAddress(GetModuleHandle("kernel32.dll"), "LoadLibraryA");
            IntPtr AllocMemoryAddr      = VirtualAllocEx(Handle, IntPtr.Zero, (uint) ((Path.Length + 1) * Marshal.SizeOf(typeof(char))), MEM_COMMIT | MEM_RESERVE, PAGE_READWRITE);
            
            bool Written                = WriteProcessMemory(Handle, AllocMemoryAddr, Encoding.Default.GetBytes(Path), (uint) ((Path.Length + 1) * Marshal.SizeOf(typeof(char))), out _);
            IntPtr RemoteThreadAddr     = CreateRemoteThread(Handle, IntPtr.Zero, 0, LoadLibraryAddr, AllocMemoryAddr, 0, IntPtr.Zero);

            return Written;
        }
    }
}
