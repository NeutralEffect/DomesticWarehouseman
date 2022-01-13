using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("ShoppingList")]
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            ShoppingListComments = new HashSet<ShoppingListComment>();
            ShoppingListEntries = new HashSet<ShoppingListEntry>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("idStorage")]
        public int IdStorage { get; set; }
        [Column("idAccountCreator")]
        public int IdAccountCreator { get; set; }
        [Column("idAccountOwner")]
        public int IdAccountOwner { get; set; }
        [Column("idCategory")]
        public int? IdCategory { get; set; }

        [ForeignKey(nameof(IdAccountCreator))]
        [InverseProperty(nameof(Account.ShoppingListIdAccountCreatorNavigations))]
        public virtual Account IdAccountCreatorNavigation { get; set; }
        [ForeignKey(nameof(IdAccountOwner))]
        [InverseProperty(nameof(Account.ShoppingListIdAccountOwnerNavigations))]
        public virtual Account IdAccountOwnerNavigation { get; set; }
        [ForeignKey(nameof(IdCategory))]
        [InverseProperty(nameof(Category.ShoppingLists))]
        public virtual Category IdCategoryNavigation { get; set; }
        [ForeignKey(nameof(IdStorage))]
        [InverseProperty(nameof(Storage.ShoppingLists))]
        public virtual Storage IdStorageNavigation { get; set; }
        [InverseProperty(nameof(ShoppingListComment.IdShoppingListNavigation))]
        public virtual ICollection<ShoppingListComment> ShoppingListComments { get; set; }
        [InverseProperty(nameof(ShoppingListEntry.IdShoppingListNavigation))]
        public virtual ICollection<ShoppingListEntry> ShoppingListEntries { get; set; }
    }
}
