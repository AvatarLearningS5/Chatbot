using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace Chatbot
{
    public class Configuration
    {
        public string AIKey { get; private set; }

        public Configuration(string aiKey) 
        {
            AIKey = aiKey;
        }
        
    }
}
