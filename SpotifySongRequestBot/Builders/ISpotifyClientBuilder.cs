using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Builders
{
    public interface ISpotifyClientBuilder
    {
        ISpotifyClientBuilder BuildClientConfig();
        ISpotifyClientBuilder BuildClientCredentialsRequest(string clientId, string clientSecret, AuthorizationCodeResponse response, Uri uri);
        ISpotifyClientBuilder BuildClientCredentialsResponse();
        SpotifyClient Finish();
    }
}
