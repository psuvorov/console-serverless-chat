using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using ConsoleServerlessChat.Domain;

namespace ConsoleServerlessChat.Net
{
    public static class Server
    {
        public static void Start(Action<string, BaseMessage> onReceiveMessage)
        {
            TcpListener listener = new TcpListener(IPAddress.Any, Chat.PortNumber);
            listener.Start();

            var clients = new HashSet<TcpClient>();

            try
            {
                while (true)
                {
                    using (TcpClient client = listener.AcceptTcpClient())
                    {
                        clients.Add(client);

                        using (NetworkStream networkStream = client.GetStream())
                        {
                            var formatter = new BinaryFormatter();
                            BaseMessage incomingMessage = (BaseMessage)formatter.Deserialize(networkStream);

                            var remoteHost = ((IPEndPoint) client.Client.RemoteEndPoint).Address.ToString();
                            onReceiveMessage(remoteHost, incomingMessage);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                // TODO: Replace with callback
                Console.Write(e.Message);

                listener.Stop();
                foreach (var client in clients)
                {
                    client?.Close();
                }

                Environment.Exit(0);
            }
        }
    }
}