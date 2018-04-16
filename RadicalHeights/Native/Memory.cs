namespace RadicalHeights.Native
{
    using System;
    using System.Runtime.InteropServices;

    public class Memory
    {
        /// <summary>
        /// Gets the process handle.
        /// </summary>
        public IntPtr ProcessHandle
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the process base address.
        /// </summary>
        public IntPtr ProcessBase
        {
            get;
            private set;
        }

        [DllImport("kernel32.dll")]
        public static extern bool ReadProcessMemory(int Process, int BaseAddress, byte[] Buffer, int Size, ref int NumberOfBytesRead);

        /// <summary>
        /// Initializes a new instance of the <see cref="Memory"/> class.
        /// </summary>
        /// <param name="Handle">The handle.</param>
        public Memory(IntPtr Handle)
        {
            this.ProcessHandle = Handle;
            this.ProcessBase   = IntPtr.Zero;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Memory"/> class.
        /// </summary>
        /// <param name="Handle">The handle.</param>
        /// <param name="Base">The base.</param>
        public Memory(IntPtr Handle, IntPtr Base)
        {
            this.ProcessHandle = Handle;
            this.ProcessBase   = Base;
        }

        /// <summary>
        /// Reads the int.
        /// </summary>
        public int ReadInt(IntPtr Offset)
        {
            var Buffer  = new byte[sizeof(int)];
            var Written = 0;

            if (ReadProcessMemory(this.ProcessHandle.ToInt32(), this.ProcessBase.ToInt32() + Offset.ToInt32(), Buffer, Buffer.Length, ref Written))
            {
                return BitConverter.ToInt32(Buffer, 0);
            }

            throw new Exception("Failed to read memory.");
        }

        /// <summary>
        /// Reads the int.
        /// </summary>
        public unsafe UIntPtr ReadUIntPtr(IntPtr Offset)
        {
            var Buffer  = new byte[sizeof(UIntPtr)];
            var Written = 0;

            if (ReadProcessMemory(this.ProcessHandle.ToInt32(), this.ProcessBase.ToInt32() + Offset.ToInt32(), Buffer, Buffer.Length, ref Written))
            {
                fixed (byte* _bp = Buffer)
                {
                    return (UIntPtr) _bp;
                }
            }

            throw new Exception("Failed to read memory.");
        }
    }
}
