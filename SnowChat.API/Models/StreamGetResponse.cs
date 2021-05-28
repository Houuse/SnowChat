using System;

namespace SnowChat.API.Models
{

        public class Message
        {
            public string subject { get; set; }
            public int seq { get; set; }
            public string hdrs { get; set; }
            public string data { get; set; }
            public DateTime time { get; set; }
        }

        public class StreamGetResponse
        {
            public string type { get; set; }
            public Message message { get; set; }
        }
        
}