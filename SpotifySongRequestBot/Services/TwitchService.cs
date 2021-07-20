using SpotifySongRequestBot.Builders;
using SpotifySongRequestBot.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Client.Events;
using TwitchLib.Client.Models;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SpotifySongRequestBot.Services
{
    public class TwitchService : ITwitchService
    {
        private TwitchClient Client { get; }
        private IBotConfiguration Configuration { get; }
        private ICommandsService CommandsService { get; }

        public TwitchService(IConfigurationReader configurationReader, ICommandsService commandsService, ITwitchClientBuilder twitchClientBuilder)
        {
            Configuration = configurationReader.GetBotConfiguration();
            CommandsService = commandsService;
            Client = twitchClientBuilder
                        .BuildCredentials(Configuration.TwitchUsername, Configuration.TwitchAccessToken)
                        .BuildClientOptions(750, TimeSpan.FromSeconds(30))
                        .BuildWebSocketClient()
                        .Finish();
        }

        private async void OnMessageReceived(object sender, OnMessageReceivedArgs e)
        {
            if (e.ChatMessage.Message.StartsWith(Configuration.CommandsConfig.CommandPrefix))
            {
                await ExecuteCommand(e.ChatMessage);
            }
        }

        private async Task ExecuteCommand(ChatMessage message)
        {
            var songRequest = Configuration.CommandsConfig.CommandPrefix + Configuration.CommandsConfig.SongRequest;
            
            if (message.Message.StartsWith(songRequest))
            {
                var songLink = message.Message.Remove(0, songRequest.Length).Trim();
                try
                {
                    var feedbackMessage = await CommandsService.SongRequestAsync(songLink);

                    if (!String.IsNullOrWhiteSpace(feedbackMessage))
                    {
                        Client.SendMessage(Configuration.TwitchChannelName, feedbackMessage);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        public async Task Start()
        {
            var creds = Configuration.GetTwitchConnectionCredentials();
            Client.Initialize(creds, Configuration.TwitchChannelName);

            Client.OnMessageReceived += OnMessageReceived;
            
            await Task.Run(() => Client.Connect());
            await CommandsService.Start();
        }
    }
}
