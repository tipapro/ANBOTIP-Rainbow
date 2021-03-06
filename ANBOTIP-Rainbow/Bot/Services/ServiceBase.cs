﻿using ANBOTIP_Rainbow.Bot.Clients;
using Discord;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ANBOTIP_Rainbow.Bot.Services
{
    class ServiceBase
    {
        private CancellationTokenSource _cts;
        private Func<CancellationToken, Task> _cycleMethod;

        public string ErrorMessage;
        public string StartMessage;
        public string StopMessage;

        public bool IsStarted { get; private set; }

        public ServiceBase(string errorMessage, string startMessage, string stopMessage)
        {
            ErrorMessage = errorMessage;
            StartMessage = startMessage;
            StopMessage = stopMessage;
            _cycleMethod = Cycle;
        }

        protected virtual Task Cycle(CancellationToken token) => Task.CompletedTask;

        public async void Run()
        {
            if (IsStarted)
            {
                await BotClient.Log(new LogMessage(LogSeverity.Info, "", ErrorMessage + ": Действие уже запущено"));
                return;
            }
            while (BotClient.Guild == null) await Task.Delay(1000);
            while (BotClient.Client.ConnectionState != ConnectionState.Connected) await Task.Delay(1000);
            _cts = new CancellationTokenSource();
            IsStarted = true;
            RunCycle();
        }

        private async void RunCycle()
        {
            try
            {
                await BotClient.Log(new LogMessage(LogSeverity.Info, "", StartMessage));
                await _cycleMethod.Invoke(_cts.Token);
            }
            catch (Exception ex)
            {
                IsStarted = false;
                if ((ex is OperationCanceledException) && (_cts != null) && (((OperationCanceledException)ex).CancellationToken == _cts.Token))
                    new ExceptionLogger().Log(ex, StopMessage);
                else
                {
                    new ExceptionLogger().Log(ex, ErrorMessage);
                    Run();
                }

            }
        }

        public void Stop()
        {
            if (IsStarted && (_cts != null)) _cts.Cancel();
        }
    }
}
