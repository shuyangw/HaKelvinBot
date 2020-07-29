using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
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
                SendMessage(targetChannelId, "Shut up Kelvin");
            else if (arg.Author.Username.Contains(MainUserShwang.Username))
                SendMessage(targetChannelId, "Howdy partner");

            return Task.CompletedTask;
        }
    }
}
