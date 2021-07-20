using Autofac;
using SpotifySongRequestBot.Services;
using System;
using System.Threading.Tasks;

namespace SpotifySongRequestBot
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var container = ContainerConfig.Configure();
            var twitchService = container.Resolve<ITwitchService>();
            
            try
            {
               await twitchService.Start();
            }
            catch (Exception e)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.White;
            }

            Console.ReadLine();
        }
    }
}
