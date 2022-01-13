using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("ShoppingListEntry")]
    public partial class ShoppingListEntry
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
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
        [InverseProperty(nameof(Resource.ShoppingListEntries))]
        public virtual Resource IdResourceNavigation { get; set; }
        [ForeignKey(nameof(IdShoppingList))]
        [InverseProperty(nameof(ShoppingList.ShoppingListEntries))]
        public virtual ShoppingList IdShoppingListNavigation { get; set; }
    }
}
