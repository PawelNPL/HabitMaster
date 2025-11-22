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
        //obsługuje to asynchroniczne połączenie z bazą danych
        //asynchronicznie by aplikacja się nie zacinała podczas jego wykonywania
        async Task init() //inicjalizacja
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

    }
}
