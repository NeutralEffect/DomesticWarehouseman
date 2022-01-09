using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Keyless]
    [Table("ShoppingListEntry")]
    public partial class ShoppingListEntry
    {
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Column("amount")]
        public int Amount { get; set; }
        [Column("checked")]
        public bool Checked { get; set; }
        [Column("idResource")]
        public int IdResource { get; set; }
        [Column("idShoppingList")]
        public int IdShoppingList { get; set; }

        [ForeignKey(nameof(IdResource))]
        public virtual Resource IdResourceNavigation { get; set; }
        [ForeignKey(nameof(IdShoppingList))]
        public virtual ShoppingList IdShoppingListNavigation { get; set; }
    }
}
