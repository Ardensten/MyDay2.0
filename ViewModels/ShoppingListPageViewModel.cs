using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using MyDay2._0.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyDay2._0.ViewModels
{
    internal partial class ShoppingListPageViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<ShoppingListItem> shoppingListItems;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string shoppingListName;

        public ShoppingListPageViewModel()
        {
            ShoppingListItems = new ObservableCollection<Models.ShoppingListItem>();
            ShoppingListName = Singleton.GetInstance().currentShoppingListName;
        }

        [RelayCommand]
        public async void AddItem()
        {
            ShoppingListItem item = new ShoppingListItem()
            {
                Id = Guid.NewGuid(),
                ListId = Singleton.GetInstance().currentShoppingListId,
                Name = Name
            };

            await GetItems().InsertOneAsync(item);
            ShoppingListItems.Add(item);
        }

        [RelayCommand]
        public async void RemoveItem(object i)
        {
            var item = (ShoppingListItem)i;
            await GetItems().DeleteOneAsync(x => x.Id == item.Id);
            ShoppingListItems.Remove(item);
        }

        public async Task GetShoppingListsItems()
        {

            List<ShoppingListItem> shoppingListItemsFromDb = await GetItems().AsQueryable().ToListAsync();
            foreach (ShoppingListItem item in shoppingListItemsFromDb.Where(x => x.ListId == Singleton.GetInstance().currentShoppingListId))
            {
                ShoppingListItems.Add(item);
            }
        }

        public IMongoCollection<ShoppingListItem> GetItems()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var items = database.GetCollection<Models.ShoppingListItem>("ShoppingListItemCollection");
            return items;
        }
    }
}
