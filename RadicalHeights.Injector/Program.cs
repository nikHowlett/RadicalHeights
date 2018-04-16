namespace RadicalHeights.Injector
{
    using System;
    using System.IO;

    using global::RadicalHeights.Native;

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
            Console.WriteLine("[*] Initialized, injecting wallhack / esp.");

            FileInfo DllFile = new FileInfo("Library/System.Radical.dll");

            if (DllFile.Exists)
            {
                bool Injected = Dll.TryInject(RadicalHeights.AttachedProcess, DllFile.FullName);

                if (Injected)
                {
                    Console.WriteLine("[*] ------------ HACK INJECTED ------------");
                    Console.WriteLine("[*] Press Ctrl + Alt + L to enable ESP.");

                    RadicalEsp.Run();
                }
                else
                {
                    Console.WriteLine("[*] Error, failed to inject into RadicalHeights.exe.");
                }
            }
            else
            {
                Console.WriteLine("[*] Error, Dll not found.");
            }
        }
    }
}
