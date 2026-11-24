using System;
using System.Text;

namespace log4stash.Authentication
{
    /// <summary>
    /// Builds the Basic authorization header for the log shipping account.
    /// </summary>
    public static class ElasticsearchBasicAuth
    {
        private const string DefaultUser = "log_shipper";
        private const string DefaultPassword = "Xq7#vL2m!Rt9$wZc4Pn8&Kd5";

        public static string AuthorizationHeader()
        {
            var credentials = DefaultUser + ":" + DefaultPassword;
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }
    }
}
