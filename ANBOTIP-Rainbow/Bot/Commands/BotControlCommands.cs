using Discord;
using System;
using System.Threading.Tasks;
using BotAnbotip.Bot.Data;
using BotAnbotip.Bot.Clients;

namespace BotAnbotip.Bot.Commands
{
    class BotControlCommands : CommandsBase
    {
        public BotControlCommands() : base
            (
            (TransformMessageToStopAsync,
            new string[] { "стоп", "stop"}),
            (TransformMessageToClearDataAsync,
            new string[] { "удалиданные", "сотриданные", "cleardata", "formatdata" })
            ){ }
        private static async Task TransformMessageToStopAsync(IMessage message, string argument)
        {
            await message.DeleteAsync();
            if (BotClient.Guild.OwnerId != message.Author.Id) return;
            await CommandManager.BotControl.StopAsync();
        }

        private static async Task TransformMessageToClearDataAsync(IMessage message, string argument)
        {
            await message.DeleteAsync();
            if (BotClient.Guild.OwnerId != message.Author.Id) return;
            await CommandManager.BotControl.ClearDataAsync();
        }

        public async Task StopAsync()
        {
            await DataManager.SaveAllDataAsync();
            await BotClient.Client.StopAsync();
            Environment.Exit(0);
        }

        public async Task ClearDataAsync()
        {
            DataManager.InitializeAllVariables();
            await DataManager.SaveAllDataAsync();
        }
    }
}
