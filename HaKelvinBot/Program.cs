﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

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

        private readonly User MainUserAustin = new User() { Username = "Zyno" };
        #endregion

        #region Fields

        #endregion

        #region Properties
        public DiscordSocketClient Client { get; private set; }

        public Tasking TaskHandler { get; private set; }

        #endregion

        #region Methods
        public static void Main(string[] args) =>
            new Program().MainAsyncTask().GetAwaiter().GetResult();

        public async Task MainAsyncTask()
        {
            //Load configuration file
            Configuration.Load("config.yaml");

            //Initialize logger
            Logger.LoggerVerbosity = Verbosity.High;
            Logger.CreateLogFile();
            Logger.Info("Connecting!");

            //Initialize Tasking mechanism
            TaskHandler = new Tasking();

            //AddTask("EchoTask_KelvinEcho", 30);
            //AddTask("EchoTask_ShwangEcho", 30);
            //AddTask("EchoTask_AustinEcho", 30);

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

            TaskHandler.AddTask(KelvinEcho);
            TaskHandler.AddTask(ShwangEcho);

            // Handle event listeners
            Client = new DiscordSocketClient();
            Client.Log += Log;
            Client.MessageReceived += Message_Received;

            //  You can assign your bot token to a string, and pass that in to connect.
            //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            var token = Configuration.Get("key");

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
