using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("EssentialList")]
    public partial class EssentialList
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
        [Column("idStorage")]
        public int IdStorage { get; set; }
        [Column("idResource")]
        public int IdResource { get; set; }

        [ForeignKey(nameof(IdResource))]
        [InverseProperty(nameof(Resource.EssentialLists))]
        public virtual Resource IdResourceNavigation { get; set; }
        [ForeignKey(nameof(IdStorage))]
        [InverseProperty(nameof(Storage.EssentialLists))]
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
