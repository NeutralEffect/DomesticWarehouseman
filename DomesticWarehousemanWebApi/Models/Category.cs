using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class Category
    {
        public Category()
        {
            Resources = new HashSet<Resource>();
            ShoppingLists = new HashSet<ShoppingList>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
