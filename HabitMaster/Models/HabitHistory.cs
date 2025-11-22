using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitMaster.Models
{
    public class HabitHistory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Indexed]
        public int HabitId { get; set; }     // jaki nawyk

        public DateTime Date { get; set; }   // kiedy wykonano

        public bool Completed { get; set; }  // czy wykonano (true/false)
    }
}
