using Newtonsoft.Json;

namespace Chatbot
{
    public class ChatMessage
    {
        public ChatMessage()
        {

        }

        public string incomingMessage { get; set; }
        public List<string> messageHistory { get; set; }
        public string? response { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
