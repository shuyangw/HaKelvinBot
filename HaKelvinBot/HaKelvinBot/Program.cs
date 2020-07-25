using System;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;

namespace HaKelvinBot
{
    public class Program
    {
        private const string KEY = "NzM2MzgwNDU2OTc4ODc0Mzc4.Xxt9vg.Qz7wNxOtIORvGubqR7ZSZYh7Suo";

        private DiscordSocketClient _client;

        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            _client = new DiscordSocketClient();

            _client.Log += Log;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = KEY;

            // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            // var token = File.ReadAllText("token.txt");
            // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            await _client.LoginAsync(TokenType.Bot, token);
            await _client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }
    }
}
