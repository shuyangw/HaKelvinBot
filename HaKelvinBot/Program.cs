﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Discord;
using Discord.WebSocket;
using Discord.Net;

using HaKelvinBot.Structures;
using HaKelvinBot.Util;

namespace HaKelvinBot
{
    /// <summary>
    /// Main class of the project
    /// </summary>
    partial class Program
    {
        #region Constants and Readonly
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
            Configuration.Load("config.yaml");

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
            var token = Configuration.Get("key");

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
