using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using MyDay2._0.Models;
using System.Collections.ObjectModel;

namespace MyDay2._0.ViewModels
{
    internal partial class ShoppingListsViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<ShoppingList> shoppingLists;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        Guid userId;
        [ObservableProperty]
        string name;

        public ShoppingListsViewModel()
        {
            ShoppingLists = new ObservableCollection<ShoppingList>();
        }

        [RelayCommand]
        public async void AddShoppingList()
        {
            ShoppingList shoppingList = new()
            {
                Id = Guid.NewGuid(),
                UserId = Singleton.GetInstance().loggedInId,
                Name = Name,
            };

            await GetShoppingLists().InsertOneAsync(shoppingList);
            ShoppingLists.Add(shoppingList);
        }

        [RelayCommand]
        public async void RemoveShoppingList(object s)
        {
            var shoppingList = (ShoppingList)s;
            await GetShoppingLists().DeleteOneAsync(x => x.Id == shoppingList.Id);
            ShoppingLists.Remove(shoppingList);
        }

        public async Task GetUsersShoppingLists()
        {

            List<ShoppingList> shoppingListsFromDb = await GetShoppingLists().AsQueryable().ToListAsync();
            foreach (var shoppingList in shoppingListsFromDb.Where(x => x.UserId == Singleton.GetInstance().loggedInId))
            {
                ShoppingLists.Add(shoppingList);
            }
        }

        public IMongoCollection<ShoppingList> GetShoppingLists()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var shoppingLists = database.GetCollection<ShoppingList>("ShoppingListCollection");
            return shoppingLists;
        }
    }
}
