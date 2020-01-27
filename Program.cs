using System;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.WebSocket;
using Discord.Rest;

namespace botbase
{
    class Program
    {
        //@"release/Info/UINFO.txt"
        public DiscordSocketClient Client;
        public CommandHandler Handler;
        public static string bottoken;
        public static bool InfoEntOpen = false;

        static void Main(string[] args) => new Program().Start().GetAwaiter().GetResult();

        public async Task Start()
        {
            Client = new DiscordSocketClient();
            Handler = new CommandHandler();

            await Client.LoginAsync(Discord.TokenType.Bot, "YOUR BOT TOKEN HERE", true);
            await Client.StartAsync();


            await Handler.Install(Client);


            // subscribe to Client.Ready callback
            Client.Ready += Client_Ready;
            await Task.Delay(-1);
        }

        private async Task Client_Ready()
        {
            Console.WriteLine("Were Online");

            return;
        }

        public static int SetBotToken(string InputToken)
        {
            bottoken = InputToken;
            return 0;
        }

    }
}
