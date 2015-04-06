using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Status
    {
        public Status()
        {
            this.StatusGroupItems = new List<StatusGroupItem>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsAccessible { get; set; }
        public virtual ICollection<StatusGroupItem> StatusGroupItems { get; set; }
    }
}
