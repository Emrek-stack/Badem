using System.Collections.Generic;

namespace Bade.Admin.Model.Model.Output
{
    public class ApplicationConfigViewModel
    {
        public int ApplicationId { get; set; }

        public string ApplicationName { get; set; }

        public string Key { get; set; }

        public string Value { get; set; }

    }

    public class ApplicationConfigOutput
    {
        public IEnumerable<ApplicationConfigViewModel> ApplicationConfigList { get; set; }
    }


}
