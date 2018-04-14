namespace RadicalHeights.Events
{
    using System;
    using System.Threading.Tasks;

    using global::RadicalHeights.Events.Handlers;
    using global::RadicalHeights.Events.Handlers.Windows;

    public static class EventHandlers
    {
        public static EventHandler<RadicalAttachedEvent> OnRadicalAttached;
        public static EventHandler<RadicalDetachedEvent> OnRadicalDetached;

        public static EventHandler<WindowsMaximizedEvent> OnWindowsMaximized;
        public static EventHandler<WindowsMinimizedEvent> OnWindowsMinimized;

        public static EventHandler<WindowsOnScreenEvent> OnWindowsShowed;
        public static EventHandler<WindowsNotOnScreenEvent> OnWindowsNotShowed;

        // Change Logs

        private static bool IsAttached;
        private static bool IsDetached = true;
        private static bool IsRunning;
        private static bool IsResponding;

        private static bool IsMaximized;
        private static bool IsMinimized;

        private static bool IsOnScreen;

        /// <summary>
        /// Runs this instance.
        /// </summary>
        internal static async Task Run()
        {
            while (true)
            {
                if (RadicalHeights.IsAttached)
                {
                    if (EventHandlers.IsAttached == false)
                    {
                        EventHandlers.IsAttached = true;
                        EventHandlers.IsDetached = false;

                        var Event = new RadicalAttachedEvent();

                        if (EventHandlers.OnRadicalAttached != null)
                        {
                            EventHandlers.OnRadicalAttached.Invoke(null, Event);
                        }
                    }
                }
                else if (RadicalHeights.IsDetached)
                {
                    if (EventHandlers.IsDetached == false)
                    {
                        EventHandlers.IsAttached = false;
                        EventHandlers.IsDetached = true;

                        var Event = new RadicalDetachedEvent();

                        if (EventHandlers.OnRadicalDetached != null)
                        {
                            EventHandlers.OnRadicalDetached.Invoke(null, Event);
                        }
                    }
                    else
                    {
                        RadicalHeights.Attach();
                    }
                }

                if (RadicalHeights.IsAttached)
                {
                    if (RadicalHeights.IsMaximized)
                    {
                        if (EventHandlers.IsMaximized == false)
                        {
                            EventHandlers.IsMaximized = true;
                            EventHandlers.IsMinimized = false;

                            var Event = new WindowsMaximizedEvent();

                            if (EventHandlers.OnWindowsMaximized != null)
                            {
                                EventHandlers.OnWindowsMaximized.Invoke(null, Event);
                            }
                        }
                    }

                    if (RadicalHeights.IsMinimized)
                    {
                        if (EventHandlers.IsMinimized == false)
                        {
                            EventHandlers.IsMaximized = false;
                            EventHandlers.IsMinimized = true;

                            var Event = new WindowsMinimizedEvent();

                            if (EventHandlers.OnWindowsMinimized != null)
                            {
                                EventHandlers.OnWindowsMinimized.Invoke(null, Event);
                            }
                        }
                    }

                    if (RadicalHeights.IsOnScreen)
                    {
                        if (EventHandlers.IsOnScreen == false)
                        {
                            EventHandlers.IsOnScreen = true;

                            var Event = new WindowsOnScreenEvent();

                            if (EventHandlers.OnWindowsShowed != null)
                            {
                                EventHandlers.OnWindowsShowed.Invoke(null, Event);
                            }
                        }
                    }
                    else
                    {
                        if (EventHandlers.IsOnScreen)
                        {
                            EventHandlers.IsOnScreen = false;

                            var Event = new WindowsNotOnScreenEvent();

                            if (EventHandlers.OnWindowsNotShowed != null)
                            {
                                EventHandlers.OnWindowsNotShowed.Invoke(null, Event);
                            }
                        }
                    }
                }

                await Task.Delay(250);
            }
        }
    }
}
