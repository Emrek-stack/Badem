using System;
using System.Collections.Generic;

namespace Bade.ModelGenerator.Models
{
    public partial class ApplicationConfig
    {
        public int Id { get; set; }
        public int ApplicationId { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public virtual Application Application { get; set; }
    }
}
