using ConsoleServerlessChat.Domain.Enums;

namespace ConsoleServerlessChat.Domain.Response
{
    public class ResultMessage
    {
        public OperationResult OperationResult { get; set; }

        public string Error { get; set; }
    }
}