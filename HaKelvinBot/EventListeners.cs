using Discord;
using Discord.WebSocket;
using HaKelvinBot.Structures;
using NLog;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;

namespace HaKelvinBot
{
    partial class Program
    {
        private const string ADMIN_SERVER_NAME = "BotTesting";

        /// <summary>
        /// Detects the event where a message is sent in any channel that is visible to the bot.
        /// </summary>
        /// <param name="arg">The message that was sent.</param>
        /// <returns>An indication that the async task is complete.</returns>
        private Task Message_Received(SocketMessage arg)
        {
            //If the detected message was a command
            if (arg.Content[0] == '!')  
            {
                //If the detected command was sent in the admin server
                if (((arg.Channel) as SocketGuildChannel).Guild.Name == ADMIN_SERVER_NAME)
                    ParseAdminServerMessage(arg.Content.Replace("!", ""));
                else
                {

                }

                goto Finish;
            }
            ParseMessage(arg);

        Finish:
            return Task.CompletedTask;
        }

        /// <summary>
        /// Listens for a successful connection of the client to the servers.
        /// </summary>
        /// <returns>An indication that the async task is complete.</returns>
        private Task Client_Connected()
        {
            Connected = true;
            //LoadPersistentTasks();
            return Task.CompletedTask;
        }

        #region Parsing messages
        public void ParseMessage(SocketMessage arg)
        {
            ulong targetChannelId = arg.Channel.Id;
            string sourceUsername = arg.Author.Username;

            //Determine if there is an EchoTask by the username
            if (TaskHandler.ContainsTask(sourceUsername) && TaskHandler.Lookup(sourceUsername).Status == BotTaskStatus.Active)
            {
                if (TaskHandler.Allowed(sourceUsername))
                    SendMessage(targetChannelId, (TaskHandler.Lookup(sourceUsername) as EchoTask).Response);
            }

            if (arg.Content == "_k")
            {
                SendMessage(targetChannelId, KELVIN_EMOJI_STRING);
            }
        }

        /// <summary>
        /// Parses commands that are not sent in the admin server.
        /// </summary>
        /// <param name="message">The message that is to be parsed.</param>
        private void ParseCommandMessage(string message)
        {
            switch (message)
            {
                case "help":
                    break;
                default:
                    break;
            }
        }

        private void ParseAdminServerMessage(string message)
        {
            switch (message)
            {
                case "help":
                    AdminSendHelp();
                    break;
                default:
                    break;
            }
        }
        #endregion
    }
}
