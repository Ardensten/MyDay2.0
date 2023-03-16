using MyDay2._0.Models;
using MyDay2._0.ViewModels;

namespace MyDay2._0.Views;

[XamlCompilation(XamlCompilationOptions.Skip)]
public partial class ShoppingLists : ContentPage
{
	public ShoppingLists()
	{
		InitializeComponent();
        BindingContext = new ViewModels.ShoppingListsViewModel();
    }

    bool pageStarted = false;
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (!pageStarted)
        {
            await (BindingContext as ShoppingListsViewModel).GetUsersShoppingLists();
            pageStarted = true;
        }
    }

    private async void OnListViewShoppingListSelected(object sender, SelectedItemChangedEventArgs e)
    {
        var list = ((ListView)sender).SelectedItem as Models.ShoppingList;
        Singleton.GetInstance().currentShoppingListId = list.Id;
        Singleton.GetInstance().currentShoppingListName = list.Name;

        await Navigation.PushAsync(new ShoppingListPage());
    }
}