using Microsoft.AspNetCore.Mvc;

namespace Chatbot.Controllers
{
    [ApiController]
    [Route("/conversation")]
    public class ConversationController : Controller
    {
        private Configuration _configuration;
        public ConversationController(Configuration configuration) 
        {
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Index([FromBody] ChatMessage chatMessage)
        {
            var api = new OpenAI_API.OpenAIAPI(_configuration.AIKey);
            var prompt = "This chatbot is for children between 10 and 12 years old. This age group is looking for deeper conversations and are just starting out with planning and might want to talk about that. The language it should use should not be too difficult and on the level of the age group. It should listen to what the children say but it should not give advice.\r\nWhen the words \"abuse\" \"suicide\" are mentioned or words that have similar meaning, redirect them to a trusted adult.  The chatbot should not use curse words and correct the children when they use them.\r\nAt the end of each conversation the chatbot should give the emotions it detected in a json file.\r\n\r\nYou are going to receive an incoming message and message history of a previous interaction. These will be provided in json format. The incoming message can be found in the \"incomingMessage\" property and the message history will be in the \"messageHistory\" property. Messages in the messageHistory are stored as a list of strings and are alternating between the user and the chatbot messages. The messages are ordered in chronological order where the first element is the first message. The first message is from the user. I want you to create a response to the incoming message with the above mentioned information. Your response should only be the created message in plain text, nothing else. When the user says they want to stop talking, create a report of the chathistory with detected emotions in json and send it back as a response instead. The format of this response should be {\"emotion\":\"\"}. \n\n" + chatMessage.ToString();
            var result = await api.Chat.CreateChatCompletionAsync(prompt);
            chatMessage.response = result.Choices[0].Message.TextContent;
            chatMessage.messageHistory.Add(chatMessage.incomingMessage);
            chatMessage.messageHistory.Add(chatMessage.response);
            chatMessage.incomingMessage = "";
            return Ok(chatMessage);
        }
    }
}

