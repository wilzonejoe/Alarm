using System.Collections.Generic;
using System.Linq;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Entity;
using SQLite;

namespace CoreWakeMeUp.database
{
    public sealed class TimeController : DatabaseController
    {
        private static TimeController _instance;


        private TimeController(string directory) : base(directory)
        {
        }

        public static TimeController Instance(string directory = null)
        {
            if (_instance == null && directory != null)
                _instance = new TimeController(directory);
            return _instance;
        }

        public List<Time> SelectAllTime()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    return connection.Table<Time>().ToList();
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return null;
            }
        }

        public bool SelectQueryTableTime(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    connection.Query<Time>("SELECT * FROM " + typeof(Time).Name + " Where ID=?", id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }
    }
}