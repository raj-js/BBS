using System;

namespace EDoc2.FAQ.Notification
{
    public class Notify
    {
        public Notify()
        {
            Id = Guid.NewGuid();
            CreateTime = DateTime.Now;
        }

        public Guid Id { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
