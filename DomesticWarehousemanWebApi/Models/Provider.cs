using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class Provider
    {
        public Provider()
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}
