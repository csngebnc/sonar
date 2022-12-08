namespace CSONGE.Messaging.Configuration
{
    public class RabbitMQConfiguration
    {
        public string HostName { get; set; }
        public ushort Port { get; set; }
        public string QueueName { get; set; }
        public string Username { get; set; } = "guest";
        public string Password { get; set; } = "guest";
        public string VirtualHost { get; set; } = "/";
    }
}
