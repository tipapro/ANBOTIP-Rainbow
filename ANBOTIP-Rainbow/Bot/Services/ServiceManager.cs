using BotAnbotip.Bot.Data;

namespace BotAnbotip.Bot.Services
{
    class ServiceManager
    {
        public static RainbowRoleAutoChangeService RainbowRoleAutoChange { get; private set; }
        public static BotHelperService BotHelper { get; private set; }

        public ServiceManager()
        {
            RainbowRoleAutoChange = new RainbowRoleAutoChangeService(
                "Ошибка автосмены цвета роли", "Автосмена цвета роли запущена", "Автосмена цвета роли остановлена");
            BotHelper = new BotHelperService(
                "Ошибка помощника бота", "Помощник бота запущен", "Помощник бота остановлен");
        }

        public void RunAll()
        {
            if (DataManager.RainbowRoleIsRunning.Value) RainbowRoleAutoChange.Run();
            BotHelper.Run();
        }

        public void TurnOffAll()
        {
            RainbowRoleAutoChange.Stop();
            //BotHelper.Stop();
        }
    }
}
