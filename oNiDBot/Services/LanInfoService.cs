using System;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace oNiDBot.Services
{
    public class LanInfoService
    {
 
        public LanInfoService()
        {
          
        }

        public async Task<NextLanInfo> GetNextLanInfo()
        {
            string htmlContents = "";
            using(var client = new WebClient())
            {
                htmlContents = await client.DownloadStringTaskAsync(new Uri("https://lan.lsucs.org.uk/"));
            }
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            doc.LoadHtml(htmlContents);

            var lanName = doc.DocumentNode.SelectSingleNode("//*[@id=\"lan\"]").InnerText;
            var startDatestr = doc.DocumentNode.SelectSingleNode("//*[@id=\"title-container\"]/script").InnerText;
            startDatestr = startDatestr.Replace("var countdown_start = \"", "");
            startDatestr = startDatestr.Replace("\";", "");

            var startDate = DateTime.Parse(startDatestr);

            return new NextLanInfo() { StartDate = startDate, Name = lanName };

        }

    }
    public class NextLanInfo
    {
        public DateTime StartDate { get; set; }
        public string Name { get; set; }
    }
}
