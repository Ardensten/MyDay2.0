using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MongoDB.Driver;
using MyDay2._0.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyDay2._0.ViewModels
{
    internal partial class SignUpViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Models.User> users;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        string password;

        public Models.User User { get; set; }

        public SignUpViewModel()
        {
            Users = new ObservableCollection<Models.User>();
        }

        [RelayCommand]
        public async void AddUser()
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Name = Name,
                Password = Password
            };

            await GetUserCollection().InsertOneAsync(user);

            Users.Add(user);
        }


        public IMongoCollection<Models.User> GetUserCollection()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var users = database.GetCollection<Models.User>("UserCollection");
            return users;
        }
    }
}
