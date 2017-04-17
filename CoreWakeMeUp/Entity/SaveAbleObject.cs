using SQLite;

namespace CoreWakeMeUp.Entity
{
    public abstract class SaveAbleObject
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
    }
}
