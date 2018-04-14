namespace RadicalHeights.Injector
{
    using System;
    using System.IO;
    using System.Threading;
    using System.Threading.Tasks;

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

            if (RadicalHeights.IsAttached != true)
            {
                Program.WaitGameStart().Wait();
            }

            RadicalHeights.Attach();

            if (RadicalHeights.IsAttached)
            {
                Console.WriteLine("[*] Waiting for the game to initialize..");

                Thread.Sleep(1000);

                Console.WriteLine("[*] Initialized, injecting wallhack / esp.");

                FileInfo DllFile = new FileInfo("Library/System.Radical.dll");

                if (DllFile.Exists)
                {
                    bool Injected = Dll.TryInject(RadicalHeights.AttachedProcess, DllFile.FullName);

                    if (Injected)
                    {
                        Console.WriteLine("[*] ------------ HACK INJECTED ------------");
                        Console.WriteLine("[*] Press Ctrl + Alt + L to enable ESP.");
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
            else
            {
                Console.WriteLine("[*] Error, tried to attach to the process and failed 2 times.");
                Console.WriteLine("[*] Error code 0x01.");
            }

            Console.ReadKey(false);
        }

        /// <summary>
        /// Waits / Blocks the current thread till the game starts.
        /// </summary>
        public static async Task WaitGameStart()
        {
            Console.WriteLine("[*] Waiting for you to start the game..");

            while (RadicalHeights.IsRunning == false)
            {
                await Task.Delay(250);
            }
        }
    }
}
