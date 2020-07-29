using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Net;

using HaKelvinBot.Structures;

namespace HaKelvinBot
{
    partial class Program
    {
        #region Constants and Readonly
        private const string KEY = "NzM2MzgwNDU2OTc4ODc0Mzc4.Xxt9vg.Qz7wNxOtIORvGubqR7ZSZYh7Suo";

        private readonly User MainUserShwang = new User() { Username = "ShwangCat" };

        private readonly User MainUserKelvin = new User() { Username = "Dank Memes" };
        #endregion

        #region Fields
        private DiscordSocketClient client_;

        #endregion

        #region Properties
        public Dictionary<string, Tuple<long, int>> FloodPreventionInfo { get; private set; }

        public Dictionary<string, bool> AllowedResponses { get; private set; }

        #endregion

        #region Methods
        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            Console.WriteLine("Connecting!");

            FloodPreventionInfo = new Dictionary<string, Tuple<long, int>>();
            AllowedResponses = new Dictionary<string, bool>();

            AddTask("EchoTask_KelvinEcho", 30);
            AddTask("EchoTask_ShwangEcho", 5);

            // Handle event listeners
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

        private void AddTask(string taskName, int waitTime)
        {
            FloodPreventionInfo.Add(taskName, Tuple.Create(GetUnixTime() - waitTime, waitTime));
            AllowedResponses.Add(taskName, true);
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private long GetUnixTime()
        {
            long epoch = (long)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            return epoch;
        }

        #endregion
    }
}
