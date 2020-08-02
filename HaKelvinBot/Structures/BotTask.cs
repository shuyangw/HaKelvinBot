using System;
using System.Collections.Generic;
using System.Text;

namespace HaKelvinBot.Structures
{
    public enum TaskStatus : ushort
    {
        Inactive = 0,
        Active = 1
    }

    public class BotTask
    {
        public string Name { get; set; }

        public int AllowTime { get; set; }

    }
}
