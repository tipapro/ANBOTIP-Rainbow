using BotAnbotip.Bot.Clients;
using BotAnbotip.Bot.Data;
using Discord;
using System.Threading.Tasks;

namespace BotAnbotip
{    public class Program
    {
        public static void Main(string[] args) => MainAsync().GetAwaiter().GetResult();
        public static async Task MainAsync()
        {
            PrivateData.Read();
            await DataManager.ReadAllDataAsync();
            await BotClient.PrepareAsync();

            bool launchResult = false;
            while (!launchResult) launchResult = await BotClient.Launch();
            await BotClient.Log(new LogMessage(LogSeverity.Info, "", "Бот запущен"));

            await Task.Delay(-1);
        }
    }
}
