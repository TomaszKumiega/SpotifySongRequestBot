using SpotifySongRequestBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SpotifySongRequestBot.Builders
{
    public class TwitchClientBuilder : ITwitchClientBuilder
    {
        private ConnectionCredentials _connectionCredentials;
        private ClientOptions _clientOptions;
        private WebSocketClient _webSocketClient;

        public ITwitchClientBuilder BuildClientOptions(int messagesAllowedInPeriod, TimeSpan timeSpan)
        {
            _clientOptions = new ClientOptions()
            {
                MessagesAllowedInPeriod = messagesAllowedInPeriod,
                ThrottlingPeriod = timeSpan
            };

            return this;
        }

        public ITwitchClientBuilder BuildCredentials(string twitchUsername, string accessToken)
        {
            _connectionCredentials = new ConnectionCredentials(twitchUsername, accessToken);

            return this;
        }

        public ITwitchClientBuilder BuildWebSocketClient()
        {
            _webSocketClient = new WebSocketClient(_clientOptions);
            
            return this;
        }

        public TwitchClient Finish()
        {
            return new TwitchClient(_webSocketClient);
        }
    }
}
