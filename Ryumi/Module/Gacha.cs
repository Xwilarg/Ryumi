using Discord;
using Discord.Commands;
using Ryumi.Database;
using System.Linq;
using System.Threading.Tasks;

namespace Ryumi.Module
{
    public class Gacha : ModuleBase
    {
        [Command("Daily")]
        public async Task Daily()
        {
            if (!StaticObjects.Db.CanDoDaily(Context.User.Id))
                await ReplyAsync($"You already did this command today.");
            else
            {
                await StaticObjects.Db.DoDaily(Context.User.Id);
                await StaticObjects.Db.AddItem(Context.User.Id, ItemID.Scrap);
                await ReplyAsync("You got a scrap.");
            }
        }

        [Command("Inventory")]
        public async Task Inventory()
        {
            await ReplyAsync(embed: new EmbedBuilder
            {
                Title = Context.User.Username + "'s inventory",
                Description = string.Join("\n", StaticObjects.Db.GetInventory(Context.User.Id).Select(x => x.Item1 + " x" + x.Item2)),
                Color = Color.Blue
            }.Build());
        }
    }
}
