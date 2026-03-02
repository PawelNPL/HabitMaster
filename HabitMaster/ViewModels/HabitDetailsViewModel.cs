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
    [QueryProperty(nameof(Habit), "Habit")]
    public partial class HabitDetailsViewModel : ObservableObject
    {
        private readonly DatabaseService _dbService;

        public HabitDetailsViewModel(DatabaseService dbService)
        {
            _dbService = dbService;
        }

        [ObservableProperty]
        private Habit habit;

        public ObservableCollection<HabitHistory> HistoryList { get; } = new();

        [RelayCommand]
        public async Task LoadHistoryAsync()
        {
            if (Habit == null) return;

            var historyFromDb = await _dbService.GetHistoryForHabitAsync(Habit.Id);

            HistoryList.Clear();
            foreach (var item in historyFromDb)
            {
                HistoryList.Add(item);
            }
        }
    }


}
