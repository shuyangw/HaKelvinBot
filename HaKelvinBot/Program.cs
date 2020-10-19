using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

using Discord;
using Discord.WebSocket;
using Discord.Net;

using HaKelvinBot.Structures;
using HaKelvinBot.Util;
using NLog;

namespace HaKelvinBot
{
    /// <summary>
    /// Main class of the project
    /// </summary>
    partial class Program
    {
        #region Constants and Readonly
        static Logger Logger = LogManager.GetCurrentClassLogger();

#if DEBUG
        private readonly User MainUserShwang = new User() { Username = "ShwangCat" };

        private readonly User MainUserKoya = new User() { Username = "Littlemt" };

        private const string KELVIN_EMOJI_STRING = "\\:USA_0_0:\\:USA_1_0:\\:USA_2_0:\\:USA_3_0:\\:USA_4_0:\\:USA_5_0:\n" +
            ":USA_0_1::USA_1_1::USA_2_1::USA_3_1::USA_4_1::USA_5_1:\n" +
            ":USA_0_2::USA_1_2::USA_2_2::USA_3_2::USA_4_2::USA_5_2:\n" +
            ":USA_0_3::USA_1_3::USA_2_3::USA_3_3::USA_4_3::USA_5_3:\n" +
            ":USA_0_4::USA_1_4::USA_2_4::USA_3_4::USA_4_4::USA_5_4:\n" +
            ":USA_0_5::USA_1_5::USA_2_5::USA_3_5::USA_4_5::USA_5_5:\n" +
            ":USA_0_6::USA_1_6::USA_2_6::USA_3_6::USA_4_6::USA_5_6:\n" +
            ":USA_0_7::USA_1_7::USA_2_7::USA_3_7::USA_4_7::USA_5_7:";

        private readonly User MainUserKelvin = new User() { Username = "Dank Memes" };

        private readonly User MainUserAustin = new User() { Username = "Zyno" };
#endif
        #endregion

        #region Fields

        #endregion

        #region Properties
        public DiscordSocketClient Client { get; private set; }

        public Tasking TaskHandler { get; private set; }

        public bool Connected { get; set; }

        #endregion

        #region Methods
        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            //Load main configuration file
            Configuration.LoadConfig("config.yaml");

            //Initialize logger
            Logger.Info("Connecting!");

            //Initialize Tasking mechanism
            TaskHandler = new Tasking();

#if DEBUG
            EchoTask KelvinEcho = new EchoTask()
            {
                Name = MainUserKelvin.Username, 
                Response = "Shut up Kelvin",
                MinTime = 30
            };

            EchoTask ShwangEcho = new EchoTask()
            {
                Name = MainUserShwang.Username,
                Response = "Howdy partner",
                MinTime = 30
            };

            EchoTask KoyaEcho = new EchoTask()
            {
                Name = MainUserKoya.Username,
                Response = KELVIN_EMOJI_STRING,
                MinTime = 1
            };
            TaskHandler.AddTask(KelvinEcho);
            TaskHandler.AddTask(KoyaEcho);
            TaskHandler.AddTask(ShwangEcho);
#endif
            // Handle event listeners
            Client = new DiscordSocketClient();
            Client.Log += Log;
            Client.MessageReceived += Message_Received;
            Client.Connected += Client_Connected;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = Configuration.GetConfig("key");

            await Client.LoginAsync(TokenType.Bot, token);
            await Client.StartAsync();

            // Block this task until the program is closed.
            await Task.Delay(-1);
        }

        private Task Log(LogMessage msg)
        {
            Logger.Info(msg.ToString());
            return Task.CompletedTask;
        }
        #endregion
    }
}
