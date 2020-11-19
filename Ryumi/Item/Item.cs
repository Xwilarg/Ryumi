namespace Ryumi.Item
{
    public class Item
    {
        public Item(string name, string description, bool isAvailableForDaily, Rarity rarity)
        {
            Name = name;
            Description = description;
            IsAvailableForDaily = isAvailableForDaily;
            Rarity = rarity;
        }

        public string Name;
        public string Description;
        public bool IsAvailableForDaily;
        public Rarity Rarity;
    }
}
