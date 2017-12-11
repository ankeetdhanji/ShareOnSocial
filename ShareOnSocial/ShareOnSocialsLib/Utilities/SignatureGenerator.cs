using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ShareOnSocialsLib.Utilities
{
    public class SignatureGenerator
    {
        private static StringBuilder mBuilder = new StringBuilder();
        
        public static string Generate(string type, string fullurl, SortedDictionary<string, string> parametersdict, string consumersecret, string usersecret = null)
        {
            type = type.ToUpper();

            string url = null;
            string extraparams = null;
            if (fullurl.Contains("?"))
            {
                // we have extra params
                int splitindex = fullurl.IndexOf("?");
                url = fullurl.Substring(0, splitindex);
                extraparams = fullurl.Substring(splitindex+1);
            }
            else
            {
                url = fullurl;
            }
            url = Uri.EscapeDataString(url);

            string parameters = Uri.EscapeDataString(GenerateParametersString(parametersdict, extraparams));

            // create secret for hashing
            string secret = Uri.EscapeDataString(consumersecret) + "&";
            if (!String.IsNullOrEmpty(usersecret))
            {
                secret += Uri.EscapeDataString(usersecret);
            }

            string basesignature = string.Format("{0}&{1}&{2}", type, url, parameters);

            // hash the base signature.
            using (HMACSHA1 hasher = new HMACSHA1(Encoding.ASCII.GetBytes(secret)))
            {
                byte[] buffer = hasher.ComputeHash(Encoding.ASCII.GetBytes(basesignature));
                string signature = Convert.ToBase64String(buffer);

                return Uri.EscapeDataString(signature);
            }
        }

        private static string GenerateParametersString(SortedDictionary<string, string> parametersdict, string extraparams)
        {
            mBuilder.Clear();
            foreach (string key in parametersdict.Keys)
            {
                mBuilder.Append(key + "=" + parametersdict[key] + "&");
            }
            if (extraparams != null)
            {
                string[] tokens = extraparams.Split('&');
                foreach (string token in tokens)
                {
                    // token expected to be key=value
                    // need to escape the value.
                    int splitindex = token.IndexOf("=");
                    string key = token.Substring(0, splitindex);
                    string value = token.Substring(splitindex + 1);
                    mBuilder.Append(key + "=" + Uri.EscapeDataString(value) + "&");
                }
            }
            mBuilder.Remove(mBuilder.Length - 1, 1);
            return mBuilder.ToString();
        }
    }
}
