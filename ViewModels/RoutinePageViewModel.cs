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
    internal partial class RoutinePageViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<Activity> activities;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        Guid routineId;
        [ObservableProperty]
        string title;
 

        public RoutinePageViewModel()
        {
            Activities = new ObservableCollection<Models.Activity>();
            Title = TitleStringBuilder();

        }

        public string TitleStringBuilder()
        {
            string routineName = Singleton.GetInstance().currentRoutineName;
            string routineTime = Singleton.GetInstance().currentRoutineTime.ToString();

            return routineName + " " + routineTime;
        }

        [RelayCommand]
        public async void AddActivity()
        {
            Activity activity = new Activity()
            {
                Id = Guid.NewGuid(),
                RoutineId = Singleton.GetInstance().currentRoutineId,
                Name = Name
            };

            await GetActivities().InsertOneAsync(activity);
            Activities.Add(activity);
        }

        public async Task GetRoutinesActivities()
        {
            List<Activity> activitiesFromDb = await GetActivities().AsQueryable().ToListAsync();
            foreach (Activity activity in activitiesFromDb.Where(x => x.RoutineId == Singleton.GetInstance().currentRoutineId))
            {
                Activities.Add(activity);
            }
        }

        public IMongoCollection<Activity> GetActivities()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var activities = database.GetCollection<Models.Activity>("ActivityCollection");
            return activities;
        }
    }
}
