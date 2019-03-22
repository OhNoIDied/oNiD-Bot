using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;

using oNiDBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace oNiDBot
{
    class Program
    {
        static void Main(string[] args)
            => new Program().MainAsync().GetAwaiter().GetResult();

        private DiscordSocketClient _client;

        public async Task MainAsync()
        {

            _client = new DiscordSocketClient();
          

            var services = ConfigureServices();
            
            await services.GetRequiredService<CommandHandlingService>().InitializeAsync(services);
            
            await _client.LoginAsync(TokenType.Bot, Context.Token);
            await _client.StartAsync();
            
            await Task.Delay(-1);
        }

        private IServiceProvider ConfigureServices()
        {
            return new ServiceCollection()
                // Base
                .AddSingleton(_client)
                .AddSingleton<CommandService>()
                .AddSingleton<CommandHandlingService>()
                .AddSingleton<LanInfoService>()
                .AddSingleton<Division2InfoService>()
                // Add additional services here...
                .BuildServiceProvider();
        }

        
    }
}
