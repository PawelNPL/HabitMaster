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

        [ObservableProperty]
        private string selectedIcon = "🎯";

        [ObservableProperty]
        private string selectedColorHex = "#FF5049";

        [RelayCommand]
        private void SelectColor(string color)
        {
            selectedColorHex = color;
        }
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
            if (string.IsNullOrWhiteSpace(NewHabitName))
                return;

            //tworzenie nowego obiektu
            var habit = new Habit
            {
                Name = NewHabitName,
                ColorHex = selectedColorHex,
                Icon = selectedIcon,
                CurrentStreak = 0,
                LastCompleted = DateTime.MinValue
            };

            //Zapis w bazie

            await _dbService.SaveHabitAsync(habit);

            //dodanie do listy na ekranie
            Habits.Add(habit);

            //Wyczyszczenie pola tekstowego

            NewHabitName = String.Empty;
        }

        //odhaczanie nawyków
        [RelayCommand]
        async Task CompleteHabitAsync(Habit habit)
        {

            //poprawiony kod zapisu
            if (habit == null )
                return;

            if (!habit.IsCompletedToday)
            {
                if (habit.LastCompleted.Date == DateTime.Today.AddDays(-1))
                {
                    habit.CurrentStreak++;
                }

                else
                {
                    habit.CurrentStreak = 1;
                }

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
                //await LoadHabitsAsync();

                var index = Habits.IndexOf(habit);
                if (index >= 0)
                {
                    Habits.RemoveAt(index);
                    Habits.Insert(index, habit);
                }
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


        [RelayCommand]
        async Task GoToDetailsAsync(Habit habit)
        {
            if (habit == null) return;

            var navigationParameter = new Dictionary<string, object>
            {
                {"Habit", habit }
            };

            await Shell.Current.GoToAsync(nameof(Views.HabitDetailsPage), navigationParameter);
        }


    }
}
