using MyDay2._0.Models;
using MyDay2._0.ViewModels;

namespace MyDay2._0.Views;

public partial class LoggedIn : ContentPage
{
	public LoggedIn()
	{
		InitializeComponent();
        BindingContext = new ViewModels.LoggedInViewModel();

    }

    private async void OnClickedRoutines(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Routines());

    }

    private async void OnClickedNotes(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new Notes());
    }
}