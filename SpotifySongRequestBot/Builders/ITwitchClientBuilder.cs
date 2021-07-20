using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;

namespace SpotifySongRequestBot.Builders
{
    public interface ITwitchClientBuilder
    {
        ITwitchClientBuilder BuildCredentials(string twitchUsername, string accessToken);
        ITwitchClientBuilder BuildClientOptions(int messagesAllowedInPeriod, TimeSpan timeSpan);
        ITwitchClientBuilder BuildWebSocketClient();
        TwitchClient Finish();
    }
}
