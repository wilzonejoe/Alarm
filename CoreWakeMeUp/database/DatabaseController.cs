using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CoreWakeMeUp.Configurations;
using CoreWakeMeUp.Entity;
using SQLite;

namespace CoreWakeMeUp.database
{
    public abstract class DatabaseController
    {
        protected readonly string _directory;

        protected DatabaseController(string directory)
        {
            _directory = directory;
        }

        public bool CreateDataBase<SaveAbleObject>()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    connection.CreateTable<SaveAbleObject>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool InsertItemIntoTable(SaveAbleObject obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    connection.Insert(obj);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool UpdateTableTime(SaveAbleObject obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    connection.Update(obj);
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool DeleteTableTime(SaveAbleObject obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, Content.dbName)))
                {
                    connection.Delete(obj);
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
