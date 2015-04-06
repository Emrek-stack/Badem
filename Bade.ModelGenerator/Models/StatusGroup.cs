using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class StatusGroup
    {
        public StatusGroup()
        {
            this.StatusGroupItems = new List<StatusGroupItem>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<StatusGroupItem> StatusGroupItems { get; set; }
    }
}
