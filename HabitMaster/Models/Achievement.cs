using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitMaster.Models
{
    public class Achievement
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; set; }         // np. "Tydzień bez przerwy"
        public string Description { get; set; }  // opis osiągnięcia

        public int RequiredStreak { get; set; }  // ile dni trzeba mieć
    }
}
