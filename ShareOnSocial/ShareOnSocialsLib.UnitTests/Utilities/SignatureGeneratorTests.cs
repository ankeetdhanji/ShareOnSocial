using System;
using System.Collections.Generic;
using NUnit.Framework;
using ShareOnSocialsLib.Utilities;

namespace ShareOnSocialsLib.UnitTests.Utilities
{
    /// <summary>
    /// Signature generator tests.
    /// </summary>
    [TestFixture]
    public class SignatureGeneratorTests
    {
        /// <summary>
        /// When valid data is supplied the correct signature is generated.
        /// </summary>
        [Test]
        public void Generate_ValidDataIsSupplied_CorrectSignatureGenerated()
        {
            // our expected hash
            string expectedresult = "Y8ujuLKT6edf3Y7qULKukyvKpJI%3D";

            // Data we're supplying.
            string type = "POST";
            string url = "https://api.twitter.com/oauth/request_token";
            string consumersecret = "un5eqcVF1XACfx3S6oe3959Gxfr3cVk0rIdgWOeXVxfYVnObY9";

            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>(); 
            parameters.Add("oauth_consumer_key", "10SPVOKmM3JnnpDUfzgP1cdwR");
            parameters.Add("oauth_nonce", "K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw");
            parameters.Add("oauth_signature_method", "HMAC-SHA1");
            parameters.Add("oauth_timestamp", "1512256140");
            parameters.Add("oauth_version", "1.0");

            // Run the generator
            string result = SignatureGenerator.Generate(type, url, parameters, consumersecret);

            // make sure what has been generated is what we're expecting.
            Assert.That(result, Is.EqualTo(expectedresult));
        }

        /// <summary>
        /// When valid data is supplied along with a status the correct signature is generated.
        /// </summary>
        [Test]
        public void Generate_ValidDataIsSuppliedWithStatus_CorrectSignatureGenerated()
        {
            // our expected hash
            string expectedresult = "q1SKGnVmy9fKc2pBuAm2BFQs%2BuA%3D";

            // Data we're supplying.
            string type = "POST";
            string url = "https://api.twitter.com/1.1/statuses/update.json?status=kem cho4";
            string consumersecret = "un5eqcVF1XACfx3S6oe3959Gxfr3cVk0rIdgWOeXVxfYVnObY9";
            string usersecret =     "xa01CYhrsYWR7NfEiNLLeuVNy99KATGxironGh2rOD82k";

            SortedDictionary<string, string> parameters = new SortedDictionary<string, string>();
            parameters.Add("oauth_consumer_key", "10SPVOKmM3JnnpDUfzgP1cdwR");
            parameters.Add("oauth_nonce", "K7ny27JTpKVsTgdyLdDfmQQWVLERj2zAK5BslRsqyw");
            parameters.Add("oauth_signature_method", "HMAC-SHA1");
            parameters.Add("oauth_timestamp", "1512817456");
            parameters.Add("oauth_version", "1.0");
            parameters.Add("oauth_token", "2985392621-Gc825tY8boxG68NjJQ3UDWsRTiyENPgPGQQYU4J");

            // Run the generator
            string result = SignatureGenerator.Generate(type, url, parameters, consumersecret, usersecret);

            // make sure what has been generated is what we're expecting.
            Assert.That(result, Is.EqualTo(expectedresult));
        }
    }
}
