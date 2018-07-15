using System;

namespace ANBOTIP_Rainbow.Bot.Data
{
    class PrivateData
    {
        public static string DropboxApiKey { get; private set; }
        public static string BotToken { get; private set; }
        public static char Prefix { get; private set; }

        public static void Read()
        {
            DropboxApiKey = Environment.GetEnvironmentVariable("DropboxToken");
            BotToken = Environment.GetEnvironmentVariable("BotToken");
            Prefix = '}';
        }
    }
}
