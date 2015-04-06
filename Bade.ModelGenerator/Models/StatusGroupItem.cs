using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class StatusGroupItem
    {
        public int Id { get; set; }
        public short StatusGroupId { get; set; }
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }
        public virtual StatusGroup StatusGroup { get; set; }
    }
}
