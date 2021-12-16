using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class User
    {
        public User()
        {
            ShoppingListIdUserCreatorNavigations = new HashSet<ShoppingList>();
            ShoppingListIdUserOwnerNavigations = new HashSet<ShoppingList>();
            StorageMembers = new HashSet<StorageMember>();
        }

        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public virtual ICollection<ShoppingList> ShoppingListIdUserCreatorNavigations { get; set; }
        public virtual ICollection<ShoppingList> ShoppingListIdUserOwnerNavigations { get; set; }
        public virtual ICollection<StorageMember> StorageMembers { get; set; }
    }
}
