using HabitMaster.ViewModels;

 namespace HabitMaster.Views;






public partial class HabitDetailsPage : ContentPage
{

	private readonly HabitDetailsViewModel _viewModel;
	public HabitDetailsPage( HabitDetailsViewModel viewModel)
	{
		InitializeComponent();
		_viewModel = viewModel;
		BindingContext = _viewModel;
	}

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);

		_viewModel.LoadHistoryCommand.Execute(null);
    }
}