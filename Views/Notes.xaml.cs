using MyDay2._0.ViewModels;

namespace MyDay2._0.Views;

public partial class Notes : ContentPage
{
	public Notes()
	{
		InitializeComponent();
		BindingContext = new ViewModels.NotesViewModel();
	}

    bool pageStarted = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!pageStarted)
        {
            Task t = (BindingContext as NotesViewModel).GetUsersNotes();
            pageStarted = true;
        }
    }
}