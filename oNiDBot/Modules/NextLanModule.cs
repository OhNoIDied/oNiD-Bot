using System;
using System.Net;
using System.Threading.Tasks;
using Discord.Commands;

namespace oNiDBot.Modules
{
    public class NextLanModule : ModuleBase<SocketCommandContext>
    {
        [Command("NextLan")]
        public Task NextLan()
        {
            string htmlContents = "";
            using(var client = new WebClient())
            {
                htmlContents = client.DownloadString("https://lan.lsucs.org.uk/");
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlContents);

            var lanName = doc.DocumentNode.SelectSingleNode("//*[@id=\"lan\"]").InnerText;
            var startDatestr = doc.DocumentNode.SelectSingleNode("//*[@id=\"title-container\"]/script").InnerText;
            startDatestr = startDatestr.Replace("var countdown_start = \"", "");
            startDatestr = startDatestr.Replace("\";", "");

            var startDate = DateTime.Parse(startDatestr);

            var days = Math.Round((  startDate - DateTime.Now ).TotalDays,0);

            return ReplyAsync(
                $"{lanName} starts {startDate.ToString("dddd, dd MMMM yyyy")} ({days} days)\n");

        }
            
    }

}
