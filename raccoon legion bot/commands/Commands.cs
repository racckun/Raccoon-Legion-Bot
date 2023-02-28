using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace raccoonLegionBotCommands
{
    public class Commands : ModuleBase<SocketCommandContext>
    {
        Random random = new Random();
        CommandService commandService = new CommandService();

        [Command("ping")]
        public async Task Ping()
        {
            await ReplyAsync("Pong!");
        }
        [Command("help")]
        public async Task Help()
        {
            var embed = new EmbedBuilder()
              .WithDescription("!help - this command;\n !congrat - congrat gif;\n !whatthedogdoing - what the dog doing gif;\n !eventping - pings everyone for an event(founder only ||epic fail billbo||);" +
              "\n !thingamabob - thingamabob gif\n !promote - promotes user using the role index\n !promoteroles - shows available role indexes\n!gudbai - gudbai\n !avatar - shows a user's avatar" +
              "\n!raccoonrate - raccoon rates you\n!raccoonbias - checks if you are a raccoon based if you have 'r', 'a', 'c' letters in your username or not\n!blahaj - blahaj" +
              "\n!suckmyrobotballs - you can force someone to suck your robot balls; usage: !suckmyrobotballs (user ping)")
              .WithTitle("List of commands")
              .WithCurrentTimestamp()
              .WithColor(Discord.Color.Blue);
            await ReplyAsync(embed: embed.Build());
        }

        [Command("eventping")]
        public async Task EventPing([Remainder] string eventDescription)
        {
            var user = Context.User as SocketGuildUser;
            var role = Context.Guild.GetRole(1068477515250798612);

            // checks if user DOES contain the required role :nerd:
            if (user.Roles.Contains(role))
            {
                var embed = new EmbedBuilder()
                   .WithTitle("Event!")
                   .WithDescription($"Event description: {eventDescription}")
                   .WithCurrentTimestamp()
                   .WithColor(Discord.Color.Blue);
                await ReplyAsync("@everyone");
                await ReplyAsync(embed: embed.Build());
            }

            // checks if user DOESNT contain the required role :nerd:
            else if (!user.Roles.Contains(role))
            {
                await ReplyAsync("You don't have the required role to perform this command! :nerd:");
            }

        }
        [Command("thingamabob")]
        public async Task Thingamabob()
        {
            var link = "https://tenor.com/view/me-at-work-i-oversee-i-oversee-the-thingamabob-thingamabob-me-at-work-i-oversee-the-thingamabob-gif-24774005";
            await ReplyAsync(link);
        }

        [Command("congrat")]
        public async Task Congrat()
        {
            var link = "https://media.discordapp.net/attachments/930452281248346132/1038350621222375434/caption.gif";
            await ReplyAsync(link);
        }

        [Command("whatthedogdoing")]
        public async Task WhatTheDogDoing()
        {
            var link = "https://tenor.com/view/dog-sims-funny-as-hell-funny-dog-what-da-dog-doing-gif-22182887";
            await ReplyAsync(link);
        }

        [Command("gudbai")]
        public async Task Gudbai()
        {
            var link = "https://media.discordapp.net/attachments/833745553279549500/1073359288891879585/image.png";
            await ReplyAsync(link);
        }

        [Command("promote")]
        public async Task Promote(IGuildUser userToPromote, int promotionIndex)
        {
            var user = Context.User as SocketGuildUser;
            var role = Context.Guild.GetRole(1068477515250798612);

            Dictionary<string, ulong> roles = new Dictionary<string, ulong>();
            roles.Add("cannon fodder", 1068754844778770503);
            roles.Add("slightly matters", 1074330796694974556);
            roles.Add("elite cannon fodder", 1074330841951510618);
            roles.Add("garbage", 1074331107002155099);
            roles.Add("private(eer)", 1074331325932261407);

            if (!user.Roles.Contains(role))
            {
                await ReplyAsync("You don't have the required role to perform this command! :nerd:");
            }

            else if (user.Roles.Contains(role))
            {
                await userToPromote.AddRoleAsync(roles.ElementAt(promotionIndex++).Value);
                await ReplyAsync($"{user.Mention} promoted {userToPromote.Mention}");

                // TODO: send congrat gif and user mention to a specific channel     
            }
        }
        [Command("promoteroles")]
        public async Task PromoteRoles()
        {
            var embed = new EmbedBuilder()
              .WithTitle("Promote roles indexes")
              .WithDescription("0 - cannon fodder\n 1 - slightly matters\n2 - elite cannon fodder\n3 - garbage\n4 - private(eer)")
              .WithColor(Discord.Color.Blue);
            await ReplyAsync(embed: embed.Build());
        }
        [Command("avatar")]
        public async Task Avatar(IGuildUser user)
        {
            await ReplyAsync(user.GetAvatarUrl());
        }
        [Command("raccoonrate")]
        public async Task RaccoonRate()
        {
            string imageURL = "https://cdn.discordapp.com/attachments/919181078495907863/1074356244317478932/771px-Procyon_lotor.jpg";
            int raccoonResult = random.Next(0, 101);

            if (raccoonResult >= 50)
            {
                imageURL = "https://cdn.discordapp.com/attachments/919181078495907863/1074356244317478932/771px-Procyon_lotor.jpg";
            }

            if (raccoonResult < 50)
            {
                imageURL = "https://cdn.discordapp.com/attachments/919181078495907863/1074356769167528036/f84V3ZFl9AJEMnwqm8XUkIddJeRp69IwA8vKBpwZ8ew.png";
            }

            var EmbedBuilder = new EmbedBuilder()
                .WithDescription($":raccoon: You are {raccoonResult}% raccoon")
                .WithColor(Discord.Color.Blue)
                .WithImageUrl(imageURL);

            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }
        [Command("raccoonbias")]
        public async Task RaccoonBias()
        {
            var link = "";
            var user = Context.User as SocketGuildUser;
            var text = "";

            if (!user.Username.Contains("r") || !user.Username.Contains("a") || !user.Username.Contains("c"))
            {
                link = "https://cdn.discordapp.com/attachments/919181078495907863/1074781512031023234/138b348b-1e3c-4e98-bc2f-c15b63ce896e.png";
                text = ":raccoon: You're not even close to being a raccoon! Epic embed fail!";
            }

            if (user.Username.Contains("r") || user.Username.Contains("a"))
            {
                link = "https://cdn.discordapp.com/attachments/919181078495907863/1074778451728404611/da1azh0-0bdb0e25-ab4a-4f4b-95a6-8666c5dfdf6b.png";
                text = ":raccoon: You scored 50 points! You're 1/2 of a raccoon!";

                if (user.Username.Contains("c"))
                {
                    link = "https://cdn.discordapp.com/attachments/919181078495907863/1074778451728404611/da1azh0-0bdb0e25-ab4a-4f4b-95a6-8666c5dfdf6b.png";
                    text = ":raccoon: You scored 100 points! You're a real raccoon!";
                }
            }
            var EmbedBuilder = new EmbedBuilder()
                .WithDescription(text)
                .WithColor(Discord.Color.Blue)
                .WithImageUrl(link);

            Embed embed = EmbedBuilder.Build();
            await ReplyAsync(embed: embed);
        }

        [Command("blahaj")]
        public async Task Blahaj()
        {
            List<string> blahaj = new List<string>();
            blahaj.Add("https://cdn.discordapp.com/attachments/919181078495907863/1076227316130263050/3484782943_ikea-blahaj-original.webp");
            blahaj.Add("https://cdn.discordapp.com/attachments/919181078495907863/1076228012724461619/s-l500.jpg");
            blahaj.Add("https://cdn.discordapp.com/attachments/919181078495907863/1076228228794040461/2166258484_ikea-blahaj-original.webp");
            blahaj.Add("https://cdn.discordapp.com/attachments/919181078495907863/1076228302756393110/ngzqcocozwh91.webp");
            blahaj.Add("https://cdn.discordapp.com/attachments/919181078495907863/1076228369932361768/FFskr4XVQAA1AAZ.jpg");

            int randomBlahaj = random.Next(blahaj.Count);

            await ReplyAsync(blahaj[randomBlahaj]);
        }
        [Command("suckmyrobotballs")]
        public async Task SuckMyRobotBalls(string userToSuckRobotBalls)
        {
            var user = Context.User as SocketGuildUser;
            var link = "https://cdn.discordapp.com/attachments/919181078495907863/1076239745119686656/suckmyrobotballs.mp4";
            await ReplyAsync($"{userToSuckRobotBalls} should suck {user.Mention}'s robot balls {link}");
        }
    }
}