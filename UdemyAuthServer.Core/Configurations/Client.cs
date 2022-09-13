using System.Collections.Generic;

namespace UdemyAuthServer.Core.Configurations
{
    public class Client
    {
        public string Id { get; set; }
        public string SecretKey { get; set; }
        // www.myapi1.com www.myapi2.com 
        public List<string> Audiences { get; set; }
    }
}
