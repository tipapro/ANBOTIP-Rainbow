using Discord;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ANBOTIP_Rainbow.Bot.Commands
{
    public abstract class CommandsBase
    {
        public (Func<IMessage, string, Task> CommandMethod, string[] CommandNames)[] Commands { get; }
        public static Dictionary<string, (ulong, List<ulong>)> MethodQueueDictionary;     // varName -- (lastId, listOfIds)

        protected CommandsBase(params (Func<IMessage, string, Task>, string[])[] commands)
        {
            Commands = commands;
            MethodQueueDictionary = new Dictionary<string, (ulong, List<ulong>)>();
        }

        public Func<IMessage, string, Task> this[string commandName]
        {
            get
            {
                foreach (var (CommandMethod, CommandNames) in Commands)
                    foreach (var name in CommandNames)
                        if (name == commandName) return CommandMethod;
                return null;
            }
        }
    }
}
