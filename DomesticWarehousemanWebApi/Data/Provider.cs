using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Provider")]
    public partial class Provider
    {
        public Provider()
        {
            Resources = new HashSet<Resource>();
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

        [InverseProperty(nameof(Resource.IdProviderNavigation))]
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
