using Discord;
using Discord.Commands;
using Ryumi.Item;
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
                var items = ItemManager.GetDailyItems();
                Rarity rarity;
                var random = StaticObjects.Rand.Next(0, 100);
                if (random == 0)
                    rarity = Rarity.UltraRare;
                else if (random < 10)
                    rarity = Rarity.SuperRare;
                else if (random < 25)
                    rarity = Rarity.Rare;
                else if (random < 50)
                    rarity = Rarity.Uncommon;
                else
                    rarity = Rarity.Common;
                var rItems = items.Where(x => x.Item2.Rarity == rarity).ToArray();
                var curr = rItems[StaticObjects.Rand.Next(0, rItems.Length)];
                await StaticObjects.Db.AddItem(Context.User.Id, curr.Item1);
                await ReplyAsync("You got a " + curr.Item2.Rarity.ToString() + " item: a " + curr.Item2.Name + ".");
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
