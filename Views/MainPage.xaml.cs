using Microsoft.Maui.ApplicationModel.Communication;
using MongoDB.Driver;
using MyDay2._0.Models;
using MyDay2._0.ViewModels;
using MyDay2._0.Views;
using System.Collections.Generic;

namespace MyDay2._0;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
        BindingContext = new ViewModels.RoutinesViewModel();
    }
    private async void OnClickedSignUp(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignUp());
    }
    private async void ConfirmLogIn(object sender, EventArgs e)
    {
        List<User> usersFromDb = GetUserCollection().AsQueryable().ToList();
        bool success = false;
        foreach (var user in usersFromDb)
        {
            if (user.Name == name.Text && user.Password == password.Text)
            {
                Singleton.GetInstance().loggedInId = user.Id;
                Singleton.GetInstance().loggedInName = user.Name;
                success = true;
            }
        }
        if (success)
        {
            await Navigation.PushAsync(new LoggedIn());
        }
    }
    public IMongoCollection<User> GetUserCollection()
    {
        var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
        settings.ServerApi = new ServerApi(ServerApiVersion.V1);
        var client = new MongoClient(settings);
        var database = client.GetDatabase("MyDayDb");
        var users = database.GetCollection<User>("UserCollection");
        return users;
    }

}

