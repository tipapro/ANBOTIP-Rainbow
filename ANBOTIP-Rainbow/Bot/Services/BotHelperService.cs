﻿using System;
using System.Threading;
using System.Threading.Tasks;
using BotAnbotip.Bot.Clients;
using Discord;

namespace BotAnbotip.Bot.Services
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
                while (BotClient.Client.ConnectionState != ConnectionState.Connected)
                {
                    await Task.Delay(new TimeSpan(0, 0, 10), token);
                    await BotClient.Launch();
                }
            }
        }
    }
}