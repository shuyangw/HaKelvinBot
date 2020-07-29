using Discord;
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
            return Task.CompletedTask;
        }
    }
}
