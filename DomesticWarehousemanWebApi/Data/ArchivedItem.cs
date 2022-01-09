using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("ArchivedItem")]
    public partial class ArchivedItem
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Column("expiresOn", TypeName = "datetime")]
        public DateTime? ExpiresOn { get; set; }
        [Column("archivedOn", TypeName = "datetime")]
        public DateTime ArchivedOn { get; set; }
        [Column("archivingReason")]
        [StringLength(100)]
        public string ArchivingReason { get; set; }
        [Column("idResource")]
        public int IdResource { get; set; }
        [Column("idStorage")]
        public int IdStorage { get; set; }

        [ForeignKey(nameof(IdResource))]
        [InverseProperty(nameof(Resource.ArchivedItems))]
        public virtual Resource IdResourceNavigation { get; set; }
        [ForeignKey(nameof(IdStorage))]
        [InverseProperty(nameof(Storage.ArchivedItems))]
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
