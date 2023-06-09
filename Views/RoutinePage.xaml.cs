using MongoDB.Bson;
using MongoDB.Driver;
using MyDay2._0.Models;
using MyDay2._0.ViewModels;

namespace MyDay2._0.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class RoutinePage : ContentPage
{
    public RoutinePage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.RoutinePageViewModel();
    }

    bool pageStarted = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!pageStarted)
        {
            await (BindingContext as RoutinePageViewModel).GetRoutinesActivities();
            pageStarted = true;
        }
    }
}