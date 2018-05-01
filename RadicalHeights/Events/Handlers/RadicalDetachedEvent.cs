namespace RadicalHeights.Events.Handlers
{
    using GameDef;

    public class RadicalDetachedEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadicalDetachedEvent"/> class.
        /// </summary>
        public RadicalDetachedEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights has been detached.");
        }
    }
}
