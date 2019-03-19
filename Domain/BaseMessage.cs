using System;

namespace ConsoleServerlessChat.Domain
{
    [Serializable]
    public abstract class BaseMessage
    {
        public Guid Id { get; set; }        
        public DateTime Date { get; set; }
        public string SenderIp { get; set; }

        public override string ToString()
        {
            return $"{Id}, {SenderIp}, {Date}";
        }
    }
}