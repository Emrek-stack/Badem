namespace Bade.Constants.Structs
{
    public class Keys
    {
        public struct Session
        {
            public const string CurrentAccountInfo = "CurrentAccountInfo";
            public const string AfterLoginReturnUrl = "AfterLoginReturnUrl";
        }

        public struct Security
        {
            public const string CommonScreens = "CommonScreens";
            public const string Admin = "Administrator";
        }

        public struct Request
        {
            public const string ReturnUrl = "returnurl";
        }

        public struct PageUrl
        {
            public const string AccountLogin = "~/account/login";
            public const string AccountLogout = "~/account/logout";
            public const string AccessDenied = "~/account/login";
        }

        public struct Cookie
        {
            public const string UserName = "aun";
        }

        public struct ViewData
        {
            public const string Fail = "Fail";
        }
    }
}