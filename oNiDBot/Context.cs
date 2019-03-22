﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oNiDBot
{
    public static class Context
    {
        public static string Token => ConfigurationManager.AppSettings["DiscordToken"];
        public static string[] GamerTags=> ConfigurationManager.AppSettings["Gamertags"].Split(',');
    }
}
