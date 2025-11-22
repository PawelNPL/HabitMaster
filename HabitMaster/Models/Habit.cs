using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace HabitMaster.Models
{
    public class Habit
    {

        [PrimaryKey, AutoIncrement] 
        public int Id { get; set; }

        public string Name { get; set; } //nazwa nawyku
        public string Icon {  get; set; } //nazwa emoji/obrazka użytego obok nawyku (będzie się dało customizować, kiedyś...)
        
        public string ColorHex { get; set; } //kolor tła kafelka

        public int CurrentStreak { get; set; } //aktualny streak

        public DateTime LastCompleted { get; set; } //kiedy ostatnio wykonano zadanie

        public bool IsCompletedToday => LastCompleted.Date == DateTime.Today;

    }
}
