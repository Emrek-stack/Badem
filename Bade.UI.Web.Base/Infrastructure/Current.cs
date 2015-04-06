using System;
using System.Configuration;
using Bade.Constants.Structs;
using Bade.UI.Web.Base.Account;
using Bade.UI.Web.Base.Manager;

namespace Bade.UI.Web.Base.Infrastructure
{
    public static class Current
    {

        private static int _applicationId;

        public static int ApplicationId
        {
            get
            {
                if (_applicationId == 0)
                    _applicationId = Convert.ToInt32(ConfigurationManager.AppSettings["ApplicationId"]);

                return _applicationId;
            }
        }

        /// <summary>
        /// Gets or sets the current user of the console application.
        /// </summary>
        public static AccountInfo Account
        {
            get
            {
                return SessionManager.Get<AccountInfo>(Keys.Session.CurrentAccountInfo);
            }
            set
            {
                SessionManager.Set(Keys.Session.CurrentAccountInfo, value);
            }
        }

        public static bool IsAccountLogin
        {
            get { return Account != null && Account.LoginStatus; }
        }
    }
}