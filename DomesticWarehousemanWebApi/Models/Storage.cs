using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class Storage
    {
        public Storage()
        {
            ArchivedItems = new HashSet<ArchivedItem>();
            EssentialLists = new HashSet<EssentialList>();
            Items = new HashSet<Item>();
            ShoppingLists = new HashSet<ShoppingList>();
            StorageMembers = new HashSet<StorageMember>();
        }

        public int Id { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<ArchivedItem> ArchivedItems { get; set; }
        public virtual ICollection<EssentialList> EssentialLists { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
        public virtual ICollection<StorageMember> StorageMembers { get; set; }
    }
}
