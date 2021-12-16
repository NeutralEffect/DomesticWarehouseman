﻿using System;
using System.Collections.Generic;

#nullable disable

namespace DomesticWarehousemanWebApi.Models
{
    public partial class EssentialList
    {
        public int Id { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public int IdStorage { get; set; }
        public int IdResource { get; set; }

        public virtual Resource IdResourceNavigation { get; set; }
        public virtual Storage IdStorageNavigation { get; set; }
    }
}