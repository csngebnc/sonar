using System;

namespace CSONGE.Messaging.Domain.Messages
{
    public class MessageBase
    {
        public MessageBase()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public MessageBase(Guid id, DateTime createDate)
        {
            Id = id;
            CreationDate = createDate;
        }

        public Guid Id { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
