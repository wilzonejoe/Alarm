using System.Collections.Generic;
using System.Linq;
using CoreWakeMeUp.Entity;
using SQLite;

namespace CoreWakeMeUp.database
{
    public class DataBase
    {
        public string folder { get; set; }

        public DataBase(string folder)
        {
            this.folder = folder;
        }

        public bool createDataBase()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    connection.CreateTable<Time>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool insertIntoTableTime(Time Time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    connection.Insert(Time);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public List<Time> selectTableTime()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    return connection.Table<Time>().ToList();

                }
            }
            catch (SQLiteException ex)
            {
                return null;
            }
        }

        public bool updateTableTime(Time Time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    connection.Query<Time>("UPDATE Time set Hour=?,Minute=?,Second=? Where ID=?", Time.Hour, Time.Minute, Time.Second, Time.ID);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool deleteTableTime(Time Time)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    connection.Delete(Time);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

        public bool selectQueryTableTime(int Id)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, "Times.db")))
                {
                    connection.Query<Time>("SELECT * FROM Time Where ID=?", Id);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                return false;
            }
        }

    }
}