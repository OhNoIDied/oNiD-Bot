using System;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using oNiDBot.Services;

namespace oNiDBot.Modules
{
    public class NextLanModule : ModuleBase<SocketCommandContext>
    {
        public LanInfoService LanInfoService { get; set; }

        [Command("NextLan")]
        public async Task NextLan()
        {
            var nextLanInfo = await LanInfoService.GetNextLanInfo();

            var days = Math.Round(( nextLanInfo.StartDate - DateTime.Now ).TotalDays,0);

            await ReplyAsync(
                $"{nextLanInfo.Name} starts {nextLanInfo.StartDate.ToString("dddd, dd MMMM yyyy")} ({days} days)\n");

        }
            
    }

}
