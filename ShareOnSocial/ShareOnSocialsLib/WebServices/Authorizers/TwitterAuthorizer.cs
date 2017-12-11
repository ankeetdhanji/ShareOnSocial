using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ShareOnSocialsLib.Utilities;
using ShareOnSocialsLib.WebServices.Http;

namespace ShareOnSocialsLib.WebServices.Authorizers
{
    public class TwitterAuthorizer : IAuthorizer
    {

        private string mConsumerKey = "<REPLACE WITH KEY>";
        private string mConsumerSecret = "<REPLACE WITH SECRET>";

        private string mTmpTokenKey;
        private string mTmpTokenSecret;

        private string mTokenKey;
        private string mTokenSecret;

        private IHttpManager mHttpManager;


        public TwitterAuthorizer(IHttpManager httpmanager)
        {
            mHttpManager = httpmanager;
        }

        public async Task<string> GetAuthorizeURL()
        {
            string url = "https://api.twitter.com/oauth/request_token";
            string authheader = GenerateHeader(url);

            string reply = await mHttpManager.SendWithAuthHeader("POST", url, authheader);

            if (reply.Contains("&")) 
            {
                string[] tokens = reply.Split('&');
                mTmpTokenKey = tokens[0];
                mTmpTokenSecret = tokens[1];
                mTokenKey = null;
                mTokenSecret = null;
                return "https://api.twitter.com/oauth/authorize?" + mTmpTokenKey;
            }
            return null;
        }

        public async Task<bool> Authorize(string pin)
        {
            string url = "https://api.twitter.com/oauth/access_token&oauth_verifier=" + pin;
            string authheader = GenerateHeader(url);

            string reply = await mHttpManager.SendWithAuthHeader("POST", url, authheader);
            if (reply.Contains("&"))
            {
                string[] tokens = reply.Split('&');
                mTmpTokenKey = null;
                mTmpTokenSecret = null;
                mTokenKey = tokens[0];
                mTokenSecret = tokens[1];
                return true;
            }

            return false;
        }

        public string GenerateHeader(string url)
        {
            TimeSpan timespan = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc));
            int timestamp = (int)timespan.TotalSeconds;
            SortedDictionary<string, string> dictionary = new SortedDictionary<string, string>();
            dictionary.Add("oauth_consumer_key", mConsumerKey);
            dictionary.Add("oauth_nonce", Guid.NewGuid().ToString("N"));
            dictionary.Add("oauth_signature_method", "HMAC-SHA1");
            dictionary.Add("oauth_timestamp", timestamp.ToString());
            dictionary.Add("oauth_version", "1.0");
            string signature = "";
            if (mTmpTokenKey != null || mTokenKey != null)
            {
                string tokenkey = (mTmpTokenKey != null) ? mTmpTokenKey : mTokenKey;
                string tokensecret = (mTmpTokenKey != null) ? mTmpTokenSecret : mTokenSecret;
                dictionary.Add("oauth_token", tokenkey);
                signature = SignatureGenerator.Generate("POST", url, dictionary, mConsumerSecret, tokensecret);
            
            }
            else
            {
                signature = SignatureGenerator.Generate("POST", url, dictionary, mConsumerSecret);
            }
            StringBuilder builder = new StringBuilder();
            foreach (string key in dictionary.Keys)
            {
                builder.Append(string.Format("{0}=\"{1}\", ", key, dictionary[key]));
            }
            builder.Append(string.Format("oauth_signature=\"{0}\"", signature));
            string authheader = builder.ToString();

            return authheader;

        }
    }
}
