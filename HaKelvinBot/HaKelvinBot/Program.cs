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

        private Dictionary<string, Tuple<long, int>> floodPreventionInfo_;
        #endregion

        #region Properties
        public Dictionary<string, Tuple<long, int>> FloodPreventionInfo
        {
            get
            {
                return floodPreventionInfo_; 
            }
            set { floodPreventionInfo_ = value; }
        }

        #endregion

        #region Methods
        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            Console.WriteLine("Connecting!");

            FloodPreventionInfo = new Dictionary<string, Tuple<long, int>>();
            FloodPreventionInfo.Add("EchoTask1", Tuple.Create(GetUnixTime(), 30));
            FloodPreventionInfo.Add("EchoTask2", Tuple.Create(GetUnixTime(), 30));

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

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private long GetUnixTime()
        {
            DateTimeOffset dto = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
            return dto.ToUnixTimeSeconds();
        }

        #endregion
    }
}
