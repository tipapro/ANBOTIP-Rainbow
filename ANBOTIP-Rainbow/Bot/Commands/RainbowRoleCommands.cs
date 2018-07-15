using System;
using System.Threading.Tasks;
using ANBOTIP_Rainbow.Bot.Data;
using Discord;
using ANBOTIP_Rainbow.Bot.Services;
using ANBOTIP_Rainbow.Bot.Clients;

namespace ANBOTIP_Rainbow.Bot.Commands
{
    class RainbowRoleCommands : CommandsBase
    {
        public RainbowRoleCommands() : base
            (
            (TransformMessageToChangeState,
            new string[] { "радуга", "rainbow" })
            ){ }

        private static async Task TransformMessageToChangeState(IMessage message, string argument)
        {
            await message.DeleteAsync();
            if (BotClient.Guild.OwnerId != message.Author.Id) return;
            
            var strArray = argument.Split(' ');

            ulong roleId = 0;
            if (strArray.Length > 1)
            {
                argument = strArray[0];
                roleId = ulong.Parse(strArray[1]);
            }

            bool changedState = false;
            switch (argument)
            {
                case "вкл":
                case "+":
                case "on": changedState = true; break;
                case "выкл":
                case "стоп":
                case "-":
                case "stop":
                case "off": changedState = false; break;
                default: throw new ArgumentException("Неопознанный аргумент", "changedState");
            }
            await CommandManager.RainbowRole.ChangeStateAsync(changedState, roleId);
        }

        public async Task ChangeStateAsync(bool changedState, ulong roleId = 0)
        {
            if (roleId != 0) await DataManager.RainbowRoleId.SaveAsync(roleId);

            if (changedState)
            {
                ServiceManager.RainbowRoleAutoChange.Run();
                await DataManager.RainbowRoleIsRunning.SaveAsync(true);
            }
            else
            {
                ServiceManager.RainbowRoleAutoChange.Stop();
                await DataManager.RainbowRoleIsRunning.SaveAsync(false);
            }
        }
    }
}
