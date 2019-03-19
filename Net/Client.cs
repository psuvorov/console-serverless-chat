using System;
using ConsoleServerlessChat.Domain;
using ConsoleServerlessChat.Domain.Request;

namespace ConsoleServerlessChat.Net
{
    internal static class Client
    {
        public static void Start(Action<BaseMessage> sendMessage)
        {
            bool keepProcessing = true;

            // TODO: Process some member exit
            while (keepProcessing)
            {
                Console.WriteLine("--- Waiting for user input...");
                string userInput = Console.ReadLine(); 

                var message = new ChatTextMessage()
                {
                    Id = Guid.NewGuid(),
                    MessageText = userInput,
                    Date = DateTime.Now
                };

                sendMessage(message);
            }
        }
    }
}