using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client.Models;

namespace SpotifySongRequestBot.Configuration
{
    public interface IBotConfiguration
    {
        string TwitchUsername { get; }
        string TwitchAccessToken { get; }
        string TwitchChannelName { get; }
        string SpotifyClientId { get; set; }
        string SpotifyClientSecret { get; set; }
        string SpofityPlaylistId { get; set; }
        
        CommandsConfiguration CommandsConfig { get; }

        ConnectionCredentials GetTwitchConnectionCredentials();
    }
}
