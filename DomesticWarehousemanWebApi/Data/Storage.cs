using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Storage")]
    public partial class Storage
    {
        public Storage()
        {
            ArchivedItems = new HashSet<ArchivedItem>();
            EssentialLists = new HashSet<EssentialList>();
            Items = new HashSet<Item>();
            Resources = new HashSet<Resource>();
            ShoppingLists = new HashSet<ShoppingList>();
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
        [StringLength(100)]
        public string DisplayName { get; set; }
        [Column("idAccountCreator")]
        public int IdAccountCreator { get; set; }
        [Column("idAccountOwner")]
        public int IdAccountOwner { get; set; }

        [ForeignKey(nameof(IdAccountCreator))]
        [InverseProperty(nameof(Account.StorageIdAccountCreatorNavigations))]
        public virtual Account IdAccountCreatorNavigation { get; set; }
        [ForeignKey(nameof(IdAccountOwner))]
        [InverseProperty(nameof(Account.StorageIdAccountOwnerNavigations))]
        public virtual Account IdAccountOwnerNavigation { get; set; }
        [InverseProperty(nameof(ArchivedItem.IdStorageNavigation))]
        public virtual ICollection<ArchivedItem> ArchivedItems { get; set; }
        [InverseProperty(nameof(EssentialList.IdStorageNavigation))]
        public virtual ICollection<EssentialList> EssentialLists { get; set; }
        [InverseProperty(nameof(Item.IdStorageNavigation))]
        public virtual ICollection<Item> Items { get; set; }
        [InverseProperty(nameof(Resource.IdStorageNavigation))]
        public virtual ICollection<Resource> Resources { get; set; }
        [InverseProperty(nameof(ShoppingList.IdStorageNavigation))]
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
        [InverseProperty(nameof(StorageMember.IdStorageNavigation))]
        public virtual ICollection<StorageMember> StorageMembers { get; set; }
    }
}
