using System;
using System.Text;
using NATS.Client;
using SnowChat.Bootstraper;

namespace SnowChat.Konsol
{
    class Program
    {
        static void Main(string[] args)
        {
                
            const string subject = "SNOWCHAT";
            const string clientName = "ClientName";
            JStreamBootstrap.makeSureTheStreamBootstraped(clientName,subject);

            var cf = new ConnectionFactory();
            var con = cf.CreateConnection("localhost:4222");
            con.Opts.Name = args.Length > 0 ? args[0] : $"Cactus:{Guid.NewGuid().ToString()}";
            
            var msgHandler = new EventHandler<MsgHandlerEventArgs>((sender, arguments) =>
            {
                Console.WriteLine($"AT {DateTime.Now.ToLongTimeString()}: " +
                                  $"{arguments.Message.Header[clientName]}: " +
                                  $"{Encoding.UTF8.GetString(arguments.Message.Data)}");
            });
            var sub = con.SubscribeAsync(subject,msgHandler);
            while (true)
            {
                string input = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(input)) continue;
                if (input == "quit") break;
                    Msg msg = new Msg()
                {
                    Subject = subject,
                    Data = Encoding.UTF8.GetBytes(input),
                };
                msg.Header[clientName] = con.Opts.Name;
                con.Publish(msg);
            }
            sub.Unsubscribe();
            con.Close();
        }
    }
}