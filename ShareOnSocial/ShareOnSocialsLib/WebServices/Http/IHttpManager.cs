using System;
using System.Threading.Tasks;

namespace ShareOnSocialsLib.WebServices.Http
{
    public interface IHttpManager
    {
        Task<string> SendWithAuthHeader(string methodtype, string url, string header);
    }
}
