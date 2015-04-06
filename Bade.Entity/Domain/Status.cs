/*************************************************
*Status Entity
* Generation Date: 16.02.2015 03:11:13
*************************************************/

using Bade.Infrastructure;

namespace Bade.Entity.Domain
{
    public class Status
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public bool IsAccessible { get; set; }

        #endregion

        #region ctor

        public Status(){}

        #endregion
    }
}
