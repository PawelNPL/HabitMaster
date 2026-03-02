using Microsoft.Extensions.DependencyInjection;
using HabitMaster.Services;
using HabitMaster.ViewModels;
using HabitMaster.Views;


namespace HabitMaster;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // twoje fonty
            });

        // REJESTRACJA SERWISÓW I VIEWMODEL
        builder.Services.AddSingleton<DatabaseService>();            // jedna instancja bazy
        builder.Services.AddTransient<HabitsViewModel>();           // viewmodel rozwiązywany na żądanie
        builder.Services.AddTransient<HabitsPage>();                // strona rozwiązywana przez DI jeśli potrzebna
        builder.Services.AddTransient<HabitDetailsViewModel>(); //ViewModel do szczegółów (statystyk) poszczególnych habitów
        builder.Services.AddTransient<HabitDetailsPage>();         //View j.w

        var app = builder.Build();

 

        return app;
    }
}
