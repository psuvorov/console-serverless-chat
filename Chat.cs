using System;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using ConsoleServerlessChat.Domain;
using ConsoleServerlessChat.Domain.Request;
using ConsoleServerlessChat.Net;

namespace ConsoleServerlessChat
{
    internal static partial class Chat
    {
        public static void SendMessage(BaseMessage message)
        {
            ProcessMessageBeforeSend(message);

            // Send the message to all reachable peers
            foreach (var peerIp in PeerAddresses)
            {
                NetHelper.SendMessage(message, peerIp, PortNumber,
                    // TODO: Replace with log file write
                    successfulSendMsg => Console.WriteLine(successfulSendMsg), 
                    errorMsg => Console.WriteLine(errorMsg));
            }
        }

        public static void OnReceiveMessage(string senderHost, BaseMessage message)
        {
            if (ReceivedMessageIds.Contains(message.Id))
                return;

            RetranslateMessageToPeers(message);
            SendStatus(senderHost, message);
            ProcessReceivedMessage(message);
        }


        private static void RetranslateMessageToPeers(BaseMessage message)
        {
            foreach (var peerIp in PeerAddresses)
            {

                NetHelper.SendMessage(message, peerIp, PortNumber,
                    // TODO: Replace with log file write
                    successfulSendMsg => Console.WriteLine($"@@@ Message '{message}' has been retranslated to {peerIp}: {successfulSendMsg}"),
                    errorMsg => Console.WriteLine($"@@@ Error sending message '{message}' to {peerIp}: {errorMsg}"));
            }
        }

        private static void ProcessMessageBeforeSend(BaseMessage message)
        {
            ReceivedMessageIds.Add(message.Id);
        }

        private static void ProcessReceivedMessage(BaseMessage message)
        {
            ReceivedMessageIds.Add(message.Id);

            Console.WriteLine($"### Incoming message :: '{message}'");
        }

        private static void SendStatus(string senderHost, BaseMessage message)
        {
            // TODO: Send service message to the sender that the message has beed delivered. In TCP protocol it's unnecesarily thought. 
        }

    }
}