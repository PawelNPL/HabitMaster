namespace HabitMaster
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(Views.HabitDetailsPage), typeof(Views.HabitDetailsPage)); //rejestracja ścieżki
        }
    }
}
