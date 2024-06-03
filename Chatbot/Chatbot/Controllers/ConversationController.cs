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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var api = new OpenAI_API.OpenAIAPI(_configuration.AIKey);
            var result = await api.Chat.CreateChatCompletionAsync("Hello!");
            Console.WriteLine(result);
            return Ok(result);
        }
    }
}
