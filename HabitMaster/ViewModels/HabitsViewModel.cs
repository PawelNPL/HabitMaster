using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using HabitMaster.Models;
using HabitMaster.Services;

namespace HabitMaster.ViewModels
{
    //ObservableObject - klasa Mvvm, automatycznie powiadamia UI o zmianach 
    public partial class HabitsViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        //lIsta nawyków wyświetlanych na ekranie
        //Automatycznie się odświaża po dodaniu/usunięciu
        public ObservableCollection<Habit> Habits { get; } = new();

        //Tu bedzie wpisywany nowy nawyk (a raczej jego nazwa)
        [ObservableProperty]
        string newHabitName;

        //konstruktor 
        public HabitsViewModel(DatabaseService dbService)
        {
            _dbService = dbService;

        }

        //ładowanie nazyków na starcie
        [RelayCommand]
        async Task LoadHabitsAsync()
        {
            var habits = await _dbService.GetHabitsAsync();
            Habits.Clear();
            foreach (var habit in habits)
                Habits.Add(habit);
        }

        //dodawanie nowego nawyku

        [RelayCommand]
        async Task AddHabitAsync()
        {
            if (string.IsNullOrWhiteSpace(newHabitName))
                return;

            //tworzenie nowego obiektu
            var habit = new Habit
            {
                Name = newHabitName,
                ColorHex = "#F5049", //domyślny kolor *później zmienić
                CurrentStreak = 0,
                LastCompleted = DateTime.MinValue
            };

            //Zapis w bazie

            await _dbService.SaveHabitAsync(habit);

            //dodanie do listy na ekranie
            Habits.Add(habit);

            //Wyczyszczenie pola tekstowego

            newHabitName = String.Empty;
        }

        //odhaczanie nawyków
        [RelayCommand]
        async Task CompleteHabitAsync(Habit habit)
        {
            if (habit == null)
                return;

            if (!habit.IsCompletedToday)
            {
                //aktualizacja danych
                habit.CurrentStreak++;
                habit.LastCompleted = DateTime.Now;

                //zapis danych w bazie
                await _dbService.SaveHabitAsync(habit);

                //zapis danych w historii
                var HistoryEntry = new HabitHistory
                {
                    HabitId = habit.Id,
                    DateCompleted = DateTime.Now
                };
                await _dbService.SaveHabitHistoryAsync(HistoryEntry);

                //odświeżenie widoku
                await LoadHabitsAsync();
            }
        }

        //Usuwanie nawyku
        [RelayCommand]
        async Task DeleteHabitAsync(Habit habit)
        {
            if (habit == null)
                return;

            //pytanie o potwierdzenie usunięcia
            bool answer = await Shell.Current.DisplayAlert("Usuwanie", $"Czy na pewno chcesz usunąć '{habit.Name}'?", "Tak", "Nie");

            if (answer)
            {
                //usunięcie z bazy
                await _dbService.DeleteHabitAsync(habit);

                //usunięcie z listy na ekranie
                Habits.Remove(habit);
            }
        }





    }
}
