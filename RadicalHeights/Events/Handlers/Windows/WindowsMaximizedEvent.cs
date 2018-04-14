namespace RadicalHeights.Events.Handlers.Windows
{
    public class WindowsMaximizedEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsMaximizedEvent"/> class.
        /// </summary>
        public WindowsMaximizedEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights has been maximized.");
        }
    }
}
