using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace DomesticWarehousemanWebApi.Data
{
    [Table("Role")]
    public partial class Role
    {
        public Role()
        {
            StorageMembers = new HashSet<StorageMember>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(100)]
        public string Name { get; set; }

        [InverseProperty(nameof(StorageMember.IdRoleNavigation))]
        public virtual ICollection<StorageMember> StorageMembers { get; set; }
    }
}
