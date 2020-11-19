using System.Collections.Generic;
using System.Linq;

namespace Ryumi.Item
{
    public static class ItemManager
    {
        private static Dictionary<ItemID, Item> _allItems = new Dictionary<ItemID, Item>
        {
            { ItemID.Scrap, new Item("Scrap", "A mess of metal, you probably need to refine it to do whatever you want with it.", true, Rarity.Common) }
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
