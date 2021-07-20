using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Services
{
    public class CommandsService : ICommandsService
    {
        private ISpotifyService SpotifyService { get; }

        public CommandsService(ISpotifyService spotifyService)
        {
            SpotifyService = spotifyService;
        }

        public async Task<string> SongRequestAsync(string songLink)
        {
            await SpotifyService.AddToQueueAsync(songLink);
            FullTrack track = await SpotifyService.GetTrackInfo(songLink);

            var artists = string.Join(", ", track.Artists.Select(x => x.Name));
            var response = string.Format(Resources.SpotifyResponses.SpotifyResponses.AddedToQueue, track.Name, artists);

            return response;
        }

        public async Task Start()
        {
            await SpotifyService.Start();
        }
    }
}
