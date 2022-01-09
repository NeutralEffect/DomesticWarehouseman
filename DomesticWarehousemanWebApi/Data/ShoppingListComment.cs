using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("ShoppingListComment")]
    public partial class ShoppingListComment
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Required]
        [Column("content")]
        [StringLength(1000)]
        public string Content { get; set; }
        [Column("idShoppingList")]
        public int IdShoppingList { get; set; }
        [Column("idAccount")]
        public int IdAccount { get; set; }

        [ForeignKey(nameof(IdAccount))]
        [InverseProperty(nameof(Account.ShoppingListComments))]
        public virtual Account IdAccountNavigation { get; set; }
        [ForeignKey(nameof(IdShoppingList))]
        [InverseProperty(nameof(ShoppingList.ShoppingListComments))]
        public virtual ShoppingList IdShoppingListNavigation { get; set; }
    }
}
