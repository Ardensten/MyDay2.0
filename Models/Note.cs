using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDay2._0.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string NoteEntry { get; set; }
    }
}
