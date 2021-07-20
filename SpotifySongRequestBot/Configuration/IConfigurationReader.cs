using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Configuration
{
    public interface IConfigurationReader
    {
        IBotConfiguration GetBotConfiguration();
    }
}
