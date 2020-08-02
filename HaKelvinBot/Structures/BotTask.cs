using HaKelvinBot.Util;
using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet.Core.Tokens;

namespace HaKelvinBot.Structures
{
    public enum TaskStatus : ushort
    {
        Inactive = 0,
        Active = 1
    }

    public abstract class BotTask
    {
        public string Name { get; set; }

        private long minTime_;

        public long MinTime
        { 
            get { return minTime_; }
            set
            {
                minTime_ = value;
                LastFireTime = MiscUtil.GetUnixTime() - minTime_;
            }
        }

        public long LastFireTime { get; set; }

        public TaskStatus Status { get; set; }

        public BotTask()
        {
            Status = TaskStatus.Active;
        }

        public abstract void Execute();
    }
}
