﻿using Discord;
using Discord.WebSocket;
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
        /// TODO: Cleanup, modularize further and reduce if statements
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

            ulong targetChannelId = arg.Channel.Id;
            if (arg.Author.Username.Contains(MainUserKelvin.Username))
            {
                if (Allow("EchoTask_KelvinEcho"))
                    SendMessage(targetChannelId, "Shut up Kelvin");
            }
            //else if (arg.Author.Username.Contains(MainUserShwang.Username))
            //{
            //    if (Allow("EchoTask_ShwangEcho"))
            //        SendMessage(targetChannelId, "Howdy partner");
            //}
            else if (arg.Author.Username.Contains(MainUserAustin.Username))
            {
                if (Allow("EchoTask_ShwangEcho"))
                    SendMessage(targetChannelId, "Ha furry");
            }


        Finish:
                return Task.CompletedTask;
        }

        #region Parsing messages
        /// <summary>
        /// Parses commands that are not sent in the admin server.
        /// </summary>
        /// <param name="message">The message that is to be parsed.</param>
        private void ParseAdminMessage(string message)
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

        private bool Allow(string taskName)
        {
            return FloodValid(taskName) && AllowedResponses[taskName];
        }

        private bool FloodValid(string taskName)
        {
            Debug.Assert(FloodPreventionInfo != null); 

            if (!FloodPreventionInfo.ContainsKey(taskName))
                return false;

            long prevTime = FloodPreventionInfo[taskName].Item1;
            long currTime = GetUnixTime();
            bool val = (currTime - prevTime >= FloodPreventionInfo[taskName].Item2);
            if (val)
                FloodPreventionInfo[taskName] = Tuple.Create(currTime, FloodPreventionInfo[taskName].Item2);
           
            return val;
        }
        #endregion
    }
}
