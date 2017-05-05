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

        public bool CreateDataBase<T>()
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, typeof(T).Name)))
                {
                    connection.CreateTable<T>();
                    return true;
                }
            }
            catch (SQLiteException ex)
            {
                System.Diagnostics.Debug.WriteLine(ex);
                return false;
            }
        }

        public bool InsertItemIntoTable<T>(T obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, typeof(T).Name)))
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

        public bool UpdateTableTime<T>(T obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, typeof(T).Name)))
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

        public bool DeleteTableTime<T>(T obj)
        {
            try
            {
                using (var connection = new SQLiteConnection(System.IO.Path.Combine(_directory, typeof(T).Name)))
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
