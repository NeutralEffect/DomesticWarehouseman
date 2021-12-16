using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class ShoppingListEntry
    {
        public int Amount { get; set; }
        public bool Checked { get; set; }
        public int IdResource { get; set; }
        public int IdShoppingList { get; set; }

        public virtual Resource IdResourceNavigation { get; set; }
        public virtual ShoppingList IdShoppingListNavigation { get; set; }
    }
}
