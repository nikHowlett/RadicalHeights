﻿namespace RadicalHeights.Events.Handlers.Windows
{
    public class WindowsNotOnScreenEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="WindowsNotOnScreenEvent"/> class.
        /// </summary>
        public WindowsNotOnScreenEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights is not on the screen anymore.");
        }
    }
}
