using Discord.Commands;
using DiscordUtils;
using System.Threading.Tasks;

namespace Ryumi.Module
{
    public class Communication : ModuleBase
    {
        [Command("Info")]
        public async Task Info()
        {
            await ReplyAsync(embed: Utils.GetBotInfo(Program.P.StartTime, "Ryumi", Program.P.Client.CurrentUser));
        }
    }
}
