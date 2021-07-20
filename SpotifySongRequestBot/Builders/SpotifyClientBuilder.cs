using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Builders
{
    public class SpotifyClientBuilder : ISpotifyClientBuilder
    {
        private AuthorizationCodeTokenRequest _clientCredentialsRequest;
        private AuthorizationCodeTokenResponse _authorizationCodeTokenResponse;
        private SpotifyClientConfig _spotifyClientConfig;

        public ISpotifyClientBuilder BuildClientConfig()
        {
            _spotifyClientConfig = SpotifyClientConfig.CreateDefault();
            return this;
        }

        public ISpotifyClientBuilder BuildClientCredentialsRequest(string clientId, string clientSecret, AuthorizationCodeResponse response, Uri uri)
        {
            _clientCredentialsRequest = new AuthorizationCodeTokenRequest(clientId, clientSecret, response.Code, uri);
            return this;
        }

        public ISpotifyClientBuilder BuildClientCredentialsResponse()
        {
            _authorizationCodeTokenResponse = new OAuthClient(_spotifyClientConfig).RequestToken(_clientCredentialsRequest).Result;
            return this;
        }

        public SpotifyClient Finish()
        {
            return new SpotifyClient(_spotifyClientConfig.WithToken(_authorizationCodeTokenResponse.AccessToken));
        }
    }
}
