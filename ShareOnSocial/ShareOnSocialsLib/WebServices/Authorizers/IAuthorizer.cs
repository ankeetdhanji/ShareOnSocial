using System;
using System.Threading.Tasks;

namespace ShareOnSocialsLib.WebServices.Authorizers
{
    public interface IAuthorizer
    {
        string GenerateHeader(string url);
        Task<string> GetAuthorizeURL();
        Task<bool> Authorize(string pin);
    }
}
