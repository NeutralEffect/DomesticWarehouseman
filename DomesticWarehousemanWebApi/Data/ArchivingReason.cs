using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("ArchivingReason")]
    public partial class ArchivingReason
    {
        public ArchivingReason()
        {
            ArchivedItems = new HashSet<ArchivedItem>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }
        [Column("description")]
        [StringLength(1000)]
        public string Description { get; set; }

        [InverseProperty(nameof(ArchivedItem.IdArchivingReasonNavigation))]
        public virtual ICollection<ArchivedItem> ArchivedItems { get; set; }
    }
}
