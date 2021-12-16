using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class ShoppingList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IdStorage { get; set; }
        public int IdUserCreator { get; set; }
        public int IdUserOwner { get; set; }
        public int? IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Storage IdStorageNavigation { get; set; }
        public virtual User IdUserCreatorNavigation { get; set; }
        public virtual User IdUserOwnerNavigation { get; set; }
    }
}
