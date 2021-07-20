using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Services
{
    public interface ICommandsService
    {
        Task<string> SongRequestAsync(string songLink);
        Task Start();
    }
}
