namespace RadicalHeights.Events.Handlers
{
    using GameDef;

    public class RadicalAttachedEvent : Event
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RadicalAttachedEvent"/> class.
        /// </summary>
        public RadicalAttachedEvent()
        {
            Logging.Info(this.GetType().BaseType, "Radical Heights has been attached.");
        }
    }
}
