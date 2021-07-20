using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifySongRequestBot.Configuration
{
    public class ConfigurationReader : IConfigurationReader
    {
        private string _configurationFilePath => $".\\Configuration\\Configuration.json";

        public IBotConfiguration GetBotConfiguration()
        {
            string json = System.IO.File.ReadAllText(_configurationFilePath, Encoding.UTF8);
            return JsonConvert.DeserializeObject<BotConfiguration>(json);
        }
    }
}
