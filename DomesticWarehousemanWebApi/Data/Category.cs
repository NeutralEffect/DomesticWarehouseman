using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Category")]
    public partial class Category
    {
        public Category()
        {
            Resources = new HashSet<Resource>();
            ShoppingLists = new HashSet<ShoppingList>();
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

        [InverseProperty(nameof(Resource.IdCategoryNavigation))]
        public virtual ICollection<Resource> Resources { get; set; }
        [InverseProperty(nameof(ShoppingList.IdCategoryNavigation))]
        public virtual ICollection<ShoppingList> ShoppingLists { get; set; }
    }
}
