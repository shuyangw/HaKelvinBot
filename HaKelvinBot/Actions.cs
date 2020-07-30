using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HaKelvinBot
{
    partial class Program
    {
        private ulong AdminChannelId = 0;

        public void AdminSendHelp()
        {
            string toSend = "AVAILABLE COMMANDS:\n" +
                            "stoptask - Stops a task that is registered with the bot";

            SendMessageAdmin(toSend);
        }

        public Task SendMessageAdmin(string message)
        {
            if (AdminChannelId == 0)
            {
                foreach (var server in client_.Guilds)
                {
                    if (server.Name == ADMIN_SERVER_NAME)
                    {
                        foreach (var channel in server.TextChannels)
                        {
                            if (channel.Name == "general")
                                AdminChannelId = channel.Id;
                        }
                    }
                }
            }
            SendMessage(AdminChannelId, message);

            return Task.CompletedTask;
        }

        public Task SendMessage(ulong targetChannelId, string message)
        {
            Debug.Assert(client_ != null);

            (client_.GetChannel(targetChannelId) as IMessageChannel).SendMessageAsync(message);

            Console.WriteLine(string.Format("Sending {0}", message));
            return Task.CompletedTask;
        }

        public Task SendMessage(string channelName, string message)
        {
            Debug.Assert(client_ != null);

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

            SendMessage(desiredChannelId, message);
            return Task.CompletedTask;
        }
    }
}
