using Discord.WebSocket;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BotAnbotip.Bot.Commands
{
    class CommandManager
    {
        public const char ArgumentPrefix = '/';

        private List<CommandsBase> _commandsCollection;

        public static BotControlCommands BotControl { get; private set; }
        public static RainbowRoleCommands RainbowRole { get; private set; }

        public CommandManager()
        {
            BotControl = new BotControlCommands();
            RainbowRole = new RainbowRoleCommands();

            _commandsCollection = new List<CommandsBase> { BotControl, RainbowRole };
        }

        public async Task RunCommand(string commandName, string argument, SocketMessage message)
        {
            foreach (var commands in _commandsCollection)
            {
                var command = commands[commandName];
                if (command != null) await command.Invoke(message, argument);
            }
        }
    }
}
