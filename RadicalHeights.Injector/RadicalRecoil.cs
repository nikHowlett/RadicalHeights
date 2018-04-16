namespace RadicalHeights.Injector
{
    using System.Threading.Tasks;

    public static class RadicalRecoil
    {
        /// <summary>
        /// Gets a value indicating whether this <see cref="RadicalRecoil"/> is initialized.
        /// </summary>
        public static bool Initialized
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="RadicalRecoil"/> is working.
        /// </summary>
        public static bool Working
        {
            get;
            private set;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public static void Run()
        {
            if (RadicalRecoil.Initialized)
            {
                return;
            }

            RadicalRecoil.Initialized = true;
        }

        /// <summary>
        /// Finds matches.
        /// </summary>
        private static async Task Work()
        {
            RadicalRecoil.Working = true;

            while (RadicalRecoil.Working)
            {
                await Task.Run(() => RadicalRecoil.OnRecoil());
            }
        }

        /// <summary>
        /// Called when recoil detected.
        /// </summary>
        private static void OnRecoil()
        {
            /* 
            var a0 = ReadMemory<uintptr_t>(GameHandle, (uintptr_t) GetModuleHandle(0) + 0x3D3A9F8);
            var a1 = ReadMemory<uintptr_t>(GameHandle, a0 + 0x128);
            var a2 = ReadMemory<uintptr_t>(GameHandle, a1 + 0x38);
            var a3 = ReadMemory<uintptr_t>(GameHandle, a2 + 0x0);
            var a4 = ReadMemory<uintptr_t>(GameHandle, a3 + 0x30);
            var a5 = ReadMemory<uintptr_t>(GameHandle, a4 + 0x3A0);
            var a6 = ReadMemory<uintptr_t>(GameHandle, a5 + 0xC60);

            WriteMemory(GameHandle, a6 + 0xB9C, sizeof(int), 0); // remove recoil 
            WriteMemory(GameHandle, a6 + 0xBA0, sizeof(int), 0); // remove recoil 

            //WriteMemory(GameHandle, a6 + 0x788, sizeof(BYTE), 0); // Value 0 = single shoot  
            //WriteMemory(GameHandle, a6 + 0x788, sizeof(BYTE), 1); // Value 1 = Semi Auto
            WriteMemory(GameHandle, a6 + 0x788, sizeof(BYTE), 2); // Value 2 = Auto
            */
        }
    }
}
