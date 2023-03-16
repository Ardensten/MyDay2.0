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
    internal partial class NotesViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<Note> notes;

        [ObservableProperty]
        Guid id;
        [ObservableProperty]
        Guid userId;
        [ObservableProperty]
        string noteEntry;


        public NotesViewModel()
        {
            Notes = new ObservableCollection<Note>();

        }

        [RelayCommand]
        public async void AddNote()
        {
            Note note = new Note()
            {
                Id = Guid.NewGuid(),
                UserId = Singleton.GetInstance().loggedInId,
                NoteEntry = NoteEntry
            };

            await GetNotes().InsertOneAsync(note);
            Notes.Add(note);
        }

        [RelayCommand]
        public async void RemoveNote(object n)
        {
            var note = (Note)n;
            await GetNotes().DeleteOneAsync(x => x.Id == note.Id);
            Notes.Remove(note);
        }


        public async Task GetUsersNotes()
        {
            List<Note> notesFromDb = await GetNotes().AsQueryable().ToListAsync();
            foreach (Note note in notesFromDb.Where(x => x.UserId == Singleton.GetInstance().loggedInId))
            {
                Notes.Add(note);
            }
        }

        public IMongoCollection<Note> GetNotes()
        {
            var settings = MongoClientSettings.FromConnectionString("mongodb://MyDayAdmin:YEDMwVJQ3HYJ3lDV@ac-fghsmx0-shard-00-00.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-01.sqyghhb.mongodb.net:27017,ac-fghsmx0-shard-00-02.sqyghhb.mongodb.net:27017/?ssl=true&replicaSet=atlas-j76h55-shard-0&authSource=admin&retryWrites=true&w=majority");
            settings.ServerApi = new ServerApi(ServerApiVersion.V1);
            var client = new MongoClient(settings);
            var database = client.GetDatabase("MyDayDb");
            var notes = database.GetCollection<Note>("NoteCollection");
            return notes;
        }
    }
}
