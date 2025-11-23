using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HabitMaster.Models;
using SQLite;

namespace HabitMaster.Services
{
    public class DatabaseService //zapis i odczyt danych
    {
        SQLiteAsyncConnection _database;
<<<<<<< Updated upstream
        //obsługuje to asynchroniczne połączenie z bazą danych
        //asynchronicznie by aplikacja się nie zacinała podczas jego wykonywania
        async Task init() //inicjalizacja
=======
        // połączenie z bazą SQLite (async – działa w tle i nie blokuje aplikacji)

        
        /// Inicjalizacja bazy danych – tworzy połączenie i tabele.
        /// Wywoływana automatycznie przed każdą operacją na bazie.
       
        async Task Init()
>>>>>>> Stashed changes
        {
            if (_database is not null) //jeśli połączenie już istnieje to go drugi raz nie otwiera
                return;

            //ustalenie ścieżki do bazy danych.
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "HabitMaster.db3");

            _database = new SQLiteAsyncConnection(dbPath);

            //stworzenie tabel(dodanie modeli z Models) AKTUALIZOWAĆ PO KAŻDYM DODANIU NOWEJ!!!!!!!!!

            await _database.CreateTableAsync<Habit>();



        }

        //Pobieranie wszystkich nawyków PÓŹNIEJ DOKOŃCZĘ

<<<<<<< Updated upstream
=======
        
        /// Pobiera wszystkie nawyki z bazy.
        
        public async Task<List<Habit>> GetHabitsAsync()
        {
            await Init();
            return await _database.Table<Habit>().ToListAsync();
        }

        
        /// Zapisuje (dodaje lub aktualizuje) nawyk.
        
        public async Task<int> SaveHabitAsync(Habit habit)
        {
            await Init();
            if (habit.Id == 0)
                return await _database.InsertAsync(habit); // nowy
            else
                return await _database.UpdateAsync(habit); // aktualizacja
        }

        
        /// Usuwa nawyk z bazy.
        
        public async Task<int> DeleteHabitAsync(Habit habit)
        {
            await Init();

            //Dodane przez Paweł, usuwanie historii nawyku
            var history = await GetHistoryForHabitAsync(habit.Id);
            foreach (var item in history)
            {
                await _database.DeleteAsync(item);
            }

            //Dodane przez Paweł, usuwanie przypomnień o nawyku
            var reminders = await GetRemindersForHabitAsync(habit.Id);
            foreach (var reminderitem in reminders)
            {
                await _database.DeleteAsync(reminderitem);
            }


            return await _database.DeleteAsync(habit);
        }

        //Usuwa wpis z historii 
        public async Task<int> DeleteHabitHistoryAsync(HabitHistory history)
        {
            await Init();
            return await _database.DeleteAsync(history);
        }

        // ============================================================
        // KATEGORIA – OPERACJE Z BAZĄ
        // ============================================================

        public async Task<List<Category>> GetCategoriesAsync()
        {
            await Init();
            return await _database.Table<Category>().ToListAsync();
        }

        public async Task<int> SaveCategoryAsync(Category category)
        {
            await Init();
            return category.Id == 0
                ? await _database.InsertAsync(category)
                : await _database.UpdateAsync(category);
        }

        public async Task<int> DeleteCategoryAsync(Category category)
        {
            await Init();
            return await _database.DeleteAsync(category);
        }

        // ============================================================
        // REMINDER – OPERACJE Z BAZĄ
        // ============================================================

        public async Task<List<Reminder>> GetRemindersForHabitAsync(int habitId)
        {
            await Init();
            return await _database.Table<Reminder>()
                .Where(r => r.HabitId == habitId)
                .ToListAsync();
        }

        public async Task<int> SaveReminderAsync(Reminder reminder)
        {
            await Init();
            return reminder.Id == 0
                ? await _database.InsertAsync(reminder)
                : await _database.UpdateAsync(reminder);
        }

        public async Task<int> DeleteReminderAsync(Reminder reminder)
        {
            await Init();
            return await _database.DeleteAsync(reminder);
        }

        // ============================================================
        // HABIT HISTORY – OPERACJE Z BAZĄ
        // ============================================================

        public async Task<List<HabitHistory>> GetHistoryForHabitAsync(int habitId)
        {
            await Init();
            return await _database.Table<HabitHistory>()
                .Where(h => h.HabitId == habitId)
                .ToListAsync();
        }

        public async Task<int> SaveHabitHistoryAsync(HabitHistory history)
        {
            await Init();
            return history.Id == 0
                ? await _database.InsertAsync(history)
                : await _database.UpdateAsync(history);
        }

        // ============================================================
        // ACHIEVEMENTS – OPERACJE Z BAZĄ
        // ============================================================

        public async Task<List<Achievement>> GetAchievementsAsync()
        {
            await Init();
            return await _database.Table<Achievement>().ToListAsync();
        }

        public async Task<int> SaveAchievementAsync(Achievement achievement)
        {
            await Init();
            return achievement.Id == 0
                ? await _database.InsertAsync(achievement)
                : await _database.UpdateAsync(achievement);
        }
>>>>>>> Stashed changes
    }
}
