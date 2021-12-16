using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class ArchivedItem
    {
        public int Id { get; set; }
        public string ArchivizationReason { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ExpiresOn { get; set; }
        public DateTime ArchivedOn { get; set; }
        public int IdResource { get; set; }
        public int IdStorage { get; set; }

        public virtual Resource IdResourceNavigation { get; set; }
        public virtual Storage IdStorageNavigation { get; set; }
    }
}
