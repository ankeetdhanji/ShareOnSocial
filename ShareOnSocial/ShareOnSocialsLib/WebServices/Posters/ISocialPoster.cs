using System.Threading.Tasks;

namespace ShareOnSocialsLib.WebServices.Posters
{
    /// <summary>
    /// Social poster.
    /// 
    /// Allows the user to make posts to their social media accounts.
    /// </summary>
    public interface ISocialPoster
    {
        /// <summary>
        /// Checks to see if the social account credentials are still valid.
        /// 
        /// </summary>
        /// <returns><c>true</c>, credientials are still value, <c>false</c> otherwise.</returns>
        bool IsAuthorized();

        /// <summary>
        /// Gets the url to allow the user to login to twitter.
        /// </summary>
        /// <returns>Twitter login url</returns>
        Task<string> GetAuthorizeURL();

        /// <summary>
        /// Authorize the social account with pin supplied.
        /// </summary>
        /// <returns><c>true</c>, if authorized successfully, <c>false</c> otherwise.</returns>
        /// <param name="pin">Pin.</param>
        Task<bool> Authorize(string pin);

        /// <summary>
        /// Post the specified message and url supplied.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="attachmenturl">Attachment url.</param>
        void Post(string message, string attachmenturl = null);
    }
}
