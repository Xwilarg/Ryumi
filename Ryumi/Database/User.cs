using System;
using System.Collections.Generic;

namespace Ryumi.Database
{
    public class User
    {
        public User(ulong id)
        {
            this.id = id.ToString();
        }

        public DateTime LastDaily = DateTime.MinValue;

        public int CurrentStreak = 0;
        public int BestStreak = 0;

        public Dictionary<string, int> Items = new Dictionary<string, int>();

        public string id;

        public void AddItem(ItemID item)
        {
            string itemStr = item.ToString();
            if (!Items.ContainsKey(itemStr))
                Items.Add(itemStr, 1);
            else
                Items[itemStr]++;
        }
    }
}
