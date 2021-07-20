using SpotifyAPI.Web;
using SpotifyAPI.Web.Auth;
using SpotifySongRequestBot.Builders;
using SpotifySongRequestBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Services
{
    public class SpotifyService : ISpotifyService
    {
        private IBotConfiguration Configuration { get; }
        private ISpotifyClientBuilder SpotifyClientBuilder { get; set; }
        private SpotifyClient SpotifyClient { get; set; }
        private EmbedIOAuthServer AuthServer { get; set; }
        private Uri AuthorizationUri { get; }

        public SpotifyService(ISpotifyClientBuilder spotifyClientBuilder, IConfigurationReader configurationReader)
        {
            Configuration = configurationReader.GetBotConfiguration();
            SpotifyClientBuilder = spotifyClientBuilder;
            AuthorizationUri = new Uri("http://localhost:8000/spotifyAuth/");
        }

        private async Task Login()
        {
            AuthServer = new EmbedIOAuthServer(AuthorizationUri, 8000);
            await AuthServer.Start();

            AuthServer.AuthorizationCodeReceived += OnAuthorizationCodeReceived;
            AuthServer.ErrorReceived += OnErrorReceived;

            var request = new LoginRequest(AuthServer.BaseUri, Configuration.SpotifyClientId, LoginRequest.ResponseType.Code)
            {
                Scope = new List<string> { Scopes.UserReadEmail, Scopes.UserModifyPlaybackState, Scopes.AppRemoteControl}
            };
            BrowserUtil.Open(request.ToUri());
        }

        private async Task OnAuthorizationCodeReceived(object sender, AuthorizationCodeResponse response)
        {
            InitializeSpotifyClient(response);
            await AuthServer.Stop();
        }

        private async Task OnErrorReceived(object sender, string error, string state)
        {
            Console.WriteLine($"Authorization error: {error}");
            await AuthServer.Stop();
        }

        private void InitializeSpotifyClient(AuthorizationCodeResponse response)
        {
            SpotifyClient =
                SpotifyClientBuilder
                    .BuildClientConfig()
                    .BuildClientCredentialsRequest(Configuration.SpotifyClientId, Configuration.SpotifyClientSecret, response, AuthorizationUri)
                    .BuildClientCredentialsResponse()
                    .Finish();
        }

        private string GetTrackIdFromLink(string songLink)
        {
            return songLink.Split('/', '?')[4];
        }

        private string GetTrackURIFromLink(string songLink)
        {
            var id = GetTrackIdFromLink(songLink);
            return $"spotify:track:{id}";
        }

        public async Task AddToQueueAsync(string songLink)
        {
            var trackURI = GetTrackURIFromLink(songLink);
            var request = new PlayerAddToQueueRequest(trackURI);
            await Task.Run(() => SpotifyClient.Player.AddToQueue(request));
        }

        public async Task<FullTrack> GetTrackInfo(string songLink)
        {
            var trackId = GetTrackIdFromLink(songLink);
            return await Task.Run(() => SpotifyClient.Tracks.Get(trackId));
        }

        public async Task Start()
        {
            await Login();
        }
    }
}
