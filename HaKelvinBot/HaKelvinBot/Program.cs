using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace HaKelvinBot
{
    public class Program
    {
        private const string KEY = "NzM2MzgwNDU2OTc4ODc0Mzc4.Xxt9vg.Qz7wNxOtIORvGubqR7ZSZYh7Suo";

        private const ulong MAIN_CHANNEL_ID = 158732651010850816;

        private DiscordSocketClient client_;

        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            Console.WriteLine("Connecting!");

            client_ = new DiscordSocketClient();

            client_.Log += Log;
            client_.MessageReceived += Message_Received;
            
            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = KEY;

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await client_.LoginAsync(TokenType.Bot, token);
            await client_.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }


        private Task Message_Received(SocketMessage arg)
        {
            ulong targetChannelId = arg.Channel.Id;
            if (arg.Author.Username.Contains("Dank Memes"))
                (client_.GetChannel(targetChannelId) as IMessageChannel).SendMessageAsync("Shut up Kelvin");
            else if (arg.Author.Username.Contains("ShwangCat"))
                (client_.GetChannel(targetChannelId) as IMessageChannel).SendMessageAsync("Howdy partner");

            return Task.CompletedTask;
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }


    }
}
