using Autofac;
using SpotifySongRequestBot.Builders;
using SpotifySongRequestBot.Configuration;
using SpotifySongRequestBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TwitchLib.Client;
using TwitchLib.Communication.Clients;
using TwitchLib.Communication.Models;

namespace SpotifySongRequestBot
{
    public static class ContainerConfig
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<TwitchClientBuilder>().As<ITwitchClientBuilder>();
            builder.RegisterType<TwitchService>().As<ITwitchService>();
            builder.RegisterType<CommandsService>().As<ICommandsService>();
            builder.RegisterType<SpotifyService>().As<ISpotifyService>();
            builder.RegisterType<ConfigurationReader>().As<IConfigurationReader>();
            builder.RegisterType<SpotifyClientBuilder>().As<ISpotifyClientBuilder>();

            return builder.Build();
        }
    }
}
