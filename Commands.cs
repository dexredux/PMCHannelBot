using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace botbase
{
    public class Commands : ModuleBase
    {
        public IReadOnlyCollection<IRole> Roles;
        public static int PMChannelCount = 0;

        [Command("CreatePMCHannel")]
        public async Task CreatePMChannel(SocketGuildUser user1, SocketGuildUser user2)
        {
            PMChannelCount++;
            string PMCHANNELDET = "pmchannel" + PMChannelCount.ToString();
            var pmchannelcreate = await Context.Guild.CreateTextChannelAsync(PMCHANNELDET);
            var pmchannelrole = await Context.Guild.CreateRoleAsync(PMCHANNELDET);

            OverwritePermissions RolePermissions = new OverwritePermissions(PermValue.Allow, PermValue.Deny, PermValue.Allow, PermValue.Allow, PermValue.Allow);
            OverwritePermissions RolePermissions2 = new OverwritePermissions(PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Deny, PermValue.Deny);

            Roles = Context.Guild.Roles;
            foreach(IRole role in Roles)
            {
                await pmchannelcreate.AddPermissionOverwriteAsync(role, RolePermissions2);
            }
            foreach (IRole role in Roles)
            {
                if(role.Name == "PMChannelBot")
                {
                    await pmchannelcreate.AddPermissionOverwriteAsync(role, RolePermissions);
                }
            }

            await pmchannelcreate.AddPermissionOverwriteAsync(pmchannelrole, RolePermissions);

            await pmchannelrole.ModifyAsync(x =>
            {
                x.Position = 10;
                x.Mentionable = false;
            });

            await pmchannelcreate.ModifyAsync(x =>
            {
                x.Position = 1;
            });

            await user1.AddRoleAsync(pmchannelrole);
            await user2.AddRoleAsync(pmchannelrole);
        }

        [Command("DeletePMChannel")]
        public async Task DeletePMChannel()
        {
            var User = (SocketGuildUser)Context.User;
            foreach(SocketRole role in User.Roles)
            {
                if (role.Permissions.Administrator)
                {
                    SocketTextChannel channel = (SocketTextChannel)Context.Channel;
                    await channel.DeleteAsync();
                    string tempchannelname = channel.Name;
                    Roles = Context.Guild.Roles;

                    foreach (IRole nrole in Roles)
                    {
                        if (nrole.Name == tempchannelname)
                        {
                            await nrole.DeleteAsync();
                        }
                    }
                }
            }


        }


        [Command("Help")]
        public async Task SendHelp()
        {
            //update with server check and privelege check as well as commands
            await ReplyAsync("My owner is lazy, this is all the commands:" + Environment.NewLine +
                "1. Ping: Kinda broken, but oh well." + Environment.NewLine +
                "2. Kick (user): Kicks a user" + Environment.NewLine +
                "3. CreatePMChannel (user 1) (user2): Creates pm channel with the given users" + Environment.NewLine +
                "4. UpdateServer: Updates the roles known to the bot on the server and activates the ability to use the meee6 role rewards bypass");
            
        }
    }
}
