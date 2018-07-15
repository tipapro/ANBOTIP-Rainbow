using System;
using System.Threading.Tasks;
using ANBOTIP_Rainbow.Bot.Clients;

namespace ANBOTIP_Rainbow.Bot.Data
{
    public class DataManager
    {
        public static DropboxData<bool> RainbowRoleIsRunning { get; set; }
        public static DropboxData<ulong> RainbowRoleId { get; set; }

        public static DropboxData<T> InitializeDropboxData<T>(DropboxData<T> obj, string fileName) => new DropboxData<T>(fileName);
        public static void InitializeAllVariables()
        {
            RainbowRoleIsRunning = InitializeDropboxData(RainbowRoleIsRunning, nameof(RainbowRoleIsRunning));
            RainbowRoleId = InitializeDropboxData(RainbowRoleId, nameof(RainbowRoleId));
        }       

        public static async Task SaveAllDataAsync()
        {
            try
            {
                await RainbowRoleIsRunning.SaveAsync();
                await RainbowRoleId.SaveAsync();
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
                await RainbowRoleId.ReadAsync();
            }
            catch (Exception ex)
            {
                new ExceptionLogger().Log(ex, "Read data error");
            }
        }
    }
}
