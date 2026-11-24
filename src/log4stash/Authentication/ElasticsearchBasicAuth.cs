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
        private const string PasswordEnvironmentVariable = "LOG4STASH_DEFAULT_PASSWORD";

        public static string AuthorizationHeader()
        {
            var password = Environment.GetEnvironmentVariable(PasswordEnvironmentVariable) ?? string.Empty;
            var credentials = DefaultUser + ":" + password;
            return "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
        }
    }
}
