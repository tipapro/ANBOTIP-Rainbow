using Discord.WebSocket;
using System;
using System.Threading.Tasks;
using BotAnbotip.Bot.Commands;
using BotAnbotip.Bot.Clients;

namespace BotAnbotip.Bot.Handlers
{
    class MessageHandler
    {
        private CommandManager _cmdManager;

        public char Prefix { get; set; }

        public MessageHandler(ulong botId, char prefix)
        {
            this.Prefix = prefix;
            _cmdManager = new CommandManager();
        }

        public async void ProcessTheMessage(SocketMessage message)
        {
            try
            {
                await Task.Run(async () =>
                {
                    if (!(message is SocketUserMessage) || (message.Content == "") || (message.Content == null) || (message.Content.ToCharArray()[0] != Prefix)) return;
                    string[] buf = message.Content.Substring(1).Split(' ');
                    string command = buf[0];
                    string argument = "";
                    if (buf.Length > 1) argument = message.Content.Substring((Prefix + command + " ").ToCharArray().Length);
                    await _cmdManager.RunCommand(command.ToLower(), argument, message);
                });
            }
            catch (Exception ex)
            {
                new ExceptionLogger().Log(ex, "Ошибка при обработке отправленного сообщения");
            }
        }
    }
}
