using HaKelvinBot.Structures;
using HaKelvinBot.Util;

using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace HaKelvinBot
{
    public class Tasking
    {
        #region Properties
        public Dictionary<string, Tuple<long, int>> FloodPreventionInfo { get; private set; }

        public Dictionary<string, bool> AllowedResponses { get; private set; }

        public Dictionary<string, BotTask> Tasks { get; private set; }
        #endregion

        #region Methods
        public Tasking() 
        {
            //Initialize structures
            Tasks = new Dictionary<string, BotTask>();

            FloodPreventionInfo = new Dictionary<string, Tuple<long, int>>();
            AllowedResponses = new Dictionary<string, bool>();
        }

        public void AddTask(EchoTask task)
        {
            //FloodPreventionInfo.Add(task.Name, Tuple.Create(MiscUtil.GetUnixTime() - time, time));
            //AllowedResponses.Add(task.Name, true);

            Tasks.Add(task.Name, task);
        }

        public BotTask Lookup(string identifier)
        {
            if (!Tasks.ContainsKey(identifier))
                return null;

            return Tasks[identifier];
        }

        public bool ContainsTask(string identifier)
        {
            return Tasks.ContainsKey(identifier);
        }

        /// <summary>
        /// Returns whether or not a task is allowed to fire. This determination is based off its status and
        /// whether or not enough time has passed since the last time it was fired.
        /// </summary>
        /// <param name="identifier">The name of the task that we want to look up.</param>
        /// <returns>A boolean denoting whether or not that task is allowed to fire.</returns>
        public bool Allowed(string identifier)
        {
            BotTask task = this.Lookup(identifier);
            if (task == null)
                return false;

            return task.Status == TaskStatus.Active && FloodValid(identifier);
        }

        public bool FloodValid(string identifier)
        {
            if (!Tasks.ContainsKey(identifier))
                return false;

            BotTask task = Tasks[identifier];

            long prevTime = task.LastFireTime;
            long currTime = MiscUtil.GetUnixTime();
            bool overTime = (currTime - prevTime >= task.MinTime);

            if (overTime)
                task.LastFireTime = currTime;

            return overTime;
        }
        #endregion

    }
}
