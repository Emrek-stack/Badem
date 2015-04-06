using System.ComponentModel.DataAnnotations.Schema;
using Bade.Infrastructure;

namespace Bade.Entity.Domain
{
    [Table("Variable.ApplicationConfig")]
    public class ApplicationConfig
    {
        public int Id { get; set; }

        public int ApplicationId { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

        //Reference
        public Application Application { get; set; }
        
    }
}