namespace RadicalHeights
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;

    using global::RadicalHeights.Native;

    using NetCoreEx.Geometry;

    using WinApi.User32;

    public static class RadicalHeights
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="RadicalHeights"/> is initiliazed.
        /// </summary>
        public static bool Initialized
        {
            get;
            set;
        }

        /// <summary>
        /// Gets the attached <see cref="RadicalHeights"/> process.
        /// </summary>
        public static Process AttachedProcess
        {
            get
            {
                if (RadicalHeights._AttachedProcess != null)
                {
                    if (RadicalHeights._AttachedProcess.HasExited == false)
                    {
                        return RadicalHeights._AttachedProcess;
                    }

                    RadicalHeights._AttachedProcess = null;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is attached.
        /// </summary>
        public static bool IsAttached
        {
            get
            {
                return RadicalHeights.AttachedProcess != null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is detached.
        /// </summary>
        public static bool IsDetached
        {
            get
            {
                return RadicalHeights.AttachedProcess == null;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is running.
        /// </summary>
        public static bool IsRunning
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    if (RadicalHeights._AttachedProcess.HasExited == false)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is responding.
        /// </summary>
        public static bool IsResponding
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return RadicalHeights._AttachedProcess.Responding;
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is minimized.
        /// </summary>
        public static bool IsMinimized
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    var Placement = Native.Window.GetWindowPlacement(RadicalHeights._AttachedProcess.MainWindowHandle);

                    if (Placement.ShowCmd == ShowWindowCommands.SW_HIDE || Placement.ShowCmd == ShowWindowCommands.SW_MINIMIZE)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is maximized.
        /// </summary>
        public static bool IsMaximized
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    var Placement = Native.Window.GetWindowPlacement(RadicalHeights._AttachedProcess.MainWindowHandle);

                    if (Placement.ShowCmd == ShowWindowCommands.SW_MAXIMIZE || Placement.ShowCmd == ShowWindowCommands.SW_SHOWMAXIMIZED)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets a value indicating whether <see cref="RadicalHeights"/> is displayed on the screen.
        /// </summary>
        public static bool IsOnScreen
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    var Placement = Native.Window.GetWindowPlacement(RadicalHeights._AttachedProcess.MainWindowHandle);
                    var Flag      = Placement.ShowCmd;

                    if (RadicalHeights.IsMaximized)
                    {
                        return true;
                    }

                    if (RadicalHeights.IsMinimized)
                    {
                        return false;
                    }

                    if (Flag == ShowWindowCommands.SW_RESTORE || Flag == ShowWindowCommands.SW_SHOWDEFAULT || Flag == ShowWindowCommands.SW_SHOWNORMAL)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> window properties.
        /// </summary>
        public static WindowPlacement Window
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return Native.Window.GetWindowPlacement(RadicalHeights._AttachedProcess.MainWindowHandle);
                }

                return new WindowPlacement();
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> window rectangle.
        /// </summary>
        public static Rectangle WindowRec
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return Native.Window.GetWindowRectangle(RadicalHeights._AttachedProcess.MainWindowHandle);
                }

                return new Rectangle(0);
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> window handle.
        /// </summary>
        public static IntPtr WindowHandle
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return RadicalHeights._AttachedProcess.MainWindowHandle;
                }

                return IntPtr.Zero;
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> modules properties.
        /// </summary>
        public static List<ProcessModule> Modules
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return RadicalHeights._AttachedProcess.Modules.Cast<ProcessModule>().ToList();
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> main module propertie.
        /// </summary>
        public static ProcessModule MainModule
        {
            get
            {
                if (RadicalHeights.AttachedProcess != null)
                {
                    return RadicalHeights._AttachedProcess.MainModule;
                }

                return null;
            }
        }

        /// <summary>
        /// Gets the <see cref="RadicalHeights"/> process memory reader.
        /// </summary>
        public static Memory Memory
        {
            get
            {
                if (RadicalHeights.IsAttached)
                {
                    return RadicalHeights._AttachedProcessMemory;
                }

                return null;
            }
        }

        private static Process _AttachedProcess;
        private static Memory _AttachedProcessMemory;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public static void Initialize()
        {
            if (RadicalHeights.Initialized)
            {
                return;
            }

            // TODO

            RadicalHeights.Initialized = true;
        }

        /// <summary>
        /// Attaches this instance to <see cref="RadicalHeights"/>.
        /// </summary>
        public static void Attach()
        {
            var Processes = Process.GetProcessesByName("RadicalHeights");
            var Processus = (Process) null;

            if (Processes.Length == 0)
            {
                // throw new ProcessNotFoundException("Processes.Length == 0 at RadicalHeights.Attach().");
            }
            else
            {
                if (Processes.Length > 1)
                {
                    Logging.Info(typeof(RadicalHeights), "Processes.Length > 1 at RadicalHeights.Attach().");

                    foreach (var Match in Processes)
                    {
                        // Get the correct instance.
                    }
                }
                else
                {
                    Processus = Processes[0];
                }
            }

            if (Processus != null)
            {
                RadicalHeights._AttachedProcess = Processus;
            }
        }

        /// <summary>
        /// Detaches this instance to <see cref="RadicalHeights"/>.
        /// </summary>
        public static void Detach()
        {
            if (RadicalHeights._AttachedProcess == null)
            {
                Logging.Info(typeof(RadicalHeights), "_AttachedProcess == null at RadicalHeights.Detach().");
            }

            RadicalHeights._AttachedProcess = null;
        }

        /// <summary>
        /// Waits / Blocks the current thread till the game starts.
        /// </summary>
        public static async Task WaitGameStart()
        {
            Logging.Info(typeof(RadicalHeights), "Waiting for you to start the game..");

            while (RadicalHeights.IsRunning == false)
            {
                await Task.Delay(250);
            }
        }

        /// <summary>
        /// Enables the events.
        /// </summary>
        public static void EnableEvents()
        {
            Events.EventHandlers.Run().ConfigureAwait(false);
        }
    }
}
