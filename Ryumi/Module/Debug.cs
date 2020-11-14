using Discord.Commands;
using System.Threading.Tasks;

namespace Ryumi.Module
{
    public class Debug : ModuleBase
    {
        [Command("Dump")]
        public async Task Dump()
        {
            await ReplyAsync(await StaticObjects.Db.DumpAsync(Context.User.Id));
        }
    }
}
