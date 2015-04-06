using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class Application
    {
        public Application()
        {
            this.ApplicationConfigs = new List<ApplicationConfig>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationConfig> ApplicationConfigs { get; set; }
    }
}
