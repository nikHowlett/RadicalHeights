namespace RadicalHeights.Events.Handlers.Windows
{
    public class WindowsOnScreenEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsOnScreenEvent"/> class.
        /// </summary>
        public WindowsOnScreenEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights is now on the screen.");
        }
    }
}
