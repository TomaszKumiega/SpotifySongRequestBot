using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace SpotifySongRequestBot.Configuration
{
    public class BotConfiguration : IBotConfiguration
    {
        public string TwitchUsername { get; set; }
        public string TwitchAccessToken { get; set; }
        public string TwitchChannelName { get; set; }
        public string SpotifyClientId { get; set; }
        public string SpotifyClientSecret { get; set; }
        public string SpofityPlaylistId { get; set; }

        public CommandsConfiguration CommandsConfig { get; set; }

        public ConnectionCredentials GetTwitchConnectionCredentials()
        {
            return new ConnectionCredentials(TwitchUsername, TwitchAccessToken);
        }
    }
}
