using System;

namespace ConsoleServerlessChat.Domain.Request
{
    [Serializable]
    public class ChatTextMessage : BaseMessage
    {
        public string MessageText { get; set; }

        public override string ToString()
        {
            return MessageText;
        }
    }
}