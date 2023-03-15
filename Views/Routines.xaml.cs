using MongoDB.Driver;
using MyDay2._0.Models;
using MyDay2._0.ViewModels;
using System.Xml.Linq;

namespace MyDay2._0.Views;

public partial class Routines : ContentPage
{
    public Routines()
    {
        InitializeComponent();
        BindingContext = new ViewModels.RoutinesViewModel();
    }
    bool pageStarted = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!pageStarted)
        {
            Task t = (BindingContext as RoutinesViewModel).GetUsersRoutines();
            pageStarted = true;
        }
    }
    private async void OnListViewRoutineSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var routine = ((ListView)sender).SelectedItem as Models.Routine;
        Singleton.GetInstance().currentRoutineId = routine.Id;
        Singleton.GetInstance().currentRoutineName = routine.Name;
        Singleton.GetInstance().currentRoutineTime = routine.Time;

        await Navigation.PushAsync(new RoutinePage());
    }
}