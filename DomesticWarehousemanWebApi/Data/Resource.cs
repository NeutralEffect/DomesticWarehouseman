using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Resource")]
    public partial class Resource
    {
        public Resource()
        {
            ArchivedItems = new HashSet<ArchivedItem>();
            EssentialLists = new HashSet<EssentialList>();
            Items = new HashSet<Item>();
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
        [StringLength(200)]
        public string Name { get; set; }
        [Required]
        [Column("description")]
        [StringLength(1000)]
        public string Description { get; set; }
        [Required]
        [Column("image")]
        public byte[] Image { get; set; }
        [Column("idProvider")]
        public int IdProvider { get; set; }
        [Column("idCategory")]
        public int IdCategory { get; set; }
        [Column("idStorage")]
        public int? IdStorage { get; set; }

        [ForeignKey(nameof(IdCategory))]
        [InverseProperty(nameof(Category.Resources))]
        public virtual Category IdCategoryNavigation { get; set; }
        [ForeignKey(nameof(IdProvider))]
        [InverseProperty(nameof(Provider.Resources))]
        public virtual Provider IdProviderNavigation { get; set; }
        [ForeignKey(nameof(IdStorage))]
        [InverseProperty(nameof(Storage.Resources))]
        public virtual Storage IdStorageNavigation { get; set; }
        [InverseProperty(nameof(ArchivedItem.IdResourceNavigation))]
        public virtual ICollection<ArchivedItem> ArchivedItems { get; set; }
        [InverseProperty(nameof(EssentialList.IdResourceNavigation))]
        public virtual ICollection<EssentialList> EssentialLists { get; set; }
        [InverseProperty(nameof(Item.IdResourceNavigation))]
        public virtual ICollection<Item> Items { get; set; }
    }
}
