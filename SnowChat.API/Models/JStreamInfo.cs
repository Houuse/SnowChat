using System;
using System.Collections.Generic;

namespace SnowChat.API.Models
{
    public class Config
    {
        public string name { get; set; }
        public List<string> subjects { get; set; }
        public string retention { get; set; }
        public int max_consumers { get; set; }
        public int max_msgs { get; set; }
        public int max_bytes { get; set; }
        public string discard { get; set; }
        public int max_msg_size { get; set; }
        public string storage { get; set; }
        public int num_replicas { get; set; }
        public long duplicate_window { get; set; }
    }

    public class State
    {
        public int messages { get; set; }
        public int bytes { get; set; }
        public int first_seq { get; set; }
        public DateTime first_ts { get; set; }
        public int last_seq { get; set; }
        public DateTime last_ts { get; set; }
        public int consumer_count { get; set; }
    }

    public class JStreamInfo
    {
        public string type { get; set; }
        public Config config { get; set; }
        public DateTime created { get; set; }
        public State state { get; set; }
    }
}