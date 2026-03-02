
using HabitMaster.ViewModels;

namespace HabitMaster.Views;

public partial class HabitsPage : ContentPage
{
	public HabitsPage(HabitsViewModel vm)
	{
        InitializeComponent();
		BindingContext = vm;

		vm.LoadHabitsCommand.Execute(null);
	}
}