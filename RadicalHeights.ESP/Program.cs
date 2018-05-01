namespace RadicalHeights.ESP
{
    using System;

    using GameDef;

    public class Program
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        public static void Main()
        {
            RadicalHeights.Initialize();
            RadicalHeights.Attach();
            RadicalHeights.EnableEvents();

            Console.WriteLine("[*] Loading ESP.");
            Console.WriteLine("[*] Waiting for you to start the game..");

            RadicalHeights.WaitGameStart().Wait();

            if (RadicalHeights.IsAttached)
            {
                Program.Inject();
            }
            else
            {
                Console.WriteLine("[*] Error, tried to attach to the process and failed 2 times.");
                Console.WriteLine("[*] Error code 0x01.");
            }

            Console.ReadKey(false);
        }

        /// <summary>
        /// Injects the cheat.
        /// </summary>
        public static void Inject()
        {
            Console.WriteLine("[*] Initialized, injecting..");
            Console.WriteLine("[*] Please wait...");

            // ---------------------

            Console.WriteLine("[*] Hooked Handle : " + RadicalHeights.HookedHandle);

            // ---------------------

            // Thread.Sleep(2500);

            var Memory = RadicalHeights.Memory;

            if (Memory != null)
            {
                Logging.Warning(typeof(RadicalHeights), "Selecting " + RadicalHeights.MainModule.ModuleName + "(" + RadicalHeights.MainModule.FileName + ")..");
                Logging.Warning(typeof(RadicalHeights), "BaseAddress : " + RadicalHeights.MainModule.BaseAddress.ToInt64().ToString("X4") + ", Size : " + RadicalHeights.MainModule.ModuleMemorySize + ".");

                if (Memory.Sigscan.SelectModule(RadicalHeights.MainModule.BaseAddress, RadicalHeights.MainModule.ModuleMemorySize))
                {
                    ulong GNamesAddr   = Memory.Sigscan.FindPattern("48 8B 35 ?? ?? ?? ?? 41 0F B7 C4 4D 8D A5 ?? ?? ?? ?? 49");
                    ulong GObjectsAddr = Memory.Sigscan.FindPattern("48 8D 0D ?? ?? ?? ?? E8 ?? ?? ?? ?? 89 43 04 48 83 C4 20 5B C3");

                    if (GNamesAddr != 0 && GObjectsAddr != 0)
                    {
                        Logging.Warning(typeof(RadicalHeights), "GNamesAddr   = " + (GNamesAddr   - (ulong) RadicalHeights.MainModule.BaseAddress.ToInt64()).ToString("X4") + " !");
                        Logging.Warning(typeof(RadicalHeights), "GObjectsAddr = " + (GObjectsAddr - (ulong) RadicalHeights.MainModule.BaseAddress.ToInt64()).ToString("X4") + " !");

                        Logging.Info(typeof(RadicalHeights), "Substracted : " + (GObjectsAddr - GNamesAddr) + " !");
                        Logging.Info(typeof(RadicalHeights), "Substracted : " + (0x03C20910   - 0x03C18058) + " !");
                    }
                    else
                    {
                        Logging.Warning(typeof(RadicalHeights), "Address == 0 at Inject().");
                    }
                }
                else
                {
                    Logging.Warning(typeof(RadicalHeights), "SelectModule != true at Inject().");
                }
            }
            else
            {
                Logging.Warning(typeof(RadicalHeights), "Memory == null at Inject().");
            }
        }
    }
}
