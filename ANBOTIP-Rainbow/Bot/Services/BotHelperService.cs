using System;
using System.Threading;
using System.Threading.Tasks;
using ANBOTIP_Rainbow.Bot.Clients;
using Discord;

namespace ANBOTIP_Rainbow.Bot.Services
{
    class BotHelperService : ServiceBase
    {
        public BotHelperService(string errorMessage, string startMessage, string stopMessage) : 
            base(errorMessage, startMessage, stopMessage)
        {
        }

        protected override async Task Cycle(CancellationToken token)
        {
            while (IsStarted)
            {             
                while ((BotClient.Client.ConnectionState != ConnectionState.Connected) || (BotClient.Client.ConnectionState != ConnectionState.Connecting))
                {
                    await Task.Delay(new TimeSpan(0, 0, 10), token);
                    await BotClient.Client.StartAsync();
                }
            }
        }
    }
}
