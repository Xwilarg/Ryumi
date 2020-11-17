using RethinkDb.Driver;
using RethinkDb.Driver.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ryumi.Database
{
    public class Db
    {
        public Db()
        {
            _r = RethinkDB.R;
            _users = new Dictionary<ulong, User>();
        }

        public async Task InitAsync(string dbName)
        {
            _dbName = dbName;
            _conn = await _r.Connection().ConnectAsync();
            if (!await _r.DbList().Contains(_dbName).RunAsync<bool>(_conn))
                _r.DbCreate(_dbName).Run(_conn);
            if (!await _r.Db(_dbName).TableList().Contains("Users").RunAsync<bool>(_conn))
                _r.Db(_dbName).TableCreate("Users").Run(_conn);
        }

        private void LoadUserInDb(ulong userId)
        {
            lock (_users)
            {
                if (!_users.ContainsKey(userId))
                {
                    if (!_r.Db(_dbName).Table("Users").GetAll(userId.ToString()).Count().Eq(0).Run<bool>(_conn))
                    {
                        var tmp = (User)_r.Db(_dbName).Table("Users").Get(userId.ToString()).Run<User>(_conn);
                        _users.Add(ulong.Parse(tmp.id), tmp);
                    }
                    else
                    {
                        var u = new User(userId);
                        _r.Db(_dbName).Table("Users").Insert(u).Run(_conn);
                        _users.Add(userId, u);
                    }
                }
            }
        }

        public async Task AddItem(ulong userId, ItemID id)
        {
            LoadUserInDb(userId);

            var user = _users[userId];
            user.AddItem(id);
            await _r.Db(_dbName).Table("Users").Update(_r.HashMap("id", userId.ToString())
                .With("Items", user.Items)
            ).RunAsync(_conn);
        }

        public async Task DoDaily(ulong userId)
        {
            LoadUserInDb(userId);

            var user = _users[userId];
            if ((int)(DateTime.Now - new DateTime(user.LastDaily.Year, user.LastDaily.Month, user.LastDaily.Day)).TotalDays > 1) // We broke our streak (didn't do daily yesterday)
            {
                if (user.CurrentStreak > user.BestStreak) // Save best streak if we did better than last one
                    user.BestStreak = user.CurrentStreak;
                user.CurrentStreak = 0;
            }
            user.CurrentStreak++;
            user.LastDaily = DateTime.Now;

            await _r.Db(_dbName).Table("Users").Update(_r.HashMap("id", userId.ToString())
                .With("CurrentStreak", user.CurrentStreak)
                .With("BestStreak", user.BestStreak)
                .With("LastDaily", user.LastDaily)
            ).RunAsync(_conn);
        }

        public bool CanDoDaily(ulong userId)
        {
            LoadUserInDb(userId);

            var lastDate = _users[userId].LastDaily;
            return (int)(DateTime.Now - new DateTime(lastDate.Year, lastDate.Month, lastDate.Day)).TotalDays > 0;
        }

        public (string, int)[] GetInventory(ulong userId)
        {
            LoadUserInDb(userId);

            return _users[userId].Items.Select(x => (x.Key, x.Value)).ToArray();
        }

        public async Task<string> DumpAsync(ulong userId)
        {
            return (await _r.Db(_dbName).Table("Users").Get(userId.ToString()).RunAsync(_conn)).ToString();
        }

        private RethinkDB _r;
        private Connection _conn;
        private string _dbName;

        private static Dictionary<ulong, User> _users;
    }
}
