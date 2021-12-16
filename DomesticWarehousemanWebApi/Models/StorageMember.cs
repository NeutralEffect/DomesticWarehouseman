using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class StorageMember
    {
        public int Id { get; set; }
        public int Role { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IdStorage { get; set; }
        public int IdUser { get; set; }

        public virtual Storage IdStorageNavigation { get; set; }
        public virtual User IdUserNavigation { get; set; }
    }
}
