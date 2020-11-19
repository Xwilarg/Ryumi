using Ryumi.Module;
using System.Collections.Generic;
using System.Linq;

namespace Ryumi.Item
{
    public static class ItemManager
    {
        private static Dictionary<ItemID, Item> _allItems = new Dictionary<ItemID, Item>
        {
            { ItemID.Scrap, new Item("Scrap", "A mess of metal, you probably need to refine it to do whatever you want with it.", true, Rarity.Common) },
            { ItemID.ElectricalComponent, new Item("Electrical Component", "Lot of components, such as LED, resistances, toaster printed circuits...", true, Rarity.Common) },
            { ItemID.EmptyBottle, new Item("Empty Bottle", "Put whatever you want in it", true, Rarity.Uncommon) },
            { ItemID.RefinedMetal, new Item("Refined Metal", "Some cool shiny metal. Nice.", false, Rarity.Uncommon) },
            { ItemID.PrintedCircuit, new Item("Printed Circuit", "A printed circuit. Solder the wires red and white to start the fan, the red and yellow to do an explosion.", false, Rarity.Uncommon) },
            { ItemID.RedPaint, new Item("Red Paint", "Allow you to paint stuffs in red.", true, Rarity.Rare) },
            { ItemID.BluePaint, new Item("Blue Paint", "Allow you to paint stuffs in blue.", true, Rarity.Rare) },
            { ItemID.GachaCoupon, new Item("Gacha Coupon", "Allow you to get lot of interesting things, like scraps, scraps, and rusted scraps.", true, Rarity.SuperRare) },
            { ItemID.DarkGem, new Item("Dark Gem", "A totally normal and not evil gem.", true, Rarity.UltraRare) }
        };

        public static Item GetItem(ItemID id)
        {
            return _allItems[id];
        }

        public static (ItemID, Item)[] GetDailyItems()
        {
            return _allItems.Where(x => x.Value.IsAvailableForDaily).Select(x => (x.Key, x.Value)).ToArray();
        }
    }
}
