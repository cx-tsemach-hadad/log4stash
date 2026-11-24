using System;
using System.Text;
using RestSharp;
using RestSharp.Authenticators;

namespace log4stash.Authentication
{
    public class BasicAuthenticationMethod : IAuthenticator
    {
        public string Password { get; set; }

        public string Username { get; set; }

        public string CreateAuthenticationHeader(RequestData requestData)
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                return ElasticsearchBasicAuth.AuthorizationHeader();
            }

            var authInfo = string.Format("{0}:{1}", Username, Password);
            var encodedAuthInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
            var authorizationHeaderValue = string.Format("{0} {1}", "Basic", encodedAuthInfo);
            return authorizationHeaderValue;
        }

        public void Authenticate(IRestClient client, IRestRequest request)
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(Password))
            {
                request.AddHeader("Authorization", ElasticsearchBasicAuth.AuthorizationHeader());
                return;
            }

            var authInfo = string.Format("{0}:{1}", Username, Password);
            var encodedAuthInfo = Convert.ToBase64String(Encoding.ASCII.GetBytes(authInfo));
            var authorizationHeaderValue = string.Format("{0} {1}", "Basic", encodedAuthInfo);
            request.AddHeader("Authorization", authorizationHeaderValue);
        }
    }
}