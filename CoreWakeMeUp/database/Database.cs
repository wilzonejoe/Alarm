using System.Collections.Generic;
using System.Linq;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Entity;
using SQLite;

namespace CoreWakeMeUp.database
{
    public class DataBase
    {
        public string Directory { get; set; }

        public DataBase(string directory)
        {
            this.Directory = directory;
        }

        public bool CreateDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
                {
                    connection.CreateTable<Time>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool InsertIntoTableTime(Time time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
                {
                    connection.Insert(time);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public List<Time> SelectTableTime()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
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

        public bool UpdateTableTime(Time time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
                {
                    connection.Query<Time>("UPDATE Time set Hour=?,Minute=?,Second=? Where ID=?", time.Hour, time.Minute, time.Second, time.ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool DeleteTableTime(Time Time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
                {
                    connection.Delete(Time);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool SelectQueryTableTime(int id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(Directory, Content.dbName)))
                {
                    connection.Query<Time>("SELECT * FROM Time Where ID=?", id);
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