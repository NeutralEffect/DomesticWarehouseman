using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class Resource
    {
        public Resource()
        {
            ArchivedItems = new HashSet<ArchivedItem>();
            EssentialLists = new HashSet<EssentialList>();
            Items = new HashSet<Item>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public byte[] Image { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IdProvider { get; set; }
        public int IdCategory { get; set; }

        public virtual Category IdCategoryNavigation { get; set; }
        public virtual Provider IdProviderNavigation { get; set; }
        public virtual ICollection<ArchivedItem> ArchivedItems { get; set; }
        public virtual ICollection<EssentialList> EssentialLists { get; set; }
        public virtual ICollection<Item> Items { get; set; }
    }
}
