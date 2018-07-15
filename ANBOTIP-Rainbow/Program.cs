using ANBOTIP_Rainbow.Bot.Clients;
using ANBOTIP_Rainbow.Bot.Data;
using Discord;
using System.Threading.Tasks;

namespace ANBOTIP_Rainbow
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
