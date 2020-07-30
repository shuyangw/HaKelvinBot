using Discord;
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

        private Task Message_Received(SocketMessage arg)
        {
            if (((arg.Channel) as SocketGuildChannel).Guild.Name == ADMIN_SERVER_NAME && arg.Content[0] == '!')
            { 
                //Remove exclamation mark
                ParseAdminMessage(arg.Content.Replace("!", ""));
                goto Finish;
            }

            ulong targetChannelId = arg.Channel.Id;
            if (arg.Author.Username.Contains(MainUserKelvin.Username))
            {
                if (Allow("EchoTask_KelvinEcho"))
                    SendMessage(targetChannelId, "Shut up Kelvin");
            }
            else if (arg.Author.Username.Contains(MainUserShwang.Username))
            {
                if (Allow("EchoTask_ShwangEcho"))
                    SendMessage(targetChannelId, "Howdy partner");
            }

            Finish:
                return Task.CompletedTask;
        }

        private void ParseAdminMessage(string message)
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
    }
}
