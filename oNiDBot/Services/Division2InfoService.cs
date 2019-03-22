using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Newtonsoft.Json;

namespace oNiDBot.Services
{
    public class Division2InfoService
    {
        
        public Division2InfoService()
        {
          
        }

        public async Task<List<division2PlayerDetails>> GetClanData()
        {
            List<string> Pids = await GetPids();

            List<division2PlayerDetails> details = await GetDetails(Pids);

            return details;
        }

        private async Task<List<division2PlayerDetails>> GetDetails(List<string> pids)
        {
            List<division2PlayerDetails> details = new List<division2PlayerDetails>();
            using(var client = new WebClient())
            {
                foreach(var pid in pids)
                {
                    var url = $"https://thedivisiontab.com/api/player.php?pid={pid}";

                    var rawJs = await client.DownloadStringTaskAsync(url);

                    var result = JsonConvert.DeserializeObject<division2PlayerDetails>(rawJs);
                    details.Add(result);
                }

            }
            return details;
        }

        private async Task<List<string>> GetPids()
        {
            List<string> Pids = new List<string>();
            using(var client = new WebClient())
            {
                foreach(var gamertag in Context.GamerTags)
                {
                    var url = $"https://thedivisiontab.com/api/search.php?name={gamertag}&platform=xbl";

                    var rawJs = await client.DownloadStringTaskAsync(url);

                    var result = JsonConvert.DeserializeObject < Division2SearchResults>(rawJs);

                    if(result.totalresults == 1)
                    {
                        Pids.Add(result.results.First().pid);
                    }
                }
                
            }
            return Pids;
        }
    }
    public class Division2SearchResults
    {
        public List<Division2SearchResult> results { get; set; }
        public int totalresults { get; set; }
    }
    public class Division2SearchResult
    {
        public string pid { get; set; }
        public string name { get; set; }
        public string user { get; set; }
        public string platform { get; set; }
        public int kills_pvp { get; set; }
        public int kills_npc { get; set; }
        public int level_pve { get; set; }
        public int level_dz { get; set; }
        public string avatar_146 { get; set; }
        public string avatar_256 { get; set; }
    }
    public class division2PlayerDetails
    {
        public string pid { get; set; }
        public string name { get; set; }
        public string platform { get; set; }
        public string user { get; set; }
        public int visitors { get; set; }
        public int utime { get; set; }
        public int ecredits { get; set; }
        public int level_pve { get; set; }
        public int level_dz { get; set; }
        public string lastmission { get; set; }
        public int xp_clan { get; set; }
        public int xp_dz { get; set; }
        public int xp_ow { get; set; }
        public int xp_pvp { get; set; }
        public int timeplayed_total { get; set; }
        public int timeplayed_dz { get; set; }
        public int timeplayed_pve { get; set; }
        public int timeplayed_pvp { get; set; }
        public int timeplayed_rogue { get; set; }
        public int maxtime_rogue { get; set; }
        public int kills_npc { get; set; }
        public int kills_pvp { get; set; }
        public int kills_total { get; set; }
        public int kills_bleeding { get; set; }
        public int kills_shocked { get; set; }
        public int kills_burning { get; set; }
        public int kills_ensnare { get; set; }
        public int kills_headshot { get; set; }
        public int kills_skill { get; set; }
        public int kills_turret { get; set; }
        public int kills_pvp_namedbosses { get; set; }
        public int kills_pvp_elitebosses { get; set; }
        public int kills_pvp_dz_total { get; set; }
        public int kills_pvp_dz_rogue { get; set; }
        public int kills_pve_hyenas { get; set; }
        public int kills_pve_outcasts { get; set; }
        public int kills_pve_blacktusk { get; set; }
        public int kills_pve_truesons { get; set; }
        public int kills_pve_dz_hyenas { get; set; }
        public int kills_pve_dz_outcasts { get; set; }
        public int kills_pve_dz_blacktusk { get; set; }
        public int kills_pve_dz_truesons { get; set; }
        public int kills_wp_pistol { get; set; }
        public int kills_wp_grenade { get; set; }
        public int kills_wp_smg { get; set; }
        public int kills_wp_shotgun { get; set; }
        public int kills_wp_rifles { get; set; }
        public int kills_specialization { get; set; }
        public string specialization { get; set; }
        public int headshots { get; set; }
        public int looted { get; set; }
        public int gearscore { get; set; }
        public string extra_data { get; set; }
        public string avatar_146 { get; set; }
        public string avatar_256 { get; set; }
        public bool playerfound { get; set; }
    }
}
