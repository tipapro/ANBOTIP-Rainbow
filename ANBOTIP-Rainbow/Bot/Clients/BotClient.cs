using System;
using System.Threading.Tasks;
using Discord.WebSocket;
using BotAnbotip.Bot.Data;
using BotAnbotip.Bot.Handlers;
using BotAnbotip.Bot.Services;
using Discord;

namespace BotAnbotip.Bot.Clients
{
    public class BotClient
    {
        private static MessageHandler _msgHandler;
        private static ServiceManager _cyclicActionManager;

        protected static DiscordSocketClient _client = new DiscordSocketClient();
        protected static SocketGuild _guild;

        public static DiscordSocketClient Client => _client;
        public static SocketGuild Guild => _guild;

        private static Task SetGuild(SocketGuild guild)
        {
            _guild = guild;
            return Task.CompletedTask;
        }

        private static Task Loaded()
        {
            if (_client.CurrentUser.Username != "ANBOTIP Rainbow") _client.CurrentUser.ModifyAsync((prop) => { prop.Username = "ANBOTIP Rainbow"; });
            return Log(new LogMessage(LogSeverity.Info, "", "Бот авторизован"));
        }

        private static Task Disconnected(Exception ex)
        {
            new ExceptionLogger().Log(ex, "Бот отключён");
            return Task.CompletedTask;
        }

        public static async Task<bool> Launch()
        {
            try
            {
                await _client.LoginAsync(TokenType.Bot, PrivateData.BotToken);
                await _client.StartAsync();
            }
            catch (Discord.Net.HttpException)
            {
                return false;
            }
            return true;
        }

        public static Task Log(LogMessage msg)
        {
            Console.WriteLine(DateTime.Now + "  " + "ANBOTIP Rainbow: " + msg.Message);
            return Task.CompletedTask;
        }

        public static async Task PrepareAsync()
        {
            _client.Log += Log;
            _client.GuildAvailable += SetGuild;
            _client.Connected += Loaded;
            _client.Disconnected += Disconnected;
            _cyclicActionManager = new ServiceManager();
            _client.Connected += OnConnection;            
            _client.Disconnected += OnDisconnection;
            _client.MessageReceived += OnMessageReceiving;

            await _client.SetGameAsync("github.com/tipapro");
        }

        private static Task OnConnection()
        {
            _cyclicActionManager.RunAll();
            _msgHandler = new MessageHandler(_client.CurrentUser.Id, PrivateData.Prefix);
            return Task.CompletedTask;
        }

        private static Task OnDisconnection(Exception ex)
        {
            _cyclicActionManager.TurnOffAll();
            return Task.CompletedTask;
        }

        private static Task OnMessageReceiving(SocketMessage message)
        {
            _msgHandler.ProcessTheMessage(message);
            return Task.CompletedTask;
        }
    }
}
