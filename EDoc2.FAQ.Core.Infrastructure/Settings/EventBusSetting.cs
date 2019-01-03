namespace EDoc2.FAQ.Core.Infrastructure.Settings
{
    public class EventBusSetting
    {
        public string SubscriptionClientName { get; set; }
        public string HostName { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int RetryCount { get; set; }
    }
}
