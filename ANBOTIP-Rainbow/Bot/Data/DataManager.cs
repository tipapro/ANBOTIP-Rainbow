using System;
using System.Threading.Tasks;
using BotAnbotip.Bot.Clients;

namespace BotAnbotip.Bot.Data
{
    public class DataManager
    {
        public static DropboxData<bool> RainbowRoleIsRunning { get; set; }
        public static DropboxData<ulong> RainbowRoleId { get; set; }
        public static DropboxData<bool> HackerChannelIsRunning { get; set; }
        public static DropboxData<ulong> HackerChannelId { get; set; }


        public static bool[] DebugTriger = new bool[5];


        public static DropboxData<T> InitializeDropboxData<T>(DropboxData<T> obj, string fileName) => new DropboxData<T>(fileName);
        public static void InitializeAllVariables()
        {
            RainbowRoleIsRunning = InitializeDropboxData(RainbowRoleIsRunning, nameof(RainbowRoleIsRunning));
            RainbowRoleId = InitializeDropboxData(RainbowRoleId, nameof(RainbowRoleId));
            HackerChannelIsRunning = InitializeDropboxData(HackerChannelIsRunning, nameof(HackerChannelIsRunning));
            HackerChannelId = InitializeDropboxData(HackerChannelId, nameof(HackerChannelId));
        }       

        public static async Task SaveAllDataAsync()
        {
            try
            {
                await RainbowRoleIsRunning.SaveAsync();
                await HackerChannelIsRunning.SaveAsync();
                await RainbowRoleId.SaveAsync();
                await HackerChannelId.SaveAsync();
            }
            catch (Exception ex)
            {
                new ExceptionLogger().Log(ex, "Save data error");
            }
        }

        public static async Task ReadAllDataAsync()
        {
            try
            {
                InitializeAllVariables();

                await RainbowRoleIsRunning.ReadAsync();
                await HackerChannelIsRunning.ReadAsync();
                await RainbowRoleId.ReadAsync();
                await HackerChannelId.ReadAsync();
            }
            catch (Exception ex)
            {
                new ExceptionLogger().Log(ex, "Read data error");
            }
        }
    }
}
