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

        public Task SendMessage(ulong targetChannelId, string message)
        {
            Debug.Assert(client_ != null);

            (client_.GetChannel(targetChannelId) as IMessageChannel).SendMessageAsync(message);

            Console.WriteLine(string.Format("Sending {0}", message));
            return Task.CompletedTask;
        }

        public Task SendMessage(string channelName, string message)
        {
            int countOfSameNameChannels = 0;
            ulong desiredChannelId = 0;

            foreach (var server in client_.Guilds)
            {
                foreach (var channel in server.TextChannels)
                {
                    if (channel.Name.ToLowerInvariant() == channelName.ToLowerInvariant())
                    {
                        ++countOfSameNameChannels;
                        desiredChannelId = channel.Id;
                    }
                }
            }
            if (countOfSameNameChannels != 1)
                return Task.CompletedTask;

            Debug.Assert(client_ != null);

            SendMessage(desiredChannelId, message);
            return Task.CompletedTask;
        }
    }
}
