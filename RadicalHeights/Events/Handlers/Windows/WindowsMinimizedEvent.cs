namespace RadicalHeights.Events.Handlers.Windows
{
    using GameDef;

    public class WindowsMinimizedEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMinimizedEvent"/> class.
        /// </summary>
        public WindowsMinimizedEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights has been minimized.");
        }
    }
}
