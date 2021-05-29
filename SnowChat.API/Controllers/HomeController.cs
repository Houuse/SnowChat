using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NATS.Client;
using SnowChat.API.Models;
using SnowChat.Bootstraper.Utility;

namespace SnowChat.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {

        private readonly ILogger<HomeController> _logger;
        private readonly ConnectionFactory _connectionFactory;
        const string subject = "SNOWCHAT";
        const string clientName = "ClientName";
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _connectionFactory = new ConnectionFactory();
        }

        [HttpGet]
        public string Get()
        {
            using var con = _connectionFactory.CreateConnection("localhost:4222");
            var result = con.Request("$JS.API.STREAM.INFO.SNOWCHAT",null);
            var streamInfo = JsonSerializer.Deserialize<JStreamInfo>(Encoding.UTF8.GetString(result.Data));
            var numberOfMessages = streamInfo.state.messages;
            var msgList = new List<Message>(){Capacity = numberOfMessages};

            for (int i = 0; i < numberOfMessages; i++)
            {
                var msg = con.Request($"$JS.API.STREAM.MSG.GET.SNOWCHAT", Encoding.UTF8.GetBytes($"{{\"seq\":{i}}}"));
                var getResponse = JsonSerializer.Deserialize<StreamGetResponse>(Encoding.UTF8.GetString(msg.Data));
                if (getResponse != null)
                {
                    var decodedMessage = new Message();
                    decodedMessage.seq = getResponse.message.seq;
                    decodedMessage.hdrs = StringDecodingUtility.Base64Decode(getResponse.message.hdrs);
                    decodedMessage.data = StringDecodingUtility.Base64Decode(getResponse.message.data);
                    decodedMessage.subject = getResponse.message.subject;
                    msgList.Add(decodedMessage);
                }
            }

            return JsonSerializer.Serialize(msgList);
        }

        [HttpPost]
        public void SendMessage(string message)
        {
            using var con = _connectionFactory.CreateConnection("localhost:4222");
            {
                if (string.IsNullOrWhiteSpace(message)) return;
                Msg msg = new Msg()
                {
                    Subject = subject,
                    Data = Encoding.UTF8.GetBytes(message),
                };
                msg.Header[clientName] = "API Client";
                con.Publish(msg);
                con.Flush();
                con.Close();
            }
        }
    }
}