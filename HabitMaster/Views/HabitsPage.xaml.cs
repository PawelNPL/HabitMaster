using HabitMaster.ViewModels;

namespace HabitMaster.Views
{
    public partial class HabitsPage : ContentPage
    {
        public HabitsPage()
        {
            InitializeComponent();

            var vm = App.ServiceProvider.GetService(typeof(HabitsViewModel)) as HabitsViewModel;
            BindingContext = vm;

            vm?.LoadHabitsCommand.Execute(null);
        }
    }
}
