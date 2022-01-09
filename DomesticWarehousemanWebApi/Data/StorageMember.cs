using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("StorageMember")]
    public partial class StorageMember
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("createdOn", TypeName = "datetime")]
        public DateTime CreatedOn { get; set; }
        [Column("updatedOn", TypeName = "datetime")]
        public DateTime UpdatedOn { get; set; }
        [Column("idRole")]
        public int IdRole { get; set; }
        [Column("idStorage")]
        public int IdStorage { get; set; }
        [Column("idAccount")]
        public int IdAccount { get; set; }

        [ForeignKey(nameof(IdAccount))]
        [InverseProperty(nameof(Account.StorageMembers))]
        public virtual Account IdAccountNavigation { get; set; }
        [ForeignKey(nameof(IdRole))]
        [InverseProperty(nameof(Role.StorageMembers))]
        public virtual Role IdRoleNavigation { get; set; }
        [ForeignKey(nameof(IdStorage))]
        [InverseProperty(nameof(Storage.StorageMembers))]
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
