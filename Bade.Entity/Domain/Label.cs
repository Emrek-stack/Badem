using System.Collections.Generic;
using Bade.Entity.Domain;

namespace Bade.Entity.Domain
{
    public class Label
    {
        public int LabelId { get; set; }

        public string Name { get; set; }


        //Reference
        public IEnumerable<LabelDetail> LabelDetail { get; set; }  
    }
}