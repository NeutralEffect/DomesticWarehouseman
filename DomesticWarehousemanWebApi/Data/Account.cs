using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Account")]
    public partial class Account
    {
        public Account()
        {
            ShoppingListComments = new HashSet<ShoppingListComment>();
            ShoppingListIdAccountCreatorNavigations = new HashSet<ShoppingList>();
            ShoppingListIdAccountOwnerNavigations = new HashSet<ShoppingList>();
            StorageMembers = new HashSet<StorageMember>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Required]
        [Column("displayName")]
        [StringLength(50)]
        public string DisplayName { get; set; }
        [Required]
        [Column("email")]
        [StringLength(1000)]
        public string Email { get; set; }
        [Required]
        [Column("passwordHash")]
        [StringLength(1000)]
        public string PasswordHash { get; set; }
        [Column("systemAdministrator")]
        public bool SystemAdministrator { get; set; }

        [InverseProperty(nameof(ShoppingListComment.IdAccountNavigation))]
        public virtual ICollection<ShoppingListComment> ShoppingListComments { get; set; }
        [InverseProperty(nameof(ShoppingList.IdAccountCreatorNavigation))]
        public virtual ICollection<ShoppingList> ShoppingListIdAccountCreatorNavigations { get; set; }
        [InverseProperty(nameof(ShoppingList.IdAccountOwnerNavigation))]
        public virtual ICollection<ShoppingList> ShoppingListIdAccountOwnerNavigations { get; set; }
        [InverseProperty(nameof(StorageMember.IdAccountNavigation))]
        public virtual ICollection<StorageMember> StorageMembers { get; set; }
    }
}
