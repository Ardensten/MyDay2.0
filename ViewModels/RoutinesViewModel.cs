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

namespace MyDay2._0.ViewModels
{
    internal partial class RoutinesViewModel : ObservableObject
    {

        [ObservableProperty]
        ObservableCollection<Routine> routines;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        Guid userId;
        [ObservableProperty]
        string name;
        [ObservableProperty]
        TimeSpan time;

         

 

        public RoutinesViewModel()
        {
            Routines = new ObservableCollection<Routine>();

        }

        [RelayCommand]
        public async void AddRoutine()
        {
            Routine routine = new Routine()
            {
                Id = Guid.NewGuid(),
                UserId = Singleton.GetInstance().loggedInId,
                Name = Name,
                Time = Time
        };

            await GetRoutines().InsertOneAsync(routine);
            Routines.Add(routine);
        }


        public async Task GetUsersRoutines()
        {
            List<Routine> routinesFromDb = await GetRoutines().AsQueryable().ToListAsync();
            foreach (var routine in routinesFromDb.Where(x => x.UserId == Singleton.GetInstance().loggedInId)) 
            { 
                Routines.Add(routine);
            }
        }

        public IMongoCollection<Routine> GetRoutines()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var routines = database.GetCollection<Models.Routine>("RoutineCollection");
            return routines;
        }
    }
}
