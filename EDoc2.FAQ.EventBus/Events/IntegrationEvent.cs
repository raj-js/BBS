using System;

namespace EDoc2.FAQ.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime CreateTime { get; }
    }
}
