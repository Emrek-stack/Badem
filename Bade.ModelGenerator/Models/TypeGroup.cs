using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class TypeGroup
    {
        public TypeGroup()
        {
            this.TypeGroupItems = new List<TypeGroupItem>();
        }

        public short Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<TypeGroupItem> TypeGroupItems { get; set; }
    }
}
