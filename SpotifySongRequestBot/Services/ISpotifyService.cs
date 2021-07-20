using SpotifyAPI.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Services
{
    public interface ISpotifyService
    {
        Task AddToQueueAsync(string songLink);
        Task<FullTrack> GetTrackInfo(string songLink);
        Task Start();
    }
}
