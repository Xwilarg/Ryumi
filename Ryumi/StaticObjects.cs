using Ryumi.Database;
using System;

namespace Ryumi
{
    public class StaticObjects
    {
        public static Random Rand { get; } = new Random();
        public static Db Db { get; }


        static StaticObjects()
        {
            Db = new Db();
            Db.InitAsync("Ryumi").GetAwaiter().GetResult();
        }
    }
}
