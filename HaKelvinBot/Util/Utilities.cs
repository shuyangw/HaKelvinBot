using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Text;

namespace HaKelvinBot.Util
{
    public class Utilities
    {
        public static DiscordSocketClient Client { get; private set; }

        public static void SetClient(DiscordSocketClient client) { Client = client; }

        public static string GetChannelById (ulong channelId)
        {
            return null;
        }

        public static string GetChannelById (ulong channelId, ulong serverId)
        {
            return null;
        }
    }
}
