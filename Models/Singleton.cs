using MyDay2._0.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDay2._0.Models
{

    public sealed class Singleton
    {
        private Singleton()
        {
        }

        private static Singleton _instance;

        public static Singleton GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Singleton();
            }
            return _instance;
        }

        public Guid loggedInId;
        public string loggedInName;
        public List<Routine> loggedInUsersRoutines;

        public Guid currentRoutineId;
        public string currentRoutineName;
        public TimeSpan currentRoutineTime;

        public Guid currentShoppingListId;
        public string currentShoppingListName;
    }
}

