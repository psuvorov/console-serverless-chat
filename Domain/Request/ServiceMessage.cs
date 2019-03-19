using System;
using ConsoleServerlessChat.Domain.Enums;

namespace ConsoleServerlessChat.Domain.Request
{
    [Serializable]
    public class ServiceMessage : BaseMessage
    {
        public ServiceMessageType ServiceMessageType { get; set; }

        public override string ToString()
        {
            return Enum.GetName(typeof(ServiceMessageType), ServiceMessageType) ?? throw new InvalidOperationException();
        }
    }
}