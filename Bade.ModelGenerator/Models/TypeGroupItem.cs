using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class TypeGroupItem
    {
        public int Id { get; set; }
        public short TypeGroupId { get; set; }
        public int TypeId { get; set; }
        public virtual Type Type { get; set; }
        public virtual TypeGroup TypeGroup { get; set; }
    }
}
