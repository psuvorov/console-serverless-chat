using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using ConsoleServerlessChat.Net;

namespace ConsoleServerlessChat
{
    internal static partial class Chat
    {
        internal static readonly HashSet<string> PeerAddresses = new HashSet<string>();

        // Keeps Ids of received messages
        internal static readonly HashSet<Guid> ReceivedMessageIds = new HashSet<Guid>();

        // TODO: Specify this as a parameter
        internal const int PortNumber = 8081;

        private static string _ipAddress;

        public static void Main(string[] args)
        {
            _ipAddress = args[0];

            InitPeerAddresses();

            Console.WriteLine($"Hello, your address is {_ipAddress}:{PortNumber}");

            Task clientTask = Task.Factory.StartNew(() => Client.Start(SendMessage));
            Task serverTask = Task.Factory.StartNew(() => Server.Start(OnReceiveMessage));

            Task.WaitAll(serverTask, clientTask);
        }

        private static void InitPeerAddresses()
        {
            // chat_config.xml file needs to be placed near exe file
            var chatConfigPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "chat_config.xml");
            var chatConfigDoc = XDocument.Load(chatConfigPath);
            var peersElement = chatConfigDoc.Element("peers");

            if (peersElement == null)
                throw new InvalidOperationException("Wrong formed chat config xml file!");

            foreach (var peerElement in peersElement.Elements("peer"))
            {
                if (_ipAddress == peerElement.Value)
                    continue;

                PeerAddresses.Add(peerElement.Value);
            }
        }
    }
}