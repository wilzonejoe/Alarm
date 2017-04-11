using System.Collections.Generic;
using System.Linq;
using Android.Util;
using CoreWakeMeUp.Entity;
using SQLite;

namespace AndroidWakeMeUp.Resources.database
{
    public class DataBase
    {
        string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
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
                Log.Info("SQLiteEx", ex.Message);
                return false;
            }
        }

    }
}