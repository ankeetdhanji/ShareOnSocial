using System;
using FakeItEasy;
using NUnit.Framework;
using ShareOnSocialsLib.UnitTests.TestHelpers;
using ShareOnSocialsLib.WebServices.Http;
using ShareOnSocialsLib.WebServices.Authorizers;

namespace ShareOnSocialsLib.UnitTests.Components.WebServices.Twitter
{
    [TestFixture]
    public class TwitterAuthorizerTests
    {
        [Test]
        public void GenerateAuthorizationLink_HttpManagerReturnsToken_CorrectURLReturned()
        {
            string authtoken = "oauth_token=qFtCYAAAAAAA3c-vAAABYBz2cMM";
            string expectedurl = string.Format("https://api.twitter.com/oauth/authorize?{0}", authtoken);
            string returnedtoken = string.Format("{0}&oauth_token_secret=1MvKrgZWqp6ovPW8fhunhHpXSR06aCbi&oauth_callback_confirmed=true", authtoken);
            IHttpManager fakehttpmanager = A.Fake<IHttpManager>();
            A.CallTo(() => fakehttpmanager.SendWithAuthHeader(A<string>.Ignored, A<string>.Ignored, A<string>.Ignored))
             .Returns(returnedtoken);

            TwitterAuthorizer twitterauthorizer = GenerateTwitterAuthorizer(fakehttpmanager);

            string result = twitterauthorizer.GetAuthorizeURL().Result;

            Assert.That(result, Is.EqualTo(expectedurl));

        }

        [TestCase("oauth_consumer_key=")]
        [TestCase("oauth_nonce=")]
        [TestCase("oauth_signature_method=\"HMAC-SHA1\"")]
        [TestCase("oauth_timestamp=")]
        [TestCase("oauth_version=\"1.0\"")]
        [TestCase("oauth_token=")]
        [TestCase("oauth_signature=")]
        public void GenerateHeader_URLSupplied_ConsumerKeyIsInHeader(string expectedvalue)
        {
            IAuthorizer authorizer = GenerateTwitterAuthorizer();
            string result = authorizer.GenerateHeader("someurl");

            Assert.That(result, Contains.Substring(expectedvalue));
        }

        private TwitterAuthorizer GenerateTwitterAuthorizer(params object[] fakes)
        {
            IHttpManager fakemanager = FakeFactory.FindOrCreate<IHttpManager>(fakes);
            return new TwitterAuthorizer(fakemanager);
        }
    }
}
