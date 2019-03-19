using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using ConsoleServerlessChat.Domain;
using ConsoleServerlessChat.Domain.Enums;
using ConsoleServerlessChat.Domain.Request;

namespace ConsoleServerlessChat.Net
{
    public static class NetHelper
    {
        public static void SendMessage(BaseMessage message, string hostName, int port, 
            Action<string> onSuccessfulMessageSendCallback, 
            Action<string> onErrorMessageSendCallback)
        {
            try
            {
                message.SenderIp = hostName;

                using (TcpClient client = new TcpClient(hostName, port))
                using (NetworkStream networkStream = client.GetStream())
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(networkStream, message);

                    switch (message)
                    {
                        case ChatTextMessage chatTextMessage:
                            onSuccessfulMessageSendCallback($"--- Text message '{chatTextMessage.MessageText}' has been sent to {hostName}");
                            break;
                        case ServiceMessage serviceMessage:
                            onSuccessfulMessageSendCallback($"--- Service message '{Enum.GetName(typeof(ServiceMessageType), serviceMessage.ServiceMessageType)}' has been sent to {hostName}");
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                onErrorMessageSendCallback(e.Message);
            }
        }
    }
}