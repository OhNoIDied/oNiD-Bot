using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using oNiDBot.Services;

namespace oNiDBot.Modules
{
    public class Division2Module : ModuleBase<SocketCommandContext>
    {
        public Division2InfoService Division2InfoService { get; set; }


        [Command("stats")]
        public async Task NextLan()
        {
            if(Context.Channel.Name != "division-2" && Context.Channel.Name != "test")
            {
                return ;
            }

            var clanData = await Division2InfoService.GetClanData();

            var Ordered =  clanData.OrderByDescending(x => x.level_pve).ThenByDescending(x => x.gearscore);


            var eb = new EmbedBuilder();
            eb.WithTitle("Current Clan Rankings");
            eb.WithColor(Color.Blue);
            foreach(var member in Ordered)
            {

                eb.AddField(member.name, $"Level: {member.level_pve} - Gear Score: {member.gearscore}");


            }

            await ReplyAsync($"",false, eb.Build());

        }

        [Command("headshots")]
        public async Task headshots()
        {
            if(Context.Channel.Name != "division-2" && Context.Channel.Name != "test")
            {
                return;
            }

            var clanData = await Division2InfoService.GetClanData();

            var paired = clanData.Select(x => new Tuple<double, division2PlayerDetails>(CalculatePerc(x.kills_total,x.kills_headshot), x));

            var Ordered = paired.OrderByDescending(x => x.Item1);


            var eb = new EmbedBuilder();
            eb.WithColor(Color.Green);

            eb.WithTitle("% of headshot kills");
            foreach(var member in Ordered)
            {
                eb.AddField(member.Item2.name, $"{Math.Round(member.Item1,2)}% ({member.Item2.kills_headshot}/{member.Item2.kills_total})");
            }

            await ReplyAsync($"", false, eb.Build());

        }

        private double CalculatePerc(int total, int count)
        {
            var t = (double) ( 100 * count ) / total;
            return t;
        }
    }
}
