using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyDay2._0.Models
{
    class ShoppingListItem
    {
        public Guid Id { get; set; }
        public Guid ListId { get; set; }
        public string Name { get; set; }
    }
}
