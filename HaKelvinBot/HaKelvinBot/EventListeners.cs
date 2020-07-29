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
        private Task Message_Received(SocketMessage arg)
        {
            ulong targetChannelId = arg.Channel.Id;
            if (arg.Author.Username.Contains(MainUserKelvin.Username))
            {
                if (FloodValid("EchoTask_KelvinEcho"))
                    SendMessage(targetChannelId, "Shut up Kelvin");
            }
            else if (arg.Author.Username.Contains(MainUserShwang.Username))
            {
                if (FloodValid("EchoTask_ShwangEcho"))
                    SendMessage(targetChannelId, "Howdy partner");
            }
            return Task.CompletedTask;
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
