using MyDay2._0.ViewModels;

namespace MyDay2._0.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class ShoppingListPage : ContentPage
{
	public ShoppingListPage()
	{
		InitializeComponent();
        BindingContext = new ViewModels.ShoppingListPageViewModel();
    }

    bool pageStarted = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!pageStarted)
        {
            await (BindingContext as ShoppingListPageViewModel).GetShoppingListsItems();
            pageStarted = true;
        }
    }
}