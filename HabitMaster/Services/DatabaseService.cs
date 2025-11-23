using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HabitMaster.Models;
using SQLite;

namespace HabitMaster.Services
{
    public class DatabaseService // klasa odpowiedzialna za zapis i odczyt danych
    {
        SQLiteAsyncConnection _database;

        //obsługuje to asynchroniczne połączenie z bazą danych
        //asynchronicznie by aplikacja się nie zacinała podczas jego wykonywania
        async Task init() //inicjalizacja

        {
            // Jeśli połączenie już istnieje, to nie otwieramy go ponownie
            if (_database is not null)
                return;

            // Ustalenie ścieżki do pliku bazy danych
            var dbPath = Path.Combine(FileSystem.AppDataDirectory, "HabitMaster.db3");

            // Utworzenie połączenia asynchronicznego z SQLite
            _database = new SQLiteAsyncConnection(dbPath);

            // Tworzenie tabel (bardzo ważne! aktualizować po dodaniu nowych modeli)
            await _database.CreateTableAsync<Habit>();
            await _database.CreateTableAsync<Category>();
            await _database.CreateTableAsync<Reminder>();
            await _database.CreateTableAsync<HabitHistory>();
            await _database.CreateTableAsync<Achievement>();
        }

        // ============================================================
        // HABIT – OPERACJE Z BAZĄ
        // ============================================================

        /// <summary>
        /// Pobiera wszystkie nawyki z bazy.
        /// </summary>
        public async Task<List<Habit>> GetHabitsAsync()
        {
            await Init();
            return await _database.Table<Habit>().ToListAsync();
        }

        /// <summary>
        /// Zapisuje (dodaje lub aktualizuje) nawyk.
        /// </summary>
        public async Task<int> SaveHabitAsync(Habit habit)
        {
            await Init();
            if (habit.Id == 0)
                return await _database.InsertAsync(habit); // nowy
            else
                return await _database.UpdateAsync(habit); // aktualizacja
        }

        /// <summary>
        /// Usuwa nawyk z bazy.
        /// </summary>
        public async Task<int> DeleteHabitAsync(Habit habit)
        {
            await Init();
            return await _database.DeleteAsync(habit);
        }

        // ============================================================
        // CATEGORY – OPERACJE Z BAZĄ
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

    }
}
