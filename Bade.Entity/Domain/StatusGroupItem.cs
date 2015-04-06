/*************************************************
*StatusGroupItem Entity
* Generation Date: 16.02.2015 03:11:13
*************************************************/

using Bade.Infrastructure;

namespace Bade.Entity.Domain
{
    public class StatusGroupItem
    {
        #region Properties
        public int Id { get; set; }

        public short StatusGroupId { get; set; }

        public int StatusId { get; set; }

        #endregion

        #region ctor

        public StatusGroupItem(){}

        #endregion
    }
}
