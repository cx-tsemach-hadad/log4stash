using System;
using System.Text;
using FluentAssertions;
using log4stash.Authentication;
using NUnit.Framework;

namespace log4stash.UnitTests
{
    class BasicAuthenticationTests
    {
        [Test]
        public void DEFAULT_AUTHORIZATION_HEADER_HAS_BASIC_PREFIX_AND_USER_PASSWORD_PAYLOAD()
        {
            const string variable = "LOG4STASH_DEFAULT_PASSWORD";
            var previous = Environment.GetEnvironmentVariable(variable);
            Environment.SetEnvironmentVariable(variable, "test-password");
            try
            {
                var header = ElasticsearchBasicAuth.AuthorizationHeader();

                header.Should().StartWith("Basic ");
                var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(header.Substring("Basic ".Length)));
                decoded.Should().StartWith("log_shipper:");
                decoded.Length.Should().BeGreaterThan("log_shipper:".Length);
            }
            finally
            {
                Environment.SetEnvironmentVariable(variable, previous);
            }
        }

        [Test]
        public void CONFIGURED_CREDENTIALS_ARE_USED_WHEN_PROVIDED()
        {
            var method = new BasicAuthenticationMethod { Username = "alice", Password = "s3cret" };

            var header = method.CreateAuthenticationHeader(new RequestData());

            var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(header.Substring("Basic ".Length)));
            decoded.Should().Be("alice:s3cret");
        }

        [Test]
        public void MISSING_CREDENTIALS_FALL_BACK_TO_DEFAULT_HEADER()
        {
            var method = new BasicAuthenticationMethod();

            method.CreateAuthenticationHeader(new RequestData()).Should().Be(ElasticsearchBasicAuth.AuthorizationHeader());
        }
    }
}
