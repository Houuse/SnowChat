using System;
using System.Text;
using NATS.Client;
using SnowChat.Bootstraper.Utility;

namespace SnowChat.Bootstraper
{
    public static class JStreamBootstrap
    {
        public static void makeSureTheStreamBootstraped(string sName,string subject)
        {
            return;
            // here tried to creat the stream programmatically in the nats server but kept getting weird errors, and there is no documentation about the topic!
            // Kept it for reference and discussion!
            using (var con = new ConnectionFactory().CreateConnection("localhost:4222"))
            {
                string streamConfigs = @"{
  'config': {
    'name': 'SNOWCHAT',
    'subjects': [
      'SNOWCHAT'
    ],
    'retention': 'limits',
    'max_consumers': -1,
    'max_msgs': -1,
    'max_bytes': -1,
    'max_age': 0,
    'max_msg_size': -1,
    'storage': 'file',
    'discard': 'old',
    'num_replicas': 1,
    'duplicate_window': 120000000000
  },
}}";
                streamConfigs = streamConfigs.Replace("sName", sName);
                streamConfigs = streamConfigs.Replace("streamSubject", subject);
                var res = con.Request($"$JS.API.STREAM.CREATE.SNOWCHAT", Encoding.UTF8.GetBytes(streamConfigs));
                var x = Encoding.UTF8.GetString(res.Data);
                return;
            }
        }
    }
}